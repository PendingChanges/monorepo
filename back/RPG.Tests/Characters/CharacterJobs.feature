Feature: Character Jobs

Testing character job system with unlock conditions

@character
Scenario: A character is created with default jobs
	Given No existing character
	When I create a character with the name "Test"
	Then The character should have 7 jobs
	And The character should have a job called "Ecuyer"
	And The character should have a job called "Chevalier"
	And The character should have a job called "Alchimiste"
	And The character should have a job called "Mage Blanc"
	And The character should have a job called "Mage Noir"
	And The character should have a job called "Archer"
	And The character should have a job called "Moine"

@character
Scenario: Ecuyer and Alchimiste are unlocked by default
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Ecuyer" should be unlocked
	And The job "Alchimiste" should be unlocked

@character
Scenario: Other jobs are locked by default
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Chevalier" should be locked
	And The job "Mage Blanc" should be locked
	And The job "Mage Noir" should be locked
	And The job "Archer" should be locked
	And The job "Moine" should be locked

@character
Scenario: Chevalier requires Ecuyer level 3
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Chevalier" should have 1 unlock condition
	And The job "Chevalier" should require "Ecuyer" at level 3

@character
Scenario: Mage Blanc requires Alchimiste level 3
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Mage Blanc" should have 1 unlock condition
	And The job "Mage Blanc" should require "Alchimiste" at level 3

@character
Scenario: Mage Noir requires Alchimiste level 3
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Mage Noir" should have 1 unlock condition
	And The job "Mage Noir" should require "Alchimiste" at level 3

@character
Scenario: Archer requires Ecuyer level 3 and Alchimiste level 2
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Archer" should have 2 unlock conditions
	And The job "Archer" should require "Ecuyer" at level 3
	And The job "Archer" should require "Alchimiste" at level 2

@character
Scenario: Moine requires Chevalier level 3
	Given No existing character
	When I create a character with the name "Test"
	Then The job "Moine" should have 1 unlock condition
	And The job "Moine" should require "Chevalier" at level 3
