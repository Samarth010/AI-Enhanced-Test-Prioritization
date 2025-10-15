using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Utils
{
    public class BaseTest
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IPage _page;

        [SetUp]
        public async Task Setup()
        {
            try
            {
                _playwright = await Playwright.CreateAsync();

                //Read browser type from config.json
                var browserType = ConfigManager.Settings.Browser.ToLower();
                var headless = ConfigManager.Settings.Headless;
                var slowMo = ConfigManager.Settings.SlowMo;

                _browser = browserType switch
                {
                    "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        SlowMo = slowMo
                    }),

                    "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        SlowMo = slowMo
                    }),

                    _ => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        SlowMo = slowMo
                    })
                };

                _page = await _browser.NewPageAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test setup failed: {ex.Message}");
            }
        }


        [TearDown]
        public async Task Teardown()
        {
            try
            {
                if (_browser != null)
                    await _browser.CloseAsync();

                _playwright.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Teardown encounterd an error: {ex.Message}");
            }
        }
    }
}
