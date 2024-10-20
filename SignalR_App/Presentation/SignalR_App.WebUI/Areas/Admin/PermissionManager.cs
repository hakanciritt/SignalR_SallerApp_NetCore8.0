using System;
namespace SignalR_App.WebUI.Areas.Admin
{
	public class PermissionManager
	{
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionManager(IHttpContextAccessor httpContextAccessor)
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

