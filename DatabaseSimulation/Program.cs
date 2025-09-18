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
            a.Add("id", 2);
            a.Add("lastName", "Иванов");
            a.Add("age", 25);
            a.Add("cost", 4.3);
            a.Add("active", false);
            Query.executeVirazenie("'id'>1", a);
            Query.executeVirazenie("'age'>=30 ", a);
            Query.executeVirazenie("30 >='age'", a);
            Query.executeVirazenie("'lastName' ilike '%п%'", a);
            Query.executeVirazenie("'lastName'='Федоров'", a);
            //while (true)
            //{
            //    var quwry = Console.ReadLine();
            //    Query.ExecuteQuery(table,quwry);
            //}
        }
    }
}
