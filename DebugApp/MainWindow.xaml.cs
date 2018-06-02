using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using TextToTimeGridLib;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace Debug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        LanguagePreset.Language lang = LanguagePreset.Language.English;
        TimeGrid grid = new TimeGridEnglish();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 1000;
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        DateTime val = DateTime.Now;

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (!(bool)checkBox.IsChecked)
                {
                    time.Content = TimeToText.GetSimple(lang, DateTime.Now).ToString();
                }
                else
                {
                    val = val.AddMinutes(1);
                    //if (val.Minute == 0)
                    //    val = val.AddHours(1);
                    time.Content = TimeToText.GetSimple(lang, val).ToString();
                }

                var mask = grid.GetBitMask((string)time.Content, (bool)chkForce.IsChecked);

                string[] sGrid = grid.ToString().Split('\n');
                string[] sMask = mask.ToString().Split('\n');
                string[] result = grid.ToString(mask).Split('\n');

                var b = new StringBuilder();

                b.AppendLine("Clock grid\tBitmask\t\tResult");
                b.AppendLine();

                for (int i = 0; i < sGrid.Length; i++)
                {
                    string line = sGrid[i].Trim() + "\t" + sMask[i].Trim() + "\t" + result[i].Trim();
                    b.AppendLine(line);
                }

                lblGrid.Content = b.ToString();

            }));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder b = new StringBuilder();

            for (int h = 0; h < 24; h++)
            {
                for (int m = 0; m < 60; m += 5)
                {
                    b.AppendLine(TimeToText.GetSimple(lang, new DateTime(2000, 1, 1, h, m, 0)).ToString());
                }
            }

            Clipboard.SetText(b.ToString());
        }

        private void comboLanguage_Loaded(object sender, RoutedEventArgs e)
        {
            comboLanguage.Items.Add("English");
            comboLanguage.Items.Add("Nederlands");
            comboLanguage.SelectedIndex = 0;
        }

        private void comboLanguage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((string)comboLanguage.SelectedValue == "English")
            {
                lang = LanguagePreset.Language.English;
                grid = new TimeGridEnglish();
            }
            else
            {
                lang = LanguagePreset.Language.Dutch;
                grid = new TimeGridDutch();
            }
        }

        private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            val = DateTime.Now;
        }
    }
}
