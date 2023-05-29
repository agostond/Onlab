namespace ClientApp
{
    partial class MainPage
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
            BtnLogout = new Button();
            LbPasswordList = new ListBox();
            BtnEnter = new Button();
            BtnEdit = new Button();
            BtnDelete = new Button();
            label1 = new Label();
            label2 = new Label();
            rBtnEnter = new RadioButton();
            rBtnEdit = new RadioButton();
            rBtnDelete = new RadioButton();
            rBtnNone = new RadioButton();
            label3 = new Label();
            BtnAddPass = new Button();
            LbPassCounter = new Label();
            label4 = new Label();
            rBtnEnteringBoth = new RadioButton();
            rBtnEnteringPassword = new RadioButton();
            rBtnEnteringUsername = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // BtnLogout
            // 
            BtnLogout.Location = new Point(1001, 676);
            BtnLogout.Name = "BtnLogout";
            BtnLogout.Size = new Size(94, 36);
            BtnLogout.TabIndex = 0;
            BtnLogout.Text = "Logout";
            BtnLogout.UseVisualStyleBackColor = true;
            BtnLogout.Click += BtnLogout_Click;
            // 
            // LbPasswordList
            // 
            LbPasswordList.FormattingEnabled = true;
            LbPasswordList.ItemHeight = 20;
            LbPasswordList.Location = new Point(21, 52);
            LbPasswordList.Name = "LbPasswordList";
            LbPasswordList.Size = new Size(406, 584);
            LbPasswordList.TabIndex = 1;
            LbPasswordList.Click += LbPasswordList_Click;
            LbPasswordList.DoubleClick += LbPasswordList_DoubleClick;
            // 
            // BtnEnter
            // 
            BtnEnter.Location = new Point(456, 52);
            BtnEnter.Name = "BtnEnter";
            BtnEnter.Size = new Size(94, 29);
            BtnEnter.TabIndex = 2;
            BtnEnter.Text = "Enter";
            BtnEnter.UseVisualStyleBackColor = true;
            BtnEnter.Click += BtnEnter_Click;
            // 
            // BtnEdit
            // 
            BtnEdit.Location = new Point(576, 52);
            BtnEdit.Name = "BtnEdit";
            BtnEdit.Size = new Size(94, 29);
            BtnEdit.TabIndex = 3;
            BtnEdit.Text = "Edit";
            BtnEdit.UseVisualStyleBackColor = true;
            BtnEdit.Click += BtnEdit_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Location = new Point(698, 52);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(94, 29);
            BtnDelete.TabIndex = 4;
            BtnDelete.Text = "Delete";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 20);
            label1.Name = "label1";
            label1.Size = new Size(383, 20);
            label1.TabIndex = 5;
            label1.Text = "Click on a password name, then select what to do with it:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 45);
            label2.Name = "label2";
            label2.Size = new Size(505, 20);
            label2.TabIndex = 6;
            label2.Text = "You can set a default action for a double click with the radio buttons above";
            // 
            // rBtnEnter
            // 
            rBtnEnter.AutoSize = true;
            rBtnEnter.Location = new Point(38, 27);
            rBtnEnter.Name = "rBtnEnter";
            rBtnEnter.Size = new Size(17, 16);
            rBtnEnter.TabIndex = 7;
            rBtnEnter.UseVisualStyleBackColor = true;
            // 
            // rBtnEdit
            // 
            rBtnEdit.AutoSize = true;
            rBtnEdit.Location = new Point(159, 27);
            rBtnEdit.Name = "rBtnEdit";
            rBtnEdit.Size = new Size(17, 16);
            rBtnEdit.TabIndex = 8;
            rBtnEdit.UseVisualStyleBackColor = true;
            // 
            // rBtnDelete
            // 
            rBtnDelete.AutoSize = true;
            rBtnDelete.Location = new Point(277, 27);
            rBtnDelete.Name = "rBtnDelete";
            rBtnDelete.Size = new Size(17, 16);
            rBtnDelete.TabIndex = 9;
            rBtnDelete.UseVisualStyleBackColor = true;
            // 
            // rBtnNone
            // 
            rBtnNone.AutoSize = true;
            rBtnNone.Checked = true;
            rBtnNone.Location = new Point(379, 27);
            rBtnNone.Name = "rBtnNone";
            rBtnNone.Size = new Size(17, 16);
            rBtnNone.TabIndex = 10;
            rBtnNone.TabStop = true;
            rBtnNone.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(821, 56);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 11;
            label3.Text = "None";
            // 
            // BtnAddPass
            // 
            BtnAddPass.Location = new Point(21, 683);
            BtnAddPass.Name = "BtnAddPass";
            BtnAddPass.Size = new Size(187, 29);
            BtnAddPass.TabIndex = 12;
            BtnAddPass.Text = "Add a new password";
            BtnAddPass.UseVisualStyleBackColor = true;
            BtnAddPass.Click += BtnAddPass_Click;
            // 
            // LbPassCounter
            // 
            LbPassCounter.AutoSize = true;
            LbPassCounter.Location = new Point(21, 648);
            LbPassCounter.Name = "LbPassCounter";
            LbPassCounter.Size = new Size(96, 20);
            LbPassCounter.TabIndex = 13;
            LbPassCounter.Text = "x/x password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 23);
            label4.Name = "label4";
            label4.Size = new Size(186, 20);
            label4.TabIndex = 14;
            label4.Text = "Password entering options:";
            // 
            // rBtnEnteringBoth
            // 
            rBtnEnteringBoth.AutoSize = true;
            rBtnEnteringBoth.Checked = true;
            rBtnEnteringBoth.Location = new Point(6, 60);
            rBtnEnteringBoth.Name = "rBtnEnteringBoth";
            rBtnEnteringBoth.Size = new Size(228, 24);
            rBtnEnteringBoth.TabIndex = 15;
            rBtnEnteringBoth.TabStop = true;
            rBtnEnteringBoth.Text = "Enter Username and Password";
            rBtnEnteringBoth.UseVisualStyleBackColor = true;
            rBtnEnteringBoth.Click += rBtnEnteringBoth_Click;
            // 
            // rBtnEnteringPassword
            // 
            rBtnEnteringPassword.AutoSize = true;
            rBtnEnteringPassword.Location = new Point(6, 91);
            rBtnEnteringPassword.Name = "rBtnEnteringPassword";
            rBtnEnteringPassword.Size = new Size(161, 24);
            rBtnEnteringPassword.TabIndex = 16;
            rBtnEnteringPassword.Text = "Enter only Password";
            rBtnEnteringPassword.UseVisualStyleBackColor = true;
            rBtnEnteringPassword.CheckedChanged += rBtnEnteringPassword_CheckedChanged;
            // 
            // rBtnEnteringUsername
            // 
            rBtnEnteringUsername.AutoSize = true;
            rBtnEnteringUsername.Location = new Point(6, 120);
            rBtnEnteringUsername.Name = "rBtnEnteringUsername";
            rBtnEnteringUsername.Size = new Size(166, 24);
            rBtnEnteringUsername.TabIndex = 17;
            rBtnEnteringUsername.Text = "Enter only Username";
            rBtnEnteringUsername.UseVisualStyleBackColor = true;
            rBtnEnteringUsername.CheckedChanged += rBtnEnteringUsername_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rBtnEnter);
            groupBox1.Controls.Add(rBtnEdit);
            groupBox1.Controls.Add(rBtnDelete);
            groupBox1.Controls.Add(rBtnNone);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(456, 87);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(517, 80);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(rBtnEnteringBoth);
            groupBox2.Controls.Add(rBtnEnteringUsername);
            groupBox2.Controls.Add(rBtnEnteringPassword);
            groupBox2.Location = new Point(456, 251);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(249, 163);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 720);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(LbPassCounter);
            Controls.Add(BtnAddPass);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(BtnDelete);
            Controls.Add(BtnEdit);
            Controls.Add(BtnEnter);
            Controls.Add(LbPasswordList);
            Controls.Add(BtnLogout);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MainPage";
            Text = "Client application for a password tool device";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnLogout;
        private ListBox LbPasswordList;
        private Button BtnEnter;
        private Button BtnEdit;
        private Button BtnDelete;
        private Label label1;
        private Label label2;
        private RadioButton rBtnEnter;
        private RadioButton rBtnEdit;
        private RadioButton rBtnDelete;
        private RadioButton rBtnNone;
        private Label label3;
        private Button BtnAddPass;
        private Label LbPassCounter;
        private Label label4;
        private RadioButton rBtnEnteringBoth;
        private RadioButton rBtnEnteringPassword;
        private RadioButton rBtnEnteringUsername;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}