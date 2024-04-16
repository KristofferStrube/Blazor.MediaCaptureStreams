using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="MediaStreamTrack"/> object represents a media source in the User Agent. An example source is a device connected to the User Agent.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediastreamtrack">See the API definition here</see>.</remarks>
public class MediaStreamTrack : EventTarget, IJSCreatable<MediaStreamTrack>
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.MediaCaptureStreams library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;

    private readonly ErrorHandlingJSObjectReference? errorHandlingJSReference;

    /// <inheritdoc/>
    public static new async Task<MediaStreamTrack> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MediaStreamTrack> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MediaStreamTrack(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal MediaStreamTrack(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
        if (ErrorHandlingJSInterop.ErrorHandlingJSInteropHasBeenSetup)
        {
            errorHandlingJSReference = new ErrorHandlingJSObjectReference(jSRuntime, jSReference)
            {
                ExtraErrorProperties = new string[] { "constraint" }
            };
            errorHandlingJSReference.ErrorMapper.TryAdd("OverconstrainedError", (jSError) => new OverconstrainedErrorException(
                jSError.ExtensionData is not null ? jSError.ExtensionData.TryGetValue("constraint", out JsonElement json) ? (json.GetString() ?? string.Empty) : string.Empty : string.Empty,
                jSError.Message,
                jSError.Stack,
                jSError.InnerException)
            );
        }
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
    /// Returns the enabled state for the <see cref="MediaStreamTrack"/>.
    /// </summary>
    public async Task<bool> GetEnabledAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "enabled");
    }

    /// <summary>
    /// Sets the enabled state for the <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <remarks>
    /// After a <see cref="MediaStreamTrack"/> has <c>ended</c>, its enabled attribute still changes value when set; it just doesn't do anything with that new value.
    /// </remarks>
    public async Task SetEnabledAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "enabled");
    }

    /// <summary>
    /// The muted attribute reflects whether the <see cref="MediaStreamTrack"/> is muted.
    /// </summary>
    public async Task<bool> GetMutedAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "muted");
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the <see cref="MediaStreamTrack"/> object's source is temporarily unable to provide data.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnMuteEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("mute", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnMuteEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnMuteEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("mute", callback, options);
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the <see cref="MediaStreamTrack"/> object's source is live again after having been temporarily unable to provide data.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnUnmuteEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("unmute", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnUnmuteEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnUnmuteEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("unmute", callback, options);
    }

    /// <summary>
    /// Gets the state of a <see cref="MediaStreamTrack"/>.
    /// </summary>
    public async Task<MediaStreamTrackState> GetReadyStateAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        string readyState = await helper.InvokeAsync<string>("getAttribute", JSReference, "readyState");
        return MediaStreamTrackStateExtensions.ParseMediaStreamTrackState(readyState);
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the <see cref="MediaStreamTrack"/> object's source will no longer provide any data, either because the user revoked the permissions, or because the source device has been ejected, or because the remote peer permanently stopped sending data.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnEndedEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("ended", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnEndedEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnEndedEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("ended", callback, options);
    }

    /// <summary>
    /// Creates a new <see cref="MediaStreamTrack"/> new the same source, but no associated device. After this it clones the state, capabilites, constraints, and settings.
    /// </summary>
    /// <returns>A new <see cref="MediaStreamTrack"/>.</returns>
    public async Task<MediaStreamTrack> CloneAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("clone");
        return new MediaStreamTrack(JSRuntime, jSInstance, new() { DisposesJSReference = true });
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
    public async Task<MediaTrackCapabilities> GetCapabilitiesAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackCapabilities>("getCapabilities");
    }

    /// <summary>
    /// Returns the <see cref="MediaTrackConstraints"/> that were the argument to the most recent successful invocation of the <see cref="ApplyContraintsAsync(MediaTrackConstraints?)"/> method on the object, maintaining the order in which they were specified. Note that some of the <see cref="MediaTrackConstraints.Advanced"/> <see cref="MediaTrackConstraintSet"/> returned may not be currently satisfied. To check which <see cref="MediaTrackConstraintSet"/> are currently in effect, the application should use <see cref="GetSettingsAsync"/>. Instead of returning the exact constraints as described above, the User Agent may return a constraint set that has the identical effect in all situations as the applied constraints.
    /// </summary>
    public async Task<MediaTrackConstraints> GetConstraintsAsync()
    {
        IJSObjectReference result = await JSReference.InvokeAsync<IJSObjectReference>("getConstraints");
        MediaTrackConstraints newMediaTrackConstraints = new();
        await MediaTrackConstraints.HydrateMediaTrackConstraints(newMediaTrackConstraints, JSRuntime, result);
        return newMediaTrackConstraints;
    }

    /// <summary>
    /// Returns the current <see cref="MediaTrackSettings"/> of all the constrainable properties of the object, whether they are platform defaults or have been set by the <see cref="ApplyContraintsAsync(MediaTrackConstraints?)"/> method. Note that a setting is a target value that complies with constraints, and therefore may differ from measured performance at times.
    /// </summary>
    public async Task<MediaTrackSettings> GetSettingsAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackSettings>("getSettings");
    }

    /// <summary>
    /// Applies the <paramref name="constraints"/> to the <see cref="MediaStreamTrack"/> using the <c>applyConstraints template method</c>.<br />
    /// If the applied constraints are too strict it will throw an <see cref="OverconstrainedErrorException"/>.
    /// </summary>
    /// <remarks>
    /// Read more about the <c>applyConstraints template method</c> <see href="https://www.w3.org/TR/mediacapture-streams/#dfn-applyconstraints-template-method">in the api specs.</see>
    /// </remarks>
    /// <param name="constraints"></param>
    /// <exception cref="OverconstrainedErrorException" />
    public async Task ApplyContraintsAsync(MediaTrackConstraints? constraints = null)
    {
        IJSObjectReference jSReference = errorHandlingJSReference ?? JSReference;
        await jSReference.InvokeVoidAsync("applyConstraints", constraints);
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
