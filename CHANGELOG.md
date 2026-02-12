# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.4.1] - 2026-02-13
### Fixed 
- Fixed that `MediaStreamTrack.GetCapabilitiesAsync` would break if a microphone supported the options `"all"` or `"remote-only"` for the `echoCancellation` capability as this wrapper currently doesn't support those values. For now we ignore those capabilty options, but we plan to add support for them in the next major version.

## [0.4.0] - 2024-10-23
### Changed
- Changed so that `MediaDevices` made from `MediaDevicesService` are marked as `DisposesJSReference`.
- Changed the version of `Blazor.DOM` to use the newest version which is `0.3.0`.

## [0.3.0] - 2024-08-25
### Added
- Added `IJSWrapperConverter` decoration to all `IJSWrapper` types so that they can be parsed directly in JSInterop.

## [0.2.0] - 2024-04-16
### Changed
- Changed the version of `Blazor.DOM` to use the newest version which is `0.2.1`.
- Changed `IMediaDevicesService.GetMediaDevicesAsync` method to not be memoized as returned wrappers should be disposed.
- Changed `MediaStreamTrackEvent.GetTrackAsync` method to not be memoized as returned wrappers should be disposed.

## [0.1.1] - 2024-04-15
### Fixed
- Fixed that `MediaDevices.GetUserMediaAsync` was not configured to use Error Handling JSInterop for Blazor Server.