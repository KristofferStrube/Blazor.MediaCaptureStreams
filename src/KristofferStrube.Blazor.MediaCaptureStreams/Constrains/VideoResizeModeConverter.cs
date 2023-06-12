using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class VideoResizeModeConverter : JsonConverter<VideoResizeMode>
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