namespace KristofferStrube.Blazor.MediaCaptureStreams;

public enum MediaStreamTrackKind
{
    Audio,
    Video
}

public static class MediaStreamTrackKindExtensions
{
    public static MediaStreamTrackKind ParseMediaStreamTrackKind(string value) => value switch
    {
        "audio" => MediaStreamTrackKind.Audio,
        "video" => MediaStreamTrackKind.Video,
        _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(MediaStreamTrackKind)}")
    };
}
