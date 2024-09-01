-- 1. Initial Setup
-- Write a program that connects to your localhost server. 
-- Create new database called MinionsDB where we will keep information about our minions and villains.
-- For each minion we should keep information about its name, age and town. 
-- Each town has information about the country where it’s located. 
-- Villains have name and evilness factor (super good, good, bad, evil, super evil). 
-- Each minion can serve several villains and each villain can have several minions serving him. 
-- Fill all tables with at least 5 records each.
-- In the end you shoud have the following tables:
--    Countries
--    Towns
--    Minions
--    EvilnessFactors
--    Villains
--    MinionsVillains

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MinionsDB')DROP DATABASE MinionsDB;
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MinionsDB')CREATE DATABASE MinionsDB;
ALTER AUTHORIZATION ON DATABASE::MinionsDB TO GeorgiV;
USE MinionsDB;

CREATE TABLE Countries(id INT PRIMARY KEY IDENTITY, Name VARCHAR(50));

CREATE TABLE Towns(id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), CountryCode INT);

CREATE TABLE Minions (id INT PRIMARY KEY IDENTITY, Name VARCHAR(100), Age DECIMAL(4,1), Townid INT);

CREATE TABLE EvilnessFactors(id INT PRIMARY KEY IDENTITY, Name VARCHAR(50));

CREATE TABLE Villains(id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorid INT);

CREATE TABLE MinionsVillains(Minionid INT, Villainid INT, PRIMARY KEY(Minionid, Villainid));


ALTER TABLE Towns ADD CONSTRAINT fk_Towns_Countries FOREIGN KEY (CountryCode) REFERENCES Countries(id);
ALTER TABLE Minions ADD CONSTRAINT fk_Minions_Towns FOREIGN KEY (Townid) REFERENCES Towns(id);
ALTER TABLE MinionsVillains ADD CONSTRAINT fk_MinionsVillains_Minions FOREIGN KEY (Minionid) REFERENCES Minions(id);
ALTER TABLE MinionsVillains ADD CONSTRAINT fk_MinionsVillains_Villains FOREIGN KEY (Villainid) REFERENCES Villains(id);
ALTER TABLE Villains ADD CONSTRAINT fk_Villains_EvilnessFactors FOREIGN KEY (EvilnessFactorid) REFERENCES EvilnessFactors(id);


INSERT INTO Countries ([Name]) 
VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway');

INSERT INTO Towns ([Name], CountryCode) 
VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4);

INSERT INTO Minions (Name,Age, TownId) 
VALUES('Bob', 42, 3),('Kevin', 1, 1),('Steward ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1);

INSERT INTO EvilnessFactors (Name) 
VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil');

INSERT INTO Villains (Name, EvilnessFactorId) 
VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2),('Victor Jr.', 5);

INSERT INTO MinionsVillains (MinionId, VillainId)
 VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7);