﻿using System;
using TextToTimeGridLib;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Presenters;

public class TimeInWordsPresenter()
{
    private readonly ITimeInWordsView _view = null!;
    private readonly TimeInWordsSettings _settings = null!;
    private readonly IDateTimeProvider _dateTimeProvider = null!;
    private readonly ITimer _timer = null!;
    private readonly TimeGrid _grid = null!;

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

    private void OnTimedEvent(object? sender, EventArgs e) => UpdateView();

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
