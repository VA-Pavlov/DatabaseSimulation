using System;
using System.Collections.Generic;

namespace DatabaseSimulation
{
    public class Table
    {
        private List<Dictionary<string, Object>> table;

        public List<Dictionary<string, Object>> SqlTable
        {
            get
            {
                if (table == null)
                {
                    table = new List<Dictionary<string, Object>>();
                }
                return table;
            }
        }

        public static void FillTable(List<Dictionary<string, Object>> table)
        {
            Dictionary<string, Object> row1 = new Dictionary<string, Object>();
            row1.Add("id", 1);
            row1.Add("lastName", "Петров");
            row1.Add("age", 30);
            row1.Add("cost", 5.4);
            row1.Add("active", true);

            Dictionary<string, Object> row2 = new Dictionary<string, Object>();
            row2.Add("id", 2);
            row2.Add("lastName", "Иванов");
            row2.Add("age", 25);
            row2.Add("cost", 4.3);
            row2.Add("active", false);

            Dictionary<string, Object> row3 = new Dictionary<string, Object>();
            row3.Add("id", 3);
            row3.Add("lastName", "Федоров");
            row3.Add("age", 40);
            row3.Add("active", true);


            table.Add(row1);
            table.Add(row2);
            table.Add(row3);

        }

        public override string ToString()
        {
            var StringTable = "";
            var titles = table[0].Keys;
            foreach (var title in titles)
            {
                StringTable += title + "\t";
            }
            StringTable += "\n";

            foreach (var row in table)
            {
                foreach (var title in titles)
                {
                    if (row.TryGetValue(title, out object value))
                        StringTable += value + "  \t";
                    else
                        StringTable += "NULL  \t";
                }
                StringTable += "\n";
            }

            return StringTable;
        }
    }
}
