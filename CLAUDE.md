# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository overview

`dnse` is a personal grab-bag of older .NET Framework (v3.5–v4.5.1) projects: a general-purpose class library, a handful of small command-line tools, standalone "concept" demos, and a third-party collections library. There is no solution file tying everything together — each project/subproject has its own `.sln` and `.csproj`, built with classic MSBuild (`ToolsVersion="12.0"`, packages via `packages.config` + NuGet, not `PackageReference`).

Top-level layout:
- `Library/` — the core reusable library, assembly name `VS.Library`, root namespace `VS.Library`. This is the main product of the repo.
- `UnitTests/Library/` — NUnit 3 tests for `Library`, project `VS.Library.UT`, solution `VS.Library.UT.sln`. References `Library.csproj` directly (project reference, not a package).
- `CommandLine/` — small standalone CLI utilities (`Common`, `TextTransCoder`, `WebLog`, `LibraryDemo`), each with its own `.sln`/`.csproj`.
- `Concepts/` — standalone example/demo projects and loose `.cs` files exploring specific techniques (immutability, LRU cache, large object management, data-contract adapters, LINQ precursors, etc.). Treat each subfolder as an independent, self-contained mini-project.
- `Test/Log4NetTest/` — a separate demo solution exercising log4net (Console and WPF hosts).
- `3rdParty/PowerCollections/` — vendored third-party library (Wintellect Power Collections), not maintained here.
- `ReferenceAssemblies/` — a locally vendored NUnit 2.x reference assembly (older/legacy; not used by `UnitTests/Library`, which pulls NUnit 3.2.1 via NuGet).

There is no README with project-specific instructions and no CI config in the repo.

## Build and test

This is a Windows/.NET Framework codebase (MSBuild, `packages.config`), so builds and tests are normally run from Visual Studio or MSBuild on Windows. If building from a shell:

```
# Restore NuGet packages for a project, then build with MSBuild, e.g.:
nuget restore UnitTests/Library/VS.Library.UT.sln
msbuild UnitTests/Library/VS.Library.UT.sln /p:Configuration=Debug
```

Running tests (NUnit 3.2.1, referenced via `packages.config` in `UnitTests/Library`):

```
# After building, run the compiled test assembly with the NUnit3 console runner:
nunit3-console UnitTests\Library\bin\Debug\VS.Library.UT.dll

# To run a single test/fixture, use NUnit's --test filter:
nunit3-console UnitTests\Library\bin\Debug\VS.Library.UT.dll --test=VS.Library.UT.<Namespace>.<Fixture>.<TestMethod>
```

`Project1.nunit` at the repo root is a legacy NUnit *project* file pointing at `UnitTests\Library\bin\Release\VS.Library.UT.dll` — it's a convenience config for the (old) NUnit GUI runner, not part of the build.

Each `Concepts/*` and `CommandLine/*` subproject is independently buildable via its own `.sln`; there's no top-level solution or build script that builds everything at once.

## Test project structure

`UnitTests/Library` mirrors `Library`'s folder/namespace structure (e.g. `Library/Collections` → `UnitTests/Library/Collections`, `Library/Pattern/Enumerable` → `UnitTests/Library/Pattern/Enumerable`). When adding a feature to `Library`, put its tests in the corresponding mirrored path under `UnitTests/Library`, using the same relative namespace under `VS.Library.UT`.

Test dependencies (from `packages.config`): NUnit 3.2.1, FluentAssertions 4.5.0, NSubstitute 1.10.0.0, AutoFixture 3.45.1. Prefer FluentAssertions-style assertions and NSubstitute for mocking to stay consistent with existing tests, and AutoFixture for generating test data where the existing tests do.

## Code style

- Root namespace `VS.Library`, sub-namespaced to mirror folder structure (`VS.Library.Collections`, `VS.Library.Pattern.Lifetime`, `VS.Library.Operation.Comparison`, etc.).
- Indentation uses tabs (see `Library/Common/Delegates.cs` and others).
- ReSharper/Rider settings in `dnse.DotSettings` (repo-wide) and `UnitTests/Library/VS.Library.UT.sln.DotSettings` set `var` usage to "use var when evident" for built-in, simple, and other types — follow this when introducing local variables.
- `Library/GlobalSuppressions.cs` carries legacy Code Analysis (FxCop) suppressions tied to `Library.FxCop`/`AllRules.ruleset`; the `Release` build config has `RunCodeAnalysis=true`.
