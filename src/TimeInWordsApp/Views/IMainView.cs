using System;
using System.Windows.Forms;

namespace TimeInWordsApp.Views;

public interface IMainView
{
    public void Show();
    public void Show(int x, int y, int width, int height);
    public void Close();
    public event EventHandler Closed;
}
