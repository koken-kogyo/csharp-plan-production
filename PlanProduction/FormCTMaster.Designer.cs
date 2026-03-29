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
            buttonAdd = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            labelMasterTitle = new System.Windows.Forms.Label();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            labelReadTitle = new System.Windows.Forms.Label();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAdd);
            panel1.Controls.Add(buttonClose);
            panel1.Controls.Add(buttonSave);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(748, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(131, 511);
            panel1.TabIndex = 0;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAdd.BackColor = System.Drawing.Color.LightGreen;
            buttonAdd.Location = new System.Drawing.Point(8, 331);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(116, 54);
            buttonAdd.TabIndex = 5;
            buttonAdd.Text = "追加";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += ButtonAdd_Click;
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
            buttonClose.TabIndex = 4;
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
            buttonSave.TabIndex = 3;
            buttonSave.Text = "保存";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSaveClose_Click;
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
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(labelMasterTitle);
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
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(10, 43);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(496, 463);
            dataGridView1.TabIndex = 2;
            // 
            // labelMasterTitle
            // 
            labelMasterTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelMasterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelMasterTitle.Location = new System.Drawing.Point(10, 5);
            labelMasterTitle.Name = "labelMasterTitle";
            labelMasterTitle.Size = new System.Drawing.Size(496, 38);
            labelMasterTitle.TabIndex = 1;
            labelMasterTitle.Text = "label1";
            labelMasterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            dataGridView2.TabIndex = 3;
            dataGridView2.RowPostPaint += DataGridView2_RowPostPaint;
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
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelMasterTitle;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label labelReadTitle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonAdd;
    }
}