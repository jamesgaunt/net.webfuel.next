namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFieldType
    {
        Unspecified = 0,

        String = 10,
        Number = 20,
        Boolean = 30,
        DateTime = 40,
        Date = 50,

        Reference = 1000,
    }
}
