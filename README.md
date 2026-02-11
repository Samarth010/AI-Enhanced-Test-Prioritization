# 📘 AI‑Enhanced Test Prioritization Framework  
A hybrid automation ecosystem combining **C# Playwright**, **SpecFlow BDD**, **API testing**, and a **Python‑based AI engine** to intelligently prioritize tests based on historical failures and real‑time code changes.

---

## 🚀 Overview  
This project delivers a **scalable, maintainable, and intelligent test automation framework** designed for modern CI/CD pipelines. It blends **UI automation**, **API validation**, and **AI‑driven test selection** to reduce execution time, improve feedback loops, and focus testing where it matters most.

The system automatically analyzes:  
- Recent code changes (via Git)  
- Impacted Page Objects  
- Historical test failures  
- Test‑to‑page mappings  

…to generate a **ranked list of tests** optimized for speed and risk coverage.

---

## 🧩 Key Features

### 🔹 Hybrid Automation Framework  
- Built with **C# Playwright** for fast, reliable UI automation  
- **Page Object Model (POM)** for maintainability  
- **SpecFlow BDD** for readable, business‑aligned test scenarios  
- Integrated **API testing layer** for end‑to‑end validation

### 🔹 AI‑Driven Test Prioritization (Python Engine)  
- Analyzes **historical failure patterns**  
- Maps tests to impacted Page Objects  
- Uses **weighted heuristics** to rank tests by risk  
- Supports both manual and autonomous modes:
  - `--changed-pages`
  - `--auto` (Git‑aware)

### 🔹 Git‑Aware Change Detection  
- Detects modified Page Objects using:
  - Commit diffs  
  - Branch‑to‑branch comparisons  
  - PR‑level change analysis  
- Enables **PR‑aware test selection** in CI/CD

### 🔹 Developer‑Friendly Tooling  
- Config‑driven execution  
- Rich reporting with:
  - Screenshots  
  - Playwright traces  
  - API response logs  
- Clean CLI interface for local and CI workflows

### 🔹 CI/CD Ready  
- Designed for GitHub Actions or any CI provider  
- Automatically selects only impacted tests  
- Reduces pipeline time while increasing confidence

---

## 📂 Project Structure
```ai-prioritization/
    │
    ├── engine/                     # Python AI engine
    │   ├── prioritize.py           # CLI entry point
    │   ├── git_analyzer.py         # Git diff + change detection
    │   ├── prioritizer.py          # Test ranking logic
    │   └── data/                   # Historical data + impact maps
    │
    ├── csharp-framework/           # Playwright + SpecFlow + API tests
    │   ├── Pages/                  # Page Objects
    │   ├── Features/               # BDD scenarios
    │   ├── Steps/                  # Step definitions
    │   └── ApiTests/               # API test suite
    │
    └── README.md
```
---

## 🧠 How the AI Prioritizer Works

1. **Detect changes**  
   - Auto mode: GitAnalyzer inspects diffs  
   - Manual mode: user provides changed pages

2. **Map changes to tests**  
   - Uses a JSON‑based impact map linking Page Objects → Tests

3. **Apply weighted scoring**  
   - Recent failures  
   - Failure frequency  
   - Severity  
   - Change impact

4. **Output prioritized test list**  
   - Highest‑risk tests first  
   - Ideal for PR validation and fast feedback

---

## 🛠️ Usage

### **Auto Mode (Git‑Aware)**
Automatically detects changed Page Objects using Git and prioritizes tests accordingly.
```
python prioritize.py --auto
```
### **Manual Mode**
Specify changed pages manually.
```
python prioritize.py --changed-pages LoginPage HomePage CartPage
```
### **Branch‑to‑Branch Diff**
```
python prioritize.py --from main --to feature/my-branch
```

---

## 📈 Benefits

- Faster CI pipelines  
- Smarter test execution  
- Reduced noise from irrelevant failures  
- Better developer feedback loops  
- Scalable architecture ready for future AI enhancements  

---

## 🤝 Contributions  
This project is designed with extensibility in mind.  
Future enhancements may include ML‑based predictions, flaky test detection, and deeper API‑UI correlation.

