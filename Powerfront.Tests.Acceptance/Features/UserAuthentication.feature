Feature: UserAuthentication
	In order to minimise security vulnerabilites and associated costs
	As a system administrator
	I want to be sure that users are authenticated against a secure, robust, flexible and scalable authentication system

@mytag
Scenario: UserShouldLogin
	Given I need to access information in the eCommerce system
	And I am not authenticated
	When I navigate to the application url
	Then I am asked for credentials
	Then I after I have authenticated I am directed to the application url that was originally requested
