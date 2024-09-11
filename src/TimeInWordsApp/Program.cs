using System.Windows.Forms;
using TimeInWordsApp;
using TimeInWordsApp.Presenters;

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
    settings.Debug = false;
    ShowTimeInWords(settings);
}

static void ShowInDebugMode(TimeInWordsSettings settings)
{
    settings.Debug = true;
    ShowTimeInWords(settings);
}

static void ShowTimeInWords(TimeInWordsSettings settings)
{
    var presenter = new MainPresenter(settings);
    Application.Run(presenter);
}
