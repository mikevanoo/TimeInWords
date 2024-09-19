using System;

namespace TimeInWords;

public interface ITimer
{
    public int Interval { get; set; }
    public event EventHandler Tick;
    public bool Enabled { get; set; }
}
