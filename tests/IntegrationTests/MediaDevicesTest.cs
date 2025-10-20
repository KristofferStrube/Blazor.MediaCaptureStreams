using KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;
using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesTest(string browserName) : BlazorTest(browserName)
{
    [Test]
    public async Task GetUserMedia_ReturnsOneAudioTrack_WhenQueryingAudio()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });

        // Act
        MediaStreamTrack[] audioTracks = await mediaStream.GetAudioTracksAsync();

        // Assert
        Assert.That(audioTracks.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserMedia_ReturnsOneVideoTrack_WhenQueryingVideo()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Video = true });

        // Act
        MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();

        // Assert
        Assert.That(videoTracks.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserMedia_Throws_TypeErrorException_WhenNoConstraintsDefined()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();

        // Assert
        Assert.ThrowsAsync<TypeErrorException>(async () => await mediaDevices.GetUserMediaAsync(new() { }));
    }

    [Test]
    public async Task GetUserMedia_Throws_OverconstrainedErrorException_WhenOverconstrained()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();

        // Get video track
        MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Video = true });
        MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
        MediaStreamTrack videoTrack = videoTracks.Single();

        // Get max video width
        MediaTrackCapabilities capabilities = await videoTrack.GetCapabilitiesAsync();
        ulong maxWidth = capabilities.Width?.Max ?? 0;

        // Assert
        Assert.ThrowsAsync<OverconstrainedErrorException>(async () => await mediaDevices.GetUserMediaAsync(new()
        {
            Video = new MediaTrackConstraints()
            {
                Width = new ConstrainULongRange() { Exact = maxWidth + 1 }
            }
        }));
    }
}
