using System;
using System.Text;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using TextToTimeGridLib;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

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
            TimeAsText.Content = TimeToText.GetSimple(_lang, _time).ToString();
        }
        else
        {
            TimeAsText.Content = TimeToText.GetSimple(_lang, DateTime.Now).ToString();
        }

        var mask = _grid.GetBitMask((string)TimeAsText.Content, ForceMatches.IsChecked == true);
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
        LanguageCombo.Items.Add("Nederlands");
        LanguageCombo.SelectedIndex = 0;
    }

    private void LanguageCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LanguageCombo.SelectedValue != null && (string)LanguageCombo.SelectedValue == "English")
        {
            _lang = LanguagePreset.Language.English;
            _grid = new TimeGridEnglish();
        }
        else
        {
            _lang = LanguagePreset.Language.Dutch;
            _grid = new TimeGridDutch();
        }
    }

    private void CheckBox_IsCheckedChanged(object sender, RoutedEventArgs e) => _time = DateTime.Now;
}
