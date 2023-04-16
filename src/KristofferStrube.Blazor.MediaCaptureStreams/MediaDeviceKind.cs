namespace KristofferStrube.Blazor.MediaCaptureStreams;

public enum MediaDeviceKind
{
    AudioInput,
    AudioOutput,
    VideoInput
}

public static class MediaDeviceKindExtensions
{
    public static MediaDeviceKind ParseMediaDeviceKind(string value) => value switch
    {
        "audioinput" => MediaDeviceKind.AudioInput,
        "audiooutput" => MediaDeviceKind.AudioOutput,
        "videoinput" => MediaDeviceKind.VideoInput,
        _ => throw new ArgumentException($"Value '{value}' was not a valid {nameof(MediaDeviceKind)}")
    };
}
