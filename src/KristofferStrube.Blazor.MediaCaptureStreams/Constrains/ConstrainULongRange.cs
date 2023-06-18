using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Constraints for <see cref="ulong"/>. Should either specify <see cref="Exact"/> or <see cref="Ideal"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-constrainulongrange">See the API definition here</see>.</remarks>
public class ConstrainULongRange : ULongRange
{
    /// <summary>
    /// The exact required value for this property.
    /// </summary>
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Exact { get; set; }

    /// <summary>
    /// The ideal (target) value for this property.
    /// </summary>
    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Ideal { get; set; }
}
