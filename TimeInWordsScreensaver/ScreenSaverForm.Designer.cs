namespace TimeInWordsScreensaver
{
    partial class ScreenSaverForm
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
            this.wordClockPanel = new TimeInWordsScreensaver.WordClockPanel(_settings);
            this.SuspendLayout();
            // 
            // wordClockPanel
            // 
            this.wordClockPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.wordClockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordClockPanel.Location = new System.Drawing.Point(0, 0);
            this.wordClockPanel.Name = "wordClockPanel";
            //this.wordClockPanel.Size = new System.Drawing.Size(1024, 768);
            this.wordClockPanel.TabIndex = 1;
            // 
            // ScreenSaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.wordClockPanel);
            this.Name = "ScreenSaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time in Words";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
        private WordClockPanel wordClockPanel;
    }
}

