using HostedTestPages.Client;
using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("KristofferStrube.Blazor.MediaCaptureStreams.HostedTestPages.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("KristofferStrube.Blazor.MediaCaptureStreams.HostedTestPages.ServerAPI"));
builder.Services.AddMediaDevicesService();

WebAssemblyHost app = builder.Build();

await app.Services.SetupErrorHandlingJSInterop();

await app.RunAsync();
