using BlazorServer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using Microsoft.Playwright;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests.Infrastructure;

[TestFixture("Chrome")]
public class BlazorTest(string browserName)
{
    private IHost? _host;

    protected Uri RootUri;
    protected virtual string[] Args => ["--use-fake-device-for-media-stream", "--use-fake-ui-for-media-stream"];

    protected MediaDevicesEvaluationContext EvaluationContext { get; set; } = default!;
    protected MediaDevicesEvaluationContext EvaluationContextCreator(IServiceProvider sp)
    {
        EvaluationContext = MediaDevicesEvaluationContext.Create(sp);
        return EvaluationContext;
    }

    protected IJSRuntime JSRuntime => EvaluationContext.JSRuntime;
    protected IMediaDevicesService MediaDevicesService => EvaluationContext.MediaDevicesService;

    protected IPlaywright PlaywrightInstance { get; set; }
    protected IBrowser Browser { get; set; }
    protected IBrowserContext Context { get; set; }
    protected IPage Page { get; set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        PlaywrightInstance = await Playwright.CreateAsync();

        IBrowserType browserType = browserName switch
        {
            "Firefox" => PlaywrightInstance.Firefox,
            _ => PlaywrightInstance.Chromium,
        };

        Browser = await browserType.LaunchAsync(new()
        {
            Args = Args,
        });
        // Create a new incognito browser context
        Context = await Browser.NewContextAsync();
        // Create a new page inside context.
        Page = await Context.NewPageAsync();

        _host = BlazorServer.Program.BuildWebHost([],
            serviceBuilder =>
            {
                _ = serviceBuilder
                    .AddScoped(typeof(EvaluationContext), EvaluationContextCreator)
                    .AddMediaDevicesService();
            }
        );

        await _host.StartAsync();

        RootUri = new(_host.Services.GetRequiredService<IServer>().Features
            .GetRequiredFeature<IServerAddressesFeature>()
            .Addresses.Single());
    }

    [SetUp]
    public async Task SetupBeforeEachTestRun()
    {
        await OnAfterRerenderAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        if (Page is not null)
        {
            await Page.CloseAsync();
        }
        if (Context is not null)
        {
            await Context.CloseAsync();
        }
        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }
        if (PlaywrightInstance is not null)
        {
            PlaywrightInstance.Dispose();
        }
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }

    protected async Task OnAfterRerenderAsync()
    {
        _ = await Page.GotoAsync(RootUri.AbsoluteUri);
        await Assertions.Expect(Page.GetByTestId("result")).ToHaveTextAsync($"done");
    }
}