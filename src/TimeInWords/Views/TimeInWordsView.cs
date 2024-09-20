using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using TextToTimeGridLib;
using TimeToTextLib;

namespace TimeInWords.Views;

public class TimeInWordsView : Panel, ITimeInWordsView
{
    public DateTime Time { get; set; } = DateTime.Now;
    public TimeToTextFormat TimeAsText { get; set; } = new();
    public bool[][] GridBitMask { get; set; } = [];

    public TextBlock TimeLabel { get; private set; }
    public TextBlock TimeAsTextLabel { get; private set; }
    public Grid DisplayGrid { get; private set; }

    private TimeInWordsSettings _settings = null!;
    private TimeInWordsSettings Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            UpdateSettings(_settings);
        }
    }
    private TimeGrid TimeGrid { get; set; } = null!;

    private TextBlock _additionalMinute1 = null!;
    private TextBlock _additionalMinute2 = null!;
    private TextBlock _additionalMinute3 = null!;
    private TextBlock _additionalMinute4 = null!;

    public TimeInWordsView()
    {
        var stack = new StackPanel();

        TimeLabel = new TextBlock { Text = "(the time)" };
        stack.Children.Add(TimeLabel);

        TimeAsTextLabel = new TextBlock { Text = "(the time as text)" };
        stack.Children.Add(TimeAsTextLabel);

        DisplayGrid = new Grid
        {
            Width = 1200,
            Height = 1000,
            // DEBUG
            Background = Brushes.Green,
        };
        stack.Children.Add(DisplayGrid);

        Children.Add(stack);

        // DEBUG
        // Background = Brushes.Red;
    }

    public void Initialise(TimeInWordsSettings settings, TimeGrid grid)
    {
        Settings = settings;
        TimeGrid = grid;

        TimeLabel.IsVisible = settings.Debug;
        TimeAsTextLabel.IsVisible = settings.Debug;
        BuildGrid();
    }

    public void Update(bool force = false)
    {
        TimeLabel.Text = Time.ToShortTimeString();

        if (Time.Second == 0 || force)
        {
            TimeAsTextLabel.Text = TimeAsText.ToString();

            // activate the letter grid
            // LoopMainGrid(
            //     (rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
            //     {
            //         if (tblLayout.GetControlFromPosition(columnIndex, rowIndex) is LedLetter led)
            //         {
            //             led.Active = GridBitMask[gridRowIndex][gridColumnIndex];
            //         }
            //     }
            // );

            // activate the additional minutes
            // _additionalMinute1.Active = TimeAsText.AdditionalMinutes >= 1;
            // _additionalMinute2.Active = TimeAsText.AdditionalMinutes >= 2;
            // _additionalMinute3.Active = TimeAsText.AdditionalMinutes >= 3;
            // _additionalMinute4.Active = TimeAsText.AdditionalMinutes >= 4;
        }
    }

    private void UpdateSettings(TimeInWordsSettings settings)
    {
        Background = new SolidColorBrush(settings.BackgroundColour);
        var debugLabelForeground = new SolidColorBrush(settings.ActiveFontColour);
        TimeLabel.Foreground = debugLabelForeground;
        TimeAsTextLabel.Foreground = debugLabelForeground;
    }

    private void BuildGrid()
    {
        // define the grid rows and columns with percentages
        // +2 on the row and column counts to accommodate the additional minute LEDs
        for (var i = 0; i < TimeGrid.GridHeight + 2; i++)
        {
            DisplayGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }

        for (var i = 0; i < TimeGrid.GridWidth + 2; i++)
        {
            DisplayGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        // add the additional minute LEDs
        // TODO
        _additionalMinute1 = new TextBlock { Text = "X" }; //new LedLight(Settings);
        _additionalMinute2 = new TextBlock { Text = "X" }; //new LedLight(Settings);
        _additionalMinute3 = new TextBlock { Text = "X" }; //new LedLight(Settings);
        _additionalMinute4 = new TextBlock { Text = "X" }; //new LedLight(Settings);

        DisplayGrid.Children.Add(_additionalMinute1);
        Grid.SetRow(_additionalMinute1, 0);
        Grid.SetColumn(_additionalMinute1, 0);

        DisplayGrid.Children.Add(_additionalMinute2);
        Grid.SetRow(_additionalMinute2, 0);
        Grid.SetColumn(_additionalMinute2, DisplayGrid.ColumnDefinitions.Count - 1);

        DisplayGrid.Children.Add(_additionalMinute3);
        Grid.SetRow(_additionalMinute3, DisplayGrid.RowDefinitions.Count - 1);
        Grid.SetColumn(_additionalMinute3, DisplayGrid.ColumnDefinitions.Count - 1);

        DisplayGrid.Children.Add(_additionalMinute4);
        Grid.SetRow(_additionalMinute4, DisplayGrid.RowDefinitions.Count - 1);
        Grid.SetColumn(_additionalMinute4, 0);

        var charGrid = TimeGrid.CharGrid;
        LoopMainGrid(
            (rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
            {
                // TODO
                // var led = new LedLetter(Settings, charGrid[gridRowIndex][gridColumnIndex].ToString());
                var led = new TextBlock { Text = charGrid[gridRowIndex][gridColumnIndex].ToString() };
                DisplayGrid.Children.Add(led);
                Grid.SetRow(led, rowIndex);
                Grid.SetColumn(led, columnIndex);
            }
        );
    }

    private delegate void LoopMainGridAction(int rowIndex, int columnIndex, int gridRowIndex, int gridColumnIndex);

    private static void LoopMainGrid(LoopMainGridAction action)
    {
        // the additional minute LEDs are in the first/last rows and columns
        // the main grid starts in the 2nd row and column so start at index 1
        for (var rowIndex = 1; rowIndex < TimeGrid.GridHeight + 1; rowIndex++)
        {
            for (var columnIndex = 1; columnIndex < TimeGrid.GridWidth + 1; columnIndex++)
            {
                var gridRowIndex = rowIndex - 1;
                var gridColumnIndex = columnIndex - 1;
                action(rowIndex, columnIndex, gridRowIndex, gridColumnIndex);
            }
        }
    }
}
