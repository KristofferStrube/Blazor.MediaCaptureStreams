using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// <see cref="MediaTrackSettings"/> represents the Settings of a <see cref="MediaStreamTrack"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#media-track-settings">See the API definition here</see></remarks>
public class MediaTrackSettings
{
    /// <summary>
    /// The width, in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public ulong Width { get; set; }

    /// <summary>
    /// The height, in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public ulong Height { get; set; }

    /// <summary>
    /// The exact aspect ratio (width in pixels divided by height in pixels, represented as a double rounded to the tenth decimal place) or aspect ratio range.
    /// </summary>
    [JsonPropertyName("aspectRatio")]
    public double AspectRatio { get; set; }

    /// <summary>
    /// The frame rate (frames per second).
    /// </summary>
    /// <remarks>
    /// As a setting, this value represents the configured frame rate. If decimation is used, this is that value rather than the native frame rate. For example, if the setting is 25 frames per second via decimation, the native frame rate of the camera is 30 frames per second but due to lighting conditions only 20 frames per second is achieved, <see cref="FrameRate"/> reports the setting: 25 frames per second.
    /// </remarks>
    [JsonPropertyName("frameRate")]
    public double FrameRate { get; set; }

    /// <summary>
    /// This is one of the members of <see cref="VideoFacingMode"/>. The members describe the directions that the camera can face, as seen from the user's perspective
    /// </summary>
    [JsonPropertyName("facingMode")]
    public VideoFacingMode? FacingMode { get; set; }

    /// <summary>
    /// This is one of the members of <see cref="VideoResizeMode"/>. The members describe the means by which the resolution can be derived by the UA. In other words, whether the UA is allowed to use cropping and downscaling on the camera output.
    /// </summary>
    [JsonPropertyName("resizeMode")]
    public VideoResizeMode? ResizeMode { get; set; }

    /// <summary>
    /// The sample rate in samples per second for the audio data.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    public ulong SampleRate { get; set; }

    /// <summary>
    /// The linear sample size in bits. As a constraint, it can only be satisfied for audio devices that produce linear samples.
    /// </summary>
    [JsonPropertyName("sampleSize")]
    public ulong SampleSize { get; set; }

    /// <summary>
    /// When one or more audio streams is being played in the processes of various microphones, it is often desirable to attempt to remove all the sound being played from the input signals recorded by the microphones. This is referred to as echo cancellation. There are cases where it is not needed and it is desirable to turn it off so that no audio artifacts are introduced. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("echoCancellation")]
    public bool EchoCancellation { get; set; }

    /// <summary>
    /// Automatic gain control is often desirable on the input signal recorded by the microphone. There are cases where it is not needed and it is desirable to turn it off so that the audio is not altered. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("autoGainControl")]
    public bool AutoGainControl { get; set; }

    /// <summary>
    /// Noise suppression is often desirable on the input signal recorded by the microphone. There are cases where it is not needed and it is desirable to turn it off so that the audio is not altered. This allows applications to control this behavior.
    /// </summary>
    [JsonPropertyName("noiseSuppression")]
    public bool NoiseSuppression { get; set; }

    /// <summary>
    /// The latency or latency range, in seconds. The latency is the time between start of processing (for instance, when sound occurs in the real world) to the data being available to the next step in the process. Low latency is critical for some applications; high latency may be acceptable for other applications because it helps with power constraints. The number is expected to be the target latency of the configuration; the actual latency may show some variation from that.
    /// </summary>
    [JsonPropertyName("latency")]
    public double Latency { get; set; }

    /// <summary>
    /// The number of independent channels of sound that the audio data contains, i.e. the number of audio samples per sample frame.
    /// </summary>
    [JsonPropertyName("channelCount")]
    public ulong ChannelCount { get; set; }

    /// <summary>
    /// The identifier of the device generating the content of the MediaStreamTrack.
    /// </summary>
    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; set; }

    /// <summary>
    /// The document-unique group identifier for the device generating the content of the MediaStreamTrack.
    /// </summary>
    [JsonPropertyName("groupId")]
    public string? GroupId { get; set; }
}
