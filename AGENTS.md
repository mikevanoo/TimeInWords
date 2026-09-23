# AGENTS.md

## Project Overview

TimeInWords is a QLOCKTWO-inspired word clock application that displays the current time as illuminated words on a letter grid. It runs as a Windows screensaver or cross-platform desktop app (including Raspberry Pi).

Supported time resolutions are 5-minute resolution and 1-minute resolution (precise).
Languages and resolution modes are listed in `src/TimeInWords/Resources/LanguagePreset.cs`.

## Build & Test

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal
```

All three commands must pass cleanly before submitting changes. CI runs on Ubuntu with .NET 10.0.

Tests run on Microsoft Testing Platform (`global.json` sets the runner), which rejects the VSTest options
`--settings` and `--logger`; passing either makes the run report "Zero tests ran". Coverage is collected
out-of-band, the way CI does it:

```bash
dotnet dotnet-coverage collect "dotnet test --no-build" -f cobertura -o tests/coverage.xml --settings coverage.runsettings
```

## Mutation Testing

Stryker.NET is pinned in the tool manifest. It reads `stryker-config.json` from the working directory,
so the config belongs in the unit test project folder and is run from there (only
`tests/TextToTimeGridLib.Tests` has one so far):

```bash
cd tests/TextToTimeGridLib.Tests
dotnet stryker
```

The config sets `"test-runner": "mtp"`, which is required — the default VSTest runner cannot see
Microsoft Testing Platform tests and reports "Zero tests ran". Surviving mutants are usually a missing
assertion rather than dead code; check whether the behaviour is worth pinning before adding a test for it,
and prefer `ignore-methods` over a test that cannot fail.

A run leaves a **mutated copy of the library DLL** in the test project's `bin`. The next
`dotnet test --no-build` then throws `TypeLoadException`, reports "Zero tests ran" for that assembly and
drops its tests from the totals (375 becomes 319) while the other assemblies still say "passed".
Always rebuild afterwards:

```bash
dotnet build --no-restore
```

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
- only use "safe" filler characters, i.e. those that are not used to spell out the required words on the grid;
- aim for roughly even distribution of filler characters and avoid placing the same character in adjacent cells;
