﻿using Avalonia.Headless.XUnit;
using TimeInWords.Controls;

namespace TimeInWords.Tests.Controls;

public class LedLightShould
{
    [AvaloniaFact]
    public void ShowCorrectText()
    {
        var settings = new TimeInWordsSettings();
        var expectedText = char.ConvertFromUtf32(9679);

        var ledLetter = new LedLight(settings);

        ledLetter.Text.Should().Be(expectedText);
    }
}
