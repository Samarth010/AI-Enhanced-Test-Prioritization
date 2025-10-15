using csharp_framework.Utils;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_framework.Pages;

namespace csharp_framework.Tests.UI
{
    [TestFixture]
    [Category("UI")]
    public class LoginTests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        public async Task Login_ValidUser_ShouldSucceed()
        {
            var loginPage = new LoginPage(_page);

            //Navigate to base URL from config
            await loginPage.NavigateToAsync(ConfigManager.Settings.BaseUrl);

            //Perform login with valid credentials from config
            await loginPage.Login(
                ConfigManager.Settings.Credentials.Username,
                ConfigManager.Settings.Credentials.Password);

            //Asset Login Success
            Assert.That(await loginPage.IsLoginSuccessful(), Is.True, "Login should succeed with valid credentials.");

        }

        [Test]
        [Category("Negative")]
        public async Task Login_InvalidUser_ShouldShowError()
        {
            var loginPage = new LoginPage(_page);

            await loginPage.NavigateToAsync((string)ConfigManager.Settings.BaseUrl);

            // Attempt login with invalid credentials
            await loginPage.Login("wronguser", "wrongpass");

            // Assert error message is shown
            var error = await loginPage.GetLoginErrorMessage();
            Assert.That(error, Does.Contain("The username and password could not be verified"),
                "Invalid login should display an error message");
        }

    }
}
