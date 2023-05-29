using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaDevicesService : IMediaDevicesService
{
    private IJSRuntime jSRuntime { get; }

    public MediaDevicesService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
    }

    public async Task<MediaDevices> GetMediaDevicesAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("navigator.mediaDevices.valueOf");
        return await MediaDevices.CreateAsync(jSRuntime, jSInstance);
    }
}
