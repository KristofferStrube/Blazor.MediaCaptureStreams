using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Constrains;

public class ConstrainBooleanParameters
{
    [JsonPropertyName("exact")]
    public bool Exact { get; set; }

    [JsonPropertyName("ideal")]
    public bool Ideal { get; set; }
}
