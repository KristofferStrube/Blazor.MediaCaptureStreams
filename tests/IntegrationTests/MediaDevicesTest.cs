namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesTest : MediaBlazorTest
{
    [Test]
    public async Task GetUserMedia_ReturnsOneAudioTrack_WhenQueryingAudio()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream?getAudio=true");

        // Assert
        await Expect(Page.GetByTestId("result"))
            .ToHaveTextAsync($"audioTracks: 1, videoTracks: 0");
    }

    [Test]
    public async Task GetUserMedia_ReturnsOneVideoTrack_WhenQueryingVideo()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream?getVideo=true");

        // Assert
        await Expect(Page.GetByTestId("result"))
            .ToHaveTextAsync($"audioTracks: 0, videoTracks: 1");
    }

    [Test]
    public async Task GetUserMedia_Throws_TypeErrorException_WhenNoConstraintsDefined()
    {
        // Act
        await Page.GotoAsync(RootUri.AbsoluteUri + "/MediaDevices/GetUserMedia/ReturnsAudioMediaStream");

        // Assert
        await Expect(Page.GetByTestId("result")).ToHaveTextAsync("TypeErrorException");
    }
}
