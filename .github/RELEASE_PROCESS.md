# Release Process for Kinde .NET SDK

This document describes the automated release process for the Kinde .NET SDK using GitHub Actions.

## Overview

The release process is fully automated using GitHub Actions workflows. When you create a version tag, the system will:

1. Build and test the project
2. Create a NuGet package
3. Create a GitHub release
4. Publish the package to NuGet.org

## Workflows

### 1. Build and Test (`build-and-test.yml`)
- **Trigger**: Push to `main`/`develop` branches or pull requests
- **Purpose**: Ensures code quality by building and testing across multiple .NET versions
- **Runs on**: Ubuntu with .NET 6.0, 7.0, and 8.0

### 2. Version Bump (`version-bump.yml`)
- **Trigger**: Push to `main` branch (excluding markdown files)
- **Purpose**: Automatically increments the patch version when code changes
- **Note**: Add `[skip ci]` to commit messages to skip this workflow

### 3. Create Release (`create-release.yml`)
- **Trigger**: Manual workflow dispatch
- **Purpose**: Creates GitHub releases with NuGet packages attached
- **Access**: Restricted to authorized users

### 4. Publish to NuGet (`publish-nuget.yml`)
- **Trigger**: Manual workflow dispatch
- **Purpose**: Publishes packages to NuGet.org
- **Access**: Restricted to authorized users

### 5. Manual Release and Publish (`manual-release.yml`)
- **Trigger**: Manual workflow dispatch
- **Purpose**: Combined workflow for creating releases and publishing to NuGet
- **Access**: Restricted to authorized users

## Creating a Release

### Option 1: Using GitHub Actions (Recommended)

1. Go to the GitHub repository
2. Navigate to the "Actions" tab
3. Select "Manual Release and Publish" workflow
4. Click "Run workflow"
5. Fill in the required information:
   - **Version**: The version to release (e.g., 1.3.2)
   - **Release notes**: Optional release notes
   - **Publish to NuGet.org**: Check this to publish to NuGet
6. Click "Run workflow"

This workflow will:
- Build and test the project
- Update the version in `Kinde.Api.csproj`
- Create a NuGet package
- Create a git tag and push it
- Create a GitHub release
- Optionally publish to NuGet.org

### Option 2: Using the Release Script

1. Navigate to the project root:
   ```bash
   cd kinde-dotnet-sdk
   ```

2. Run the release script:
   ```bash
   ./scripts/create-release.sh 1.3.2
   ```

   This script will:
   - Update the version in `Kinde.Api.csproj`
   - Build and test the project
   - Create a NuGet package
   - Commit the version change
   - Create and push a git tag

### Option 3: Manual Process

1. Update the version in `Kinde.Api/Kinde.Api.csproj`:
   ```xml
   <Version>1.3.2</Version>
   ```

2. Build and test:
   ```bash
   dotnet build --configuration Release
   dotnet test --configuration Release
   ```

3. Create and push a tag:
   ```bash
   git add Kinde.Api/Kinde.Api.csproj
   git commit -m "Bump version to 1.3.2"
   git tag -a v1.3.2 -m "Release version 1.3.2"
   git push origin main
   git push origin v1.3.2
   ```

## Required Secrets

To enable NuGet publishing, add the following secrets to your GitHub repository:

1. Go to your repository settings
2. Navigate to "Secrets and variables" → "Actions"
3. Add the following secret:
   - `NUGET_API_KEY`: Your NuGet API key for publishing packages

## User Access Control

The manual release workflows are restricted to authorized users. To modify the access restrictions:

1. Edit the workflow files in `.github/workflows/`
2. Update the `if` condition in the job section
3. Add or remove usernames/organizations as needed

Example:
```yaml
if: |
  github.actor == 'kinde-oss' ||
  github.actor == 'your-username' ||
  contains(github.actor, 'kinde')
```

**Note**: Replace `'your-username'` with actual GitHub usernames who should have access to run these workflows.

### Getting a NuGet API Key

1. Go to [NuGet.org](https://www.nuget.org)
2. Sign in to your account
3. Go to "API Keys" in your account settings
4. Create a new API key with "Push" permissions
5. Copy the key and add it to your GitHub secrets

## Versioning Strategy

The project follows semantic versioning (SemVer):

- **Major version** (1.x.x): Breaking changes
- **Minor version** (x.2.x): New features, backward compatible
- **Patch version** (x.x.3): Bug fixes, backward compatible

The automated version bump workflow increments the patch version automatically. For major or minor version changes, manually update the version in the csproj file.

## Troubleshooting

### Workflow Failures

1. Check the GitHub Actions tab for detailed error messages
2. Ensure all required secrets are configured
3. Verify the NuGet API key has the correct permissions

### Package Publishing Issues

1. Check if the package already exists on NuGet.org
2. Verify the version number is unique
3. Ensure the package meets NuGet.org requirements

### Skipping Automated Version Bump

Add `[skip ci]` to your commit message to skip the automated version bump:

```bash
git commit -m "Update documentation [skip ci]"
```

## Monitoring

- **GitHub Actions**: Monitor workflow runs in the Actions tab
- **NuGet.org**: Check package status at https://www.nuget.org/packages/Kinde.SDK
- **GitHub Releases**: View releases at https://github.com/kinde-oss/kinde-dotnet-sdk/releases

## Support

If you encounter issues with the release process:

1. Check the GitHub Actions logs for detailed error messages
2. Verify all secrets are properly configured
3. Ensure you have the necessary permissions for the repository
