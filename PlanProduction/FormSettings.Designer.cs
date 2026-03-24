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
            label1 = new System.Windows.Forms.Label();
            buttonSaveClose = new System.Windows.Forms.Button();
            buttonCancelClose = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            textBoxSearchOdCd = new System.Windows.Forms.TextBox();
            checkBoxHeader = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            buttonClear = new System.Windows.Forms.Button();
            ColumnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ColumnOdCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnKtCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnWkGrNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnListOrder = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ColumnTanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column可動率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnButton = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            label1.Location = new System.Drawing.Point(13, 9);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(736, 45);
            label1.TabIndex = 0;
            label1.Text = "このパソコンで使用する「手配先コード」を選択してください";
            // 
            // buttonSaveClose
            // 
            buttonSaveClose.BackColor = System.Drawing.Color.LightCoral;
            buttonSaveClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonSaveClose.Location = new System.Drawing.Point(581, 483);
            buttonSaveClose.Name = "buttonSaveClose";
            buttonSaveClose.Size = new System.Drawing.Size(177, 52);
            buttonSaveClose.TabIndex = 22;
            buttonSaveClose.Text = "保存して閉じる";
            buttonSaveClose.UseVisualStyleBackColor = false;
            buttonSaveClose.Click += buttonSaveClose_Click;
            // 
            // buttonCancelClose
            // 
            buttonCancelClose.BackColor = System.Drawing.Color.LightCoral;
            buttonCancelClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonCancelClose.Location = new System.Drawing.Point(764, 483);
            buttonCancelClose.Name = "buttonCancelClose";
            buttonCancelClose.Size = new System.Drawing.Size(146, 52);
            buttonCancelClose.TabIndex = 21;
            buttonCancelClose.Text = "閉じる";
            buttonCancelClose.UseVisualStyleBackColor = false;
            buttonCancelClose.Click += buttonCancelClose_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColumnSelected, ColumnOdCd, ColumnKtCd, ColumnWkGrNm, ColumnListOrder, ColumnTanName, Column可動率, ColumnButton });
            dataGridView1.Location = new System.Drawing.Point(18, 130);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Size = new System.Drawing.Size(909, 347);
            dataGridView1.TabIndex = 23;
            // 
            // label2
            // 
            label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label2.Location = new System.Drawing.Point(60, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(399, 25);
            label2.TabIndex = 24;
            label2.Text = "KM5010：原価管理 「作業グループマスタ」";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label3.Location = new System.Drawing.Point(456, 63);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(337, 25);
            label3.TabIndex = 25;
            label3.Text = "ローカル設定ファイル [ AppSettings.json ]";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSearchOdCd
            // 
            textBoxSearchOdCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxSearchOdCd.Location = new System.Drawing.Point(60, 101);
            textBoxSearchOdCd.Name = "textBoxSearchOdCd";
            textBoxSearchOdCd.Size = new System.Drawing.Size(125, 23);
            textBoxSearchOdCd.TabIndex = 26;
            textBoxSearchOdCd.TextChanged += textBoxSearchOdCd_TextChanged;
            // 
            // checkBoxHeader
            // 
            checkBoxHeader.AutoSize = true;
            checkBoxHeader.Location = new System.Drawing.Point(30, 107);
            checkBoxHeader.Name = "checkBoxHeader";
            checkBoxHeader.Size = new System.Drawing.Size(15, 14);
            checkBoxHeader.TabIndex = 27;
            checkBoxHeader.UseVisualStyleBackColor = true;
            checkBoxHeader.CheckedChanged += checkBoxHeader_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label4.Location = new System.Drawing.Point(26, 502);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(237, 20);
            label4.TabIndex = 28;
            label4.Text = "初期表示を行う手配先コードを選択：";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(332, 499);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(165, 23);
            comboBox1.TabIndex = 29;
            // 
            // buttonClear
            // 
            buttonClear.Location = new System.Drawing.Point(191, 102);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(75, 23);
            buttonClear.TabIndex = 30;
            buttonClear.Text = "条件クリア";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
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
            ColumnButton.HeaderText = "CT登録";
            ColumnButton.MinimumWidth = 6;
            ColumnButton.Name = "ColumnButton";
            ColumnButton.Text = "登録";
            ColumnButton.ToolTipText = "品番毎のCTを設定";
            ColumnButton.UseColumnTextForButtonValue = true;
            ColumnButton.Width = 80;
            // 
            // FormSrttings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(944, 560);
            Controls.Add(buttonClear);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(checkBoxHeader);
            Controls.Add(textBoxSearchOdCd);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(buttonSaveClose);
            Controls.Add(buttonCancelClose);
            Controls.Add(label1);
            Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "FormSrttings";
            Text = "[生産計画] 設定画面";
            FormClosed += FormSrttings_FormClosed;
            KeyDown += FormSrttings_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveClose;
        private System.Windows.Forms.Button buttonCancelClose;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSearchOdCd;
        private System.Windows.Forms.CheckBox checkBoxHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonClear;
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