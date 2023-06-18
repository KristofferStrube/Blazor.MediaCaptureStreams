using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="string"/>, an array of <see cref="string"/>s, or a <see cref="ConstrainDOMStringParameters"/> object.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainDomString>))]
public class ConstrainDomString : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from a <see cref="string"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="string"/>.</param>
    public ConstrainDomString(string value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from an array of <see cref="string"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="string"/>s.</param>
    public ConstrainDomString(string[] values) : base(values) { }

    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from a <see cref="ConstrainDOMStringParameters"/> object explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainDOMStringParameters"/> object.</param>
    public ConstrainDomString(ConstrainDOMStringParameters value) : base(value) { }

    internal ConstrainDomString(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from a <see cref="string"/>.
    /// </summary>
    /// <param name="value">A <see cref="string"/>.</param>
    public static implicit operator ConstrainDomString(string value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from an array of <see cref="string"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="string"/>s.</param>
    public static implicit operator ConstrainDomString(string[] values)
    {
        return new(values);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainDomString"/> from a <see cref="ConstrainDOMStringParameters"/> object.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainDOMStringParameters"/> object.</param>
    public static implicit operator ConstrainDomString(ConstrainDOMStringParameters value)
    {
        return new(value);
    }
}
