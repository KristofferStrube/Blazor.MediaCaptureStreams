using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="MediaDeviceInfo"/> interface gives access to the basic information of the input or output device it represents. It may be extended by <see cref="InputDeviceInfo"/> specifying that it is an input device.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#device-info">See the API definition here</see>.</remarks>
public class MediaDeviceInfo : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDeviceInfo"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDeviceInfo"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaDeviceInfo"/>.</returns>
    public static Task<MediaDeviceInfo> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaDeviceInfo(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDeviceInfo"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDeviceInfo"/>.</param>
    protected MediaDeviceInfo(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Returns the identifier of the represented device. The device must be uniquely identified by its identifier and its kind.
    /// </summary>
    public async Task<string> GetDeviceIdAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "deviceId");
    }

    /// <summary>
    /// Returns the kind of the represented device.
    /// </summary>
    public async Task<MediaDeviceKind> GetKindAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        string kind = await helper.InvokeAsync<string>("getAttribute", JSReference, "kind");
        return MediaDeviceKindExtensions.ParseMediaDeviceKind(kind);
    }

    /// <summary>
    /// Returns a label describing this device (for example <c>"External USB Webcam"</c>). This label is intended to allow the end user to tell the difference between devices. Applications can’t assume that the label contains any specific information, such as the device type or model. If the device has no associated label, then this will return the empty string.
    /// </summary>
    public async Task<string> GetLabelAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "label");
    }

    /// <summary>
    /// Returns the group identifier of the represented device. Two devices have the same group identifier if they belong to the same physical device. For example, the audio input and output devices representing the speaker and microphone of the same headset have the same groupId.
    /// </summary>
    public async Task<string> GetGroupIdAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "groupId");
    }

    /// <summary>
    /// Serializes the object using the <see href="https://webidl.spec.whatwg.org/#default-tojson-steps">default toJSON operation</see>.
    /// </summary>
    /// <returns></returns>
    public async Task<string> ToJSONAsync()
    {
        return await JSReference.InvokeAsync<string>("toJSON");
    }
}