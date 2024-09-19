using Avalonia;
using Avalonia.Headless;
using TimeInWords.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace TimeInWords.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
