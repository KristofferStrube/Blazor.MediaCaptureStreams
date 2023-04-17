using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainULongRange : ULongRange
{
    [JsonPropertyName("exact")]
    public ulong Exact { get; set; }

    [JsonPropertyName("ideal")]
    public ulong Ideal { get; set; }
}
