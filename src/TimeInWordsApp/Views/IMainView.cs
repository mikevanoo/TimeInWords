using System;
using System.Windows.Forms;

namespace TimeInWordsApp.Views;

public interface IMainView
{
    public TimeInWordsSettings Settings { get; set; }
    public bool IsFullScreen { get; set; }
    public FormStartPosition StartPosition { get; set; }

    public IMainView CreateNewInstance(bool isFullScreen);
    public void SetBounds(int x, int y, int width, int height);
    public void Show();
    public void Close();
    public event EventHandler Closed;
}
