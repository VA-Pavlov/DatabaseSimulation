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
                    Delete(query.Split(' ')[2]);
                    break;
                case "INSERT":
                    break;
                default:
                    Console.WriteLine("Некорректный запрос");
                    break;
            }
        }

        private static void Delete(string expression) //id!=34 id>5
        {
            
            var sign = "";
            var number = "";
            for (int i = 2;i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]))
                    number += expression[i];
                else
                    sign += expression[i];
            }
            switch (sign)
            {
                case "=":
                    break;
                case "!=":
                    break;
                case ">":
                    break;
                case "<":
                    break;
                case "<=":
                    break;
                case ">=":
                    break;
                case "like":
                    break;
                default:
                    Console.WriteLine("Некорректный запрос в блоке WHERE");
                    break;
            }
        }
    }
}
