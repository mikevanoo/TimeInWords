namespace TimeInWords.Views;

public interface IMainViewFactory
{
    public IMainView Create(TimeInWordsSettings settings, bool isFullScreen);
}
