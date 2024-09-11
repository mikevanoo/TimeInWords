using System;
using System.Drawing;

namespace TimeInWordsApp.Controls;

public interface IFadeableControl
{
    public bool InvokeRequired { get; }
    public object Invoke(Delegate method);
    public Color ForeColor { get; set; }
}
