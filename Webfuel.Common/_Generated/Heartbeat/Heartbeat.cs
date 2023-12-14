using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class Heartbeat
    {
        public Heartbeat() { }
        
        public Heartbeat(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Heartbeat.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Heartbeat.Name):
                        Name = (string)value!;
                        break;
                    case nameof(Heartbeat.Live):
                        Live = (bool)value!;
                        break;
                    case nameof(Heartbeat.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Heartbeat.ProviderName):
                        ProviderName = (string)value!;
                        break;
                    case nameof(Heartbeat.ProviderParameter):
                        ProviderParameter = (string)value!;
                        break;
                    case nameof(Heartbeat.MinTime):
                        MinTime = (string)value!;
                        break;
                    case nameof(Heartbeat.MaxTime):
                        MaxTime = (string)value!;
                        break;
                    case nameof(Heartbeat.Schedule):
                        Schedule = (string)value!;
                        break;
                    case nameof(Heartbeat.NextExecutionScheduledAt):
                        NextExecutionScheduledAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(Heartbeat.SchedulerExceptionMessage):
                        SchedulerExceptionMessage = (string)value!;
                        break;
                    case nameof(Heartbeat.LogSuccessfulExecutions):
                        LogSuccessfulExecutions = (bool)value!;
                        break;
                    case nameof(Heartbeat.LastExecutionAt):
                        LastExecutionAt = value == DBNull.Value ? (DateTimeOffset?)null : (DateTimeOffset?)value;
                        break;
                    case nameof(Heartbeat.LastExecutionMessage):
                        LastExecutionMessage = (string)value!;
                        break;
                    case nameof(Heartbeat.LastExecutionSuccess):
                        LastExecutionSuccess = (bool)value!;
                        break;
                    case nameof(Heartbeat.LastExecutionMicroseconds):
                        LastExecutionMicroseconds = (int)value!;
                        break;
                    case nameof(Heartbeat.LastExecutionMetadataJson):
                        LastExecutionMetadataJson = (string)value!;
                        break;
                    case nameof(Heartbeat.RecentExecutionSuccessCount):
                        RecentExecutionSuccessCount = (int)value!;
                        break;
                    case nameof(Heartbeat.RecentExecutionFailureCount):
                        RecentExecutionFailureCount = (int)value!;
                        break;
                    case nameof(Heartbeat.RecentExecutionMicrosecondsAverage):
                        RecentExecutionMicrosecondsAverage = (int)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public bool Live  { get; set; } = false;
        public int SortOrder  { get; set; } = 0;
        public string ProviderName  { get; set; } = String.Empty;
        public string ProviderParameter  { get; set; } = String.Empty;
        public string MinTime  { get; set; } = String.Empty;
        public string MaxTime  { get; set; } = String.Empty;
        public string Schedule  { get; set; } = String.Empty;
        public DateTimeOffset? NextExecutionScheduledAt  { get; set; } = null;
        public string SchedulerExceptionMessage  { get; set; } = String.Empty;
        public bool LogSuccessfulExecutions  { get; set; } = false;
        public DateTimeOffset? LastExecutionAt  { get; set; } = null;
        public string LastExecutionMessage  { get; set; } = String.Empty;
        public bool LastExecutionSuccess  { get; set; } = false;
        public int LastExecutionMicroseconds  { get; set; } = 0;
        public string LastExecutionMetadataJson  { get; set; } = String.Empty;
        public int RecentExecutionSuccessCount  { get; set; } = 0;
        public int RecentExecutionFailureCount  { get; set; } = 0;
        public int RecentExecutionMicrosecondsAverage  { get; set; } = 0;
        public Heartbeat Copy()
        {
            var entity = new Heartbeat();
            entity.Id = Id;
            entity.Name = Name;
            entity.Live = Live;
            entity.SortOrder = SortOrder;
            entity.ProviderName = ProviderName;
            entity.ProviderParameter = ProviderParameter;
            entity.MinTime = MinTime;
            entity.MaxTime = MaxTime;
            entity.Schedule = Schedule;
            entity.NextExecutionScheduledAt = NextExecutionScheduledAt;
            entity.SchedulerExceptionMessage = SchedulerExceptionMessage;
            entity.LogSuccessfulExecutions = LogSuccessfulExecutions;
            entity.LastExecutionAt = LastExecutionAt;
            entity.LastExecutionMessage = LastExecutionMessage;
            entity.LastExecutionSuccess = LastExecutionSuccess;
            entity.LastExecutionMicroseconds = LastExecutionMicroseconds;
            entity.LastExecutionMetadataJson = LastExecutionMetadataJson;
            entity.RecentExecutionSuccessCount = RecentExecutionSuccessCount;
            entity.RecentExecutionFailureCount = RecentExecutionFailureCount;
            entity.RecentExecutionMicrosecondsAverage = RecentExecutionMicrosecondsAverage;
            return entity;
        }
    }
}

