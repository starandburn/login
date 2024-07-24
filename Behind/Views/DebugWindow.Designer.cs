namespace Nkk.IT.Trial.Programing.Login.Views
{
    partial class DebugWindow
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
            dgvUsers = new DataGridView();
            panel1 = new Panel();
            label3 = new Label();
            groupBox1 = new GroupBox();
            lblVariableZ = new Label();
            lblVariableY = new Label();
            lblVariableX = new Label();
            txtVariableZ = new TextBox();
            txtVariableY = new TextBox();
            txtVariableX = new TextBox();
            lstLog = new ListBox();
            label4 = new Label();
            splitter1 = new Splitter();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(8, 0);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.RowTemplate.Height = 25;
            dgvUsers.ScrollBars = ScrollBars.Vertical;
            dgvUsers.Size = new Size(565, 193);
            dgvUsers.TabIndex = 0;
            dgvUsers.TabStop = false;
            dgvUsers.CellDoubleClick += OnUsersGridCellDoubleClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(4);
            panel1.Size = new Size(584, 205);
            panel1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 173);
            label3.Name = "label3";
            label3.Size = new Size(200, 28);
            label3.TabIndex = 3;
            label3.Text = "ログイン情報データベース";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblVariableZ);
            groupBox1.Controls.Add(lblVariableY);
            groupBox1.Controls.Add(lblVariableX);
            groupBox1.Controls.Add(txtVariableZ);
            groupBox1.Controls.Add(txtVariableY);
            groupBox1.Controls.Add(txtVariableX);
            groupBox1.Location = new Point(12, 4);
            groupBox1.Margin = new Padding(8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(560, 166);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "変数の状態";
            // 
            // lblVariableZ
            // 
            lblVariableZ.AutoSize = true;
            lblVariableZ.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblVariableZ.Location = new Point(11, 120);
            lblVariableZ.Name = "lblVariableZ";
            lblVariableZ.Size = new Size(64, 28);
            lblVariableZ.TabIndex = 2;
            lblVariableZ.Text = "変数Z";
            lblVariableZ.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVariableY
            // 
            lblVariableY.AutoSize = true;
            lblVariableY.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblVariableY.Location = new Point(11, 78);
            lblVariableY.Name = "lblVariableY";
            lblVariableY.Size = new Size(64, 28);
            lblVariableY.TabIndex = 2;
            lblVariableY.Text = "変数Y";
            lblVariableY.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVariableX
            // 
            lblVariableX.AutoSize = true;
            lblVariableX.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblVariableX.Location = new Point(11, 36);
            lblVariableX.Name = "lblVariableX";
            lblVariableX.Size = new Size(64, 28);
            lblVariableX.TabIndex = 2;
            lblVariableX.Text = "変数X";
            lblVariableX.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtVariableZ
            // 
            txtVariableZ.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVariableZ.BackColor = SystemColors.ButtonHighlight;
            txtVariableZ.Location = new Point(115, 117);
            txtVariableZ.Name = "txtVariableZ";
            txtVariableZ.PlaceholderText = "（何も入っていません）";
            txtVariableZ.ReadOnly = true;
            txtVariableZ.Size = new Size(430, 34);
            txtVariableZ.TabIndex = 1;
            txtVariableZ.TabStop = false;
            // 
            // txtVariableY
            // 
            txtVariableY.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVariableY.BackColor = SystemColors.ButtonHighlight;
            txtVariableY.Location = new Point(115, 75);
            txtVariableY.Name = "txtVariableY";
            txtVariableY.PlaceholderText = "（何も入っていません）";
            txtVariableY.ReadOnly = true;
            txtVariableY.Size = new Size(430, 34);
            txtVariableY.TabIndex = 1;
            txtVariableY.TabStop = false;
            // 
            // txtVariableX
            // 
            txtVariableX.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVariableX.BackColor = SystemColors.ButtonHighlight;
            txtVariableX.Location = new Point(115, 33);
            txtVariableX.Name = "txtVariableX";
            txtVariableX.PlaceholderText = "（何も入っていません）";
            txtVariableX.ReadOnly = true;
            txtVariableX.Size = new Size(430, 34);
            txtVariableX.TabIndex = 0;
            txtVariableX.TabStop = false;
            // 
            // lstLog
            // 
            lstLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstLog.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lstLog.FormattingEnabled = true;
            lstLog.IntegralHeight = false;
            lstLog.ItemHeight = 21;
            lstLog.Location = new Point(11, 46);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(562, 194);
            lstLog.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 15);
            label4.Name = "label4";
            label4.Size = new Size(104, 28);
            label4.TabIndex = 0;
            label4.Text = "システムログ";
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 205);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 456);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 205);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvUsers);
            splitContainer1.Panel1.Padding = new Padding(8, 0, 8, 8);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lstLog);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Padding = new Padding(8, 4, 8, 8);
            splitContainer1.Size = new Size(581, 456);
            splitContainer1.SplitterDistance = 201;
            splitContainer1.TabIndex = 4;
            splitContainer1.TabStop = false;
            // 
            // DebugWindow
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 661);
            Controls.Add(splitContainer1);
            Controls.Add(splitter1);
            Controls.Add(panel1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4);
            Name = "DebugWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "デバッグ情報";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsers;
		private Panel panel1;
		private TextBox txtVariableX;
		private TextBox txtVariableY;
		private GroupBox groupBox1;
		private Label lblVariableY;
		private Label lblVariableX;
		private Label label3;
		private ListBox lstLog;
		private Label label4;
		private Label lblVariableZ;
		private TextBox txtVariableZ;
		private Splitter splitter1;
		private SplitContainer splitContainer1;
	}
}