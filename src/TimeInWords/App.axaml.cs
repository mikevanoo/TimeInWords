using Avalonia;
using Avalonia.Markup.Xaml;

namespace TimeInWords;

public class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
}
