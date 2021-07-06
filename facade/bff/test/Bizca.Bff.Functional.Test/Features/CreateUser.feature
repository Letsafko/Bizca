Feature: CreateUser
	As a partner
	I want to create a new user
	In ordeer to create his account

@mytag
Scenario: Create a new user
	Given Partner creates randomized informations for user 'bob'.
	 When Partner creates 'bob' through api.
	 Then the response should be '201'.
	  And '1' event of type 'SendConfirmationEmalNotification' has been published.