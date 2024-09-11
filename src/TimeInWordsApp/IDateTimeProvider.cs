using System;

namespace TimeInWordsApp;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
}
