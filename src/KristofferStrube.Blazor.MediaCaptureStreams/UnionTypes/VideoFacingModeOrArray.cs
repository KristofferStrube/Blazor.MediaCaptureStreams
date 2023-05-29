using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<VideoFacingModeOrArray>))]
public class VideoFacingModeOrArray : UnionType
{
    public VideoFacingModeOrArray(VideoFacingMode value) : base(value) { }
    public VideoFacingModeOrArray(VideoFacingMode[] values) : base(values) { }
    internal VideoFacingModeOrArray(object value) : base(value) { }

    public static implicit operator VideoFacingModeOrArray(VideoFacingMode value)
    {
        return new(value);
    }

    public static implicit operator VideoFacingModeOrArray(VideoFacingMode[] values)
    {
        return new(values);
    }
}
