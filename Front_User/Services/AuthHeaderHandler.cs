using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    public AuthHeaderHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

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