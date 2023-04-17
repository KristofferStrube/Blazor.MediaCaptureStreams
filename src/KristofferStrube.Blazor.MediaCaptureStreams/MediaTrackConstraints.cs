using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackConstraints : MediaTrackConstraintSet
{
    [JsonPropertyName("advanced")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaTrackConstraintSet[]? Advanced { get; set; }
}
