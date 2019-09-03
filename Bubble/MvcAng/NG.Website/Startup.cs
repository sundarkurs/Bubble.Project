using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(NG.Website.Startup))]

namespace NG.Website
{
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            var config = new
            {
                Authority = ConfigurationManager.AppSettings["Identity.Host.Url"],
                ClientId = ConfigurationManager.AppSettings["Identity.ClientId"],
                RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutRedirectUri"],
                Scope = ConfigurationManager.AppSettings["Scope"],
                UserInfoUri = $"{ConfigurationManager.AppSettings["Identity.Host.Url"]}/connect/userinfo"
            };

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                ExpireTimeSpan = TimeSpan.FromMinutes(10),
                SlidingExpiration = true
            });

            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                SignInAsAuthenticationType = "Cookies",
                Authority = config.Authority,
                ClientId = config.ClientId,
                RedirectUri = config.RedirectUri,
                PostLogoutRedirectUri = config.PostLogoutRedirectUri,
                ResponseType = "id_token token",
                Scope = config.Scope,
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async noti =>
                    {
                        var claimsToExclude = new[] {
                            "aud", "iss", "nbf", "exp", "nonce", "iat", "at_hash"
                        };

                        var claimsToKeep = noti.AuthenticationTicket.Identity.Claims
                                .Where(x => false == claimsToExclude.Contains(x.Type)).ToList();

                        claimsToKeep.Add(new Claim("id_token", noti.ProtocolMessage.IdToken));

                        if (noti.ProtocolMessage.AccessToken != null)
                        {
                            claimsToKeep.Add(new Claim("access_token", noti.ProtocolMessage.AccessToken));

                            var userInfoClient = new UserInfoClient(new Uri(config.UserInfoUri), noti.ProtocolMessage.AccessToken);
                            var userInfoResponse = await userInfoClient.GetAsync();
                            var userInfoClaims = userInfoResponse.Claims
                                .Where(x => x.Item1 != "sub")
                                .Select(x => new Claim(x.Item1, x.Item2));
                            claimsToKeep.AddRange(userInfoClaims);
                        }

                        var claimsIdentity = new ClaimsIdentity(noti.AuthenticationTicket.Identity.AuthenticationType, "name", "role");
                        claimsIdentity.AddClaims(claimsToKeep);

                        noti.AuthenticationTicket = new AuthenticationTicket(claimsIdentity, noti.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = noti =>
                    {
                        if (noti.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idToken = noti.OwinContext.Authentication.User.FindFirst("id_token")?.Value;
                            noti.ProtocolMessage.IdTokenHint = idToken;
                        }

                        if (noti.ProtocolMessage.RequestType != OpenIdConnectRequestType.LogoutRequest)
                        {
                            return Task.FromResult(0);
                        }

                        if (!noti.OwinContext.Authentication.User.Claims.Any())
                        {
                            return Task.FromResult(0);
                        }

                        var idTokenHint = noti.OwinContext.Authentication.User.FindFirst("id_token").Value;
                        noti.ProtocolMessage.IdTokenHint = idTokenHint;

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}