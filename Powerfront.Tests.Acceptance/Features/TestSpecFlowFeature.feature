Feature: TestSpecFlowFeature
	In order to see how old my customers are
	As an administrator
	I want to be able to see a list of customers and their ages

@resetDatabase
Scenario: UserWantsToSeeListOfCustomersWithAges
	Given I have navigated to the maintenance page
	And I have selected to see a list of customers and ages
	When I click list customers
	Then the resulting page should display a list of cutomers and their ages
