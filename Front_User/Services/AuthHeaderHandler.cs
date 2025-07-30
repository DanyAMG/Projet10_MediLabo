using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

/// <summary>
/// A custom HTTP message handler that adds the JWT token from localStorage to outgoing HTTP requests.
/// </summary>
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    /// <summary>
    /// Initializes a new instance of the AuthHeaderHandler class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime used to access the browser's localStorage.</param>
    public AuthHeaderHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Intercepts HTTP requests and adds the Authorization header if a JWT token is found in localStorage.
    /// </summary>
    /// <param name="request">The outgoing HTTP request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The HTTP response message returned by the inner handler.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("//////////////// AuthHeaderHandler appelé  ///////////////");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");

        if (!string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"//////////////// Token trouvé : {token}  ///////////////");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("//////////////// Pas de token trouvé  ///////////////");
        }
            return await base.SendAsync(request, cancellationToken);
    }
}