USE [nba]
GO


CREATE OR ALTER PROCEDURE [dbo].[UpdatePlayer] 
@PlayerId INT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Active BIT,
@BirthDate DATETIME2,
@Salary DECIMAL(19,0),
@TeamId INT = NULL

AS
BEGIN

SET NOCOUNT ON;

UPDATE Player SET
			FirstName = @FirstName,
			LastName = @LastName,
			Active = @Active,
			BirthDate = @BirthDate,
			Salary = @Salary,
			TeamId = @TeamId
		WHERE
			PlayerId = @PlayerId;

END