using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace KristofferStrube.Blazor.MediaCaptureStreams.Extensions;

/// <summary>
/// Taken from the extension method used in https://github.com/KristofferStrube/Blazor.ServiceWorker
/// </summary>
/// <remarks>Adopted from <see href="https://stackoverflow.com/a/53299290">this StackOverflow answer</see>.</remarks>
internal static class MemoizerExtensions
{
    internal static ConditionalWeakTable<object, ConcurrentDictionary<string, object>> _weakCache = new();

    internal static Task<TResult> MemoizedTask<TResult>(
        this object context,
        Func<Task<TResult>> f,
        [CallerMemberName] string? cacheKey = null)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(cacheKey);

        ConcurrentDictionary<string, object> methodCaches = _weakCache.GetOrCreateValue(context);

        ConcurrentDictionary<string, Task<TResult>> methodCache = (ConcurrentDictionary<string, Task<TResult>>)methodCaches
            .GetOrAdd(cacheKey, _ => new ConcurrentDictionary<string, Task<TResult>>());
        return methodCache.GetOrAdd(cacheKey, _ => f());
    }
}