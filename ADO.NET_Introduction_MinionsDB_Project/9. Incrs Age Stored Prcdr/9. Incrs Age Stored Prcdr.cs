using ConnectionString;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _9.Incrs_Age_Stored_Prcdr
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "9. Increase Age Stored Procedure ";
         //Create stored procedure usp_GetOlder (directly in the database using Management Studio or any other similar tool) 
         //that receives MinionId and increases that minion’s age by 1.
         //Write a program that uses that stored procedure to increase the age of a minion whose id will be given as input from the console.
         //After that print the name and the age of that minion.
         ///-----Решена - но update-ва само по един миньон а трябва по няколко

         SqlConnection SQLconnection;
         SqlCommand SQLcommand;
         string selectALL = "SELECT Id, Name, Age FROM Minions";
         string updateMinionsCommand;
         int affectedRows = 0;
         List<int> minionsIDsList = new List<int>();
         //-----------
         Console.WriteLine(" Increase Age Stored Procedure. Select Minions IDs.");
         PrintAllMinions(selectALL, "Server=GeorgiAcerV3;Database=MinionsDB;Integrated Security=true;");
         Console.Write("Input: ");
         //-----------
         char[] separators = new char[] { ' ', '.' };
         string[] minionsIDs_Console = Console.ReadLine().Split(separators, StringSplitOptions.RemoveEmptyEntries);
         foreach (var item in minionsIDs_Console) { minionsIDsList.Add(Convert.ToInt32(item)); } //---Слагане на запетайки слез всяко ID---

         //------------------------------------------------The logic of the problem----------------------------------------
         string dropProcedure_Command = "IF EXISTS(SELECT * FROM sys.procedures WHERE name = 'usp_GetOlder') DROP PROCEDURE usp_GetOlder";
         string createProcedure_command = @"CREATE PROCEDURE usp_GetOlder @MinionId INT   AS   UPDATE Minions SET Age += 1 WHERE Id = @MinionId";

         ///"SQLconnectionStr.connectionStr" е в отелна програма с допълнително направен клас "public static class SQLconnectionStr(){...}". Добавя се с "references"
         using (SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
         {
            SQLconnection.Open();

            //---- DROP процедура
            using (SQLcommand = new SqlCommand(dropProcedure_Command, SQLconnection))
            {
               SQLcommand.ExecuteNonQuery();
            }

            //---- CREATE процедура
            using (SQLcommand = new SqlCommand(createProcedure_command, SQLconnection))
            {
               SQLcommand.ExecuteNonQuery();
            }

            //---- UPDATE-тва годините на миньоните
                        for (int i = 0; i < minionsIDsList.Count; i++)
            {
               updateMinionsCommand = "EXEC usp_GetOlder @MinionId = @ChoicedMinionId";
               using (SQLcommand = new SqlCommand(updateMinionsCommand, SQLconnection))
               {
                  SQLcommand.Parameters.AddWithValue("@ChoicedMinionId", minionsIDsList[i]);
                if(SQLcommand.ExecuteNonQuery() > 0) affectedRows++;
               }
            }
            Console.WriteLine($"Afected rows {affectedRows} \n");
         }

         //----------------------------------------------------------------------------------------
         PrintAllMinions(selectALL, "Server=GeorgiAcerV3;Database=MinionsDB;Integrated Security=true;");
         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
      private static void PrintAllMinions(string selectALL, string SQLconnectionStr)
      {
         SqlCommand SQLcommand;
         SqlDataReader SQLreader;
         SqlConnection SQLconnection;

         using (SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            SQLconnection.Open();
            //----Принтира миньоните
            SQLcommand = new SqlCommand(selectALL, SQLconnection);
            SQLreader = SQLcommand.ExecuteReader();
            if (SQLreader.Read())
            {
               Console.WriteLine(" | {0,-3}| {1,-10}| {2,-4}|", SQLreader[0], SQLreader[1], SQLreader[2]);
               while (SQLreader.Read())
               {
                  Console.WriteLine(" | {0,-3}| {1,-10}| {2,-4}|", SQLreader[0], SQLreader[1], SQLreader[2]);
               }
            }
            else { Console.WriteLine("No such minions Id!"); }
         }
         Console.WriteLine();
      }
   }
}
