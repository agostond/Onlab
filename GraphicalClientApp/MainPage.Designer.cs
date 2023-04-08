﻿namespace ClientApp
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
            this.BtnLogout = new System.Windows.Forms.Button();
            this.LbPasswordList = new System.Windows.Forms.ListBox();
            this.BtnEnter = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rBtnEnter = new System.Windows.Forms.RadioButton();
            this.rBtnEdit = new System.Windows.Forms.RadioButton();
            this.rBtnDelete = new System.Windows.Forms.RadioButton();
            this.rBtnNone = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnAddPass = new System.Windows.Forms.Button();
            this.LbPassCounter = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rBtnEnteringBoth = new System.Windows.Forms.RadioButton();
            this.rBtnEnteringPassword = new System.Windows.Forms.RadioButton();
            this.rBtnEnteringUsername = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnLogout
            // 
            this.BtnLogout.Location = new System.Drawing.Point(1003, 679);
            this.BtnLogout.Name = "BtnLogout";
            this.BtnLogout.Size = new System.Drawing.Size(94, 29);
            this.BtnLogout.TabIndex = 0;
            this.BtnLogout.Text = "Logout";
            this.BtnLogout.UseVisualStyleBackColor = true;
            this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // LbPasswordList
            // 
            this.LbPasswordList.FormattingEnabled = true;
            this.LbPasswordList.ItemHeight = 20;
            this.LbPasswordList.Location = new System.Drawing.Point(21, 52);
            this.LbPasswordList.Name = "LbPasswordList";
            this.LbPasswordList.Size = new System.Drawing.Size(406, 584);
            this.LbPasswordList.TabIndex = 1;
            this.LbPasswordList.Click += new System.EventHandler(this.LbPasswordList_Click);
            this.LbPasswordList.DoubleClick += new System.EventHandler(this.LbPasswordList_DoubleClick);
            // 
            // BtnEnter
            // 
            this.BtnEnter.Location = new System.Drawing.Point(456, 52);
            this.BtnEnter.Name = "BtnEnter";
            this.BtnEnter.Size = new System.Drawing.Size(94, 29);
            this.BtnEnter.TabIndex = 2;
            this.BtnEnter.Text = "Enter";
            this.BtnEnter.UseVisualStyleBackColor = true;
            this.BtnEnter.Click += new System.EventHandler(this.BtnEnter_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(576, 52);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(94, 29);
            this.BtnEdit.TabIndex = 3;
            this.BtnEdit.Text = "Edit";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(698, 52);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(94, 29);
            this.BtnDelete.TabIndex = 4;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Click on a password name, then select what to do with it:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(505, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "You can set a default action for a double click with the radio buttons above";
            // 
            // rBtnEnter
            // 
            this.rBtnEnter.AutoSize = true;
            this.rBtnEnter.Location = new System.Drawing.Point(38, 26);
            this.rBtnEnter.Name = "rBtnEnter";
            this.rBtnEnter.Size = new System.Drawing.Size(17, 16);
            this.rBtnEnter.TabIndex = 7;
            this.rBtnEnter.UseVisualStyleBackColor = true;
            // 
            // rBtnEdit
            // 
            this.rBtnEdit.AutoSize = true;
            this.rBtnEdit.Location = new System.Drawing.Point(159, 26);
            this.rBtnEdit.Name = "rBtnEdit";
            this.rBtnEdit.Size = new System.Drawing.Size(17, 16);
            this.rBtnEdit.TabIndex = 8;
            this.rBtnEdit.UseVisualStyleBackColor = true;
            // 
            // rBtnDelete
            // 
            this.rBtnDelete.AutoSize = true;
            this.rBtnDelete.Location = new System.Drawing.Point(277, 26);
            this.rBtnDelete.Name = "rBtnDelete";
            this.rBtnDelete.Size = new System.Drawing.Size(17, 16);
            this.rBtnDelete.TabIndex = 9;
            this.rBtnDelete.UseVisualStyleBackColor = true;
            // 
            // rBtnNone
            // 
            this.rBtnNone.AutoSize = true;
            this.rBtnNone.Checked = true;
            this.rBtnNone.Location = new System.Drawing.Point(380, 26);
            this.rBtnNone.Name = "rBtnNone";
            this.rBtnNone.Size = new System.Drawing.Size(17, 16);
            this.rBtnNone.TabIndex = 10;
            this.rBtnNone.TabStop = true;
            this.rBtnNone.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(820, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "None";
            // 
            // BtnAddPass
            // 
            this.BtnAddPass.Location = new System.Drawing.Point(21, 682);
            this.BtnAddPass.Name = "BtnAddPass";
            this.BtnAddPass.Size = new System.Drawing.Size(187, 29);
            this.BtnAddPass.TabIndex = 12;
            this.BtnAddPass.Text = "Add a new password";
            this.BtnAddPass.UseVisualStyleBackColor = true;
            this.BtnAddPass.Click += new System.EventHandler(this.BtnAddPass_Click);
            // 
            // LbPassCounter
            // 
            this.LbPassCounter.AutoSize = true;
            this.LbPassCounter.Location = new System.Drawing.Point(21, 648);
            this.LbPassCounter.Name = "LbPassCounter";
            this.LbPassCounter.Size = new System.Drawing.Size(96, 20);
            this.LbPassCounter.TabIndex = 13;
            this.LbPassCounter.Text = "x/x password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Password entering options:";
            // 
            // rBtnEnteringBoth
            // 
            this.rBtnEnteringBoth.AutoSize = true;
            this.rBtnEnteringBoth.Checked = true;
            this.rBtnEnteringBoth.Location = new System.Drawing.Point(6, 60);
            this.rBtnEnteringBoth.Name = "rBtnEnteringBoth";
            this.rBtnEnteringBoth.Size = new System.Drawing.Size(228, 24);
            this.rBtnEnteringBoth.TabIndex = 15;
            this.rBtnEnteringBoth.TabStop = true;
            this.rBtnEnteringBoth.Text = "Enter Username and Password";
            this.rBtnEnteringBoth.UseVisualStyleBackColor = true;
            this.rBtnEnteringBoth.Click += new System.EventHandler(this.rBtnEnteringBoth_Click);
            // 
            // rBtnEnteringPassword
            // 
            this.rBtnEnteringPassword.AutoSize = true;
            this.rBtnEnteringPassword.Location = new System.Drawing.Point(6, 90);
            this.rBtnEnteringPassword.Name = "rBtnEnteringPassword";
            this.rBtnEnteringPassword.Size = new System.Drawing.Size(161, 24);
            this.rBtnEnteringPassword.TabIndex = 16;
            this.rBtnEnteringPassword.Text = "Enter only Password";
            this.rBtnEnteringPassword.UseVisualStyleBackColor = true;
            this.rBtnEnteringPassword.CheckedChanged += new System.EventHandler(this.rBtnEnteringPassword_CheckedChanged);
            // 
            // rBtnEnteringUsername
            // 
            this.rBtnEnteringUsername.AutoSize = true;
            this.rBtnEnteringUsername.Location = new System.Drawing.Point(6, 120);
            this.rBtnEnteringUsername.Name = "rBtnEnteringUsername";
            this.rBtnEnteringUsername.Size = new System.Drawing.Size(166, 24);
            this.rBtnEnteringUsername.TabIndex = 17;
            this.rBtnEnteringUsername.Text = "Enter only Username";
            this.rBtnEnteringUsername.UseVisualStyleBackColor = true;
            this.rBtnEnteringUsername.CheckedChanged += new System.EventHandler(this.rBtnEnteringUsername_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rBtnEnter);
            this.groupBox1.Controls.Add(this.rBtnEdit);
            this.groupBox1.Controls.Add(this.rBtnDelete);
            this.groupBox1.Controls.Add(this.rBtnNone);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(456, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 80);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rBtnEnteringBoth);
            this.groupBox2.Controls.Add(this.rBtnEnteringUsername);
            this.groupBox2.Controls.Add(this.rBtnEnteringPassword);
            this.groupBox2.Location = new System.Drawing.Point(456, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 163);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 720);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LbPassCounter);
            this.Controls.Add(this.BtnAddPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnEnter);
            this.Controls.Add(this.LbPasswordList);
            this.Controls.Add(this.BtnLogout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainPage";
            this.Text = "Client application for a password tool device";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyClose);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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