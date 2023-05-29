using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

[JsonConverter(typeof(UnionTypeJsonConverter<UnionType>))]
public class UnionType
{
    protected UnionType(object value)
    {
        Value = value;
        Type = value.GetType();
    }

    public object Value { get; init; }
    public Type Type { get; init; }
}
