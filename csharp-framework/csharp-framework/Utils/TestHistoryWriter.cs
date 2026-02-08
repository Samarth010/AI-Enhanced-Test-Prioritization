using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Utils
{
    public class TestHistoryWriter
    {


        private static string GetSolutionRoot()
        {
            var dir = AppContext.BaseDirectory;

            while (!Directory.GetFiles(dir, "*.sln").Any())
            {
                dir = Directory.GetParent(dir).FullName;
            }

            return dir;
        }

        private static readonly string HistoryPath =
           Path.Combine(GetSolutionRoot(),"..", "ai-prioritization", "data", "test_history.json");

        public class TestEntry
        {
            public string name { get; set; }
            public string status { get; set; }
            public double duration { get; set; }
            public List<string> pages { get; set; }
            public string timestamp { get; set; }
        }

        public class TestHistory
        {
            public List<TestEntry> tests { get; set; } = new();
        }

        public static void AddTestResult(string testName, string status, double duration, List<string> pages)
        {
            Console.WriteLine("📌 [TestHistoryWriter] AddTestResult called");

            Console.WriteLine($"📌 Test Name: {testName}");
            Console.WriteLine($"📌 Status: {status}");
            Console.WriteLine($"📌 Duration: {duration}");
            Console.WriteLine($"📌 Pages: {string.Join(", ", pages)}");

            Console.WriteLine($"📌 Writing to path: {HistoryPath}");

            var directory = Path.GetDirectoryName(HistoryPath);
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"📌 Directory does not exist. Creating: {directory}");
                Directory.CreateDirectory(directory);
            }
            else
            {
                Console.WriteLine($"📌 Directory exists: {directory}");
            }

            TestHistory history;

            if (File.Exists(HistoryPath))
            {
                Console.WriteLine("📌 test_history.json exists. Loading existing data.");
                var json = File.ReadAllText(HistoryPath);
                history = JsonConvert.DeserializeObject<TestHistory>(json) ?? new TestHistory();
            }
            else
            {
                Console.WriteLine("📌 test_history.json does NOT exist. Creating new file.");
                history = new TestHistory();
            }

            history.tests.Add(new TestEntry
            {
                name = testName,
                status = status,
                duration = duration,
                pages = pages,
                timestamp = DateTime.UtcNow.ToString("o")
            });

            var updatedJson = JsonConvert.SerializeObject(history, Formatting.Indented);
            File.WriteAllText(HistoryPath, updatedJson);

            Console.WriteLine("📌 Successfully wrote test result to test_history.json");

        }


    }
}
