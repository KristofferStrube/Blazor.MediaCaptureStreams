using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Constraints for <see cref="VideoResizeMode"/>. Should either specify <see cref="Exact"/> or <see cref="Ideal"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dictionary-constraindomstringparameters-members">See the API definition here</see>.</remarks>
public class ConstrainVideoResizeModeParameters
{
    /// <summary>
    /// The exact required value for this property.
    /// </summary>
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoResizeModeOrArray? Exact { get; set; }

    /// <summary>
    /// The ideal (target) value for this property.
    /// </summary>
    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoResizeModeOrArray? Ideal { get; set; }

    internal static async Task<ConstrainVideoResizeModeParameters> CreateFromJSObjectReference(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        ConstrainVideoResizeModeParameters result = new();
        ValueReference reference = await ValueReference.CreateAsync(jSRuntime, jSReference);
        await SetVideoResizeModeOrArrayProperty(reference, "exact", (value) => result.Exact = new(value));
        await SetVideoResizeModeOrArrayProperty(reference, "ideal", (value) => result.Ideal = new(value));
        return result;
    }

    private static async Task SetVideoResizeModeOrArrayProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoResizeMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoResizeMode[]>();
        reference.Attribute = attribute;
        object? width = await reference.GetValueAsync();
        if (width is not null)
        {
            propertySetter(width);
        }
    }
}
