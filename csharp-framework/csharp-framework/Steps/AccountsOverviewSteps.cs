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
        private readonly AccountDetailsPage _accountDetailsPage;
        private readonly Config _config;


        // Constructor receives Page Objects registered in Hooks
        public AccountsOverviewSteps(LoginPage loginPage, AccountsOverviewPage accountsOverviewPage, Config config, AccountDetailsPage accountDetailsPage)
        {
            _loginPage = loginPage;
            _accountsOverviewPage = accountsOverviewPage;
            _config = config;
            _accountDetailsPage = accountDetailsPage;
        }

        // Logs in using valid credentials fetched from connfig file
        [Given("I log in with valid credentials")]
        public async Task GivenILogInWithValidCredentials()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync(_config.Credentials.Username, _config.Credentials.Password);
        }

        // Ensures that at least one account balance is displayed in the table
        [Then("I should see my account balances listed")]
        public async Task ThenIShouldSeeMyAccountBalancesListed()
        {
            var balances = await _accountsOverviewPage.GetBalancesAsync();
            balances.Should().OnlyContain(b => !string.IsNullOrWhiteSpace(b));

        }

        [Then("I should see the Accounts Overview page")]
        public async Task ThenIShouldSeeTheAccountsOverviewPage()
        {
            var loaded = await _accountsOverviewPage.IsLoadedAsync();
            loaded.Should().BeTrue("the Accounts Overview page should be visible after login");

        }

        [Given("the user is logged into ParaBank")]
        public async Task GivenTheUserIsLoggedIntoParaBank()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.LoginAsync(_config.Credentials.Username, _config.Credentials.Password);
        }

        [Given("the user navigates to the Accounts Overview page")]
        public async Task GivenTheUserNavigatesToTheAccountsOverviewPage()
        {
            await _accountsOverviewPage.NavigateAsync();
        }

        [Then("at least one account should be listed")]
        public async Task ThenAtLeastOneAccountShouldBeListed()
        {
            var count = await _accountsOverviewPage.GetAccountCountAsync();
            count.Should().BeGreaterThan(0);
        }

        [Then("each account should display a current balance")]
        public async Task ThenEachAccountShouldDisplayACurrentBalance()
        {
            var balances = await _accountsOverviewPage.GetBalancesAsync();
            balances.Should().OnlyContain(b => !string.IsNullOrWhiteSpace(b));
        }

        [When("the user selects a specific account number")]
        public async Task WhenTheUserSelectsASpecificAccountNumber()
        {
            await _accountsOverviewPage.OpenFirstAccountAsync();
        }

        [Then("the Account Details page should be displayed")]
        public async Task ThenTheAccountDetailsPageShouldBeDisplayed()
        {
            var loaded = await _accountDetailsPage.IsLoadedAsync();
            loaded.Should().BeTrue();
        }
    }

}
