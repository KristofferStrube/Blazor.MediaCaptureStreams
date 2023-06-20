using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// <see cref="MediaStreamConstraints"/> is used to instruct the User Agent what sort of <see cref="MediaStreamTrack"/>s to include in the <see cref="MediaStream"/> returned by <see cref="MediaDevices.GetUserMediaAsync(MediaStreamConstraints)"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#mediastreamconstraints">See the API definition here</see>.</remarks>
public class MediaStreamConstraints
{
    /// <summary>
    /// If <see langword="true"/>, it requests that the returned <see cref="MediaStream"/> contain a video track.<br />
    /// If a <see cref="MediaTrackConstraints"/> is provided, it further specifies the nature and settings of the video track.<br />
    /// If <see langword="false"/>, the <see cref="MediaStream"/> must not contain a video track.
    /// </summary>
    [JsonPropertyName("video")]
    public BoolOrMediaTrackConstraints Video { get; set; } = false;

    /// <summary>
    /// If <see langword="true"/>, it requests that the returned  <see cref="MediaStream"/> contain an audio track.<br />
    /// If a <see cref="MediaTrackConstraints"/> structure is provided, it further specifies the nature and settings of the audio track.<br />
    /// If <see langword="false"/>, the <see cref="MediaStream"/> must not contain an audio track.
    /// </summary>
    [JsonPropertyName("audio")]
    public BoolOrMediaTrackConstraints Audio { get; set; } = false;
}
