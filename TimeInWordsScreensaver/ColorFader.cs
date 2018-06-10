using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeInWordsScreensaver
{
    public class ColorFader
    {
        private readonly Color _from;
        private readonly Color _to;

        private readonly double _stepR;
        private readonly double _stepG;
        private readonly double _stepB;

        private readonly uint _intervals;

        private readonly Control _control;

        public static void SetControlForeColor(Control control, Color toColor, uint intervals = 20, int sleep = 20)
        {
            ColorFader colorFader = new ColorFader(control, control.ForeColor, toColor, intervals);

            Task t = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(sleep);
                foreach (Color color in colorFader.Fade())
                {
                    colorFader.SetControlForeColor(color);
                    await Task.Delay(sleep);
                }
            });
        }
        
        private ColorFader(Control control, Color from, Color to, uint intervals)
        {
            _control = control ?? throw new ArgumentNullException(nameof(control));

            if (intervals == 0)
            {
                throw new ArgumentException($"{nameof(intervals)} must be a positive number");
            }
                
            _from = from;
            _to = to;
            _intervals = intervals;

            _stepR = (double)(_to.R - _from.R) / _intervals;
            _stepG = (double)(_to.G - _from.G) / _intervals;
            _stepB = (double)(_to.B - _from.B) / _intervals;
        }

        private IEnumerable<Color> Fade()
        {
            for (uint i = 0; i < _intervals; ++i)
            {
                yield return Color.FromArgb((int)(_from.R + i * _stepR), (int)(_from.G + i * _stepG), (int)(_from.B + i * _stepB));
            }
            yield return _to; // make sure we always return the exact target color last
        }

        private void SetControlForeColor(Color color)
        {
            if (_control.InvokeRequired)
            {
                _control.Invoke((MethodInvoker) delegate { _control.ForeColor = color; });
            }
            else
            {
                _control.ForeColor = color;
            }
        }
    }
}
