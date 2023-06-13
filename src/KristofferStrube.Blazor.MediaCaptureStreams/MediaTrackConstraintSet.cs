using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Each member of a <see cref="MediaTrackConstraintSet"/> corresponds to a constrainable property and specifies a subset of the property's valid <see cref="MediaTrackCapabilities"/> values. Applying a <see cref="MediaTrackConstraintSet"/> instructs the User Agent to restrict the settings of the corresponding constrainable properties to the specified values or ranges of values. A given property may occur both in the basic <see cref="MediaTrackConstraints"/> set and in the <see cref="MediaTrackConstraints.Advanced"/> array, and may occur at most once in each <see cref="MediaTrackConstraintSet"/> in the <see cref="MediaTrackConstraints.Advanced"/> array.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-mediatrackconstraintset">See the API definition here</see></remarks>
public class MediaTrackConstraintSet
{
    /// <summary>
    /// The width, in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Width { get; set; }

    /// <summary>
    /// The height, in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Height { get; set; }

    /// <summary>
    /// The exact aspect ratio (width in pixels divided by height in pixels, represented as a double rounded to the tenth decimal place) or aspect ratio range.
    /// </summary>
    [JsonPropertyName("aspectRatio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? AspectRatio { get; set; }

    /// <summary>
    /// The frame rate (frames per second).
    /// As a setting, this value represents the configured frame rate. If decimation is used, this is that value rather than the native frame rate. For example, if the setting is 25 frames per second via decimation, the native frame rate of the camera is 30 frames per second but due to lighting conditions only 20 frames per second is achieved, <see cref="FrameRate"/> reports the setting: 25 frames per second.
    /// </summary>
    [JsonPropertyName("frameRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? FrameRate { get; set; }

    /// <summary>
    /// This is one of the members of <see cref="VideoFacingMode"/>. The members describe the directions that the camera can face, as seen from the user's perspective.
    /// </summary>
    /// <remarks>
    /// Note that <see cref="MediaStreamTrack.GetConstraintsAsync"/> may not return exactly the same string for strings not in this enum. This preserves the possibility of using a future version of WebIDL enum for this property.
    /// </remarks>
    [JsonPropertyName("facingMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainVideoFacingMode? FacingMode { get; set; }

    /// <summary>
    /// This is one of the members of <see cref="VideoResizeMode"/>. The members describe the means by which the resolution can be derived by the UA. In other words, whether the UA is allowed to use cropping and downscaling on the camera output.
    /// </summary>
    /// <remarks>
    /// Note that <see cref="MediaStreamTrack.GetConstraintsAsync"/> may not return exactly the same string for strings not in this enum. This preserves the possibility of using a future version of WebIDL enum for this property.
    /// </remarks>
    [JsonPropertyName("resizeMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainVideoResizeMode? ResizeMode { get; set; }

    /// <summary>
    /// The sample rate in samples per second for the audio data.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleRate { get; set; }

    /// <summary>
    /// The linear sample size in bits. As a constraint, it can only be satisfied for audio devices that produce linear samples.
    /// </summary>
    [JsonPropertyName("sampleSize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleSize { get; set; }

    /// <summary>
    /// When one or more audio streams is being played in the processes of various microphones, it is often desirable to attempt to remove all the sound being played from the input signals recorded by the microphones. This is referred to as echo cancellation. There are cases where it is not needed and it is desirable to turn it off so that no audio artifacts are introduced. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("echoCancellation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? EchoCancellation { get; set; }

    /// <summary>
    /// Automatic gain control is often desirable on the input signal recorded by the microphone. There are cases where it is not needed and it is desirable to turn it off so that the audio is not altered. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("autoGainControl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? AutoGainControl { get; set; }

    /// <summary>
    /// Noise suppression is often desirable on the input signal recorded by the microphone. There are cases where it is not needed and it is desirable to turn it off so that the audio is not altered. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("noiseSuppression")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? NoiseSuppression { get; set; }

    /// <summary>
    /// The latency or latency range, in seconds. The latency is the time between start of processing (for instance, when sound occurs in the real world) to the data being available to the next step in the process. Low latency is critical for some applications; high latency may be acceptable for other applications because it helps with power constraints. The number is expected to be the target latency of the configuration; the actual latency may show some variation from that.
    /// </summary>
    [JsonPropertyName("latency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? Latency { get; set; }

    /// <summary>
    /// The number of independent channels of sound that the audio data contains, i.e. the number of audio samples per sample frame.
    /// </summary>
    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? ChannelCount { get; set; }

    /// <summary>
    /// The identifier of the device generating the content of the <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <remarks>
    /// This property can be used for initial media selection with <see cref="MediaDevices.GetUserMediaAsync(MediaStreamConstraints)"/>. However, it is not useful for subsequent media control with <see cref="MediaStreamTrack.ApplyContraintsAsync(MediaTrackConstraints?)"/>, since any attempt to set a different value will result in an unsatisfiable <see cref="MediaTrackConstraintSet"/>. If a string of length 0 is used as a deviceId value constraint with <see cref="MediaDevices.GetUserMediaAsync(MediaStreamConstraints)"/>, it may be interpreted as if the constraint is not specified.
    /// </remarks>
    [JsonPropertyName("deviceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? DeviceId { get; set; }

    /// <summary>
    /// The document-unique group identifier for the device generating the content of the <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <remarks>
    /// Note that the setting of this property is uniquely determined by the source that is attached to the <see cref="MediaStreamTrack"/>. Since this property is not stable between browsing sessions, its usefulness for initial media selection with <see cref="MediaDevices.GetUserMediaAsync(MediaStreamConstraints)"/> is limited. It is not useful for subsequent media control with <see cref="MediaStreamTrack.ApplyContraintsAsync(MediaTrackConstraints?)"/>, since any attempt to set a different value will result in an unsatisfiable <see cref="MediaTrackConstraintSet"/>.
    /// </remarks>
    [JsonPropertyName("groupId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? GroupId { get; set; }

    /// <summary>
    /// This method is used to hydrate a <see cref="MediaTrackConstraintSet"/> or a class that extends this from a <see cref="IJSObjectReference"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="hydrateObject">The object that will get its properties populated.</param>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">The reference to the JS object that the properties will be read from.</param>
    /// <returns></returns>
    public static async Task<T> HydrateMediaTrackConstraintSet<T>(T hydrateObject, IJSRuntime jSRuntime, IJSObjectReference jSReference) where T : MediaTrackConstraintSet
    {
        ValueReference reference = await ValueReference.CreateAsync(jSRuntime, jSReference);
        await SetConstrainULongProperty(reference, "width", (value) => hydrateObject.Width = new(value));
        await SetConstrainULongProperty(reference, "height", (value) => hydrateObject.Height = new(value));
        await SetConstrainDoubleProperty(reference, "aspectRatio", (value) => hydrateObject.AspectRatio = new(value));
        await SetConstrainDoubleProperty(reference, "frameRate", (value) => hydrateObject.FrameRate = new(value));
        await SetConstrainVideoFacingModeProperty(reference, "facingMode", (value) => hydrateObject.FacingMode = new(value));
        await SetConstrainVideoResizeModeProperty(reference, "resizeMode", (value) => hydrateObject.ResizeMode = new(value));
        await SetConstrainULongProperty(reference, "sampleRate", (value) => hydrateObject.SampleRate = new(value));
        await SetConstrainULongProperty(reference, "sampleSize", (value) => hydrateObject.SampleSize = new(value));
        await SetConstrainBooleanProperty(reference, "echoCancellation", (value) => hydrateObject.EchoCancellation = new(value));
        await SetConstrainBooleanProperty(reference, "autoGainControl", (value) => hydrateObject.AutoGainControl = new(value));
        await SetConstrainBooleanProperty(reference, "noiseSuppression", (value) => hydrateObject.NoiseSuppression = new(value));
        await SetConstrainDoubleProperty(reference, "latency", (value) => hydrateObject.Latency = new(value));
        await SetConstrainULongProperty(reference, "channelCount", (value) => hydrateObject.ChannelCount = new(value));
        await SetConstrainDomStringProperty(reference, "deviceId", (value) => hydrateObject.DeviceId = new(value));
        await SetConstrainDomStringProperty(reference, "groupId", (value) => hydrateObject.GroupId = new(value));
        return hydrateObject;
    }

    private static async Task SetConstrainULongProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["number"] = async () => await reference.GetValueAsync<ulong>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainULongRange>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainDoubleProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["number"] = async () => await reference.GetValueAsync<double>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainDoubleRange>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainDomStringProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<string>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<string[]>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainDOMStringParameters>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainVideoFacingModeProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoFacingMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoFacingMode[]>();
        reference.ValueMapper["object"] = async () => await ConstrainVideoFacingModeParameters.CreateFromJSObjectReference(reference.JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainVideoResizeModeProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoResizeMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoResizeMode[]>();
        reference.ValueMapper["object"] = async () => await ConstrainVideoResizeModeParameters.CreateFromJSObjectReference(reference.JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainBooleanProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainBooleanParameters>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.Attribute = attribute;
        object? value = await reference.GetValueAsync();
        if (value is not null)
        {
            propertySetter(value);
        }
    }
}
