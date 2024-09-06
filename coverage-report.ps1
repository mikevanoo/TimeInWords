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

# collect code coverage
dotnet test --collect "XPlat Code Coverage" --settings coverlet.runsettings

# generate report
dotnet reportgenerator -reports:"$testsFolder/**/coverage.cobertura.xml" -targetdir:$reportFolder -reporttypes:Html

# open report
Start-Process "$reportFolder/index.html"
