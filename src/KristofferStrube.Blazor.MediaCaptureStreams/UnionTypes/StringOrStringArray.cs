using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<StringOrStringArray>))]
public class StringOrStringArray : UnionType
{
    public StringOrStringArray(string value) : base(value) { }
    public StringOrStringArray(string[] values) : base(values) { }

    public static implicit operator StringOrStringArray(string value)
        => new(value);

    public static implicit operator StringOrStringArray(string[] values)
        => new(values);
}
