using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaStreamConstraints
{
    [JsonPropertyName("video")]
    public BooleanOrMediaTrackConstraints Video { get; set; } = false;

    [JsonPropertyName("audio")]
    public BooleanOrMediaTrackConstraints Audio { get; set; } = false;
}
