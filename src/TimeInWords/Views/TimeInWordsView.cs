using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using TextToTimeGridLib;
using TimeToTextLib;

namespace TimeInWords.Views;

public class TimeInWordsView : Panel, ITimeInWordsView
{
    public DateTime Time { get; set; }
    public TimeToTextFormat TimeAsText { get; set; }
    public bool[][] GridBitMask { get; set; }

    private readonly TextBlock _lblTime;
    private readonly TextBlock _lblTimeAsText;

    public TimeInWordsView()
    {
        var stack = new StackPanel();

        _lblTime = new TextBlock { Text = "(the time)" };
        stack.Children.Add(_lblTime);

        _lblTimeAsText = new TextBlock { Text = "(the time as text)" };
        stack.Children.Add(_lblTimeAsText);

        Children.Add(stack);

        // DEBUG
        // Background = Brushes.Red;
    }

    public void Initialise(TimeInWordsSettings settings, TimeGrid grid) { }

    public void Update(bool force = false)
    {
        _lblTime.Text = Time.ToShortTimeString();

        if (Time.Second == 0 || force)
        {
            _lblTimeAsText.Text = TimeAsText.ToString();

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
}
