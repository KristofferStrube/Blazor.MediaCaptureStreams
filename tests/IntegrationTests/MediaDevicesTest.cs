namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesTest : MediaBlazorTest
{
    [Test]
    public async Task GetUserMedia_ReturnsAudioMediaStream_WhenAccepting()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream");

        // Assert
        await Expect(Page.GetByTestId("result")).ToHaveTextAsync("success");
    }
}
