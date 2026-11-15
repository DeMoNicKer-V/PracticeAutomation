using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Utils;

/// Reads booleans sent either as true/false or as 0/1.
public sealed class NumberToBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.GetInt32() != 0,
            _ => throw new JsonException($"Unexpected token {reader.TokenType} for a boolean")
        };
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}