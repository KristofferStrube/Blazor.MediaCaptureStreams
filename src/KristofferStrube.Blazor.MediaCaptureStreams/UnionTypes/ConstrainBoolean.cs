using KristofferStrube.Blazor.MediaCaptureStreams.Constrains;
using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainBoolean : UnionType
{
    public ConstrainBoolean(bool value) : base(value) { }
    public ConstrainBoolean(ConstrainBooleanParameters value) : base(value) { }

    public static implicit operator ConstrainBoolean(bool value)
        => new(value);

    public static implicit operator ConstrainBoolean(ConstrainBooleanParameters value)
        => new(value);
}
