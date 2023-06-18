using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// The enum describes the directions that the camera can face, as seen from the user's perspective.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-videofacingmodeenum">See the API definition here</see>.</remarks>
[JsonConverter(typeof(VideoFacingModeConverter))]
public enum VideoFacingMode
{
    /// <summary>
    /// The source is facing toward the user (a self-view camera).
    /// </summary>
    User,

    /// <summary>
    /// The source is facing away from the user (viewing the environment).
    /// </summary>
    Environment,

    /// <summary>
    /// The source is facing to the left of the user.
    /// </summary>
    Left,

    /// <summary>
    /// The source is facing to the right of the user.
    /// </summary>
    Right
}

internal class VideoFacingModeConverter : JsonConverter<VideoFacingMode>
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