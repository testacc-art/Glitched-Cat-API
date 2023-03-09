using System;

namespace GlitchedCat.Domain.Common.Logging;

public class LogData
{
    public string Message { get; set; }
    public DateTime ExecutionTime { get; set; }
    public TimeSpan TimeElapsed { get; set; }
}