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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            buttonRefresh = new System.Windows.Forms.Button();
            buttonAddPlan = new System.Windows.Forms.Button();
            buttonAddAchieve = new System.Windows.Forms.Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerMain.Location = new System.Drawing.Point(0, 0);
            splitContainerMain.Margin = new System.Windows.Forms.Padding(4);
            splitContainerMain.Name = "splitContainerMain";
            splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(dataGridView1);
            splitContainerMain.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(chart1);
            splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(13, 14, 13, 14);
            splitContainerMain.Size = new System.Drawing.Size(640, 815);
            splitContainerMain.SplitterDistance = 641;
            splitContainerMain.SplitterWidth = 6;
            splitContainerMain.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.Size = new System.Drawing.Size(640, 579);
            dataGridView1.TabIndex = 1;
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
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 579);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(640, 62);
            tableLayoutPanel1.TabIndex = 0;
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
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea3);
            chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            chart1.Legends.Add(legend3);
            chart1.Location = new System.Drawing.Point(13, 14);
            chart1.Margin = new System.Windows.Forms.Padding(4);
            chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chart1.Series.Add(series3);
            chart1.Size = new System.Drawing.Size(614, 140);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // FormOrderList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(640, 815);
            Controls.Add(splitContainerMain);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormOrderList";
            Text = "[生産計画] 手配一覧";
            FormClosing += FormOrderList_FormClosing;
            Load += FormOrderList_Load;
            KeyDown += FormOrderList_KeyDown;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonAddPlan;
        private System.Windows.Forms.Button buttonAddAchieve;
    }
}