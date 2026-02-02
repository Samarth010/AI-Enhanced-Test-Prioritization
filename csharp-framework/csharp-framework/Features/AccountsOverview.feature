@accounts @smoke @regression
Feature: Accounts Overview functionality
  As a logged-in ParaBank user
  I want to view my accounts overview
  So that I can see my account balances
  
  Scenario: View Accounts Overview after successful login
	Given I am on the login page
	And I log in with valid credentials
	Then I should see the Accounts Overview page
	And I should see my account balances listed
    
  Scenario: Accounts Overview displays account information
    Given the user is logged into ParaBank
    And the user navigates to the Accounts Overview page
    Then at least one account should be listed
    And each account should display a current balance

  Scenario: Navigation to account details
    Given the user is logged into ParaBank
    And the user navigates to the Accounts Overview page
    When the user selects a specific account number
    Then the Account Details page should be displayed


