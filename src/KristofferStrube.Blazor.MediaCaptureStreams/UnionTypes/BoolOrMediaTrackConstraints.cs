using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

[JsonConverter(typeof(UnionTypeJsonConverter<BoolOrMediaTrackConstraints>))]
public class BoolOrMediaTrackConstraints : UnionType
{
    public BoolOrMediaTrackConstraints(bool value) : base(value) { }
    public BoolOrMediaTrackConstraints(MediaTrackConstraints value) : base(value) { }

    public static implicit operator BoolOrMediaTrackConstraints(bool value)
    {
        return new(value);
    }

    public static implicit operator BoolOrMediaTrackConstraints(MediaTrackConstraints value)
    {
        return new(value);
    }
}
