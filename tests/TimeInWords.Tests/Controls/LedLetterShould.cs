// using TimeInWordsApp.Controls;
//
// namespace TimeInWordsApp.Tests.Controls;
//
// public class LedLetterShould
// {
//     [Fact]
//     public void ShowCorrectText()
//     {
//         var settings = new TimeInWordsSettings();
//         var expectedText = "test text";
//
//         var ledLetter = new LedLetter(settings, expectedText);
//
//         ledLetter.Text.Should().Be(expectedText);
//     }
//
//     [Fact]
//     public void UseInactiveColorInitially()
//     {
//         var settings = new TimeInWordsSettings();
//         var ledLetter = new LedLetter(settings, "X");
//
//         ledLetter.ForeColor.Should().Be(settings.InactiveFontColour);
//     }
//
//     [Fact]
//     public async Task UseActiveColorWhenGoingActive()
//     {
//         var settings = new TimeInWordsSettings();
//         var ledLetter = new LedLetter(settings, "X");
//
//         ledLetter.Active = true;
//         await WaitForColorFade();
//
//         ledLetter.ForeColor.Should().Be(settings.ActiveFontColour);
//     }
//
//     [Fact]
//     public async Task UseInactiveColorWhenGoingInactive()
//     {
//         var settings = new TimeInWordsSettings();
//         var ledLetter = new LedLetter(settings, "X");
//
//         ledLetter.Active = true;
//         await WaitForColorFade();
//
//         ledLetter.Active = false;
//         await WaitForColorFade();
//
//         ledLetter.ForeColor.Should().Be(settings.InactiveFontColour);
//     }
//
//     private static async Task WaitForColorFade() => await Task.Delay(750);
// }
