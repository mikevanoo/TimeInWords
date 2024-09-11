using TimeInWordsApp.Presenters;

namespace TimeInWordsApp.Views
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timeInWordsView = new TimeInWordsView();
            this.dateTimeProvider  = new DateTimeProvider();
            this.timer = new TimeInWordsTimer();
            this.timeInWordsPresenter = new TimeInWordsPresenter(this.timeInWordsView, this._settings, this.dateTimeProvider, this.timer);
            this.SuspendLayout();
            //
            // timeInWordsView
            //
            this.timeInWordsView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timeInWordsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeInWordsView.Location = new System.Drawing.Point(0, 0);
            this.timeInWordsView.Name = "timeInWordsView";
            this.timeInWordsView.TabIndex = 1;
            //
            // MainView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.timeInWordsView);
            this.Name = "MainView";
            this.Text = "Time in Words";
            this.ResumeLayout(false);

        }

        #endregion
        private TimeInWordsView timeInWordsView;
        private DateTimeProvider dateTimeProvider;
        private ITimer timer;
        private TimeInWordsPresenter timeInWordsPresenter;

    }
}

