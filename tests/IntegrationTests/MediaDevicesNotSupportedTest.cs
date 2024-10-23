using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesNotSupportedTest : MediaBlazorTest
{
    protected override string[] Args => [];

    [Test]
    public async Task GetUserMedia_Throws_NotSupportedErrorException_WhenNotSupported()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using MediaDevices mediaDevices = await EvaluationContext.MediaDevicesService.GetMediaDevicesAsync();
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });
            MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
            return videoTracks.Length;
        };

        // Act
        await DoneLoadingPageAsync();

        // Assert
        Assert.That(EvaluationContext.Exception, Is.InstanceOf<NotSupportedErrorException>().Or.InstanceOf<NotFoundErrorException>());
    }
}
