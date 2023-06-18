using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Constraints for <see cref="string"/>s. Should either specify <see cref="Exact"/> or <see cref="Ideal"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-constraindomstringparameters">See the API definition here</see>.</remarks>
public class ConstrainDOMStringParameters
{
    /// <summary>
    /// The exact required value for this property.
    /// </summary>
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StringOrStringArray? Exact { get; set; }

    /// <summary>
    /// The ideal (target) value for this property.
    /// </summary>
    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StringOrStringArray? Ideal { get; set; }
}
