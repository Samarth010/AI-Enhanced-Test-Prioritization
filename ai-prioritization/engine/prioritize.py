import os
import argparse
from prioritizer import TestPrioritizer
from git_analyzer import GitAnalyzer

# Compute absolute paths to the data folder
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
DATA_DIR = os.path.join(SCRIPT_DIR, "..", "data")
REPO_ROOT = os.path.abspath(os.path.join(SCRIPT_DIR, "..", ".."))
BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

HISTORY_PATH = os.path.join(BASE_DIR, "data", "test_history.json")
IMPACT_MAP_PATH = os.path.join(BASE_DIR, "data", "test_impact_map.json")

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--changed-pages", nargs="+", help="List of changed pages (e.g. LoginPage DashboardPage)")
    parser.add_argument("--auto", action="store_true", help="Automatically detect changed pages from Git")
    args = parser.parse_args()

    # Require one mode
    if not args.auto and not args.changed_pages:
        parser.error("You must specify either --changed-pages or --auto")

    # Always create prioritizer
    prioritizer = TestPrioritizer(HISTORY_PATH, IMPACT_MAP_PATH)

    # Determine changed pages
    if args.auto:
        analyzer = GitAnalyzer(REPO_ROOT)
        changed_files = analyzer.get_changed_files()
        changed_pages = analyzer.extract_changed_pages(changed_files)
        print("Detected changed pages:", changed_pages)
    else:
        changed_pages = args.changed_pages

    # Now run prioritization
    results = prioritizer.prioritize(changed_pages)

    print("\n=== PRIORITIZED TESTS ===")
    for r in results:
        print(r)

if __name__ == "__main__":
    main()