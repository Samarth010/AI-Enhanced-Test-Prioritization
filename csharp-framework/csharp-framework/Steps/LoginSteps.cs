using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using csharp_framework.Pages;
using FluentAssertions;
using csharp_framework.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;


[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;
    private readonly Config _config;
    public LoginSteps(LoginPage loginPage, Config config)
    {
        _loginPage = loginPage;
        _config = config;
    }

    [Given("I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        await _loginPage.NavigateAsync();
    }

    [When("I enter valid credentials")]
    public async Task WhenIEnterValidCredentials()
    {
        await _loginPage.LoginAsync(_config.Credentials.Username, _config.Credentials.Password);
    }

    [When("I enter invalid credentials")]
    public async Task WhenIEnterInvalidCredentials()
    {
        await _loginPage.LoginAsync("wrongUser", "wrongPass");
    }

    [When("I click Log In")]
    public Task WhenIClickLogIn()
    {
        return Task.CompletedTask;
    }

    [Then("I should see a login error message")]
    public async Task ThenIShouldSeeALoginErrorMessage()
    {
        var error = await _loginPage.GetLoginErrorMessage();
        error.Should().NotBeNullOrWhiteSpace("an invalid login should show an error message");
    }
}
