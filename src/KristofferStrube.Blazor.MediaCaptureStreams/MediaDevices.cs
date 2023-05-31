using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.MediaCaptureStreams.Extensions;
using KristofferStrube.Blazor.MediaCaptureStreams.Exceptions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaDevices : EventTarget
{
    private readonly Lazy<Task<IJSObjectReference>> mediaCaptureStreamsHelperTask;
    private readonly ErrorHandlingJSObjectReference? errorHandlingJSReference;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="MediaDevices"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="MediaDevices"/>.</param>
    /// <returns>A wrapper instance for a <see cref="MediaDevices"/>.</returns>
    public static Task<MediaDevices> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new MediaDevices(jSRuntime, jSReference));
    }

    protected MediaDevices(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        mediaCaptureStreamsHelperTask = new(jSRuntime.GetHelperAsync);
        if (ErrorHandlingJSInterop.ErrorHandlingJSInteropHasBeenSetup)
        {
            errorHandlingJSReference = new ErrorHandlingJSObjectReference(jSReference);
        }
    }

    public async Task<MediaDeviceInfo[]> EnumerateDevicesAsync()
    {
        IJSObjectReference helper = await mediaCaptureStreamsHelperTask.Value;
        IJSObjectReference devices = await JSReference.InvokeAsync<IJSObjectReference>("enumerateDevices");
        int length = await helper.InvokeAsync<int>("getAttribute", devices, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                    {
                        var reference = new ValueReference(JSRuntime, devices, i);
                        reference.ValueMapper["mediadeviceinfo"] = async () => await MediaDeviceInfo.CreateAsync(JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
                        reference.ValueMapper["inputdeviceinfo"] = async () => await InputDeviceInfo.CreateAsync(JSRuntime, await reference.GetValueAsync<IJSObjectReference>());
                        return (MediaDeviceInfo)(await reference.GetValueAsync())!;
                    })
                .ToArray()
        );
    }

    public async Task<MediaTrackSupportedConstraints> GetSupportedConstraintsAsync()
    {
        return await JSReference.InvokeAsync<MediaTrackSupportedConstraints>("getSupportedConstraints");
    }

    /// <summary>
    /// Prompts the user for permission to use their Web cam or other video or audio input.<br />
    /// </summary>
    /// <remarks>
    /// If neither <see cref="MediaStreamConstraints.Audio"/> nor <see cref="MediaStreamConstraints.Video"/> has been set on the <paramref name="constraints"/> then it throws a <see cref="TypeErrorException"/>.<br />
    /// If the document is not fully active then it throws a <see cref="InvalidStateErrorException"/>.<br />
    /// If the <paramref name="constraints"/> requested audio devices but the browser does not allow to use that feature then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If the <paramref name="constraints"/> requested video devices but the browser does not allow to use that feature then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If it is not possible to find an initial set of candidate devices for one of the requested <c>kind</c>s of devices then it throws a <see cref="NotFoundErrorException"/>.<br />
    /// If one of the constraints is malformed then it throws a <see cref="TypeErrorException"/>.<br />
    /// If one of the constraints is too restrictive then it throws a <see cref="OverconstrainedErrorException"/>; If any device of the relevant <c>kind</c> is already open then the <see cref="OverconstrainedErrorException.Constraint"/> property is set to the name of the overrestrictive constraint.<br />
    /// If the user has not given permissions to access the specific <c>kind</c> of device then it throws a <see cref="NotAllowedErrorException"/>.<br />
    /// If a hardware error such as an OS/program/webpage lock prevents access and a given candidate device is the last of its <c>kind</c> then it throws a <see cref="NotReadableErrorException"/>.<br />
    /// If any other error happen while trying to access a device and the given candidate device is the last of its <c>kind</c> it throws a <see cref="AbortErrorException"/>.
    /// </remarks>
    /// <param name="constraints">The constraints that define the requested <c>kind</c>s of devices should follow.</param>
    /// <returns>A suitable <see cref="MediaStream"/> that follows the <paramref name="constraints"/>.</returns>
    /// <exception cref="InvalidStateErrorException" />
    /// <exception cref="NotAllowedErrorException" />
    /// <exception cref="NotFoundErrorException" />
    /// <exception cref="TypeErrorException" />
    /// <exception cref="OverconstrainedErrorException" />
    /// <exception cref="NotAllowedErrorException" />
    /// <exception cref="NotReadableErrorException" />
    /// <exception cref="AbortErrorException" />
    public async Task<MediaStream> GetUserMediaAsync(MediaStreamConstraints constraints)
    {
        var jSReference = errorHandlingJSReference ?? JSReference;
        IJSObjectReference jSInstance = await jSReference.InvokeAsync<IJSObjectReference>("getUserMedia", constraints);
        if (jSInstance is IErrorHandlingJSInProcessObjectReference { } errorHandling)
        {
            jSInstance = errorHandling.JSReference;
        }
        return await MediaStream.CreateAsync(JSRuntime, jSInstance);
    }
}
