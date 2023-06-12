using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

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

    public InputDeviceInfo(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<MediaTrackCapabilities> GetCapabilitiesAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackCapabilities>("getCapabilities");
    }
}
