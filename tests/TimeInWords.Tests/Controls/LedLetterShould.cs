using Avalonia.Headless.XUnit;
using Avalonia.Media;
using TimeInWords.Controls;

namespace TimeInWords.Tests.Controls;

public class LedLetterShould
{
    [AvaloniaFact]
    public void ShowCorrectText()
    {
        var settings = new TimeInWordsSettings();
        var expectedText = "test text";

        var ledLetter = new LedLetter(settings, expectedText);

        ledLetter.Text.Should().Be(expectedText);
    }

    [AvaloniaFact]
    public void UseInactiveColorInitially()
    {
        var settings = new TimeInWordsSettings();
        var ledLetter = new LedLetter(settings, "X");

        (ledLetter.Foreground as SolidColorBrush)?.Color.Should().Be(settings.InactiveFontColour);
    }

    [AvaloniaFact]
    public async Task UseActiveColorWhenGoingActive()
    {
        var settings = new TimeInWordsSettings();
        var ledLetter = new LedLetter(settings, "X") { Active = true };

        await WaitForColorFade();

        (ledLetter.Foreground as SolidColorBrush)?.Color.Should().Be(settings.ActiveFontColour);
    }

    [AvaloniaFact]
    public async Task UseInactiveColorWhenGoingInactive()
    {
        var settings = new TimeInWordsSettings();
        var ledLetter = new LedLetter(settings, "X") { Active = true };

        await WaitForColorFade();

        ledLetter.Active = false;
        await WaitForColorFade();

        (ledLetter.Foreground as SolidColorBrush)?.Color.Should().Be(settings.InactiveFontColour);
    }

    private static async Task WaitForColorFade() => await Task.Delay(750);
}
