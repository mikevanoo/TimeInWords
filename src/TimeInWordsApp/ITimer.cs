using System;

namespace TimeInWordsApp;

public interface ITimer : IDisposable
{
    public int Interval { get; set; }
    public event EventHandler Tick;
    public bool Enabled { get; set; }
}
