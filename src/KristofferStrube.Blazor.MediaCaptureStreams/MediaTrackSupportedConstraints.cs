using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackSupportedConstraints
{
    [JsonPropertyName("width")]
    public bool Width { get; set; } = true;

    [JsonPropertyName("height")]
    public bool Height { get; set; } = true;

    [JsonPropertyName("aspectRatio")]
    public bool AspectRatio { get; set; } = true;

    [JsonPropertyName("frameRate")]
    public bool FrameRate { get; set; } = true;

    [JsonPropertyName("facingMode")]
    public bool FacingMode { get; set; } = true;

    [JsonPropertyName("resizeMode")]
    public bool ResizeMode { get; set; } = true;

    [JsonPropertyName("sampleRate")]
    public bool DampleRate { get; set; } = true;

    [JsonPropertyName("sampleSize")]
    public bool SampleSize { get; set; } = true;

    [JsonPropertyName("echoCancellation")]
    public bool EchoCancellation { get; set; } = true;

    [JsonPropertyName("autoGainControl")]
    public bool AutoGainControl { get; set; } = true;

    [JsonPropertyName("noiseSuppression")]
    public bool NoiseSuppression { get; set; } = true;

    [JsonPropertyName("latency")]
    public bool Latency { get; set; } = true;

    [JsonPropertyName("channelCount")]
    public bool ChannelCount { get; set; } = true;

    [JsonPropertyName("deviceId")]
    public bool DeviceId { get; set; } = true;

    [JsonPropertyName("groupId")]
    public bool GroupId { get; set; } = true;
}
