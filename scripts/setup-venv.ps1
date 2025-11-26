# PowerShell script to setup Python virtual environment for post-processing scripts

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$VenvDir = Join-Path $ScriptDir "venv"

# Check if Python 3 is available
try {
    $pythonVersion = python --version 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "Python not found"
    }
} catch {
    Write-Host "Error: Python 3 is required but not found. Please install Python 3." -ForegroundColor Red
    exit 1
}

# Create virtual environment if it doesn't exist
if (-not (Test-Path $VenvDir)) {
    Write-Host "Creating Python virtual environment..."
    python -m venv $VenvDir
}

# Activate virtual environment and install dependencies
Write-Host "Installing dependencies..."
& "$VenvDir\Scripts\Activate.ps1"
pip install --quiet --upgrade pip
pip install --quiet -r "$ScriptDir\requirements.txt"

Write-Host "Virtual environment setup complete." -ForegroundColor Green
Write-Host "To activate: .\venv\Scripts\Activate.ps1"

