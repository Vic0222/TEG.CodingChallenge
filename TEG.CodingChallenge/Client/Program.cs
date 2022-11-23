using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Polly.Extensions.Http;
using Polly;
using TEG.CodingChallenge.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient("Default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddPolicyHandler(GetRetryPolicy());

builder.Services
    .AddScoped<HttpClient>(svc =>
        svc.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));

await builder.Build().RunAsync();



IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(retry * 15));
}