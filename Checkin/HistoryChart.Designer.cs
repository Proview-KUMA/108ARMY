namespace InI
{
    partial class HistoryChart
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_YearTab1 = new System.Windows.Forms.ComboBox();
            this.btn_InqTab1 = new System.Windows.Forms.Button();
            this.chart_RealTest = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_YearTab2 = new System.Windows.Forms.ComboBox();
            this.btn_InqTab2 = new System.Windows.Forms.Button();
            this.chart_PassRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_YearTab3 = new System.Windows.Forms.ComboBox();
            this.btn_InqTab3 = new System.Windows.Forms.Button();
            this.chart_StandartReplace = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb_YearTab4 = new System.Windows.Forms.ComboBox();
            this.btn_InqTab4 = new System.Windows.Forms.Button();
            this.chart_RankTest = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_RealTest)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_PassRate)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_StandartReplace)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_RankTest)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 640);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbb_YearTab1);
            this.tabPage1.Controls.Add(this.btn_InqTab1);
            this.tabPage1.Controls.Add(this.chart_RealTest);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(842, 605);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "每月實測人數";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "年份：";
            // 
            // cbb_YearTab1
            // 
            this.cbb_YearTab1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_YearTab1.FormattingEnabled = true;
            this.cbb_YearTab1.Location = new System.Drawing.Point(73, 6);
            this.cbb_YearTab1.Name = "cbb_YearTab1";
            this.cbb_YearTab1.Size = new System.Drawing.Size(130, 30);
            this.cbb_YearTab1.TabIndex = 5;
            // 
            // btn_InqTab1
            // 
            this.btn_InqTab1.Location = new System.Drawing.Point(271, 4);
            this.btn_InqTab1.Name = "btn_InqTab1";
            this.btn_InqTab1.Size = new System.Drawing.Size(135, 32);
            this.btn_InqTab1.TabIndex = 4;
            this.btn_InqTab1.Text = "查詢";
            this.btn_InqTab1.UseVisualStyleBackColor = true;
            this.btn_InqTab1.Click += new System.EventHandler(this.btn_InqTab1_Click);
            // 
            // chart_RealTest
            // 
            this.chart_RealTest.Location = new System.Drawing.Point(7, 45);
            this.chart_RealTest.Name = "chart_RealTest";
            this.chart_RealTest.Size = new System.Drawing.Size(825, 550);
            this.chart_RealTest.TabIndex = 3;
            this.chart_RealTest.Text = "chart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbb_YearTab2);
            this.tabPage2.Controls.Add(this.btn_InqTab2);
            this.tabPage2.Controls.Add(this.chart_PassRate);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(842, 605);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "每月合格率";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "年份：";
            // 
            // cbb_YearTab2
            // 
            this.cbb_YearTab2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_YearTab2.FormattingEnabled = true;
            this.cbb_YearTab2.Location = new System.Drawing.Point(73, 6);
            this.cbb_YearTab2.Name = "cbb_YearTab2";
            this.cbb_YearTab2.Size = new System.Drawing.Size(130, 30);
            this.cbb_YearTab2.TabIndex = 7;
            // 
            // btn_InqTab2
            // 
            this.btn_InqTab2.Location = new System.Drawing.Point(271, 4);
            this.btn_InqTab2.Name = "btn_InqTab2";
            this.btn_InqTab2.Size = new System.Drawing.Size(135, 32);
            this.btn_InqTab2.TabIndex = 6;
            this.btn_InqTab2.Text = "查詢";
            this.btn_InqTab2.UseVisualStyleBackColor = true;
            this.btn_InqTab2.Click += new System.EventHandler(this.btn_InqTab2_Click);
            // 
            // chart_PassRate
            // 
            this.chart_PassRate.Location = new System.Drawing.Point(7, 45);
            this.chart_PassRate.Name = "chart_PassRate";
            this.chart_PassRate.Size = new System.Drawing.Size(825, 550);
            this.chart_PassRate.TabIndex = 5;
            this.chart_PassRate.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cbb_YearTab3);
            this.tabPage3.Controls.Add(this.btn_InqTab3);
            this.tabPage3.Controls.Add(this.chart_StandartReplace);
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(842, 605);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "每月標準及替代項目比率";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "年份：";
            // 
            // cbb_YearTab3
            // 
            this.cbb_YearTab3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_YearTab3.FormattingEnabled = true;
            this.cbb_YearTab3.Location = new System.Drawing.Point(73, 6);
            this.cbb_YearTab3.Name = "cbb_YearTab3";
            this.cbb_YearTab3.Size = new System.Drawing.Size(130, 30);
            this.cbb_YearTab3.TabIndex = 7;
            // 
            // btn_InqTab3
            // 
            this.btn_InqTab3.Location = new System.Drawing.Point(271, 4);
            this.btn_InqTab3.Name = "btn_InqTab3";
            this.btn_InqTab3.Size = new System.Drawing.Size(135, 32);
            this.btn_InqTab3.TabIndex = 6;
            this.btn_InqTab3.Text = "查詢";
            this.btn_InqTab3.UseVisualStyleBackColor = true;
            this.btn_InqTab3.Click += new System.EventHandler(this.btn_InqTab3_Click);
            // 
            // chart_StandartReplace
            // 
            this.chart_StandartReplace.Location = new System.Drawing.Point(7, 45);
            this.chart_StandartReplace.Name = "chart_StandartReplace";
            this.chart_StandartReplace.Size = new System.Drawing.Size(825, 550);
            this.chart_StandartReplace.TabIndex = 5;
            this.chart_StandartReplace.Text = "chart1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.cbb_YearTab4);
            this.tabPage4.Controls.Add(this.btn_InqTab4);
            this.tabPage4.Controls.Add(this.chart_RankTest);
            this.tabPage4.Location = new System.Drawing.Point(4, 31);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(842, 605);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "每年官士兵到測數";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "年份：";
            // 
            // cbb_YearTab4
            // 
            this.cbb_YearTab4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_YearTab4.FormattingEnabled = true;
            this.cbb_YearTab4.Location = new System.Drawing.Point(73, 6);
            this.cbb_YearTab4.Name = "cbb_YearTab4";
            this.cbb_YearTab4.Size = new System.Drawing.Size(130, 30);
            this.cbb_YearTab4.TabIndex = 9;
            // 
            // btn_InqTab4
            // 
            this.btn_InqTab4.Location = new System.Drawing.Point(271, 4);
            this.btn_InqTab4.Name = "btn_InqTab4";
            this.btn_InqTab4.Size = new System.Drawing.Size(135, 32);
            this.btn_InqTab4.TabIndex = 8;
            this.btn_InqTab4.Text = "查詢";
            this.btn_InqTab4.UseVisualStyleBackColor = true;
            this.btn_InqTab4.Click += new System.EventHandler(this.btn_InqTab4_Click);
            // 
            // chart_RankTest
            // 
            this.chart_RankTest.Location = new System.Drawing.Point(7, 45);
            this.chart_RankTest.Name = "chart_RankTest";
            this.chart_RankTest.Size = new System.Drawing.Size(825, 550);
            this.chart_RankTest.TabIndex = 7;
            this.chart_RankTest.Text = "chart1";
            // 
            // HistoryChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HistoryChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "歷史數據查詢";
            this.Load += new System.EventHandler(this.HistoryChart_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_RealTest)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_PassRate)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_StandartReplace)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_RankTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_InqTab1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_RealTest;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_InqTab2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_PassRate;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_InqTab3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_StandartReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_YearTab1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_YearTab2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_YearTab3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb_YearTab4;
        private System.Windows.Forms.Button btn_InqTab4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_RankTest;
    }
}