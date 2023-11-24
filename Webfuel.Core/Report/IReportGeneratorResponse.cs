namespace Webfuel
{
    public interface IReportGeneratorResponse
    {
        int ProgressPercentage { get; } // 0 - 100

        bool Complete { get; }
    }
}
