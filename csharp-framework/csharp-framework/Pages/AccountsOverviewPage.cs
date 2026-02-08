using csharp_framework.Hooks;
using csharp_framework.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Pages
{
    // Page Object representing the Accounts Overview page in ParaBank
    public class AccountsOverviewPage : BasePage
    {
        // Locator for the Accounts Overview page
        private readonly ILocator _heading;
        private readonly ILocator _accountRows;
        private readonly ILocator _balanceCells;
        private readonly ILocator _accountLinks;

        // Constructor receives the shared Playwright IPage instance from DI
        public AccountsOverviewPage(IPage page, Config config) : base(page , config)
        {
            // Heading element used to confirm the page has loaded
            _heading = _page.GetByRole(AriaRole.Heading, new() { Name = "Accounts Overview" });
            _accountRows = _page.Locator("#accountTable tbody tr");
            _balanceCells = _page.Locator("#accountTable tbody tr td:nth-child(2)");
            _accountLinks = _page.Locator("#accountTable tbody tr td a");

            //For traking page usage in Hooks
            PageTracker.Register(nameof(AccountsOverviewPage));

        }

        // Navigates to the Accounts Overview page
        public async Task NavigateAsync()
        {
            await NavigateToAsync("/parabank/overview.htm");
        }

        // Returns true if the Accounts Overview page heading is visible
        public async Task<bool> IsLoadedAsync()
        {
            return await _heading.IsVisibleAsync();
        }

        // Returns the number of account rows displayed in the table
        public async Task<int> GetAccountCountAsync()
        {
            return await _accountRows.CountAsync();
        }

        // Returns a list of account balances displayed in the table
        public async Task<List<string>> GetBalancesAsync()
        {
            var count = await _balanceCells.CountAsync();
            var list = new List<string>();

            for (int i = 0; i < count; i++)
                list.Add(await _balanceCells.Nth(i).InnerTextAsync());

            return list;
        }

        // Opens the details page for the first account listed
        public async Task OpenFirstAccountAsync()
        {
            await _accountLinks.First.ClickAsync();
        }

    }

}
