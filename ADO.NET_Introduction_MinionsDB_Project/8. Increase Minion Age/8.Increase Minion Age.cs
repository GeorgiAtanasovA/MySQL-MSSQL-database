using ConnectionString;
using System;
using System.Data.SqlClient;

namespace _8.Increase_Minion_Age
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "8. Increase Minion Age";
         //  8. Increase Minion Age
         //Read from the console minion IDs separated by space.Increment the age of those minions by 1 and make their names title case. 
         //Finally, print the name and the age of all minions in the database, each on a new row in format "<Name> <Age>".
         // ------------- Решена -------------

         Console.WriteLine(" Increase Minion Age. Select Minions IDs.");
         PrintAllMinions("SELECT Id, Name, Age FROM Minions", SQLconnectionStr.connectionStr);
         Console.Write("Input: ");

         char[] separators = new char[] { ' ', '.' };
         string[] minionsIDs_Console = Console.ReadLine().Split(separators, StringSplitOptions.RemoveEmptyEntries);
         string minionsIDsCommaSepar = "";
         //Слагане на запетайки слез всяко ID
         for (int i = 0; i < minionsIDs_Console.Length; i++)
         {
            if (i == minionsIDs_Console.Length - 1) { minionsIDsCommaSepar += minionsIDs_Console[i]; }
            else { minionsIDsCommaSepar += minionsIDs_Console[i] + ","; }
         }

         //----------------------------------------------------------------------
         SqlCommand SQLcommand;
         SqlConnection SQLconnection;

         string selectALL = "SELECT Id, Name, Age FROM Minions WHERE Id IN (" + minionsIDsCommaSepar + ")";
         string updateSelectedMinions = "UPDATE Minions SET Name = UPPER(Name), Age += 1 WHERE Id IN (" + minionsIDsCommaSepar + ")";

         using (SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
         {
            SQLconnection.Open();

            //---- UPDATE-тва миньоните
            SQLcommand = new SqlCommand(updateSelectedMinions, SQLconnection);
            SQLcommand.ExecuteNonQuery();
            SQLcommand.Dispose();
         }
         PrintAllMinions(selectALL, SQLconnectionStr.connectionStr);
         //----------------------------------------------------------------------


         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
      private static void PrintAllMinions(string selectALL, string connectionStr)
      {
         SqlConnection SQLconnection;
         SqlCommand SQLcommand;
         SqlDataReader SQLreader;

         using (SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
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
