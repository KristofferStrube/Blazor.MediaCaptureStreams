using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="MediaStreamTrack"/> object represents a media source in the User Agent. An example source is a device connected to the User Agent.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediastreamtrack">See the API definition here</see></remarks>
public class MediaStreamTrack : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrack"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStreamTrack"/>.</returns>
    public static Task<MediaStreamTrack> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaStreamTrack(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrack"/>.</param>
    protected MediaStreamTrack(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Returns the kind of the <see cref="MediaStreamTrack"/>.
    /// </summary>
    public async Task<MediaStreamTrackKind> GetKindAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        string kind = await helper.InvokeAsync<string>("getAttribute", JSReference, "kind");
        return MediaStreamTrackKindExtensions.ParseMediaStreamTrackKind(kind);
    }

    /// <summary>
    /// Returns the id of the <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <remarks>
    /// The proper way to find a specific <see cref="MediaStreamTrack"/> object in the set returned from <see cref="MediaStream.GetTracksAsync"/> is to look it up by its id.
    /// </remarks>
    public async Task<string> GetIdAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "id");
    }

    /// <summary>
    /// Returns the label of the <see cref="MediaStreamTrack"/>.
    /// </summary>
    public async Task<string> GetLabelAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "label");
    }

    /// <summary>
    /// If the <see cref="MediaStreamTrack"/> has not already <c>ended</c> it notifies the source of the track that it is <c>ended</c>.
    /// </summary>
    public async Task StopAsync()
    {
        await JSReference.InvokeVoidAsync("stop");
    }

    /// <summary>
    /// Returns the capabilites of the source that this <see cref="MediaStreamTrack"/>, the constrainable object, represents.
    /// </summary>
    /// <returns></returns>
    public async Task<MediaTrackCapabilities> GetCapabilitiesAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackCapabilities>("getCapabilities");
    }

    /// <summary>
    /// Returns the <see cref="MediaTrackConstraints"/> that were the argument to the most recent successful invocation of the <see cref="ApplyContraintsAsync(MediaTrackConstraints?)"/> method on the object, maintaining the order in which they were specified. Note that some of the <see cref="MediaTrackConstraints.Advanced"/> <see cref="MediaTrackConstraintSet"/> returned may not be currently satisfied. To check which <see cref="MediaTrackConstraintSet"/> are currently in effect, the application should use <see cref="GetSettingsAsync"/>. Instead of returning the exact constraints as described above, the UA MAY return a constraint set that has the identical effect in all situations as the applied constraints.
    /// </summary>
    /// <returns></returns>
    public async Task<MediaTrackConstraints> GetConstraintsAsync()
    {
        IJSObjectReference result = await JSReference.InvokeAsync<IJSObjectReference>("getConstraints");
        MediaTrackConstraints newMediaTrackConstraints = new MediaTrackConstraints();
        await MediaTrackConstraints.HydrateMediaTrackConstraints(newMediaTrackConstraints, JSRuntime, result);
        return newMediaTrackConstraints;
    }

    /// <summary>
    /// Returns the current <see cref="MediaTrackSettings"/> of all the constrainable properties of the object, whether they are platform defaults or have been set by the <see cref="ApplyContraintsAsync(MediaTrackConstraints?)"/> method. Note that a setting is a target value that complies with constraints, and therefore may differ from measured performance at times.
    /// </summary>
    /// <returns></returns>
    public async Task<MediaTrackSettings> GetSettingsAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackSettings>("getSettings");
    }

    /// <summary>
    /// Applies the <paramref name="constraints"/> to the <see cref="MediaStreamTrack"/> using the <c>applyConstraints template method</c>.
    /// </summary>
    /// <remarks>
    /// Read more about the <c>applyConstraints template method</c> <see href="https://www.w3.org/TR/mediacapture-streams/#dfn-applyconstraints-template-method">in the api specs.</see>
    /// </remarks>
    /// <param name="constraints"></param>
    /// <returns></returns>
    public async Task ApplyContraintsAsync(MediaTrackConstraints? constraints = null)
    {
        await JSReference.InvokeVoidAsync("applyConstraints", constraints);
    }
}
