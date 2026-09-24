using Avalonia;
using Avalonia.Headless.XUnit;

namespace TimeInWords.Tests;

public class ScreenProviderShould
{
    [AvaloniaFact]
    public void ReturnTheWorkingAreaAndScalingOfEachPlatformScreen()
    {
        // the headless platform reports a single 1920x1280 screen at 100% scaling
        var screens = new ScreenProvider().GetScreens();

        screens.Should().Equal(new ScreenArea(new PixelRect(0, 0, 1920, 1280), 1.0));
    }
}
