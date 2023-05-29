using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackSettings
{
    [JsonPropertyName("width")]
    public ulong Width { get; set; }

    [JsonPropertyName("height")]
    public ulong Height { get; set; }

    [JsonPropertyName("aspectRatio")]
    public double AspectRatio { get; set; }

    [JsonPropertyName("frameRate")]
    public double FrameRate { get; set; }

    [JsonPropertyName("facingMode")]
    public string? FacingMode { get; set; }

    [JsonPropertyName("resizeMode")]
    public string? ResizeMode { get; set; }

    [JsonPropertyName("sampleRate")]
    public ulong DampleRate { get; set; }

    [JsonPropertyName("sampleSize")]
    public ulong SampleSize { get; set; }

    [JsonPropertyName("echoCancellation")]
    public bool EchoCancellation { get; set; }

    [JsonPropertyName("autoGainControl")]
    public bool AutoGainControl { get; set; }

    [JsonPropertyName("noiseSuppression")]
    public bool NoiseSuppression { get; set; }

    [JsonPropertyName("latency")]
    public double Latency { get; set; }

    [JsonPropertyName("channelCount")]
    public ulong ChannelCount { get; set; }

    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; set; }

    [JsonPropertyName("groupId")]
    public string? GroupId { get; set; }
}
