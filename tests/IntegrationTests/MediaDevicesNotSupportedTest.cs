using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests;

public class MediaDevicesNotSupportedTest(string browserName) : BlazorTest(browserName)
{
    protected override string[] Args => [];

    [Test]
    public async Task GetUserMedia_Throws_NotSupportedErrorException_WhenNotSupported()
    {
        // Arrange
        await using MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();

        // Assert
        Assert.ThrowsAsync<NotSupportedErrorException>(async () => await mediaDevices.GetUserMediaAsync(new() { Audio = true }));
    }
}
