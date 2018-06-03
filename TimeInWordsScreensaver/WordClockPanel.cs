using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextToTimeGridLib;
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
            
            Settings = new WordClockSettings();
            Initialise(Settings);
        }

        #endregion

        #region Events
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            SetTime(Settings);
        }

        #endregion

        #region Private Methods

        private void Initialise(WordClockSettings settings)
        {
            BuildGrid(settings);
            SetTime(settings, true);
        }

        private void BuildGrid(WordClockSettings settings)
        {
            tblLayout.RowStyles.Clear();
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Percent));
            tblLayout.ColumnStyles.Clear();
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

            TimeGrid grid = TimeGrid.Get(settings.Language);
            char[][] charGrid = grid.CharGrid;
            
            tblLayout.RowCount = TimeGrid.GridHeight;
            tblLayout.ColumnCount = TimeGrid.GridWidth;

            for (int rowIndex = 0; rowIndex < TimeGrid.GridHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < TimeGrid.GridWidth; columnIndex++)
                {
                    LedLetter led= new LedLetter(settings, charGrid[rowIndex][columnIndex].ToString());
                    tblLayout.Controls.Add(led, columnIndex, rowIndex);
                }
            }

            //Resize += PositionLayout;
        }

        private void SetTime(WordClockSettings settings, bool force = false)
        {
            DateTime now = DateTime.Now;
            lblTime.Text = now.ToLongTimeString();
            if (now.Second == 0 || force)
            {
                TimeToTextFormat timeToText = TimeToText.GetSimple(settings.Language, now);
                lblTimeAsText.Text = timeToText.ToString();

                TimeGrid grid = TimeGrid.Get(settings.Language);
                bool[][] bitMask = grid.GetBitMask(timeToText.TimeAsText, true).Mask;

                for (int rowIndex = 0; rowIndex < tblLayout.RowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < tblLayout.ColumnCount; columnIndex++)
                    {
                        if (tblLayout.GetControlFromPosition(columnIndex, rowIndex) is LedLetter led)
                        {
                            led.Active = bitMask[rowIndex][columnIndex];
                        }
                    }
                }

            }
        }

        private void PositionLayout(object sender, EventArgs args)
        {
            // FIXME doesn't work
            //tblLayout.Location = new Point(
            //    ClientSize.Width / 2 - tblLayout.Size.Width / 2,
            //    ClientSize.Height / 2 - tblLayout.Size.Height / 2);
            //tblLayout.Anchor = AnchorStyles.None;
            //tblLayout.Dock = DockStyle.Fill;
        }

        private void UpdateSettings(WordClockSettings settings)
        {
            // TODO Langauge
            BackColor = settings.BackgroundColour;
            lblTime.ForeColor = settings.ActiveFontColour;
            lblTimeAsText.ForeColor = settings.ActiveFontColour;
        }


        #endregion
    }
}
