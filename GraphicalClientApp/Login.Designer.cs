namespace ClientApp
{
    partial class LoginPage
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
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnMassDelete = new System.Windows.Forms.Button();
            this.TbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cBShow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(296, 85);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(90, 27);
            this.BtnLogin.TabIndex = 0;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnMassDelete
            // 
            this.BtnMassDelete.Location = new System.Drawing.Point(10, 137);
            this.BtnMassDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnMassDelete.Name = "BtnMassDelete";
            this.BtnMassDelete.Size = new System.Drawing.Size(137, 22);
            this.BtnMassDelete.TabIndex = 1;
            this.BtnMassDelete.Text = "Delete previous user";
            this.BtnMassDelete.UseVisualStyleBackColor = true;
            this.BtnMassDelete.Click += new System.EventHandler(this.BtnMassDelete_Click);
            // 
            // TbPassword
            // 
            this.TbPassword.Location = new System.Drawing.Point(220, 60);
            this.TbPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbPassword.MaxLength = 30;
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.PasswordChar = '*';
            this.TbPassword.Size = new System.Drawing.Size(166, 23);
            this.TbPassword.TabIndex = 2;
            this.TbPassword.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please enter the master password:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cBShow
            // 
            this.cBShow.AutoSize = true;
            this.cBShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.cBShow.Location = new System.Drawing.Point(395, 62);
            this.cBShow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBShow.Name = "cBShow";
            this.cBShow.Size = new System.Drawing.Size(54, 19);
            this.cBShow.TabIndex = 4;
            this.cBShow.Text = "Show";
            this.cBShow.UseVisualStyleBackColor = true;
            this.cBShow.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 168);
            this.Controls.Add(this.cBShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbPassword);
            this.Controls.Add(this.BtnMassDelete);
            this.Controls.Add(this.BtnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginPage";
            this.Text = "Logging in...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnLogin;
        private Button BtnMassDelete;
        private TextBox TbPassword;
        private Label label1;
        private CheckBox cBShow;
    }
}