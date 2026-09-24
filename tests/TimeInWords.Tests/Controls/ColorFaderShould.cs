using Avalonia.Headless.XUnit;
using Avalonia.Media;
using TimeInWords.Controls;

namespace TimeInWords.Tests.Controls;

public class ColorFaderShould
{
    [AvaloniaFact]
    public async Task FadeToEndColor()
    {
        var startColor = Color.FromRgb(255, 255, 255);
        var endColor = Color.FromRgb(0, 0, 0);

        var control = Substitute.For<IFadeableControl>();
        control.Foreground = new SolidColorBrush(startColor);

        await ColorFader.FadeForegroundAsync(control, endColor, intervals: 3, stepDelayMs: 0);

        control.Foreground.Should().BeOfType<SolidColorBrush>().Which.Color.Should().Be(endColor);
    }

    [AvaloniaFact]
    public async Task StepEachChannelTowardsTheEndColorIndependently()
    {
        // red rises, green falls and blue stays put, each in equal steps from the start colour
        var control = new RecordingControl(Color.FromRgb(10, 200, 100));

        await ColorFader.FadeForegroundAsync(control, Color.FromRgb(100, 50, 100), intervals: 3, stepDelayMs: 0);

        control
            .AppliedColors.Should()
            .Equal(
                Color.FromRgb(10, 200, 100),
                Color.FromRgb(40, 150, 100),
                Color.FromRgb(70, 100, 100),
                Color.FromRgb(100, 50, 100)
            );
    }

    [AvaloniaFact]
    public async Task EndExactlyOnTheEndColorWhenTheStepsDoNotDivideEvenly()
    {
        // 10 / 3 rounds down to steps of 3, so the last step has to make up the difference
        var control = new RecordingControl(Color.FromRgb(0, 0, 0));

        await ColorFader.FadeForegroundAsync(control, Color.FromRgb(10, 10, 10), intervals: 3, stepDelayMs: 0);

        control
            .AppliedColors.Should()
            .Equal(Color.FromRgb(0, 0, 0), Color.FromRgb(3, 3, 3), Color.FromRgb(6, 6, 6), Color.FromRgb(10, 10, 10));
    }

    [AvaloniaFact]
    public async Task LeaveTheForegroundAloneWhenItIsNotASolidColor()
    {
        var gradient = new LinearGradientBrush();
        var control = Substitute.For<IFadeableControl>();
        control.Foreground = gradient;

        await ColorFader.FadeForegroundAsync(control, Colors.Black, intervals: 3, stepDelayMs: 0);

        control.Foreground.Should().BeSameAs(gradient);
    }

    [AvaloniaTheory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public async Task RejectIntervalsThatAreNotPositive(int intervals)
    {
        var control = new RecordingControl(Colors.White);

        var fade = () => ColorFader.FadeForegroundAsync(control, Colors.Black, intervals, stepDelayMs: 0);

        (await fade.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>())
            .WithParameterName(nameof(intervals))
            .Which.ActualValue.Should()
            .Be(intervals);
        control.AppliedColors.Should().BeEmpty();
    }

    [AvaloniaFact]
    public async Task RejectIntervalsThatAreNotPositiveEvenWhenThereIsNothingToFade()
    {
        var control = Substitute.For<IFadeableControl>();
        control.Foreground = new LinearGradientBrush();

        var fade = () => ColorFader.FadeForegroundAsync(control, Colors.Black, intervals: 0, stepDelayMs: 0);

        await fade.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("intervals");
    }

    [AvaloniaFact]
    public async Task RejectAMissingControl()
    {
        var fade = () => ColorFader.FadeForegroundAsync(null!, Colors.Black, intervals: 3, stepDelayMs: 0);

        await fade.Should().ThrowExactlyAsync<ArgumentNullException>().WithParameterName("control");
    }

    [AvaloniaFact]
    public async Task NotChangeTheColorWhenCancelledBeforeTheFadeStarts()
    {
        var control = new RecordingControl(Colors.White);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await ColorFader.FadeForegroundAsync(control, Colors.Black, intervals: 3, stepDelayMs: 0, cts.Token);

        control.AppliedColors.Should().BeEmpty();
    }

    [AvaloniaFact]
    public async Task StopPartWayWhenCancelledDuringTheFade()
    {
        using var cts = new CancellationTokenSource();
        var control = new RecordingControl(Colors.White);
        control.ColorSet += (_, _) =>
        {
            if (control.AppliedColors.Count == 2)
            {
                cts.Cancel();
            }
        };

        await ColorFader.FadeForegroundAsync(control, Colors.Black, intervals: 3, stepDelayMs: 0, cts.Token);

        // cancelling ends the fade quietly: no exception, and the end colour is never applied
        control.AppliedColors.Should().HaveCount(2);
        control.AppliedColors.Should().NotContain(Colors.Black);
    }

    private sealed class RecordingControl(Color startColor) : IFadeableControl
    {
        public event EventHandler? ColorSet;

        public List<Color> AppliedColors { get; } = [];

        public IBrush? Foreground
        {
            get;
            set
            {
                field = value;
                AppliedColors.Add(value.Should().BeOfType<SolidColorBrush>().Which.Color);
                ColorSet?.Invoke(this, EventArgs.Empty);
            }
        } = new SolidColorBrush(startColor);
    }
}
