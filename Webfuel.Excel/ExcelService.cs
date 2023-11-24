using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Excel
{
    public interface IExcelService
    {
        ExcelWorkbook RenderWorkbook<T>(IEnumerable<T> items, string sheetName);

        void RenderWorksheet<T>(ExcelWorkbook workbook, IEnumerable<T> items, string sheetName);
    }

    [Service(typeof(IExcelService))]
    internal class ExcelService: IExcelService
    {
        public ExcelWorkbook RenderWorkbook<T>(IEnumerable<T> items, string sheetName)
        {
            var workbook = new ExcelWorkbook();
            RenderWorksheet(workbook, items, sheetName);
            return workbook;
        }

        public void RenderWorksheet<T>(ExcelWorkbook workbook, IEnumerable<T> items, string sheetName)
        {
            var worksheet = workbook.AddWorksheet(sheetName);
            var columns = RenderHeaders<T>(worksheet);
            RenderItems<T>(worksheet, items, columns);
        }

        // Implementation

        List<ExcelColumn<T>> RenderHeaders<T>(ExcelWorksheet worksheet)
        {
            var headers = ExcelUtility.GenerateColumns<T>();
            for(var i = 0; i < headers.Count; i++)
            {
                worksheet.Cell(i + 1, 1).SetValue(headers[i].Name);
            }
            return headers;
        }

        void RenderItems<T>(ExcelWorksheet worksheet, IEnumerable<T> items, List<ExcelColumn<T>> columns)
        {
            var row = 1;
            foreach(var item in items)
            {
                row++;
                foreach(var column in columns)
                {
                    worksheet.Cell(row, column.Index).SetValue(column.GetValue(item));
                }
            }
        }
    }
}
