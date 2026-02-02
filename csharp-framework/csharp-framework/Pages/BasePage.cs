using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_framework.Utils;

namespace csharp_framework.Pages
{
    public abstract class BasePage
    {

        protected readonly IPage _page;
        protected readonly Config _config;

        protected BasePage(IPage page, Config config)
        {
            _page = page;
            _config = config;
        }

        /// Navigate to a given URL.
        /// Any page can call this method to go to a specific URL.
        public async Task NavigateToAsync(string relativePath)
        {
            //var baseUrl = _config.BaseUrl.TrimEnd('/');
            //var path = relativePath.TrimStart('/');
            //var url = $"{baseUrl}/{path}";
            //await _page.GotoAsync(url);

            var baseUrl = _config.BaseUrl?.TrimEnd('/');

            // Validation check
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("BaseUrl is missing from configuration!");

            var path = relativePath.TrimStart('/');
            var url = $"{baseUrl}/{path}";

            Console.WriteLine($"DEBUG: Navigating to {url}");
            await _page.GotoAsync(url);
        }

        /// Wait until the page URL contains the expected substring.
        /// Useful for verifying navigation.
        public async Task WaitForUrlContainsAsync(string partialUrl)
        {
            await Assertions.Expect(_page)
                .ToHaveURLAsync(new System.Text.RegularExpressions.Regex($".*{partialUrl}.*"));
        }

        /// Get the current page title.
        public async Task<string> GetTitleAsync()
        {
            return await _page.TitleAsync();
        }

        /// Take a screenshot and save it to the given path.
        public async Task TakeScreenshotAsync(string path)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
        }
    }

}
