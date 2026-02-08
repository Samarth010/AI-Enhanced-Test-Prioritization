import subprocess
import os

class GitAnalyzer:
    def __init__(self, repo_root):
        self.repo_root = repo_root

    def get_changed_files(self):
        """
        Returns a list of changed files in the last commit.
        Uses: git diff --name-only HEAD~1 HEAD
        """
        result = subprocess.run(
            ["git", "diff", "--name-only", "HEAD~1", "HEAD"],
            cwd=self.repo_root,
            capture_output=True,
            text=True
        )

        if result.returncode != 0:
            print("Git command failed. Are you inside a Git repo?")
            return []

        return result.stdout.splitlines()

    def extract_changed_pages(self, changed_files):
        """
        Maps changed .cs files inside Pages/ to PageObject names.
        Example: Pages/LoginPage.cs → LoginPage
        """
        pages = []

        for file in changed_files:
            if "Pages" in file and file.endswith(".cs"):
                filename = os.path.basename(file)
                page_name = filename.replace(".cs", "")
                pages.append(page_name)

        return pages