# AGENTS.md

## Project Overview

TimeInWords is a QLOCKTWO-inspired word clock application that displays the current time as illuminated words on a letter grid. It runs as a Windows screensaver or cross-platform desktop app (including Raspberry Pi).

Support resolutions:
- 5-minute resolution
- 1-minute resolution (precise)

Languages and resolution modes are listed in `src/TimeInWords/Resources/LanguagePreset.cs`. 

## Tech Stack

- **Language:** C# on .NET 10.0
- **UI Framework:** Avalonia 11.3.x (cross-platform desktop GUI)
- **Testing:** xUnit, NSubstitute (mocking), AwesomeAssertions (fluent assertions), Avalonia.Headless.XUnit
- **CI:** GitHub Actions (`.github/workflows/dotnetcore.yml`)

## Build & Test

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal --logger trx --settings coverlet.runsettings
```

All three commands must pass cleanly before submitting changes. CI runs on Ubuntu with .NET 10.0.

## Project Structure

```
src/
  TimeInWords/          # Main screensaver/desktop app (Avalonia, WinExe)
  TimeToTextLib/        # Core library: converts DateTime to word representation
  TextToTimeGridLib/    # Core library: maps word text to LED bitmask grid
  DebugApp/             # Avalonia debug harness
  ConsoleDebugApp/      # Console debug utility
tests/
  TimeInWords.Tests/    # Presenter, view, and control tests
  TimeToTextLib.Tests/  # Time-to-text algorithm tests
  TextToTimeGridLib.Tests/  # Grid and bitmask tests
```

## Architecture

**MVP (Model-View-Presenter)** pattern throughout the UI layer:

- **Views** implement interfaces (`ITimeInWordsView`, `IMainView`, `ISettingsEditorView`) and handle rendering only.
- **Presenters** (`TimeInWordsPresenter`, `MainPresenter`, `SettingsEditorPresenter`) contain all logic and coordinate between views and models.
- **Models/Libraries** (`TimeToTextLib`, `TextToTimeGridLib`) are standalone, reusable libraries with no UI dependencies.

Key design decisions:
- Constructor injection for all dependencies. No DI container — manual wiring in `Program.cs`.
- Factory pattern for view creation (`IMainViewFactory`).
- Strategy pattern for language support (`LanguagePreset`, `TimeGrid` with per-language subclasses in `Presets/` and `Grids/` folders).
- Interfaces for testability: `IDateTimeProvider`, `ITimer`.

## Code Conventions

Follow `.editorconfig` as the source of truth; it is comprehensive and enforced as warnings/errors.

High-signal defaults to keep in mind:
- File-scoped namespaces are required.
- Use `var` consistently.
- Private fields use `_camelCase`; interfaces use `I` prefix; type parameters use `T` prefix.
- Keep braces on all control blocks.

## Testing Conventions

- **Test class naming:** `{ClassName}Should.cs` (e.g., `TimeInWordsPresenterShould.cs`).
- **Test method naming:** Descriptive PascalCase phrases (e.g., `InitialiseTheView`, `ConfigureTimerCorrectly`).
- **Test structure mirrors source:** `tests/TimeInWords.Tests/Presenters/` mirrors `src/TimeInWords/Presenters/`.
- **Assertions:** Use AwesomeAssertions fluent syntax (`.Should().Be()`, `.Should().NotBeNull()`).
- **Mocking:** Use NSubstitute (`Substitute.For<T>()`, `.Returns()`, `.Received()`).
- **Parameterized tests:** Use `[Theory]` with `[InlineData]` or `[ClassData]` for data-driven tests.
- **Avalonia UI tests:** Use `Avalonia.Headless.XUnit` for headless view testing.
- **Global usings:** Test projects use `GlobalUsings.cs` for common imports.

All new code must have corresponding tests. Presenter tests mock view interfaces; library tests cover algorithm correctness with comprehensive input scenarios.

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
