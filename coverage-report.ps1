#!/usr/bin/env -S pwsh -noprofile

$testsFolder = "tests"
$testResultsFolder = "TestResults"
$reportFolder = "coveragereport"

# clean test results folder from each test project
Get-ChildItem $testsFolder -Directory -Include @($testResultsFolder) -Recurse -Force |
    Sort-Object FullName -Descending |
    Remove-Item -Recurse -Force

# clean report output folder
Remove-Item -Path $reportFolder -Recurse -Force -ErrorAction SilentlyContinue

# build first so restore+build aren't instrumented by the coverage profiler
dotnet build

# collect code coverage (--no-build avoids re-running restore/build under the profiler)
dotnet dotnet-coverage collect "dotnet test --no-build" -f cobertura -o "$testsFolder/coverage.xml" --settings coverage.runsettings

# generate report
dotnet reportgenerator -reports:"$testsFolder/**/coverage.xml" -targetdir:$reportFolder -reporttypes:Html

# open report
Start-Process "$reportFolder/index.html"
