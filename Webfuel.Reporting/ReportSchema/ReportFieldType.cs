namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFieldType
    {
        Unspecified = 0,

        // Primatives
        String = 10,
        Decimal = 20,
        Boolean = 30, 
        DateTime = 40,
        Date = 50,

        // Complex Types
        Reference = 1000,
        ReferenceList = 1010,
    }
}
