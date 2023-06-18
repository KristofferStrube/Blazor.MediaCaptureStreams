using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Union Type representing either a single <see cref="bool"/> or a <see cref="MediaTrackConstraints"/> object.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<BoolOrMediaTrackConstraints>))]
public class BoolOrMediaTrackConstraints : UnionType
{
    /// <summary>
    /// Creates a <see cref="BoolOrMediaTrackConstraints"/> from a <see cref="bool"/> explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="bool"/>.</param>
    public BoolOrMediaTrackConstraints(bool value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="BoolOrMediaTrackConstraints"/> from a <see cref="MediaTrackConstraints"/> object explicitly instead of using the implicit converter.
    /// </summary>
    /// <param name="value">A <see cref="MediaTrackConstraints"/> object.</param>
    public BoolOrMediaTrackConstraints(MediaTrackConstraints value) : base(value) { }

    /// <summary>
    /// Creates a <see cref="BoolOrMediaTrackConstraints"/> from a <see cref="bool"/>.
    /// </summary>
    /// <param name="value">A <see cref="bool"/>.</param>
    public static implicit operator BoolOrMediaTrackConstraints(bool value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a <see cref="BoolOrMediaTrackConstraints"/> from a <see cref="MediaTrackConstraints"/> object.
    /// </summary>
    /// <param name="value">A <see cref="MediaTrackConstraints"/> object.</param>
    public static implicit operator BoolOrMediaTrackConstraints(MediaTrackConstraints value)
    {
        return new(value);
    }
}
