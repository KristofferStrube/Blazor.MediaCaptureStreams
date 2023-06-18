using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Constraints for <see cref="VideoFacingMode"/>. Should either specify <see cref="Exact"/> or <see cref="Ideal"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dictionary-constraindomstringparameters-members">See the API definition here</see>.</remarks>
public class ConstrainVideoFacingModeParameters
{
    /// <summary>
    /// The exact required value for this property.
    /// </summary>
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoFacingModeOrArray? Exact { get; set; }

    /// <summary>
    /// The ideal (target) value for this property.
    /// </summary>
    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoFacingModeOrArray? Ideal { get; set; }

    internal static async Task<ConstrainVideoFacingModeParameters> CreateFromJSObjectReference(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        ConstrainVideoFacingModeParameters result = new();
        ValueReference reference = await ValueReference.CreateAsync(jSRuntime, jSReference);
        await SetVideoFacingModeOrArrayProperty(reference, "exact", (value) => result.Exact = new(value));
        await SetVideoFacingModeOrArrayProperty(reference, "ideal", (value) => result.Ideal = new(value));
        return result;
    }

    private static async Task SetVideoFacingModeOrArrayProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoFacingMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoFacingMode[]>();
        reference.Attribute = attribute;
        object? width = await reference.GetValueAsync();
        if (width is not null)
        {
            propertySetter(width);
        }
    }
}
