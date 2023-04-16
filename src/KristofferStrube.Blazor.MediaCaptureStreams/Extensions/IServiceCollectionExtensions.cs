using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMediaDevicesService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IMediaDevicesService, MediaDevicesService>();
    }
}
