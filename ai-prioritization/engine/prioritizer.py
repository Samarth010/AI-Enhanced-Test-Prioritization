import json
from datetime import datetime, timezone

class TestPrioritizer:
    def __init__(self, history_path, impact_map_path):
        self.history_path = history_path
        self.impact_map_path = impact_map_path
        self.history = self.load_history()
        self.impact_map = self.load_impact_map()

    def load_history(self):
        with open(self.history_path, "r") as f:
            return json.load(f)["tests"]

    def load_impact_map(self):
        with open(self.impact_map_path, "r") as f:
            return json.load(f)

    def get_tests_impacted_by_changes(self, changed_pages):
        impacted_tests = set()

        for page in changed_pages:
            if page in self.impact_map:
                for test in self.impact_map[page]:
                    impacted_tests.add(test)

        return list(impacted_tests)
    
    def get_latest_test_runs(self):
        latest = {}
        for test in self.history:
            name = test["name"]
            timestamp = test["timestamp"]
            if name not in latest or timestamp > latest[name]["timestamp"]:
                latest[name] = test
        return list(latest.values())

    def score_test(self, test):
        score = 0

        # 1. Recent failures get higher priority
        if test["status"] == "Failed":
            score += 3

        # 2. Slow tests get higher priority (they cost more)
        score += min(test["duration"] / 5, 2)

        # 3. Recency weighting
        timestamp = datetime.fromisoformat(test["timestamp"].replace("Z", "+00:00"))
        now = datetime.now(timezone.utc)
        age_days = (now - timestamp).days
        score += max(0, 2 - (age_days * 0.1))

        return score

    def prioritize(self, changed_pages):
        impacted_tests = self.get_tests_impacted_by_changes(changed_pages)

        latest_tests = self.get_latest_test_runs()

        scored_tests = []
        for test in latest_tests:
            if test["name"] in impacted_tests:
                scored_tests.append({
                    "name": test["name"],
                    "score": self.score_test(test),
                    "status": test["status"],
                    "duration": test["duration"]
                })

        # Sort by score descending
        scored_tests.sort(key=lambda x: x["score"], reverse=True)
        return scored_tests