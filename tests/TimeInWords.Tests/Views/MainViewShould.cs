using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Threading;
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
        view.SystemDecorations.Should().Be(SystemDecorations.Full);
        view.WindowState.Should().Be(WindowState.Normal);
        view.Topmost.Should().BeFalse();
    }

    [AvaloniaFact]
    public void ShowTheViewWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);

        view.Show();

        view.IsVisible.Should().BeTrue();
        view.ShowInTaskbar.Should().BeFalse();
        view.SystemDecorations.Should().Be(SystemDecorations.None);
        view.WindowState.Should().Be(WindowState.FullScreen);
        view.Topmost.Should().BeTrue();
    }

    [AvaloniaFact]
    public void ShowTheViewAtPositionWithSize()
    {
        var view = new MainView(new TimeInWordsSettings(), false);

        view.Show(50, 100, 500, 600);

        view.WindowStartupLocation.Should().Be(WindowStartupLocation.Manual);
        // Avalonia bug, see https://github.com/AvaloniaUI/Avalonia/issues/17071
        // view.Position.Should().BeEquivalentTo(new PixelPoint(50, 100));
        view.Width.Should().Be(500);
        view.Height.Should().Be(600);
    }

    [AvaloniaFact]
    public void CloseTheViewOnKeyDownWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.KeyPressQwerty(PhysicalKey.Escape, RawInputModifiers.None);

        monitoredView.Should().Raise("Closed");
    }

    [AvaloniaFact]
    public void CloseTheViewOnPointerMovedWhenFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        view.MouseMove(new Point(100, 100));
        view.MouseMove(new Point(100, 126));

        monitoredView.Should().Raise("Closed");
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

    [AvaloniaFact]
    public async Task ToggleFullScreenOnMouseDoubleClickWhenNotFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), false);
        using var monitoredView = view.Monitor<IMainView>();
        view.Show();

        DoubleClick();
        view.WindowState.Should().Be(WindowState.FullScreen);

        // simulate user delay
        await Task.Delay(500);
        Dispatcher.UIThread.RunJobs();

        DoubleClick();
        view.WindowState.Should().Be(WindowState.Normal);

        return;

        void DoubleClick()
        {
            view.MouseDown(new Point(100, 100), MouseButton.Left);
            view.MouseDown(new Point(100, 100), MouseButton.Left);
        }
    }
}
