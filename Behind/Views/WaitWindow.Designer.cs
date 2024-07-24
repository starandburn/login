namespace Nkk.IT.Trial.Programing.Login.Views
{
	partial class WaitWindow
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
            components = new System.ComponentModel.Container();
            pbProgress = new ProgressBar();
            timWait = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // pbProgress
            // 
            pbProgress.Location = new Point(12, 12);
            pbProgress.MarqueeAnimationSpeed = 10;
            pbProgress.Maximum = 10;
            pbProgress.Name = "pbProgress";
            pbProgress.Size = new Size(299, 23);
            pbProgress.Style = ProgressBarStyle.Marquee;
            pbProgress.TabIndex = 1;
            pbProgress.UseWaitCursor = true;
            pbProgress.Click += pbProgress_Click;
            // 
            // timWait
            // 
            timWait.Enabled = true;
            timWait.Tick += timWait_Tick;
            // 
            // WaitWindow
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 50);
            ControlBox = false;
            Controls.Add(pbProgress);
            Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WaitWindow";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;
            Load += WaitWindow_Load;
            ResumeLayout(false);
        }

        #endregion
        private ProgressBar pbProgress;
		private System.Windows.Forms.Timer timWait;
	}
}