namespace InI
{
    partial class SystemTimeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Check_Close = new System.Windows.Forms.Button();
            this.lab_SysTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "檢錄電腦系統時間：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(16, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "請確認檢錄電腦系統時間是否正確，";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(16, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(485, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "確保「年齡計算」及「成績標準」正確性。";
            // 
            // btn_Check_Close
            // 
            this.btn_Check_Close.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Check_Close.Location = new System.Drawing.Point(369, 205);
            this.btn_Check_Close.Name = "btn_Check_Close";
            this.btn_Check_Close.Size = new System.Drawing.Size(157, 47);
            this.btn_Check_Close.TabIndex = 0;
            this.btn_Check_Close.Text = "確認後關閉";
            this.btn_Check_Close.UseVisualStyleBackColor = true;
            this.btn_Check_Close.Click += new System.EventHandler(this.btn_Check_Close_Click);
            // 
            // lab_SysTime
            // 
            this.lab_SysTime.AutoSize = true;
            this.lab_SysTime.Font = new System.Drawing.Font("新細明體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_SysTime.ForeColor = System.Drawing.Color.Blue;
            this.lab_SysTime.Location = new System.Drawing.Point(14, 62);
            this.lab_SysTime.Name = "lab_SysTime";
            this.lab_SysTime.Size = new System.Drawing.Size(0, 29);
            this.lab_SysTime.TabIndex = 4;
            // 
            // SystemTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(569, 264);
            this.ControlBox = false;
            this.Controls.Add(this.lab_SysTime);
            this.Controls.Add(this.btn_Check_Close);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SystemTimeForm";
            this.Text = "檢錄電腦系統時間";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemTimeForm_FormClosing);
            this.Load += new System.EventHandler(this.SystemTimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Check_Close;
        private System.Windows.Forms.Label lab_SysTime;
    }
}