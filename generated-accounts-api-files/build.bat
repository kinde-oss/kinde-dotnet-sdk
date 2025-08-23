@echo off
setlocal enabledelayedexpansion

echo Building Kinde.Accounts project...

REM Navigate to the src directory
cd src

REM Build the project
dotnet build Kinde.Accounts\Kinde.Accounts.csproj --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo Build completed successfully!
    echo The DLL can be found in: src\Kinde.Accounts\bin\Release\net9.0\Kinde.Accounts.dll
) else (
    echo Build failed with error code %ERRORLEVEL%
    exit /b %ERRORLEVEL%
)
