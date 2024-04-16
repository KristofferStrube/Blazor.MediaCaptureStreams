using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// An <see cref="Event"/> that is fired when a <see cref="MediaStream"/> adds or removes a <see cref="MediaStreamTrack"/> using the <see cref="MediaStream.AddTrackAsync(MediaStreamTrack)"/> or <see cref="MediaStream.RemoveTrackAsync(MediaStreamTrack)"/> method.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediastreamtrackevent">See the API definition here</see>.</remarks>
public class MediaStreamTrackEvent : Event, IJSCreatable<MediaStreamTrackEvent>
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.MediaCaptureStreams library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    /// <inheritdoc/>
    public static new async Task<MediaStreamTrackEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MediaStreamTrackEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MediaStreamTrackEvent(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type">The type of the event (An example could be <c>"addtrack"</c>)</param>
    /// <param name="eventInitDict">The details needed to construct a new event.</param>
    public static async Task<MediaStreamTrackEvent> CreateAsync(IJSRuntime jSRuntime, string type, MediaStreamTrackEventInit eventInitDict)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamTrackEvent", type, eventInitDict);
        return new(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected MediaStreamTrackEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Returns the <see cref="MediaStreamTrack"/> that this event is related to.
    /// </summary>
    /// <returns>A memoized instance of a <see cref="MediaStreamTrack"/>.</returns>
    public async Task<MediaStreamTrack> GetTrackAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "track");
        return new MediaStreamTrack(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc/>
    public new async ValueTask DisposeAsync()
    {
        if (mediaCaptureStreamsHelperTask.IsValueCreated)
        {
            IJSObjectReference module = await mediaCaptureStreamsHelperTask.Value;
            await module.DisposeAsync();
        }
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
