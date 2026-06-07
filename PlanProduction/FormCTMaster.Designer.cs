namespace PlanProduction
{
    partial class FormCTMaster
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
            panel1 = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            buttonFilterClear2 = new System.Windows.Forms.Button();
            textBoxHmCd2 = new System.Windows.Forms.TextBox();
            buttonAddCT = new System.Windows.Forms.Button();
            buttonAddWKSEQ = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            buttonFilterClear = new System.Windows.Forms.Button();
            buttonAddHMCD = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            textBoxHmCd = new System.Windows.Forms.TextBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            labelCTMasterTitle = new System.Windows.Forms.Label();
            dataGridView3 = new System.Windows.Forms.DataGridView();
            labelDTMasterTitle = new System.Windows.Forms.Label();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            labelReadTitle = new System.Windows.Forms.Label();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(buttonFilterClear2);
            panel1.Controls.Add(textBoxHmCd2);
            panel1.Controls.Add(buttonAddCT);
            panel1.Controls.Add(buttonAddWKSEQ);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(buttonFilterClear);
            panel1.Controls.Add(buttonAddHMCD);
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(buttonSave);
            panel1.Controls.Add(textBoxHmCd);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(748, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(131, 511);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label2.Location = new System.Drawing.Point(13, 131);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(98, 15);
            label2.TabIndex = 12;
            label2.Text = "未登録絞り込み：";
            // 
            // buttonFilterClear2
            // 
            buttonFilterClear2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonFilterClear2.Location = new System.Drawing.Point(46, 179);
            buttonFilterClear2.Name = "buttonFilterClear2";
            buttonFilterClear2.Size = new System.Drawing.Size(75, 23);
            buttonFilterClear2.TabIndex = 11;
            buttonFilterClear2.Text = "条件クリア";
            buttonFilterClear2.UseVisualStyleBackColor = true;
            buttonFilterClear2.Click += ButtonFilterClear2_Click;
            // 
            // textBoxHmCd2
            // 
            textBoxHmCd2.BackColor = System.Drawing.SystemColors.Window;
            textBoxHmCd2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxHmCd2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            textBoxHmCd2.Location = new System.Drawing.Point(10, 149);
            textBoxHmCd2.Name = "textBoxHmCd2";
            textBoxHmCd2.Size = new System.Drawing.Size(111, 29);
            textBoxHmCd2.TabIndex = 10;
            textBoxHmCd2.TextChanged += textBoxHmCd2_TextChanged;
            // 
            // buttonAddCT
            // 
            buttonAddCT.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAddCT.BackColor = System.Drawing.Color.LightGreen;
            buttonAddCT.Location = new System.Drawing.Point(7, 209);
            buttonAddCT.Name = "buttonAddCT";
            buttonAddCT.Size = new System.Drawing.Size(116, 54);
            buttonAddCT.TabIndex = 9;
            buttonAddCT.Text = "CTに追加(Alt+C)";
            buttonAddCT.UseVisualStyleBackColor = false;
            buttonAddCT.Click += ButtonAddCT_Click;
            // 
            // buttonAddWKSEQ
            // 
            buttonAddWKSEQ.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAddWKSEQ.BackColor = System.Drawing.Color.LightGreen;
            buttonAddWKSEQ.Location = new System.Drawing.Point(7, 329);
            buttonAddWKSEQ.Name = "buttonAddWKSEQ";
            buttonAddWKSEQ.Size = new System.Drawing.Size(116, 54);
            buttonAddWKSEQ.TabIndex = 6;
            buttonAddWKSEQ.Text = "順序追加 (Alt+S)";
            buttonAddWKSEQ.UseVisualStyleBackColor = false;
            buttonAddWKSEQ.Click += buttonAddWKSEQ_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label1.Location = new System.Drawing.Point(11, 44);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(86, 15);
            label1.TabIndex = 8;
            label1.Text = "品番絞り込み：";
            // 
            // buttonFilterClear
            // 
            buttonFilterClear.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonFilterClear.Location = new System.Drawing.Point(44, 92);
            buttonFilterClear.Name = "buttonFilterClear";
            buttonFilterClear.Size = new System.Drawing.Size(75, 23);
            buttonFilterClear.TabIndex = 4;
            buttonFilterClear.Text = "条件クリア";
            buttonFilterClear.UseVisualStyleBackColor = true;
            buttonFilterClear.Click += ButtonFilterClear_Click;
            // 
            // buttonAddHMCD
            // 
            buttonAddHMCD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAddHMCD.BackColor = System.Drawing.Color.LightGreen;
            buttonAddHMCD.Location = new System.Drawing.Point(8, 269);
            buttonAddHMCD.Name = "buttonAddHMCD";
            buttonAddHMCD.Size = new System.Drawing.Size(116, 54);
            buttonAddHMCD.TabIndex = 5;
            buttonAddHMCD.Text = "品番追加 (Alt+A)";
            buttonAddHMCD.UseVisualStyleBackColor = false;
            buttonAddHMCD.Click += ButtonAddHMCD_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            buttonClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonClose.Location = new System.Drawing.Point(8, 452);
            buttonClose.Margin = new System.Windows.Forms.Padding(4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(116, 54);
            buttonClose.TabIndex = 8;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += ButtonClose_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonSave.BackColor = System.Drawing.Color.LightCoral;
            buttonSave.Location = new System.Drawing.Point(8, 391);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(116, 54);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "保存 (F9)";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSaveClose_Click;
            // 
            // textBoxHmCd
            // 
            textBoxHmCd.BackColor = System.Drawing.SystemColors.Window;
            textBoxHmCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxHmCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            textBoxHmCd.Location = new System.Drawing.Point(8, 62);
            textBoxHmCd.Name = "textBoxHmCd";
            textBoxHmCd.Size = new System.Drawing.Size(111, 29);
            textBoxHmCd.TabIndex = 3;
            textBoxHmCd.TextChanged += textBoxHmCd_TextChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView2);
            splitContainer1.Panel2.Controls.Add(labelReadTitle);
            splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            splitContainer1.Size = new System.Drawing.Size(748, 511);
            splitContainer1.SplitterDistance = 511;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(10, 5);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dataGridView1);
            splitContainer2.Panel1.Controls.Add(labelCTMasterTitle);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dataGridView3);
            splitContainer2.Panel2.Controls.Add(labelDTMasterTitle);
            splitContainer2.Size = new System.Drawing.Size(496, 501);
            splitContainer2.SplitterDistance = 251;
            splitContainer2.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 38);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(251, 463);
            dataGridView1.TabIndex = 0;
            // 
            // labelCTMasterTitle
            // 
            labelCTMasterTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelCTMasterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelCTMasterTitle.Location = new System.Drawing.Point(0, 0);
            labelCTMasterTitle.Name = "labelCTMasterTitle";
            labelCTMasterTitle.Size = new System.Drawing.Size(251, 38);
            labelCTMasterTitle.TabIndex = 3;
            labelCTMasterTitle.Text = "label1";
            labelCTMasterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView3.Location = new System.Drawing.Point(0, 38);
            dataGridView3.Margin = new System.Windows.Forms.Padding(4);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new System.Drawing.Size(241, 463);
            dataGridView3.TabIndex = 1;
            // 
            // labelDTMasterTitle
            // 
            labelDTMasterTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelDTMasterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelDTMasterTitle.Location = new System.Drawing.Point(0, 0);
            labelDTMasterTitle.Name = "labelDTMasterTitle";
            labelDTMasterTitle.Size = new System.Drawing.Size(241, 38);
            labelDTMasterTitle.TabIndex = 5;
            labelDTMasterTitle.Text = "label3";
            labelDTMasterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView2.Location = new System.Drawing.Point(5, 43);
            dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new System.Drawing.Size(222, 463);
            dataGridView2.TabIndex = 2;
            // 
            // labelReadTitle
            // 
            labelReadTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelReadTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelReadTitle.Location = new System.Drawing.Point(5, 5);
            labelReadTitle.Name = "labelReadTitle";
            labelReadTitle.Size = new System.Drawing.Size(222, 38);
            labelReadTitle.TabIndex = 2;
            labelReadTitle.Text = "label2";
            labelReadTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 511);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(879, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FormCTMaster
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(879, 533);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormCTMaster";
            Text = "[生産計画] CTマスタメンテナンス";
            FormClosing += FormCTMaster_FormClosing;
            Load += FormCTMasterMainte_Load;
            KeyDown += FormCTMaster_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label labelReadTitle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAddHMCD;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelCTMasterTitle;
        private System.Windows.Forms.Label labelDTMasterTitle;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox textBoxHmCd;
        private System.Windows.Forms.Button buttonFilterClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddWKSEQ;
        private System.Windows.Forms.Button buttonAddCT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonFilterClear2;
        private System.Windows.Forms.TextBox textBoxHmCd2;
    }
}