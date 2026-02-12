namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaStreamTrackTests(string browserName) : BlazorTest(browserName)
{
    [Test]
    public async Task GetCapabilities_ReturnsCapabilities()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();
        await using MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new() { Audio = true });
        MediaStreamTrack[] audioTracks = await mediaStream.GetAudioTracksAsync();
        MediaStreamTrack audioTrack = audioTracks.Single();

        // Act
        var capabilities = await audioTrack.GetCapabilitiesAsync();

        // Assert
        Assert.That(capabilities.EchoCancellation, Is.Not.Null);
    }
}
