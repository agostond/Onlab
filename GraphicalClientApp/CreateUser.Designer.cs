namespace ClientApp
{
    partial class CreateUserPage
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
            this.BtnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TbNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBShow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnCreate
            // 
            this.BtnCreate.Location = new System.Drawing.Point(314, 74);
            this.BtnCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(82, 22);
            this.BtnCreate.TabIndex = 0;
            this.BtnCreate.Text = "Create";
            this.BtnCreate.UseVisualStyleBackColor = true;
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please create a master password";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TbNewPassword
            // 
            this.TbNewPassword.Location = new System.Drawing.Point(212, 47);
            this.TbNewPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbNewPassword.MaxLength = 30;
            this.TbNewPassword.Name = "TbNewPassword";
            this.TbNewPassword.PasswordChar = '*';
            this.TbNewPassword.Size = new System.Drawing.Size(184, 23);
            this.TbNewPassword.TabIndex = 2;
            this.TbNewPassword.TextChanged += new System.EventHandler(this.TbNewPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Note: If you forget this one, then you have to ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(10, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "delete all data from the tool, to acces it again";
            // 
            // cBShow
            // 
            this.cBShow.AutoSize = true;
            this.cBShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.cBShow.Location = new System.Drawing.Point(401, 50);
            this.cBShow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBShow.Name = "cBShow";
            this.cBShow.Size = new System.Drawing.Size(54, 19);
            this.cBShow.TabIndex = 5;
            this.cBShow.Text = "Show";
            this.cBShow.UseVisualStyleBackColor = true;
            this.cBShow.CheckedChanged += new System.EventHandler(this.cBShow_CheckedChanged);
            // 
            // CreateUserPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 168);
            this.Controls.Add(this.cBShow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbNewPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CreateUserPage";
            this.Text = "Createing a new user...";
            this.Load += new System.EventHandler(this.CreateUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnCreate;
        private Label label1;
        private TextBox TbNewPassword;
        private Label label2;
        private Label label3;
        private CheckBox cBShow;
    }
}