using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;

/// <summary>
/// Some operations throw or fire OverconstrainedError. This is an extension of <see cref="DOMException"/> that carries additional information related to constraints failure.
/// </summary>
public class OverconstrainedErrorException : DOMException
{
    public IJSObjectReference JSReference { get; }

    /// <summary>
    /// The name of the constraint associated with this error, or "" if no specific constraint name is revealed.
    /// </summary>
    public string Constraint { get; }

    /// <inheritdoc />
    /// <param name="constraint">The constraint that was overconstrained.</param>
    public OverconstrainedErrorException(string constraint, string message, string? jSStackTrace, Exception innerException) : base(message, "OverconstrainedErrorException", jSStackTrace, innerException)
    {
        Constraint = constraint;
    }
}
