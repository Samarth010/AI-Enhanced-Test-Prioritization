using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace csharp_framework.Pages
{
    public class LoginPage : BasePage
    {
        // User-facing selectors that can be reused across tests
        private readonly ILocator _usernameInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;
        private readonly ILocator _accountsOverviewHeading;
        private readonly ILocator _errorMessage;

        public LoginPage(IPage page) : base(page)
        {
            //User-facing selectors
            _usernameInput = _page.Locator("input[name='username']");
            _passwordInput = _page.Locator("input[name='password']");
            _loginButton = _page.GetByRole(AriaRole.Button, new() { Name = "Log In" });
            _accountsOverviewHeading = _page.GetByRole(AriaRole.Heading, new() { Name = "Accounts Overview" });
            _errorMessage = _page.Locator("#rightPanel .error");
            //_errorMessage = _page.GetByRole(AriaRole.Alert);

        }

        /// Performs login by given credentials.
        public async Task Login(string username, string password)
        {
            try
            {
                await _usernameInput.FillAsync(username);
                await _passwordInput.FillAsync(password);
                await _loginButton.ClickAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Login failed", ex);
            }
            
        }

        /// Verifies that login succeeded by checking the Accounts Overview heading.
        public async Task<bool> IsLoginSuccessful()
        {
            try
            {
                await Assertions.Expect(_accountsOverviewHeading).ToBeVisibleAsync();
                return true;
            }
            catch
            {
                //To check expected failure path
                return false;
            }
        }

        /// Gets and shows the error message displayed on failed login.
        public async Task<string> GetLoginErrorMessage()
        {
            try
            {
                await Assertions.Expect(_errorMessage).ToBeVisibleAsync();
                return await _errorMessage.InnerTextAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get login error message", ex);
            }      
        }

    }
}
