namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesNotSupportedTest : MediaBlazorTest
{
    protected override string[] Args => Array.Empty<string>(); 

    [Test]
    public async Task GetUserMedia_Throws_NotSupportedErrorException_WhenNotSupported()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream?getAudio=true");

        // Assert
        await Expect(Page.GetByTestId("result")).ToHaveTextAsync("NotSupportedErrorException");
    }
}
