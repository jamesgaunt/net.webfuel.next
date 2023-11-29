using System.Text;

namespace Webfuel.Scribble
{
    public class ScribbleStringBuilder
    {
        private readonly StringBuilder Builder = new StringBuilder();

        public void Clear()
        {
            Builder.Clear();
        }

        public ScribbleStringBuilder Append(object? value)
        {
            Builder.Append(value);
            return this;
        }

        public int Length { get { return Builder.Length; } }

        public override string ToString()
        {
            return Builder.ToString();
        }
    }

    public class ScribbleEnvironment
    {
        public ScribbleStringApi String { get; } = new ScribbleStringApi();

        public ScribbleDateTimeApi DateTime { get; } = new ScribbleDateTimeApi();

        public ScribbleDateApi Date { get; } = new ScribbleDateApi();

        public ScribbleTypeApi Type { get; } = new ScribbleTypeApi();

        public ScribbleParseApi Parse { get; } = new ScribbleParseApi();

        public ScribbleDayOfWeekApi DayOfWeek { get; } = new ScribbleDayOfWeekApi();

        public ScribbleGuidApi Guid { get; } = new ScribbleGuidApi();

        public ScribbleMathApi Math { get; } = new ScribbleMathApi();
    }

    public class ScribbleMathApi
    {
        public int Clamp(int value, int min = int.MinValue, int max = int.MaxValue)
        {
            return Math.Clamp(value, min, max);
        }

        public double Clamp(double value, double min = double.MinValue, double max = double.MaxValue)
        {
            return Math.Clamp(value, min, max);
        }

        public decimal Clamp(decimal value, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            return Math.Clamp(value, min, max);
        }
    }

    public class ScribbleStringApi
    {
        public bool IsNullOrEmpty(string? value)
        {
            return String.IsNullOrEmpty(value);
        }

        public bool IsNullOrWhiteSpace(string? value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public string Empty
        {
            get { return String.Empty; }
        }

        public ScribbleStringBuilder Builder()
        {
            return new ScribbleStringBuilder();
        }
    }

    public class ScribbleGuidApi
    {
        public Guid? Parse(string? value, Guid? fallback = null)
        {
            if (Guid.TryParse(value, out var result))
                return result;
            return fallback;
        }

        public Guid Empty { get { return Guid.Empty; } }

        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }


    public class ScribbleDateTimeApi
    {
        public DateTimeOffset UtcNow { get { return DateTimeOffset.UtcNow; } }

        public DateTimeOffset Now { get { return DateTimeOffset.Now; } }

        public DateTimeOffset MinValue { get { return DateTimeOffset.MinValue; } }

        public DateTimeOffset MaxValue { get { return DateTimeOffset.MaxValue; } }
    }

    public class ScribbleDateApi
    {
        public DateOnly Today { get { return DateOnly.FromDateTime(DateTime.Today); } }
    }

     public class ScribbleTypeApi
    {
        public string Name(object value)
        {
            if (value == null)
                return "null";
            return value.GetType().Name;
        }

        public string FullName(object value)
        {
            if (value == null)
                return "null";
            return value.GetType().FullName ?? String.Empty;
        }
    }

    public class ScribbleParseApi
    {
        public int? Int(string input)
        {
            if (int.TryParse(input, out var value))
                return value;
            return null;
        }
    }

    public class ScribbleDayOfWeekApi
    {
        public DayOfWeek Monday => DayOfWeek.Monday;

        public DayOfWeek Tuesday => DayOfWeek.Tuesday;

        public DayOfWeek Wednesday => DayOfWeek.Wednesday;

        public DayOfWeek Thursday => DayOfWeek.Thursday;

        public DayOfWeek Friday => DayOfWeek.Friday;

        public DayOfWeek Saturday => DayOfWeek.Saturday;

        public DayOfWeek Sunday => DayOfWeek.Sunday;
    }
}