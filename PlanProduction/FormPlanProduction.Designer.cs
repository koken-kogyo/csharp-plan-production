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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            button3 = new System.Windows.Forms.Button();
            splitContainer上下 = new System.Windows.Forms.SplitContainer();
            splitContainer計画と実績 = new System.Windows.Forms.SplitContainer();
            dataGridViewPlan = new System.Windows.Forms.DataGridView();
            panelPlanResult = new System.Windows.Forms.Panel();
            textBoxPlan可動率 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            checkBoxPlan早昼 = new System.Windows.Forms.CheckBox();
            checkBoxPlanピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxPlan休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxPlanお昼稼働 = new System.Windows.Forms.CheckBox();
            labelPlanCaption = new System.Windows.Forms.Label();
            dataGridViewAchieve = new System.Windows.Forms.DataGridView();
            panelAchieveResult = new System.Windows.Forms.Panel();
            textBoxAchieve可動率 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            checkBoxAchieve早昼 = new System.Windows.Forms.CheckBox();
            checkBoxAchieveピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxAchieve休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxAchieveお昼稼働 = new System.Windows.Forms.CheckBox();
            labelAchieveCaption = new System.Windows.Forms.Label();
            panelTitle = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new System.Windows.Forms.Panel();
            monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            buttonOrderList = new System.Windows.Forms.Button();
            buttonPlanEntry = new System.Windows.Forms.Button();
            buttonSettings = new System.Windows.Forms.Button();
            buttonExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer上下).BeginInit();
            splitContainer上下.Panel1.SuspendLayout();
            splitContainer上下.Panel2.SuspendLayout();
            splitContainer上下.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer計画と実績).BeginInit();
            splitContainer計画と実績.Panel1.SuspendLayout();
            splitContainer計画と実績.Panel2.SuspendLayout();
            splitContainer計画と実績.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlan).BeginInit();
            panelPlanResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAchieve).BeginInit();
            panelAchieveResult.SuspendLayout();
            panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerMain.Location = new System.Drawing.Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.AutoScroll = true;
            splitContainerMain.Panel1.Controls.Add(button3);
            splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(10, 30, 2, 30);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(splitContainer上下);
            splitContainerMain.Panel2.Controls.Add(panel1);
            splitContainerMain.Size = new System.Drawing.Size(1037, 413);
            splitContainerMain.SplitterDistance = 147;
            splitContainerMain.TabIndex = 8;
            // 
            // button3
            // 
            button3.Dock = System.Windows.Forms.DockStyle.Top;
            button3.Location = new System.Drawing.Point(10, 30);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(135, 36);
            button3.TabIndex = 1;
            button3.Text = "実行時に削除される";
            button3.UseVisualStyleBackColor = true;
            // 
            // splitContainer上下
            // 
            splitContainer上下.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer上下.Location = new System.Drawing.Point(0, 0);
            splitContainer上下.Name = "splitContainer上下";
            splitContainer上下.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer上下.Panel1
            // 
            splitContainer上下.Panel1.Controls.Add(splitContainer計画と実績);
            splitContainer上下.Panel1.Controls.Add(panelTitle);
            splitContainer上下.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            // 
            // splitContainer上下.Panel2
            // 
            splitContainer上下.Panel2.Controls.Add(chart1);
            splitContainer上下.Panel2.Padding = new System.Windows.Forms.Padding(5, 5, 0, 15);
            splitContainer上下.Size = new System.Drawing.Size(669, 413);
            splitContainer上下.SplitterDistance = 306;
            splitContainer上下.TabIndex = 5;
            // 
            // splitContainer計画と実績
            // 
            splitContainer計画と実績.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer計画と実績.Location = new System.Drawing.Point(0, 45);
            splitContainer計画と実績.Name = "splitContainer計画と実績";
            // 
            // splitContainer計画と実績.Panel1
            // 
            splitContainer計画と実績.Panel1.Controls.Add(dataGridViewPlan);
            splitContainer計画と実績.Panel1.Controls.Add(panelPlanResult);
            splitContainer計画と実績.Panel1.Controls.Add(labelPlanCaption);
            splitContainer計画と実績.Panel1.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            // 
            // splitContainer計画と実績.Panel2
            // 
            splitContainer計画と実績.Panel2.Controls.Add(dataGridViewAchieve);
            splitContainer計画と実績.Panel2.Controls.Add(panelAchieveResult);
            splitContainer計画と実績.Panel2.Controls.Add(labelAchieveCaption);
            splitContainer計画と実績.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            splitContainer計画と実績.Size = new System.Drawing.Size(669, 256);
            splitContainer計画と実績.SplitterDistance = 342;
            splitContainer計画と実績.TabIndex = 8;
            // 
            // dataGridViewPlan
            // 
            dataGridViewPlan.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewPlan.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewPlan.Location = new System.Drawing.Point(5, 23);
            dataGridViewPlan.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            dataGridViewPlan.Name = "dataGridViewPlan";
            dataGridViewPlan.RowHeadersWidth = 51;
            dataGridViewPlan.RowTemplate.Height = 21;
            dataGridViewPlan.Size = new System.Drawing.Size(334, 172);
            dataGridViewPlan.TabIndex = 5;
            // 
            // panelPlanResult
            // 
            panelPlanResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPlanResult.Controls.Add(textBoxPlan可動率);
            panelPlanResult.Controls.Add(label2);
            panelPlanResult.Controls.Add(checkBoxPlan早昼);
            panelPlanResult.Controls.Add(checkBoxPlanピカピカ);
            panelPlanResult.Controls.Add(checkBoxPlan休憩稼働);
            panelPlanResult.Controls.Add(checkBoxPlanお昼稼働);
            panelPlanResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelPlanResult.Location = new System.Drawing.Point(5, 195);
            panelPlanResult.Name = "panelPlanResult";
            panelPlanResult.Size = new System.Drawing.Size(334, 61);
            panelPlanResult.TabIndex = 4;
            // 
            // textBoxPlan可動率
            // 
            textBoxPlan可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlan可動率.Enabled = false;
            textBoxPlan可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBoxPlan可動率.Location = new System.Drawing.Point(263, 16);
            textBoxPlan可動率.Name = "textBoxPlan可動率";
            textBoxPlan可動率.Size = new System.Drawing.Size(45, 36);
            textBoxPlan可動率.TabIndex = 12;
            textBoxPlan可動率.Text = "100";
            textBoxPlan可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(316, 29);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(19, 15);
            label2.TabIndex = 11;
            label2.Text = "％";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxPlan早昼
            // 
            checkBoxPlan早昼.AutoSize = true;
            checkBoxPlan早昼.Enabled = false;
            checkBoxPlan早昼.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlan早昼.Location = new System.Drawing.Point(116, 33);
            checkBoxPlan早昼.Name = "checkBoxPlan早昼";
            checkBoxPlan早昼.Size = new System.Drawing.Size(127, 19);
            checkBoxPlan早昼.TabIndex = 7;
            checkBoxPlan早昼.Text = "早昼 (11:30～12:15)";
            checkBoxPlan早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanピカピカ
            // 
            checkBoxPlanピカピカ.AutoSize = true;
            checkBoxPlanピカピカ.Enabled = false;
            checkBoxPlanピカピカ.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlanピカピカ.Location = new System.Drawing.Point(116, 8);
            checkBoxPlanピカピカ.Name = "checkBoxPlanピカピカ";
            checkBoxPlanピカピカ.Size = new System.Drawing.Size(98, 19);
            checkBoxPlanピカピカ.TabIndex = 6;
            checkBoxPlanピカピカ.Text = "ピカピカ大作戦";
            checkBoxPlanピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlan休憩稼働
            // 
            checkBoxPlan休憩稼働.AutoSize = true;
            checkBoxPlan休憩稼働.Enabled = false;
            checkBoxPlan休憩稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlan休憩稼働.Location = new System.Drawing.Point(12, 33);
            checkBoxPlan休憩稼働.Name = "checkBoxPlan休憩稼働";
            checkBoxPlan休憩稼働.Size = new System.Drawing.Size(98, 19);
            checkBoxPlan休憩稼働.TabIndex = 5;
            checkBoxPlan休憩稼働.Text = "休憩時間稼働";
            checkBoxPlan休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanお昼稼働
            // 
            checkBoxPlanお昼稼働.AutoSize = true;
            checkBoxPlanお昼稼働.Enabled = false;
            checkBoxPlanお昼稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlanお昼稼働.Location = new System.Drawing.Point(12, 8);
            checkBoxPlanお昼稼働.Name = "checkBoxPlanお昼稼働";
            checkBoxPlanお昼稼働.Size = new System.Drawing.Size(95, 19);
            checkBoxPlanお昼稼働.TabIndex = 4;
            checkBoxPlanお昼稼働.Text = "お昼休み稼働";
            checkBoxPlanお昼稼働.UseVisualStyleBackColor = true;
            // 
            // labelPlanCaption
            // 
            labelPlanCaption.BackColor = System.Drawing.Color.LightBlue;
            labelPlanCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelPlanCaption.Dock = System.Windows.Forms.DockStyle.Top;
            labelPlanCaption.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelPlanCaption.Location = new System.Drawing.Point(5, 0);
            labelPlanCaption.Name = "labelPlanCaption";
            labelPlanCaption.Size = new System.Drawing.Size(334, 23);
            labelPlanCaption.TabIndex = 2;
            labelPlanCaption.Text = "計画";
            labelPlanCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewAchieve
            // 
            dataGridViewAchieve.AllowUserToAddRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewAchieve.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewAchieve.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewAchieve.Location = new System.Drawing.Point(3, 23);
            dataGridViewAchieve.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            dataGridViewAchieve.Name = "dataGridViewAchieve";
            dataGridViewAchieve.RowHeadersWidth = 51;
            dataGridViewAchieve.RowTemplate.Height = 21;
            dataGridViewAchieve.Size = new System.Drawing.Size(320, 172);
            dataGridViewAchieve.TabIndex = 7;
            // 
            // panelAchieveResult
            // 
            panelAchieveResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelAchieveResult.Controls.Add(textBoxAchieve可動率);
            panelAchieveResult.Controls.Add(label1);
            panelAchieveResult.Controls.Add(checkBoxAchieve早昼);
            panelAchieveResult.Controls.Add(checkBoxAchieveピカピカ);
            panelAchieveResult.Controls.Add(checkBoxAchieve休憩稼働);
            panelAchieveResult.Controls.Add(checkBoxAchieveお昼稼働);
            panelAchieveResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelAchieveResult.Location = new System.Drawing.Point(3, 195);
            panelAchieveResult.Name = "panelAchieveResult";
            panelAchieveResult.Size = new System.Drawing.Size(320, 61);
            panelAchieveResult.TabIndex = 6;
            // 
            // textBoxAchieve可動率
            // 
            textBoxAchieve可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxAchieve可動率.Enabled = false;
            textBoxAchieve可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBoxAchieve可動率.Location = new System.Drawing.Point(268, 16);
            textBoxAchieve可動率.Name = "textBoxAchieve可動率";
            textBoxAchieve可動率.Size = new System.Drawing.Size(45, 36);
            textBoxAchieve可動率.TabIndex = 18;
            textBoxAchieve可動率.Text = "100";
            textBoxAchieve可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(321, 29);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(19, 15);
            label1.TabIndex = 17;
            label1.Text = "％";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxAchieve早昼
            // 
            checkBoxAchieve早昼.AutoSize = true;
            checkBoxAchieve早昼.Enabled = false;
            checkBoxAchieve早昼.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieve早昼.Location = new System.Drawing.Point(119, 33);
            checkBoxAchieve早昼.Name = "checkBoxAchieve早昼";
            checkBoxAchieve早昼.Size = new System.Drawing.Size(127, 19);
            checkBoxAchieve早昼.TabIndex = 16;
            checkBoxAchieve早昼.Text = "早昼 (11:30～12:15)";
            checkBoxAchieve早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveピカピカ
            // 
            checkBoxAchieveピカピカ.AutoSize = true;
            checkBoxAchieveピカピカ.Enabled = false;
            checkBoxAchieveピカピカ.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieveピカピカ.Location = new System.Drawing.Point(119, 8);
            checkBoxAchieveピカピカ.Name = "checkBoxAchieveピカピカ";
            checkBoxAchieveピカピカ.Size = new System.Drawing.Size(98, 19);
            checkBoxAchieveピカピカ.TabIndex = 15;
            checkBoxAchieveピカピカ.Text = "ピカピカ大作戦";
            checkBoxAchieveピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieve休憩稼働
            // 
            checkBoxAchieve休憩稼働.AutoSize = true;
            checkBoxAchieve休憩稼働.Enabled = false;
            checkBoxAchieve休憩稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieve休憩稼働.Location = new System.Drawing.Point(15, 33);
            checkBoxAchieve休憩稼働.Name = "checkBoxAchieve休憩稼働";
            checkBoxAchieve休憩稼働.Size = new System.Drawing.Size(98, 19);
            checkBoxAchieve休憩稼働.TabIndex = 14;
            checkBoxAchieve休憩稼働.Text = "休憩時間稼働";
            checkBoxAchieve休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveお昼稼働
            // 
            checkBoxAchieveお昼稼働.AutoSize = true;
            checkBoxAchieveお昼稼働.Enabled = false;
            checkBoxAchieveお昼稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieveお昼稼働.Location = new System.Drawing.Point(15, 8);
            checkBoxAchieveお昼稼働.Name = "checkBoxAchieveお昼稼働";
            checkBoxAchieveお昼稼働.Size = new System.Drawing.Size(95, 19);
            checkBoxAchieveお昼稼働.TabIndex = 13;
            checkBoxAchieveお昼稼働.Text = "お昼休み稼働";
            checkBoxAchieveお昼稼働.UseVisualStyleBackColor = true;
            // 
            // labelAchieveCaption
            // 
            labelAchieveCaption.BackColor = System.Drawing.Color.MistyRose;
            labelAchieveCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelAchieveCaption.Dock = System.Windows.Forms.DockStyle.Top;
            labelAchieveCaption.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelAchieveCaption.Location = new System.Drawing.Point(3, 0);
            labelAchieveCaption.Name = "labelAchieveCaption";
            labelAchieveCaption.Size = new System.Drawing.Size(320, 23);
            labelAchieveCaption.TabIndex = 2;
            labelAchieveCaption.Text = "実績";
            labelAchieveCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTitle
            // 
            panelTitle.Controls.Add(labelTitle);
            panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            panelTitle.Location = new System.Drawing.Point(0, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            panelTitle.Size = new System.Drawing.Size(669, 45);
            panelTitle.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            labelTitle.Location = new System.Drawing.Point(5, 5);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(664, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "【3/21】 計画と実績";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(5, 5);
            chart1.Margin = new System.Windows.Forms.Padding(4);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(664, 83);
            chart1.TabIndex = 10;
            chart1.Text = "chart1";
            // 
            // panel1
            // 
            panel1.Controls.Add(monthCalendar1);
            panel1.Controls.Add(buttonOrderList);
            panel1.Controls.Add(buttonPlanEntry);
            panel1.Controls.Add(buttonSettings);
            panel1.Controls.Add(buttonExit);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(669, 0);
            panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(217, 413);
            panel1.TabIndex = 4;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            monthCalendar1.ForeColor = System.Drawing.Color.Red;
            monthCalendar1.Location = new System.Drawing.Point(9, 45);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 14;
            // 
            // buttonOrderList
            // 
            buttonOrderList.Location = new System.Drawing.Point(10, 286);
            buttonOrderList.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonOrderList.Name = "buttonOrderList";
            buttonOrderList.Size = new System.Drawing.Size(199, 52);
            buttonOrderList.TabIndex = 13;
            buttonOrderList.Text = "手配一覧の参照";
            buttonOrderList.UseVisualStyleBackColor = false;
            buttonOrderList.Click += ButtonOrderList_Click;
            // 
            // buttonPlanEntry
            // 
            buttonPlanEntry.Location = new System.Drawing.Point(10, 222);
            buttonPlanEntry.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonPlanEntry.Name = "buttonPlanEntry";
            buttonPlanEntry.Size = new System.Drawing.Size(199, 52);
            buttonPlanEntry.TabIndex = 12;
            buttonPlanEntry.Text = "計画入力";
            buttonPlanEntry.UseVisualStyleBackColor = false;
            buttonPlanEntry.Click += ButtonPlanEntry_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Location = new System.Drawing.Point(9, 5);
            buttonSettings.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new System.Drawing.Size(199, 35);
            buttonSettings.TabIndex = 11;
            buttonSettings.Text = "設定変更";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += ButtonSettings_Click;
            // 
            // buttonExit
            // 
            buttonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonExit.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            buttonExit.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonExit.Location = new System.Drawing.Point(9, 349);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new System.Drawing.Size(199, 52);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "終了";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += ButtonExit_Click;
            // 
            // FormPlanProduction
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1037, 413);
            Controls.Add(splitContainerMain);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "FormPlanProduction";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "[生産計画]";
            FormClosing += FormPlanProduction_FormClosing;
            Load += FormPlanProduction_Load;
            KeyDown += FormPlanProduction_KeyDown;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            splitContainer上下.Panel1.ResumeLayout(false);
            splitContainer上下.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer上下).EndInit();
            splitContainer上下.ResumeLayout(false);
            splitContainer計画と実績.Panel1.ResumeLayout(false);
            splitContainer計画と実績.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer計画と実績).EndInit();
            splitContainer計画と実績.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlan).EndInit();
            panelPlanResult.ResumeLayout(false);
            panelPlanResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAchieve).EndInit();
            panelAchieveResult.ResumeLayout(false);
            panelAchieveResult.PerformLayout();
            panelTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button buttonOrderList;
        private System.Windows.Forms.Button buttonPlanEntry;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.SplitContainer splitContainer上下;
        private System.Windows.Forms.SplitContainer splitContainer計画と実績;
        private System.Windows.Forms.Panel panelPlanResult;
        private System.Windows.Forms.Label labelPlanCaption;
        private System.Windows.Forms.Panel panelAchieveResult;
        private System.Windows.Forms.Label labelAchieveCaption;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private System.Windows.Forms.DataGridView dataGridViewAchieve;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxPlan早昼;
        private System.Windows.Forms.CheckBox checkBoxPlanピカピカ;
        private System.Windows.Forms.CheckBox checkBoxPlan休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxPlanお昼稼働;
        private System.Windows.Forms.TextBox textBoxPlan可動率;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAchieve可動率;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAchieve早昼;
        private System.Windows.Forms.CheckBox checkBoxAchieveピカピカ;
        private System.Windows.Forms.CheckBox checkBoxAchieve休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxAchieveお昼稼働;
        private System.Windows.Forms.Button button3;
    }
}

