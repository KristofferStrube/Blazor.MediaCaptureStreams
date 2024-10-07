using KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;
using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesTest : MediaBlazorTest
{
    protected override string[] Args => ["--use-fake-device-for-media-stream", "--use-fake-ui-for-media-stream"];

    [Test]
    public async Task GetUserMedia_ReturnsOneAudioTrack_WhenQueryingAudio()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using MediaDevices mediaDevices = await EvaluationContext.MediaDevicesService.GetMediaDevicesAsync();
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });
            MediaStreamTrack[] audioTracks = await mediaStream.GetAudioTracksAsync();
            return audioTracks.Length;
        };

        // Act
        await DoneLoadingPageAsync();

        // Assert
        Assert.That(EvaluationContext.Result, Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserMedia_ReturnsOneVideoTrack_WhenQueryingVideo()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using MediaDevices mediaDevices = await EvaluationContext.MediaDevicesService.GetMediaDevicesAsync();
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Video = true });
            MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
            return videoTracks.Length;
        };

        // Act
        await DoneLoadingPageAsync();

        // Assert
        Assert.That(EvaluationContext.Result, Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserMedia_Throws_TypeErrorException_WhenNoConstraintsDefined()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using MediaDevices mediaDevices = await EvaluationContext.MediaDevicesService.GetMediaDevicesAsync();
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { });
            MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
            return videoTracks.Length;
        };

        // Act
        await DoneLoadingPageAsync();

        // Assert
        Assert.That(EvaluationContext.Exception, Is.InstanceOf<TypeErrorException>());
    }

    [Test]
    public async Task GetUserMedia_Throws_OverconstrainedErrorException_WhenOverconstrained()
    {
        // Arrange
        AfterRenderAsync = async () =>
        {
            await using MediaDevices mediaDevices = await EvaluationContext.MediaDevicesService.GetMediaDevicesAsync();

            // Get video track
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Video = true });
            MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
            MediaStreamTrack videoTrack = videoTracks.Single();

            // Get max video width
            MediaTrackCapabilities capabilities = await videoTrack.GetCapabilitiesAsync();
            ulong maxWidth = capabilities.Width?.Max ?? 0;

            // Get video with a requested width larger than the max capability
            return await mediaDevices.GetUserMediaAsync(new()
            {
                Video = new MediaTrackConstraints()
                {
                    Width = new ConstrainULongRange() { Exact = maxWidth + 1 }
                }
            });
        };

        // Act
        await DoneLoadingPageAsync();

        // Assert
        Assert.That(EvaluationContext.Exception, Is.InstanceOf<OverconstrainedErrorException>());
    }
}
