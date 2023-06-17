using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="MediaStream"/> is used to group several <see cref="MediaStreamTrack"/> objects into one unit that can be recorded or rendered in a media element.<br />
/// Each <see cref="MediaStream"/> can contain zero or more <see cref="MediaStreamTrack"/> objects. All tracks in a <see cref="MediaStream"/> are intended to be synchronized when rendered. This is not a hard requirement, since it might not be possible to synchronize tracks from sources that have different clocks. Different <see cref="MediaStream"/> objects do not need to be synchronized.<br />
/// A <see cref="MediaStream"/> object has an input and an output that represent the combined input and output of all the object's tracks. The output of the <see cref="MediaStream"/> controls how the object is rendered, e.g., what is saved if the object is recorded to a file or what is displayed if the object is used in a video element. A single <see cref="MediaStream"/> object can be attached to multiple different outputs at the same time.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediastream">See the API definition here</see></remarks>
public class MediaStream : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStream"/>.</returns>
    public static Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStream(jSRuntime, jSReference));
    }

    /// <summary>
    /// Creates an empty <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="MediaStream"/></returns>
    public static new async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStream");
        return new MediaStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Creates a new <see cref="MediaStream"/> with the same tracks as the parsed <paramref name="stream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="stream">An existing <see cref="MediaStream"/>.</param>
    /// <returns>A new <see cref="MediaStream"/></returns>
    public static async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, MediaStream stream)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamFromStreamOrTracks", stream.JSReference);
        return new MediaStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Creates a new <see cref="MediaStream"/> from an array of <see cref="MediaStreamTrack"/> which could originate from different <see cref="MediaStream"/>s.
    /// </summary>
    /// <remarks>
    /// It removes duplicate <see cref="MediaStreamTrack"/> from <paramref name="tracks"/> when constructing the <see cref="MediaStream"/>.
    /// </remarks>
    /// <param name="jSRuntime"></param>
    /// <param name="tracks"></param>
    /// <returns>A new <see cref="MediaStream"/></returns>
    public static async Task<MediaStream> CreateAsync(IJSRuntime jSRuntime, params MediaStreamTrack[] tracks)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructMediaStreamFromStreamOrTracks", tracks.Select(track => track.JSReference).ToArray());
        return new MediaStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStream"/>.</param>
    protected MediaStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Returns the id of the <see cref="MediaStream"/>. When a <see cref="string"/> is created, the User Agent must generate an identifier string, and must initialize the object's id attribute to that string, unless the object is created as part of a special purpose algorithm that specifies how the stream id must be initialized.
    /// </summary>
    public async Task<string> GetIdAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "id");
    }

    /// <summary>
    /// Returns an array of <see cref="MediaStreamTrack"/> objects representing the audio tracks in this stream.
    /// </summary>
    /// <remarks>
    /// The conversion from the track set to the an array is User Agent defined and the order does not have to be stable between calls.
    /// </remarks>
    /// <returns>An array of the <see cref="MediaStreamTrack"/>s of the stream that are of kind <see cref="MediaStreamTrackKind.Audio" />.</returns>
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

    /// <summary>
    /// Returns an array of <see cref="MediaStreamTrack"/> objects representing the video tracks in this stream.
    /// </summary>
    /// <remarks>
    /// The conversion from the track set to the an array is User Agent defined and the order does not have to be stable between calls.
    /// </remarks>
    /// <returns>An array of the <see cref="MediaStreamTrack"/>s of the stream that are of kind <see cref="MediaStreamTrackKind.Video" />.</returns>
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

    /// <summary>
    /// Returns a sequence of <see cref="MediaStreamTrack"/> objects representing all the tracks in this stream.
    /// </summary>
    /// <remarks>
    /// The conversion from the track set to the an array is User Agent defined and the order does not have to be stable between calls.
    /// </remarks>
    /// <returns>All tracks of the stream.</returns>
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

    /// <summary>
    /// Finds the track that matches the given <paramref name="trackId"/>. If there exists no track with the given id in the stream then it returns <see langword="null"/>.
    /// </summary>
    /// <param name="trackId">The id of the <see cref="MediaStreamTrack"/> that you wish to get from the stream.</param>
    public async Task<MediaStreamTrack?> GetTrackByIdAsync(string trackId)
    {
        IJSObjectReference? jSInstance = await JSReference.InvokeAsync<IJSObjectReference?>("getTrackById", trackId);
        if (jSInstance is null)
        {
            return null;
        }
        return new MediaStreamTrack(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Adds the given <paramref name="track"/> to this <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="track"></param>
    /// <returns></returns>
    public async Task AddTrackAsync(MediaStreamTrack track)
    {
        await JSReference.InvokeVoidAsync("addTrack", track.JSReference);
    }

    /// <summary>
    /// Removes the given <paramref name="track"/> from this <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="track"></param>
    /// <returns></returns>
    public async Task RemoveTrackAsync(MediaStreamTrack track)
    {
        await JSReference.InvokeVoidAsync("removeTrack", track.JSReference);
    }

    /// <summary>
    /// Clones this <see cref="MediaStream"/> and all its tracks. The new stream will have a newly generated id. To see more details about how each track is cloned in this process check out <see cref="MediaStreamTrack.CloneAsync"/>.
    /// </summary>
    /// <returns>A new <see cref="MediaStream"/>.</returns>
    public async Task<MediaStream> CloneAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("clone");
        return new MediaStream(JSRuntime, jSInstance);
    }

    /// <summary>
    /// Returns a boolean indicating whether this <see cref="MediaStream"/> is active.
    /// </summary>
    public async Task<bool> GetActiveAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "active");
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when a new <see cref="MediaStreamTrack"/> has been added to this stream. Note that this event is not fired when the script directly modifies the tracks of a <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnAddTrackEventListenerAsync(EventListener<MediaStreamTrackEvent> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("addtrack", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnAddTrackEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnAddTrackEventListenerAsync(EventListener<MediaStreamTrackEvent> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("addtrack", callback, options);
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when a <see cref="MediaStreamTrack"/> has been removed from this stream. Note that this event is not fired when the script directly modifies the tracks of a <see cref="MediaStream"/>.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnRemoveTrackEventListenerAsync(EventListener<MediaStreamTrackEvent> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("removetrack", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnRemoveTrackEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnRemoveTrackEventListenerAsync(EventListener<MediaStreamTrackEvent> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("removetrack", callback, options);
    }
}
