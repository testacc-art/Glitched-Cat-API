using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlitchedCat.Domain.Common.Logging
{
    public class LoggingService : ILogger
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }
        
        public LoggingService()
        {
            // Default constructor does nothing
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Convert state to JSON for logging
            var json = JsonConvert.SerializeObject(state);

            // Send logs to data lake
            SendToDataLake(json);

            // Log to console or other logging destination
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
        
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

        private async void SendToDataLake(string json)
        {
            //TODO: Code to send logs to data lake goes here
        }
    }
}