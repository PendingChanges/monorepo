Feature: Song

@song
Scenario: I can create a song
	Given No existing tag
	When I create a song with name "<name>" and artist id "<artistId>"
	Then A song with name "<name>" and artist id "<artistId>" is created
Examples: 
	| name  | artistId                             |
	| pouet | 6268CBF0-BEB8-42D2-A181-7F714B890806 |
