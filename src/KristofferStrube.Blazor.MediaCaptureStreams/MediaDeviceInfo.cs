using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaDeviceInfo : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDeviceInfo"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDeviceInfo"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaDeviceInfo"/>.</returns>
    public static Task<MediaDeviceInfo> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaDeviceInfo(jSRuntime, jSReference));
    }

    public MediaDeviceInfo(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<string> GetDeviceIdAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "deviceId");
    }

    public async Task<MediaDeviceKind> GetKindAsync()
    {
        var helper = await helperTask.Value;
        var kind = await helper.InvokeAsync<string>("getAttribute", JSReference, "label");
        return MediaDeviceKindExtensions.ParseMediaDeviceKind(kind);
    }

    public async Task<string> GetLabelAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "label");
    }

    public async Task<string> GetGroupIdAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "groupId");
    }
}