using System;
using System.Data.SqlClient;

namespace my_first_SQL_program
{
   class my_first_SQL_program
   {
      static void Main(string[] args)
      {
         Console.Title = "My First C#-DB-SQL Connection Program - ADO.NET";

         // ------ Моята първа програма за връзка с база данни ----- Това е ADO.NET програма
         Console.WriteLine("Моята първа програма за връзка с база данни");
         Console.WriteLine("SELECT * FROM Minions; - команда");

         string connectStr = @"Server=GeorgiAcerV3;Database=;Integrated Security=true";
         string commandStr = @"SELECT * FROM MinionsDB.dbo.Minions";

         SqlConnection my_first_SQL_connection;
         SqlCommand my_first_SQL_command;
         SqlDataReader my_first_SQL_DataReader;

         // Select records
         using (my_first_SQL_connection = new SqlConnection(connectStr))
         {
            my_first_SQL_connection.Open();

            using (my_first_SQL_command = new SqlCommand(commandStr, my_first_SQL_connection))
            {
               my_first_SQL_DataReader = my_first_SQL_command.ExecuteReader();

               Console.WriteLine(new string('-', 34));
               // Прочита имената на колоните
               Console.WriteLine("| {0,-3}| {1,-9}| {2,-6}| {3,-7}|",
                                 my_first_SQL_DataReader.GetName(0),
                                 my_first_SQL_DataReader.GetName(1),
                                 my_first_SQL_DataReader.GetName(2),
                                 my_first_SQL_DataReader.GetName(3));
               Console.WriteLine(new string('-', 34));

               // Отпечатване на съдаржанието на колоните
               // Reader-а стига до края на колоната, връща false и спира
               while (my_first_SQL_DataReader.Read())
               {
                  Console.WriteLine("| {0,-3}| {1,-9}| {2,-6}| {3,-7}|",
                                     my_first_SQL_DataReader[0],
                                     my_first_SQL_DataReader[1],
                                     my_first_SQL_DataReader[2],
                                     my_first_SQL_DataReader[3]);
               }
            }
         }
         Console.WriteLine(("").PadRight(34, '-'));
         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
   }
}
