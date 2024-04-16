namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A service for accessing the global object's associated <see cref="MediaDevices"/>.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#navigator-interface-extensions">See the API definition here</see>.</remarks>
public interface IMediaDevicesService
{
    /// <summary>
    /// Return the global object's associated <see cref="MediaDevices"/>.
    /// </summary>
    Task<MediaDevices> GetMediaDevicesAsync();
}