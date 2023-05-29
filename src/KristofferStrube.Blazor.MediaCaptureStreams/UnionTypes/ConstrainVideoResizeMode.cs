using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainVideoResizeMode>))]
public class ConstrainVideoResizeMode : UnionType
{
    public ConstrainVideoResizeMode(VideoResizeMode value) : base(value) { }
    public ConstrainVideoResizeMode(VideoResizeMode[] values) : base(values) { }
    public ConstrainVideoResizeMode(ConstrainVideoResizeModeParameters value) : base(value) { }
    internal ConstrainVideoResizeMode(object value) : base(value) { }

    public static implicit operator ConstrainVideoResizeMode(VideoResizeMode value)
    {
        return new(value);
    }

    public static implicit operator ConstrainVideoResizeMode(VideoResizeMode[] values)
    {
        return new(values);
    }

    public static implicit operator ConstrainVideoResizeMode(ConstrainVideoResizeModeParameters value)
    {
        return new(value);
    }
}
