using csharp_framework.Pages;
using csharp_framework.Utils;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Newtonsoft.Json;
using Reqnroll;
using Reqnroll.BoDi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private IPage _page;

        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        } 

        // Runs once before the entire test run
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,       // Set to true for CI
                SlowMo = 0              // Optional: slow down actions for debugging
            });

            // Ensure artifact folders exist
            Directory.CreateDirectory("traces");
            Directory.CreateDirectory("screenshots");
        }

        // Runs before each scenario
        [BeforeScenario]
        public async Task SetupPlaywright(ScenarioContext scenarioContext)
        {
            //Record Start time
            scenarioContext["StartTime"] = DateTime.UtcNow;

            // Load config
            var config = ConfigManager.Settings;
            _container.RegisterInstanceAs(config);

            // Create Playwright
            var playwright = await Playwright.CreateAsync();
            _container.RegisterInstanceAs(playwright);

            // Launch browser
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = config.Headless,
                SlowMo = config.SlowMo
            });
            _container.RegisterInstanceAs(browser);

            // Create context
            var context = await browser.NewContextAsync();
            _container.RegisterInstanceAs(context);

            // Start tracing
            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });


            // Create page
            var page = await context.NewPageAsync();
            _container.RegisterInstanceAs(page);
        }


        // Runs after each scenario
        [AfterScenario]
        public async Task Cleanup(ScenarioContext scenarioContext)
        {
            Console.WriteLine("📌 AfterScenario triggered");
            Console.WriteLine($"📌 Scenario: {scenarioContext.ScenarioInfo.Title}");
            Console.WriteLine($"📌 Error: {scenarioContext.TestError}");

            var context = _container.Resolve<IBrowserContext>();
            var browser = _container.Resolve<IBrowser>();
            var page = _container.Resolve<IPage>();

            var scenarioName = scenarioContext.ScenarioInfo.Title;
            var status = scenarioContext.TestError == null ? "Passed" : "Failed";

            //Retreive start time
            var startTime = (DateTime)scenarioContext["StartTime"];
            var duration = (DateTime.UtcNow - startTime).TotalSeconds;

            // Gets the pages used during the test and resets the tracker for the next test
            var pagesUsed = PageTracker.GetAndReset();


            Console.WriteLine("📌 Writing test history entry...");
            // Write to test history
            TestHistoryWriter.AddTestResult(
                scenarioName,
                status,
                duration,
                pagesUsed
            );


            // Stop tracing and save trace
            await context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"traces/{scenarioName}_trace.zip"
            });

            // Screenshot on failure
            if (scenarioContext.TestError != null)
            {
                await page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = $"screenshots/{scenarioName}_FAILED.png",
                    FullPage = true
                });
            }

            await context.CloseAsync();
            await browser.CloseAsync();

        }

    }

    public static class PageTracker
    {
        public static HashSet<string> PagesUsed = new HashSet<string>();

        public static void Register(string pageName)
        {
            PagesUsed.Add(pageName);
        }

        public static List<string> GetAndReset()
        {
            var list = PagesUsed.ToList();
            PagesUsed.Clear();
            return list;
        }
    }
}


