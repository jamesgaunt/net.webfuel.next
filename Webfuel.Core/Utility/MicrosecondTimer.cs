using System.Diagnostics;

namespace Webfuel
{
    public static class MicrosecondTimer
    {
        public static long Timestamp
        {
            get
            {
                return (Stopwatch.GetTimestamp() * 1000L * 1000L) / Stopwatch.Frequency;
            }
        }
    }
}
