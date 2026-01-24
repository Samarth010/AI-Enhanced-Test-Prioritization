using csharp_framework.Pages;
using Microsoft.Playwright;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Hooks
{
    [Binding]
    public class ReqnrollHooks
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private IPage _page;

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
        }

        // Runs before each scenario
        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext context)
        {
            _page = await _browser.NewPageAsync();

            // Register Playwright page + Page Objects for DI
            context.ScenarioContainer.RegisterInstanceAs(_page);
            context.ScenarioContainer.RegisterInstanceAs(new LoginPage(_page));
        }

        // Runs after each scenario
        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_page != null)
                await _page.CloseAsync();
        }

        // Runs once after the entire test run
        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }
    }
}


