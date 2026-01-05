using System;
using System.Collections.Generic;
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

    public static void SetControlForeColor(IFadeableControl control, Color toColor, int intervals = 20, int sleep = 20)
    {
        var currentColor = (control.Foreground as SolidColorBrush)?.Color;
        var colorFader = new ColorFader(control, currentColor, toColor, intervals);

        Task.Factory.StartNew(async () =>
        {
            await Task.Delay(sleep);
            foreach (var color in colorFader.Fade())
            {
                colorFader.SetControlForeColor(color);
                await Task.Delay(sleep);
            }
        });
    }

    private ColorFader(IFadeableControl control, Color? fromColor, Color toColor, int intervals)
    {
        if (!fromColor.HasValue)
        {
            throw new ArgumentNullException(nameof(fromColor));
        }

        _control = control ?? throw new ArgumentNullException(nameof(control));

        if (intervals == 0)
        {
            throw new ArgumentException($"{nameof(intervals)} must be a positive number");
        }

        _fromColor = fromColor.Value;
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

    private void SetControlForeColor(Color color) =>
        Dispatcher.UIThread.Post(() => _control.Foreground = new SolidColorBrush(color));
}
