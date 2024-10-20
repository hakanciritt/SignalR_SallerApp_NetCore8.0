﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SignalR_App.Application.Filters
{

    public class CustomAuthorizeWebAttribute : Attribute, IAuthorizationFilter
    {
        List<string> _permissions { get; set; } = new();

        public CustomAuthorizeWebAttribute() { }
        public CustomAuthorizeWebAttribute(params string[] permissions)
        {
            _permissions.AddRange(permissions.ToList());
        }
        public CustomAuthorizeWebAttribute(string permission)
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

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            //var token = context?.HttpContext.Request.Headers["Authorization"].ToString();
            var token = context?.HttpContext.Request.Cookies["token"]?.ToString();

            if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new RedirectToRouteResult(new { area = "Admin",controller = "User" , action = "Index" });
                return;
            }

            var issuer = configuration["Jwt:Issuer"] ?? "signalrapp.com";
            var audience = configuration["Jwt:Audience"] ?? "https://localhost:7215";
            var securityKey = configuration["Jwt:SecurityKey"] ?? "secretkey123578@Gaserikaqwertyukey";

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

            token = token.Replace("Bearer", "").Replace("bearer", "").Replace(" ", "");

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken validatedToken);
            if (validatedToken is null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new RedirectToRouteResult(new { area = "Admin", controller = "Home", action = "Index" });
                return;
            }

            var claims = claimsPrincipal.Claims.Select(c => c.Value).ToList();

            if (!_permissions.Any() && validatedToken is null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new RedirectToRouteResult(new { area = "Admin", controller = "Home", action = "Index" });
            }

            foreach (var permission in _permissions.ToList())
            {
                if (claims.Contains(permission)) return;
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new RedirectToRouteResult(new { area = "Admin", controller = "Home", action = "Index" });
                }
            }
        }
    }
}

