using Frontend;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7047/") });
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<RiskService>();

await builder.Build().RunAsync();
