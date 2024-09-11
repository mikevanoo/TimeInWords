using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TimeInWordsApp.Views;

public partial class MainView : Form, IMainView
{
    private bool _isFullScreen;

    public TimeInWordsSettings Settings { get; set; }

    public bool IsFullScreen
    {
        get => _isFullScreen;
        set
        {
            _isFullScreen = value;
            if (value)
            {
                Capture = true;
                ShowCursor(false);
                ToggleFullscreen(FullscreenMode.ForceFullscreen);
                Bounds = Screen.PrimaryScreen.Bounds;

                KeyDown += MainView_KeyDown;
                if (!TimeInWordsSettings.IsRunningOnMono())
                {
                    MouseMove += MainView_MouseMove;
                }
                FormClosing += MainView_FormClosing;
                MouseDoubleClick -= ToggleFullscreen;
                KeyDown -= Window_KeyDown;

                ShowInTaskbar = false;
            }
            else
            {
                Capture = false;
                ShowCursor(true);
                ToggleFullscreen(FullscreenMode.PreventFullscreen);

                KeyDown -= MainView_KeyDown;
                if (!TimeInWordsSettings.IsRunningOnMono())
                {
                    MouseMove -= MainView_MouseMove;
                }
                FormClosing -= MainView_FormClosing;
                MouseDoubleClick += ToggleFullscreen;
                KeyDown += Window_KeyDown;

                ShowInTaskbar = true;
            }
        }
    }

    public IMainView CreateNewInstance(bool isFullScreen) => new MainView(Settings, isFullScreen);

    public MainView(TimeInWordsSettings settings, bool isFullScreen)
    {
        Settings = settings;
        InitializeComponent();
        IsFullScreen = isFullScreen;
    }

    private void MainView_KeyDown(object sender, KeyEventArgs e)
    {
        if (IsFullScreen)
        {
            Close();
        }
    }

    private static int _oldX;
    private static int _oldY;

    private void MainView_MouseMove(object sender, MouseEventArgs e)
    {
        // Determines whether the mouse was moved and whether the movement was large.
        // if so, the screen saver is ended.
        if ((_oldX > 0 & _oldY > 0) & (Math.Abs(e.X - _oldX) > 3 | Math.Abs(e.Y - _oldY) > 3))
        {
            if (IsFullScreen)
            {
                Close();
            }
        }

        // Assigns the current X and Y locations to OldX and OldY.
        _oldX = e.X;
        _oldY = e.Y;
    }

    private void MainView_FormClosing(object sender, FormClosingEventArgs e) => ShowCursor(true);

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F11:
                ToggleFullscreen();
                break;
            case Keys.Escape:
                ToggleFullscreen(FullscreenMode.PreventFullscreen);
                break;
            default:
                // do nothing
                break;
        }
    }

    private void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor = Cursors.Default;
            Cursor.Show();
        }
        else
        {
            Cursor = new Cursor(
                Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "transparent-cursor.ico")
            );
            Cursor.Hide();
        }
    }

    private void ToggleFullscreen(object sender, MouseEventArgs e) => ToggleFullscreen();

    private enum FullscreenMode
    {
        Nothing,
        ForceFullscreen,
        PreventFullscreen,
    }

    private void ToggleFullscreen(FullscreenMode mode = FullscreenMode.Nothing)
    {
        if (
            (
                mode != FullscreenMode.PreventFullscreen
                && FormBorderStyle == FormBorderStyle.Sizable
                && WindowState == FormWindowState.Normal
                && !TopMost
            ) || (mode == FullscreenMode.ForceFullscreen)
        )
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
            BringToFront();
            Focus();
        }
        else
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            TopMost = false;
        }
    }

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }
}
