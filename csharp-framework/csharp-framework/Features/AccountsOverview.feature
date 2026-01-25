Feature: Accounts Overview functionality
  As a logged-in ParaBank user
  I want to view my accounts overview
  So that I can see my account balances

  Scenario: View Accounts Overview after sucessful login
	Given I am on the login page
	And I log in with valid credentials
	Then I should see the Accounts Overview page
	Then I should see my account balances listed