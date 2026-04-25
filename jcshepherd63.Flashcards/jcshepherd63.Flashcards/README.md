# Flashcard Application

INITIAL LOCAL SETUP:

Please run the following local commands to setup the db on your local.

sqllocaldb create FlashcardAppLocalDb
Run Table Creation Script.sql file on the FlashcardAppLocalDb

Features:

SQL Server Database with the connection string stored in an appsettings.json file
Console built with Spectre for tidy user interaction

Challenges:

As the project size gets bigger, I find myself getting lost more often; however, I am trying to focus on cleanly organized code, so I have been able to figure it out every time.
I did not have to change the architecture of the application at all though once I got the basic structure so I was very happy about that.
Areas to Improve:

I really need to improve my overall naming abilities. I kept getting confused by the names on my methods, properties, and variables. The KISS standard of keeping things simple should help with that going forward.
I also need to improve my understanding of the abstract keyword.
Finally, I need to do a better job of passing parameters into methods as opposed to setting them equal to a variable inside a method to make it accessible within.