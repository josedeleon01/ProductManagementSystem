using System.Net.Http.Headers;
using System.Net;
using ProductManagementSystem.Client.Interfaces.Users;

namespace ProductManagementSystem.Client.Handlers
{
    public class AuthenticationHandler(IUserService authenticationService, IConfiguration configuration) : DelegatingHandler
    {
        private bool _refreshing;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await authenticationService.GetJwtAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(configuration["ApiURL:Url"] ?? "") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await base.SendAsync(request, cancellationToken);

            if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    _refreshing = true;

                    if (await authenticationService.RefreshAsync())
                    {
                        jwt = await authenticationService.GetJwtAsync();

                        if (isToServer && !string.IsNullOrEmpty(jwt))
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
                finally
                {
                    _refreshing = false;
                }
            }

            return response;
        }
    }
}
