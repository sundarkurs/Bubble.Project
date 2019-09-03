using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NG.Website.Base
{

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var claims = ((System.Security.Claims.ClaimsPrincipal)((System.Web.HttpContextWrapper)httpContext).User).Claims.ToList();
                var userRoles = claims.Where(s => s.Type == "usertype").Select(v => v.Value).ToList();

                foreach (var role in _roles)
                {
                    if (userRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
                filterContext.HttpContext.Response.StatusCode = 403;
            }
        }

    }
}