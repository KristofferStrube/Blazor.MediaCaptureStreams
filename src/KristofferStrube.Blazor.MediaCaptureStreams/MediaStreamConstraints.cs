using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaStreamConstraints
{
    [JsonPropertyName("video")]
    public BoolOrMediaTrackConstraints Video { get; set; } = false;

    [JsonPropertyName("audio")]
    public BoolOrMediaTrackConstraints Audio { get; set; } = false;
}
