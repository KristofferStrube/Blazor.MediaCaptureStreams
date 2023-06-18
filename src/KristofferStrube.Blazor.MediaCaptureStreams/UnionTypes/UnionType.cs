using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams.UnionTypes;

/// <summary>
/// A common Union Type class.
/// </summary>
[JsonConverter(typeof(UnionTypeJsonConverter<UnionType>))]
public class UnionType
{
    /// <summary>
    /// Creates a Union Type class from a value.
    /// </summary>
    /// <param name="value"></param>
    protected UnionType(object value)
    {
        Value = value;
        Type = value.GetType();
    }

    /// <summary>
    /// The value of the Union Type.
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// The type of the Union Type.
    /// </summary>
    public Type Type { get; }
}
