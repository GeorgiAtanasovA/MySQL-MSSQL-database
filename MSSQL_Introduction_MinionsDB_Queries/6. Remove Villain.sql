--6.	*Remove Villain 
--Write a program that receives the ID of a villain, deletes him from the database and releases his minions from serving to him. 
--Print on two lines the name of the deleted villain in format "<Name> was deleted." and the number of minions released in format "<MinionCount> minions were released.". 
--Make sure all operations go as planned, otherwise do not make any changes in the database.
--If there is no villain in the database with the given ID, print "No such villain was found.".

USE MinionsDB;

SELECT Minions.id, Minions.Name, 
       MinionsVillains.MinionId, MinionsVillains.VillainId, 
       Villains.Id, Villains.Name 
FROM Minions
FULL OUTER JOIN MinionsVillains ON Minions.Id = MinionsVillains.MinionId
FULL OUTER JOIN Villains        ON MinionsVillains.VillainId = Villains.Id
WHERE Villains.Id = 2
ORDER BY Minions.Id

SELECT Minions.id, Minions.Name, 
       MinionsVillains.MinionId, MinionsVillains.VillainId
FROM Minions
LEFT JOIN MinionsVillains ON Minions.Id = MinionsVillains.MinionId
WHERE MinionsVillains.VillainId = 1

SELECT Villains.id, Villains.Name, 
       MinionsVillains.MinionId
FROM Villains
LEFT OUTER JOIN MinionsVillains ON Villains.Id = MinionsVillains.VillainId
WHERE Villains.Id = 1

SELECT * FROM MinionsVillains WHERE MinionsVillains.VillainId = 1;
SELECT COUNT(*)AS 'MinionsOfVillainCount' FROM MinionsVillains WHERE MinionsVillains.VillainId = 1;
SELECT * FROM Minions;
SELECT * FROM Villains;
DELETE FROM MinionsVillains WHERE VillainId = 1;
DELETE FROM Villains WHERE Id = 1;

