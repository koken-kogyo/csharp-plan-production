namespace PlanProduction
{
    partial class FormPlanEntry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            buttonClose = new System.Windows.Forms.Button();
            buttonSaveClose = new System.Windows.Forms.Button();
            buttonAchieveClear = new System.Windows.Forms.Button();
            buttonPlanCopy = new System.Windows.Forms.Button();
            buttonPlanClear = new System.Windows.Forms.Button();
            buttonPlanPrint = new System.Windows.Forms.Button();
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            dataGridViewPlan = new System.Windows.Forms.DataGridView();
            Plan品番 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            PlanCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan本数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan開始時刻 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan終了時刻 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan休憩時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan作業者 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Plan備考 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            labelPlanTitle = new System.Windows.Forms.Label();
            labelTitleOdCd = new System.Windows.Forms.Label();
            panelPlanOptions = new System.Windows.Forms.Panel();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            textBoxPlanEndTime = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            textBoxPlanStartTime = new System.Windows.Forms.TextBox();
            textBoxPlanKdo = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            textBoxPlan可動率 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBoxPlanCT = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            textBoxPlanQty = new System.Windows.Forms.TextBox();
            labelPlanQty2 = new System.Windows.Forms.Label();
            labelPlanQty = new System.Windows.Forms.Label();
            checkBoxPlan早昼 = new System.Windows.Forms.CheckBox();
            checkBoxPlanピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxPlan休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxPlanお昼稼働 = new System.Windows.Forms.CheckBox();
            dataGridViewAchieve = new System.Windows.Forms.DataGridView();
            Achieve品番 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            AchieveCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve本数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve開始時刻 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve終了時刻 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve休憩時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve作業者 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Achieve備考 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            labelAchieveTitle = new System.Windows.Forms.Label();
            labelTitleDate = new System.Windows.Forms.Label();
            panelAchieveOptions = new System.Windows.Forms.Panel();
            checkBoxAchieve早昼 = new System.Windows.Forms.CheckBox();
            checkBoxAchieveピカピカ = new System.Windows.Forms.CheckBox();
            checkBoxAchieve休憩稼働 = new System.Windows.Forms.CheckBox();
            checkBoxAchieveお昼稼働 = new System.Windows.Forms.CheckBox();
            tableLayoutPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlan).BeginInit();
            panelPlanOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAchieve).BeginInit();
            panelAchieveOptions.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.ColumnCount = 6;
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.666666F));
            tableLayoutPanelButtons.Controls.Add(buttonClose, 5, 0);
            tableLayoutPanelButtons.Controls.Add(buttonSaveClose, 4, 0);
            tableLayoutPanelButtons.Controls.Add(buttonAchieveClear, 3, 0);
            tableLayoutPanelButtons.Controls.Add(buttonPlanCopy, 2, 0);
            tableLayoutPanelButtons.Controls.Add(buttonPlanClear, 1, 0);
            tableLayoutPanelButtons.Controls.Add(buttonPlanPrint, 0, 0);
            tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 499);
            tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(4);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelButtons.Size = new System.Drawing.Size(1159, 55);
            tableLayoutPanelButtons.TabIndex = 1;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            buttonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonClose.Location = new System.Drawing.Point(964, 4);
            buttonClose.Margin = new System.Windows.Forms.Padding(4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(186, 47);
            buttonClose.TabIndex = 5;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += ButtonClose_Click;
            // 
            // buttonSaveClose
            // 
            buttonSaveClose.BackColor = System.Drawing.Color.LightCoral;
            buttonSaveClose.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonSaveClose.Location = new System.Drawing.Point(773, 4);
            buttonSaveClose.Margin = new System.Windows.Forms.Padding(4);
            buttonSaveClose.Name = "buttonSaveClose";
            buttonSaveClose.Size = new System.Drawing.Size(183, 47);
            buttonSaveClose.TabIndex = 4;
            buttonSaveClose.Text = "保存して閉じる";
            buttonSaveClose.UseVisualStyleBackColor = false;
            buttonSaveClose.Click += ButtonSaveClose_Click;
            // 
            // buttonAchieveClear
            // 
            buttonAchieveClear.BackColor = System.Drawing.Color.MistyRose;
            buttonAchieveClear.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonAchieveClear.Location = new System.Drawing.Point(582, 4);
            buttonAchieveClear.Margin = new System.Windows.Forms.Padding(4);
            buttonAchieveClear.Name = "buttonAchieveClear";
            buttonAchieveClear.Size = new System.Drawing.Size(183, 47);
            buttonAchieveClear.TabIndex = 3;
            buttonAchieveClear.Text = "実績クリア";
            buttonAchieveClear.UseVisualStyleBackColor = false;
            buttonAchieveClear.Click += ButtonAchieveClear_Click;
            // 
            // buttonPlanCopy
            // 
            buttonPlanCopy.BackColor = System.Drawing.Color.MistyRose;
            buttonPlanCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPlanCopy.Location = new System.Drawing.Point(391, 4);
            buttonPlanCopy.Margin = new System.Windows.Forms.Padding(4);
            buttonPlanCopy.Name = "buttonPlanCopy";
            buttonPlanCopy.Size = new System.Drawing.Size(183, 47);
            buttonPlanCopy.TabIndex = 2;
            buttonPlanCopy.Text = "実績へコピー";
            buttonPlanCopy.UseVisualStyleBackColor = false;
            // 
            // buttonPlanClear
            // 
            buttonPlanClear.BackColor = System.Drawing.Color.LightBlue;
            buttonPlanClear.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPlanClear.Location = new System.Drawing.Point(200, 4);
            buttonPlanClear.Margin = new System.Windows.Forms.Padding(4);
            buttonPlanClear.Name = "buttonPlanClear";
            buttonPlanClear.Size = new System.Drawing.Size(183, 47);
            buttonPlanClear.TabIndex = 1;
            buttonPlanClear.Text = "計画クリア";
            buttonPlanClear.UseVisualStyleBackColor = false;
            buttonPlanClear.Click += ButtonPlanClear_Click;
            // 
            // buttonPlanPrint
            // 
            buttonPlanPrint.BackColor = System.Drawing.Color.LightGreen;
            buttonPlanPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPlanPrint.Location = new System.Drawing.Point(9, 4);
            buttonPlanPrint.Margin = new System.Windows.Forms.Padding(4);
            buttonPlanPrint.Name = "buttonPlanPrint";
            buttonPlanPrint.Size = new System.Drawing.Size(183, 47);
            buttonPlanPrint.TabIndex = 0;
            buttonPlanPrint.Text = "計画を印刷";
            buttonPlanPrint.UseVisualStyleBackColor = false;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerMain.Location = new System.Drawing.Point(0, 0);
            splitContainerMain.Margin = new System.Windows.Forms.Padding(4);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(dataGridViewPlan);
            splitContainerMain.Panel1.Controls.Add(labelPlanTitle);
            splitContainerMain.Panel1.Controls.Add(labelTitleOdCd);
            splitContainerMain.Panel1.Controls.Add(panelPlanOptions);
            splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(10, 0, 3, 0);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(dataGridViewAchieve);
            splitContainerMain.Panel2.Controls.Add(labelAchieveTitle);
            splitContainerMain.Panel2.Controls.Add(labelTitleDate);
            splitContainerMain.Panel2.Controls.Add(panelAchieveOptions);
            splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 10, 0);
            splitContainerMain.Size = new System.Drawing.Size(1159, 499);
            splitContainerMain.SplitterDistance = 572;
            splitContainerMain.SplitterWidth = 5;
            splitContainerMain.TabIndex = 2;
            // 
            // dataGridViewPlan
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewPlan.ColumnHeadersHeight = 30;
            dataGridViewPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Plan品番, PlanCT, Plan本数, Plan開始時刻, Plan終了時刻, Plan休憩時間, Plan作業者, Plan備考 });
            dataGridViewPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewPlan.Location = new System.Drawing.Point(10, 70);
            dataGridViewPlan.Margin = new System.Windows.Forms.Padding(4);
            dataGridViewPlan.Name = "dataGridViewPlan";
            dataGridViewPlan.RowHeadersWidth = 30;
            dataGridViewPlan.RowTemplate.Height = 30;
            dataGridViewPlan.Size = new System.Drawing.Size(559, 335);
            dataGridViewPlan.TabIndex = 10;
            // 
            // Plan品番
            // 
            Plan品番.HeaderText = "品番";
            Plan品番.Name = "Plan品番";
            Plan品番.Width = 140;
            // 
            // PlanCT
            // 
            PlanCT.HeaderText = "CT";
            PlanCT.Name = "PlanCT";
            PlanCT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            PlanCT.Width = 45;
            // 
            // Plan本数
            // 
            Plan本数.HeaderText = "本数";
            Plan本数.Name = "Plan本数";
            Plan本数.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            Plan本数.Width = 55;
            // 
            // Plan開始時刻
            // 
            Plan開始時刻.HeaderText = "開始時刻";
            Plan開始時刻.Name = "Plan開始時刻";
            Plan開始時刻.Width = 55;
            // 
            // Plan終了時刻
            // 
            Plan終了時刻.HeaderText = "終了時刻";
            Plan終了時刻.Name = "Plan終了時刻";
            Plan終了時刻.Width = 55;
            // 
            // Plan休憩時間
            // 
            Plan休憩時間.HeaderText = "休憩時間";
            Plan休憩時間.Name = "Plan休憩時間";
            Plan休憩時間.Width = 55;
            // 
            // Plan作業者
            // 
            Plan作業者.HeaderText = "作業者";
            Plan作業者.Name = "Plan作業者";
            Plan作業者.Width = 50;
            // 
            // Plan備考
            // 
            Plan備考.HeaderText = "備考";
            Plan備考.Name = "Plan備考";
            // 
            // labelPlanTitle
            // 
            labelPlanTitle.BackColor = System.Drawing.Color.LightBlue;
            labelPlanTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelPlanTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelPlanTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelPlanTitle.Location = new System.Drawing.Point(10, 39);
            labelPlanTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPlanTitle.Name = "labelPlanTitle";
            labelPlanTitle.Size = new System.Drawing.Size(559, 31);
            labelPlanTitle.TabIndex = 7;
            labelPlanTitle.Text = "計画";
            labelPlanTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitleOdCd
            // 
            labelTitleOdCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTitleOdCd.Dock = System.Windows.Forms.DockStyle.Top;
            labelTitleOdCd.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Bold);
            labelTitleOdCd.Location = new System.Drawing.Point(10, 0);
            labelTitleOdCd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTitleOdCd.Name = "labelTitleOdCd";
            labelTitleOdCd.Size = new System.Drawing.Size(559, 39);
            labelTitleOdCd.TabIndex = 6;
            labelTitleOdCd.Text = "【6031A】 ＢＥ１曲げ";
            labelTitleOdCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPlanOptions
            // 
            panelPlanOptions.Controls.Add(textBoxPlanEndTime);
            panelPlanOptions.Controls.Add(textBoxPlanStartTime);
            panelPlanOptions.Controls.Add(label10);
            panelPlanOptions.Controls.Add(label9);
            panelPlanOptions.Controls.Add(label8);
            panelPlanOptions.Controls.Add(textBoxPlanKdo);
            panelPlanOptions.Controls.Add(label6);
            panelPlanOptions.Controls.Add(label7);
            panelPlanOptions.Controls.Add(label1);
            panelPlanOptions.Controls.Add(label3);
            panelPlanOptions.Controls.Add(textBoxPlan可動率);
            panelPlanOptions.Controls.Add(label2);
            panelPlanOptions.Controls.Add(textBoxPlanCT);
            panelPlanOptions.Controls.Add(label4);
            panelPlanOptions.Controls.Add(label5);
            panelPlanOptions.Controls.Add(textBoxPlanQty);
            panelPlanOptions.Controls.Add(labelPlanQty2);
            panelPlanOptions.Controls.Add(labelPlanQty);
            panelPlanOptions.Controls.Add(checkBoxPlan早昼);
            panelPlanOptions.Controls.Add(checkBoxPlanピカピカ);
            panelPlanOptions.Controls.Add(checkBoxPlan休憩稼働);
            panelPlanOptions.Controls.Add(checkBoxPlanお昼稼働);
            panelPlanOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelPlanOptions.Location = new System.Drawing.Point(10, 405);
            panelPlanOptions.Margin = new System.Windows.Forms.Padding(4);
            panelPlanOptions.Name = "panelPlanOptions";
            panelPlanOptions.Size = new System.Drawing.Size(559, 94);
            panelPlanOptions.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label10.Location = new System.Drawing.Point(420, 42);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(19, 15);
            label10.TabIndex = 25;
            label10.Text = "～";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label9.Location = new System.Drawing.Point(441, 70);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(55, 15);
            label9.TabIndex = 24;
            label9.Text = "終了時刻";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlanEndTime
            // 
            textBoxPlanEndTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanEndTime.Enabled = false;
            textBoxPlanEndTime.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBoxPlanEndTime.Location = new System.Drawing.Point(438, 30);
            textBoxPlanEndTime.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlanEndTime.Name = "textBoxPlanEndTime";
            textBoxPlanEndTime.Size = new System.Drawing.Size(61, 36);
            textBoxPlanEndTime.TabIndex = 23;
            textBoxPlanEndTime.Text = "17:10";
            textBoxPlanEndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label8.Location = new System.Drawing.Point(363, 70);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(55, 15);
            label8.TabIndex = 22;
            label8.Text = "開始時刻";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlanStartTime
            // 
            textBoxPlanStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanStartTime.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBoxPlanStartTime.Location = new System.Drawing.Point(360, 30);
            textBoxPlanStartTime.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlanStartTime.Name = "textBoxPlanStartTime";
            textBoxPlanStartTime.Size = new System.Drawing.Size(61, 36);
            textBoxPlanStartTime.TabIndex = 21;
            textBoxPlanStartTime.Text = "08:15";
            textBoxPlanStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBoxPlanStartTime.Enter += TextBoxPlanStartTime_Enter;
            textBoxPlanStartTime.KeyDown += TextBoxPlanStartTime_KeyDown;
            textBoxPlanStartTime.KeyPress += TextBoxPlanStartTime_KeyPress;
            textBoxPlanStartTime.MouseDown += TextBoxPlanStartTime_MouseDown;
            // 
            // textBoxPlanKdo
            // 
            textBoxPlanKdo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanKdo.Enabled = false;
            textBoxPlanKdo.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            textBoxPlanKdo.Location = new System.Drawing.Point(183, 35);
            textBoxPlanKdo.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlanKdo.Name = "textBoxPlanKdo";
            textBoxPlanKdo.Size = new System.Drawing.Size(49, 29);
            textBoxPlanKdo.TabIndex = 20;
            textBoxPlanKdo.Text = "8.95";
            textBoxPlanKdo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label6.Location = new System.Drawing.Point(234, 42);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(31, 15);
            label6.TabIndex = 19;
            label6.Text = "時間";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label7.Location = new System.Drawing.Point(180, 70);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(55, 15);
            label7.TabIndex = 18;
            label7.Text = "計画稼働";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 6F);
            label1.Location = new System.Drawing.Point(282, 81);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 11);
            label1.TabIndex = 17;
            label1.Text = "(べきどうりつ)";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label3.Location = new System.Drawing.Point(282, 68);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 15);
            label3.TabIndex = 16;
            label3.Text = "可動率";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlan可動率
            // 
            textBoxPlan可動率.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlan可動率.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            textBoxPlan可動率.Location = new System.Drawing.Point(282, 30);
            textBoxPlan可動率.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlan可動率.Name = "textBoxPlan可動率";
            textBoxPlan可動率.Size = new System.Drawing.Size(43, 36);
            textBoxPlan可動率.TabIndex = 15;
            textBoxPlan可動率.Text = "100";
            textBoxPlan可動率.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBoxPlan可動率.Enter += TextBoxPlan可動率_Enter;
            textBoxPlan可動率.KeyDown += TextBoxPlan可動率_KeyDown;
            textBoxPlan可動率.MouseDown += TextBoxPlan可動率_MouseDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(327, 42);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(19, 15);
            label2.TabIndex = 14;
            label2.Text = "％";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlanCT
            // 
            textBoxPlanCT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanCT.Enabled = false;
            textBoxPlanCT.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            textBoxPlanCT.Location = new System.Drawing.Point(93, 35);
            textBoxPlanCT.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlanCT.Name = "textBoxPlanCT";
            textBoxPlanCT.Size = new System.Drawing.Size(49, 29);
            textBoxPlanCT.TabIndex = 13;
            textBoxPlanCT.Text = "18.95";
            textBoxPlanCT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            label4.Location = new System.Drawing.Point(144, 42);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(31, 15);
            label4.TabIndex = 12;
            label4.Text = "時間";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            label5.Location = new System.Drawing.Point(92, 70);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 15);
            label5.TabIndex = 11;
            label5.Text = "CT×本数";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPlanQty
            // 
            textBoxPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPlanQty.Enabled = false;
            textBoxPlanQty.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            textBoxPlanQty.Location = new System.Drawing.Point(13, 35);
            textBoxPlanQty.Margin = new System.Windows.Forms.Padding(4);
            textBoxPlanQty.Name = "textBoxPlanQty";
            textBoxPlanQty.Size = new System.Drawing.Size(51, 29);
            textBoxPlanQty.TabIndex = 10;
            textBoxPlanQty.Text = "1,999";
            textBoxPlanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelPlanQty2
            // 
            labelPlanQty2.AutoSize = true;
            labelPlanQty2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            labelPlanQty2.Location = new System.Drawing.Point(66, 42);
            labelPlanQty2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPlanQty2.Name = "labelPlanQty2";
            labelPlanQty2.Size = new System.Drawing.Size(19, 15);
            labelPlanQty2.TabIndex = 9;
            labelPlanQty2.Text = "本";
            labelPlanQty2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPlanQty
            // 
            labelPlanQty.AutoSize = true;
            labelPlanQty.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            labelPlanQty.Location = new System.Drawing.Point(11, 70);
            labelPlanQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPlanQty.Name = "labelPlanQty";
            labelPlanQty.Size = new System.Drawing.Size(55, 15);
            labelPlanQty.TabIndex = 8;
            labelPlanQty.Text = "合計本数";
            labelPlanQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxPlan早昼
            // 
            checkBoxPlan早昼.AutoSize = true;
            checkBoxPlan早昼.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlan早昼.Location = new System.Drawing.Point(319, 8);
            checkBoxPlan早昼.Margin = new System.Windows.Forms.Padding(4);
            checkBoxPlan早昼.Name = "checkBoxPlan早昼";
            checkBoxPlan早昼.Size = new System.Drawing.Size(127, 19);
            checkBoxPlan早昼.TabIndex = 3;
            checkBoxPlan早昼.Text = "早昼 (11:30～12:15)";
            checkBoxPlan早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanピカピカ
            // 
            checkBoxPlanピカピカ.AutoSize = true;
            checkBoxPlanピカピカ.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlanピカピカ.Location = new System.Drawing.Point(213, 8);
            checkBoxPlanピカピカ.Margin = new System.Windows.Forms.Padding(4);
            checkBoxPlanピカピカ.Name = "checkBoxPlanピカピカ";
            checkBoxPlanピカピカ.Size = new System.Drawing.Size(98, 19);
            checkBoxPlanピカピカ.TabIndex = 2;
            checkBoxPlanピカピカ.Text = "ピカピカ大作戦";
            checkBoxPlanピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlan休憩稼働
            // 
            checkBoxPlan休憩稼働.AutoSize = true;
            checkBoxPlan休憩稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlan休憩稼働.Location = new System.Drawing.Point(107, 8);
            checkBoxPlan休憩稼働.Margin = new System.Windows.Forms.Padding(4);
            checkBoxPlan休憩稼働.Name = "checkBoxPlan休憩稼働";
            checkBoxPlan休憩稼働.Size = new System.Drawing.Size(98, 19);
            checkBoxPlan休憩稼働.TabIndex = 1;
            checkBoxPlan休憩稼働.Text = "休憩時間稼働";
            checkBoxPlan休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlanお昼稼働
            // 
            checkBoxPlanお昼稼働.AutoSize = true;
            checkBoxPlanお昼稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxPlanお昼稼働.Location = new System.Drawing.Point(4, 8);
            checkBoxPlanお昼稼働.Margin = new System.Windows.Forms.Padding(4);
            checkBoxPlanお昼稼働.Name = "checkBoxPlanお昼稼働";
            checkBoxPlanお昼稼働.Size = new System.Drawing.Size(95, 19);
            checkBoxPlanお昼稼働.TabIndex = 0;
            checkBoxPlanお昼稼働.Text = "お昼休み稼働";
            checkBoxPlanお昼稼働.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAchieve
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewAchieve.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewAchieve.ColumnHeadersHeight = 30;
            dataGridViewAchieve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewAchieve.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Achieve品番, AchieveCT, Achieve本数, Achieve開始時刻, Achieve終了時刻, Achieve休憩時間, Achieve作業者, Achieve備考 });
            dataGridViewAchieve.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewAchieve.Location = new System.Drawing.Point(3, 70);
            dataGridViewAchieve.Margin = new System.Windows.Forms.Padding(4);
            dataGridViewAchieve.Name = "dataGridViewAchieve";
            dataGridViewAchieve.RowHeadersWidth = 30;
            dataGridViewAchieve.RowTemplate.Height = 30;
            dataGridViewAchieve.Size = new System.Drawing.Size(569, 335);
            dataGridViewAchieve.TabIndex = 10;
            // 
            // Achieve品番
            // 
            Achieve品番.HeaderText = "品番";
            Achieve品番.Name = "Achieve品番";
            Achieve品番.Width = 140;
            // 
            // AchieveCT
            // 
            AchieveCT.HeaderText = "CT";
            AchieveCT.Name = "AchieveCT";
            AchieveCT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            AchieveCT.Width = 45;
            // 
            // Achieve本数
            // 
            Achieve本数.HeaderText = "本数";
            Achieve本数.Name = "Achieve本数";
            Achieve本数.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            Achieve本数.Width = 55;
            // 
            // Achieve開始時刻
            // 
            Achieve開始時刻.HeaderText = "開始時刻";
            Achieve開始時刻.Name = "Achieve開始時刻";
            Achieve開始時刻.Width = 55;
            // 
            // Achieve終了時刻
            // 
            Achieve終了時刻.HeaderText = "終了時刻";
            Achieve終了時刻.Name = "Achieve終了時刻";
            Achieve終了時刻.Width = 55;
            // 
            // Achieve休憩時間
            // 
            Achieve休憩時間.HeaderText = "休憩時間";
            Achieve休憩時間.Name = "Achieve休憩時間";
            Achieve休憩時間.Width = 55;
            // 
            // Achieve作業者
            // 
            Achieve作業者.HeaderText = "作業者";
            Achieve作業者.Name = "Achieve作業者";
            Achieve作業者.Width = 50;
            // 
            // Achieve備考
            // 
            Achieve備考.HeaderText = "備考";
            Achieve備考.Name = "Achieve備考";
            // 
            // labelAchieveTitle
            // 
            labelAchieveTitle.BackColor = System.Drawing.Color.MistyRose;
            labelAchieveTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelAchieveTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelAchieveTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            labelAchieveTitle.Location = new System.Drawing.Point(3, 39);
            labelAchieveTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelAchieveTitle.Name = "labelAchieveTitle";
            labelAchieveTitle.Size = new System.Drawing.Size(569, 31);
            labelAchieveTitle.TabIndex = 5;
            labelAchieveTitle.Text = "実績";
            labelAchieveTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitleDate
            // 
            labelTitleDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelTitleDate.Dock = System.Windows.Forms.DockStyle.Top;
            labelTitleDate.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Bold);
            labelTitleDate.Location = new System.Drawing.Point(3, 0);
            labelTitleDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTitleDate.Name = "labelTitleDate";
            labelTitleDate.Size = new System.Drawing.Size(569, 39);
            labelTitleDate.TabIndex = 4;
            labelTitleDate.Text = "【3/21】 計画と実績";
            labelTitleDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAchieveOptions
            // 
            panelAchieveOptions.Controls.Add(checkBoxAchieve早昼);
            panelAchieveOptions.Controls.Add(checkBoxAchieveピカピカ);
            panelAchieveOptions.Controls.Add(checkBoxAchieve休憩稼働);
            panelAchieveOptions.Controls.Add(checkBoxAchieveお昼稼働);
            panelAchieveOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelAchieveOptions.Location = new System.Drawing.Point(3, 405);
            panelAchieveOptions.Margin = new System.Windows.Forms.Padding(4);
            panelAchieveOptions.Name = "panelAchieveOptions";
            panelAchieveOptions.Size = new System.Drawing.Size(569, 94);
            panelAchieveOptions.TabIndex = 2;
            // 
            // checkBoxAchieve早昼
            // 
            checkBoxAchieve早昼.AutoSize = true;
            checkBoxAchieve早昼.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieve早昼.Location = new System.Drawing.Point(320, 8);
            checkBoxAchieve早昼.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAchieve早昼.Name = "checkBoxAchieve早昼";
            checkBoxAchieve早昼.Size = new System.Drawing.Size(127, 19);
            checkBoxAchieve早昼.TabIndex = 7;
            checkBoxAchieve早昼.Text = "早昼 (11:30～12:15)";
            checkBoxAchieve早昼.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveピカピカ
            // 
            checkBoxAchieveピカピカ.AutoSize = true;
            checkBoxAchieveピカピカ.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieveピカピカ.Location = new System.Drawing.Point(214, 8);
            checkBoxAchieveピカピカ.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAchieveピカピカ.Name = "checkBoxAchieveピカピカ";
            checkBoxAchieveピカピカ.Size = new System.Drawing.Size(98, 19);
            checkBoxAchieveピカピカ.TabIndex = 6;
            checkBoxAchieveピカピカ.Text = "ピカピカ大作戦";
            checkBoxAchieveピカピカ.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieve休憩稼働
            // 
            checkBoxAchieve休憩稼働.AutoSize = true;
            checkBoxAchieve休憩稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieve休憩稼働.Location = new System.Drawing.Point(108, 8);
            checkBoxAchieve休憩稼働.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAchieve休憩稼働.Name = "checkBoxAchieve休憩稼働";
            checkBoxAchieve休憩稼働.Size = new System.Drawing.Size(98, 19);
            checkBoxAchieve休憩稼働.TabIndex = 5;
            checkBoxAchieve休憩稼働.Text = "休憩時間稼働";
            checkBoxAchieve休憩稼働.UseVisualStyleBackColor = true;
            // 
            // checkBoxAchieveお昼稼働
            // 
            checkBoxAchieveお昼稼働.AutoSize = true;
            checkBoxAchieveお昼稼働.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            checkBoxAchieveお昼稼働.Location = new System.Drawing.Point(5, 8);
            checkBoxAchieveお昼稼働.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAchieveお昼稼働.Name = "checkBoxAchieveお昼稼働";
            checkBoxAchieveお昼稼働.Size = new System.Drawing.Size(95, 19);
            checkBoxAchieveお昼稼働.TabIndex = 4;
            checkBoxAchieveお昼稼働.Text = "お昼休み稼働";
            checkBoxAchieveお昼稼働.UseVisualStyleBackColor = true;
            // 
            // FormPlanEntry
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1159, 554);
            Controls.Add(splitContainerMain);
            Controls.Add(tableLayoutPanelButtons);
            Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "FormPlanEntry";
            Text = "[生産計画] 計画と実績の入力";
            FormClosing += FormPlanEntry_FormClosing;
            Load += FormPlanEntry_Load;
            KeyDown += FormOrderList_KeyDown;
            tableLayoutPanelButtons.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlan).EndInit();
            panelPlanOptions.ResumeLayout(false);
            panelPlanOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAchieve).EndInit();
            panelAchieveOptions.ResumeLayout(false);
            panelAchieveOptions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelPlanOptions;
        private System.Windows.Forms.Panel panelAchieveOptions;
        private System.Windows.Forms.Button buttonPlanPrint;
        private System.Windows.Forms.CheckBox checkBoxPlan早昼;
        private System.Windows.Forms.CheckBox checkBoxPlanピカピカ;
        private System.Windows.Forms.CheckBox checkBoxPlan休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxPlanお昼稼働;
        private System.Windows.Forms.CheckBox checkBoxAchieve早昼;
        private System.Windows.Forms.CheckBox checkBoxAchieveピカピカ;
        private System.Windows.Forms.CheckBox checkBoxAchieve休憩稼働;
        private System.Windows.Forms.CheckBox checkBoxAchieveお昼稼働;
        private System.Windows.Forms.TextBox textBoxPlanCT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPlanQty;
        private System.Windows.Forms.Label labelPlanQty2;
        private System.Windows.Forms.Label labelPlanQty;
        private System.Windows.Forms.TextBox textBoxPlanKdo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlan可動率;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPlanTitle;
        private System.Windows.Forms.Label labelTitleOdCd;
        private System.Windows.Forms.Label labelAchieveTitle;
        private System.Windows.Forms.Label labelTitleDate;
        private System.Windows.Forms.DataGridView dataGridViewAchieve;
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan品番;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanCT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan本数;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan開始時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan終了時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan休憩時間;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan作業者;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plan備考;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve品番;
        private System.Windows.Forms.DataGridViewTextBoxColumn AchieveCT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve本数;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve開始時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve終了時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve休憩時間;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve作業者;
        private System.Windows.Forms.DataGridViewTextBoxColumn Achieve備考;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxPlanStartTime;
        private System.Windows.Forms.Button buttonPlanClear;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSaveClose;
        private System.Windows.Forms.Button buttonAchieveClear;
        private System.Windows.Forms.Button buttonPlanCopy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPlanEndTime;
    }
}