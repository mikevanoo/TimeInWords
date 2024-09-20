using Avalonia.Headless.XUnit;
using Avalonia.Media;
using TextToTimeGridLib;
using TextToTimeGridLib.Grids;
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

        view.Initialise(settings, new TimeGridEnglish());

        // +2 on the row and column counts to accommodate the additional minute LEDs
        view.DisplayGrid.ColumnDefinitions.Should().HaveCount(TimeGrid.GridWidth + 2);
        view.DisplayGrid.RowDefinitions.Should().HaveCount(TimeGrid.GridHeight + 2);
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
}
