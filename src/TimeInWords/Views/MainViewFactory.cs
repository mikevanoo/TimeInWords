namespace TimeInWords.Views;

public class MainViewFactory : IMainViewFactory
{
    public IMainView Create(TimeInWordsSettings settings, bool isFullScreen) => new MainView(settings, isFullScreen);
}
