using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <inheritdoc cref="IMediaDevicesService"/>
public class MediaDevicesService : IMediaDevicesService
{
    private Lazy<Task<MediaDevices>> mediaDevicesTask;

    /// <summary>
    /// Constructs a new instance of the service.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public MediaDevicesService(IJSRuntime jSRuntime)
    {
        mediaDevicesTask = new(async () =>
        {
            IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("navigator.mediaDevices.valueOf");
            return await MediaDevices.CreateAsync(jSRuntime, jSInstance);
        });
    }

    /// <inheritdoc/>
    public async Task<MediaDevices> GetMediaDevicesAsync()
    {
        return await mediaDevicesTask.Value;
    }
}
