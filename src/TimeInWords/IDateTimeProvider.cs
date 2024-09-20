using System;

namespace TimeInWords;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
}
