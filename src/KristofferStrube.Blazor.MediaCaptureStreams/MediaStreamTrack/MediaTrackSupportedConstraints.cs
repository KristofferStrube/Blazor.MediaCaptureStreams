using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// <see cref="MediaTrackSupportedConstraints"/> represents the list of constraints recognized by a User Agent for controlling the <see cref="MediaTrackCapabilities"/> of a <see cref="MediaStreamTrack"/> object.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#media-track-supported-constraints">See the API definition here</see>.</remarks>
public class MediaTrackSupportedConstraints
{
    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.Width"/> constraint.
    /// </summary>
    [JsonPropertyName("width")]
    public bool Width { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.Height"/> constraint.
    /// </summary>
    [JsonPropertyName("height")]
    public bool Height { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.AspectRatio"/> constraint.
    /// </summary>
    [JsonPropertyName("aspectRatio")]
    public bool AspectRatio { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.FrameRate"/> constraint.
    /// </summary>
    [JsonPropertyName("frameRate")]
    public bool FrameRate { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.FacingMode"/> constraint.
    /// </summary>
    [JsonPropertyName("facingMode")]
    public bool FacingMode { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.ResizeMode"/> constraint.
    /// </summary>
    [JsonPropertyName("resizeMode")]
    public bool ResizeMode { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.SampleRate"/> constraint.
    /// </summary>
    [JsonPropertyName("sampleRate")]
    public bool SampleRate { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.SampleSize"/> constraint.
    /// </summary>
    [JsonPropertyName("sampleSize")]
    public bool SampleSize { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.EchoCancellation"/> constraint.
    /// </summary>
    [JsonPropertyName("echoCancellation")]
    public bool EchoCancellation { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.AutoGainControl"/> constraint.
    /// </summary>
    [JsonPropertyName("autoGainControl")]
    public bool AutoGainControl { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.NoiseSuppression"/> constraint.
    /// </summary>
    [JsonPropertyName("noiseSuppression")]
    public bool NoiseSuppression { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.Latency"/> constraint.
    /// </summary>
    [JsonPropertyName("latency")]
    public bool Latency { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.ChannelCount"/> constraint.
    /// </summary>
    [JsonPropertyName("channelCount")]
    public bool ChannelCount { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.DeviceId"/> constraint.
    /// </summary>
    [JsonPropertyName("deviceId")]
    public bool DeviceId { get; set; }

    /// <summary>
    /// Indicates whether the User Agent recognizes the <see cref="MediaTrackConstraintSet.GroupId"/> constraint.
    /// </summary>
    [JsonPropertyName("groupId")]
    public bool GroupId { get; set; }
}
