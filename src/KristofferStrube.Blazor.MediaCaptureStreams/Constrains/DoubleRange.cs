using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class DoubleRange
{
    [JsonPropertyName("max")]
    public double Max { get; set; }

    [JsonPropertyName("min")]
    public double Min { get; set; }
}
