using ConnectionString;
using System;
using System.Data.SqlClient;

namespace _5.Change_Town_Names_Casing
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "5. Change Town Names Casing - ADO.NET";

         // 5.Change Town Names Casing
         // Write a program that changes all town names to uppercase for a given country.
         // You will receive one line of input with the name of the country.
         // Print the number of towns that were changed in the format "<ChangedTownsCount> town names were affected.".
         // On a second line, print the names that were changed, separated with a comma and a space.
         // If no towns were affected(the country does not exist in the database or has no cities connected to it), print "No town names were affected.".
         // ------ Решена ------

         string selectALLCountries = "SELECT * FROM MinionsDB.dbo.Countries";
         string SQLcommandStr_SELECT_Аffected = "SELECT Towns.Name FROM MinionsDB.dbo.Towns JOIN MinionsDB.dbo.Countries ON Countries.Id = Towns.CountryCode WHERE Countries.Name = @countryName";
         string SQLcommandStr_UPPER = "UPDATE MinionsDB.dbo.Towns SET Name = UPPER(Name) WHERE CountryCode = (SELECT Id FROM MinionsDB.dbo.Countries WHERE Name = @countryName)";

         SqlCommand SQLcommand;
         SqlDataReader SQLreader;
         SqlConnection SQLconnection;

         ///"SQLconnectionStr.connectionStr" е в отелна програма с допълнително направен клас "public static class SQLconnectionStr(){...}". Добавя се с "references"
         using (SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
         {
            SQLconnection.Open();

            //-------------- Принтиране на държавите за по лесно избиране на държава -------
            SQLcommand = new SqlCommand(selectALLCountries, SQLconnection);
            SQLreader = SQLcommand.ExecuteReader();
            while (SQLreader.Read()) { Console.WriteLine(" {0}", SQLreader[1]); }
            Console.Write("\n Set towns Uppercase - choice country: ");
            string countryInput = Console.ReadLine();

            //-------------- UPDATE UPPER на избраните думи -------
            SQLreader.Close();
            SQLcommand = new SqlCommand(SQLcommandStr_UPPER, SQLconnection);
            SQLcommand.Parameters.AddWithValue("@countryName", countryInput);

            int affectedRows = Convert.ToInt32(SQLcommand.ExecuteNonQuery());//--- Показва съобщението за броя на засегнати колони

            if (affectedRows > 0)
            {
               Console.WriteLine("\n " + affectedRows + " town names were affected.");

               //-------------- SELECT на променените думи -------
               SQLcommand = new SqlCommand(SQLcommandStr_SELECT_Аffected, SQLconnection);
               SQLcommand.Parameters.AddWithValue("@countryName", countryInput);
               SQLreader = SQLcommand.ExecuteReader();

               Console.Write(" Town: ");
               while (SQLreader.Read()) { Console.Write("'{0, -5}' ", SQLreader[0]); }

               Console.WriteLine();
            }
            else { Console.WriteLine(" No town names were affected."); }
         }
         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
   }
}
