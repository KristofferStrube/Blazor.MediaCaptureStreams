using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainVideoFacingModeParameters
{
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoFacingModeOrArray? Exact { get; set; }

    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoFacingModeOrArray? Ideal { get; set; }

    public static async Task<ConstrainVideoFacingModeParameters> CreateFromJSObjectReference(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        var result = new ConstrainVideoFacingModeParameters();
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
        var width = await reference.GetValueAsync();
        if (width is not null)
        {
            propertySetter(width);
        }
    }
}
