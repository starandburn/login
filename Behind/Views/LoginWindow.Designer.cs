namespace Nkk.IT.Trial.Programing.Login.Views
{
	partial class LoginWindow
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            txtUserId = new TextBox();
            txtPassword = new TextBox();
            lblUserIdCaption = new Label();
            lblPasswordCaption = new Label();
            btnLogin = new Button();
            btnCancel = new Button();
            btnEye = new Button();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            lblMessage = new Label();
            lblCopyright = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtUserId
            // 
            txtUserId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUserId.ImeMode = ImeMode.Disable;
            txtUserId.Location = new Point(25, 211);
            txtUserId.Margin = new Padding(3, 4, 3, 4);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(463, 47);
            txtUserId.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.ImeMode = ImeMode.Disable;
            txtPassword.Location = new Point(25, 347);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.ShortcutsEnabled = false;
            txtPassword.Size = new Size(463, 47);
            txtPassword.TabIndex = 3;
            // 
            // lblUserIdCaption
            // 
            lblUserIdCaption.AutoSize = true;
            lblUserIdCaption.Location = new Point(25, 154);
            lblUserIdCaption.Name = "lblUserIdCaption";
            lblUserIdCaption.Size = new Size(141, 41);
            lblUserIdCaption.TabIndex = 0;
            lblUserIdCaption.Text = "ユーザーID:";
            // 
            // lblPasswordCaption
            // 
            lblPasswordCaption.AutoSize = true;
            lblPasswordCaption.Location = new Point(25, 290);
            lblPasswordCaption.Name = "lblPasswordCaption";
            lblPasswordCaption.Size = new Size(134, 41);
            lblPasswordCaption.TabIndex = 2;
            lblPasswordCaption.Text = "パスワード:";
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogin.Location = new Point(573, 154);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(185, 104);
            btnLogin.TabIndex = 4;
            btnLogin.TabStop = false;
            btnLogin.Text = "ログイン";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += OnLoginButtonClick;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(573, 342);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(185, 52);
            btnCancel.TabIndex = 5;
            btnCancel.TabStop = false;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += OnCancelButtonClick;
            // 
            // btnEye
            // 
            btnEye.Location = new Point(557, 13);
            btnEye.Margin = new Padding(3, 4, 3, 4);
            btnEye.Name = "btnEye";
            btnEye.Size = new Size(43, 38);
            btnEye.TabIndex = 6;
            btnEye.TabStop = false;
            btnEye.Text = "👁";
            btnEye.UseVisualStyleBackColor = true;
            btnEye.MouseDown += OnEyeButtonMouseDown;
            btnEye.MouseUp += OnEyeByttonMouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(btnEye);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(782, 98);
            panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(782, 98);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblMessage.Location = new Point(0, 101);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(782, 37);
            lblMessage.TabIndex = 8;
            lblMessage.Text = "Message";
            lblMessage.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblCopyright
            // 
            lblCopyright.BackColor = Color.DarkBlue;
            lblCopyright.Dock = DockStyle.Bottom;
            lblCopyright.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCopyright.ForeColor = Color.Transparent;
            lblCopyright.Location = new Point(0, 414);
            lblCopyright.Margin = new Padding(0);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(782, 39);
            lblCopyright.TabIndex = 9;
            lblCopyright.Text = "Copyright© {year}, Japan International Institute of Cybernetics all rights reserved.";
            lblCopyright.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LoginWindow
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(782, 453);
            Controls.Add(btnLogin);
            Controls.Add(lblCopyright);
            Controls.Add(lblMessage);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(lblPasswordCaption);
            Controls.Add(lblUserIdCaption);
            Controls.Add(txtPassword);
            Controls.Add(txtUserId);
            Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(600, 100);
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginWindow";
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            FormClosed += OnClosed;
            Load += OnLoad;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserId;
		private TextBox txtPassword;
		private Label lblUserIdCaption;
		private Label lblPasswordCaption;
		private Button btnLogin;
		private Button btnCancel;
		private Button btnEye;
		private Panel panel1;
		private PictureBox pictureBox1;
		private Label lblMessage;
		private Label lblCopyright;
	}
}
