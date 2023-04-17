using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

[JsonConverter(typeof(UnionTypeJsonConverter))]
public class UnionType
{
    protected UnionType(object value)
    {
        Value = value;
    }

    public object Value { get; init; }
}
