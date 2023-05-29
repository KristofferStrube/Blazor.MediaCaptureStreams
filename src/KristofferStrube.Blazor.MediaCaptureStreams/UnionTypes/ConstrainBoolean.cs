using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainBoolean>))]
public class ConstrainBoolean : UnionType
{
    public ConstrainBoolean(bool value) : base(value) { }
    public ConstrainBoolean(ConstrainBooleanParameters value) : base(value) { }
    internal ConstrainBoolean(object value) : base(value) { }

    public static implicit operator ConstrainBoolean(bool value)
    {
        return new(value);
    }

    public static implicit operator ConstrainBoolean(ConstrainBooleanParameters value)
    {
        return new(value);
    }
}
