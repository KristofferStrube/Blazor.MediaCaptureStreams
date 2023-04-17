using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainDoubleRange : DoubleRange
{
    [JsonPropertyName("exact")]
    public double Exact { get; set; }

    [JsonPropertyName("ideal")]
    public double Ideal { get; set; }
}
