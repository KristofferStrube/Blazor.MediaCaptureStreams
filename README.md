[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.MediaCaptureStreams)](https://github.com/KristofferStrube/Blazor.MediaCaptureStreams/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.MediaCaptureStreams)](https://github.com/KristofferStrube/Blazor.MediaCaptureStreams/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.MediaCaptureStreams)](https://github.com/KristofferStrube/Blazor.MediaCaptureStreams/stargazers)
[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.MediaCaptureStreams?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.MediaCaptureStreams/)

# Blazor.MediaCaptureStreams
A Blazor wrapper for the [Media Capture and Streams](https://www.w3.org/TR/mediacapture-streams/) browser API.

The API standardizes ways to request access to local multimedia devices, such as microphones or video cameras. This also includes the MediaStream API, which provides the means to control where multimedia stream data is consumed, and provides some information and configuration options for the devices that produce the media. This project implements a wrapper around the API for Blazor so that we can easily and safely interact with the media streams of the browser.

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.MediaCaptureStreams/

On each page, you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status](https://kristofferstrube.github.io/Blazor.MediaCaptureStreams/Status) page you can see how much of the WebIDL specs this wrapper has covered.

# Getting Started
## Prerequisites
You need to install .NET 7.0 or newer to use the library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

## Installation
You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:
```bash
dotnet add package KristofferStrube.Blazor.MediaCaptureStreams
```

# Usage
The package can be used in Blazor WebAssembly and Blazor Server projects.
## Import
You need to reference the package in order to use it in your pages. This can be done in `_Import.razor` by adding the following.
```razor
@using KristofferStrube.Blazor.MediaCaptureStreams
```

## Add to service collection
The library has one service which is the `IMediaDevicesService` which can be used to access the `MediaDevices` of the current browser context. An easy way to make the service available on all your pages is by registering it in the `IServiceCollection` so that it can be dependency injected in the pages that need it. This is done in `Program.cs` by using our extension `AddMediaDevicesService()` before you build the host like we do in the following code block. If you use Blazor WASM you also need to invoke the `SetupErrorHandlingJSInterop()` extension on the `IServiceProvider` after building the host but before running like do we here:
```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Adding IMediaDevicesService to service collection.
builder.Services.AddMediaDevicesService();

var app = builder.Build();

// For Blazor WASM you need to call this to make Error Handling JS Interop.
await app.Services.SetupErrorHandlingJSInterop();

await app.RunAsync();
```

## Inject in page
Then the service can be injected into a page
```razor
@inject IMediaDevicesService MediaDevicesService;
```
and we can use it to get the `MediaDevices` object that is the primary access point for the API and open a `MediaStream` representing a microphone device like so:
```razor
@if (error is not null)
{
    <code>@error</code>
}
else if (deviceLabel is not null)
{
    <p>We opened the media device with label: <b>@deviceLabel</b></p>
}
else
{
    <button @onclick="Open">Open Microphone MediaStream</button>
}

@code {
    private string? deviceLabel;
    private string? error;

    private async Task Open()
    {
        try
        {
            MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();
            MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new MediaStreamConstraints() { Audio = true });
            MediaStreamTrack[] audioTracks = await mediaStream.GetAudioTracksAsync();
            deviceLabel = await audioTracks.First().GetLabelAsync();
        }
        catch (WebIDLException ex)
        {
            error = $"{ex.GetType().Name}: {ex.Message}";
        }
    }
}
```
We can read, process, and record the `MediaStream` and `MediaStreamTrack`s through other API's. Among these are the [Web Audio](https://www.w3.org/TR/webaudio/), [WebRTC](https://www.w3.org/TR/webrtc/), [MediaStream Image Capture](https://www.w3.org/TR/image-capture/), and [MediaStream Recording](https://www.w3.org/TR/mediastream-recording/) API's. We are currently working on making a Blazor wrapper for the Web Audio API in the [Blazor.WebAudio project](https://github.com/KristofferStrube/Blazor.WebAudio). If you want an example of outputting the content of a video stream track check out [the live video demo sample](https://kristofferstrube.github.io/Blazor.MediaCaptureStreams/Video)

But before using the `MediaStream` and `MediaStreamTrack`s in other API's we can further constrain the details of the streams. We can do this by parsing some `MediaTrackConstraints` to the `Audio` property of the `MediaStreamConstraints` in our previous example. But before we make constraints we can also check what the possible range of values is for our devices so that we don't try to overconstrain the media devices. In the following sample, we open a `MediaStream` representing some camera, then check the capabilities of its first track and apply some new constraints to it depending on the constraints' max values for the resolution.

```csharp
MediaDevices mediaDevices = await MediaDevicesService.GetMediaDevicesAsync();
MediaStream mediaStream = await mediaDevices.GetUserMediaAsync(new MediaStreamConstraints() { Video = true });

MediaStreamTrack[] videoTracks = await mediaStream.GetVideoTracksAsync();
MediaStreamTrack firstVideoTrack = videoTracks.First();

MediaTrackCapabilities capabilities = await firstVideoTrack.GetCapabilitiesAsync();
await firstVideoTrack.ApplyContraintsAsync(new MediaTrackConstraints()
    {
        Height = capabilities.Height?.Max,
        Width = capabilities.Width?.Max
    });
```

If we had not checked the capabilities of the track before setting its values we could potentially have set some values outside the valid range. If we did this we would get an `OverConstrainedException` that we could catch as we did with the more general `WebIDLException` in the previous sample.

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related repositories
This project uses the *Blazor.WebIDL* package to make error handling JSInterop and it uses the *Blazor.DOM* package to listen to events from the *EventTarget*'s in the package like `MediaDevices`, `MediaStream`, and `MediaStreamTrack`.
- https://github.com/KristofferStrube/Blazor.WebIDL
- https://github.com/KristofferStrube/Blazor.DOM

The project is used in the *Blazor.WebAudio* library to create `AudioNode`s representing microphones that can be used together with other audio sources in many different ways.
- https://github.com/KristofferStrube/Blazor.WebAudio


# Related articles
This repository was built with inspiration and help from the following series of articles:

- [Typed exceptions for JSInterop in Blazor](https://kristoffer-strube.dk/post/typed-exceptions-for-jsinterop-in-blazor/)
- [Wrapping Compression Streams in Blazor](https://kristoffer-strube.dk/post/wrapping-compression-streams-in-blazor/)
- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)

