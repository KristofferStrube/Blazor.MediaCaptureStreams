using KristofferStrube.Blazor.MediaCaptureStreams.Constrains;
using KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class ConstrainDomString : UnionType
{
    public ConstrainDomString(string value) : base(value) { }
    public ConstrainDomString(string[] values) : base(values) { }
    public ConstrainDomString(ConstrainDOMStringParameters value) : base(value) { }

    public static implicit operator ConstrainDomString(string value)
        => new(value);

    public static implicit operator ConstrainDomString(string[] values)
        => new(values);

    public static implicit operator ConstrainDomString(ConstrainDOMStringParameters value)
        => new(value);
}
