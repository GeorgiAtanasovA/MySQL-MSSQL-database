--4.	Add Minion
--Write a program that reads information about a minion and its villain and adds it to the database. 
--In case the town of the minion is not in the database, insert it as well. 
--In case the villain is not present in the database, add him too with a default evilness factor of "evil". 
--Finally set the new minion to be a servant of the villain. Print appropriate messages after each operation.
--          Input
--  The input comes in two lines:
--  •	 On the first line, you will receive the minion information in the format "Minion: <Name> <Age> <TownName>"
--  •	 On the second – the villain information in the format "Villain: <Name>"
--          Output
--  After completing an operation, you must print one of the following messages:
--  •  After adding a new town to the database: "Town <TownName> was added to the database."
--  •	 After adding a new villain to the database: "Villain <VillainName> was added to the database."
--  •	 Finally, after successfully adding the minion to the database and making it a servant of a villain: "Successfully added <MinionName> to be minion of <VillainName>."
-- *Bonus task: Make sure all operations are executed successfully. In case of an error do not change the database.

USE MinionsDB;

--Problem 04
SELECT Id FROM Villains WHERE Name = @Name
SELECT Id FROM Minions WHERE Name = @Name
INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@MinionId, @VillainId)
INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)
INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)
INSERT INTO Towns (Name) VALUES (@townName)
SELECT Id FROM Towns WHERE Name = @townName