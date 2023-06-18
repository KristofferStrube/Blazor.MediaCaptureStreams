using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="VideoResizeMode"/> or an array of <see cref="VideoResizeMode"/>s.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<VideoResizeModeOrArray>))]
public class VideoResizeModeOrArray : UnionType
{
    /// <summary>
    /// Creates a <see cref="VideoResizeModeOrArray"/> from a <see cref="VideoResizeMode"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="VideoResizeMode"/>.</param>
    public VideoResizeModeOrArray(VideoResizeMode value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="VideoResizeModeOrArray"/> from an array of <see cref="VideoResizeMode"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoResizeMode"/>s.</param>
    public VideoResizeModeOrArray(VideoResizeMode[] values) : base(values) { }

    internal VideoResizeModeOrArray(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="VideoResizeModeOrArray"/> from a <see cref="VideoResizeMode"/>.
    /// </summary>
    /// <param name="value">A <see cref="VideoResizeMode"/>.</param>
    public static implicit operator VideoResizeModeOrArray(VideoResizeMode value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="VideoResizeModeOrArray"/> from an array of <see cref="VideoResizeMode"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoResizeMode"/>s.</param>
    public static implicit operator VideoResizeModeOrArray(VideoResizeMode[] values)
    {
        return new(values);
    }
}
