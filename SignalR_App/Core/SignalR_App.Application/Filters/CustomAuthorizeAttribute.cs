﻿using Microsoft.AspNetCore.Authorization;
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
            var result = context.ActionDescriptor.FilterDescriptors
                .Where(c => c.Scope == FilterScope.Action)
                .Select(c => c.Filter)
                .OfType<AllowAnonymousAttribute>();

            if (result.Any()) return;

            bool? isAuthenticate = context?.HttpContext?.User?.Identity?.IsAuthenticated;
            if (isAuthenticate.HasValue && !isAuthenticate.Value)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new { error = "Erişim engellendi." });
                return;
            }

            var claims = context?.HttpContext.User.Claims.Select(c => c.Value).ToList();

            if (!_permissions.Any() && (isAuthenticate.HasValue && isAuthenticate.Value))
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