using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseSimulation
{
    public class Query
    {
        public static void ExecuteQuery(Table table, string query)
        {
            switch (query.Split(' ')[0].ToUpper())
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
        //select * where 'id'>1 and ‘age’>=30 or ‘lastName’ ilike ‘%п%’
        public static void blockWhere(string query, Dictionary<string, Object> row)
        {
            var i = query.ToUpper().IndexOf("WHERE");
            if (i != -1)
            {
                var newLine = query.Substring(i + 5);//'id'>1 and ‘age’>=30 or ‘lastName’ ilike ‘%п%’
                string[] comandArray = newLine.Trim().Split(' ');//{'id'>1|and|‘age’>=30|or|‘lastName’ ilike ‘%п%’}->‘lastName’ilike‘%п%’!!!
            }
        }

        //‘lastName’ ilike ‘%п%’

        //приходит
        //'id'>1                 [][id][>1]
        //'age'>=30              [][age][>=30]  30>='age' [30>=][age][]
        //'lastName'='Федоров'   [][lastName][=][Федоров][]
        //ИТОГ:
        //columnName = id , znak = > , value = 1
        //columnName = age , znak = >= , value = 30
        //columnName = lastName , znak = = , value = Федоров
        public static bool executeVirazenie(string query, Dictionary<string, Object> row)
        {
            //string[] titleColumn = row.Keys.ToArray();
            //string[] simbol = { ">", "<", "=", "!=","<=",">=","LIKE"};

            string columnName = "";
            string znak = "";
            string value = "";
            string[] splitQwery = query.Split('\'');
            if (splitQwery.Length > 3)
            {//[][lastName][=][Федоров][]
                columnName = "lastName";
                znak = splitQwery[2];
                value = splitQwery[1] == "lastName" ? splitQwery[3] : splitQwery[1];
            }
            else
            {//[][id][>1] или [30>=][age][]
                columnName = splitQwery[1];
                int index = 0;
                if (splitQwery[0] == "")//[][id][>1]
                { 
                    index = 2; 
                }
                else//[30>=][age][]
                    index = 0;
                var arZnak = splitQwery[index].Where(z => !char.IsDigit(z));
                var arValue = splitQwery[index].Where(z => char.IsDigit(z));
                znak = string.Join("", arZnak);
                value = string.Join("", arValue);
            }
            Console.WriteLine($"columnName = {columnName} znak = {znak} value = {value}");
            //string nameColumt = query.Split('\'')[1];

            return false;
        }
    }
}
