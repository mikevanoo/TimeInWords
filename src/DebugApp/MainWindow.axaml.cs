using System.Text;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Threading;
using TextToTimeGridLib;
using TextToTimeGridLib.Grids;
using TimeToTextLib;
using Timer = System.Timers.Timer;

namespace DebugApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private LanguagePreset.Language _lang = LanguagePreset.Language.English;
    private TimeGrid _grid = new TimeGridEnglish();
    private DateTime _time = DateTime.Now;

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var timer = new Timer();
        timer.Interval = 1000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
        UpdateDisplay();
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e) => Dispatcher.UIThread.InvokeAsync(UpdateDisplay);

    private void UpdateDisplay()
    {
        if (AdvanceEverySecond.IsChecked == true)
        {
            _time = _time.AddMinutes(1);
        }
        var timeAsText = TimeToText.GetSimple(_lang, _time).ToString();

        TimeAsText.Content = $"{_time.ToShortTimeString()}{Environment.NewLine}{timeAsText}";

        var mask = _grid.GetBitMask(timeAsText, ForceMatches.IsChecked == true);
        var gridString = _grid.ToString().Split('\n');
        var maskString = mask.ToString().Split('\n');
        var result = _grid.ToString(mask).Split('\n');

        var sb = new StringBuilder();

        sb.AppendLine("Clock grid\t Bitmask\t\tResult");
        sb.AppendLine();

        for (var i = 0; i < gridString.Length; i++)
        {
            var line = $"{gridString[i].Trim()}\t{maskString[i].Trim()}\t{result[i].Trim()}";
            sb.AppendLine(line);
        }

        Grid.Content = sb.ToString();
    }

    private void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        var b = new StringBuilder();

        for (var h = 0; h < 24; h++)
        {
            for (var m = 0; m < 60; m++)
            {
                b.AppendLine(TimeToText.GetSimple(_lang, new DateTime(2000, 1, 1, h, m, 0)).ToString());
            }
        }

        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            if (Clipboard != null)
            {
                await Clipboard.SetTextAsync(b.ToString());
            }
        });
    }

    private void LanguageCombo_Loaded(object sender, RoutedEventArgs e)
    {
        LanguageCombo.Items.Add("English");
        LanguageCombo.Items.Add("Dutch");
        LanguageCombo.Items.Add("French");
        LanguageCombo.SelectedIndex = 0;
    }

    private void LanguageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (LanguageCombo.SelectedValue)
        {
            case "Dutch":
                _lang = LanguagePreset.Language.Dutch;
                _grid = new TimeGridDutch();
                break;
            case "French":
                _lang = LanguagePreset.Language.French;
                _grid = new TimeGridFrench();
                break;
            default:
                _lang = LanguagePreset.Language.English;
                _grid = new TimeGridEnglish();
                break;
        }
    }
}
