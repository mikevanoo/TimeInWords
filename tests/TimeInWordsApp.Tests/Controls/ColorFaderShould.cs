﻿using System.Drawing;
using TimeInWordsApp.Controls;

namespace TimeInWordsApp.Tests.Controls;

public class ColorFaderShould
{
    [Fact]
    public async Task FadeToEndColor()
    {
        var control = Substitute.For<IFadeableControl>();
        control.ForeColor = Color.White;

        ColorFader.SetControlForeColor(control, Color.Black, 3, 10);

        await Task.Delay(500); // ensure we leave plenty of time to the fade to occur

        control.ForeColor.Should().Be(Color.Black);
    }
}
