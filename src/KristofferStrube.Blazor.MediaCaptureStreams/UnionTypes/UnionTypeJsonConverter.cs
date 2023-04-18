using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Text.Json.JsonSerializer;

namespace KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

internal class UnionTypeJsonConverter<T> : JsonConverter<T> where T : UnionType
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new InvalidOperationException("Can't serialize UnionTypes from the Blazor.MediaCaptureStreams library.");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(Serialize(value.Value, options));
    }
}
