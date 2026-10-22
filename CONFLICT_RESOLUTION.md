# Resolving merge conflicts on fix/connections-deserialization

When GitHub says "This branch has conflicts", main has changed files we didn't touch.
Our PR only added 3 files; the conflicts are in **generated**/script files. Resolve by
taking **main's version** for those files.

Run these in the repo root:

```bash
# 1. Ensure you're on the PR branch and have latest from GitHub
git fetch origin
git checkout fix/connections-deserialization
git pull origin fix/connections-deserialization   # optional: get latest PR branch

# 2. Merge main into this branch (this will create the conflicts)
git merge origin/main

# 3. When conflict markers appear, take main's version for the conflicting files
git checkout --theirs Kinde.Api.Test/Integration/Api/Generated/OrganizationsApiIntegrationTests.cs
git checkout --theirs Kinde.Api.Test/Integration/Api/Generated/RolesApiIntegrationTests.cs
git checkout --theirs scripts/post-process-generated-code.py
git add Kinde.Api.Test/Integration/Api/Generated/OrganizationsApiIntegrationTests.cs
git add Kinde.Api.Test/Integration/Api/Generated/RolesApiIntegrationTests.cs
git add scripts/post-process-generated-code.py

# 4. Finish the merge
git status   # ensure no other files are still conflicted
git commit --no-edit

# 5. Push the updated branch (resolved merge)
git push origin fix/connections-deserialization
```

**Why `--theirs`:** When merging `origin/main` *into* `fix/connections-deserialization`,
"theirs" is main. We did not change those three files, so keeping main's version is correct.

If you prefer to resolve manually, open each conflicted file, remove the `<<<<<<<`,
`=======`, `>>>>>>>` markers and keep the main branch version of the conflicting sections.
