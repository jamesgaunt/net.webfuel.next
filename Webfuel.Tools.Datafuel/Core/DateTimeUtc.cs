using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel
{
    public struct DateTimeUtc
    {
        private DateTime DateTime;
        
        public DateTimeUtc(DateTime dateTime)
        {
            DateTime = dateTime.ToUniversalTime();
        }

        public DateTimeUtc(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            DateTime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
        }

        public DateTimeUtc(long ticks)
        {
            DateTime = new DateTime(ticks, DateTimeKind.Utc);
        }

        public int Year { get => DateTime.Year; }

        public int Month { get => DateTime.Month; }

        public int Day { get => DateTime.Day; }

        public int Hour { get => DateTime.Hour; }

        public int Minute { get => DateTime.Minute; }

        public int Second { get => DateTime.Second; }

        public int Millisecond { get => DateTime.Millisecond; }

        public long Ticks { get => DateTime.Ticks; }

        public static DateTimeUtc Now { get => new DateTimeUtc(DateTime.UtcNow); }

        public static DateTimeUtc MinValue { get => new DateTimeUtc(DateTime.MinValue); }

        public static DateTimeUtc MaxValue { get => new DateTimeUtc(DateTime.MaxValue); }

        public DateTimeUtc AddDays(double value)
        {
            return new DateTimeUtc(DateTime.AddDays(value));
        }

        public DateTimeUtc AddHours(double value)
        {
            return new DateTimeUtc(DateTime.AddHours(value));
        }

        public static DateTimeUtc? RobustParse(string text)
        {
            if(DateTime.TryParse(text, out var result))
            {
                return new DateTimeUtc(result);
            }
            return null;
        }

        public override string ToString()
        {
            return DateTime.ToString();
        }

        public DateTime ToDateTime()
        {
            return DateTime;
        }

        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return DateTime.Equals(obj);
        }

        public static bool operator ==(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime == d1.DateTime;
        }

        public static bool operator !=(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime != d1.DateTime;
        }

        public static bool operator >=(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime >= d1.DateTime;
        }

        public static bool operator <=(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime <= d1.DateTime;
        }

        public static bool operator >(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime > d1.DateTime;
        }

        public static bool operator <(DateTimeUtc d0, DateTimeUtc d1)
        {
            return d0.DateTime < d1.DateTime;
        }
    }

    public class DateTimeUtcConverter : JsonConverter<DateTimeUtc>
    {
        public override DateTimeUtc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeUtc.RobustParse(reader.GetString()!) ?? DateTimeUtc.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeUtc value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}