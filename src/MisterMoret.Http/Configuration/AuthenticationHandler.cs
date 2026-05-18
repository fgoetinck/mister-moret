using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MisterMoret.Http.Configuration.Interfaces;

namespace MisterMoret.Http.Configuration;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly string _scheme;

    public AuthenticationHandler(IAccessTokenProvider accessTokenProvider, string scheme)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scheme);
        
        _accessTokenProvider = accessTokenProvider;
        _scheme = scheme;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_accessTokenProvider.AccessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(_scheme, _accessTokenProvider.AccessToken);
        }

        return base.SendAsync(request, cancellationToken);
    }
}