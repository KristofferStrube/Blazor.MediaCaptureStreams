using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackConstraintSet
{
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Width { get; set; }

    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? Height { get; set; }

    [JsonPropertyName("aspectRatio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? AspectRatio { get; set; }

    [JsonPropertyName("frameRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? FrameRate { get; set; }

    [JsonPropertyName("facingMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainVideoFacingMode? FacingMode { get; set; }

    [JsonPropertyName("resizeMode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainVideoResizeMode? ResizeMode { get; set; }

    [JsonPropertyName("sampleRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleRate { get; set; }

    [JsonPropertyName("sampleSize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? SampleSize { get; set; }

    [JsonPropertyName("echoCancellation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? EchoCancellation { get; set; }

    [JsonPropertyName("autoGainControl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? AutoGainControl { get; set; }

    [JsonPropertyName("noiseSuppression")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainBoolean? NoiseSuppression { get; set; }

    [JsonPropertyName("latency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDouble? Latency { get; set; }

    [JsonPropertyName("channelCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainULong? ChannelCount { get; set; }

    [JsonPropertyName("deviceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? DeviceId { get; set; }

    [JsonPropertyName("groupId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConstrainDomString? GroupId { get; set; }

    public static async Task<T> HydrateMediaTrackConstraintSet<T>(T hydrateObject, IJSRuntime jSRuntime, IJSObjectReference jSReference) where T : MediaTrackConstraintSet
    {
        ValueReference reference = await ValueReference.CreateAsync(jSRuntime, jSReference);
        await SetConstrainULongProperty(reference, "width", (value) => hydrateObject.Width = new(value));
        await SetConstrainULongProperty(reference, "height", (value) => hydrateObject.Height = new(value));
        await SetConstrainDoubleProperty(reference, "aspectRatio", (value) => hydrateObject.AspectRatio = new(value));
        await SetConstrainDoubleProperty(reference, "frameRate", (value) => hydrateObject.FrameRate = new(value));
        await SetConstrainVideoFacingModeProperty(reference, "facingMode", (value) => hydrateObject.FacingMode = new(value));
        await SetConstrainVideoResizeModeProperty(reference, "resizeMode", (value) => hydrateObject.ResizeMode = new(value));
        await SetConstrainULongProperty(reference, "sampleRate", (value) => hydrateObject.SampleRate = new(value));
        await SetConstrainULongProperty(reference, "sampleSize", (value) => hydrateObject.SampleSize = new(value));
        await SetConstrainBooleanProperty(reference, "echoCancellation", (value) => hydrateObject.EchoCancellation = new(value));
        await SetConstrainBooleanProperty(reference, "autoGainControl", (value) => hydrateObject.AutoGainControl = new(value));
        await SetConstrainBooleanProperty(reference, "noiseSuppression", (value) => hydrateObject.NoiseSuppression = new(value));
        await SetConstrainDoubleProperty(reference, "latency", (value) => hydrateObject.Latency = new(value));
        await SetConstrainULongProperty(reference, "channelCount", (value) => hydrateObject.ChannelCount = new(value));
        await SetConstrainDomStringProperty(reference, "deviceId", (value) => hydrateObject.DeviceId = new(value));
        await SetConstrainDomStringProperty(reference, "groupId", (value) => hydrateObject.GroupId = new(value));
        return hydrateObject;
    }

    private static async Task SetConstrainULongProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["number"] = async () => await reference.GetValueAsync<ulong>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainULongRange>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainDoubleProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["number"] = async () => await reference.GetValueAsync<double>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainDoubleRange>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainDomStringProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<string>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<string[]>();
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainDOMStringParameters>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainVideoFacingModeProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoFacingMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoFacingMode[]>();
        reference.ValueMapper["object"] = async () => await ConstrainVideoFacingModeParameters.CreateFromJSObjectReference(reference.JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainVideoResizeModeProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["string"] = async () => await reference.GetValueAsync<VideoResizeMode>();
        reference.ValueMapper["array"] = async () => await reference.GetValueAsync<VideoResizeMode[]>();
        reference.ValueMapper["object"] = async () => await ConstrainVideoResizeModeParameters.CreateFromJSObjectReference(reference.JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetConstrainBooleanProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.ValueMapper["object"] = async () => await reference.GetValueAsync<ConstrainBooleanParameters>();
        await SetProperty(reference, attribute, propertySetter);
    }

    private static async Task SetProperty(ValueReference reference, string attribute, Action<object> propertySetter)
    {
        reference.Attribute = attribute;
        var value = await reference.GetValueAsync();
        if (value is not null)
        {
            propertySetter(value);
        }
    }
}
