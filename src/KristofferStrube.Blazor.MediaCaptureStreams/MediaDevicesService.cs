using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <inheritdoc cref="IMediaDevicesService"/>
public class MediaDevicesService : IMediaDevicesService
{
    private readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a new instance of the service.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public MediaDevicesService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
    }

    /// <inheritdoc/>
    public async Task<MediaDevices> GetMediaDevicesAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("navigator.mediaDevices.valueOf");
        return await MediaDevices.CreateAsync(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
