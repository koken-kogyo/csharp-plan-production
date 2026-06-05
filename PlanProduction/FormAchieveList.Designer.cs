namespace PlanProduction
{
    partial class FormAchieveList
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
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel1 = new System.Windows.Forms.Panel();
            buttonAchieveExcel = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(813, 419);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAchieveExcel);
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(monthCalendar1);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(813, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(216, 419);
            panel1.TabIndex = 1;
            // 
            // buttonAchieveExcel
            // 
            buttonAchieveExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAchieveExcel.BackColor = System.Drawing.Color.LightGreen;
            buttonAchieveExcel.Location = new System.Drawing.Point(9, 313);
            buttonAchieveExcel.Margin = new System.Windows.Forms.Padding(4);
            buttonAchieveExcel.Name = "buttonAchieveExcel";
            buttonAchieveExcel.Size = new System.Drawing.Size(199, 47);
            buttonAchieveExcel.TabIndex = 17;
            buttonAchieveExcel.Text = "外部出力 (F12)";
            buttonAchieveExcel.UseVisualStyleBackColor = false;
            buttonAchieveExcel.Click += ButtonAchieveExcel_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            buttonClose.Location = new System.Drawing.Point(9, 368);
            buttonClose.Margin = new System.Windows.Forms.Padding(4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(199, 47);
            buttonClose.TabIndex = 16;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Font = new System.Drawing.Font("Yu Gothic UI", 24F);
            monthCalendar1.ForeColor = System.Drawing.Color.Red;
            monthCalendar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            monthCalendar1.Location = new System.Drawing.Point(9, 9);
            monthCalendar1.MaxSelectionCount = 99;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 15;
            // 
            // FormAchieveList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1029, 419);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormAchieveList";
            Text = "[生産計画] 実績リスト";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormAchieveList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAchieveExcel;
    }
}