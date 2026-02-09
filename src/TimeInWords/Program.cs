using System;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.Configuration;
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
        var settings = ReadSettings();

        if (args.Length > 0)
        {
            var firstArgument = args[0].ToLowerInvariant().Trim().Substring(0, 2);

            switch (firstArgument)
            {
                case "/c":
                    // Windows "Change Screensaver" dialog Settings mode
                    var settingsView = new SettingsEditorView();
                    _ = new SettingsEditorPresenter(settingsView, cts);
                    app.Run(cts.Token);
                    return;
                case "/p":
                    // Windows "Change Screensaver" dialog Preview mode
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

        // Start the main app
        _ = new MainPresenter(settings, new MainViewFactory(), cts);
        app.Run(cts.Token);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace();

    private static TimeInWordsSettings ReadSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        TimeInWordsSettings settings = new();
        configuration.Bind(settings);

        return settings;
    }
}
