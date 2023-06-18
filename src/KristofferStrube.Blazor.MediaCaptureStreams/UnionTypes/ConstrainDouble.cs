using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="double"/> or a <see cref="ConstrainDoubleRange"/>.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainDouble>))]
public class ConstrainDouble : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainDouble"/> from a <see cref="double"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="double"/>.</param>
    public ConstrainDouble(double value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainDouble"/> from a <see cref="ConstrainDoubleRange"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainDoubleRange"/>.</param>
    public ConstrainDouble(ConstrainDoubleRange value) : base(value) { }

    internal ConstrainDouble(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainDouble"/> from a <see cref="double"/>.
    /// </summary>
    /// <param name="value">A <see cref="double"/>.</param>
    public static implicit operator ConstrainDouble(double value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainDouble"/> from a <see cref="ConstrainDoubleRange"/>.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainDoubleRange"/>.</param>
    public static implicit operator ConstrainDouble(ConstrainDoubleRange value)
    {
        return new(value);
    }
}
