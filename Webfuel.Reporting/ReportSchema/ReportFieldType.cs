namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFieldType
    {
        Unspecified = 0,

        // Primative Types

        String = 10,
        Number = 20,
        Boolean = 30, 
        DateTime = 40,
        Date = 50,

        // Complex Types

        Reference = 1000,
        ReferenceList = 1010,
        Expression = 1020,
    }
}
