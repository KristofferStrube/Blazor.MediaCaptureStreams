using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaStream : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    public static Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStream(jSRuntime, jSReference));
    }

    public static new async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStream");
        return new MediaStream(jSRuntime, jSInstance);
    }

    public static async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, MediaStream stream)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamFromStreamOrTracks", stream.JSReference);
        return new MediaStream(jSRuntime, jSInstance);
    }

    public static async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, params MediaStreamTrack[] tracks)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamFromStreamOrTracks", tracks.Select(track => track.JSReference).ToArray());
        return new MediaStream(jSRuntime, jSInstance);
    }

    protected MediaStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    public async Task<string> GetIdAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "id");
    }

    public async Task<MediaStreamTrack[]> GetAudioTracksAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference audioTracks = await JSReference.InvokeAsync<IJSObjectReference>("getAudioTracks");
        int length = await helper.InvokeAsync<int>("getAttribute", audioTracks, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                {
                    IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", audioTracks, i);
                    return await MediaStreamTrack.CreateAsync(JSRuntime, jSInstance);
                })
                .ToArray()
        );
    }

    public async Task<MediaStreamTrack[]> GetVideoTracksAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference audioTracks = await JSReference.InvokeAsync<IJSObjectReference>("getVideoTracks");
        int length = await helper.InvokeAsync<int>("getAttribute", audioTracks, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                {
                    IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", audioTracks, i);
                    return await MediaStreamTrack.CreateAsync(JSRuntime, jSInstance);
                })
                .ToArray()
        );
    }

    public async Task<MediaStreamTrack[]> GetTracksAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference audioTracks = await JSReference.InvokeAsync<IJSObjectReference>("getTracks");
        int length = await helper.InvokeAsync<int>("getAttribute", audioTracks, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                {
                    IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", audioTracks, i);
                    return await MediaStreamTrack.CreateAsync(JSRuntime, jSInstance);
                })
                .ToArray()
        );
    }

    public async Task RemoveTrackAsync(MediaStreamTrack track)
    {
        await JSReference.InvokeVoidAsync("removeTrack", track.JSReference);
    }

    public async Task<bool> GetActiveAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "active");
    }
}
