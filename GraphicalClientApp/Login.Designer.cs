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
            BtnLogin = new Button();
            BtnMassDelete = new Button();
            TbPassword = new TextBox();
            label1 = new Label();
            cBShow = new CheckBox();
            SuspendLayout();
            // 
            // BtnLogin
            // 
            BtnLogin.Location = new Point(338, 113);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(103, 36);
            BtnLogin.TabIndex = 0;
            BtnLogin.Text = "Login";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // BtnMassDelete
            // 
            BtnMassDelete.Location = new Point(11, 183);
            BtnMassDelete.Name = "BtnMassDelete";
            BtnMassDelete.Size = new Size(157, 29);
            BtnMassDelete.TabIndex = 1;
            BtnMassDelete.Text = "Delete previous user";
            BtnMassDelete.UseVisualStyleBackColor = true;
            BtnMassDelete.Click += BtnMassDelete_Click;
            // 
            // TbPassword
            // 
            TbPassword.Location = new Point(251, 80);
            TbPassword.MaxLength = 30;
            TbPassword.Name = "TbPassword";
            TbPassword.PasswordChar = '*';
            TbPassword.Size = new Size(189, 27);
            TbPassword.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 83);
            label1.Name = "label1";
            label1.Size = new Size(233, 20);
            label1.TabIndex = 3;
            label1.Text = "Please enter the master password:";
            // 
            // cBShow
            // 
            cBShow.AutoSize = true;
            cBShow.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            cBShow.Location = new Point(451, 83);
            cBShow.Name = "cBShow";
            cBShow.Size = new Size(65, 24);
            cBShow.TabIndex = 4;
            cBShow.Text = "Show";
            cBShow.UseVisualStyleBackColor = true;
            cBShow.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(528, 224);
            Controls.Add(cBShow);
            Controls.Add(label1);
            Controls.Add(TbPassword);
            Controls.Add(BtnMassDelete);
            Controls.Add(BtnLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoginPage";
            Text = "Logging in...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnLogin;
        private Button BtnMassDelete;
        private TextBox TbPassword;
        private Label label1;
        private CheckBox cBShow;
    }
}