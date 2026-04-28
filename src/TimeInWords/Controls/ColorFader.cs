using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Threading;

namespace TimeInWords.Controls;

public class ColorFader
{
    private readonly Color _fromColor;
    private readonly Color _toColor;

    private readonly byte _stepR;
    private readonly byte _stepG;
    private readonly byte _stepB;

    private readonly int _intervals;

    private readonly IFadeableControl _control;

    public static Task FadeForegroundAsync(
        IFadeableControl control,
        Color toColor,
        int intervals = 20,
        int stepDelayMs = 20,
        CancellationToken cancellationToken = default
    )
    {
        if (control.Foreground is not SolidColorBrush brush)
        {
            return Task.CompletedTask;
        }

        var fader = new ColorFader(control, brush.Color, toColor, intervals);

        return Dispatcher.UIThread.InvokeAsync(async () =>
        {
            try
            {
                await Task.Delay(stepDelayMs, cancellationToken);
                foreach (var color in fader.Fade())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    fader._control.Foreground = new SolidColorBrush(color);
                    await Task.Delay(stepDelayMs, cancellationToken);
                }
            }
            catch (OperationCanceledException) { }
        });
    }

    private ColorFader(IFadeableControl control, Color fromColor, Color toColor, int intervals)
    {
        _control = control ?? throw new ArgumentNullException(nameof(control));

        if (intervals == 0)
        {
            throw new ArgumentException($"{nameof(intervals)} must be a positive number");
        }

        _fromColor = fromColor;
        _toColor = toColor;
        _intervals = intervals;

        _stepR = (byte)((_toColor.R - _fromColor.R) / _intervals);
        _stepG = (byte)((_toColor.G - _fromColor.G) / _intervals);
        _stepB = (byte)((_toColor.B - _fromColor.B) / _intervals);
    }

    private IEnumerable<Color> Fade()
    {
        for (var i = 0; i < _intervals; ++i)
        {
            yield return Color.FromRgb(
                (byte)(_fromColor.R + i * _stepR),
                (byte)(_fromColor.G + i * _stepG),
                (byte)(_fromColor.B + i * _stepB)
            );
        }
        yield return _toColor; // make sure we always return the exact target color last
    }
}
