using System;
using System.IO;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using TimeInWords.Presenters;

namespace TimeInWords.Views;

public partial class MainView : Window, IMainView
{
    private TimeInWordsSettings Settings { get; set; } = null!;

    private bool IsFullScreen
    {
        get;
        set
        {
            field = value;
            if (value)
            {
                ShowCursor(false);
                ToggleFullscreen(FullscreenMode.ForceFullscreen);
                ShowInTaskbar = false;

                PointerMoved += OnPointerMoved;
                KeyDown += OnKeyDownFullScreen;

                PointerPressed -= OnPointerPressed;
                KeyDown -= OnKeyDownNotFullScreen;
            }
            else
            {
                ShowCursor(true);
                ToggleFullscreen(FullscreenMode.PreventFullscreen);
                ShowInTaskbar = true;

                PointerPressed += OnPointerPressed;
                KeyDown += OnKeyDownNotFullScreen;

                PointerMoved -= OnPointerMoved;
                KeyDown -= OnKeyDownFullScreen;
            }
        }
    }

    public MainView(TimeInWordsSettings settings, bool isFullScreen)
        : this()
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));

        InitializeComponent();

        ITimeInWordsView timeInWordsView = new TimeInWordsView();
        Content = timeInWordsView;

        var dateTimeProvider = new DateTimeProvider();
        ITimer timer = new TimeInWordsTimer();
        _ = new TimeInWordsPresenter(timeInWordsView, Settings, dateTimeProvider, timer);

        IsFullScreen = isFullScreen;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public MainView()
    {
        // needed to suppress Avalonia warning
        // AVLN3001: XAML resource "avares://TimeInWords/Views/MainView.axaml" won't be reachable via runtime loader, as no public constructor was found
    }

    public void Show(int x, int y, int width, int height)
    {
        WindowStartupLocation = WindowStartupLocation.Manual;
        Position = new PixelPoint(x, y);
        Width = width;
        Height = height;
        Show();
    }

    private enum FullscreenMode
    {
        Nothing,
        ForceFullscreen,
        PreventFullscreen,
    }

    private void ToggleFullscreen(FullscreenMode mode = FullscreenMode.Nothing)
    {
        if (
            (mode != FullscreenMode.PreventFullscreen && WindowState == WindowState.Normal)
            || mode == FullscreenMode.ForceFullscreen
        )
        {
            SystemDecorations = SystemDecorations.None;
            WindowState = WindowState.FullScreen;
            Topmost = true;
            Focus();
        }
        else
        {
            SystemDecorations = SystemDecorations.Full;
            WindowState = WindowState.Normal;
            Topmost = false;
        }
    }

    private void OnKeyDownFullScreen(object? sender, KeyEventArgs e)
    {
        if (IsFullScreen)
        {
            Close();
        }
    }

    private void OnKeyDownNotFullScreen(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.F11:
                ToggleFullscreen();
                break;
            case Key.Escape:
                ToggleFullscreen(FullscreenMode.PreventFullscreen);
                break;
        }
    }

    private double _oldX;
    private double _oldY;

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        const int MovementThreshold = 25;

        var position = e.GetPosition(this);

        // Determines whether the mouse was moved and whether the movement was large.
        // if so, the screen saver is ended.
        if (
            (_oldX > 0 & _oldY > 0)
            & (Math.Abs(position.X - _oldX) > MovementThreshold | Math.Abs(position.Y - _oldY) > MovementThreshold)
        )
        {
            if (IsFullScreen)
            {
                Close();
            }
        }

        _oldX = position.X;
        _oldY = position.Y;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // double-click?
        if (e.ClickCount == 2)
        {
            ToggleFullscreen();
        }
    }

    private void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor = Cursor.Default;
        }
        else
        {
            var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;
            var bitMap = new Bitmap(Path.Combine(directoryPath, "transparent-cursor.ico"));
            var transparentCursor = new Cursor(bitMap, new PixelPoint(0, 0));
            Cursor = transparentCursor;
        }
    }
}
