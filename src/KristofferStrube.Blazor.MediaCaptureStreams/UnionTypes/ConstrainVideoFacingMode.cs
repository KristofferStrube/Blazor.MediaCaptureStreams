using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainVideoFacingMode>))]
public class ConstrainVideoFacingMode : UnionType
{
    public ConstrainVideoFacingMode(VideoFacingMode value) : base(value) { }
    public ConstrainVideoFacingMode(VideoFacingMode[] values) : base(values) { }
    public ConstrainVideoFacingMode(ConstrainVideoFacingModeParameters value) : base(value) { }
    internal ConstrainVideoFacingMode(object value) : base(value) { }

    public static implicit operator ConstrainVideoFacingMode(VideoFacingMode value)
    {
        return new(value);
    }

    public static implicit operator ConstrainVideoFacingMode(VideoFacingMode[] values)
    {
        return new(values);
    }

    public static implicit operator ConstrainVideoFacingMode(ConstrainVideoFacingModeParameters value)
    {
        return new(value);
    }
}
