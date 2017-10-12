@echo off
echo *** Proteus.Utility.Configuration
setlocal
setlocal enabledelayedexpansion
setlocal enableextensions
set errorlevel=0

mkdir ..\..\package-nuget


for /f %%v in ('powershell -noprofile "(Get-Command ..\..\build\Release\Proteus.Utility.Configuration.dll).FileVersionInfo.FileVersion"') do set version=%%v
..\..\tools\NuGet\NuGet.exe pack Proteus.Utility.Configuration.nuspec -properties version=%version% -OutputDirectory ..\..\package-nuget
echo *** Finished building Proteus.Utility.Configuration

