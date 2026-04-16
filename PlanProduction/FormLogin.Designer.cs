namespace PlanProduction
{
    partial class FormLogin
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
            labelID = new System.Windows.Forms.Label();
            textBoxID = new System.Windows.Forms.TextBox();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            checkBoxMem = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // labelID
            // 
            labelID.AutoSize = true;
            labelID.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelID.Location = new System.Drawing.Point(17, 24);
            labelID.Name = "labelID";
            labelID.Size = new System.Drawing.Size(106, 21);
            labelID.TabIndex = 0;
            labelID.Text = "従業員番号：";
            // 
            // textBoxID
            // 
            textBoxID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxID.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            textBoxID.Location = new System.Drawing.Point(129, 22);
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new System.Drawing.Size(100, 29);
            textBoxID.TabIndex = 1;
            textBoxID.KeyDown += textBoxID_KeyDown;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOK.Location = new System.Drawing.Point(80, 88);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(90, 28);
            buttonOK.TabIndex = 3;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.Location = new System.Drawing.Point(185, 88);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(90, 28);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "キャンセル";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // checkBoxMem
            // 
            checkBoxMem.AutoSize = true;
            checkBoxMem.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            checkBoxMem.Location = new System.Drawing.Point(129, 57);
            checkBoxMem.Name = "checkBoxMem";
            checkBoxMem.Size = new System.Drawing.Size(138, 19);
            checkBoxMem.TabIndex = 2;
            checkBoxMem.Text = "従業員番号を記録する";
            checkBoxMem.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(287, 123);
            Controls.Add(checkBoxMem);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxID);
            Controls.Add(labelID);
            Name = "FormLogin";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ログイン画面";
            FormClosing += FormLogin_FormClosing;
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxMem;
    }
}