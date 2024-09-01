using System;
using System.Data.SqlClient;

namespace _3.Minion_Names
{
   class Program
   {
      static void Main(string[] args)
      {
         object obj = 11;
         //string str1 = (string)obj; //Не работи с INT
         //string str2 = obj.ToString(); //Не работи с NULL
         string str3 = Convert.ToString(obj); //Това е най-добрия метод
         // Кастване и конвертиране към string - не е към задачата



         Console.Title = "3. Minion Names - ADO.NET";
         //   3.Minion Names
         //Write a program that prints on the console all minion names and age for a given villain id, ordered by name alphabetically.
         //If there is no villain with the given ID, print "No villain with ID <VillainId> exists in the database".
         //If the selected villain has no minions, print "(no minions)" on the second row.


         Console.WriteLine(" Who's are the minions of the villain. Choice Villain IDs.");
         PrintAllMinions("SELECT Id, Name FROM Villains", "Server=GeorgiAcerV3;Database=MinionsDB;Integrated Security=true;");
         Console.Write("Input: ");

         int VillainIdParameter = int.Parse(Console.ReadLine());
         string SQLconnectionStr = @"Server=GeorgiAcerV3;Database=;Integrated Security=true";
         string SQLcommandStr = @"SELECT 
                               	  Villains.Name  AS 'Villain', 
                                    MinionsVillains.MinionId,
                                    Minions.Name AS 'Minion', 
                                    Minions.Age AS 'MinionAge'
                               FROM MinionsDB.dbo.MinionsVillains
                               RIGHT JOIN MinionsDB.dbo.Minions ON MinionsVillains.MinionId = Minions.Id
                               RIGHT JOIN MinionsDB.dbo.Villains ON MinionsVillains.VillainId =  Villains.Id
                               WHERE Villains.Id = " + Convert.ToString(VillainIdParameter) +
                              "ORDER BY 'Villain'";

         using (SqlConnection SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            Console.Write("Open connection");
            SQLconnection.Open();
            Console.WriteLine(" - done\n");

            using (SqlCommand SQLcommand = new SqlCommand(SQLcommandStr, SQLconnection))
            {
               SqlDataReader SQLreader = SQLcommand.ExecuteReader();
               try
               {
                  SQLreader.Read();
                  int tiretaLength = (SQLreader.GetName(0) + ": " + SQLreader[0]).Length;
                  Console.WriteLine(SQLreader.GetName(0) + ": " + SQLreader[0]);
                  Console.WriteLine(new string('-', tiretaLength));
                  SQLreader.Close();

                  SQLreader = SQLcommand.ExecuteReader();
                  while (SQLreader.Read())
                  {
                     if (SQLreader[1].ToString() == "") { Console.WriteLine("(no minions)"); }
                     else { Console.WriteLine("{0}. {1} {2}", SQLreader[1], SQLreader[2], SQLreader[3]); }
                  }
               }
               catch (Exception)
               {
                  Console.WriteLine($"No villain with ID '{VillainIdParameter}' exists in the database.");
               }
            }
         }
         Console.WriteLine("\n--- Done ---");
         Console.ReadLine();
      }
      private static void PrintAllMinions(string selectALL, string SQLconnectionStr)
      {
         SqlConnection SQLconnection;
         SqlCommand SQLcommand;
         SqlDataReader SQLreader;

         using (SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            SQLconnection.Open();

            //----Принтира злодеите
            SQLcommand = new SqlCommand(selectALL, SQLconnection);
            SQLreader = SQLcommand.ExecuteReader();

            if (SQLreader.Read())
            {
               Console.WriteLine($" | {SQLreader[0],-3}| {SQLreader[1],-10} |");
               while (SQLreader.Read())
               {
                  Console.WriteLine($" | {SQLreader[0],-3}| { SQLreader[1],-10} |");
               }
            }
            else { Console.WriteLine("No such minions Id!"); }
         }
         Console.WriteLine();
      }
   }
}
