using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using csharp_framework.Utils;

namespace csharp_framework.Pages
{
    public class AccountDetailsPage : BasePage
    {
        private readonly ILocator _heading;

        public AccountDetailsPage(IPage page, Config config) : base(page, config)
        {
            _heading = _heading = _page.GetByRole(AriaRole.Heading, new() { Name = "Account Details" });
        }

        public async Task<bool> IsLoadedAsync()
        {
            return await _heading.IsVisibleAsync();
        }
    }
}
