Feature: Character

A short summary of the feature

@character
Scenario: A character can be created with default values
	Given No existing character
	When I create a character with the name "<name>"
	Then The character's name should be "<name>"
	And The character's level should be <level>
	And The character's strength should be <strength>
	And The character's precision should be <precision>
	And The character's intelligence should be <intelligence>
	And The character's hp should be <hp>
	And The character's mp should be <mp>
	And the character's defense should be <defense>
	And the character's spirit should be <spirit>
	And the character's evasion should be <evasion>
	And the character's speed should be <speed>
Examples:
	| name  | level | strength | precision | intelligence | hp | mp | defense | spirit | evasion | speed |
	| pouet | 1     | 10       | 10        | 10           | 100 | 50 | 5       | 5      | 5       | 5     |

@character
Scenario: A character can be renamed
	Given An existing character with the name "<oldName>"
	When I rename the character to "<newName>"
	Then The character's name should be "<newName>"
Examples: 
	| oldName | newName |
	| pouet   | pouet2  |