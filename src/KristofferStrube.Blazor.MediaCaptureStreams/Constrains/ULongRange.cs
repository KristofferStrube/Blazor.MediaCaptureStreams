using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A range for <see cref="ulong"/> values.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-ulongrange">See the API definition here</see>.</remarks>
public class ULongRange
{
    /// <summary>
    /// The maximum valid value of this property.
    /// </summary>
    [JsonPropertyName("max")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Max { get; set; }

    /// <summary>
    /// The minimum value of this property.
    /// </summary>
    [JsonPropertyName("min")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Min { get; set; }
}
