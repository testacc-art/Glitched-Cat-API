using System;

namespace GlitchedCat.Domain.Common.Logging;

public abstract class BaseLog
{
    public Guid Id { get; set; }
    public string Log { get; set; }
    public DateTime ExecutionTime { get; set; }
    public TimeSpan TimeElapsed { get; set; }
}