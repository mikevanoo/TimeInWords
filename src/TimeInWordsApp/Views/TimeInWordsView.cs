using System;
using System.Drawing;
using System.Windows.Forms;
using TextToTimeGridLib;
using TimeInWordsApp.Controls;
using TimeInWordsApp.Presenters;
using TimeToTextLib;

namespace TimeInWordsApp.Views;

public partial class TimeInWordsView : Panel, ITimeInWordsView
{
    private TimeInWordsPresenter Presenter { get; set; }

    private TimeInWordsSettings _settings;
    private TimeInWordsSettings Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            UpdateSettings(_settings);
        }
    }

    private TimeGrid Grid { get; set; }
    private LedLight _additionalMinute1;
    private LedLight _additionalMinute2;
    private LedLight _additionalMinute3;
    private LedLight _additionalMinute4;

    internal TimeInWordsView()
    {
        InitializeComponent();
    }

    public DateTime Time { get; set; }
    public TimeToTextFormat TimeAsText { get; set; }
    public bool[][] GridBitMask { get; set; }

    public void Initialise(TimeInWordsPresenter presenter, TimeInWordsSettings settings, TimeGrid grid)
    {
        Presenter = presenter;
        Settings = settings;
        Grid = grid;

        lblTime.Visible = Settings.Debug;
        lblTimeAsText.Visible = Settings.Debug;
        BuildGrid();
    }

    public void Update(bool force = false)
    {
        // debug
        lblTime.Text = Time.ToShortTimeString();

        if (Time.Second == 0 || force)
        {
            // debug
            lblTimeAsText.Text = TimeAsText.ToString();

            // activate the letter grid
            LoopMainGrid(
                (rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
                {
                    if (tblLayout.GetControlFromPosition(columnIndex, rowIndex) is LedLetter led)
                    {
                        led.Active = GridBitMask[gridRowIndex][gridColumnIndex];
                    }
                }
            );

            // activate the additional minutes
            _additionalMinute1.Active = TimeAsText.AdditionalMinutes >= 1;
            _additionalMinute2.Active = TimeAsText.AdditionalMinutes >= 2;
            _additionalMinute3.Active = TimeAsText.AdditionalMinutes >= 3;
            _additionalMinute4.Active = TimeAsText.AdditionalMinutes >= 4;
        }
    }

    private void BuildGrid()
    {
        tblLayout.RowStyles.Clear();
        tblLayout.RowStyles.Add(new RowStyle(SizeType.Percent));
        tblLayout.ColumnStyles.Clear();
        tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

        // +2 on the row and column counts to accommodate the additional minute LEDs
        tblLayout.RowCount = TimeGrid.GridHeight + 2;
        tblLayout.ColumnCount = TimeGrid.GridWidth + 2;

        // add the additional minute LEDs;
        _additionalMinute1 = new LedLight(Settings);
        _additionalMinute2 = new LedLight(Settings);
        _additionalMinute3 = new LedLight(Settings);
        _additionalMinute4 = new LedLight(Settings);
        tblLayout.Controls.Add(_additionalMinute1, 0, 0);
        tblLayout.Controls.Add(_additionalMinute2, tblLayout.ColumnCount - 1, 0);
        tblLayout.Controls.Add(_additionalMinute3, tblLayout.ColumnCount - 1, tblLayout.RowCount - 1);
        tblLayout.Controls.Add(_additionalMinute4, 0, tblLayout.RowCount - 1);

        var charGrid = Grid.CharGrid;
        LoopMainGrid(
            (rowIndex, columnIndex, gridRowIndex, gridColumnIndex) =>
            {
                var led = new LedLetter(Settings, charGrid[gridRowIndex][gridColumnIndex].ToString());
                tblLayout.Controls.Add(led, columnIndex, rowIndex);
            }
        );

        Resize += PositionLayout;

        // set initial position
        PositionLayout(this, EventArgs.Empty);
    }

    private void PositionLayout(object sender, EventArgs args) =>
        tblLayout.Location = new Point(
            ClientSize.Width / 2 - tblLayout.Size.Width / 2,
            ClientSize.Height / 2 - tblLayout.Size.Height / 2
        );

    private void UpdateSettings(TimeInWordsSettings settings)
    {
        BackColor = settings.BackgroundColour;
        lblTime.ForeColor = settings.ActiveFontColour;
        lblTimeAsText.ForeColor = settings.ActiveFontColour;
    }

    private delegate void LoopMainGridAction(int rowIndex, int columnIndex, int gridRowIndex, int gridColumnIndex);

    private static void LoopMainGrid(LoopMainGridAction action)
    {
        // the additional minute LEDs are in the first/last rows and columns
        // the main grid starts in the 2nd row and column so start at index 1
        for (var rowIndex = 1; rowIndex < TimeGrid.GridHeight + 1; rowIndex++)
        {
            for (var columnIndex = 1; columnIndex < TimeGrid.GridWidth + 1; columnIndex++)
            {
                var gridRowIndex = rowIndex - 1;
                var gridColumnIndex = columnIndex - 1;
                action(rowIndex, columnIndex, gridRowIndex, gridColumnIndex);
            }
        }
    }
}
