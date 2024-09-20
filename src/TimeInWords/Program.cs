using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using TimeInWords.Presenters;
using TimeInWords.Views;

namespace TimeInWords;

internal static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

    // Application entry point. Avalonia is completely initialized.
    private static void AppMain(Application app, string[] args)
    {
        // A cancellation token source that will be used to stop the main loop
        var cts = new CancellationTokenSource();

        // Do your startup code here
        var settings = new TimeInWordsSettings();

        if (args.Length > 0)
        {
            var firstArgument = args[0].ToLowerInvariant().Trim().Substring(0, 2);

            switch (firstArgument)
            {
                case "/c":
                case "/p":
                    // Windows "Change Screensaver" dialog Settings and Preview modes respectively
                    // Ignore and exit
                    return;
                case "/s":
                    settings.Debug = false;
                    break;
                default:
                    settings.Debug = true;
                    break;
            }
        }
        else
        {
            settings.Debug = true;
        }

        _ = new MainPresenter(settings, new MainViewFactory(), cts);

        // Start the main loop
        app.Run(cts.Token);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace();
}
