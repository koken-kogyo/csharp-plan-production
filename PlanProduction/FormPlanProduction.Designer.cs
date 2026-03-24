namespace PlanProduction
{
    partial class FormPlanProduction
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel1 = new System.Windows.Forms.Panel();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            buttonSettings = new System.Windows.Forms.Button();
            buttonExit = new System.Windows.Forms.Button();
            buttonDelete = new System.Windows.Forms.Button();
            buttonUndo = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Size = new System.Drawing.Size(944, 562);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            dataGridView1.DragDrop += DataGridView1_DragDrop;
            dataGridView1.DragOver += DataGridView1_DragOver;
            dataGridView1.MouseDown += DataGridView1_MouseDown;
            dataGridView1.MouseMove += DataGridView1_MouseMove;
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(buttonSettings);
            panel1.Controls.Add(buttonExit);
            panel1.Controls.Add(buttonDelete);
            panel1.Controls.Add(buttonUndo);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(817, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(127, 562);
            panel1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(15, 204);
            button2.Margin = new System.Windows.Forms.Padding(4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(97, 29);
            button2.TabIndex = 13;
            button2.Text = "手配一覧";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(15, 241);
            button1.Margin = new System.Windows.Forms.Padding(4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(97, 34);
            button1.TabIndex = 12;
            button1.Text = "計画入力";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Location = new System.Drawing.Point(15, 167);
            buttonSettings.Margin = new System.Windows.Forms.Padding(4);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new System.Drawing.Size(97, 29);
            buttonSettings.TabIndex = 11;
            buttonSettings.Text = "設定画面";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            buttonExit.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            buttonExit.Location = new System.Drawing.Point(16, 469);
            buttonExit.Margin = new System.Windows.Forms.Padding(2);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new System.Drawing.Size(97, 81);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "終了";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += ButtonExit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new System.Drawing.Point(16, 332);
            buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(97, 62);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "行削除 (&D)";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // buttonUndo
            // 
            buttonUndo.Location = new System.Drawing.Point(16, 402);
            buttonUndo.Margin = new System.Windows.Forms.Padding(4);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new System.Drawing.Size(97, 60);
            buttonUndo.TabIndex = 3;
            buttonUndo.Text = "元に戻す (Ctrl+Z)";
            buttonUndo.UseVisualStyleBackColor = true;
            buttonUndo.Click += ButtonUndo_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(chart1);
            panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel2.Location = new System.Drawing.Point(0, 402);
            panel2.Margin = new System.Windows.Forms.Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(817, 160);
            panel2.TabIndex = 4;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(130, 38);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(300, 92);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // FormPlanProduction
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(944, 562);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormPlanProduction";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "[生産計画]";
            KeyDown += FormPlanProduction_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Panel panel2;
        //private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button2;
    }
}

