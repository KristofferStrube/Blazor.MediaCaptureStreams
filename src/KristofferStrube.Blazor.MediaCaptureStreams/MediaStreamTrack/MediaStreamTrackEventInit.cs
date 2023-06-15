using KristofferStrube.Blazor.DOM;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// The track attribute represents the <see cref="MediaStreamTrack"/> object associated with the event.
/// </summary>
public class MediaStreamTrackEventInit : EventInit
{
    /// <summary>
    /// Constructs a <see cref="MediaStreamTrackEventInit"/> with the required <see cref="MediaStreamTrack"/>.
    /// </summary>
    /// <param name="track">A <see cref="MediaStreamTrack"/>.</param>
    public MediaStreamTrackEventInit(MediaStreamTrack track)
    {
        Track = track;
    }

    /// <summary>
    /// The required <see cref="MediaStreamTrack"/>.
    /// </summary>
    [JsonPropertyName("track")]
    public MediaStreamTrack Track { get; set; }
}
