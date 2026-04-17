# AGENTS.md

## Project Overview

TimeInWords is a QLOCKTWO-inspired word clock application that displays the current time as illuminated words on a letter grid. It runs as a Windows screensaver or cross-platform desktop app (including Raspberry Pi).

Supported time resolutions are 5-minute resolution and 1-minute resolution (precise).
Languages and resolution modes are listed in `src/TimeInWords/Resources/LanguagePreset.cs`.

## Build & Test

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal --logger trx --settings coverlet.runsettings
```

All three commands must pass cleanly before submitting changes. CI runs on Ubuntu with .NET 10.0.

## Architecture

**MVP (Model-View-Presenter)** pattern throughout the UI layer. Views handle rendering only; presenters contain logic; `TimeToTextLib` and `TextToTimeGridLib` are standalone libraries with no UI dependencies.

Key design decisions:
- Constructor injection for all dependencies. No DI container — manual wiring in `Program.cs`.
- Factory pattern for view creation.
- Strategy pattern for language support (`LanguagePreset`, `TimeGrid` with per-language subclasses in `Presets/` and `Grids/` folders).
- Interfaces for testability (e.g. `IDateTimeProvider`, `ITimer`).

## Code Conventions

Follow `.editorconfig` as the source of truth; it is comprehensive and enforced as warnings/errors.

## Testing

All new code must have corresponding tests. 
Presenter tests mock view interfaces; library tests cover algorithm correctness with comprehensive input scenarios. 
Match the style and structure of existing tests.

## Adding a New Language

Follow the existing pattern where a language usually has both 5-minute and precise variants.

1. Add enum values to `LanguagePreset.Language` (typically `<Language>` and `<Language>Precise`).
2. Create preset classes in `src/TimeToTextLib/Presets/` inheriting from `LanguagePreset` (typically both variants).
3. Create grid classes in `src/TextToTimeGridLib/Grids/` inheriting from `TimeGrid` (typically both variants).
4. Register all new variants in both static factories (`LanguagePreset.Get()`, `TimeGrid.Get()`).
5. Add tests for all variants in both `tests/TimeToTextLib.Tests/Presets/` and `tests/TextToTimeGridLib.Tests/Grids/`.
6. Keep names aligned (`<Language>` / `<Language>Precise`) so settings and app configuration stay consistent.

NOTE: when building the grids, ensure that:
- words on the same row that are displayed at the same time are separated by at least one filler character;
- filler characters are distributed across the row, not just on the right-hand side;
