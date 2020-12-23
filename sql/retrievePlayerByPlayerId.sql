USE [nba]
GO


CREATE OR ALTER PROCEDURE [dbo].[RetrievePlayerByPlayerId] 
@PlayerId INT

AS
BEGIN

SET NOCOUNT ON;

SELECT PlayerId, FirstName, LastName, Active, BirthDate, Salary, Player.TeamId, RecordVersion FROM Player
	WHERE PlayerId = @PlayerId;

END