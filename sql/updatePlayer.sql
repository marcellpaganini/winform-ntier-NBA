USE [nba]
GO


CREATE OR ALTER PROCEDURE [dbo].[UpdatePlayer] 
@RecordVersion ROWVERSION OUTPUT,
@PlayerId INT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Active BIT,
@BirthDate DATETIME2,
@Salary DECIMAL(19,0),
@TeamId INT = NULL

AS
BEGIN

IF(SELECT [RecordVersion] FROM Player WHERE PlayerId = @PlayerId) <> @RecordVersion
	THROW 50001, 'The record has been updated since the last time you retrieved it', 1

UPDATE Player SET
			FirstName = @FirstName,
			LastName = @LastName,
			Active = @Active,
			BirthDate = @BirthDate,
			Salary = @Salary,
			TeamId = @TeamId
		WHERE
			PlayerId = @PlayerId;

SET @RecordVersion = (SELECT RecordVersion FROM Player WHERE PlayerId = @PlayerId)

END