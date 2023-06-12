using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class VideoFacingModeConverter : JsonConverter<VideoFacingMode>
{
    public override VideoFacingMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return value switch
        {
            "user" => VideoFacingMode.User,
            "environment" => VideoFacingMode.Environment,
            "left" => VideoFacingMode.Left,
            "right" => VideoFacingMode.Right,
            _ => throw new ArgumentException($"The value '{value}' was not valid for enum VideoFacingMode")
        };
    }

    public override void Write(Utf8JsonWriter writer, VideoFacingMode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLower());
    }
}