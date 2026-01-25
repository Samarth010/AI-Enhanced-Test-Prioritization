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
        // Locator for the page heading (e.g., "Accounts Overview")
        private readonly ILocator _heading;

        // Locator for the accounts table that lists account balances
        private readonly ILocator _accountsTable;

        // Constructor receives the shared Playwright IPage instance from DI
        public AccountsOverviewPage(IPage page) : base(page)
        {
            // Heading element used to confirm the page has loaded
            _heading = _page.Locator("h1.title");

            // Main accounts table containing account rows
            _accountsTable = _page.Locator("#accountTable");
        }

        // Returns true if the Accounts Overview page heading is visible
        public async Task<bool> IsLoadedAsync()
        {
            return await _heading.IsVisibleAsync();
        }

        // Returns the number of account rows displayed in the table
        public async Task<int> GetAccountCountAsync()
        {
            return await _accountsTable.Locator("tbody tr").CountAsync();
        }
    }

}
