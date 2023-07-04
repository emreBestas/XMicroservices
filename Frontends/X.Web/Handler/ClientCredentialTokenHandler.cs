using System.Net.Http.Headers;
using X.Web.Exceptions;
using X.Web.Services.Interfaces;

namespace X.Web.Handler
{
    public class ClientCredentialTokenHandler:DelegatingHandler
    {
        private readonly IClientCredentialTokenService _credentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService credentialTokenService)
        {
            _credentialTokenService = credentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",await _credentialTokenService.GetTokenAsync());
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ) { throw new UnAuthorizeException(); }
            return response;
        }
    }
}
