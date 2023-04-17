using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Constrains;

public class ConstrainDOMStringParameters
{
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StringOrStringArray? Exact { get; set; }

    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StringOrStringArray? Ideal { get; set; }
}
