using TimeInWordsApp.Controls;

namespace TimeInWordsApp.Tests.Controls;

public class LedLightShould
{
    [Fact]
    public void ShowCorrectText()
    {
        var settings = new TimeInWordsSettings();
        var expectedText = char.ConvertFromUtf32(9679);

        var ledLetter = new LedLight(settings);

        ledLetter.Text.Should().Be(expectedText);
    }
}
