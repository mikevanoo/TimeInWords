using System.Collections.Generic;

namespace TimeInWords;

public interface IScreenProvider
{
    public IReadOnlyList<ScreenArea> GetScreens();
}
