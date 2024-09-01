using ConnectionString;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _7.Print_All_Minion_Names
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "7. Print All Minion Names";

         //7. Print All Minion Names
         //Write a program that prints all minion names from the minions table in the following order:
         //first record, last record, first +1, last - 1, first + 2, last - 2 … first + n, last - n.
         //1 10 2 9 3 8 4 7 5 6
       

         //           Example
         //  ----------------- ---------
         // | Original Order | | Output |
         // ------------------ --------- 
         // | Bob            | | Bob    |
         // | Kevin          | | Jully  |
         // | Steward        | | Kevin  |
         // | Jimmy          | | Becky  |
         // | Vicky          | | Stewar |
         // | Becky          | | Vicky  |
         // | Jully          | | Jimmy  |
         //  ----------------- ---------

         List<string> minionsNames = new List<string>();

         using (SqlConnection SQLconnection = new SqlConnection(SQLconnectionStr.connectionStr))
         {
            SQLconnection.Open();
            using (SqlCommand SQLcommand = new SqlCommand("SELECT Name FROM Minions", SQLconnection))
            {
               using (SqlDataReader SQLreader = SQLcommand.ExecuteReader())
               {
                  int i = 0;
                  while (SQLreader.Read())
                  {
                     i++;
                     minionsNames.Add(i + "  " + (string)SQLreader[0]);//----- Записват се всички миньони в листа за обработка и принитране
                  }
               }
            }
         }
         Console.WriteLine(" Old Order!");
         foreach (var item in minionsNames) { Console.WriteLine("  " + item); }
         Console.WriteLine("------------");
         Console.WriteLine(" New Order!");

         //--------------------------------------------------------------------
         for (int i = 0; i < minionsNames.Count / 2; i++) // Тук принтира когато СА четен брой записи в таблица
         {
            Console.WriteLine("  " + minionsNames[i]);
            Console.WriteLine("  " + minionsNames[minionsNames.Count - 1 - i]);
         }
         if (minionsNames.Count % 2 != 0)// Тук принтира когато НЕ са четен брой записи в таблица
         {
            Console.WriteLine("  " + minionsNames[minionsNames.Count / 2]);
         }
         //--------------------------------------------------------------------

         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
   }
}
