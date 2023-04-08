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
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(698, 52);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(94, 29);
            this.BtnDelete.TabIndex = 4;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Double click on a password name, then select what to do with it:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(433, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(505, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "You can set a default action for a double click with the radio buttons above";
            // 
            // rBtnEnter
            // 
            this.rBtnEnter.AutoSize = true;
            this.rBtnEnter.Location = new System.Drawing.Point(492, 92);
            this.rBtnEnter.Name = "rBtnEnter";
            this.rBtnEnter.Size = new System.Drawing.Size(17, 16);
            this.rBtnEnter.TabIndex = 7;
            this.rBtnEnter.UseVisualStyleBackColor = true;
            // 
            // rBtnEdit
            // 
            this.rBtnEdit.AutoSize = true;
            this.rBtnEdit.Location = new System.Drawing.Point(616, 92);
            this.rBtnEdit.Name = "rBtnEdit";
            this.rBtnEdit.Size = new System.Drawing.Size(17, 16);
            this.rBtnEdit.TabIndex = 8;
            this.rBtnEdit.UseVisualStyleBackColor = true;
            // 
            // rBtnDelete
            // 
            this.rBtnDelete.AutoSize = true;
            this.rBtnDelete.Location = new System.Drawing.Point(737, 92);
            this.rBtnDelete.Name = "rBtnDelete";
            this.rBtnDelete.Size = new System.Drawing.Size(17, 16);
            this.rBtnDelete.TabIndex = 9;
            this.rBtnDelete.UseVisualStyleBackColor = true;
            // 
            // rBtnNone
            // 
            this.rBtnNone.AutoSize = true;
            this.rBtnNone.Checked = true;
            this.rBtnNone.Location = new System.Drawing.Point(834, 92);
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
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 720);
            this.Controls.Add(this.LbPassCounter);
            this.Controls.Add(this.BtnAddPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rBtnNone);
            this.Controls.Add(this.rBtnDelete);
            this.Controls.Add(this.rBtnEdit);
            this.Controls.Add(this.rBtnEnter);
            this.Controls.Add(this.label2);
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
    }
}