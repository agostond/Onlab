namespace ClientApp
{
    partial class AddPassword
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
            BtnSend = new Button();
            BtnCancel = new Button();
            TbNewPageName = new TextBox();
            TbNewUsername = new TextBox();
            TbNewPassword = new TextBox();
            nUDTab = new NumericUpDown();
            nUDEnter = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cBShow = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)nUDTab).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nUDEnter).BeginInit();
            SuspendLayout();
            // 
            // BtnSend
            // 
            BtnSend.Location = new Point(11, 289);
            BtnSend.Name = "BtnSend";
            BtnSend.Size = new Size(94, 29);
            BtnSend.TabIndex = 0;
            BtnSend.Text = "Send";
            BtnSend.UseVisualStyleBackColor = true;
            BtnSend.Click += BtnSend_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(522, 289);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(94, 29);
            BtnCancel.TabIndex = 1;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // TbNewPageName
            // 
            TbNewPageName.Location = new Point(173, 41);
            TbNewPageName.MaxLength = 15;
            TbNewPageName.Name = "TbNewPageName";
            TbNewPageName.Size = new Size(290, 27);
            TbNewPageName.TabIndex = 2;
            // 
            // TbNewUsername
            // 
            TbNewUsername.Location = new Point(173, 93);
            TbNewUsername.MaxLength = 31;
            TbNewUsername.Name = "TbNewUsername";
            TbNewUsername.Size = new Size(290, 27);
            TbNewUsername.TabIndex = 3;
            // 
            // TbNewPassword
            // 
            TbNewPassword.Location = new Point(173, 141);
            TbNewPassword.MaxLength = 63;
            TbNewPassword.Name = "TbNewPassword";
            TbNewPassword.PasswordChar = '*';
            TbNewPassword.Size = new Size(290, 27);
            TbNewPassword.TabIndex = 4;
            // 
            // nUDTab
            // 
            nUDTab.Location = new Point(406, 177);
            nUDTab.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nUDTab.Name = "nUDTab";
            nUDTab.Size = new Size(57, 27);
            nUDTab.TabIndex = 5;
            nUDTab.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nUDEnter
            // 
            nUDEnter.Location = new Point(406, 217);
            nUDEnter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nUDEnter.Name = "nUDEnter";
            nUDEnter.Size = new Size(57, 27);
            nUDEnter.TabIndex = 6;
            nUDEnter.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 41);
            label1.Name = "label1";
            label1.Size = new Size(118, 20);
            label1.TabIndex = 7;
            label1.Text = "New page name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 93);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 8;
            label2.Text = "New user name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 141);
            label3.Name = "label3";
            label3.Size = new Size(106, 20);
            label3.TabIndex = 9;
            label3.Text = "New password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(49, 184);
            label4.Name = "label4";
            label4.Size = new Size(351, 20);
            label4.TabIndex = 10;
            label4.Text = "Tabulator number between username and password";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(49, 224);
            label5.Name = "label5";
            label5.Size = new Size(252, 20);
            label5.TabIndex = 11;
            label5.Text = "Number of enters after the password";
            // 
            // cBShow
            // 
            cBShow.AutoSize = true;
            cBShow.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            cBShow.Location = new Point(469, 145);
            cBShow.Name = "cBShow";
            cBShow.Size = new Size(65, 24);
            cBShow.TabIndex = 12;
            cBShow.Text = "Show";
            cBShow.UseVisualStyleBackColor = true;
            cBShow.CheckedChanged += cBShow_CheckedChanged;
            // 
            // AddPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 331);
            Controls.Add(cBShow);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nUDEnter);
            Controls.Add(nUDTab);
            Controls.Add(TbNewPassword);
            Controls.Add(TbNewUsername);
            Controls.Add(TbNewPageName);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSend);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddPassword";
            Text = "Password Editor";
            ((System.ComponentModel.ISupportInitialize)nUDTab).EndInit();
            ((System.ComponentModel.ISupportInitialize)nUDEnter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnSend;
        private Button BtnCancel;
        private TextBox TbNewPageName;
        private TextBox TbNewUsername;
        private TextBox TbNewPassword;
        private NumericUpDown nUDTab;
        private NumericUpDown nUDEnter;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckBox cBShow;
    }
}