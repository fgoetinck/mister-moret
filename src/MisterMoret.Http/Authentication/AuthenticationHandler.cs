using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MisterMoret.Http.Authentication;

/// <summary>
/// A <see cref="DelegatingHandler"/> that retrieves a bearer token from <see cref="IAccessTokenProvider"/> and
/// attaches it as an <c>Authorization</c> header on each outgoing HTTP request.
/// </summary>
/// <remarks>
/// When constructed with a client name, the token is looked up by that name via
/// <see cref="IAccessTokenProvider.GetAccessToken(string)"/>. When constructed without a name, the global default
/// token is used via <see cref="IAccessTokenProvider.GetAccessToken()"/>. If no token is available for the
/// configured lookup, the request is forwarded without an <c>Authorization</c> header.
/// </remarks>
public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly string _scheme;
    private readonly string? _clientName;

    /// <summary>
    /// Initializes a new instance of <see cref="AuthenticationHandler"/> that uses the global default access token.
    /// </summary>
    /// <param name="accessTokenProvider">
    /// The provider used to retrieve the access token for each request. Must not be <see langword="null"/>.
    /// </param>
    /// <param name="scheme">
    /// The authentication scheme name placed in the <c>Authorization</c> header (e.g. <c>"Bearer"</c>).
    /// Must not be <see langword="null"/>, empty, or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="scheme"/> is null, empty, or whitespace.</exception>
    public AuthenticationHandler(IAccessTokenProvider accessTokenProvider, string scheme)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scheme);

        _accessTokenProvider = accessTokenProvider;
        _scheme = scheme;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="AuthenticationHandler"/> that uses a client-specific access token
    /// looked up by <paramref name="name"/>.
    /// </summary>
    /// <param name="accessTokenProvider">
    /// The provider used to retrieve the access token for each request. Must not be <see langword="null"/>.
    /// </param>
    /// <param name="scheme">
    /// The authentication scheme name placed in the <c>Authorization</c> header (e.g. <c>"Bearer"</c>).
    /// Must not be <see langword="null"/>, empty, or whitespace.
    /// </param>
    /// <param name="name">
    /// The client name used to look up the token from <paramref name="accessTokenProvider"/>.
    /// Must not be <see langword="null"/>, empty, or whitespace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="scheme"/> or <paramref name="name"/> is null, empty, or whitespace.
    /// </exception>
    public AuthenticationHandler(IAccessTokenProvider accessTokenProvider, string scheme, string name) : this(
        accessTokenProvider, scheme)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        _clientName = name;
    }

    /// <summary>
    /// Retrieves the applicable access token and, when one is available, sets the <c>Authorization</c> header
    /// on <paramref name="request"/> before forwarding it to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The outgoing HTTP request message to authenticate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation of the send operation.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> returned by the inner handler.</returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _clientName == null
            ? _accessTokenProvider.GetAccessToken()
            : _accessTokenProvider.GetAccessToken(_clientName);

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(_scheme, token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}