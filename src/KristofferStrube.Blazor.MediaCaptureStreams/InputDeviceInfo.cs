using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// A <see cref="InputDeviceInfo"/> interface gives access to the capabilities of the input device it represents.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#device-info">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class InputDeviceInfo : MediaDeviceInfo, IJSCreatable<InputDeviceInfo>
{
    /// <inheritdoc/>
    public static new async Task<InputDeviceInfo> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<InputDeviceInfo> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new InputDeviceInfo(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    public InputDeviceInfo(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Returns a <see cref="MediaTrackCapabilities"/> object describing the primary audio or video track of a device's <see cref="MediaStream"/> (according to its kind value), in the absence of any user-supplied constraints.
    /// If no access has been granted to any local devices and this <see cref="InputDeviceInfo"/> has been filtered with respect to unique identifying information, then the object that this returns has all-null values for its properties.
    /// </summary>
    public async Task<MediaTrackCapabilities> GetCapabilitiesAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackCapabilities>("getCapabilities");
    }
}
