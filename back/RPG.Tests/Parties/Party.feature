Feature: Party

A short summary of the feature

@tag1
Scenario: I can add a member to a party
	Given A party with a member called "<alreadyThere>"
	When I add a member "<added>" to the party
	Then The last member of the party should be "<added>"
Examples:
	| alreadyThere | added  |
	| pouet        | pouet2 |