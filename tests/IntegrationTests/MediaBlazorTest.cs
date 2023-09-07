using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaBlazorTest
{
    private readonly WebApplicationFactory<KristofferStrube.Blazor.MediacaptureStreams.HostedTestPages.Server.Program> webApplicationFactory = new();
    private HttpClient? httpClient;

    protected static readonly Uri RootUri = new("http://127.0.0.1");
    protected virtual string[] Args => new string[] { "--use-fake-device-for-media-stream", "--use-fake-ui-for-media-stream" };
protected IPlaywright PlaywrightInstance { get; set; }
    protected IBrowserContext Context { get; set; }
    protected IPage Page { get; set; }

    [SetUp]
    public async Task Setup()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        IBrowser browser = await PlaywrightInstance.Chromium.LaunchAsync(new()
        {
            Args = Args
        });
        // Create a new incognito browser context
        Context = await browser.NewContextAsync();
        // Create a new page inside context.
        Page = await Context.NewPageAsync();

        httpClient = webApplicationFactory.CreateClient(new() { BaseAddress = RootUri });

        await Context.RouteAsync("**", async (route) =>
        {
            IRequest request = route.Request;

            ByteArrayContent? content = request.PostDataBuffer is { } postDataBuffer
                ? new ByteArrayContent(postDataBuffer)
                : null;

            HttpRequestMessage requestMessage = new(new(request.Method), request.Url)
            {
                Content = content
            };

            foreach (KeyValuePair<string, string> header in request.Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            byte[] responseBody = await response.Content.ReadAsByteArrayAsync();
            IEnumerable<KeyValuePair<string, string>> responseHeaders = response.Content.Headers
                .Select(header => new KeyValuePair<string, string>(header.Key, header.Value.First()));

            await route.FulfillAsync(new()
            {
                BodyBytes = responseBody,
                Headers = responseHeaders,
                Status = (int)response.StatusCode
            });

        });
    }

    [TearDown]
    public void TearDown()
    {
        httpClient?.Dispose();
        PlaywrightInstance.Dispose();
    }

    protected static ILocatorAssertions Expect(ILocator locator)
    {
        return Assertions.Expect(locator);
    }

    protected static IPageAssertions Expect(IPage page)
    {
        return Assertions.Expect(page);
    }

    protected static IAPIResponseAssertions Expect(IAPIResponse response)
    {
        return Assertions.Expect(response);
    }
}