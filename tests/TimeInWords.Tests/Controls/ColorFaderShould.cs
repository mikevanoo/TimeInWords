using Avalonia.Headless.XUnit;
using Avalonia.Media;
using TimeInWords.Controls;

namespace TimeInWords.Tests.Controls;

public class ColorFaderShould
{
    [AvaloniaFact]
    public async Task FadeToEndColor()
    {
        var startColor = Color.FromRgb(255, 255, 255);
        var endColor = Color.FromRgb(0, 0, 0);

        var control = Substitute.For<IFadeableControl>();
        control.Foreground = new SolidColorBrush(startColor);

        await ColorFader.FadeForegroundAsync(control, endColor, intervals: 3, stepDelayMs: 0);

        (control.Foreground as SolidColorBrush)!.Color.Should().Be(endColor);
    }
}
