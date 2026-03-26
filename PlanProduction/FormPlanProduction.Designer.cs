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
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            button3 = new System.Windows.Forms.Button();
            splitContainer上下 = new System.Windows.Forms.SplitContainer();
            splitContainer計画と実績 = new System.Windows.Forms.SplitContainer();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panelPlanResult = new System.Windows.Forms.Panel();
            textBox可動率 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            checkBoxPlan早昼 = new System.Windows.Forms.CheckBox();
            checkBoxPlanピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxPlan休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxPlanお昼稼働 = new System.Windows.Forms.CheckBox();
            labelPlanCaption = new System.Windows.Forms.Label();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            panelAchieveResult = new System.Windows.Forms.Panel();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            checkBox4 = new System.Windows.Forms.CheckBox();
            labelAchieveCaption = new System.Windows.Forms.Label();
            panelTitle = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new System.Windows.Forms.Panel();
            monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            buttonSettings = new System.Windows.Forms.Button();
            buttonExit = new System.Windows.Forms.Button();
            buttonDelete = new System.Windows.Forms.Button();
            buttonUndo = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelPlanResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
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
            splitContainerMain.Panel1.Controls.Add(button3);
            splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(10, 32, 2, 10);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(splitContainer上下);
            splitContainerMain.Panel2.Controls.Add(panel1);
            splitContainerMain.Size = new System.Drawing.Size(1214, 591);
            splitContainerMain.SplitterDistance = 201;
            splitContainerMain.TabIndex = 8;
            // 
            // button3
            // 
            button3.Dock = System.Windows.Forms.DockStyle.Top;
            button3.Location = new System.Drawing.Point(10, 32);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(189, 36);
            button3.TabIndex = 0;
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
            splitContainer上下.Size = new System.Drawing.Size(792, 591);
            splitContainer上下.SplitterDistance = 438;
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
            splitContainer計画と実績.Panel1.Controls.Add(dataGridView1);
            splitContainer計画と実績.Panel1.Controls.Add(panelPlanResult);
            splitContainer計画と実績.Panel1.Controls.Add(labelPlanCaption);
            splitContainer計画と実績.Panel1.Padding = new System.Windows.Forms.Padding(5, 0, 3, 0);
            // 
            // splitContainer計画と実績.Panel2
            // 
            splitContainer計画と実績.Panel2.Controls.Add(dataGridView2);
            splitContainer計画と実績.Panel2.Controls.Add(panelAchieveResult);
            splitContainer計画と実績.Panel2.Controls.Add(labelAchieveCaption);
            splitContainer計画と実績.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            splitContainer計画と実績.Size = new System.Drawing.Size(792, 388);
            splitContainer計画と実績.SplitterDistance = 406;
            splitContainer計画と実績.TabIndex = 8;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(5, 23);
            dataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Size = new System.Drawing.Size(398, 304);
            dataGridView1.TabIndex = 5;
            // 
            // panelPlanResult
            // 
            panelPlanResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPlanResult.Controls.Add(textBox可動率);
            panelPlanResult.Controls.Add(label2);
            panelPlanResult.Controls.Add(checkBoxPlan早昼);
            panelPlanResult.Controls.Add(checkBoxPlanピカピカ);
            panelPlanResult.Controls.Add(checkBoxPlan休憩稼働);
            panelPlanResult.Controls.Add(checkBoxPlanお昼稼働);
            panelPlanResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelPlanResult.Location = new System.Drawing.Point(5, 327);
            panelPlanResult.Name = "panelPlanResult";
            panelPlanResult.Size = new System.Drawing.Size(398, 61);
            panelPlanResult.TabIndex = 4;
            // 
            // textBox可動率
            // 
            textBox可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox可動率.Enabled = false;
            textBox可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox可動率.Location = new System.Drawing.Point(263, 16);
            textBox可動率.Name = "textBox可動率";
            textBox可動率.Size = new System.Drawing.Size(45, 36);
            textBox可動率.TabIndex = 12;
            textBox可動率.Text = "100";
            textBox可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            labelPlanCaption.Size = new System.Drawing.Size(398, 23);
            labelPlanCaption.TabIndex = 2;
            labelPlanCaption.Text = "計画";
            labelPlanCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView2.Location = new System.Drawing.Point(3, 23);
            dataGridView2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.RowTemplate.Height = 21;
            dataGridView2.Size = new System.Drawing.Size(379, 304);
            dataGridView2.TabIndex = 7;
            // 
            // panelAchieveResult
            // 
            panelAchieveResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelAchieveResult.Controls.Add(textBox1);
            panelAchieveResult.Controls.Add(label1);
            panelAchieveResult.Controls.Add(checkBox1);
            panelAchieveResult.Controls.Add(checkBox2);
            panelAchieveResult.Controls.Add(checkBox3);
            panelAchieveResult.Controls.Add(checkBox4);
            panelAchieveResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelAchieveResult.Location = new System.Drawing.Point(3, 327);
            panelAchieveResult.Name = "panelAchieveResult";
            panelAchieveResult.Size = new System.Drawing.Size(379, 61);
            panelAchieveResult.TabIndex = 6;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Enabled = false;
            textBox1.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBox1.Location = new System.Drawing.Point(268, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(45, 36);
            textBox1.TabIndex = 18;
            textBox1.Text = "100";
            textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Enabled = false;
            checkBox1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBox1.Location = new System.Drawing.Point(119, 33);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(127, 19);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "早昼 (11:30～12:15)";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Enabled = false;
            checkBox2.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBox2.Location = new System.Drawing.Point(119, 8);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(98, 19);
            checkBox2.TabIndex = 15;
            checkBox2.Text = "ピカピカ大作戦";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Enabled = false;
            checkBox3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBox3.Location = new System.Drawing.Point(15, 33);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(98, 19);
            checkBox3.TabIndex = 14;
            checkBox3.Text = "休憩時間稼働";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Enabled = false;
            checkBox4.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBox4.Location = new System.Drawing.Point(15, 8);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(95, 19);
            checkBox4.TabIndex = 13;
            checkBox4.Text = "お昼休み稼働";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // labelAchieveCaption
            // 
            labelAchieveCaption.BackColor = System.Drawing.Color.MistyRose;
            labelAchieveCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelAchieveCaption.Dock = System.Windows.Forms.DockStyle.Top;
            labelAchieveCaption.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelAchieveCaption.Location = new System.Drawing.Point(3, 0);
            labelAchieveCaption.Name = "labelAchieveCaption";
            labelAchieveCaption.Size = new System.Drawing.Size(379, 23);
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
            panelTitle.Size = new System.Drawing.Size(792, 45);
            panelTitle.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 128);
            labelTitle.Location = new System.Drawing.Point(5, 5);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(787, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "3/21 計画と実績";
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
            chart1.Size = new System.Drawing.Size(787, 129);
            chart1.TabIndex = 10;
            chart1.Text = "chart1";
            // 
            // panel1
            // 
            panel1.Controls.Add(monthCalendar1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(buttonSettings);
            panel1.Controls.Add(buttonExit);
            panel1.Controls.Add(buttonDelete);
            panel1.Controls.Add(buttonUndo);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(792, 0);
            panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(217, 591);
            panel1.TabIndex = 4;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            monthCalendar1.ForeColor = System.Drawing.Color.Red;
            monthCalendar1.Location = new System.Drawing.Point(9, 45);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 14;
            monthCalendar1.DateSelected += monthCalendar1_DateSelected;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(9, 297);
            button2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(199, 41);
            button2.TabIndex = 13;
            button2.Text = "手配一覧 (OrderList)";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(9, 350);
            button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(199, 41);
            button1.TabIndex = 12;
            button1.Text = "計画入力";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            buttonSettings.Click += buttonSettings_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            buttonExit.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            buttonExit.Location = new System.Drawing.Point(9, 527);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new System.Drawing.Size(199, 52);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "終了";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += ButtonExit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new System.Drawing.Point(9, 403);
            buttonDelete.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(199, 41);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "行削除 (&D)";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonUndo
            // 
            buttonUndo.Location = new System.Drawing.Point(9, 456);
            buttonUndo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new System.Drawing.Size(199, 41);
            buttonUndo.TabIndex = 3;
            buttonUndo.Text = "元に戻す (Ctrl+Z)";
            buttonUndo.UseVisualStyleBackColor = true;
            // 
            // FormPlanProduction
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1214, 591);
            Controls.Add(splitContainerMain);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "FormPlanProduction";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "[生産計画]";
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelPlanResult.ResumeLayout(false);
            panelPlanResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.SplitContainer splitContainer上下;
        private System.Windows.Forms.SplitContainer splitContainer計画と実績;
        private System.Windows.Forms.Panel panelPlanResult;
        private System.Windows.Forms.Label labelPlanCaption;
        private System.Windows.Forms.Panel panelAchieveResult;
        private System.Windows.Forms.Label labelAchieveCaption;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxPlan早昼;
        private System.Windows.Forms.CheckBox checkBoxPlanピカピカ;
        private System.Windows.Forms.CheckBox checkBoxPlan休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxPlanお昼稼働;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox可動率;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

