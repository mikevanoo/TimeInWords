using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TimeInWordsScreensaver
{
    public partial class ScreenSaverForm : Form
    {
        #region Properties

        private WordClockSettings _settings;
        private bool _isScreensaver;
        public bool IsScreensaver
        {
            get { return _isScreensaver; }
            set
            {
                _isScreensaver = value;
                if (value)
                {
                    // Capture the mouse
                    Capture = true;

                    // Set the application to full screen mode and hide the mouse
                    ShowCursor(false);
                    ToggleFullscreen(FullscreenMode.ForceFullscreen);
                    Bounds = Screen.PrimaryScreen.Bounds;

                    KeyDown += Screensaver_KeyDown;
                    if (!WordClockSettings.IsRunningOnMono())
                    {
                        MouseMove += Screensaver_MouseMove;
                    }
                    FormClosing += Screensaver_FormClosing;
                    MouseDoubleClick -= ToggleFullscreen;
                    KeyDown -= Window_KeyDown;

                    ShowInTaskbar = false;
                }
                else
                {
                    Capture = false;
                    ShowCursor(true);
                    ToggleFullscreen(FullscreenMode.PreventFullscreen);

                    KeyDown -= Screensaver_KeyDown;
                    if (!WordClockSettings.IsRunningOnMono())
                    {
                        MouseMove -= Screensaver_MouseMove;
                    }
                    FormClosing -= Screensaver_FormClosing;
                    MouseDoubleClick += ToggleFullscreen;
                    KeyDown += Window_KeyDown;

                    ShowInTaskbar = true;
                }
            }
        }

        private IntPtr previewWndHandle;
        private bool _isPreview;
        public bool IsPreview
        {
            get { return _isPreview; }
            set
            {
                _isPreview = value;
                if (value)
                {
                    // Set the preview window as the parent of this window
                    SetParent(Handle, previewWndHandle);

                    // Make this a child window so it will close when the parent dialog closes
                    // GWL_STYLE = -16, WS_CHILD = 0x40000000
                    _ = SetWindowLong(Handle, -16, new IntPtr(GetWindowLong(Handle, -16) | 0x40000000));

                    // Place our window inside the parent
                    Rectangle ParentRect;
                    GetClientRect(previewWndHandle, out ParentRect);
                    Size = ParentRect.Size;
                    Location = new Point(0, 0);

                    ShowCursor(true);
                }
                else
                {
                    // Set the preview window as the parent of this window
                    //SetParent(this.Handle, IntPtr.Zero);

                    //// Make this a child window so it will close when the parent dialog closes
                    //// GWL_STYLE = -16, WS_CHILD = 0x40000000
                    //SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

                    //// Place our window inside the parent
                    //Rectangle ParentRect;
                    //GetClientRect(PreviewWndHandle, out ParentRect);
                    //Size = ParentRect.Size;
                    //Location = new Point(0, 0);
                }
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

        #endregion

        #region Constructors

        public ScreenSaverForm(
            WordClockSettings settings,
            bool isScreensaver = false,
            IntPtr previewHandle = default(IntPtr)
        )
        {
            _settings = settings;
            previewWndHandle = previewHandle;

            InitializeComponent();

            IsScreensaver = isScreensaver;
            IsPreview = (previewHandle != default(IntPtr));
        }

        #endregion

        #region Events

        private void Screensaver_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsScreensaver && !IsPreview)
            {
                Close();
            }
        }

        static int OldX,
            OldY;

        private void Screensaver_MouseMove(object sender, MouseEventArgs e)
        {
            //Determines whether the mouse was moved and whether the movement was large.
            //if so, the screen saver is ended.
            if ((OldX > 0 & OldY > 0) & (Math.Abs(e.X - OldX) > 3 | Math.Abs(e.Y - OldY) > 3))
            {
                if (IsScreensaver && !IsPreview)
                {
                    Close();
                }
            }

            //Assigns the current X and Y locations to OldX and OldY.
            OldX = e.X;
            OldY = e.Y;
        }

        private void Screensaver_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShowCursor(true);
        }

        #endregion

        #region Fullscreen

        private void ToggleFullscreen(object sender, MouseEventArgs e)
        {
            ToggleFullscreen();
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
                (
                    mode != FullscreenMode.PreventFullscreen
                    && FormBorderStyle == FormBorderStyle.Sizable
                    && WindowState == FormWindowState.Normal
                    && !TopMost
                ) // && OptionsBTN.Visible)
                || (mode == FullscreenMode.ForceFullscreen)
            )
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = true;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                ToggleFullscreen();
            if (e.KeyCode == Keys.Escape)
                ToggleFullscreen(FullscreenMode.PreventFullscreen);
        }

        #endregion Fullscreen

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion


        #region Native
#pragma warning disable SYSLIB1054

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

#pragma warning restore SYSLIB1054
        #endregion
    }
}
