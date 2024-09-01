--3. Minion Names
--Write a program that prints on the console all minion names and age for a given villain id, ordered by name alphabetically.
--If there is no villain with the given ID, print "No villain with ID <VillainId> exists in the database.".
--If the selected villain has no minions, print "(no minions)" on the second row.

USE MinionsDB;
SELECT Id, Name, Age FROM Minions;
SELECT * FROM MinionsVillains ORDER BY VillainId;
SELECT Id, Name, EvilnessFactorId FROM Villains;



SELECT
	  Villains.Name  AS 'Villain', 
     MinionsVillains.MinionId,
     Minions.Name AS 'Minion', 
     Minions.Age AS 'MinionAge'
FROM MinionsDB.dbo.MinionsVillains
LEFT JOIN MinionsDB.dbo.Minions ON MinionsVillains.MinionId = Minions.Id
RIGHT JOIN MinionsDB.dbo.Villains ON MinionsVillains.VillainId =  Villains.Id
WHERE Villains.Id = 7
ORDER BY 'Villain'