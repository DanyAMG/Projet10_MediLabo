using Frontend;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<RiskService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddHttpClient("AuthenticatedClient", client =>
{
client.BaseAddress = new Uri("http://localhost:5092");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthenticatedClient"));

await builder.Build().RunAsync();
