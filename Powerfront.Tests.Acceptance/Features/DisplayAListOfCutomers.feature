Feature: DisplayAListOfCutomers
	In order to see how old my customers are
	As an administrator
	I want to be able to see a list of customers and their ages

@resetDatabase
@createScenarioSnapshotsFolder
Scenario: UserWantsToSeeListOfCustomersWithAges
	Given I have navigated to the application root
	When I click the maintenance button
	Then the resulting page should display a list of cutomers and their ages
