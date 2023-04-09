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
            this.BtnLogin.Location = new System.Drawing.Point(338, 113);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(94, 29);
            this.BtnLogin.TabIndex = 0;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnMassDelete
            // 
            this.BtnMassDelete.Location = new System.Drawing.Point(12, 183);
            this.BtnMassDelete.Name = "BtnMassDelete";
            this.BtnMassDelete.Size = new System.Drawing.Size(157, 29);
            this.BtnMassDelete.TabIndex = 1;
            this.BtnMassDelete.Text = "Delete previous user";
            this.BtnMassDelete.UseVisualStyleBackColor = true;
            this.BtnMassDelete.Click += new System.EventHandler(this.BtnMassDelete_Click);
            // 
            // TbPassword
            // 
            this.TbPassword.Location = new System.Drawing.Point(251, 80);
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.PasswordChar = '*';
            this.TbPassword.Size = new System.Drawing.Size(189, 27);
            this.TbPassword.TabIndex = 2;
            this.TbPassword.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please enter the master password:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cBShow
            // 
            this.cBShow.AutoSize = true;
            this.cBShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.cBShow.Location = new System.Drawing.Point(451, 83);
            this.cBShow.Name = "cBShow";
            this.cBShow.Size = new System.Drawing.Size(65, 24);
            this.cBShow.TabIndex = 4;
            this.cBShow.Text = "Show";
            this.cBShow.UseVisualStyleBackColor = true;
            this.cBShow.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 224);
            this.Controls.Add(this.cBShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbPassword);
            this.Controls.Add(this.BtnMassDelete);
            this.Controls.Add(this.BtnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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