using System;
using Avalonia.Threading;

namespace TimeInWords.Views;

public class TimeInWordsTimer : ITimer
{
    private readonly DispatcherTimer _timer = new();

    public int Interval
    {
        get => (int)_timer.Interval.TotalMilliseconds;
        set => _timer.Interval = TimeSpan.FromMilliseconds(value);
    }

    public bool Enabled
    {
        get => _timer.IsEnabled;
        set => _timer.IsEnabled = value;
    }

    public event EventHandler Tick
    {
        add => _timer.Tick += value;
        remove => _timer.Tick -= value;
    }
}
