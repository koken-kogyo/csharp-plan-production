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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            buttonAddAchieve = new System.Windows.Forms.Button();
            buttonAddPlan = new System.Windows.Forms.Button();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chart1);
            splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10);
            splitContainer1.Size = new System.Drawing.Size(498, 582);
            splitContainer1.SplitterDistance = 458;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(498, 414);
            dataGridView1.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(10, 10);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(478, 100);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // buttonAddAchieve
            // 
            buttonAddAchieve.BackColor = System.Drawing.Color.MistyRose;
            buttonAddAchieve.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAddAchieve.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonAddAchieve.Location = new System.Drawing.Point(152, 3);
            buttonAddAchieve.Name = "buttonAddAchieve";
            buttonAddAchieve.Size = new System.Drawing.Size(143, 38);
            buttonAddAchieve.TabIndex = 1;
            buttonAddAchieve.Text = "実績へ追加";
            buttonAddAchieve.UseVisualStyleBackColor = false;
            // 
            // buttonAddPlan
            // 
            buttonAddPlan.BackColor = System.Drawing.Color.LightBlue;
            buttonAddPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAddPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonAddPlan.Location = new System.Drawing.Point(3, 3);
            buttonAddPlan.Name = "buttonAddPlan";
            buttonAddPlan.Size = new System.Drawing.Size(143, 38);
            buttonAddPlan.TabIndex = 0;
            buttonAddPlan.Text = "計画へ追加";
            buttonAddPlan.UseVisualStyleBackColor = false;
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
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 414);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(498, 44);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = System.Drawing.Color.LightGreen;
            buttonRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonRefresh.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            buttonRefresh.Location = new System.Drawing.Point(301, 3);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new System.Drawing.Size(194, 38);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "再読み込み";
            buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // FormOrderList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(498, 582);
            Controls.Add(splitContainer1);
            Name = "FormOrderList";
            Text = "[生産計画] 手配一覧";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonAddPlan;
        private System.Windows.Forms.Button buttonAddAchieve;
    }
}