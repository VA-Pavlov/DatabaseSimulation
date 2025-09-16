using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSimulation
{
    public class Query
    {
        public static void ExecuteQuery(Table table, string query)
        {
            switch(query.Split(' ')[0].ToUpper()) 
            {
                case "SELECT":
                    Console.WriteLine(table);
                    break;
                case "UPDATE":

                    break;
                case "DELETE":
                    break;
                case "INSERT":
                    break;
                default:
                    Console.WriteLine("Некорректный запрос");
                    break;
            }
        }

        
    }
}
