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
    public static void Main(string[] args)
    {
        // Trace.Listeners.Add(new ConsoleTraceListener());
        // Trace.AutoFlush = true;

        BuildAvaloniaApp().Start(AppMain, args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont(); //.LogToTrace(Avalonia.Logging.LogEventLevel.Information);

    // Application entry point. Avalonia is completely initialized.
    private static void AppMain(Application app, string[] args)
    {
        var mode = ParseStartupMode(args);
        if (mode == StartupMode.Preview)
        {
            return;
        }

        var cts = new CancellationTokenSource();
        var settings = ReadSettings();
        settings.Debug = mode != StartupMode.Screensaver;

        if (mode == StartupMode.Config)
        {
            var settingsView = new SettingsEditorView();
            _ = new SettingsEditorPresenter(settingsView, cts);
        }
        else
        {
            _ = new MainPresenter(settings, new MainViewFactory(), cts);
        }

        app.Run(cts.Token);
    }

    private enum StartupMode
    {
        Debug,
        Screensaver,
        Config,
        Preview,
    }

    private static StartupMode ParseStartupMode(string[] args) =>
        args.Length == 0
            ? StartupMode.Debug
            : args[0].ToLowerInvariant() switch
            {
                ['/', 'c', ..] => StartupMode.Config, // Windows "Change Screensaver" dialog Settings mode
                ['/', 'p', ..] => StartupMode.Preview, // Windows "Change Screensaver" dialog Preview mode. Ignore and exit
                ['/', 's', ..] => StartupMode.Screensaver, // Full-screen mode
                _ => StartupMode.Debug,
            };

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
