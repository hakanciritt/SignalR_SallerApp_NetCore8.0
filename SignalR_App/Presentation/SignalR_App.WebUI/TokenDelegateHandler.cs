
using Microsoft.AspNetCore.Authentication;

namespace SignalR_App.WebUI
{
    public class TokenDelegateHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenDelegateHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _httpContextAccessor.HttpContext?.GetTokenAsync("access_token");

            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("token", out string value))
            {
                request.Headers.Add("Authorization", $"Bearer {value}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
