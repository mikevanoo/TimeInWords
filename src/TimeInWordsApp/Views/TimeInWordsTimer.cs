using System;
using System.Windows.Forms;

namespace TimeInWordsApp.Views;

public class TimeInWordsTimer : ITimer
{
    private readonly Timer _timer = new();

    public int Interval
    {
        get => _timer.Interval;
        set => _timer.Interval = value;
    }

    public bool Enabled
    {
        get => _timer.Enabled;
        set => _timer.Enabled = value;
    }

    public event EventHandler Tick
    {
        add => _timer.Tick += value;
        remove => _timer.Tick -= value;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
