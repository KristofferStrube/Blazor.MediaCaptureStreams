using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="ulong"/> or a <see cref="ConstrainULongRange"/>.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainULong>))]
public class ConstrainULong : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainULong"/> from a <see cref="ulong"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ulong"/>.</param>
    public ConstrainULong(ulong value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainULong"/> from a <see cref="ConstrainULongRange"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainULongRange"/>.</param>
    public ConstrainULong(ConstrainULongRange value) : base(value) { }

    internal ConstrainULong(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainULong"/> from a <see cref="ulong"/>.
    /// </summary>
    /// <param name="value">A <see cref="ulong"/>.</param>
    public static implicit operator ConstrainULong(ulong value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainULong"/> from a <see cref="ConstrainULongRange"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainULongRange"/>.</param>
    public static implicit operator ConstrainULong(ConstrainULongRange value)
    {
        return new(value);
    }
}
