namespace ScoreClose
{
    partial class ScoreCloseForm2
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
            this.date = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.RB_id = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_id = new System.Windows.Forms.TextBox();
            this.RB_clothesNum = new System.Windows.Forms.RadioButton();
            this.TB_clothesNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // date
            // 
            this.date.CalendarFont = new System.Drawing.Font("標楷體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.date.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.date.Location = new System.Drawing.Point(169, 9);
            this.date.MaxDate = new System.DateTime(4000, 11, 18, 0, 0, 0, 0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(200, 30);
            this.date.TabIndex = 0;
            this.date.Value = new System.DateTime(2013, 11, 19, 0, 0, 0, 0);
            this.date.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(166, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "檢視成績單";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RB_id
            // 
            this.RB_id.AutoSize = true;
            this.RB_id.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RB_id.Location = new System.Drawing.Point(12, 148);
            this.RB_id.Name = "RB_id";
            this.RB_id.Size = new System.Drawing.Size(148, 28);
            this.RB_id.TabIndex = 2;
            this.RB_id.Text = "身分證字號";
            this.RB_id.UseVisualStyleBackColor = true;
            this.RB_id.CheckedChanged += new System.EventHandler(this.RB_id_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "選擇鑑測日期";
            // 
            // TB_id
            // 
            this.TB_id.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_id.Location = new System.Drawing.Point(166, 147);
            this.TB_id.MaxLength = 10;
            this.TB_id.Name = "TB_id";
            this.TB_id.Size = new System.Drawing.Size(201, 36);
            this.TB_id.TabIndex = 4;
            this.TB_id.TextChanged += new System.EventHandler(this.TB_id_TextChanged);
            // 
            // RB_clothesNum
            // 
            this.RB_clothesNum.AutoSize = true;
            this.RB_clothesNum.Checked = true;
            this.RB_clothesNum.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RB_clothesNum.Location = new System.Drawing.Point(84, 99);
            this.RB_clothesNum.Name = "RB_clothesNum";
            this.RB_clothesNum.Size = new System.Drawing.Size(76, 28);
            this.RB_clothesNum.TabIndex = 6;
            this.RB_clothesNum.TabStop = true;
            this.RB_clothesNum.Text = "背號";
            this.RB_clothesNum.UseVisualStyleBackColor = true;
            // 
            // TB_clothesNum
            // 
            this.TB_clothesNum.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_clothesNum.Location = new System.Drawing.Point(166, 94);
            this.TB_clothesNum.MaxLength = 10;
            this.TB_clothesNum.Name = "TB_clothesNum";
            this.TB_clothesNum.Size = new System.Drawing.Size(201, 36);
            this.TB_clothesNum.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(11, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "輸入背號或身分證字號";
            // 
            // ScoreCloseForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(388, 255);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_clothesNum);
            this.Controls.Add(this.RB_clothesNum);
            this.Controls.Add(this.TB_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RB_id);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.date);
            this.Name = "ScoreCloseForm2";
            this.Text = "成績結算";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton RB_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_id;
        private System.Windows.Forms.RadioButton RB_clothesNum;
        private System.Windows.Forms.TextBox TB_clothesNum;
        private System.Windows.Forms.Label label2;



    }
}