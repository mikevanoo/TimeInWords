using Avalonia.Media;

namespace TimeInWords.Controls;

public interface IFadeableControl
{
    public IBrush? Foreground { get; set; }
}
