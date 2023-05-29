using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainULong>))]
public class ConstrainULong : UnionType
{
    public ConstrainULong(ulong value) : base(value) { }
    public ConstrainULong(ConstrainULongRange value) : base(value) { }
    internal ConstrainULong(object value) : base(value) { }

    public static implicit operator ConstrainULong(ulong value)
    {
        return new(value);
    }

    public static implicit operator ConstrainULong(ConstrainULongRange value)
    {
        return new(value);
    }
}
