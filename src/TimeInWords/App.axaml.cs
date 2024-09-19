using Avalonia;
using Avalonia.Markup.Xaml;

namespace TimeInWords;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
}
