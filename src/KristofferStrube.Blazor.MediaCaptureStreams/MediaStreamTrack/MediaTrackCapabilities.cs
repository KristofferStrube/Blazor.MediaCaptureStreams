using KristofferStrube.Blazor.MediaCaptureStreams.Converters;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// <see cref="MediaTrackCapabilities"/> represents the Capabilities of a <see cref="MediaStreamTrack"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#media-track-capabilities">See the API definition here</see>.</remarks>
public class MediaTrackCapabilities
{
    /// <summary>
    /// The width, in pixels. As a capability, its valid range should span the video source's pre-set width values with min being equal to 1 and max being the largest width.
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ULongRange? Width { get; set; }

    /// <summary>
    /// The height, in pixels. As a capability, its valid range should span the video source's pre-set height values with min being equal to 1 and max being the largest height.
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ULongRange? Height { get; set; }

    /// <summary>
    /// The exact aspect ratio (width in pixels divided by height in pixels, represented as a double rounded to the tenth decimal place) or aspect ratio range.
    /// </summary>
    [JsonPropertyName("aspectRatio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DoubleRange? AspectRatio { get; set; }

    /// <summary>
    /// The frame rate (frames per second). If video source's pre-set can determine frame rates, then, as a capability, its valid range should span the video source's pre-set frame rate values with min being equal to 0 and max being the largest frame rate.
    /// </summary>
    [JsonPropertyName("frameRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DoubleRange? FrameRate { get; set; }

    /// <summary>
    /// A camera can report multiple facing modes. For example, in a high-end telepresence solution with several cameras facing the user, a camera to the left of the user can report both <see cref="VideoFacingMode.Left"/> and <see cref="VideoFacingMode.User"/>.
    /// </summary>
    [JsonPropertyName("facingMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? FacingMode { get; set; }

    /// <summary>
    /// The User Agent may use cropping and downscaling to offer more resolution choices than this camera naturally produces. The reported sequence must list all the means the User Agent may employ to derive resolution choices for this camera. The value <see cref="VideoResizeMode.None"/> must be present, indicating the ability to constrain the UA from cropping and downscaling.
    /// </summary>
    [JsonPropertyName("resizeMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? ResizeMode { get; set; }

    /// <summary>
    /// The sample rate in samples per second for the audio data.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ULongRange? SampleRate { get; set; }

    /// <summary>
    /// The linear sample size in bits.
    /// </summary>
    [JsonPropertyName("sampleSize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ULongRange? SampleSize { get; set; }

    /// <summary>
    /// If the source cannot do echo cancellation a single <see langword="false"/> is reported. If echo cancellation cannot be turned off, a single <see langword="true"/> is reported. If the script can control the feature, the source reports an array with both <see langword="true"/> and <see langword="false"/> as possible values.
    /// </summary>
    [JsonPropertyName("echoCancellation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(NullableArrayOfBooleansIgnoringStringsConverter))]
    public bool[]? EchoCancellation { get; set; }

    /// <summary>
    /// If the source cannot do auto gain control a single <see langword="false"/> is reported. If auto gain control cannot be turned off, a single <see langword="true"/> is reported. If the script can control the feature, the source reports an array with both <see langword="true"/> and <see langword="false"/> as possible values.
    /// </summary>
    [JsonPropertyName("autoGainControl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool[]? AutoGainControl { get; set; }

    /// <summary>
    /// If the source cannot do noise suppression a single <see langword="false"/> is reported. If noise suppression cannot be turned off, a single <see langword="true"/> is reported. If the script can control the feature, the source reports an array with both <see langword="true"/> and <see langword="false"/> as possible values.
    /// </summary>
    [JsonPropertyName("noiseSuppression")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool[]? NoiseSuppression { get; set; }

    /// <summary>
    /// The latency or latency range, in seconds. The latency is the time between start of processing (for instance, when sound occurs in the real world) to the data being available to the next step in the process. Low latency is critical for some applications; high latency may be acceptable for other applications because it helps with power constraints. The number is expected to be the target latency of the configuration; the actual latency may show some variation from that.
    /// </summary>
    [JsonPropertyName("latency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DoubleRange? Latency { get; set; }

    /// <summary>
    /// The number of independent channels of sound that the audio data contains, i.e. the number of audio samples per sample frame.
    /// </summary>
    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ULongRange? ChannelCount { get; set; }

    /// <summary>
    /// The identifier of the device generating the content of the <see cref="MediaStreamTrack"/>. <see cref="MediaStreamTrack.GetCapabilitiesAsync"/> will return only a single value for <see cref="DeviceId"/>.
    /// </summary>
    [JsonPropertyName("deviceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DeviceId { get; set; }

    /// <summary>
    /// The document-unique group identifier for the device generating the content of the <see cref="MediaStreamTrack"/>. <see cref="MediaStreamTrack.GetCapabilitiesAsync"/> will return only a single value for groupId.
    /// </summary>
    [JsonPropertyName("groupId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GroupId { get; set; }
}
