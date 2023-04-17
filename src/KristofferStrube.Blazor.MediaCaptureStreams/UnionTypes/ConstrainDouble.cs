using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainDouble : UnionType
{
    public ConstrainDouble(double value) : base(value) { }
    public ConstrainDouble(ConstrainDoubleRange value) : base(value) { }

    public static implicit operator ConstrainDouble(ulong value)
        => new(value);

    public static implicit operator ConstrainDouble(ConstrainDoubleRange value)
        => new(value);
}
