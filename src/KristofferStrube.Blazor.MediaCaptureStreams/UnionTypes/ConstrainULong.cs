using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainULong : UnionType
{
    public ConstrainULong(ulong value) : base(value) { }
    public ConstrainULong(ConstrainULongRange value) : base(value) { }

    public static implicit operator ConstrainULong(ulong value)
        => new(value);

    public static implicit operator ConstrainULong(ConstrainULongRange value)
        => new(value);
}
