using Avalonia;
using Avalonia.Controls;

namespace TimeInWords.Views;

public partial class MainView : Window, IMainView
{
    // private TimeInWordsView timeInWordsView;
    // private DateTimeProvider dateTimeProvider;
    // private ITimer timer;
    // private TimeInWordsPresenter timeInWordsPresenter;

    public MainView(TimeInWordsSettings settings, bool isFullScreen)
    {
        InitializeComponent();

        // timeInWordsView = new TimeInWordsView();
        // dateTimeProvider = new DateTimeProvider();
        // timer = new TimeInWordsTimer();
        // timeInWordsPresenter = new TimeInWordsPresenter(
        //     this.timeInWordsView,
        //     this.Settings,
        //     this.dateTimeProvider,
        //     this.timer
        // );
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
