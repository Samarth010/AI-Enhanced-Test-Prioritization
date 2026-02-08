import json
import os

class HistoryManager:
    def __init__(self, history_path):
        self.history_path = history_path

    def load_history(self):
        """Loads test history JSON."""
        if not os.path.exists(self.history_path):
            return {"tests": []}

        with open(self.history_path, "r") as f:
            return json.load(f)

    def save_history(self, history):
        """Writes updated test history back to disk."""
        with open(self.history_path, "w") as f:
            json.dump(history, f, indent=4)

    def get_latest_runs(self):
        """Returns only the latest run per test name."""
        history = self.load_history()["tests"]
        latest = {}

        for test in history:
            name = test["name"]
            timestamp = test["timestamp"]

            if name not in latest or timestamp > latest[name]["timestamp"]:
                latest[name] = test

        return list(latest.values())