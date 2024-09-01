--9.	Increase Age Stored Procedure 
--Create stored procedure usp_GetOlder (directly in the database using Management Studio or any other similar tool) 
--that receives MinionId and increases that minion’s age by 1. 
--Write a program that uses that stored procedure to increase the age of a minion whose id will be given as input from the console. 
--After that print the name and the age of that minion.

USE MinionsDB;
--------------------------------
IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'usp_GetOlder') DROP PROCEDURE usp_GetOlder;
------------------------------------------------
GO
CREATE PROCEDURE usp_GetOlder @MinionId INT
AS
   UPDATE Minions SET Age += 100 WHERE Id = @MinionId
GO
------------------------------------------------
EXEC usp_GetOlder @MinionId = 1
----------------
SELECT Name,Age FROM Minions;
----------------
