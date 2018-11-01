namespace InI
{
    partial class ResultChart
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Update_7DayPlayerCount = new System.Windows.Forms.Button();
            this.chart_7DayPlayerCount = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.chart_7DayInTest = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.chart_7DaySeleteOther = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.chart_SeleteOtherItem = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DayPlayerCount)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DayInTest)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DaySeleteOther)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_SeleteOtherItem)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 640);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.btn_Update_7DayPlayerCount);
            this.tabPage1.Controls.Add(this.chart_7DayPlayerCount);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(842, 605);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未來7日網路報進人數";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(522, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 30);
            this.textBox1.TabIndex = 5;
            // 
            // btn_Update_7DayPlayerCount
            // 
            this.btn_Update_7DayPlayerCount.Location = new System.Drawing.Point(700, 6);
            this.btn_Update_7DayPlayerCount.Name = "btn_Update_7DayPlayerCount";
            this.btn_Update_7DayPlayerCount.Size = new System.Drawing.Size(135, 32);
            this.btn_Update_7DayPlayerCount.TabIndex = 4;
            this.btn_Update_7DayPlayerCount.Text = "立即更新";
            this.btn_Update_7DayPlayerCount.UseVisualStyleBackColor = true;
            this.btn_Update_7DayPlayerCount.Click += new System.EventHandler(this.btn_Update_7DayPlayerCount_Click);
            // 
            // chart_7DayPlayerCount
            // 
            this.chart_7DayPlayerCount.Location = new System.Drawing.Point(7, 45);
            this.chart_7DayPlayerCount.Name = "chart_7DayPlayerCount";
            this.chart_7DayPlayerCount.Size = new System.Drawing.Size(825, 550);
            this.chart_7DayPlayerCount.TabIndex = 3;
            this.chart_7DayPlayerCount.Text = "chart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.chart_7DayInTest);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(842, 605);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "近7日網路報進到測率";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(700, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "立即更新";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chart_7DayInTest
            // 
            this.chart_7DayInTest.Location = new System.Drawing.Point(7, 45);
            this.chart_7DayInTest.Name = "chart_7DayInTest";
            this.chart_7DayInTest.Size = new System.Drawing.Size(825, 550);
            this.chart_7DayInTest.TabIndex = 5;
            this.chart_7DayInTest.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.chart_7DaySeleteOther);
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(842, 605);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "近7日多元選項比例";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(700, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "立即更新";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // chart_7DaySeleteOther
            // 
            this.chart_7DaySeleteOther.Location = new System.Drawing.Point(7, 45);
            this.chart_7DaySeleteOther.Name = "chart_7DaySeleteOther";
            this.chart_7DaySeleteOther.Size = new System.Drawing.Size(825, 550);
            this.chart_7DaySeleteOther.TabIndex = 5;
            this.chart_7DaySeleteOther.Text = "chart1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.chart_SeleteOtherItem);
            this.tabPage4.Location = new System.Drawing.Point(4, 31);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(842, 605);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "單日多元選項比例";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(700, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 32);
            this.button3.TabIndex = 6;
            this.button3.Text = "立即更新";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // chart_SeleteOtherItem
            // 
            this.chart_SeleteOtherItem.Location = new System.Drawing.Point(7, 45);
            this.chart_SeleteOtherItem.Name = "chart_SeleteOtherItem";
            this.chart_SeleteOtherItem.Size = new System.Drawing.Size(825, 550);
            this.chart_SeleteOtherItem.TabIndex = 5;
            this.chart_SeleteOtherItem.Text = "chart1";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button4);
            this.tabPage5.Controls.Add(this.chart4);
            this.tabPage5.Location = new System.Drawing.Point(4, 31);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(842, 605);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "單日單項合格率";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(700, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 32);
            this.button4.TabIndex = 6;
            this.button4.Text = "立即更新";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // chart4
            // 
            this.chart4.Location = new System.Drawing.Point(7, 45);
            this.chart4.Name = "chart4";
            this.chart4.Size = new System.Drawing.Size(825, 550);
            this.chart4.TabIndex = 5;
            this.chart4.Text = "chart1";
            // 
            // ResultChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ResultChart";
            this.Text = "數據查詢";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResultChart_FormClosing);
            this.Load += new System.EventHandler(this.ResultChart_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DayPlayerCount)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DayInTest)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_7DaySeleteOther)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_SeleteOtherItem)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_7DayPlayerCount;
        private System.Windows.Forms.Button btn_Update_7DayPlayerCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_7DayInTest;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_7DaySeleteOther;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_SeleteOtherItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.TextBox textBox1;
    }
}