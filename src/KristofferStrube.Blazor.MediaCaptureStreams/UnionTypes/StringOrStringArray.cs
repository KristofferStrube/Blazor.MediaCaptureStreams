using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="string"/> or an array of <see cref="string"/>s.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<StringOrStringArray>))]
public class StringOrStringArray : UnionType
{
    /// <summary>
    /// Creates a <see cref="StringOrStringArray"/> from a <see cref="string"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="string"/>.</param>
    public StringOrStringArray(string value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="StringOrStringArray"/> from an array of <see cref="string"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="string"/>s.</param>
    public StringOrStringArray(string[] values) : base(values) { }

    internal StringOrStringArray(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="StringOrStringArray"/> from a <see cref="string"/>.
    /// </summary>
    /// <param name="value">A <see cref="string"/>.</param>
    public static implicit operator StringOrStringArray(string value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="StringOrStringArray"/> from an array of <see cref="string"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="string"/>s.</param>
    public static implicit operator StringOrStringArray(string[] values)
    {
        return new(values);
    }
}
