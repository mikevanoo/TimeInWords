using Avalonia.Headless.XUnit;
using Avalonia.Media;
using Avalonia.Threading;
using TimeInWords.Controls;

namespace TimeInWords.Tests.Controls;

public class LedLetterShould
{
    private readonly TimeInWordsSettings _settings = new();

    [AvaloniaFact]
    public void ShowCorrectText()
    {
        var expectedText = "test text";

        var ledLetter = new LedLetter(_settings, expectedText);

        ledLetter.Text.Should().Be(expectedText);
    }

    [AvaloniaFact]
    public void UseInactiveColorInitially()
    {
        var ledLetter = new LedLetter(_settings, "X");

        ledLetter.Active.Should().BeFalse();
        ForegroundOf(ledLetter).Should().Be(_settings.InactiveFontColour);
    }

    [AvaloniaFact]
    public void FadeRatherThanSwitchColorWhenGoingActive()
    {
        var ledLetter = new LedLetter(_settings, "X") { Active = true };

        Dispatcher.UIThread.RunJobs();

        // at the default speed the fade has only just started, so the colour has not changed yet
        ledLetter.FadeStepDelayMs.Should().Be(ColorFader.DefaultStepDelayMs);
        ForegroundOf(ledLetter).Should().Be(_settings.InactiveFontColour);
    }

    [AvaloniaFact]
    public void UseActiveColorWhenGoingActive()
    {
        var ledLetter = new LedLetter(_settings, "X") { FadeStepDelayMs = 0, Active = true };

        Dispatcher.UIThread.RunJobs();

        ForegroundOf(ledLetter).Should().Be(_settings.ActiveFontColour);
    }

    [AvaloniaFact]
    public void UseInactiveColorWhenGoingInactive()
    {
        var ledLetter = new LedLetter(_settings, "X") { FadeStepDelayMs = 0, Active = true };

        Dispatcher.UIThread.RunJobs();

        ledLetter.Active = false;
        Dispatcher.UIThread.RunJobs();

        ForegroundOf(ledLetter).Should().Be(_settings.InactiveFontColour);
    }

    [AvaloniaFact]
    public void NotFadeWhenSetToItsCurrentState()
    {
        var ledLetter = new LedLetter(_settings, "X") { FadeStepDelayMs = 0, Active = false };
        var foreground = ledLetter.Foreground;

        Dispatcher.UIThread.RunJobs();

        ledLetter.Foreground.Should().BeSameAs(foreground);
    }

    private static Color ForegroundOf(LedLetter ledLetter) =>
        ledLetter.Foreground.Should().BeOfType<SolidColorBrush>().Which.Color;
}
