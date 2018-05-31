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
        private readonly List<Form> _forms = new List<Form>();
        
        public ScreensaverApplicationContext(bool isScreensaver = false, IntPtr previewHandle = default(IntPtr))
            : base()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                ScreenSaverForm form = new ScreenSaverForm(isScreensaver, previewHandle);
                _forms.Add(form);
                form.Closed += OnFormClosed;
                
                // position the form on the relevant screen
                form.StartPosition = FormStartPosition.Manual;
                Rectangle bounds = screen.Bounds;
                form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                form.Show();
            }
        }
        
        private void OnFormClosed(object sender, EventArgs e) 
        {
            // if one form closes, close all of them and then exit
            foreach (Form form in _forms)
            {
                form.Closed -= OnFormClosed;
                form.Close();
            }
            ExitThread();
        }
    }
}
