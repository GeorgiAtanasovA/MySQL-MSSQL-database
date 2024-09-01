using System;
using System.Data.SqlClient;

namespace _6.Remove_Villain_trudna_zad
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "6. Remove_Villain - ADO.NET";

         //6.  * Remove Villain
         //Write a program that receives the ID of a villain, deletes him from the database and releases his minions from serving to him.
         //Print on two lines the name of the deleted villain in format "<Name> was deleted." and the number of minions released in format "<MinionCount> minions were released.".
         ///Make sure all operations go as planned, otherwise do not make any changes in the database. --- Не е решено
         //If there is no villain in the database with the given ID, print "No such villain was found.".
        

         string SQLconnectionStr = "Server=GeorgiAcerV3;Database=MinionsDB;Integrated Security=true";

         SqlConnection SQLconnection;
         SqlDataReader SQLreader;
         SqlCommand SQLcommand;

         ///Принтиране на имената на злодеите за по лесно избиране
         PrintVillainsNames(SQLconnectionStr, out SQLconnection, out SQLcommand, out SQLreader);
     
         Console.Write("Choise Villain's ID for delete: ");
         int villainId = int.Parse(Console.ReadLine());

         string command_MinionsOfVillainCount = "SELECT COUNT(*) AS 'MinionsOfVillainCount' FROM MinionsVillains WHERE MinionsVillains.VillainId =" + villainId;
         string command_villainsName = "SELECT Name FROM Villains WHERE Id =" + villainId;
         string command_deleteVillainMapTable = "DELETE FROM MinionsVillains WHERE VillainId = " + villainId;
         string command_deleteVillainVillainsTable = "DELETE FROM Villains WHERE Id = " + villainId;

         int minionsOfVillainCount = 0;
         string villainName = "";


         using (SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            SQLconnection.Open();

            //-----------Името на злодея - проверка-----------
            SQLcommand = new SqlCommand(command_villainsName, SQLconnection);
            SQLreader = SQLcommand.ExecuteReader();
            if (SQLreader.Read())
            {
               //-----------Името на злодея - взимане-----------
               villainName = (string)SQLreader[0];
               SQLreader.Close();

               //-----------Бройката на миньоните -----------
               SQLcommand = new SqlCommand(command_MinionsOfVillainCount, SQLconnection);
               SQLreader = SQLcommand.ExecuteReader();
               SQLreader.Read();
               minionsOfVillainCount = (int)SQLreader[0];
               SQLreader.Close();

               //-----------Изтриване на злодея от мапващата таблица-----------
               SQLcommand = new SqlCommand(command_deleteVillainMapTable, SQLconnection);
               SQLcommand.ExecuteNonQuery();
               SQLreader.Close();

               //-----------Изтриване на злодея от таблица на злодеите-----------
               SQLcommand = new SqlCommand(command_deleteVillainVillainsTable, SQLconnection);
               SQLcommand.ExecuteNonQuery();
               SQLreader.Close();

               Console.WriteLine("{0} Was deleted.", villainName);
               Console.WriteLine("{0} minions were released.\n", minionsOfVillainCount);
            }
            else { Console.WriteLine("No such villain was found.\n"); }
         }
         PrintVillainsNames(SQLconnectionStr, out SQLconnection, out SQLcommand, out SQLreader);

         Console.WriteLine("\n------- Done -------");
         Console.ReadLine();
      }
      private static void PrintVillainsNames(string SQLconnectionStr, out SqlConnection SQLconnection, out SqlCommand SQLcommand, out SqlDataReader SQLreader)
      {
         using (SQLconnection = new SqlConnection(SQLconnectionStr))
         {
            SQLconnection.Open();
            using (SQLcommand = new SqlCommand("SELECT * FROM Villains", SQLconnection))
            {
               SQLreader = SQLcommand.ExecuteReader();
               while (SQLreader.Read())
               {
                  Console.WriteLine("|{0,-3} | {1,-10} | {2,-3}|", SQLreader[0], SQLreader[1], SQLreader[2]);
               }
               Console.WriteLine();
            }
         }
      }
   }
}
