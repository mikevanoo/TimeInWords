# AGENTS.md

## Project Overview

TimeInWords is a QLOCKTWO-inspired word clock application that displays the current time as illuminated words on a letter grid. It runs as a Windows screensaver or cross-platform desktop app (including Raspberry Pi). Supports English, Dutch, and French.

## Tech Stack

- **Language:** C# on .NET 10.0
- **UI Framework:** Avalonia 11.3.x (cross-platform desktop GUI)
- **Testing:** xUnit, NSubstitute (mocking), AwesomeAssertions (fluent assertions), Avalonia.Headless.XUnit
- **Package Management:** Central package versioning via `Directory.Packages.props`
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

Follow the `.editorconfig` — it is comprehensive and enforced as warnings/errors. Key rules:

- **Namespaces:** File-scoped (`namespace Foo;`), required as error severity.
- **Naming:** PascalCase for public members, `_camelCase` for private fields, `I` prefix for interfaces, `T` prefix for type parameters.
- **`var`:** Use `var` everywhere (built-in types, apparent types, and elsewhere).
- **Expression-bodied members:** Preferred for methods, properties, accessors, lambdas. Not for constructors.
- **Braces:** Always required (`csharp_prefer_braces = true`).
- **Pattern matching:** Preferred over `is`/`as` with cast/null checks.
- **Null handling:** Use `?.`, `??`, and `is null` (not `ReferenceEquals`).
- **Formatting:** 4-space indentation, newline before all open braces, max 120 character line length.
- **Usings:** `System` usings sorted first, placed outside namespace.

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

1. Add a new `LanguagePreset.Language` enum value.
2. Create a preset class in `src/TimeToTextLib/Presets/` inheriting from `LanguagePreset`.
3. Create a grid class in `src/TextToTimeGridLib/Grids/` inheriting from `TimeGrid`.
4. Register both in the static factory methods (`LanguagePreset.Get()`, `TimeGrid.Get()`).
5. Add tests in both `tests/TimeToTextLib.Tests/Presets/` and `tests/TextToTimeGridLib.Tests/Grids/`.
