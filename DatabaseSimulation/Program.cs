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
            Table table = new Table();
            Table.FillTable(table.GetTable());
            while (true)
            {
                var quwry = Console.ReadLine();
                Query.ExecuteQuery(table,quwry);
            }
        }
    }
}
