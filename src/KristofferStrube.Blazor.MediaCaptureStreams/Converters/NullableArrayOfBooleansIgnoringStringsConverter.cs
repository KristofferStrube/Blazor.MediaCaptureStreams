using System.Text.Json;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Converters;

/// <summary>
/// This is going to be removed, as it is only here to make sure that strings in the array of echoCancellation options doesn't break the wrapper.
/// </summary>
internal class NullableArrayOfBooleansIgnoringStringsConverter : JsonConverter<bool[]?>
{
    public override bool[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
        {
            return null;
        }
        if (reader.TokenType is not JsonTokenType.StartArray)
        {
            throw new JsonException($"Tried to deserialize a nullable array of booleans, but encounted a token of type '{reader.TokenType}' which was unexpected. Expected a '{JsonTokenType.Null}' or {JsonTokenType.StartArray}");
        }

        List<bool> entries = new();
        while (true)
        {
            reader.Read();
            if (reader.TokenType is JsonTokenType.EndArray)
            {
                break;
            }
            if (reader.TokenType is JsonTokenType.True)
            {
                entries.Add(true);
            }
            else if (reader.TokenType is JsonTokenType.False)
            {
                entries.Add(false);
            }
        }
        return entries.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, bool[]? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStartArray();
            foreach (bool boolean in value)
            {
                writer.WriteBooleanValue(boolean);
            }
            writer.WriteEndArray();
        }
    }
}
