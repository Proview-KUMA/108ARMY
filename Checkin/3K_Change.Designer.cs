namespace InI
{
    partial class _3K_Change
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbt_800_Swim = new System.Windows.Forms.RadioButton();
            this.rbt_Jump = new System.Windows.Forms.RadioButton();
            this.rbt_5K = new System.Windows.Forms.RadioButton();
            this.rbt_3K = new System.Windows.Forms.RadioButton();
            this.btn_Enter = new System.Windows.Forms.Button();
            this.btn_Ese = new System.Windows.Forms.Button();
            this.lab_age45up = new System.Windows.Forms.Label();
            this.lab_agealert = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbt_800_Swim);
            this.groupBox1.Controls.Add(this.rbt_Jump);
            this.groupBox1.Controls.Add(this.rbt_5K);
            this.groupBox1.Controls.Add(this.rbt_3K);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(25, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 228);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "無法實施測驗項目";
            // 
            // rbt_800_Swim
            // 
            this.rbt_800_Swim.AutoSize = true;
            this.rbt_800_Swim.BackColor = System.Drawing.SystemColors.Control;
            this.rbt_800_Swim.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbt_800_Swim.ForeColor = System.Drawing.Color.Blue;
            this.rbt_800_Swim.Location = new System.Drawing.Point(20, 169);
            this.rbt_800_Swim.Name = "rbt_800_Swim";
            this.rbt_800_Swim.Size = new System.Drawing.Size(223, 39);
            this.rbt_800_Swim.TabIndex = 4;
            this.rbt_800_Swim.Text = "800公尺游走(4)";
            this.rbt_800_Swim.UseVisualStyleBackColor = false;
            this.rbt_800_Swim.CheckedChanged += new System.EventHandler(this.rbt_800_Swim_CheckedChanged);
            // 
            // rbt_Jump
            // 
            this.rbt_Jump.AutoSize = true;
            this.rbt_Jump.BackColor = System.Drawing.SystemColors.Control;
            this.rbt_Jump.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbt_Jump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.rbt_Jump.Location = new System.Drawing.Point(20, 124);
            this.rbt_Jump.Name = "rbt_Jump";
            this.rbt_Jump.Size = new System.Drawing.Size(191, 39);
            this.rbt_Jump.TabIndex = 3;
            this.rbt_Jump.Text = "5分鐘跳繩(3)";
            this.rbt_Jump.UseVisualStyleBackColor = false;
            this.rbt_Jump.CheckedChanged += new System.EventHandler(this.rbt_Jump_CheckedChanged);
            // 
            // rbt_5K
            // 
            this.rbt_5K.AutoSize = true;
            this.rbt_5K.BackColor = System.Drawing.SystemColors.Control;
            this.rbt_5K.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbt_5K.ForeColor = System.Drawing.Color.Green;
            this.rbt_5K.Location = new System.Drawing.Point(20, 79);
            this.rbt_5K.Name = "rbt_5K";
            this.rbt_5K.Size = new System.Drawing.Size(191, 39);
            this.rbt_5K.TabIndex = 2;
            this.rbt_5K.Text = "5公里健走(2)";
            this.rbt_5K.UseVisualStyleBackColor = false;
            this.rbt_5K.CheckedChanged += new System.EventHandler(this.rbt_5K_CheckedChanged);
            // 
            // rbt_3K
            // 
            this.rbt_3K.AutoSize = true;
            this.rbt_3K.BackColor = System.Drawing.Color.Yellow;
            this.rbt_3K.Checked = true;
            this.rbt_3K.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbt_3K.Location = new System.Drawing.Point(20, 34);
            this.rbt_3K.Name = "rbt_3K";
            this.rbt_3K.Size = new System.Drawing.Size(283, 39);
            this.rbt_3K.TabIndex = 1;
            this.rbt_3K.TabStop = true;
            this.rbt_3K.Text = "三千公尺徒手跑步(1)";
            this.rbt_3K.UseVisualStyleBackColor = false;
            this.rbt_3K.CheckedChanged += new System.EventHandler(this.rbt_3K_CheckedChanged);
            // 
            // btn_Enter
            // 
            this.btn_Enter.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_Enter.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Enter.Location = new System.Drawing.Point(25, 271);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(157, 49);
            this.btn_Enter.TabIndex = 5;
            this.btn_Enter.Text = "確認送出(Enter)";
            this.btn_Enter.UseVisualStyleBackColor = false;
            this.btn_Enter.Click += new System.EventHandler(this.btn_Enter_Click);
            // 
            // btn_Ese
            // 
            this.btn_Ese.BackColor = System.Drawing.Color.HotPink;
            this.btn_Ese.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Ese.Location = new System.Drawing.Point(217, 271);
            this.btn_Ese.Name = "btn_Ese";
            this.btn_Ese.Size = new System.Drawing.Size(157, 49);
            this.btn_Ese.TabIndex = 6;
            this.btn_Ese.Text = "離開(ESC)";
            this.btn_Ese.UseVisualStyleBackColor = false;
            this.btn_Ese.Click += new System.EventHandler(this.btn_Ese_Click);
            // 
            // lab_age45up
            // 
            this.lab_age45up.AutoSize = true;
            this.lab_age45up.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_age45up.ForeColor = System.Drawing.Color.Yellow;
            this.lab_age45up.Location = new System.Drawing.Point(21, 325);
            this.lab_age45up.Name = "lab_age45up";
            this.lab_age45up.Size = new System.Drawing.Size(385, 24);
            this.lab_age45up.TabIndex = 7;
            this.lab_age45up.Text = "[多元選項]若未符合年齡條件，需出示證明。";
            // 
            // lab_agealert
            // 
            this.lab_agealert.AutoSize = true;
            this.lab_agealert.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_agealert.ForeColor = System.Drawing.Color.Red;
            this.lab_agealert.Location = new System.Drawing.Point(21, 356);
            this.lab_agealert.Name = "lab_agealert";
            this.lab_agealert.Size = new System.Drawing.Size(326, 24);
            this.lab_agealert.TabIndex = 8;
            this.lab_agealert.Text = "(年齡45歲(含)以上，可選取多元選項)";
            // 
            // _3K_Change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(399, 389);
            this.ControlBox = false;
            this.Controls.Add(this.lab_agealert);
            this.Controls.Add(this.lab_age45up);
            this.Controls.Add(this.btn_Ese);
            this.Controls.Add(this.btn_Enter);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "_3K_Change";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多元選項";
            this.Load += new System.EventHandler(this._3K_Change_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._3K_Change_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Enter;
        private System.Windows.Forms.RadioButton rbt_800_Swim;
        private System.Windows.Forms.RadioButton rbt_Jump;
        private System.Windows.Forms.RadioButton rbt_5K;
        private System.Windows.Forms.RadioButton rbt_3K;
        private System.Windows.Forms.Button btn_Ese;
        private System.Windows.Forms.Label lab_age45up;
        private System.Windows.Forms.Label lab_agealert;
    }
}