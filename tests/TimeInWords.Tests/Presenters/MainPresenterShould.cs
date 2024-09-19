using Avalonia.Headless.XUnit;
using TimeInWords.Presenters;
using TimeInWords.Views;

namespace TimeInWords.Tests.Presenters;

public class MainPresenterShould
{
    [AvaloniaFact]
    public void InitialiseTheViewWhenInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);
        var settings = new TimeInWordsSettings { Debug = true };

        _ = new MainPresenter(settings, viewFactory, new CancellationTokenSource());

        viewFactory.Received(1).Create(settings, false);
        view.Received(1).Show();
    }

    [AvaloniaFact]
    public void InitialiseTheViewsWhenNotInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);
        var settings = new TimeInWordsSettings { Debug = false };

        _ = new MainPresenter(settings, viewFactory, new CancellationTokenSource());

        viewFactory.Received().Create(settings, true);
        view.Received().Show(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());
    }

    [AvaloniaFact]
    public void CloseAllViewsWhenOneViewClosesWhenInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        var viewFactory = Substitute.For<IMainViewFactory>();
        viewFactory.Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>()).Returns(view);

        var settings = new TimeInWordsSettings { Debug = false };

        _ = new MainPresenter(settings, viewFactory, new CancellationTokenSource());

        view.Closed += Raise.Event();

        view.Received().Close();
    }
}
