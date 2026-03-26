namespace PlanProduction
{
    partial class FormSrttings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            panel2 = new System.Windows.Forms.Panel();
            checkBoxAllCheck = new System.Windows.Forms.CheckBox();
            buttonSaveClose = new System.Windows.Forms.Button();
            buttonCancelClose = new System.Windows.Forms.Button();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            label5 = new System.Windows.Forms.Label();
            buttonClear = new System.Windows.Forms.Button();
            checkBoxSelected = new System.Windows.Forms.CheckBox();
            textBoxSearchOdCd = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            ColumnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ColumnOdCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnKtCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnWkGrNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnListOrder = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ColumnTanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column可動率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnButton = new System.Windows.Forms.DataGridViewButtonColumn();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBoxAllCheck);
            panel2.Controls.Add(buttonSaveClose);
            panel2.Controls.Add(buttonCancelClose);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label4);
            panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel2.Location = new System.Drawing.Point(0, 627);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(929, 81);
            panel2.TabIndex = 32;
            // 
            // checkBoxAllCheck
            // 
            checkBoxAllCheck.AutoSize = true;
            checkBoxAllCheck.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxAllCheck.Location = new System.Drawing.Point(16, 11);
            checkBoxAllCheck.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAllCheck.Name = "checkBoxAllCheck";
            checkBoxAllCheck.Size = new System.Drawing.Size(195, 25);
            checkBoxAllCheck.TabIndex = 36;
            checkBoxAllCheck.Text = "表示されている全てを選択";
            checkBoxAllCheck.UseVisualStyleBackColor = true;
            checkBoxAllCheck.CheckedChanged += checkBoxAllCheck_CheckedChanged;
            // 
            // buttonSaveClose
            // 
            buttonSaveClose.BackColor = System.Drawing.SystemColors.Control;
            buttonSaveClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonSaveClose.Location = new System.Drawing.Point(508, 14);
            buttonSaveClose.Margin = new System.Windows.Forms.Padding(4);
            buttonSaveClose.Name = "buttonSaveClose";
            buttonSaveClose.Size = new System.Drawing.Size(228, 54);
            buttonSaveClose.TabIndex = 33;
            buttonSaveClose.Text = "保存して閉じる (F9)";
            buttonSaveClose.UseVisualStyleBackColor = false;
            buttonSaveClose.Click += buttonSaveClose_Click;
            // 
            // buttonCancelClose
            // 
            buttonCancelClose.BackColor = System.Drawing.SystemColors.Control;
            buttonCancelClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonCancelClose.Location = new System.Drawing.Point(744, 14);
            buttonCancelClose.Margin = new System.Windows.Forms.Padding(4);
            buttonCancelClose.Name = "buttonCancelClose";
            buttonCancelClose.Size = new System.Drawing.Size(172, 54);
            buttonCancelClose.TabIndex = 32;
            buttonCancelClose.Text = "閉じる";
            buttonCancelClose.UseVisualStyleBackColor = false;
            buttonCancelClose.Click += buttonCancelClose_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = System.Drawing.SystemColors.Control;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(246, 36);
            comboBox1.Margin = new System.Windows.Forms.Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(211, 29);
            comboBox1.TabIndex = 31;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            label4.Location = new System.Drawing.Point(15, 42);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(223, 19);
            label4.TabIndex = 30;
            label4.Text = "初期表示を行う手配先コードを選択：";
            // 
            // panel3
            // 
            panel3.Controls.Add(label5);
            panel3.Controls.Add(buttonClear);
            panel3.Controls.Add(checkBoxSelected);
            panel3.Controls.Add(textBoxSearchOdCd);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(929, 122);
            panel3.TabIndex = 33;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label5.Location = new System.Drawing.Point(200, 71);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(109, 15);
            label5.TabIndex = 37;
            label5.Text = "手配先コードフィルター";
            // 
            // buttonClear
            // 
            buttonClear.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonClear.Location = new System.Drawing.Point(463, 66);
            buttonClear.Margin = new System.Windows.Forms.Padding(4);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(84, 22);
            buttonClear.TabIndex = 36;
            buttonClear.Text = "条件クリア";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // checkBoxSelected
            // 
            checkBoxSelected.AutoSize = true;
            checkBoxSelected.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            checkBoxSelected.Location = new System.Drawing.Point(16, 70);
            checkBoxSelected.Margin = new System.Windows.Forms.Padding(4);
            checkBoxSelected.Name = "checkBoxSelected";
            checkBoxSelected.Size = new System.Drawing.Size(143, 19);
            checkBoxSelected.TabIndex = 35;
            checkBoxSelected.Text = "設定済みの手配先コード";
            checkBoxSelected.UseVisualStyleBackColor = true;
            checkBoxSelected.CheckedChanged += checkBoxSelected_CheckedChanged;
            // 
            // textBoxSearchOdCd
            // 
            textBoxSearchOdCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxSearchOdCd.Location = new System.Drawing.Point(318, 62);
            textBoxSearchOdCd.Margin = new System.Windows.Forms.Padding(4);
            textBoxSearchOdCd.Name = "textBoxSearchOdCd";
            textBoxSearchOdCd.Size = new System.Drawing.Size(124, 29);
            textBoxSearchOdCd.TabIndex = 34;
            textBoxSearchOdCd.TextChanged += textBoxSearchOdCd_TextChanged;
            // 
            // label3
            // 
            label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label3.Location = new System.Drawing.Point(441, 99);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(337, 21);
            label3.TabIndex = 33;
            label3.Text = "ローカル設定ファイル [ AppSettings.json ]";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label2.Location = new System.Drawing.Point(41, 99);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(401, 21);
            label2.TabIndex = 32;
            label2.Text = "KM5010：原価管理 「作業グループマスタ」";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            label1.Location = new System.Drawing.Point(106, 9);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(736, 45);
            label1.TabIndex = 31;
            label1.Text = "このパソコンで使用する「手配先コード」を選択してください";
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 122);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(929, 505);
            panel1.TabIndex = 34;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColumnSelected, ColumnOdCd, ColumnKtCd, ColumnWkGrNm, ColumnListOrder, ColumnTanName, Column可動率, ColumnButton });
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.Size = new System.Drawing.Size(929, 505);
            dataGridView1.TabIndex = 24;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // ColumnSelected
            // 
            ColumnSelected.DataPropertyName = "CHECKED";
            ColumnSelected.HeaderText = "  ★";
            ColumnSelected.MinimumWidth = 6;
            ColumnSelected.Name = "ColumnSelected";
            ColumnSelected.Width = 40;
            // 
            // ColumnOdCd
            // 
            ColumnOdCd.DataPropertyName = "ODCD";
            ColumnOdCd.HeaderText = "手配先コード";
            ColumnOdCd.MinimumWidth = 6;
            ColumnOdCd.Name = "ColumnOdCd";
            ColumnOdCd.Width = 125;
            // 
            // ColumnKtCd
            // 
            ColumnKtCd.DataPropertyName = "WKGRCD";
            ColumnKtCd.HeaderText = "工程コード";
            ColumnKtCd.MinimumWidth = 6;
            ColumnKtCd.Name = "ColumnKtCd";
            ColumnKtCd.ToolTipText = "KM5010の作業グループコード";
            ColumnKtCd.Width = 125;
            // 
            // ColumnWkGrNm
            // 
            ColumnWkGrNm.DataPropertyName = "WKGRNM";
            ColumnWkGrNm.HeaderText = "作業グループ名称";
            ColumnWkGrNm.MinimumWidth = 6;
            ColumnWkGrNm.Name = "ColumnWkGrNm";
            ColumnWkGrNm.Width = 150;
            // 
            // ColumnListOrder
            // 
            ColumnListOrder.DataPropertyName = "SORTORDER";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            ColumnListOrder.DefaultCellStyle = dataGridViewCellStyle2;
            ColumnListOrder.HeaderText = "手配リスト順番";
            ColumnListOrder.Items.AddRange(new object[] { "品番優先", "手配日優先" });
            ColumnListOrder.MinimumWidth = 6;
            ColumnListOrder.Name = "ColumnListOrder";
            ColumnListOrder.ToolTipText = "同一手配先コードが複数の場合は先頭の値が適用されます";
            ColumnListOrder.Width = 125;
            // 
            // ColumnTanName
            // 
            ColumnTanName.DataPropertyName = "TANNAME";
            ColumnTanName.HeaderText = "担当者名";
            ColumnTanName.MinimumWidth = 6;
            ColumnTanName.Name = "ColumnTanName";
            ColumnTanName.ToolTipText = "同一手配先コードが複数の場合は先頭の値が適用されます";
            ColumnTanName.Width = 125;
            // 
            // Column可動率
            // 
            Column可動率.DataPropertyName = "AVA";
            Column可動率.HeaderText = "可動率";
            Column可動率.MinimumWidth = 6;
            Column可動率.Name = "Column可動率";
            Column可動率.ToolTipText = "同一手配先コードが複数の場合は先頭の値が適用されます";
            Column可動率.Width = 85;
            // 
            // ColumnButton
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            ColumnButton.DefaultCellStyle = dataGridViewCellStyle3;
            ColumnButton.HeaderText = "CT編集";
            ColumnButton.MinimumWidth = 6;
            ColumnButton.Name = "ColumnButton";
            ColumnButton.Text = "編集";
            ColumnButton.ToolTipText = "品番毎のCTを設定";
            ColumnButton.UseColumnTextForButtonValue = true;
            ColumnButton.Width = 80;
            // 
            // FormSrttings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(929, 708);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            Name = "FormSrttings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "[生産計画] 設定画面";
            FormClosed += FormSrttings_FormClosed;
            Load += FormSrttings_Load;
            KeyDown += FormSrttings_KeyDown;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSaveClose;
        private System.Windows.Forms.Button buttonCancelClose;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxSelected;
        private System.Windows.Forms.TextBox textBoxSearchOdCd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxAllCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOdCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKtCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWkGrNm;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnListOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column可動率;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnButton;
    }
}