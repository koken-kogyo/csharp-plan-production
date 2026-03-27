namespace PlanProduction
{
    partial class FormOrderList
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            buttonRefresh = new System.Windows.Forms.Button();
            buttonAddPlan = new System.Windows.Forms.Button();
            buttonAddAchieve = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel作業時間 = new System.Windows.Forms.Panel();
            textBox作業時間 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            textBox可動率 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            labelTitleOdCd = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel作業時間.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(buttonRefresh, 2, 0);
            tableLayoutPanel1.Controls.Add(buttonAddPlan, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonAddAchieve, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 306);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(640, 62);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = System.Drawing.Color.LightGreen;
            buttonRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonRefresh.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonRefresh.Location = new System.Drawing.Point(388, 4);
            buttonRefresh.Margin = new System.Windows.Forms.Padding(4);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new System.Drawing.Size(248, 54);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "再読み込み";
            buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // buttonAddPlan
            // 
            buttonAddPlan.BackColor = System.Drawing.Color.LightBlue;
            buttonAddPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAddPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonAddPlan.Location = new System.Drawing.Point(4, 4);
            buttonAddPlan.Margin = new System.Windows.Forms.Padding(4);
            buttonAddPlan.Name = "buttonAddPlan";
            buttonAddPlan.Size = new System.Drawing.Size(184, 54);
            buttonAddPlan.TabIndex = 0;
            buttonAddPlan.Text = "計画へ追加";
            buttonAddPlan.UseVisualStyleBackColor = false;
            // 
            // buttonAddAchieve
            // 
            buttonAddAchieve.BackColor = System.Drawing.Color.MistyRose;
            buttonAddAchieve.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAddAchieve.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonAddAchieve.Location = new System.Drawing.Point(196, 4);
            buttonAddAchieve.Margin = new System.Windows.Forms.Padding(4);
            buttonAddAchieve.Name = "buttonAddAchieve";
            buttonAddAchieve.Size = new System.Drawing.Size(184, 54);
            buttonAddAchieve.TabIndex = 1;
            buttonAddAchieve.Text = "実績へ追加";
            buttonAddAchieve.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel作業時間);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(labelTitleOdCd);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(640, 61);
            panel1.TabIndex = 3;
            // 
            // panel作業時間
            // 
            panel作業時間.Controls.Add(textBox作業時間);
            panel作業時間.Controls.Add(label4);
            panel作業時間.Controls.Add(label5);
            panel作業時間.Dock = System.Windows.Forms.DockStyle.Right;
            panel作業時間.Location = new System.Drawing.Point(447, 0);
            panel作業時間.Name = "panel作業時間";
            panel作業時間.Size = new System.Drawing.Size(193, 61);
            panel作業時間.TabIndex = 4;
            // 
            // textBox作業時間
            // 
            textBox作業時間.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox作業時間.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox作業時間.Location = new System.Drawing.Point(72, 12);
            textBox作業時間.Name = "textBox作業時間";
            textBox作業時間.Size = new System.Drawing.Size(72, 36);
            textBox作業時間.TabIndex = 8;
            textBox作業時間.Text = "10.25";
            textBox作業時間.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label4.Location = new System.Drawing.Point(11, 26);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(55, 15);
            label4.TabIndex = 7;
            label4.Text = "作業時間";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label5.Location = new System.Drawing.Point(150, 25);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(31, 15);
            label5.TabIndex = 6;
            label5.Text = "時間";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox可動率);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new System.Drawing.Point(243, 12);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(154, 36);
            panel2.TabIndex = 3;
            // 
            // textBox可動率
            // 
            textBox可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox可動率.Location = new System.Drawing.Point(69, 0);
            textBox可動率.Name = "textBox可動率";
            textBox可動率.Size = new System.Drawing.Size(45, 36);
            textBox可動率.TabIndex = 8;
            textBox可動率.Text = "100";
            textBox可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label3.Location = new System.Drawing.Point(16, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 15);
            label3.TabIndex = 7;
            label3.Text = "可動率";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(122, 13);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(19, 15);
            label2.TabIndex = 6;
            label2.Text = "％";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 6F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(16, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 11);
            label1.TabIndex = 5;
            label1.Text = "べきどうりつ";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitleOdCd
            // 
            labelTitleOdCd.AutoSize = true;
            labelTitleOdCd.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            labelTitleOdCd.Location = new System.Drawing.Point(24, 21);
            labelTitleOdCd.Name = "labelTitleOdCd";
            labelTitleOdCd.Size = new System.Drawing.Size(152, 21);
            labelTitleOdCd.TabIndex = 1;
            labelTitleOdCd.Text = "【6031A】 ＢＥ１曲げ";
            labelTitleOdCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 61);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.Size = new System.Drawing.Size(640, 245);
            dataGridView1.TabIndex = 4;
            // 
            // FormOrderList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(640, 368);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormOrderList";
            Text = "[生産計画] 手配一覧";
            FormClosing += FormOrderList_FormClosing;
            Load += FormOrderList_Load;
            KeyDown += FormOrderList_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel作業時間.ResumeLayout(false);
            panel作業時間.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonAddPlan;
        private System.Windows.Forms.Button buttonAddAchieve;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitleOdCd;
        private System.Windows.Forms.Panel panel作業時間;
        private System.Windows.Forms.TextBox textBox作業時間;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox可動率;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}