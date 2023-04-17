using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaDevices : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDevices"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDevices"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaDevices"/>.</returns>
    public static Task<MediaDevices> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaDevices(jSRuntime, jSReference));
    }

    protected MediaDevices(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public async Task<MediaDeviceInfo[]> EnumerateDevicesAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference devices = await JSReference.InvokeAsync<IJSObjectReference>("enumerateDevices");
        int length = await helper.InvokeAsync<int>("getAttribute", devices, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                    {
                        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", devices, i);
                        return await MediaDeviceInfo.CreateAsync(JSRuntime, jSInstance);
                    })
                .ToArray()
        );
    }

    public async Task<MediaStream> GetUserMediaAsync(MediaStreamConstraints? constraints = null)
    {
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getUserMedia", constraints);
        return await MediaStream.CreateAsync(JSRuntime, jSInstance);
    }
}
