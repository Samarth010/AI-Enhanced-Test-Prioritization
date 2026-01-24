using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using csharp_framework.Pages;
using FluentAssertions;


[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;
    public LoginSteps(LoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    [Given("I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        await _loginPage.NavigateAsync();
    }

    [When("I enter valid credentials")]
    public async Task WhenIEnterValidCredentials()
    {
        await _loginPage.Login("validUser", "validPass");
    }

    [When("I enter invalid credentials")]
    public async Task WhenIEnterInvalidCredentials()
    {
        await _loginPage.Login("wrongUser", "wrongPass");
    }

    [When("I click Log In")]
    public Task WhenIClickLogIn()
    {
        return Task.CompletedTask;
    }

    [Then("I should see the Accounts Overview page")]
    public async Task ThenIShouldSeeTheAccountsOverviewPage()
    {
        var success = await _loginPage.IsLoginSuccessful();
        success.Should().BeTrue("a successful login should navigate to Accounts Overview");
    }
}
