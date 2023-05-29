using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.MediaCaptureStreams;

public class MediaTrackConstraints : MediaTrackConstraintSet
{
    [JsonPropertyName("advanced")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaTrackConstraintSet[]? Advanced { get; set; }

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
                var advancedHydrateObject = new MediaTrackConstraintSet();
                advanced.Add(await HydrateMediaTrackConstraintSet(advancedHydrateObject, jSRuntime, await array.AtAsync(i)));
            }
        }
        return await HydrateMediaTrackConstraintSet(hydrateObject, jSRuntime, jSReference);
    }
}
