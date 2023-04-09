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
            this.BtnSend = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TbNewPageName = new System.Windows.Forms.TextBox();
            this.TbNewUsername = new System.Windows.Forms.TextBox();
            this.TbNewPassword = new System.Windows.Forms.TextBox();
            this.nUDTab = new System.Windows.Forms.NumericUpDown();
            this.nUDEnter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBShow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDEnter)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(12, 289);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(94, 29);
            this.BtnSend.TabIndex = 0;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(522, 289);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(94, 29);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TbNewPageName
            // 
            this.TbNewPageName.Location = new System.Drawing.Point(173, 41);
            this.TbNewPageName.Name = "TbNewPageName";
            this.TbNewPageName.Size = new System.Drawing.Size(290, 27);
            this.TbNewPageName.TabIndex = 2;
            // 
            // TbNewUsername
            // 
            this.TbNewUsername.Location = new System.Drawing.Point(173, 93);
            this.TbNewUsername.Name = "TbNewUsername";
            this.TbNewUsername.Size = new System.Drawing.Size(290, 27);
            this.TbNewUsername.TabIndex = 3;
            // 
            // TbNewPassword
            // 
            this.TbNewPassword.Location = new System.Drawing.Point(173, 142);
            this.TbNewPassword.Name = "TbNewPassword";
            this.TbNewPassword.PasswordChar = '*';
            this.TbNewPassword.Size = new System.Drawing.Size(290, 27);
            this.TbNewPassword.TabIndex = 4;
            // 
            // nUDTab
            // 
            this.nUDTab.Location = new System.Drawing.Point(406, 177);
            this.nUDTab.Name = "nUDTab";
            this.nUDTab.Size = new System.Drawing.Size(57, 27);
            this.nUDTab.TabIndex = 5;
            this.nUDTab.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nUDEnter
            // 
            this.nUDEnter.Location = new System.Drawing.Point(406, 217);
            this.nUDEnter.Name = "nUDEnter";
            this.nUDEnter.Size = new System.Drawing.Size(57, 27);
            this.nUDEnter.TabIndex = 6;
            this.nUDEnter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "New page name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "New user name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "New password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tabulator number between username and password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(323, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Enter number between username and password";
            // 
            // cBShow
            // 
            this.cBShow.AutoSize = true;
            this.cBShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.cBShow.Location = new System.Drawing.Point(469, 145);
            this.cBShow.Name = "cBShow";
            this.cBShow.Size = new System.Drawing.Size(65, 24);
            this.cBShow.TabIndex = 12;
            this.cBShow.Text = "Show";
            this.cBShow.UseVisualStyleBackColor = true;
            this.cBShow.CheckedChanged += new System.EventHandler(this.cBShow_CheckedChanged);
            // 
            // AddPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 330);
            this.Controls.Add(this.cBShow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nUDEnter);
            this.Controls.Add(this.nUDTab);
            this.Controls.Add(this.TbNewPassword);
            this.Controls.Add(this.TbNewUsername);
            this.Controls.Add(this.TbNewPageName);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSend);
            this.Name = "AddPassword";
            this.Text = "Adding a new password...";
            ((System.ComponentModel.ISupportInitialize)(this.nUDTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDEnter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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