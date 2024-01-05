using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SignalR_App.Application.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        List<string> _permissions { get; set; } = new();
        public CustomAuthorizeAttribute() { }
        public CustomAuthorizeAttribute(params string[] permissions)
        {
            _permissions.AddRange(permissions.ToList());
        }
        public CustomAuthorizeAttribute(string permission)
        {
            _permissions.Add(permission);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool? isAuthenticate = context?.HttpContext?.User?.Identity?.IsAuthenticated;
            if (isAuthenticate.HasValue && !isAuthenticate.Value)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new { error = "Erişim engellendi." });
                return;
            }

            var claims = context.HttpContext.User.Claims.Select(c => c.Value).ToList();

            foreach (var permission in _permissions.ToList())
            {
                if (claims.Contains(permission)) return;
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new UnauthorizedObjectResult(new
                    { error = $"{permission} yetkisine sahip olmadığınız için giriş yapamazsınız." });
                }
            }
        }
    }
}
