using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Table table = new Table();
            //Table.FillTable(table.GetTable());
            Dictionary<string, Object> a = new Dictionary<string, Object>();
            a.Add("id", 0);
            a.Add("lastName", "Иванов");
            a.Add("age", 50);
            a.Add("cost", 4.3);
            a.Add("active", false);
            Query.blockWhere("select * where 'id'>1 and 'age'>=30 or 'lastName' like '%п%'", a);
            //Console.WriteLine(Query.executeVirazenie("'age'>=30 ", a));
            //Console.WriteLine(Query.executeVirazenie("'lastName' ilike '%п%'", a));
            //Console.WriteLine(Query.executeVirazenie("'lastName'='Иванов'", a));
            //while (true)
            //{
            //    var quwry = Console.ReadLine();
            //    Query.ExecuteQuery(table,quwry);
            //}
        }
    }
}
