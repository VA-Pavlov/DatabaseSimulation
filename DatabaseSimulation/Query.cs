using DatabaseSimulation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseSimulation
{
    public class Query
    {
        static string[] simbol = { ">", "<", "=", "!=", "<=", ">=", "LIKE" };
        public static void ExecuteQuery(Table table, string query)
        {
            var rows = table.SqlTable;
            bool flag = query.ToUpper().IndexOf("WHERE") != -1;
            switch (query.Split(' ')[0].ToUpper())
            {
                case "SELECT":
                    if (flag)
                    {
                        foreach(var row in rows)
                        {
                            if (blockWhere(query, row))
                            {
                                Console.WriteLine(string.Join("\t|", row));
                            }
                        }
                    }
                    else
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
        //select * where 'id'>1 and 'age'>=30 or 'lastName' ilike '%п%'
        public static bool blockWhere(string query, Dictionary<string, Object> row)
        {
            var i = query.ToUpper().IndexOf("WHERE");
            if (i != -1)
            {
                var newLine = query.Substring(i + 5);//'id'>1 and ‘age’>=30 or ‘lastName’ ilike ‘%п%’
                List<string> comandArray = newLine.Trim().Split(' ').ToList();//{'id'>1|and|‘age’>=30|or|‘lastName’ like ‘%п%’}->‘lastName’ilike‘%п%’!!!
                connectLikes(comandArray);//'id'>1 | and | 'age'>=30 | or | 'lastName'like'%п%'
                for (int j = 0; j < comandArray.Count; j++)
                {
                    var elemrnt = comandArray[j];
                    if (elemrnt.ToLower() != "and" && elemrnt.ToLower() != "or")
                    {
                        comandArray[j] = executeVirazenie(elemrnt, row) ? "1" : "0";
                    }
                }//0 | and | 1 | or | 0
                while (comandArray.Contains("and") || comandArray.Contains("AND"))
                {
                    for (int index = 0; index < comandArray.Count; index++)
                    {
                        if (comandArray[index].ToLower() == "and")
                        {
                            comandArray[index] = (int.Parse(comandArray[index - 1]) * int.Parse(comandArray[index + 1])).ToString();
                            comandArray.Remove(comandArray[index + 1]);
                            comandArray.Remove(comandArray[index]);
                            index--;
                        }
                    }
                }//0 | or | 0
                while (comandArray.Contains("or") || comandArray.Contains("OR"))
                {
                    for (int index = 0; index < comandArray.Count; index++)
                    {
                        if (comandArray[index].ToLower() == "or")
                        {
                            var z = int.Parse(comandArray[index - 1]);
                            var t = int.Parse(comandArray[index + 1]);
                            comandArray[index] = (z + t).ToString();
                            comandArray.Remove(comandArray[index + 1]);
                            comandArray.Remove(comandArray[index]);
                            index--;
                        }
                    }
                }
                return comandArray[0]=="1";
            }
            return false;

        }

        public static void connectLikes(List<string> comandArray)
        {
            for (int j = 0; j < comandArray.Count; j++)
            {
                var i = comandArray[j].ToUpper().IndexOf("LASTNAME");

                if (i != -1)
                {
                    if (simbol.Contains(comandArray[j - 1].ToUpper()))
                    {
                        comandArray[j] = comandArray[j] + comandArray[j - 1] + comandArray[j - 2];
                        comandArray.Remove(comandArray[j - 1]);
                        comandArray.Remove(comandArray[j - 1]);
                    }
                    else if (simbol.Contains(comandArray[j + 1].ToUpper()))
                    {
                        comandArray[j] = comandArray[j] + comandArray[j + 1] + comandArray[j + 2];
                        comandArray.Remove(comandArray[j + 1]);
                        comandArray.Remove(comandArray[j + 1]);
                    }
                }
            }

        }

        public static bool executeVirazenie(string query, Dictionary<string, Object> row)
        {
            var array = getCutVirazenie(query);
            string columnName = array[0];
            string znak = array[1];
            string value = array[2];

            switch (znak)
            {
                case ">":
                    if (Convert.ToDouble(row[columnName]) > Convert.ToInt32(value))
                        return true;
                    break;
                case "<":
                    if (Convert.ToDouble(row[columnName]) < Convert.ToInt32(value))
                        return true;
                    break;
                case "=":
                    if (row[columnName].ToString() == value)
                        return true;
                    break;
                case "!=":
                    if (row[columnName].ToString() == value)
                        return true;
                    break;
                case ">=":
                    if (Convert.ToDouble(row[columnName]) >= Convert.ToInt32(value))
                        return true;
                    break;
                case "<=":
                    if (Convert.ToDouble(row[columnName]) <= Convert.ToInt32(value))
                        return true;
                    break;
                case "LIKE":
                    int dick = 0;
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (value[i] == Convert.ToString(row[columnName])[dick] || value[i] == '_')
                        {
                            dick++;
                            if (dick + 1 != Convert.ToString(row[columnName]).Length && i + 1 == value.Length)
                                return false;
                            continue;
                        }
                        else if (value[i] == '%')
                        {
                            if (i + 1 != value.Length)
                                i++;
                            while (true)
                            {
                                if (Convert.ToString(row[columnName]).Length == dick)
                                    return true;
                                else if (value[i] == Convert.ToString(row[columnName])[dick])
                                    break;
                                dick++;
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;

            }

            return false;
        }
        public static string[] getCutVirazenie(string query)
        {
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
                var arZnak = splitQwery[index].Where(z => !char.IsDigit(z));// [30>=] ->[>][=>]
                var arValue = splitQwery[index].Where(z => char.IsDigit(z));// [30>=] -> [3[]0]
                znak = string.Join("", arZnak);
                value = string.Join("", arValue);
            }
            return new string[] { columnName.Trim(), znak.Trim(), value.Trim() };
        }
    }
}
//var i = query.ToUpper().IndexOf("WHERE");
//if (i != -1)
//{
//    var newline = query.Substring(i + 5);
//    Console.WriteLine(newline);
//    string[] comandArray = newline.Trim().Split(' ');
//    for (int j = 0; j < comandArray.Length; j++)
//    {
//        i = comandArray[j].ToUpper().IndexOf("LASTNAME");

//        if (i != -1)
//        {
//            if (simbol.Contains(comandArray[j - 1].ToUpper()))
//            {
//                string comandArray1 = comandArray[j] + comandArray[j - 1] + comandArray[j - 2];
//                Console.WriteLine(comandArray1);
//            }
//            else if (simbol.Contains(comandArray[j + 1].ToUpper()))
//            {
//                string comandArray1 = comandArray[j] + comandArray[j + 1] + comandArray[j + 2];
//                Console.WriteLine(comandArray1);
//            }
//        }
//    }
//}