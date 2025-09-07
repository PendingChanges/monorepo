Feature: Character

A short summary of the feature

@character
Scenario: A character must have a name
	Given No existing character
	When I create a character with the name "<name>"
	Then The character's name should be "<name>"
Examples:
	| name  |
	| pouet |

@character
Scenario: A character can be renamed
	Given An existing character with the name "<oldName>"
	When I rename the character to "<newName>"
	Then The character's name should be "<newName>"
Examples: 
	| oldName | newName |
	| pouet   | pouet2  |

@character
Scenario: A character can gain experience points
	Given An existing character with the name "<name>" and <xpBase> experience points
	When I add <xp> experience points to the character
	Then The character should have <xpSum> experience points
Examples: 
	| name  | xpBase | xp | xpSum |
	| pouet | 0      | 5  | 5     |