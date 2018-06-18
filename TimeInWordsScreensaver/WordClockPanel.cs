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

        private DateTime _debugDateTime = DateTime.Now;
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

        private LedLight _additionalMinute1;
        private LedLight _additionalMinute2;
        private LedLight _additionalMinute3;
        private LedLight _additionalMinute4;

        delegate void LoopMainGridAction(int rowIndex, int columnIndex, int gridRowIndex, int gridColumnIndex);

        #endregion

        #region Constructor

        public WordClockPanel(WordClockSettings settings)
        {
            InitializeComponent();

            if (settings.Debug)
            {
                _debugDateTime = _debugDateTime.AddSeconds(0 - _debugDateTime.Second);
            }

            Settings = settings;
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
            lblTime.Visible = settings.Debug;
            lblTimeAsText.Visible = settings.Debug;
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
            
            // +2 on the row and column counts to accommodate the additional minute LEDs
            tblLayout.RowCount = TimeGrid.GridHeight + 2;
            tblLayout.ColumnCount = TimeGrid.GridWidth + 2;

            // add the additional minute LEDs;
            _additionalMinute1 = new LedLight(settings);
            _additionalMinute2 = new LedLight(settings);
            _additionalMinute3 = new LedLight(settings);
            _additionalMinute4 = new LedLight(settings);
            tblLayout.Controls.Add(_additionalMinute1, 0, 0);
            tblLayout.Controls.Add(_additionalMinute2, tblLayout.ColumnCount - 1, 0);
            tblLayout.Controls.Add(_additionalMinute3, 0, tblLayout.RowCount - 1);
            tblLayout.Controls.Add(_additionalMinute4, tblLayout.ColumnCount - 1, tblLayout.RowCount - 1);
            
            LoopMainGrid((rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
            {
                LedLetter led = new LedLetter(settings, charGrid[gridRowIndex][gridColumnIndex].ToString());
                tblLayout.Controls.Add(led, columnIndex, rowIndex);
            });

            Resize += PositionLayout;

            // set initial position
            PositionLayout(this, EventArgs.Empty);
        }

        private void SetTime(WordClockSettings settings, bool force = false)
        {
            DateTime now = DateTime.Now;

            if (settings.Debug)
            {
                // force the minutes to move forward with every tick
                _debugDateTime = _debugDateTime.AddMinutes(1);
                now = _debugDateTime;
            }
            
            // debug
            lblTime.Text = now.ToLongTimeString();
            
            if (now.Second == 0 || force)
            {
                TimeToTextFormat timeToText = TimeToText.GetSimple(settings.Language, now);

                // debug
                lblTimeAsText.Text = timeToText.ToString();

                TimeGrid grid = TimeGrid.Get(settings.Language);
                bool[][] bitMask = grid.GetBitMask(timeToText.TimeAsText, true).Mask;

                // activate the letter grid
                LoopMainGrid((rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
                {
                    if (tblLayout.GetControlFromPosition(columnIndex, rowIndex) is LedLetter led)
                    {
                        led.Active = bitMask[gridRowIndex][gridColumnIndex];
                    }
                });

                // activate the additional minutes
                _additionalMinute1.Active = timeToText.AdditionalMinutes >= 1;
                _additionalMinute2.Active = timeToText.AdditionalMinutes >= 2;
                _additionalMinute3.Active = timeToText.AdditionalMinutes >= 3;
                _additionalMinute4.Active = timeToText.AdditionalMinutes >= 4;
            }
        }

        private void PositionLayout(object sender, EventArgs args)
        {
            tblLayout.Location = new Point(
                    ClientSize.Width / 2 - tblLayout.Size.Width / 2,
                    ClientSize.Height / 2 - tblLayout.Size.Height / 2);
        }

        private void UpdateSettings(WordClockSettings settings)
        {
            // TODO Langauge
            BackColor = settings.BackgroundColour;
            lblTime.ForeColor = settings.ActiveFontColour;
            lblTimeAsText.ForeColor = settings.ActiveFontColour;
        }

        /// <summary>
        /// Loops the main grid.
        /// </summary>
        /// <param name="action">The action to execute for each cell in the main grid.</param>
        private void LoopMainGrid(LoopMainGridAction action)
        {
            // the additional minute LEDs are in the first/last rows and columns
            // the main grid starts in the 2nd row and column so start at index 1
            for (int rowIndex = 1; rowIndex < TimeGrid.GridHeight + 1; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex < TimeGrid.GridWidth + 1; columnIndex++)
                {
                    int gridRowIndex = rowIndex - 1;
                    int gridColumnIndex = columnIndex - 1;
                    action(rowIndex, columnIndex, gridRowIndex, gridColumnIndex);
                }
            }
        }

        #endregion
    }
}
