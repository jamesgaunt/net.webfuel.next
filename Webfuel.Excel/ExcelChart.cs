using ClosedXML.Excel;

namespace Webfuel.Excel;

public class ExcelChart
{
    internal readonly IXLChart _chart;

    internal ExcelChart(IXLChart chart)
    {
        _chart = chart;
    }
}
