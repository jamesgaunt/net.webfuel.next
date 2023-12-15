using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using Serilog;

namespace Webfuel.Common
{
    public interface IHeartbeatService
    {
        List<string> EnumerateHeartbeatProviderNames();

        Task ExecuteScheduledHeartbeats();

        Task ExecuteHeartbeat(Guid heartbeatId);
    }

    [Service(typeof(IHeartbeatService), typeof(ICleanupProvider))]
    internal class HeartbeatService: IHeartbeatService, ICleanupProvider
    {
        private readonly IHeartbeatRepository _heartbeatRepository;
        private readonly IHeartbeatExecutionRepository _heartbeatExecutionRepository;
        private readonly IServiceProvider _serviceProvider;

        public HeartbeatService(IHeartbeatRepository heartbeatRepository, IHeartbeatExecutionRepository heartbeatExecutionRepository, IServiceProvider serviceProvider)
        {
            _heartbeatRepository = heartbeatRepository;
            _heartbeatExecutionRepository = heartbeatExecutionRepository;
            _serviceProvider = serviceProvider;
        }

        public List<string> EnumerateHeartbeatProviderNames()
        {
            var result = new List<String>();

            foreach (var provider in _serviceProvider.GetServices<IHeartbeatProvider>())
                result.AddRange(provider.HeartbeatProviderNames);

            result = result.OrderBy(p => p).ToList();
            result.Insert(0, "None");

            return result;
        }

        public async Task ExecuteScheduledHeartbeats()
        {
            var heartbeats = await _heartbeatRepository.SelectHeartbeat();
            foreach (var heartbeat in heartbeats.OrderByDescending(p => p.NextExecutionScheduledAt))
            {
                if (!heartbeat.Live || heartbeat.NextExecutionScheduledAt > DateTime.UtcNow)
                    continue; // Not scheduled

                await ExecuteHeartbeatImpl(heartbeat);
            }
        }

        public async Task ExecuteHeartbeat(Guid heartbeatId)
        {
            var heartbeat = await _heartbeatRepository.GetHeartbeat(heartbeatId);
            if (heartbeat == null)
                throw new InvalidOperationException("The specified heartbeat does not exist");
            await ExecuteHeartbeatImpl(heartbeat);
        }

        async Task ExecuteHeartbeatImpl(Heartbeat heartbeat)
        {
            var startTimestamp = MicrosecondTimer.Timestamp;

            try
            {
                if (heartbeat.LastExecutionAt.HasValue && heartbeat.LastExecutionAt.Value.Date != DateTime.Today)
                {
                    // Reset recent metrics
                    heartbeat.RecentExecutionFailureCount = 0;
                    heartbeat.RecentExecutionMicrosecondsAverage = 0;
                    heartbeat.RecentExecutionSuccessCount = 0;
                }

                heartbeat.LastExecutionAt = DateTime.UtcNow;
                heartbeat.LastExecutionSuccess = false;
                heartbeat.LastExecutionMessage = String.Empty;
                heartbeat.LastExecutionMicroseconds = 0;

                var provider = FindProvider(heartbeat.ProviderName);
                if (provider == null)
                    throw new InvalidOperationException($"Cannot find heartbeat provider {heartbeat.ProviderName}");

                var message = await provider.ExecuteHeartbeatAsync(heartbeat);
                var metadataJson = heartbeat.LastExecutionMetadataJson;

                // We need this hack for when we are running our own heartbeat as it saves an updated version of all heartbeats
                if (heartbeat.ProviderName.Contains("HeartbeatService"))
                {
                    heartbeat = await _heartbeatRepository.RequireHeartbeat(heartbeat.Id);
                    heartbeat.LastExecutionAt = DateTime.UtcNow;
                    heartbeat.LastExecutionSuccess = false;
                    heartbeat.LastExecutionMessage = String.Empty;
                    heartbeat.LastExecutionMicroseconds = 0;
                }

                heartbeat.LastExecutionMetadataJson = metadataJson;
                heartbeat.LastExecutionMessage = message;
                heartbeat.LastExecutionSuccess = true;
            }
            catch (Exception ex)
            {
                heartbeat.LastExecutionMessage = ex.Message;
                Log.Error(ex, $"Exception executing heartbeat {heartbeat.Name}");
            }
            finally
            {
                // Update the recent health metrics

                var recentTotal = heartbeat.RecentExecutionSuccessCount + heartbeat.RecentExecutionFailureCount;
                var recentMicroseconds = heartbeat.RecentExecutionMicrosecondsAverage * recentTotal;

                if (heartbeat.LastExecutionSuccess)
                    heartbeat.RecentExecutionSuccessCount++;
                else
                    heartbeat.RecentExecutionFailureCount++;

                heartbeat.LastExecutionMicroseconds = (int)(MicrosecondTimer.Timestamp - startTimestamp);
                heartbeat.RecentExecutionMicrosecondsAverage = (recentMicroseconds + heartbeat.LastExecutionMicroseconds) / (recentTotal + 1);

                SetNextScheduledExecution(heartbeat);

                await _heartbeatRepository.UpdateHeartbeat(heartbeat);
            }

            if ((heartbeat.LogSuccessfulExecutions || !heartbeat.LastExecutionSuccess) && !String.IsNullOrEmpty(heartbeat.LastExecutionMessage))
            {
                // Record the execution
                var execution = new HeartbeatExecution
                {
                    ExecutedAt = heartbeat.LastExecutionAt ?? DateTime.UtcNow,
                    Message = heartbeat.LastExecutionMessage,
                    Success = heartbeat.LastExecutionSuccess,
                    Microseconds = heartbeat.LastExecutionMicroseconds,
                    MetadataJson = heartbeat.LastExecutionMetadataJson,
                    HeartbeatId = heartbeat.Id,
                };
                await _heartbeatExecutionRepository.InsertHeartbeatExecution(execution);
            }
        }

        void SetNextScheduledExecution(Heartbeat heartbeat)
        {
            try
            {
                var next = GetNextScheduledExecution(heartbeat.Schedule, heartbeat.MinTime, heartbeat.MaxTime, DateTime.UtcNow);
                if (next.HasValue)
                {
                    heartbeat.NextExecutionScheduledAt = next.Value;
                    heartbeat.SchedulerExceptionMessage = String.Empty;
                }
                else
                {
                    heartbeat.NextExecutionScheduledAt = DateTime.UtcNow.AddHours(1);
                    heartbeat.SchedulerExceptionMessage = "Scheduler returned null";
                }
            }
            catch (Exception ex)
            {
                heartbeat.NextExecutionScheduledAt = DateTime.UtcNow.AddHours(1);
                heartbeat.SchedulerExceptionMessage = ex.Message;
            }
        }

        DateTimeOffset? GetNextScheduledExecution(string schedule, string minTime, string maxTime, DateTime baseTime)
        {
            var cron = CrontabSchedule.TryParse(schedule);
            if (cron == null)
                return null;
            return GetNextScheduledExecution(cron, ParseTime(minTime) ?? TimeSpan.FromDays(0), ParseTime(maxTime) ?? TimeSpan.FromDays(1), baseTime);
        }

        DateTime? GetNextScheduledExecution(CrontabSchedule schedule, TimeSpan minTime, TimeSpan maxTime, DateTime baseTime)
        {
            baseTime = ClampBaseTime(minTime, maxTime, baseTime);
            return schedule.GetNextOccurrence(baseTime);
        }

        TimeSpan? ParseTime(string time)
        {
            if (String.IsNullOrEmpty(time))
                return null;

            var parts = time.Split(':');
            if (parts.Length != 2)
                return null;

            if (!Int32.TryParse(parts[0], out var hours))
                return null;

            if (!Int32.TryParse(parts[1], out var minutes))
                return null;

            if (hours < 0 || hours > 24 || minutes < 0 || minutes > 59)
                return null;

            return new TimeSpan(hours: hours, minutes: minutes, seconds: 0);
        }

        DateTime ClampBaseTime(TimeSpan minTime, TimeSpan maxTime, DateTime baseTime)
        {
            var clampTime = new TimeSpan(hours: baseTime.Hour, minutes: baseTime.Minute, seconds: baseTime.Second);

            if (minTime < maxTime)
            {
                if (clampTime < minTime)
                    clampTime = minTime;
                else if (clampTime > maxTime)
                    clampTime = minTime.Add(TimeSpan.FromDays(1)); // Rollover to next day

            }
            else if (minTime > maxTime)
            {
                if (clampTime > maxTime && clampTime < minTime)
                    clampTime = minTime;
            }
            return baseTime.Date.Add(clampTime).AddSeconds(-1); // We take it back 1 second as usually we want the first run to be at the start of the clamp window
        }

        IHeartbeatProvider? FindProvider(string providerName)
        {
            foreach (var provider in _serviceProvider.GetServices<IHeartbeatProvider>())
            {
                if (provider.HeartbeatProviderNames.Contains(providerName))
                    return provider;
            }
            return null;
        }

        public async Task<int> CleanupAsync()
        {
            var query = await _heartbeatExecutionRepository.QueryHeartbeatExecution(new Query
            {
                Sort = { new QuerySort { Field = "Id", Direction = 1 } },
                Take = 10,
                Projection = { "Id", nameof(HeartbeatExecution.ExecutedAt) }
            });

            var count = 0;
            foreach (var item in query.Items)
            {
                if (item.ExecutedAt > DateTime.Now.AddDays(-2))
                    continue;
                await _heartbeatExecutionRepository.DeleteHeartbeatExecution(item.Id);
                count++;
            }

            return count;
        }
    }
}
