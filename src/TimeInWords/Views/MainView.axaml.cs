using Avalonia;
using Avalonia.Controls;
using TimeInWords.Presenters;

namespace TimeInWords.Views;

public partial class MainView : Window, IMainView
{
    private ITimeInWordsView timeInWordsView;
    private DateTimeProvider dateTimeProvider;
    private ITimer timer;
    private TimeInWordsPresenter timeInWordsPresenter;

    public MainView(TimeInWordsSettings settings, bool isFullScreen)
    {
        InitializeComponent();

        timeInWordsView = new TimeInWordsView();
        Content = timeInWordsView;

        dateTimeProvider = new DateTimeProvider();
        timer = new TimeInWordsTimer();
        timeInWordsPresenter = new TimeInWordsPresenter(timeInWordsView, settings, dateTimeProvider, timer);
    }

    public void Show(int x, int y, int width, int height)
    {
        WindowStartupLocation = WindowStartupLocation.Manual;
        Position = new PixelPoint(x, y);
        Width = width;
        Height = height;
        Show();
    }
}
