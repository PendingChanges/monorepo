Feature: Tag

A short summary of the feature

@tag
Scenario: Create a tag
	Given No existing tag
	When I create a tag with value "<value>"
	Then A tag with value "<value>" is created
Examples:
	| value |
	| pouet |

@tag
Scenario: Delete a tag
	Given A tag with value "<value>"
	When I delete the tag with value "<value>"
	Then The tag with value "<value>" is deleted
Examples:
	| value |
	| pouet |