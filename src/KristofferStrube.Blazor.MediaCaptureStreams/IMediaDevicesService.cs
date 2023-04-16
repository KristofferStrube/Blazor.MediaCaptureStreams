using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public interface IMediaDevicesService
{
    Task<MediaDevices> GetMediaDevicesAsync();
}