#!/bin/sh
# ref: https://help.github.com/articles/adding-an-existing-project-to-github-using-the-command-line/
#
# Usage example: /bin/sh ./git_push.sh wing328 openapi-petstore-perl "minor update" "gitlab.com"

git_user_id=${1-}
git_repo_id=${2-}
release_note=${3-}
git_host=${4-}
git_branch=${5:-main}

if [ -z "${git_host}" ]; then git_host="github.com"; fi

if [ -z "${git_user_id}" ] || [ -z "${git_repo_id}" ]; then
    echo "[ERROR] Missing required arguments: git_user_id and git_repo_id."
    echo "usage: ./git_push.sh <git_user_id> <git_repo_id> [release_note] [git_host] [branch]"
    exit 2
fi

if [ -z "${release_note}" ]; then release_note="Minor update"; fi

# Initialize the local directory as a Git repository
git init

# Adds the files in the local repository and stages them for commit.
git add .

# Commits the tracked changes and prepares them to be pushed to a remote repository.
git commit -m "$release_note"

# Sets the new remote if missing
if ! git remote get-url origin >/dev/null 2>&1; then
  git remote add origin "https://${git_host}/${git_user_id}/${git_repo_id}.git"
fi

# Prepare a transient askpass helper if GIT_TOKEN is set
askpass=""
if [ "${GIT_TOKEN-}" ]; then
  askpass="$(mktemp)"
  # This helper answers username/password prompts without echoing secrets into logs
  cat >"${askpass}" <<'EOF'
#!/bin/sh
case "$1" in
  *Username*) echo "x-access-token" ;;
  *Password*) echo "${GIT_TOKEN}" ;;
  *) echo "" ;;
esac
EOF
  chmod 700 "${askpass}"
  trap 'rm -f "${askpass}"' EXIT
fi

# Use askpass helper if available
if [ "${askpass}" ]; then
  export GIT_ASKPASS="${askpass}"
fi

git pull origin ${git_branch}

# Push changes to the configured branch
echo "[INFO] Pushing to https://${git_host}/${git_user_id}/${git_repo_id}.git (${git_branch})"
if [ -n "${askpass}" ]; then
  GIT_ASKPASS="${askpass}" GIT_TERMINAL_PROMPT=0 git push -u origin "${git_branch}"
else
  git push -u origin "${git_branch}"
fi
