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
    // Step definitions for the Account Details feature
    [Binding]
    public class AccountDetailsSteps
    {
        private readonly AccountDetailsPage _accountDetailsPage;

        // Constructor receives only AccountDetailsPage
        public AccountDetailsSteps(AccountDetailsPage accountDetailsPage)
        {
            _accountDetailsPage = accountDetailsPage;
        }

        [Then("the Account Details page should be displayed")]
        public async Task ThenTheAccountDetailsPageShouldBeDisplayed()
        {
            var loaded = await _accountDetailsPage.IsLoadedAsync();
            loaded.Should().BeTrue();
        }
    }
}
