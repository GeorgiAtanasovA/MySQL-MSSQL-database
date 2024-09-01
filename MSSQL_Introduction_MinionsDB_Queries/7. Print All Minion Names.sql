--7. Print All Minion Names
--Write a program that prints all minion names from the minions table in the following order: 
--first record, last record, first + 1, last - 1, first + 2, last - 2 … first + n, last - n. 
--1 10 2	9 3 8	4 7 5 6

--           Example
-- ------------------ ----------
-- | Original Order | | Output |
-- ------------------ ----------
-- |  Bob           | | Bob	 |
-- |  Kevin         | | Jully  |
-- |  Steward       | | Kevin  |
-- |  Jimmy         | | Becky  |
-- |  Vicky         | | Stewar |
-- |  Becky         | | Vicky  |
-- |  Jully         | | Jimmy  |    
-- -----------------  ----------

USE MinionsDB;

SELECT * FROM Minions;

SELECT m1.Id, m2.Id
FROM Minions AS m1, 
     Minions AS m2 
where m1.Id + 1 = m2.Id


