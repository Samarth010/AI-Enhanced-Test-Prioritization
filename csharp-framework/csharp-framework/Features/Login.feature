Feature: Login functionality
  As a ParaBank user
  I want to log into the application
  So that I can access my accounts

  Scenario: Successful login
    Given I am on the login page
    When I enter valid credentials
    And I click Log In
    Then I should see the Accounts Overview page

  Scenario: Invalid login
    Given I am on the login page
    When I enter invalid credentials
    And I click Log In
    Then I should see a login error message