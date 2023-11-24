using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Excel;

namespace Webfuel.Tools.ConsoleApp
{
    public interface IUserImporter
    {
        Task Import();
    }

    [Service(typeof(IUserImporter))]
    internal class UserImporter: IUserImporter
    {
        public Task Import()
        {
            Console.WriteLine(@"Opening D:\users.xlsx");
            var workbook = ExcelWorkbook.Load(@"D:\users.xlsx");

            Console.WriteLine(@"Opened");

            var ws = workbook.Worksheet(1);
            var row = 2;

            while (!ws.IsRowEmpty(row))
            {
                Console.WriteLine(ws.Cell(row, ws.FindHeaderCol("EMAIL")).GetValue<string>());
                row++;
            }

            return Task.CompletedTask;
        }
    }
}
