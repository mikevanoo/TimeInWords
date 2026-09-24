using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using TimeInWords.Views;

namespace TimeInWords.Tests.Views;

public class MainViewShould
{
    [AvaloniaFact]
    public void ShowTheViewWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);

        view.Show();

        view.IsVisible.Should().BeTrue();
        view.ShowInTaskbar.Should().BeTrue();
        view.WindowDecorations.Should().Be(WindowDecorations.Full);
        view.WindowState.Should().Be(WindowState.Normal);
        view.Topmost.Should().BeFalse();
        view.Cursor.Should().BeSameAs(Cursor.Default);
    }

    [AvaloniaFact]
    public void ShowTheViewWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);

        view.Show();

        view.IsVisible.Should().BeTrue();
        view.ShowInTaskbar.Should().BeFalse();
        view.WindowDecorations.Should().Be(WindowDecorations.None);
        view.WindowState.Should().Be(WindowState.FullScreen);
        view.Topmost.Should().BeTrue();
        // the screensaver hides the pointer behind a transparent cursor
        view.Cursor.Should().NotBeNull().And.NotBeSameAs(Cursor.Default);
    }

    [AvaloniaFact]
    public void ShowTheViewAtPositionWithSize()
    {
        var view = new MainView(new TimeInWordsSettings(), false);

        view.Show(50, 100, 500, 600);

        view.WindowStartupLocation.Should().Be(WindowStartupLocation.Manual);
        view.Position.Should().BeEquivalentTo(new PixelPoint(50, 100));
        view.Width.Should().Be(500);
        view.Height.Should().Be(600);
    }

    [AvaloniaTheory]
    [InlineData(PhysicalKey.Escape)]
    [InlineData(PhysicalKey.Space)]
    [InlineData(PhysicalKey.A)]
    public void CloseTheViewOnAnyKeyDownWhenFullScreen(PhysicalKey key)
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.KeyPressQwerty(key, RawInputModifiers.None);

        monitoredView.Should().Raise("Closed");
    }

    [AvaloniaTheory]
    [InlineData(126, 100)]
    [InlineData(74, 100)]
    [InlineData(100, 126)]
    [InlineData(100, 74)]
    public void CloseTheViewOnPointerMovedMoreThanTheThresholdWhenFullScreen(int x, int y)
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.MouseMove(new Point(100, 100));
        view.MouseMove(new Point(x, y));

        monitoredView.Should().Raise("Closed");
    }

    [AvaloniaFact]
    public void NotCloseTheViewOnPointerMovedUpToTheThresholdWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        // each step moves exactly the threshold of 25 on each axis, which is tolerated
        view.MouseMove(new Point(100, 100));
        view.MouseMove(new Point(125, 125));
        view.MouseMove(new Point(100, 100));

        monitoredView.Should().NotRaise("Closed");
        view.IsVisible.Should().BeTrue();
    }

    [AvaloniaFact]
    public void NotCloseTheViewOnTheFirstPointerMoveWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        // there is no earlier position to compare with, so however far the pointer appears to jump it is not a move
        view.MouseMove(new Point(500, 500));

        monitoredView.Should().NotRaise("Closed");
        view.IsVisible.Should().BeTrue();
    }

    [AvaloniaFact]
    public void NotCloseTheViewOnPointerMovedWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.MouseMove(new Point(100, 100));
        view.MouseMove(new Point(300, 300));

        monitoredView.Should().NotRaise("Closed");
        view.IsVisible.Should().BeTrue();
    }

    [AvaloniaFact]
    public void ToggleFullScreenOnKeyDownF11WhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        view.Show();

        view.KeyPressQwerty(PhysicalKey.F11, RawInputModifiers.None);
        view.WindowState.Should().Be(WindowState.FullScreen);

        view.KeyPressQwerty(PhysicalKey.F11, RawInputModifiers.None);
        view.WindowState.Should().Be(WindowState.Normal);
    }

    [AvaloniaFact]
    public void CloseFullScreenOnKeyDownEscapeWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        view.Show();

        view.KeyPressQwerty(PhysicalKey.F11, RawInputModifiers.None);
        view.WindowState.Should().Be(WindowState.FullScreen);

        view.KeyPressQwerty(PhysicalKey.Escape, RawInputModifiers.None);
        view.WindowState.Should().Be(WindowState.Normal);
    }

    [AvaloniaTheory]
    [InlineData(PhysicalKey.Space)]
    [InlineData(PhysicalKey.A)]
    public void IgnoreOtherKeysWhenNotFullScreen(PhysicalKey key)
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.KeyPressQwerty(key, RawInputModifiers.None);

        view.WindowState.Should().Be(WindowState.Normal);
        monitoredView.Should().NotRaise("Closed");
    }

    [AvaloniaFact]
    public void ToggleFullScreenOnMouseDoubleClickWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        view.Show();

        DoubleClick(new Point(100, 100));
        view.WindowState.Should().Be(WindowState.FullScreen);

        // a double-click somewhere else starts a new click count rather than continuing the last one
        DoubleClick(new Point(300, 300));
        view.WindowState.Should().Be(WindowState.Normal);

        return;

        void DoubleClick(Point point)
        {
            view.MouseDown(point, MouseButton.Left);
            view.MouseUp(point, MouseButton.Left);
            view.MouseDown(point, MouseButton.Left);
            view.MouseUp(point, MouseButton.Left);
        }
    }

    [AvaloniaFact]
    public void NotToggleFullScreenOnSingleClickWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        view.Show();

        view.MouseDown(new Point(100, 100), MouseButton.Left);
        view.MouseUp(new Point(100, 100), MouseButton.Left);

        view.WindowState.Should().Be(WindowState.Normal);
    }
}
