using csharp_framework.Pages;
using csharp_framework.Utils;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Newtonsoft.Json;
using Reqnroll;
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
    public class ReqnrollHooks
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private IPage _page;

        private readonly IObjectContainer _container;

        public ReqnrollHooks(IObjectContainer container)
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
        }

        // Runs before each scenario
        [BeforeScenario]
        public async Task SetupPlaywright()
        {
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

            // Create page
            var page = await context.NewPageAsync();
            _container.RegisterInstanceAs(page);
        }


        // Runs after each scenario
        [AfterScenario]
        public async Task Cleanup()
        {
            var context = _container.Resolve<IBrowserContext>();
            await context.CloseAsync();

            var browser = _container.Resolve<IBrowser>();
            await browser.CloseAsync();
        } 

    }
}


