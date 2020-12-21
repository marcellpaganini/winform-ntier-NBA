USE [nba]
GO

CREATE OR ALTER   PROCEDURE [dbo].[RetrieveTeamByDivision] 
@Division INT

AS
BEGIN

SET NOCOUNT ON;

SELECT teamId, TeamName FROM Team
	WHERE Division = @Division;

END