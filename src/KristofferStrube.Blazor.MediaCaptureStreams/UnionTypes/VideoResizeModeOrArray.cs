using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<VideoResizeModeOrArray>))]
public class VideoResizeModeOrArray : UnionType
{
    public VideoResizeModeOrArray(VideoResizeMode value) : base(value) { }
    public VideoResizeModeOrArray(VideoResizeMode[] values) : base(values) { }
    internal VideoResizeModeOrArray(object value) : base(value) { }

    public static implicit operator VideoResizeModeOrArray(VideoResizeMode value)
    {
        return new(value);
    }

    public static implicit operator VideoResizeModeOrArray(VideoResizeMode[] values)
    {
        return new(values);
    }
}
