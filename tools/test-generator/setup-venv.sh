#!/bin/bash
# Setup script for test generator virtual environment

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo "Setting up virtual environment for test generator..."

# Create virtual environment if it doesn't exist
if [ ! -d "venv" ]; then
    python3 -m venv venv
    echo "✓ Virtual environment created"
else
    echo "✓ Virtual environment already exists"
fi

# Activate virtual environment
source venv/bin/activate

# Upgrade pip
pip install --upgrade pip --quiet

# Install requirements
echo "Installing dependencies..."
pip install -q -r requirements.txt

echo "✓ Test generator setup complete!"
echo ""
echo "To activate the virtual environment, run:"
echo "  source venv/bin/activate"

