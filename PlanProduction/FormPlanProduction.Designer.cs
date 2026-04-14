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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlanProduction));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            button3 = new System.Windows.Forms.Button();
            splitContainer上下 = new System.Windows.Forms.SplitContainer();
            splitContainer計画と実績 = new System.Windows.Forms.SplitContainer();
            dataGridViewPlan = new System.Windows.Forms.DataGridView();
            panelPlanResult = new System.Windows.Forms.Panel();
            textBoxPlanQty = new System.Windows.Forms.TextBox();
            labelPlanQty = new System.Windows.Forms.Label();
            textBoxPlanEndTime = new System.Windows.Forms.TextBox();
            textBoxPlanStartTime = new System.Windows.Forms.TextBox();
            labelPlanTime = new System.Windows.Forms.Label();
            textBoxPlan可動率 = new System.Windows.Forms.TextBox();
            labelPlanAva = new System.Windows.Forms.Label();
            checkBoxPlan早昼 = new System.Windows.Forms.CheckBox();
            checkBoxPlanピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxPlan休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxPlanお昼稼働 = new System.Windows.Forms.CheckBox();
            labelPlanCaption = new System.Windows.Forms.Label();
            dataGridViewAchieve = new System.Windows.Forms.DataGridView();
            panelAchieveResult = new System.Windows.Forms.Panel();
            textBoxAchieveQty = new System.Windows.Forms.TextBox();
            labelAchieveQty = new System.Windows.Forms.Label();
            textBoxAchieveEndTime = new System.Windows.Forms.TextBox();
            textBoxAchieveStartTime = new System.Windows.Forms.TextBox();
            labelAchieveTime = new System.Windows.Forms.Label();
            textBoxAchieve可動率 = new System.Windows.Forms.TextBox();
            labelAchieveAva = new System.Windows.Forms.Label();
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
            resources.ApplyResources(splitContainerMain, "splitContainerMain");
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            resources.ApplyResources(splitContainerMain.Panel1, "splitContainerMain.Panel1");
            splitContainerMain.Panel1.Controls.Add(button3);
            // 
            // splitContainerMain.Panel2
            // 
            resources.ApplyResources(splitContainerMain.Panel2, "splitContainerMain.Panel2");
            splitContainerMain.Panel2.Controls.Add(splitContainer上下);
            splitContainerMain.Panel2.Controls.Add(panel1);
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // splitContainer上下
            // 
            resources.ApplyResources(splitContainer上下, "splitContainer上下");
            splitContainer上下.Name = "splitContainer上下";
            // 
            // splitContainer上下.Panel1
            // 
            resources.ApplyResources(splitContainer上下.Panel1, "splitContainer上下.Panel1");
            splitContainer上下.Panel1.Controls.Add(splitContainer計画と実績);
            splitContainer上下.Panel1.Controls.Add(panelTitle);
            // 
            // splitContainer上下.Panel2
            // 
            resources.ApplyResources(splitContainer上下.Panel2, "splitContainer上下.Panel2");
            splitContainer上下.Panel2.Controls.Add(chart1);
            // 
            // splitContainer計画と実績
            // 
            resources.ApplyResources(splitContainer計画と実績, "splitContainer計画と実績");
            splitContainer計画と実績.Name = "splitContainer計画と実績";
            // 
            // splitContainer計画と実績.Panel1
            // 
            resources.ApplyResources(splitContainer計画と実績.Panel1, "splitContainer計画と実績.Panel1");
            splitContainer計画と実績.Panel1.Controls.Add(dataGridViewPlan);
            splitContainer計画と実績.Panel1.Controls.Add(panelPlanResult);
            splitContainer計画と実績.Panel1.Controls.Add(labelPlanCaption);
            // 
            // splitContainer計画と実績.Panel2
            // 
            resources.ApplyResources(splitContainer計画と実績.Panel2, "splitContainer計画と実績.Panel2");
            splitContainer計画と実績.Panel2.Controls.Add(dataGridViewAchieve);
            splitContainer計画と実績.Panel2.Controls.Add(panelAchieveResult);
            splitContainer計画と実績.Panel2.Controls.Add(labelAchieveCaption);
            // 
            // dataGridViewPlan
            // 
            resources.ApplyResources(dataGridViewPlan, "dataGridViewPlan");
            dataGridViewPlan.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewPlan.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewPlan.Name = "dataGridViewPlan";
            dataGridViewPlan.RowTemplate.Height = 21;
            // 
            // panelPlanResult
            // 
            resources.ApplyResources(panelPlanResult, "panelPlanResult");
            panelPlanResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPlanResult.Controls.Add(textBoxPlanQty);
            panelPlanResult.Controls.Add(labelPlanQty);
            panelPlanResult.Controls.Add(textBoxPlanEndTime);
            panelPlanResult.Controls.Add(textBoxPlanStartTime);
            panelPlanResult.Controls.Add(labelPlanTime);
            panelPlanResult.Controls.Add(textBoxPlan可動率);
            panelPlanResult.Controls.Add(labelPlanAva);
            panelPlanResult.Controls.Add(checkBoxPlan早昼);
            panelPlanResult.Controls.Add(checkBoxPlanピカピカ);
            panelPlanResult.Controls.Add(checkBoxPlan休憩稼働);
            panelPlanResult.Controls.Add(checkBoxPlanお昼稼働);
            panelPlanResult.Name = "panelPlanResult";
            // 
            // textBoxPlanQty
            // 
            resources.ApplyResources(textBoxPlanQty, "textBoxPlanQty");
            textBoxPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanQty.Name = "textBoxPlanQty";
            // 
            // labelPlanQty
            // 
            resources.ApplyResources(labelPlanQty, "labelPlanQty");
            labelPlanQty.Name = "labelPlanQty";
            // 
            // textBoxPlanEndTime
            // 
            resources.ApplyResources(textBoxPlanEndTime, "textBoxPlanEndTime");
            textBoxPlanEndTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanEndTime.Name = "textBoxPlanEndTime";
            // 
            // textBoxPlanStartTime
            // 
            resources.ApplyResources(textBoxPlanStartTime, "textBoxPlanStartTime");
            textBoxPlanStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanStartTime.Name = "textBoxPlanStartTime";
            // 
            // labelPlanTime
            // 
            resources.ApplyResources(labelPlanTime, "labelPlanTime");
            labelPlanTime.Name = "labelPlanTime";
            // 
            // textBoxPlan可動率
            // 
            resources.ApplyResources(textBoxPlan可動率, "textBoxPlan可動率");
            textBoxPlan可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlan可動率.Name = "textBoxPlan可動率";
            // 
            // labelPlanAva
            // 
            resources.ApplyResources(labelPlanAva, "labelPlanAva");
            labelPlanAva.Name = "labelPlanAva";
            // 
            // checkBoxPlan早昼
            // 
            resources.ApplyResources(checkBoxPlan早昼, "checkBoxPlan早昼");
            checkBoxPlan早昼.Name = "checkBoxPlan早昼";
            checkBoxPlan早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanピカピカ
            // 
            resources.ApplyResources(checkBoxPlanピカピカ, "checkBoxPlanピカピカ");
            checkBoxPlanピカピカ.Name = "checkBoxPlanピカピカ";
            checkBoxPlanピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlan休憩稼働
            // 
            resources.ApplyResources(checkBoxPlan休憩稼働, "checkBoxPlan休憩稼働");
            checkBoxPlan休憩稼働.Name = "checkBoxPlan休憩稼働";
            checkBoxPlan休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanお昼稼働
            // 
            resources.ApplyResources(checkBoxPlanお昼稼働, "checkBoxPlanお昼稼働");
            checkBoxPlanお昼稼働.Name = "checkBoxPlanお昼稼働";
            checkBoxPlanお昼稼働.UseVisualStyleBackColor = true;
            // 
            // labelPlanCaption
            // 
            resources.ApplyResources(labelPlanCaption, "labelPlanCaption");
            labelPlanCaption.BackColor = System.Drawing.Color.LightBlue;
            labelPlanCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelPlanCaption.Name = "labelPlanCaption";
            // 
            // dataGridViewAchieve
            // 
            resources.ApplyResources(dataGridViewAchieve, "dataGridViewAchieve");
            dataGridViewAchieve.AllowUserToAddRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewAchieve.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewAchieve.Name = "dataGridViewAchieve";
            dataGridViewAchieve.RowTemplate.Height = 21;
            // 
            // panelAchieveResult
            // 
            resources.ApplyResources(panelAchieveResult, "panelAchieveResult");
            panelAchieveResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelAchieveResult.Controls.Add(textBoxAchieveQty);
            panelAchieveResult.Controls.Add(labelAchieveQty);
            panelAchieveResult.Controls.Add(textBoxAchieveEndTime);
            panelAchieveResult.Controls.Add(textBoxAchieveStartTime);
            panelAchieveResult.Controls.Add(labelAchieveTime);
            panelAchieveResult.Controls.Add(textBoxAchieve可動率);
            panelAchieveResult.Controls.Add(labelAchieveAva);
            panelAchieveResult.Controls.Add(checkBoxAchieve早昼);
            panelAchieveResult.Controls.Add(checkBoxAchieveピカピカ);
            panelAchieveResult.Controls.Add(checkBoxAchieve休憩稼働);
            panelAchieveResult.Controls.Add(checkBoxAchieveお昼稼働);
            panelAchieveResult.Name = "panelAchieveResult";
            // 
            // textBoxAchieveQty
            // 
            resources.ApplyResources(textBoxAchieveQty, "textBoxAchieveQty");
            textBoxAchieveQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxAchieveQty.Name = "textBoxAchieveQty";
            // 
            // labelAchieveQty
            // 
            resources.ApplyResources(labelAchieveQty, "labelAchieveQty");
            labelAchieveQty.Name = "labelAchieveQty";
            // 
            // textBoxAchieveEndTime
            // 
            resources.ApplyResources(textBoxAchieveEndTime, "textBoxAchieveEndTime");
            textBoxAchieveEndTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxAchieveEndTime.Name = "textBoxAchieveEndTime";
            // 
            // textBoxAchieveStartTime
            // 
            resources.ApplyResources(textBoxAchieveStartTime, "textBoxAchieveStartTime");
            textBoxAchieveStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxAchieveStartTime.Name = "textBoxAchieveStartTime";
            // 
            // labelAchieveTime
            // 
            resources.ApplyResources(labelAchieveTime, "labelAchieveTime");
            labelAchieveTime.Name = "labelAchieveTime";
            // 
            // textBoxAchieve可動率
            // 
            resources.ApplyResources(textBoxAchieve可動率, "textBoxAchieve可動率");
            textBoxAchieve可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxAchieve可動率.Name = "textBoxAchieve可動率";
            // 
            // labelAchieveAva
            // 
            resources.ApplyResources(labelAchieveAva, "labelAchieveAva");
            labelAchieveAva.Name = "labelAchieveAva";
            // 
            // checkBoxAchieve早昼
            // 
            resources.ApplyResources(checkBoxAchieve早昼, "checkBoxAchieve早昼");
            checkBoxAchieve早昼.Name = "checkBoxAchieve早昼";
            checkBoxAchieve早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveピカピカ
            // 
            resources.ApplyResources(checkBoxAchieveピカピカ, "checkBoxAchieveピカピカ");
            checkBoxAchieveピカピカ.Name = "checkBoxAchieveピカピカ";
            checkBoxAchieveピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieve休憩稼働
            // 
            resources.ApplyResources(checkBoxAchieve休憩稼働, "checkBoxAchieve休憩稼働");
            checkBoxAchieve休憩稼働.Name = "checkBoxAchieve休憩稼働";
            checkBoxAchieve休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveお昼稼働
            // 
            resources.ApplyResources(checkBoxAchieveお昼稼働, "checkBoxAchieveお昼稼働");
            checkBoxAchieveお昼稼働.Name = "checkBoxAchieveお昼稼働";
            checkBoxAchieveお昼稼働.UseVisualStyleBackColor = true;
            // 
            // labelAchieveCaption
            // 
            resources.ApplyResources(labelAchieveCaption, "labelAchieveCaption");
            labelAchieveCaption.BackColor = System.Drawing.Color.MistyRose;
            labelAchieveCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelAchieveCaption.Name = "labelAchieveCaption";
            // 
            // panelTitle
            // 
            resources.ApplyResources(panelTitle, "panelTitle");
            panelTitle.Controls.Add(labelTitle);
            panelTitle.Name = "panelTitle";
            // 
            // labelTitle
            // 
            resources.ApplyResources(labelTitle, "labelTitle");
            labelTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTitle.Name = "labelTitle";
            // 
            // chart1
            // 
            resources.ApplyResources(chart1, "chart1");
            chartArea2.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart1.Legends.Add(legend2);
            chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart1.Series.Add(series2);
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(monthCalendar1);
            panel1.Controls.Add(buttonOrderList);
            panel1.Controls.Add(buttonPlanEntry);
            panel1.Controls.Add(buttonSettings);
            panel1.Controls.Add(buttonExit);
            panel1.Name = "panel1";
            // 
            // monthCalendar1
            // 
            resources.ApplyResources(monthCalendar1, "monthCalendar1");
            monthCalendar1.ForeColor = System.Drawing.Color.Red;
            monthCalendar1.Name = "monthCalendar1";
            // 
            // buttonOrderList
            // 
            resources.ApplyResources(buttonOrderList, "buttonOrderList");
            buttonOrderList.Name = "buttonOrderList";
            buttonOrderList.UseVisualStyleBackColor = false;
            buttonOrderList.Click += ButtonOrderList_Click;
            // 
            // buttonPlanEntry
            // 
            resources.ApplyResources(buttonPlanEntry, "buttonPlanEntry");
            buttonPlanEntry.Name = "buttonPlanEntry";
            buttonPlanEntry.UseVisualStyleBackColor = false;
            buttonPlanEntry.Click += ButtonPlanEntry_Click;
            // 
            // buttonSettings
            // 
            resources.ApplyResources(buttonSettings, "buttonSettings");
            buttonSettings.Name = "buttonSettings";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += ButtonSettings_Click;
            // 
            // buttonExit
            // 
            resources.ApplyResources(buttonExit, "buttonExit");
            buttonExit.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            buttonExit.Name = "buttonExit";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += ButtonExit_Click;
            // 
            // FormPlanProduction
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainerMain);
            Name = "FormPlanProduction";
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
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private System.Windows.Forms.DataGridView dataGridViewAchieve;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxPlan早昼;
        private System.Windows.Forms.CheckBox checkBoxPlanピカピカ;
        private System.Windows.Forms.CheckBox checkBoxPlan休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxPlanお昼稼働;
        private System.Windows.Forms.TextBox textBoxPlan可動率;
        private System.Windows.Forms.Label labelPlanAva;
        private System.Windows.Forms.TextBox textBoxAchieve可動率;
        private System.Windows.Forms.Label labelAchieveAva;
        private System.Windows.Forms.CheckBox checkBoxAchieve早昼;
        private System.Windows.Forms.CheckBox checkBoxAchieveピカピカ;
        private System.Windows.Forms.CheckBox checkBoxAchieve休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxAchieveお昼稼働;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBoxPlanEndTime;
        private System.Windows.Forms.TextBox textBoxPlanStartTime;
        private System.Windows.Forms.Label labelPlanTime;
        private System.Windows.Forms.TextBox textBoxPlanQty;
        private System.Windows.Forms.Label labelPlanQty;
        private System.Windows.Forms.TextBox textBoxAchieveQty;
        private System.Windows.Forms.Label labelAchieveQty;
        private System.Windows.Forms.TextBox textBoxAchieveEndTime;
        private System.Windows.Forms.TextBox textBoxAchieveStartTime;
        private System.Windows.Forms.Label labelAchieveTime;
    }
}

