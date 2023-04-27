. .\build-functions.ps1

$verbosity = "normal"
Write-Host "Cleaning Build Space"

$buildConfig = "Release"
Clean

$buildConfig = "Debug"
Clean
