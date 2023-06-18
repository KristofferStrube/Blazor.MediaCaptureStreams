using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="VideoResizeMode"/>, an array of <see cref="VideoResizeMode"/>s, or a <see cref="ConstrainVideoResizeModeParameters"/> object.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<ConstrainVideoResizeMode>))]
public class ConstrainVideoResizeMode : UnionType
{
    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from a <see cref="VideoResizeMode"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="VideoResizeMode"/>.</param>
    public ConstrainVideoResizeMode(VideoResizeMode value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from an array of <see cref="VideoResizeMode"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoResizeMode"/>s.</param>
    public ConstrainVideoResizeMode(VideoResizeMode[] values) : base(values) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from a <see cref="ConstrainVideoResizeModeParameters"/> object explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainVideoResizeModeParameters"/> object.</param>
    public ConstrainVideoResizeMode(ConstrainVideoResizeModeParameters value) : base(value) { }

    internal ConstrainVideoResizeMode(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from a <see cref="VideoResizeMode"/>.
    /// </summary>
    /// <param name="value">A <see cref="VideoResizeMode"/>.</param>
    public static implicit operator ConstrainVideoResizeMode(VideoResizeMode value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from an array of <see cref="VideoResizeMode"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoResizeMode"/>s.</param>
    public static implicit operator ConstrainVideoResizeMode(VideoResizeMode[] values)
    {
        return new(values);
    }

    /// <summary>
    /// Creates a <see cref="ConstrainVideoResizeMode"/> from a <see cref="ConstrainVideoResizeModeParameters"/> object.
    /// </summary>
    /// <param name="value">A <see cref="ConstrainVideoResizeModeParameters"/> object.</param>
    public static implicit operator ConstrainVideoResizeMode(ConstrainVideoResizeModeParameters value)
    {
        return new(value);
    }
}
