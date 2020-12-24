# nTierNBA	Marcell Tanure

Player Management with CRUD operations and N-Tier architecture. User is able to see teams by division and players by team in a gridview
that allows user to choose a player for deletion.

User will also be able to see a specific player by Id, update, and insert a new player.

Content practiced: N-Tier architecture, input/output/inputOutput parameters, data annotations, concurrency, sql stored procedures.

Contains:

*SQL*
Sript to create the nba database.
Stored procedures used for CRUD operations. 

*C#*

Models => Player/Team

Types => Parameters structure

DataAccess => Methods used to retrieve/send data to database

Repository => RetrieveTeamList() / RetrieveTeamsByDivision() / RetrieveSearchByTeamId() / Delete() / RetrieveById() / 
PopulatePlayerRecord() / Insert() / Update()

Service =>

Validation using data annotation and custom validation for minimum age requirement.
