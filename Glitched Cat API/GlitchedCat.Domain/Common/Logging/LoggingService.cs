using System;
using System.Diagnostics;
using System.Threading.Tasks; 
using Newtonsoft.Json;

namespace GlitchedCat.Domain.Common.Logging
{
    public interface ILoggingService
    {
        Task LogAsync(LogData log);
        Task<TResponse> ExecuteAndLogAsync<TRequest, TResponse>(Func<TRequest, TResponse> action, TRequest request);
    }
    public class LoggingService : ILoggingService
    {
        public Task LogAsync(LogData log)
        {
            var json = JsonConvert.SerializeObject(log);

            return LogToDataLakeAsync(json);
        }
        
        protected virtual Task LogToDataLakeAsync(string json)
        {
            //TODO: Code to send logs to data lake goes here
            return Task.CompletedTask;
        }
        
        public Task<TResponse> ExecuteAndLogAsync<TRequest, TResponse>(Func<TRequest, TResponse> action, TRequest request)
        {
            var stopwatch = new Stopwatch();
        
            var log = new LogData()
            {
                Log = $"Executing {typeof(TRequest).Name}",
                ExecutionTime = DateTime.Now,
            };
            _ = LogAsync(log);
        
            stopwatch.Start();

            // Execute method
            var response = action(request);
            
            stopwatch.Stop();
        
            log.Log = $"Executed {typeof(TRequest).Name}";
            log.ExecutionTime = DateTime.Now;
            log.TimeElapsed = stopwatch.Elapsed;

            _ = LogAsync(log);

            return Task.FromResult(response);
        }

    }
}