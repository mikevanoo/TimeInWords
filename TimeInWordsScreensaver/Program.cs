using System;
using System.Windows.Forms;

namespace TimeInWordsScreensaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            if (args.Length > 0)
            {
                // Get the command line arguments
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                {
                    secondArgument = args[1];
                }

                // analyze command line arguments
                switch (firstArgument)
                {
                    case "/c":
                        // Show the options dialog
                        //ShowOptions();
                        break;
                    case "/p":
                        // Preview
                        ShowPreview(secondArgument);
                        break;
                    case "/s":
                        // Show screensaver form
                        ShowScreenSaver();
                        break;
                    case "/d":
                        // Show screensver in debug mode
                        ShowProgram();
                        break;
                    default:
                        MessageBox.Show($"Invalid command line argument: {firstArgument}", "Invalid Command Line Argument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                // If no arguments were passed in, show the program
                ShowProgram();
            }
        }

        //static void ShowOptions()
        //{
        //    optionsForm = new SettingsDialog(new WordClockScr.Elements.SettingsMachine(), true);
        //    Application.Run(optionsForm);
        //}

        static void ShowScreenSaver()
        {
            ScreensaverApplicationContext context = new ScreensaverApplicationContext(true);
            Application.Run(context);
        }

        static void ShowProgram()
        {
            ScreenSaverForm form = new ScreenSaverForm(false);
            Application.Run(form);
        }

        static void ShowPreview(string secondArgument)
        {
            if (secondArgument == null)
            {
                MessageBox.Show("Sorry, but the expected window handle was not provided.", "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IntPtr previewWndHandle = new IntPtr(long.Parse(secondArgument));
            ScreenSaverForm form = new ScreenSaverForm(false, previewWndHandle);
            Application.Run(form);
        }
    }
}
