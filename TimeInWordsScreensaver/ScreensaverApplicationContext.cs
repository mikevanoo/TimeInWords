using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeInWordsScreensaver
{
    internal class ScreensaverApplicationContext : ApplicationContext
    {
        private int _formCount;
        
        public ScreensaverApplicationContext(bool isScreensaver = false, IntPtr previewHandle = default(IntPtr))
            : base()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                ScreenSaverForm form = new ScreenSaverForm(isScreensaver, previewHandle);
                form.Closed += OnFormClosed; 
                
                // position the form on the relevant screen
                form.StartPosition = FormStartPosition.Manual;
                Rectangle bounds = screen.Bounds;
                form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                form.Show();

                _formCount++;
            }
        }
        
        private void OnFormClosed(object sender, EventArgs e) 
        {
            _formCount--;
            if (_formCount == 0) 
            {
                ExitThread();
            }
        }
    }
}
