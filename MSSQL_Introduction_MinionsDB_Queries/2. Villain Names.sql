--2. Villain Names
--Write a program that prints on the console all villains’ names and their number of minions of those who have more than 3 minions ordered descending by number of minions.

USE MinionsDB;

SELECT Name, COUNT(VillainId) 
FROM Villains AS v 
JOIN MinionsVillains ON v.Id = VillainId 
GROUP BY Id, Name 
HAVING COUNT(VillainId) >= 3 
ORDER BY COUNT(VillainId) DESC;
