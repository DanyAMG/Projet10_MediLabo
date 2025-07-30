using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

/// <summary>
/// DelegatingHandler that injects the Authorization header from the current HTTP context
/// into outgoing HTTP requests.
/// </summary>
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Constructor that receives an IHttpContextAccessor to access the current HTTP request headers.
    /// </summary>
    /// <param name="httpContextAccessor">Provides access to the current HTTP context.</param>
    public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Overrides the SendAsync method to attach the Authorization header (Bearer token)
    /// to outgoing HTTP requests if it exists in the incoming request.
    /// </summary>
    /// <param name="request">The outgoing HTTP request.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The HTTP response message.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("//////////////// AuthHeaderHandler appelé pour le rapport de risque ///////////////");
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(token))
        {
            token = token.Replace("Bearer ", "");
            Console.WriteLine($"//////////////// Token trouvé pour le rapport de risque : {token}  ///////////////");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("//////////////// Pas de token trouvé pour le rapport de risque ///////////////");
        }
            return await base.SendAsync(request, cancellationToken);
    }
}