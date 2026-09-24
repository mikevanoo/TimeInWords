using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Media;
using TextToTimeGridLib;
using TextToTimeGridLib.Grids;
using TimeInWords.Controls;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Tests.Views;

public class TimeInWordsViewShould
{
    [AvaloniaFact]
    public void ShowDebugLabelsWhenInitialisedInDebugMode()
    {
        var view = new TimeInWordsView();
        var settings = new TimeInWordsSettings { Debug = true };

        view.Initialise(settings, new TimeGridEnglish());

        view.TimeLabel.IsVisible.Should().BeTrue();
        view.TimeAsTextLabel.IsVisible.Should().BeTrue();
    }

    [AvaloniaFact]
    public void NotShowDebugLabelsWhenInitialisedNotInDebugMode()
    {
        var view = new TimeInWordsView();
        var settings = new TimeInWordsSettings { Debug = false };

        view.Initialise(settings, new TimeGridEnglish());

        view.TimeLabel.IsVisible.Should().BeFalse();
        view.TimeAsTextLabel.IsVisible.Should().BeFalse();
    }

    [AvaloniaFact]
    public void ApplySettingsWhenInitialised()
    {
        var view = new TimeInWordsView();
        var settings = new TimeInWordsSettings();

        view.Initialise(settings, new TimeGridEnglish());

        view.Background.Should().BeOfType<SolidColorBrush>();
        (view.Background as SolidColorBrush)!.Color.Should().Be(settings.BackgroundColour);

        view.TimeLabel.Foreground.Should().BeOfType<SolidColorBrush>();
        (view.TimeLabel.Foreground as SolidColorBrush)!.Color.Should().Be(settings.ActiveFontColour);

        view.TimeAsTextLabel.Foreground.Should().BeOfType<SolidColorBrush>();
        (view.TimeAsTextLabel.Foreground as SolidColorBrush)!.Color.Should().Be(settings.ActiveFontColour);
    }

    [AvaloniaFact]
    public void BuildTheGridWhenInitialised()
    {
        var view = new TimeInWordsView();
        var settings = new TimeInWordsSettings();
        var grid = new TimeGridEnglish();

        view.Initialise(settings, grid);

        // +2 on the row and column counts to accommodate the additional minute LEDs
        view.DisplayGrid.ColumnDefinitions.Should().HaveCount(grid.GridWidth + 2);
        view.DisplayGrid.RowDefinitions.Should().HaveCount(grid.GridHeight + 2);
    }

    [AvaloniaFact]
    public void DisplayCorrectTextInDebugLabelsWhenUpdated()
    {
        var view = new TimeInWordsView();
        var settings = new TimeInWordsSettings { Debug = true };
        var timeGridEnglish = new TimeGridEnglish();
        view.Initialise(settings, timeGridEnglish);
        view.Time = DateTime.Now;
        view.TimeAsText = new TimeToTextFormat { TimeAsText = "the time as text", AdditionalMinutes = 1 };
        view.GridBitMask = timeGridEnglish.GetBitMask(view.TimeAsText.ToString(), true).Mask;

        view.Update(true);

        view.TimeLabel.Text.Should().Be(view.Time.ToShortTimeString());
        view.TimeAsTextLabel.Text.Should().Be(view.TimeAsText.ToString());
    }

    [AvaloniaFact]
    public void LightExactlyTheLettersInTheBitmaskWhenUpdated()
    {
        var view = new TimeInWordsView();
        var grid = new TimeGridEnglish();
        view.Initialise(new TimeInWordsSettings(), grid);
        var expected = grid.GetBitMask("IT IS TEN PAST TWO", true).Mask;
        view.GridBitMask = expected;

        view.Update(true);

        LitLetters(view, grid).Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [AvaloniaFact]
    public void DimLettersThatLeaveTheBitmaskWhenUpdatedAgain()
    {
        var view = new TimeInWordsView();
        var grid = new TimeGridEnglish();
        view.Initialise(new TimeInWordsSettings(), grid);
        view.GridBitMask = grid.GetBitMask("IT IS TEN PAST TWO", true).Mask;
        view.Update(true);
        var expected = grid.GetBitMask("IT IS HALF PAST TWO", true).Mask;
        view.GridBitMask = expected;

        view.Update(true);

        LitLetters(view, grid).Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [AvaloniaTheory]
    [InlineData(0, false, false, false, false)]
    [InlineData(1, true, false, false, false)]
    [InlineData(2, true, true, false, false)]
    [InlineData(3, true, true, true, false)]
    [InlineData(4, true, true, true, true)]
    public void LightTheAdditionalMinuteLedsClockwiseFromTopLeftWhenUpdated(
        int additionalMinutes,
        bool topLeft,
        bool topRight,
        bool bottomRight,
        bool bottomLeft
    )
    {
        var view = new TimeInWordsView();
        var grid = new TimeGridEnglish();
        view.Initialise(new TimeInWordsSettings(), grid);
        view.TimeAsText = new TimeToTextFormat
        {
            TimeAsText = "IT IS TWO OCLOCK",
            AdditionalMinutes = additionalMinutes,
        };
        view.GridBitMask = grid.GetBitMask(view.TimeAsText.TimeAsText, true).Mask;

        view.Update(true);

        // the LEDs sit in the corners of the ring around the letter grid
        var lastRow = grid.GridHeight + 1;
        var lastColumn = grid.GridWidth + 1;
        LedAt<LedLight>(view, 0, 0).Active.Should().Be(topLeft);
        LedAt<LedLight>(view, 0, lastColumn).Active.Should().Be(topRight);
        LedAt<LedLight>(view, lastRow, lastColumn).Active.Should().Be(bottomRight);
        LedAt<LedLight>(view, lastRow, 0).Active.Should().Be(bottomLeft);
    }

    [AvaloniaFact]
    public void OnlyUpdateTheClockLabelWhenNotForcedPartWayThroughAMinute()
    {
        var view = new TimeInWordsView();
        var grid = new TimeGridEnglish();
        view.Initialise(new TimeInWordsSettings { Debug = true }, grid);
        var timeAsTextBefore = view.TimeAsTextLabel.Text;
        view.Time = new DateTime(2024, 1, 1, 2, 10, 30);
        view.TimeAsText = new TimeToTextFormat { TimeAsText = "IT IS TEN PAST TWO", AdditionalMinutes = 4 };
        view.GridBitMask = grid.GetBitMask(view.TimeAsText.TimeAsText, true).Mask;

        view.Update();

        view.TimeLabel.Text.Should().Be(view.Time.ToShortTimeString());
        view.TimeAsTextLabel.Text.Should().Be(timeAsTextBefore);
        view.DisplayGrid.Children.OfType<LedLetter>().Should().AllSatisfy(led => led.Active.Should().BeFalse());
    }

    [AvaloniaFact]
    public void RedrawOnTheMinuteWithoutBeingForced()
    {
        var view = new TimeInWordsView();
        var grid = new TimeGridEnglish();
        view.Initialise(new TimeInWordsSettings { Debug = true }, grid);
        view.Time = new DateTime(2024, 1, 1, 2, 10, 0);
        view.TimeAsText = new TimeToTextFormat { TimeAsText = "IT IS TEN PAST TWO", AdditionalMinutes = 0 };
        var expected = grid.GetBitMask(view.TimeAsText.TimeAsText, true).Mask;
        view.GridBitMask = expected;

        view.Update();

        view.TimeAsTextLabel.Text.Should().Be(view.TimeAsText.ToString());
        LitLetters(view, grid).Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    // the letter grid is offset by one row and column to make room for the additional minute LEDs
    private static bool[][] LitLetters(TimeInWordsView view, TimeGrid grid) =>
        Enumerable
            .Range(1, grid.GridHeight)
            .Select(row =>
                Enumerable
                    .Range(1, grid.GridWidth)
                    .Select(column => LedAt<LedLetter>(view, row, column).Active)
                    .ToArray()
            )
            .ToArray();

    private static T LedAt<T>(TimeInWordsView view, int row, int column)
        where T : LedLetter =>
        view
            .DisplayGrid.Children.OfType<LedLetter>()
            .Should()
            .ContainSingle(led => Grid.GetRow(led) == row && Grid.GetColumn(led) == column)
            .Which.Should()
            .BeOfType<T>()
            .Which;
}
