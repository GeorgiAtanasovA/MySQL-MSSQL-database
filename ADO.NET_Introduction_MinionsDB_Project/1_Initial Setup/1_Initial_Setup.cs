using System;
using System.Data.SqlClient;
using System.Threading;

namespace _1_Initial_Setup
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "1. Initial Setup - ADO.NET";

         //   1.Initial Setup
         //Write a program that connects to your localhost server. 
         //Create new database called MinionsDB where we will keep information about our minions and villains.
         //For each minion we should keep information about its name, age and town. 
         //Each town has information about the country where it’s located. 
         //Villains have name and evilness factor (super good, good, bad, evil, super evil).
         //Each minion can serve several villains and each villain can have several minions serving him.
         //Fill all tables with at least 5 records each.
         //    In the end you shoud have the following tables:
         //     Countries
         //     Towns
         //     Minions
         //     EvilnessFactors
         //     Villains
         //     MinionsVillains
       

         string ifDB_ExistsDrop = @"IF EXISTS(SELECT name FROM sys.databases WHERE name = 'MinionsDB') DROP DATABASE MinionsDB ";

         string ifDB_NotExistsCreate = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MinionsDB') CREATE DATABASE MinionsDB";

         string authorizationStr = "ALTER AUTHORIZATION ON DATABASE::MinionsDB TO Georgi_V";

         string createTablesStr = @"USE MinionsDB
                                    CREATE TABLE Countries(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50)) 
                                    CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))
                                    CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))
                                    CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))
                                    CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))
                                    CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))";

         string insertIntoTables = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')
                                     INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 4),('Frankfurt', 4),('Oslo', 5)
                                     INSERT INTO Minions (Name, Age, TownId) VALUES('Bob', 13, 3),('Kevin', 14, 1),('Steward', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2), ('Carry ', 50, 10),('Becky',125,5),('Mars',21,1), ('Misho',5,10),('Zoe', 125, 5),('Json', 21, 1)
                                     INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')
                                     INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2),('Victor Jr.',5)
                                     INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(11,5),(8,4),(9,7),(7,1),(5,3),(4,3),(2,1)";

         using (SqlConnection connection = new SqlConnection("Server=GeorgiAcerV3;Database=;Integrated Security=true"))
         {
            try
            {
               Console.Write(" Opening connection");
               connection.Open();
               Console.WriteLine(" - done - \n");

               using (SqlCommand command = new SqlCommand(ifDB_ExistsDrop, connection))
               {
                  command.ExecuteNonQuery();
                  Console.WriteLine(" Drop DB If Exists - done");
                  Thread.Sleep(500);
               }
               using (SqlCommand command = new SqlCommand(ifDB_NotExistsCreate, connection))
               {
                  command.ExecuteNonQuery();
                  Console.WriteLine(" Create Database - done");
                  Thread.Sleep(500);
               }
               using (SqlCommand command = new SqlCommand(createTablesStr, connection))
               {
                  command.ExecuteNonQuery();
                  Console.WriteLine(" Create Tables - done");
                  Thread.Sleep(500);
               }
               using (SqlCommand command = new SqlCommand(insertIntoTables, connection))
               {
                  command.ExecuteNonQuery();
                  Console.WriteLine(" Insert Data Into Tables - done");
                  Thread.Sleep(500);
               }
               using (SqlCommand command = new SqlCommand(authorizationStr, connection))
               {
                  command.ExecuteNonQuery();
                  Console.WriteLine(" Authorization User - done");
                  Thread.Sleep(500);
               }
            }
            catch (Exception ex)
            {
               Console.WriteLine("\n" + ex.Message);
            }
         }
         Console.WriteLine("\n--- Done ---");
         Console.ReadLine();
      }
   }
}
