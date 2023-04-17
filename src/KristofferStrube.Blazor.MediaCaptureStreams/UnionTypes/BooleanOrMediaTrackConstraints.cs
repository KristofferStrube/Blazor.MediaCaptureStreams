using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class BooleanOrMediaTrackConstraints : UnionType
{
    public BooleanOrMediaTrackConstraints(bool value) : base(value) { }
    public BooleanOrMediaTrackConstraints(MediaTrackConstraints value) : base(value) { }

    public static implicit operator BooleanOrMediaTrackConstraints(bool value)
        => new(value);

    public static implicit operator BooleanOrMediaTrackConstraints(MediaTrackConstraints value)
        => new(value);
}
