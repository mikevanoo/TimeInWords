using System.Windows.Forms;
using TimeInWordsApp;
using TimeInWordsApp.Views;

var settings = new TimeInWordsSettings();

Application.EnableVisualStyles();

if (args.Length > 0)
{
    var firstArgument = args[0].ToLowerInvariant().Trim().Substring(0, 2);

    switch (firstArgument)
    {
        case "/c":
        case "/p":
            // Windows "Change Screensaver" dialog Settings and Preview modes respectively
            // Ignore
            break;
        case "/s":
            ShowFullScreen(settings);
            break;
        default:
            ShowInDebugMode(settings);
            break;
    }
}
else
{
    ShowInDebugMode(settings);
}

return;

static void ShowFullScreen(TimeInWordsSettings settings)
{
    var context = new TimeInWordsApplicationContext(settings, true);
    Application.Run(context);
}

static void ShowInDebugMode(TimeInWordsSettings settings)
{
    settings.Debug = true;
    var mainView = new MainView(settings);
    Application.Run(mainView);
}
