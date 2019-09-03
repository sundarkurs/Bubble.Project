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
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                ExpireTimeSpan = TimeSpan.FromMinutes(10),
                SlidingExpiration = true
            });

            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            var baseAddress = ConfigurationManager.AppSettings["Identity.Host.Url"];


            var clientId = ConfigurationManager.AppSettings["Identity.ClientId"];

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                SignInAsAuthenticationType = "Cookies",
                Authority = baseAddress,
                ClientId = clientId,
                RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutRedirectUri"],
                
                ResponseType = "id_token token",
                Scope = ConfigurationManager.AppSettings["Scope"],
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var claimsToExclude = new[]
                        {
                            "aud", "iss", "nbf", "exp", "nonce", "iat", "at_hash"
                        };

                        var claimsToKeep =
                            n.AuthenticationTicket.Identity.Claims
                                .Where(x => false == claimsToExclude.Contains(x.Type)).ToList();
                        claimsToKeep.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

                        if (n.ProtocolMessage.AccessToken != null)
                        {
                            claimsToKeep.Add(new Claim("access_token", n.ProtocolMessage.AccessToken));

                            var userInfoClient = new UserInfoClient(new Uri($"{baseAddress}/connect/userinfo"),
                                n.ProtocolMessage.AccessToken);
                            var userInfoResponse = await userInfoClient.GetAsync();
                            var userInfoClaims = userInfoResponse.Claims
                                .Where(x => x.Item1 != "sub") // filter sub since we're already getting it from id_token
                                .Select(x => new Claim(x.Item1, x.Item2));
                            claimsToKeep.AddRange(userInfoClaims);
                        }

                        var ci = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType, "name", "role");
                        ci.AddClaims(claimsToKeep);

                        n.AuthenticationTicket = new AuthenticationTicket(
                            ci, n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idToken = n.OwinContext.Authentication.User.FindFirst("id_token")?.Value;
                            n.ProtocolMessage.IdTokenHint = idToken;
                        }

                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType != OpenIdConnectRequestType.LogoutRequest)
                        {
                            return Task.FromResult(0);
                        }

                        if (!n.OwinContext.Authentication.User.Claims.Any())
                        {
                            return Task.FromResult(0);
                        }

                        var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                        n.ProtocolMessage.IdTokenHint = idTokenHint;

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}