#!/bin/sh
# ref: https://help.github.com/articles/adding-an-existing-project-to-github-using-the-command-line/
#
# Usage example: /bin/sh ./git_push.sh wing328 openapi-petstore-perl "minor update" "gitlab.com"

git_user_id=$1
git_repo_id=$2
release_note=$3
git_host=$4

if [ "$git_host" = "" ]; then
    git_host="github.com"
    echo "[INFO] No command line input provided. Set \$git_host to $git_host"
fi

if [ "$git_user_id" = "" ]; then
    git_user_id="GIT_USER_ID"
    echo "[INFO] No command line input provided. Set \$git_user_id to $git_user_id"
fi

if [ "$git_repo_id" = "" ]; then
    git_repo_id="GIT_REPO_ID"
    echo "[INFO] No command line input provided. Set \$git_repo_id to $git_repo_id"
fi

if [ "$release_note" = "" ]; then
    release_note="Minor update"
    echo "[INFO] No command line input provided. Set \$release_note to $release_note"
fi

# Initialize the local directory as a Git repository
git init

# Adds the files in the local repository and stages them for commit.
git add .

# Commits the tracked changes and prepares them to be pushed to a remote repository.
git commit -m "$release_note"

# Sets the new remote
if ! git remote get-url origin >/dev/null 2>&1; then # 'origin' not defined

    echo "[INFO] Configuring origin without embedding credentials. Ensure a credential helper or gh auth is configured."
    git remote add origin "https://${git_host}/${git_user_id}/${git_repo_id}.git"

fi

# Determine current branch (or default to "main" if detached)
branch="${GIT_BRANCH:-$(git rev-parse --abbrev-ref HEAD 2>/dev/null || echo main)}"

echo "Git pushing to https://${git_host}/${git_user_id}/${git_repo_id}.git (branch: ${branch})"
# Push and set upstream for future pushes
git push -u origin "${branch}" 2>&1 | grep -v 'To https'
