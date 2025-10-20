using BlazorServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests.Infrastructure;

public class MediaDevicesEvaluationContext(IJSRuntime jSRuntime, IMediaDevicesService mediaDeviceService) : EvaluationContext
{
    public IJSRuntime JSRuntime => jSRuntime;

    public IMediaDevicesService MediaDevicesService => mediaDeviceService;

    public static MediaDevicesEvaluationContext Create(IServiceProvider provider)
    {
        IMediaDevicesService mediaDevicesService = provider.GetRequiredService<IMediaDevicesService>();
        IJSRuntime jSRuntime = provider.GetRequiredService<IJSRuntime>();

        return new MediaDevicesEvaluationContext(jSRuntime, mediaDevicesService);
    }
}
