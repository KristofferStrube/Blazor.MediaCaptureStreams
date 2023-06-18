using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// The enum describes the means by which the resolution can be derived by the User Agent. In other words, whether the User Agent is allowed to use cropping and downscaling on the camera output.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-videoresizemodeenum">See the API definition here</see>.</remarks>
[JsonConverter(typeof(VideoResizeModeConverter))]
public enum VideoResizeMode
{
    /// <summary>
    /// This resolution and frame rate is offered by the camera, its driver, or the OS.
    /// </summary>
    /// <remarks>
    /// The User Agent may report this value to disguise concurrent use, but only when the camera is in use in another navigable.
    /// </remarks>
    None,

    /// <summary>
    /// This resolution is downscaled and/or cropped from a higher camera resolution by the User Agent, or its frame rate is decimated by the User Agent. The media must not be upscaled, stretched or have fake data created that did not occur in the input source.
    /// </summary>
    CropAndScale
}

internal class VideoResizeModeConverter : JsonConverter<VideoResizeMode>
{
    public override VideoResizeMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return value switch
        {
            "none" => VideoResizeMode.None,
            "crop-and-scale" => VideoResizeMode.CropAndScale,
            _ => throw new ArgumentException($"The value '{value}' was not valid for enum VideoResizeMode")
        };
    }

    public override void Write(Utf8JsonWriter writer, VideoResizeMode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value switch
        {
            VideoResizeMode.None => "none",
            VideoResizeMode.CropAndScale => "crop-and-scale",
            _ => throw new ArgumentException("The enum value 'value' was not valid for enum VideoResizeMode")
        });
    }
}