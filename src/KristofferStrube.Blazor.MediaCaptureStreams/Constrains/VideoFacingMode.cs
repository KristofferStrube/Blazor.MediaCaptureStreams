using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(VideoFacingModeConverter))]
public enum VideoFacingMode
{
    User,
    Environment,
    Left,
    Right
}
