using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Extensions;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/KristofferStrube.Blazor.MediaCaptureStreams/KristofferStrube.Blazor.MediaCaptureStreams.js");
    }
}
