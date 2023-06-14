using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

/// <summary>
/// Each member of a <see cref="MediaTrackConstraintSet"/> corresponds to a constrainable property and specifies a subset of the property's valid <see cref="MediaTrackCapabilities"/> values. Applying a <see cref="MediaTrackConstraintSet"/> instructs the User Agent to restrict the settings of the corresponding constrainable properties to the specified values or ranges of values. A given property may occur both in the basic <see cref="MediaTrackConstraints"/> set and in the <see cref="Advanced"/> array, and may occur at most once in each <see cref="MediaTrackConstraintSet"/> in the <see cref="Advanced"/> array.
/// </summary>
/// <remarks><see href="https://www.w3.org/TR/mediacapture-streams/#media-track-constraints">See the API definition here</see></remarks>
public class MediaTrackConstraints : MediaTrackConstraintSet
{
    /// <summary>
    /// This is the array of <see cref="MediaTrackConstraintSet"/> that the User Agent must attempt to satisfy, in order, skipping only those that cannot be satisfied. The order of these <see cref="MediaTrackConstraintSet"/>s is significant. In particular, when they are passed as an argument to <see cref="MediaStreamTrack.ApplyContraintsAsync(MediaTrackConstraints?)"/>, the User Agent must try to satisfy them in the order that is specified.
    /// </summary>
    /// <remarks>
    /// An example could be that advanced <see cref="MediaTrackConstraintSet"/>s C1 and C2 can be satisfied individually, but not together, then whichever of C1 and C2 is first in this array will be satisfied, and the other will not. The User Agent must attempt to satisfy all <see cref="MediaTrackConstraintSet"/>s in the array, even if some cannot be satisfied. Thus, in the preceding example, if constraint C3 is specified after C1 and C2, the User Agent will attempt to satisfy C3 even though C2 cannot be satisfied.
    /// </remarks>
    [JsonPropertyName("advanced")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaTrackConstraintSet[]? Advanced { get; set; }

    /// <summary>
    /// This method is used to hydrate a <see cref="MediaTrackConstraints"/> from a <see cref="IJSObjectReference"/>.
    /// </summary>
    /// <param name="hydrateObject">The object that will get its properties populated.</param>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">The reference to the JS object that the properties will be read from.</param>
    /// <returns></returns>
    public static async Task<MediaTrackConstraints> HydrateMediaTrackConstraints(MediaTrackConstraints hydrateObject, IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        ValueReference advancedReference = new ValueReference(jSRuntime, jSReference, "advanced");
        advancedReference.ValueMapper["array"] = async () => new TypedArray<IJSObjectReference>(jSRuntime, await advancedReference.GetValueAsync<IJSObjectReference>());
        if (await advancedReference.GetValueAsync() is TypedArray<IJSObjectReference> array)
        {
            long length = await array.GetLengthAsync();
            List<MediaTrackConstraintSet> advanced = new((int)length);
            for (long i = 0; i < length; i++)
            {
                MediaTrackConstraintSet advancedHydrateObject = new MediaTrackConstraintSet();
                advanced.Add(await HydrateMediaTrackConstraintSet(advancedHydrateObject, jSRuntime, await array.AtAsync(i)));
            }
        }
        return await HydrateMediaTrackConstraintSet(hydrateObject, jSRuntime, jSReference);
    }
}
