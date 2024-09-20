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

        ColorFader.SetControlForeColor(control, endColor, 3, 0);

        await Task.Delay(1000); // ensure we leave plenty of time to the fade to occur

        (control.Foreground as SolidColorBrush)?.Color.Should().Be(endColor);
    }
}
