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
            BtnCreate = new Button();
            label1 = new Label();
            TbNewPassword = new TextBox();
            label2 = new Label();
            label3 = new Label();
            cBShow = new CheckBox();
            SuspendLayout();
            // 
            // BtnCreate
            // 
            BtnCreate.Location = new Point(359, 99);
            BtnCreate.Name = "BtnCreate";
            BtnCreate.Size = new Size(94, 29);
            BtnCreate.TabIndex = 0;
            BtnCreate.Text = "Create";
            BtnCreate.UseVisualStyleBackColor = true;
            BtnCreate.Click += BtnCreate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 67);
            label1.Name = "label1";
            label1.Size = new Size(224, 20);
            label1.TabIndex = 1;
            label1.Text = "Please create a master password";
            // 
            // TbNewPassword
            // 
            TbNewPassword.Location = new Point(242, 63);
            TbNewPassword.MaxLength = 30;
            TbNewPassword.Name = "TbNewPassword";
            TbNewPassword.PasswordChar = '*';
            TbNewPassword.Size = new Size(210, 27);
            TbNewPassword.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(11, 141);
            label2.Name = "label2";
            label2.Size = new Size(294, 20);
            label2.TabIndex = 3;
            label2.Text = "Note: If you forget this one, then you have to ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(11, 161);
            label3.Name = "label3";
            label3.Size = new Size(299, 20);
            label3.TabIndex = 4;
            label3.Text = "delete all data from the tool, to acces it again";
            // 
            // cBShow
            // 
            cBShow.AutoSize = true;
            cBShow.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            cBShow.Location = new Point(458, 67);
            cBShow.Name = "cBShow";
            cBShow.Size = new Size(65, 24);
            cBShow.TabIndex = 5;
            cBShow.Text = "Show";
            cBShow.UseVisualStyleBackColor = true;
            cBShow.CheckedChanged += cBShow_CheckedChanged;
            // 
            // CreateUserPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 224);
            Controls.Add(cBShow);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(TbNewPassword);
            Controls.Add(label1);
            Controls.Add(BtnCreate);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "CreateUserPage";
            Text = "Createing a new user...";
            ResumeLayout(false);
            PerformLayout();
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