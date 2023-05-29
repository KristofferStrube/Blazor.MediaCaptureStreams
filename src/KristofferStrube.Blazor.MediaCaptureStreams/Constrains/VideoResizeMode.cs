using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(VideoResizeModeConverter))]
public enum VideoResizeMode
{
    None,
    CropAndScale
}
