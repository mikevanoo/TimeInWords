using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Media;

namespace TimeInWords.Controls;

[SuppressMessage(
    "Microsoft.Design",
    "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
    Justification = "Avalonia controls are not disposed by the framework; cleanup is performed in OnDetachedFromLogicalTree."
)]
internal class LedLetter : TextBlock, IFadeableControl
{
    private readonly TimeInWordsSettings _settings;
    private CancellationTokenSource? _fadeCts;

    public bool Active
    {
        get;
        set
        {
            if (field != value)
            {
                var endColor = value ? _settings.ActiveFontColour : _settings.InactiveFontColour;

                field = value;

                // Handle fading overlaps by cancelling the current one
                _fadeCts?.Cancel();
                _fadeCts?.Dispose();
                _fadeCts = new CancellationTokenSource();
                _ = ColorFader.FadeForegroundAsync(this, endColor, cancellationToken: _fadeCts.Token);
            }
        }
    }

    public LedLetter(TimeInWordsSettings settings, string text)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        FontFamily = new FontFamily("Inter");
        FontSize = 37.5f;
        TextAlignment = TextAlignment.Center;
        Text = text;
        Foreground = new SolidColorBrush(_settings.InactiveFontColour);
        Background = new SolidColorBrush(_settings.BackgroundColour);
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        // Handle active fading by cancelling
        _fadeCts?.Cancel();
        _fadeCts?.Dispose();
        _fadeCts = null;
        base.OnDetachedFromLogicalTree(e);
    }
}
