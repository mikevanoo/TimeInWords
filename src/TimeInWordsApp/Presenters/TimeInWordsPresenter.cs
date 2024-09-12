using System;
using TextToTimeGridLib;
using TimeInWordsApp.Views;
using TimeToTextLib;

namespace TimeInWordsApp.Presenters;

public class TimeInWordsPresenter() : IDisposable
{
    private readonly ITimeInWordsView _view;
    private readonly TimeInWordsSettings _settings;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITimer _timer;
    private readonly TimeGrid _grid;

    private DateTime _debugDateTime;

    public TimeInWordsPresenter(
        ITimeInWordsView view,
        TimeInWordsSettings settings,
        IDateTimeProvider dateTimeProvider,
        ITimer timer
    )
        : this()
    {
        _view = view;
        _settings = settings;
        _dateTimeProvider = dateTimeProvider;
        _debugDateTime = _dateTimeProvider.Now;
        _debugDateTime = _debugDateTime.AddSeconds(0 - _debugDateTime.Second);

        _grid = TimeGrid.Get(_settings.Language);
        _view.Initialise(_settings, _grid);
        UpdateView(true);

        _timer = timer;
        _timer.Interval = 1000;
        _timer.Tick += OnTimedEvent;
        _timer.Enabled = true;
    }

    public void Dispose()
    {
        _timer.Enabled = false;
        _timer.Dispose();
        GC.SuppressFinalize(this);
    }

    private void OnTimedEvent(object source, EventArgs e) => UpdateView();

    private void UpdateView(bool force = false)
    {
        var now = _dateTimeProvider.Now;

        if (_settings.Debug)
        {
            _debugDateTime = _debugDateTime.AddMinutes(1);
            now = _debugDateTime;
        }

        _view.Time = now;
        _view.TimeAsText = TimeToText.GetSimple(_settings.Language, now);
        _view.GridBitMask = _grid.GetBitMask(_view.TimeAsText.TimeAsText, true).Mask;
        _view.Update(force);
    }
}
