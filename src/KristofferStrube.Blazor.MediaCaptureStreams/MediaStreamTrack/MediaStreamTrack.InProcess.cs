using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <inheritdoc />
public class MediaStreamTrackInProcess : MediaStreamTrack, IEventTargetInProcess
{
    private readonly IJSInProcessObjectReference inProcessDOMHelper;

    /// <summary>
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc />
    public new IJSInProcessObjectReference JSReference { get; }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrackInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrackInProcess"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaStreamTrackInProcess"/>.</returns>
    public async static Task<MediaStreamTrackInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        var helper = await jSRuntime.GetInProcessHelperAsync();
        var DOMHelper = await Blazor.DOM.Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(jSRuntime);
        return new MediaStreamTrackInProcess(jSRuntime, helper, DOMHelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaStreamTrackInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in-process helper instance.</param>
    /// <param name="inProcessDOMHelper">An in-process helper instance from the Blazor DOM library.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaStreamTrackInProcess"/>.</param>
    protected internal MediaStreamTrackInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference inProcessDOMHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        this.inProcessDOMHelper = inProcessDOMHelper;
        JSReference = jSReference;
    }

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(inProcessDOMHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(inProcessDOMHelper, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(inProcessDOMHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(inProcessDOMHelper, callback, options);
    }

    /// <inheritdoc/>
    public bool DispatchEvent(Event eventInstance)
    {
        return IEventTargetInProcessExtensions.DispatchEvent(this, eventInstance);
    }

    /// <inheritdoc cref="MediaStreamTrack.GetKindAsync"/>
    public MediaStreamTrackKind Kind => MediaStreamTrackKindExtensions.ParseMediaStreamTrackKind(inProcessHelper.Invoke<string>("getAttribute", JSReference, "kind"));

    /// <inheritdoc cref="MediaStreamTrack.GetIdAsync"/>
    public string Id => inProcessHelper.Invoke<string>("getAttribute", JSReference, "id");

    /// <inheritdoc cref="MediaStreamTrack.GetLabelAsync"/>
    public string Label => inProcessHelper.Invoke<string>("getAttribute", JSReference, "label");

    /// <summary>
    /// Gets or sets the enabled state for the <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <remarks>
    /// After a <see cref="MediaStreamTrack"/> has <c>ended</c>, its enabled attribute still changes value when set; it just doesn't do anything with that new value.
    /// </remarks>
    public bool Enabled
    {
        get => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "enabled");
        set
        {
            inProcessHelper.InvokeVoid("setAttribute", JSReference, "enabled", value);
        }
    }

    /// <inheritdoc cref="MediaStreamTrack.GetMutedAsync"/>
    public bool Muted => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "muted");

    /// <inheritdoc cref="MediaStreamTrack.AddOnMuteEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public void AddOnMuteEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, AddEventListenerOptions? options = null)
         where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        AddEventListener("mute", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.RemoveOnMuteEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public void RemoveOnMuteEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, EventListenerOptions? options = null)
        where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        RemoveEventListener("mute", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.AddOnUnmuteEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public void AddOnUnmuteEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, AddEventListenerOptions? options = null)
        where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        AddEventListener("unmute", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.RemoveOnUnmuteEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public void RemoveOnUnmuteEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, EventListenerOptions? options = null)
        where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        RemoveEventListener("unmute", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.GetReadyStateAsync"/>
    public MediaStreamTrackState ReadyState => MediaStreamTrackStateExtensions.ParseMediaStreamTrackState(inProcessHelper.Invoke<string>("getAttribute", JSReference, "readyState"));

    /// <inheritdoc cref="MediaStreamTrack.AddOnEndedEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public void AddOnEndedEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, AddEventListenerOptions? options = null)
        where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        AddEventListener("ended", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.RemoveOnEndedEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public void RemoveOnEndedEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent> callback, EventListenerOptions? options = null)
        where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent> where TEvent : Event, IJSCreatable<TEvent>
    {
        RemoveEventListener("ended", callback, options);
    }

    /// <inheritdoc cref="MediaStreamTrack.CloneAsync"/>
    public new async Task<MediaStreamTrackInProcess> CloneAsync()
    {
        var helper = await JSRuntime.GetInProcessHelperAsync();
        var DOMHelper = await Blazor.DOM.Extensions.IJSRuntimeExtensions.GetInProcessHelperAsync(JSRuntime);

        IJSInProcessObjectReference jSInstance = await JSReference.InvokeAsync<IJSInProcessObjectReference>("clone");
        return new(JSRuntime, helper, DOMHelper, jSInstance);
    }

    /// <inheritdoc cref="MediaStreamTrack.StopAsync"/>
    public void Stop()
    {
        JSReference.InvokeVoid("stop");
    }

    /// <inheritdoc cref="MediaStreamTrack.GetCapabilitiesAsync"/>
    public MediaTrackCapabilities GetCapabilities() => JSReference.Invoke<MediaTrackCapabilities>("getCapabilities");

    /// <inheritdoc cref="MediaStreamTrack.GetConstraintsAsync"/>
    public MediaTrackConstraints GetConstraints()
    {
        IJSObjectReference result = JSReference.Invoke<IJSObjectReference>("getConstraints");
        MediaTrackConstraints newMediaTrackConstraints = new();
        MediaTrackConstraints.HydrateMediaTrackConstraintsAsync(newMediaTrackConstraints, JSRuntime, result);
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
