﻿Feature: Idea

A short summary of the feature

@idea
Scenario: The user create an idea
	Given No existing idea
	When A user with id "testuser" create an idea with name "Idea Test" and descrition "Description test"
	Then An idea "Idea Test" with description "Description test" owned by "testuser" is created

@idea
Scenario: A user delete its own idea
	Given An existing idea with name "Idea Test", description "Description Test" and an owner "testuser"
	When A user with id "testuser" delete the idea
	Then The idea is deleted
	And No errors

@idea
Scenario: A user tries to delete an idea he doesn't own
	Given An existing idea with name "Idea Test", description "Description Test" and an owner "testuser"
	When A user with id "testuser2" delete the idea
	Then An error with code "NOT_IDEA_OWNER" is raised
	And The idea is not deleted


@idea
Scenario: A user modify the idea
	Given An existing idea with name "<ideaName>", description "<ideaDescritpion>" and an owner "<userid>"
	When A user with id "<userid>" modify the idea to new name "<newName>" and new description "<newDescription>"
	Then The idea is modified with new name "<newName>" and new description "<newDescription>"
	And No errors

Examples:
	| userid   | ideaName | ideaDescritpion  | newName       | newDescription            |
	| testuser | Idea     | Idea Description | Idea Modified | Idea Description Modified |