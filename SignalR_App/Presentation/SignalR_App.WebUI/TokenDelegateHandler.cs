
namespace SignalR_App.WebUI
{
    public class TokenDelegateHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            
            return base.SendAsync(request, cancellationToken);
        }
    }
}
