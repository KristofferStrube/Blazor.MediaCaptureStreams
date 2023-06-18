using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="VideoFacingMode"/>, an array of <see cref="VideoFacingMode"/>s, or a <see cref="ConstrainVideoFacingModeParameters"/> object.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainVideoFacingMode>))]
public class ConstrainVideoFacingMode : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from a <see cref="VideoFacingMode"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="VideoFacingMode"/>.</param>
    public ConstrainVideoFacingMode(VideoFacingMode value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from an array of <see cref="VideoFacingMode"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoFacingMode"/>s.</param>
    public ConstrainVideoFacingMode(VideoFacingMode[] values) : base(values) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from a <see cref="ConstrainVideoFacingModeParameters"/> object explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainVideoFacingModeParameters"/> object.</param>
    public ConstrainVideoFacingMode(ConstrainVideoFacingModeParameters value) : base(value) { }

    internal ConstrainVideoFacingMode(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from a <see cref="VideoFacingMode"/>.
    /// </summary>
    /// <param name="value">A <see cref="VideoFacingMode"/>.</param>
    public static implicit operator ConstrainVideoFacingMode(VideoFacingMode value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from an array of <see cref="VideoFacingMode"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoFacingMode"/>s.</param>
    public static implicit operator ConstrainVideoFacingMode(VideoFacingMode[] values)
    {
        return new(values);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoFacingMode"/> from a <see cref="ConstrainVideoFacingModeParameters"/> object.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainVideoFacingModeParameters"/> object.</param>
    public static implicit operator ConstrainVideoFacingMode(ConstrainVideoFacingModeParameters value)
    {
        return new(value);
    }
}
