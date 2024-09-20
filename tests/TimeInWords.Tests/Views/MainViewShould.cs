using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using TimeInWords.Views;

namespace TimeInWords.Tests.Views;

public class MainViewShould
{
    [AvaloniaFact]
    public void ShowTheViewNotFullScreen()
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
    public void ShowTheViewFullScreen()
    {
        var view = new MainView(new TimeInWordsSettings(), true);

        view.Show();

        view.IsVisible.Should().BeTrue();
        view.ShowInTaskbar.Should().BeFalse();
        view.SystemDecorations.Should().Be(SystemDecorations.None);
        view.WindowState.Should().Be(WindowState.Maximized);
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
}
