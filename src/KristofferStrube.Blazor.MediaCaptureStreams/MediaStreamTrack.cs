using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaStreamTrack : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    public static Task<MediaStreamTrack> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamTrack(jSRuntime, jSReference));
    }

    protected MediaStreamTrack(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public async Task<MediaStreamTrackKind> GetKindAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        string kind = await helper.InvokeAsync<string>("getAttribute", JSReference, "kind");
        return MediaStreamTrackKindExtensions.ParseMediaStreamTrackKind(kind);
    }

    public async Task<string> GetIdAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "id");
    }

    public async Task<string> GetLabelAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "label");
    }

    public async Task<MediaTrackConstraints> GetConstraintsAsync()
    {
        IJSObjectReference result = await JSReference.InvokeAsync<IJSObjectReference>("getConstraints");
        var newMediaTrackConstraints = new MediaTrackConstraints();
        await MediaTrackConstraints.HydrateMediaTrackConstraints(newMediaTrackConstraints, JSRuntime, result);
        return newMediaTrackConstraints;
    }

    public async Task StopAsync()
    {
        await JSReference.InvokeVoidAsync("stop");
    }

    public async Task<MediaTrackSettings> GetSettingsAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackSettings>("getSettings");
    }

    public async Task ApplyContraintsAsync(MediaTrackConstraints? constraints = null)
    {
        await JSReference.InvokeVoidAsync("applyConstraints", constraints);
    }
}
