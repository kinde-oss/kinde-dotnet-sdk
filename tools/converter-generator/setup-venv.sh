#!/bin/bash
# Setup Python virtual environment for converter generator

set -e

echo "Setting up Python virtual environment..."

# Create venv if it doesn't exist
if [ ! -d "venv" ]; then
    python3 -m venv venv
    echo "Created virtual environment"
fi

# Activate venv
source venv/bin/activate

# Upgrade pip
pip install --upgrade pip

# Install requirements
pip install -r requirements.txt

echo ""
echo "Virtual environment setup complete!"
echo "To activate: source venv/bin/activate"
echo "To run generator: python generate-converters-v2.py"

