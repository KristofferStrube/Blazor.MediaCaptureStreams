using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;

/// <summary>
/// Some operations throw or fire OverconstrainedError. This is an extension of <see cref="DOMException"/> that carries additional information related to constraints failure.
/// </summary>
public class OverconstrainedErrorException : DOMException
{
    /// <summary>
    /// The name of a constraint associated with this error, or "" if no specific constraint name is revealed.
    /// </summary>
    public string Constraint { get; }

    /// <inheritdoc />
    /// <param name="constraint">The name of the constraint associated with the error.</param>
    protected OverconstrainedErrorException(string constraint, string message, string? jSStackTrace, Exception innerException) : base(message, "OverconstrainedErrorException", jSStackTrace, innerException)
    {
        Constraint = constraint;
    }
}
