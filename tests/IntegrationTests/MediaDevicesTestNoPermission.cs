namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesTestNoPermission : MediaBlazorTest
{
    protected override string[] Args => Array.Empty<string>(); 

    [Test]
    public async Task GetUserMedia_ThrowsException_WhenDismissing()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream");

        // Assert
        await Expect(Page.GetByTestId("result")).ToHaveTextAsync("Not supported");
    }
}
