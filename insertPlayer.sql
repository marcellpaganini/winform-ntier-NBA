USE [nba]
GO


CREATE OR ALTER PROCEDURE [dbo].[InsertPlayer] 
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Active BIT,
@BirthDate DATETIME2,
@Salary DECIMAL(19,0),
@TeamId INT = NULL

AS
BEGIN

SET NOCOUNT ON;

INSERT INTO Player
	(FirstName, LastName, Active, BirthDate, Salary, TeamId)
VALUES
	(@FirstName, @LastName, @Active, @BirthDate, @Salary, @TeamId)

END