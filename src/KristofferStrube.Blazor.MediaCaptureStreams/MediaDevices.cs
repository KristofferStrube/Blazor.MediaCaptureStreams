using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;
using System.Text.Json;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// The <see cref="MediaDevices"/> object is the entry point to the API used to examine and get access to media devices available to the User Agent.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediadevices">See the API definition here</see> and <see href="https://www.w3.org/TR/mediacapture-streams/#mediadevices-interface-extensions">here</see>.</remarks>
public class MediaDevices : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;
    private readonly ErrorHandlingJSObjectReference? errorHandlingJSReference;

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

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDevices"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDevices"/>.</param>
    protected MediaDevices(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
        if (ErrorHandlingJSInterop.ErrorHandlingJSInteropHasBeenSetup)
        {
            errorHandlingJSReference = new ErrorHandlingJSObjectReference(jSReference)
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
    /// Adds an <see cref="EventListener{TEvent}"/> for when the set of media devices, available to the User Agent, has changed. The current list devices can be retrieved with the <see cref="EnumerateDevicesAsync"/> method.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task AddOnDeviceChangeEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("devicechange", callback, options);
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnDeviceChangeEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnDeviceChangeEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("devicechange", callback, options);
    }

    /// <summary>
    /// Collects information about the User Agent's available media input and output devices.
    /// The Task will be fulfilled with a sequence of <see cref="MediaDeviceInfo"/> objects representing the User Agent's available media input and output devices if enumeration is successful.
    /// Elements of this sequence that represent input devices will be of type <see cref="InputDeviceInfo"/> which extends <see cref="MediaDeviceInfo"/>
    /// </summary>
    /// <remarks>
    /// If the user has not been asked for permission to access the microphones or the cameras by calling <see cref="GetUserMediaAsync(MediaStreamConstraints)"/> first then they will get a placeholder <see cref="MediaDeviceInfo"/> with the relevant <see cref="MediaDeviceKind"/> specified but with empty labels and ids.
    /// </remarks>
    /// <returns>A sequence that will first contain all microphones with the default microphone first, then all cameras with the default camera first and then other devices with the first one being the default audio output device if applicable.</returns>
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
                        ValueReference reference = new ValueReference(JSRuntime, devices, i);
                        reference.ValueMapper["mediadeviceinfo"] = async () => await MediaDeviceInfo.CreateAsync(JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
                        reference.ValueMapper["inputdeviceinfo"] = async () => await InputDeviceInfo.CreateAsync(JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
                        return (MediaDeviceInfo)(await reference.GetValueAsync())!;
                    })
                .ToArray()
        );
    }

    /// <summary>
    /// Returns the constrainable properties known to the User Agent.
    /// A supported constrainable property will be a non-null value and any constrainable properties not supported by the User Agent will have a null value present in the returned object.
    /// </summary>
    public async Task<MediaTrackSupportedConstraints> GetSupportedConstraintsAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackSupportedConstraints>("getSupportedConstraints");
    }

    /// <summary>
    /// Prompts the user for permission to use their Web cam or other video or audio input.<br />
    /// </summary>
    /// <remarks>
    /// If neither <see cref="MediaStreamConstraints.Audio"/> nor <see cref="MediaStreamConstraints.Video"/> has been set on the <paramref name="constraints"/> then it throws a <see cref="TypeErrorException"/>.<br />
    /// If the document is not fully active then it throws a <see cref="InvalidStateErrorException"/>.<br />
    /// If the <paramref name="constraints"/> requested audio devices but the browser does not allow to use that feature then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If the <paramref name="constraints"/> requested video devices but the browser does not allow to use that feature then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If it is not possible to find an initial set of candidate devices for one of the requested <c>kind</c>s of devices then it throws a <see cref="NotFoundErrorException"/>.<br />
    /// If one of the constraints is malformed then it throws a <see cref="TypeErrorException"/>.<br />
    /// If one of the constraints is too restrictive then it throws a <see cref="OverconstrainedErrorException"/>; If any device of the relevant <c>kind</c> is already open then the <see cref="OverconstrainedErrorException.Constraint"/> property is set to the name of the overrestrictive constraint.<br />
    /// If the user has not given permissions to access the specific <c>kind</c> of device then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If a hardware error such as an OS/program/webpage lock prevents access and a given candidate device is the last of its <c>kind</c> then it throws a <see cref="NotReadableErrorException"/>.<br />
    /// If any other error happen while trying to access a device and the given candidate device is the last of its <c>kind</c> it throws a <see cref="AbortErrorException"/>.
    /// </remarks>
    /// <param name="constraints">The constraints that define the requested <c>kind</c>s of devices should follow.</param>
    /// <returns>A suitable <see cref="MediaStream"/> that follows the <paramref name="constraints"/>.</returns>
    /// <exception cref="InvalidStateErrorException" />
    /// <exception cref="NotAllowedErrorException" />
    /// <exception cref="NotFoundErrorException" />
    /// <exception cref="TypeErrorException" />
    /// <exception cref="OverconstrainedErrorException" />
    /// <exception cref="NotAllowedErrorException" />
    /// <exception cref="NotReadableErrorException" />
    /// <exception cref="AbortErrorException" />
    public async Task<MediaStream> GetUserMediaAsync(MediaStreamConstraints constraints)
    {
        IJSObjectReference jSReference = errorHandlingJSReference ?? JSReference;
        IJSObjectReference jSInstance = await jSReference.InvokeAsync<IJSObjectReference>("getUserMedia", constraints);
        return await MediaStream.CreateAsync(JSRuntime, jSInstance);
    }
}
