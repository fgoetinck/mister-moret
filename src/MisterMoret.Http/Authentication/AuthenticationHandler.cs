using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MisterMoret.Http.Authentication;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly string _scheme;
    private readonly string _clientName;

    public AuthenticationHandler(IAccessTokenProvider accessTokenProvider, string name, string scheme)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scheme);
        
        _accessTokenProvider = accessTokenProvider;
        _scheme = scheme;
        _clientName = name;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _accessTokenProvider.GetAccessToken(_clientName);
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(_scheme, token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}