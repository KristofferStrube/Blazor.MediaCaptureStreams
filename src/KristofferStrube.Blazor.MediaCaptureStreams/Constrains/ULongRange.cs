using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ULongRange
{
    [JsonPropertyName("max")]
    public ulong Max { get; set; }

    [JsonPropertyName("min")]
    public ulong Min { get; set; }
}
