using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

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
            var attribute = context.ActionDescriptor.FilterDescriptors
                .Where(c => c.Scope == FilterScope.Action)
                .Select(c => c.Filter)
                .OfType<AllowAnonymousAttribute>()?.FirstOrDefault();

            if (attribute != null) return;

            var token = context?.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new { error = "Erişim engellendi." });
                return;
            }

            //todo:hakan buradaki bilgiler configuration dosyasından çekilir hale getirilecek.

            var issuer = "signalrapp.com";
            var audience = "https://localhost:7215";
            var securityKey = "secretkey123578@Gaserikaqwertyukey";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
            };

            token = token.Replace("Bearer ", "").Replace("bearer ", "").Replace(" ", "");

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken validatedToken);
            if (validatedToken is null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new { error = "Erişim engellendi." });
                return;
            }

            var claims = claimsPrincipal.Claims.Select(c => c.Value).ToList();

            if (!_permissions.Any() && validatedToken is null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new UnauthorizedObjectResult(new { error = $"geçerli izinlere sahip olmalısınız." });
            }

            foreach (var permission in _permissions.ToList())
            {
                if (claims.Contains(permission)) return;
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new UnauthorizedObjectResult(new
                    { error = $"{permission} yetkisine sahip olmalısınız." });
                }
            }
        }
    }
}
