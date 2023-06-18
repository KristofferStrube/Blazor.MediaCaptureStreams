using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="bool"/> or a <see cref="ConstrainBooleanParameters"/> object.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainBoolean>))]
public class ConstrainBoolean : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainBoolean"/> from a <see cref="bool"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="bool"/>.</param>
    public ConstrainBoolean(bool value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainBoolean"/> from a <see cref="ConstrainBooleanParameters"/> object explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainBooleanParameters"/> object.</param>
    public ConstrainBoolean(ConstrainBooleanParameters value) : base(value) { }

    internal ConstrainBoolean(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainBoolean"/> from a <see cref="bool"/>.
    /// </summary>
    /// <param name="value">A <see cref="bool"/>.</param>
    public static implicit operator ConstrainBoolean(bool value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainBoolean"/> from a <see cref="ConstrainBooleanParameters"/> object.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainBooleanParameters"/> object.</param>
    public static implicit operator ConstrainBoolean(ConstrainBooleanParameters value)
    {
        return new(value);
    }
}
