using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Constraints for <see cref="bool"/>. Should either specify <see cref="Exact"/> or <see cref="Ideal"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-constrainbooleanparameters">See the API definition here</see>.</remarks>
public class ConstrainBooleanParameters
{
    /// <summary>
    /// The exact required value for this property.
    /// </summary>
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Exact { get; set; }

    /// <summary>
    /// The ideal (target) value for this property.
    /// </summary>
    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Ideal { get; set; }
}
