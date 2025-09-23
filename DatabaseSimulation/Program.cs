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
            //Query.blockWhere("select * where 'id'>1 and 'age'>=30 or 'lastName' like '%п%'", a);
            Table table = new Table();
            Table.FillTable(table.SqlTable);
            while (true)
            {

                var quwry = Console.ReadLine();
                Query.ExecuteQuery(table, quwry);
            }
        }
    }
}
