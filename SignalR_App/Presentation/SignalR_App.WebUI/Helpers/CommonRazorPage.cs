using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace SignalR_App.WebUI.Helpers
{
    public abstract class CommonRazorPage<TModel> : RazorPage<TModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonRazorPage()
        {

        }
        public CommonRazorPage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsInPermission(string permissionName)
        {
            var claims = _httpContextAccessor?.HttpContext?.User.Claims.Select(c => c.Value).ToList() ?? new();
            return claims.Exists(d => d == permissionName);
            
        }
    }
}

