using csharp_framework.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Tests.UI
{
    public class LoginTests : BaseTest
    {
        [Test, Category("Smoke")]
        public async Task Login_ValidUser_ShouldSucceed()
        {
            await _page.GotoAsync(ConfigManager.Settings.BaseUrl);
            await _page.FillAsync("input[name='username']", ConfigManager.Settings.Credentials.Username);
            await _page.FillAsync("input[name='password']", ConfigManager.Settings.Credentials.Password);
            await _page.ClickAsync("input[value='Log In']");
            Assert.That(await _page.InnerTextAsync("h1.title"), Does.Contain("Accounts Overview"));
        }
    }
}
