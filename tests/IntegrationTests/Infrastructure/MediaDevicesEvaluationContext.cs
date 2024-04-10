using BlazorServer;

namespace KristofferStrube.Blazor.MediaCaptureStreams.IntegrationTests.Infrastructure;

public class MediaDevicesEvaluationContext(IMediaDevicesService mediaDeviceService) : EvaluationContext
{
    public IMediaDevicesService MediaDevicesService => mediaDeviceService;
}
