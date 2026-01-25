using csharp_framework.Pages;
using csharp_framework.Utils;
using FluentAssertions; 
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Steps
{
    // Step definitions for the Accounts Overview feature
    [Binding]
    public class AccountsOverviewSteps
    {
        // Page Objects injected via Reqnroll's dependency injection
        private readonly LoginPage _loginPage;
        private readonly AccountsOverviewPage _accountsOverviewPage;
        private readonly Config _config;


        // Constructor receives Page Objects registered in Hooks
        public AccountsOverviewSteps(LoginPage loginPage, AccountsOverviewPage accountsOverviewPage, Config config)
        {
            _loginPage = loginPage;
            _accountsOverviewPage = accountsOverviewPage;
            _config = config;
        }

        // Logs in using valid credentials fetched from connfig file
        [Given("I log in with valid credentials")]
        public async Task GivenILogInWithValidCredentials()
        {
            await _loginPage.Login(_config.Credentials.Username, _config.Credentials.Password);
        }

        // Verifies that the Accounts Overview page has successfully loaded
        //[Then("I should be on the Accounts Overview page")]
        //public async Task ThenIShouldBeOnTheAccountsOverviewPage()
        //{
        //    var isLoaded = await _accountsOverviewPage.IsLoadedAsync();
        //    isLoaded.Should().BeTrue("a successful login should redirect to the Accounts Overview page");
        //}

        // Ensures that at least one account balance is displayed in the table
        [Then("I should see my account balances listed")]
        public async Task ThenIShouldSeeMyAccountBalancesListed()
        {
            var count = await _accountsOverviewPage.GetAccountCountAsync();
            count.Should().BeGreaterThan(0, "a logged-in user should have at least one account displayed");
        }
    }

}
