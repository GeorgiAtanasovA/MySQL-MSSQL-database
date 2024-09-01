using ConnectionString;
using System;
using System.Data.SqlClient;

namespace _4.Add_Minion
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "4. Add Minion - ADO.NET";


         // 4.Add Minion
         // Write a program that reads information about a minion and its villain and adds it to the database. 
         // In case the town of the minion is not in the database, insert it as well.
         // In case the villain is not present in the database, add him too with a default evilness factor of "evil".
         // Finally set the new minion to be a servant of the villain.Print appropriate messages after each operation.
         // Input
         //   The input comes in two lines:
         //   •	 On the first line, you will receive the minion information in the format "Minion: <Name> <Age> <TownName>"
         //   •	 On the second – the villain information in the format "Villain: <Name>"
         //           Output
         //   After completing an operation, you must print one of the following messages:
         //   •    After adding a new town to the database: "Town <TownName> was added to the database."
         //   •	 After adding a new villain to the database: "Villain <VillainName> was added to the database."
         //   •	 Finally, after successfully adding the minion to the database and making it a servant of a villain: "Successfully added <MinionName> to be minion of <VillainName>."
         /// * Bonus task: Make sure all operations are executed successfully. In case of an error do not change the database. -- Не е решено
       

         Console.WriteLine("Write the minion's data and its villain's name!");
         Console.WriteLine("        Name  Age  TownName");


         Console.Write("Minion: ");
         char[] separators = new char[] { ' ', '.' };
         string[] inputMinionData = Console.ReadLine().Split(separators, StringSplitOptions.RemoveEmptyEntries);
         if (inputMinionData.Length < 3) { Console.WriteLine("Данните за миньона не са пълни!"); Console.ReadLine(); return; }
         Console.Write("Villain: ");
         string inputVillainName = Console.ReadLine();

         Console.WriteLine();

         SqlConnection SQLconnection;
         SqlCommand SQLcommand;
         SqlDataReader SQLreader;

         string name = inputMinionData[0];
         string age = inputMinionData[1];
         string townName = inputMinionData[2];
         int townId = -1;

         string if_MinionExists_commandStr = "SELECT * FROM Minions WHERE Name = '" + inputMinionData[0] + "'";// На тази поз.е името на Миньона
         string if_TownExists_commandStr = "SELECT * FROM Towns WHERE Name = '" + inputMinionData[2] + "'";// На тази поз.е името на града
         string if_VillainExists_commandStr = "SELECT * FROM Villains WHERE Name = '" + inputVillainName + "'"; //Това е по-правилно но трябва да е с параметар - има в следващите задачи
         string if_VillainExistс_commandStr1 = $"SELECT * FROM Villains WHERE Name = '{inputVillainName}'"; ///--- Това е SQL Injection - това е ГРЕШНО да се прави ----

         ///"SQLconnectionStr.connectionStr" е в отелна програма с допълнително направен клас "public static class SQLconnectionStr(){...}". Добавя се с "references"
         using (SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
         {
            SQLconnection.Open();

            SqlTransaction sqltransaction = SQLconnection.BeginTransaction();

            SQLcommand = new SqlCommand(if_MinionExists_commandStr, SQLconnection);
            SQLcommand.Transaction = sqltransaction;
            int? if_MinionExists = (int?)SQLcommand.ExecuteScalar();

            //------------------------------------//Проверка - Дали МИНЬОНА е в базата данни
            if (if_MinionExists != null)
            {
               Console.WriteLine("Миньона '{0}' е в базата данни.", inputVillainName);
            }
            else
            {
               // Ако миньона не е в базата данни трябва да го добави, но преди това проверява дали града и злодея са в базат данни и ако не са ги добавя и взима id-то на града
               //---------------------------------//Проверка - Дали ГРАДА е в базата данни
               SQLcommand = new SqlCommand(if_TownExists_commandStr, SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               int? if_TownExists = (int?)SQLcommand.ExecuteScalar(); ///Задаване на "INT" да приема "NULL" стойности
               SQLreader = SQLcommand.ExecuteReader();
               SQLreader.Read();

               if (if_TownExists != null)
               {
                  Console.WriteLine("Град '{0}' е в базата данни.", SQLreader[1]); SQLreader.Close();

                  //Ако града е в базата данни ще му вземе Id-то
                  //------- Взимане ID-то на града за INSERT с миньона в базата данни -------
                  SQLcommand = new SqlCommand("SELECT Id FROM Towns WHERE Name =" + "'" + townName + "'", SQLconnection);
                  SQLcommand.Transaction = sqltransaction;
                  SQLreader = SQLcommand.ExecuteReader();
                  SQLreader.Read();
                  townId = Convert.ToInt32(SQLreader[0]);
                  SQLreader.Close();
               }
               else
               {
                  SQLreader.Close();
                  //Ако града не е в базата данни ще го INSERT-не и ще му вземе Id-то 
                  SQLcommand = new SqlCommand("INSERT INTO Towns(Name) VALUES(" + "'" + townName + "'" + ")", SQLconnection);
                  SQLcommand.Transaction = sqltransaction;
                  SQLcommand.ExecuteNonQuery();
                  Console.WriteLine("Град '" + townName + "' беше добавен в базата данни.");

                  //------- Взимане ID-то на града за INSERT с миньона в DB -------
                  SQLcommand = new SqlCommand("SELECT Id FROM Towns WHERE Name =" + "'" + townName + "'", SQLconnection);
                  SQLcommand.Transaction = sqltransaction;
                  SQLreader = SQLcommand.ExecuteReader();
                  SQLreader.Read();
                  townId = Convert.ToInt32(SQLreader[0]);
                  SQLreader.Close();
               }

               //------------------------------------ //Проверка - Дали ЗЛОДЕЯ е в базата данни
               SQLcommand = new SqlCommand(if_VillainExists_commandStr, SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               SQLreader = SQLcommand.ExecuteReader();

               if (SQLreader.Read())
               {
                  Console.WriteLine($"Злодея '{SQLreader[1]}' е в базата данни."); SQLreader.Close();
               }
               else
               {
                  //------------------------------------ INSERT-ване на злодей
                  SQLreader.Close();
                  SQLcommand = new SqlCommand("INSERT INTO Villains(Name, EvilnessFactorId) VALUES(" + "'" + inputVillainName + "', " + 4 + ")", SQLconnection);
                  SQLcommand.Transaction = sqltransaction;
                  SQLcommand.ExecuteNonQuery();
                  Console.WriteLine("Злодея '" + inputVillainName + "' беше добавен в базата данни.");
               }

               //------------------------------------ INSERT-ване на миньон
               SQLcommand = new SqlCommand("INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age,  @townId)", SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               SQLcommand.Parameters.AddWithValue("@name", name);
               SQLcommand.Parameters.AddWithValue("@age", age);
               SQLcommand.Parameters.AddWithValue("@townId", townId);
               SQLcommand.ExecuteNonQuery();
               Console.WriteLine("Миньона '{0}' беше добавен в базата данни.", name);

               //------------------------------------ INSERT-ване в мапващата таблица(миньон служещ на злодей) и взимане Id-тата на миньона и злодея
               SQLcommand = new SqlCommand("SELECT Id FROM Villains WHERE Name = @VillainName", SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               SQLcommand.Parameters.AddWithValue("@VillainName", inputVillainName);
               SQLreader = SQLcommand.ExecuteReader();
               string VillainId = "???";
               if (SQLreader.Read()) { VillainId = Convert.ToString(SQLreader[0]); }
               SQLreader.Close();

               //------------------------------------ Взимане Id-то на миньона 
               SQLcommand = new SqlCommand("SELECT Id FROM Minions WHERE Name = @MinionName", SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               SQLcommand.Parameters.AddWithValue("@MinionName", name);
               SQLreader = SQLcommand.ExecuteReader();
               SQLreader.Read();
               string MinionId = Convert.ToString(SQLreader[0]);
               SQLreader.Close();

               //------------------------------------ Взимане Id-то на злодея
               SQLcommand = new SqlCommand("INSERT INTO MinionsVillains(MinionId, VillainId) VALUES (@MinionId, @VillainId)", SQLconnection);
               SQLcommand.Transaction = sqltransaction;
               SQLcommand.Parameters.AddWithValue("@MinionId", MinionId);
               SQLcommand.Parameters.AddWithValue("@VillainId", VillainId);
               SQLcommand.ExecuteNonQuery();

               Console.WriteLine("Успешно беше добавен '{0}' да бъде миньон на '{1}'.", name, inputVillainName);
            }
         }
         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
   }
}
