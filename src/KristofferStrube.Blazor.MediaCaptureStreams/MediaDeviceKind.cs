namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// An enum specifying the different kinds a device can be.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#dom-mediadevicekind">See the API definition here</see>.</remarks>
public enum MediaDeviceKind
{
    /// <summary>
    /// Represents an audio input device; for example a microphone.
    /// </summary>
    AudioInput,
    /// <summary>
    /// Represents an audio output device; for example a pair of headphones.
    /// </summary>
    AudioOutput,
    /// <summary>
    /// Represents a video input device; for example a webcam.
    /// </summary>
    VideoInput
}

/// <summary>
/// A class for extension methods related to <see cref="MediaDeviceKind"/>.
/// </summary>
public static class MediaDeviceKindExtensions
{
    /// <summary>
    /// A method for parsing a <see cref="string"/> to a <see cref="MediaDeviceKind"/> enum value.
    /// </summary>
    /// <param name="value">A string that is <c>"audioinput"</c>, <c>"audiooutput"</c>, or <c>"videoinput"</c></param>
    /// <returns>The matching <see cref="MediaDeviceKind"/></returns>
    public static MediaDeviceKind ParseMediaDeviceKind(string value)
    {
        return value switch
        {
            "audioinput" => MediaDeviceKind.AudioInput,
            "audiooutput" => MediaDeviceKind.AudioOutput,
            "videoinput" => MediaDeviceKind.VideoInput,
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(MediaDeviceKind)}")
        };
    }
}
