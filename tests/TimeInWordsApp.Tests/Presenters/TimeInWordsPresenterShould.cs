﻿using NSubstitute.ReceivedExtensions;
using TextToTimeGridLib;
using TimeInWordsApp.Presenters;
using TimeInWordsApp.Views;

namespace TimeInWordsApp.Tests.Presenters;

public class TimeInWordsPresenterShould
{
    [Fact]
    public void InitialiseTheView()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var now = DateTime.Now;
        dateTimeProvider.Now.Returns(now);
        var timer = Substitute.For<ITimer>();
        var presenter = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        view.Received(1).Initialise(presenter, settings, TimeGrid.Get(settings.Language));
        view.Time.Should().Be(now);
        view.TimeAsText.Should().NotBeNull();
        view.GridBitMask.Should().NotBeNull();
        view.Received(1).Update(true);
    }

    [Fact]
    public void ConfigureTimerCorrectly()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        timer.Interval.Should().Be(1000);
        timer.Enabled.Should().BeTrue();
    }

    [Fact]
    public void UpdateTheViewFromTimerTickEvent()
    {
        var view = Substitute.For<ITimeInWordsView>();
        var settings = new TimeInWordsSettings();
        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        var timer = Substitute.For<ITimer>();

        _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);

        timer.Tick += Raise.Event();

        view.Time.Should().Be(dateTimeProvider.Now);
        view.TimeAsText.Should().NotBeNull();
        view.GridBitMask.Should().NotBeNull();
        view.Received(1).Update(true);
        view.Received(1).Update();
    }
}