using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="VideoFacingMode"/> or an array of <see cref="VideoFacingMode"/>s.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<VideoFacingModeOrArray>))]
public class VideoFacingModeOrArray : UnionType
{
    /// <summary>
    /// Creates a <see cref="VideoFacingModeOrArray"/> from a <see cref="VideoFacingMode"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="VideoFacingMode"/>.</param>
    public VideoFacingModeOrArray(VideoFacingMode value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="VideoFacingModeOrArray"/> from an array of <see cref="VideoFacingMode"/>s explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoFacingMode"/>s.</param>
    public VideoFacingModeOrArray(VideoFacingMode[] values) : base(values) { }

    internal VideoFacingModeOrArray(object value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="VideoFacingModeOrArray"/> from a <see cref="VideoFacingMode"/>.
    /// </summary>
    /// <param name="value">A <see cref="VideoFacingMode"/>.</param>
    public static implicit operator VideoFacingModeOrArray(VideoFacingMode value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="VideoFacingModeOrArray"/> from an array of <see cref="VideoFacingMode"/>s.
    /// </summary>
    /// <param name="values">An array of <see cref="VideoFacingMode"/>s.</param>
    public static implicit operator VideoFacingModeOrArray(VideoFacingMode[] values)
    {
        return new(values);
    }
}
