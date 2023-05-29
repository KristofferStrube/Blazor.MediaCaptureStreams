using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainDomString>))]
public class ConstrainDomString : UnionType
{
    public ConstrainDomString(string value) : base(value) { }
    public ConstrainDomString(string[] values) : base(values) { }
    public ConstrainDomString(ConstrainDOMStringParameters value) : base(value) { }
    internal ConstrainDomString(object value) : base(value) { }

    public static implicit operator ConstrainDomString(string value)
    {
        return new(value);
    }

    public static implicit operator ConstrainDomString(string[] values)
    {
        return new(values);
    }

    public static implicit operator ConstrainDomString(ConstrainDOMStringParameters value)
    {
        return new(value);
    }
}
