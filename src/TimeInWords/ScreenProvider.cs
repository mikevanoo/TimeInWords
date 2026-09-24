using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace TimeInWords;

public class ScreenProvider : IScreenProvider
{
    // the platform only exposes its screens through a window
    public IReadOnlyList<ScreenArea> GetScreens() =>
        new Window().Screens.All.Select(screen => new ScreenArea(screen.WorkingArea, screen.Scaling)).ToList();
}
