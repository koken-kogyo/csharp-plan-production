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
            buttonClose = new System.Windows.Forms.Button();
            buttonRefresh = new System.Windows.Forms.Button();
            buttonAddPlan = new System.Windows.Forms.Button();
            buttonAddAchieve = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            checkBoxKTSEQ = new System.Windows.Forms.CheckBox();
            checkBoxWKNOTE = new System.Windows.Forms.CheckBox();
            checkBoxHMRNM = new System.Windows.Forms.CheckBox();
            labelTitleOdCd = new System.Windows.Forms.Label();
            checkBoxPKey = new System.Windows.Forms.CheckBox();
            panel2 = new System.Windows.Forms.Panel();
            textBox可動率 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel計画稼働時間 = new System.Windows.Forms.Panel();
            textBox作業時間 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel計画稼働時間.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(buttonClose, 3, 0);
            tableLayoutPanel1.Controls.Add(buttonRefresh, 2, 0);
            tableLayoutPanel1.Controls.Add(buttonAddPlan, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonAddAchieve, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 306);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(900, 62);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            buttonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonClose.Location = new System.Drawing.Point(679, 4);
            buttonClose.Margin = new System.Windows.Forms.Padding(4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(217, 54);
            buttonClose.TabIndex = 8;
            buttonClose.TabStop = false;
            buttonClose.Text = "閉じる (Esc)";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += ButtonClose_Click;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = System.Drawing.SystemColors.Control;
            buttonRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonRefresh.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonRefresh.Location = new System.Drawing.Point(454, 4);
            buttonRefresh.Margin = new System.Windows.Forms.Padding(4);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new System.Drawing.Size(217, 54);
            buttonRefresh.TabIndex = 7;
            buttonRefresh.TabStop = false;
            buttonRefresh.Text = "再読み込み (F5)";
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
            buttonAddPlan.Size = new System.Drawing.Size(217, 54);
            buttonAddPlan.TabIndex = 5;
            buttonAddPlan.TabStop = false;
            buttonAddPlan.Text = "計画へ追加 (F4)";
            buttonAddPlan.UseVisualStyleBackColor = false;
            buttonAddPlan.Click += ButtonAddPlan_Click;
            // 
            // buttonAddAchieve
            // 
            buttonAddAchieve.BackColor = System.Drawing.Color.MistyRose;
            buttonAddAchieve.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAddAchieve.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonAddAchieve.Location = new System.Drawing.Point(229, 4);
            buttonAddAchieve.Margin = new System.Windows.Forms.Padding(4);
            buttonAddAchieve.Name = "buttonAddAchieve";
            buttonAddAchieve.Size = new System.Drawing.Size(217, 54);
            buttonAddAchieve.TabIndex = 6;
            buttonAddAchieve.TabStop = false;
            buttonAddAchieve.Text = "実績へ追加 (F6)";
            buttonAddAchieve.UseVisualStyleBackColor = false;
            buttonAddAchieve.Click += ButtonAddAchieve_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxKTSEQ);
            panel1.Controls.Add(checkBoxWKNOTE);
            panel1.Controls.Add(checkBoxHMRNM);
            panel1.Controls.Add(labelTitleOdCd);
            panel1.Controls.Add(checkBoxPKey);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel計画稼働時間);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(900, 61);
            panel1.TabIndex = 3;
            // 
            // checkBoxKTSEQ
            // 
            checkBoxKTSEQ.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxKTSEQ.AutoSize = true;
            checkBoxKTSEQ.Checked = true;
            checkBoxKTSEQ.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKTSEQ.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxKTSEQ.ForeColor = System.Drawing.SystemColors.ControlDark;
            checkBoxKTSEQ.Location = new System.Drawing.Point(376, 39);
            checkBoxKTSEQ.Name = "checkBoxKTSEQ";
            checkBoxKTSEQ.Size = new System.Drawing.Size(60, 19);
            checkBoxKTSEQ.TabIndex = 7;
            checkBoxKTSEQ.Text = "KTSEQ";
            checkBoxKTSEQ.UseVisualStyleBackColor = true;
            checkBoxKTSEQ.CheckedChanged += CheckBoxKTSEQ_CheckedChanged;
            // 
            // checkBoxWKNOTE
            // 
            checkBoxWKNOTE.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxWKNOTE.AutoSize = true;
            checkBoxWKNOTE.Checked = true;
            checkBoxWKNOTE.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxWKNOTE.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxWKNOTE.ForeColor = System.Drawing.SystemColors.ControlDark;
            checkBoxWKNOTE.Location = new System.Drawing.Point(520, 39);
            checkBoxWKNOTE.Name = "checkBoxWKNOTE";
            checkBoxWKNOTE.Size = new System.Drawing.Size(74, 19);
            checkBoxWKNOTE.TabIndex = 2;
            checkBoxWKNOTE.Text = "WKNOTE";
            checkBoxWKNOTE.UseVisualStyleBackColor = true;
            checkBoxWKNOTE.CheckedChanged += CheckBoxWKNOTE_CheckedChanged;
            // 
            // checkBoxHMRNM
            // 
            checkBoxHMRNM.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxHMRNM.AutoSize = true;
            checkBoxHMRNM.Checked = true;
            checkBoxHMRNM.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHMRNM.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxHMRNM.ForeColor = System.Drawing.SystemColors.ControlDark;
            checkBoxHMRNM.Location = new System.Drawing.Point(442, 39);
            checkBoxHMRNM.Name = "checkBoxHMRNM";
            checkBoxHMRNM.Size = new System.Drawing.Size(73, 19);
            checkBoxHMRNM.TabIndex = 1;
            checkBoxHMRNM.Text = "HMRNM";
            checkBoxHMRNM.UseVisualStyleBackColor = true;
            checkBoxHMRNM.CheckedChanged += CheckBoxHMRNM_CheckedChanged;
            // 
            // labelTitleOdCd
            // 
            labelTitleOdCd.AutoSize = true;
            labelTitleOdCd.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            labelTitleOdCd.Location = new System.Drawing.Point(9, 9);
            labelTitleOdCd.Name = "labelTitleOdCd";
            labelTitleOdCd.Size = new System.Drawing.Size(226, 32);
            labelTitleOdCd.TabIndex = 1;
            labelTitleOdCd.Text = "【6031A】 ＢＥ１曲げ";
            labelTitleOdCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxPKey
            // 
            checkBoxPKey.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBoxPKey.AutoSize = true;
            checkBoxPKey.Checked = true;
            checkBoxPKey.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPKey.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxPKey.ForeColor = System.Drawing.SystemColors.ControlDark;
            checkBoxPKey.Location = new System.Drawing.Point(315, 39);
            checkBoxPKey.Name = "checkBoxPKey";
            checkBoxPKey.Size = new System.Drawing.Size(55, 19);
            checkBoxPKey.TabIndex = 0;
            checkBoxPKey.Text = "主キー";
            checkBoxPKey.UseVisualStyleBackColor = true;
            checkBoxPKey.CheckedChanged += CheckBoxPKey_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox可動率);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(596, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(117, 61);
            panel2.TabIndex = 6;
            // 
            // textBox可動率
            // 
            textBox可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox可動率.Location = new System.Drawing.Point(52, 14);
            textBox可動率.Name = "textBox可動率";
            textBox可動率.Size = new System.Drawing.Size(41, 36);
            textBox可動率.TabIndex = 3;
            textBox可動率.Text = "100";
            textBox可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox可動率.Enter += TextBox可動率_Enter;
            textBox可動率.KeyDown += TextBox可動率_KeyDown;
            textBox可動率.MouseDown += TextBox可動率_MouseDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label3.Location = new System.Drawing.Point(7, 30);
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
            label2.Location = new System.Drawing.Point(95, 27);
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
            label1.Location = new System.Drawing.Point(7, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 11);
            label1.TabIndex = 5;
            label1.Text = "べきどうりつ";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel計画稼働時間
            // 
            panel計画稼働時間.Controls.Add(textBox作業時間);
            panel計画稼働時間.Controls.Add(label4);
            panel計画稼働時間.Controls.Add(label5);
            panel計画稼働時間.Dock = System.Windows.Forms.DockStyle.Right;
            panel計画稼働時間.Location = new System.Drawing.Point(713, 0);
            panel計画稼働時間.Name = "panel計画稼働時間";
            panel計画稼働時間.Size = new System.Drawing.Size(187, 61);
            panel計画稼働時間.TabIndex = 4;
            // 
            // textBox作業時間
            // 
            textBox作業時間.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox作業時間.Enabled = false;
            textBox作業時間.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox作業時間.Location = new System.Drawing.Point(84, 12);
            textBox作業時間.Name = "textBox作業時間";
            textBox作業時間.Size = new System.Drawing.Size(64, 36);
            textBox作業時間.TabIndex = 4;
            textBox作業時間.Text = "10.25";
            textBox作業時間.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label4.Location = new System.Drawing.Point(3, 26);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(79, 15);
            label4.TabIndex = 7;
            label4.Text = "計画稼働時間";
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
            dataGridView1.Size = new System.Drawing.Size(900, 245);
            dataGridView1.TabIndex = 4;
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            // 
            // FormOrderList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 368);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormOrderList";
            Text = "[生産計画] 手配一覧";
            FormClosing += FormOrderList_FormClosing;
            Load += FormOrderList_Load;
            Shown += FormOrderList_Shown;
            KeyDown += FormOrderList_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel計画稼働時間.ResumeLayout(false);
            panel計画稼働時間.PerformLayout();
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
        private System.Windows.Forms.Panel panel計画稼働時間;
        private System.Windows.Forms.TextBox textBox作業時間;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxPKey;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox可動率;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxWKNOTE;
        private System.Windows.Forms.CheckBox checkBoxHMRNM;
        private System.Windows.Forms.CheckBox checkBoxKTSEQ;
    }
}