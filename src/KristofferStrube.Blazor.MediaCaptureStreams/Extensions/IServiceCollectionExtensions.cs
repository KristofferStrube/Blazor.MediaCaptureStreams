using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Contains extension methods for adding services to a <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="IMediaDevicesService"/> to the service collection as a scoped service.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddMediaDevicesService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IMediaDevicesService, MediaDevicesService>();
    }
}
