using NSubstitute.ReceivedExtensions;
using TextToTimeGridLib;
using TimeInWordsApp.Presenters;
using TimeInWordsApp.Views;

namespace TimeInWordsApp.Tests.Presenters;

public class MainPresenterShould
{
    [Fact]
    public void InitialiseTheViewWhenInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);
        var settings = new TimeInWordsSettings { Debug = true };

        _ = new MainPresenter(settings, viewFactory);

        viewFactory.Received(1).Create(settings, false);
        view.Received(1).Show();
    }

    [Fact]
    public void InitialiseTheViewsWhenNotInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);
        var settings = new TimeInWordsSettings { Debug = false };

        _ = new MainPresenter(settings, viewFactory);

        viewFactory.Received().Create(settings, true);
        view.Received().Show(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());
    }

    [Fact]
    public void CloseAllViewsWhenOneViewClosesWhenInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);
        var settings = new TimeInWordsSettings { Debug = false };

        _ = new MainPresenter(settings, viewFactory);

        view.Closed += Raise.Event();

        view.Received().Close();
    }

    // [Fact]
    // public void ConfigureTimerCorrectly()
    // {
    //     var view = Substitute.For<ITimeInWordsView>();
    //     var settings = new TimeInWordsSettings();
    //     var dateTimeProvider = Substitute.For<IDateTimeProvider>();
    //     var timer = Substitute.For<ITimer>();
    //
    //     _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);
    //
    //     timer.Interval.Should().Be(1000);
    //     timer.Enabled.Should().BeTrue();
    // }
    //
    // [Fact]
    // public void UpdateTheViewFromTimerTickEvent()
    // {
    //     var view = Substitute.For<ITimeInWordsView>();
    //     var settings = new TimeInWordsSettings();
    //     var dateTimeProvider = Substitute.For<IDateTimeProvider>();
    //     var timer = Substitute.For<ITimer>();
    //
    //     _ = new TimeInWordsPresenter(view, settings, dateTimeProvider, timer);
    //
    //     timer.Tick += Raise.Event();
    //
    //     view.Time.Should().Be(dateTimeProvider.Now);
    //     view.TimeAsText.Should().NotBeNull();
    //     view.GridBitMask.Should().NotBeNull();
    //     view.Received(1).Update(true);
    //     view.Received(1).Update();
    // }
}
