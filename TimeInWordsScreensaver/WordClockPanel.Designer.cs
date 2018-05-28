namespace TimeInWordsScreensaver
{
    partial class WordClockPanel
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimeAsText = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTime.Location = new System.Drawing.Point(0, 17);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(53, 17);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "lblTime";
            // 
            // lblTimeAsText
            // 
            this.lblTimeAsText.AutoSize = true;
            this.lblTimeAsText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTimeAsText.Location = new System.Drawing.Point(0, 0);
            this.lblTimeAsText.Name = "lblTimeAsText";
            this.lblTimeAsText.Size = new System.Drawing.Size(96, 17);
            this.lblTimeAsText.TabIndex = 0;
            this.lblTimeAsText.Text = "lblTimeAsText";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WordClockPanel
            // 
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTimeAsText);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeAsText;
        private System.Windows.Forms.Timer timer1;
    }
}
