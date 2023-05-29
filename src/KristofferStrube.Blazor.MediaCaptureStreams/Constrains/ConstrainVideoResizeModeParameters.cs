using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainVideoResizeModeParameters
{
    [JsonPropertyName("exact")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoResizeModeOrArray? Exact { get; set; }

    [JsonPropertyName("ideal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoResizeModeOrArray? Ideal { get; set; }

    public static async Task<ConstrainVideoResizeModeParameters> CreateFromJSObjectReference(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        var result = new ConstrainVideoResizeModeParameters();
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
        var width = await reference.GetValueAsync();
        if (width is not null)
        {
            propertySetter(width);
        }
    }
}
