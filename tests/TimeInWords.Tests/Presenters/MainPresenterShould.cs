using Avalonia;
using TimeInWords.Presenters;
using TimeInWords.Views;

namespace TimeInWords.Tests.Presenters;

public sealed class MainPresenterShould : IDisposable
{
    private readonly TimeInWordsSettings _settings = new() { Debug = false };
    private readonly IMainViewFactory _viewFactory = Substitute.For<IMainViewFactory>();
    private readonly IScreenProvider _screenProvider = Substitute.For<IScreenProvider>();
    private readonly CancellationTokenSource _mainLoopCts = new();

    public void Dispose() => _mainLoopCts.Dispose();

    [Fact]
    public void ShowOneWindowedViewWhenInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        _viewFactory.Create(_settings, false).Returns(view);
        _settings.Debug = true;

        CreatePresenter();

        _viewFactory.Received(1).Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>());
        view.Received(1).Show();
        view.DidNotReceive().Show(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());
        _screenProvider.DidNotReceive().GetScreens();
    }

    [Fact]
    public void ExitWhenTheViewClosesInDebugMode()
    {
        var view = Substitute.For<IMainView>();
        _viewFactory.Create(_settings, false).Returns(view);
        _settings.Debug = true;
        CreatePresenter();

        view.Closed += Raise.Event();

        _mainLoopCts.IsCancellationRequested.Should().BeTrue();
        view.DidNotReceive().Close();
    }

    [Fact]
    public void ShowAFullScreenViewOnEachScreenWhenNotInDebugMode()
    {
        var views = GivenScreens(
            new ScreenArea(new PixelRect(0, 0, 1920, 1080), 1.0),
            new ScreenArea(new PixelRect(1920, -200, 2560, 1400), 1.5)
        );

        CreatePresenter();

        _viewFactory.Received(2).Create(Arg.Any<TimeInWordsSettings>(), Arg.Any<bool>());
        _viewFactory.Received(2).Create(_settings, true);
        views[0].Received(1).Show(0, 0, 1920, 1080);
        // the size is converted from physical pixels to device-independent units, rounding down
        views[1].Received(1).Show(1920, -200, 1706, 933);
        views.Should().AllSatisfy(view => view.DidNotReceive().Show());
    }

    [Fact]
    public void NotExitWhileTheViewsAreOpenWhenNotInDebugMode()
    {
        GivenScreens(new ScreenArea(new PixelRect(0, 0, 1920, 1080), 1.0));

        CreatePresenter();

        _mainLoopCts.IsCancellationRequested.Should().BeFalse();
    }

    [Fact]
    public void CloseEveryViewAndExitWhenOneViewClosesWhenNotInDebugMode()
    {
        var views = GivenScreens(
            new ScreenArea(new PixelRect(0, 0, 1920, 1080), 1.0),
            new ScreenArea(new PixelRect(1920, 0, 1920, 1080), 1.0)
        );
        CreatePresenter();

        views[1].Closed += Raise.Event();

        views.Should().AllSatisfy(view => view.Received(1).Close());
        _mainLoopCts.IsCancellationRequested.Should().BeTrue();
    }

    [Fact]
    public void CloseEachViewOnlyOnceWhenClosingAViewRaisesItsClosedEvent()
    {
        var views = GivenScreens(
            new ScreenArea(new PixelRect(0, 0, 1920, 1080), 1.0),
            new ScreenArea(new PixelRect(1920, 0, 1920, 1080), 1.0)
        );
        // a real window raises Closed when it is closed, even when that close was requested by the presenter
        foreach (var view in views)
        {
            var closed = false;
            view.When(v => v.Close())
                .Do(_ =>
                {
                    if (!closed)
                    {
                        closed = true;
                        view.Closed += Raise.Event();
                    }
                });
        }
        CreatePresenter();

        views[0].Closed += Raise.Event();

        views.Should().AllSatisfy(view => view.Received(1).Close());
    }

    private void CreatePresenter() => _ = new MainPresenter(_settings, _viewFactory, _screenProvider, _mainLoopCts);

    private IMainView[] GivenScreens(params ScreenArea[] screens)
    {
        var views = screens.Select(_ => Substitute.For<IMainView>()).ToArray();
        _screenProvider.GetScreens().Returns(screens);
        _viewFactory.Create(_settings, true).Returns(views[0], views[1..]);
        return views;
    }
}
