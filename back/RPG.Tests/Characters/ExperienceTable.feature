Feature: ExperienceTable

A short summary of the feature

@experience-table
Scenario: I can get the level for a given experience points
	Given A experience table with the following levels:
		| Level | ExperiencePoints |
		| 1     | 0                |
		| 2     | 100              |
		| 3     | 300              |
		| 4     | 600              |
		| 5     | 1000             |
	When I ask for the level corresponding to <experiencePoints> experience points
	Then I should get level <level>
Examples:
	| experiencePoints | level |
	| 0                | 1     |
	| 50               | 1     |
	| 100              | 2     |
	| 250              | 2     |
	| 300              | 3     |
	| 450              | 3     |
	| 600              | 4     |
	| 800              | 4     |
	| 1000             | 5     |
	| 1500             | 5     |

@experience-table
Scenario: I can get the experience points required for next level
	Given A experience table with the following levels:
		| Level | ExperiencePoints |
		| 1     | 0                |
		| 2     | 100              |
		| 3     | 300              |
		| 4     | 600              |
		| 5     | 1000             |
	When I ask for the experience points required for next level from experience points <experiencePoints>
	Then I should get <experiencePointsForNextLevel> experience points
Examples:
	| experiencePoints | experiencePointsForNextLevel |
	| 0                | 100                          |
	| 50               | 50                           |
	| 100              | 200                          |
	| 250              | 50                           |
	| 300              | 300                          |
	| 450              | 150                          |
	| 600              | 400                          |
	| 800              | 200                          |
	| 1000             | 0                            |
