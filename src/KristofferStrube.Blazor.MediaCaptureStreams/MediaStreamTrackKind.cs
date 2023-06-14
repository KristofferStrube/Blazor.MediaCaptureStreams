namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// The kinds of <see cref="MediaStreamTrack"/>.
/// </summary>
public enum MediaStreamTrackKind
{
    /// <summary>
    /// The source is an audio source.
    /// </summary>
    Audio,
    /// <summary>
    /// The source is an video source.
    /// </summary>
    Video,
    /// <summary>
    /// The kind that a <see cref="MediaStreamTrack"/> is first created.
    /// </summary>
    Main
}

/// <summary>
/// A class for extension methods related to <see cref="MediaStreamTrackKind"/>.
/// </summary>
public static class MediaStreamTrackKindExtensions
{
    /// <summary>
    /// A method for parsing a <see cref="string"/> to a <see cref="MediaStreamTrackKind"/> enum value.
    /// </summary>
    /// <param name="value">A string that is <c>"audio"</c>, <c>"video"</c>, or <c>"main"</c></param>
    /// <returns>The matching <see cref="MediaStreamTrackKind"/></returns>
    public static MediaStreamTrackKind ParseMediaStreamTrackKind(string value)
    {
        return value switch
        {
            "audio" => MediaStreamTrackKind.Audio,
            "video" => MediaStreamTrackKind.Video,
            "main" => MediaStreamTrackKind.Main,
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(MediaStreamTrackKind)}")
        };
    }
}
