namespace Nkk.IT.Trial.Programing.Login.Views
{
    partial class WelcomeWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeWindow));
            btnReturn = new Button();
            pictureBox1 = new PictureBox();
            btnExit = new Button();
            lblUser = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnReturn
            // 
            btnReturn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReturn.AutoSize = true;
            btnReturn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnReturn.DialogResult = DialogResult.OK;
            btnReturn.Font = new Font("Yu Gothic UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnReturn.Location = new Point(416, 389);
            btnReturn.Margin = new Padding(5, 6, 5, 6);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(245, 110);
            btnReturn.TabIndex = 0;
            btnReturn.Text = "ログイン画面に\r\n戻る";
            btnReturn.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(675, 514);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.AutoSize = true;
            btnExit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExit.DialogResult = DialogResult.Cancel;
            btnExit.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnExit.Location = new Point(599, 4);
            btnExit.Margin = new Padding(5, 6, 5, 6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(72, 42);
            btnExit.TabIndex = 2;
            btnExit.Text = "終了";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(12, 4);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(262, 37);
            lblUser.TabIndex = 3;
            lblUser.Text = "{user}さん、こんにちは。";
            // 
            // WelcomeWindow
            // 
            AcceptButton = btnReturn;
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(675, 514);
            ControlBox = false;
            Controls.Add(lblUser);
            Controls.Add(btnExit);
            Controls.Add(btnReturn);
            Controls.Add(pictureBox1);
            Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WelcomeWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ようこそNKKシステムへ";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReturn;
        private PictureBox pictureBox1;
        private Button btnExit;
        private Label lblUser;
    }
}