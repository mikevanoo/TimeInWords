using System.Drawing;
using System.Windows.Forms;

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
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
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
            this.lblTime.Visible = false;
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
            this.lblTime.Visible = false;
            //
            // timer1
            //
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // tblLayout
            //
            this.tblLayout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tblLayout.ColumnCount = 1;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblLayout.Dock = System.Windows.Forms.DockStyle.None;

            // change below for debug colour
            //this.tblLayout.BackColor = Color.Red;

            this.tblLayout.Padding = new Padding(50,20,50,20);
            this.tblLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblLayout.Location = new System.Drawing.Point(25, 25);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 1;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            // change below for grid size
            this.tblLayout.Size = new System.Drawing.Size(1200,1000);

            this.tblLayout.TabIndex = 0;
            //
            // WordClockPanel
            //
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTimeAsText);
            this.Controls.Add(this.tblLayout);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeAsText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tblLayout;
    }
}
