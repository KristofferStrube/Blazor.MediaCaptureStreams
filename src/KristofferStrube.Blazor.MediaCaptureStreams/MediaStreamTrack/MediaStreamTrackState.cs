namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// An enum that indicates the state of a <see cref="MediaStreamTrack"/>. In the <see cref="Live"/> state, the track is active and media is available for use by consumers.
/// </summary>
public enum MediaStreamTrackState
{
    /// <summary>
    /// The track is active (the track's underlying media source is making a best-effort attempt to provide data in real time).
    /// The output of a track in the <see cref="Live"/> state can be switched on and off with the enabled attribute.
    /// </summary>
    Live,
    /// <summary>
    /// The track has <c>ended</c> (the track's underlying media source is no longer providing data, and will never provide more data for this track). Once a track enters this state, it never exits it.
    /// For example, a video track in a <see cref="MediaStream"/> ends when the user unplugs the USB web camera that acts as the track's media source.
    /// </summary>
    Ended
}

/// <summary>
/// A class for extension methods related to <see cref="MediaStreamTrackState"/>.
/// </summary>
public static class MediaStreamTrackStateExtensions
{
    /// <summary>
    /// A method for parsing a <see cref="string"/> to a <see cref="MediaStreamTrackState"/> enum value.
    /// </summary>
    /// <param name="value">A string that is <c>"live"</c> or <c>"ended"</c></param>
    /// <returns>The matching <see cref="MediaStreamTrackState"/></returns>
    public static MediaStreamTrackState ParseMediaStreamTrackState(string value)
    {
        return value switch
        {
            "live" => MediaStreamTrackState.Live,
            "ended" => MediaStreamTrackState.Ended,
            _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(MediaStreamTrackState)}")
        };
    }
}
