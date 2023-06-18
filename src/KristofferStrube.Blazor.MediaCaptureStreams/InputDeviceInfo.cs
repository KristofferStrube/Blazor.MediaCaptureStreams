using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="InputDeviceInfo"/> interface gives access to the capabilities of the input device it represents.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#device-info">See the API definition here</see>.</remarks>
public class InputDeviceInfo : MediaDeviceInfo
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="InputDeviceInfo"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="InputDeviceInfo"/>.</param>
    /// <returns>A wrapper instance for a <see cref="InputDeviceInfo"/>.</returns>
    public static new Task<InputDeviceInfo> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new InputDeviceInfo(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="InputDeviceInfo"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="InputDeviceInfo"/>.</param>
    public InputDeviceInfo(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Returns a <see cref="MediaTrackCapabilities"/> object describing the primary audio or video track of a device's <see cref="MediaStream"/> (according to its kind value), in the absence of any user-supplied constraints.
    /// If no access has been granted to any local devices and this <see cref="InputDeviceInfo"/> has been filtered with respect to unique identifying information, then the object that this returns has all-null values for its properties.
    /// </summary>
    public async Task<MediaTrackCapabilities> GetCapabilitiesAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackCapabilities>("getCapabilities");
    }
}
