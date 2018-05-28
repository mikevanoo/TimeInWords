using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeToTextLib;

namespace TimeInWordsScreensaver
{
    public partial class WordClockPanel : Panel
    {
        #region Member Data

        private WordClockSettings _settings;

        internal WordClockSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                UpdateSettings(_settings);
            }
        }

        #endregion

        #region Constructor

        public WordClockPanel()
        {
            InitializeComponent();

            // Use double buffering to improve drawing performance
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            Settings = new WordClockSettings();
            SetTime();
        }

        #endregion

        #region Events
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            SetTime();
        }

        #endregion

        #region Private Methods

        private void SetTime()
        {
            DateTime now = DateTime.Now;
            lblTime.Text = now.ToLongTimeString();
            lblTimeAsText.Text = TimeToText.GetSimple(LanguagePreset.Language.English, now);
        }

        private void UpdateSettings(WordClockSettings settings)
        {
            BackColor = settings.BackgroundColour;
            lblTime.ForeColor = settings.ActiveFontColour;
            lblTimeAsText.ForeColor = settings.ActiveFontColour;
        }


        #endregion
    }
}
