using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackConstraintSet
{
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Width { get; set; }

    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Height { get; set; }

    [JsonPropertyName("aspectRatio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? AspectRatio { get; set; }

    [JsonPropertyName("frameRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? FrameRate { get; set; }

    [JsonPropertyName("facingMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? FacingMode { get; set; }

    [JsonPropertyName("resizeMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? ResizeMode { get; set; }

    [JsonPropertyName("sampleRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleRate { get; set; }

    [JsonPropertyName("sampleSize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleSize { get; set; }

    [JsonPropertyName("echoCancellation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? EchoCancellation { get; set; }

    [JsonPropertyName("autoGainControl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? AutoGainControl { get; set; }

    [JsonPropertyName("noiseSuppression")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? NoiseSuppression { get; set; }

    [JsonPropertyName("latency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? Latency { get; set; }

    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? ChannelCount { get; set; }

    [JsonPropertyName("deviceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? DeviceId { get; set; }

    [JsonPropertyName("groupId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? GroupId { get; set; }
}
