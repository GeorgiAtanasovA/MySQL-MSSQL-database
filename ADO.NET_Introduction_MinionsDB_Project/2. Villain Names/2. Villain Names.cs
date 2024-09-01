using System;
using System.Data.SqlClient;

namespace _2.Villain_Names
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "2. Villain Names - ADO.NET";

         //   2.Villain Names
         //Write a program that prints on the console all villains’ names and their number of minions of those who have more than 3 minions ordered descending by number of minions.
         //------- Решена --------

         string SQLconnectionStr = @"Server=GeorgiAcerV3;Database=;Integrated Security=true";
         string SQLcommandStr = @"SELECT Name, COUNT(VillainId) AS 'Count'
                               FROM MinionsDB.dbo.Villains
                               JOIN MinionsDB.dbo.MinionsVillains ON Id = VillainId
                               GROUP BY Id, Name
                               HAVING COUNT(VillainId) >= 3
                               ORDER BY COUNT(VillainId) DESC";

         SqlConnection SQLconnection;
         SqlCommand SQLcommand;
         SqlDataReader SQLreader;

         using (SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            SQLconnection.Open();

            using (SQLcommand = new SqlCommand(SQLcommandStr, SQLconnection))
            {
               SQLreader = SQLcommand.ExecuteReader();
               Console.WriteLine(new string('-', 22));
               Console.WriteLine("| {0,-10} | {1,-5} |", SQLreader.GetName(0), SQLreader.GetName(1));
               Console.WriteLine(new string('-', 22));

               while (SQLreader.Read())
               {
                  Console.WriteLine("| {0,-10} | {1,-5} |", SQLreader[0], SQLreader[1]);
               }
            }
         }
         Console.WriteLine(new string('-', 22));
         Console.ReadLine();
      }
   }
}
