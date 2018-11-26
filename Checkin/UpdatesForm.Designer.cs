namespace InI
{
    partial class UpdatesForm
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
            this.txb_Id = new System.Windows.Forms.TextBox();
            this.btn_InqData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lab_status = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_date = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lab_Age = new System.Windows.Forms.Label();
            this.dtp_Birth = new System.Windows.Forms.DateTimePicker();
            this.btn_Update = new System.Windows.Forms.Button();
            this.txb_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lab_ingMsg = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "身份證字號：";
            // 
            // txb_Id
            // 
            this.txb_Id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txb_Id.Location = new System.Drawing.Point(143, 10);
            this.txb_Id.MaxLength = 10;
            this.txb_Id.Name = "txb_Id";
            this.txb_Id.Size = new System.Drawing.Size(174, 33);
            this.txb_Id.TabIndex = 1;
            // 
            // btn_InqData
            // 
            this.btn_InqData.Location = new System.Drawing.Point(339, 10);
            this.btn_InqData.Name = "btn_InqData";
            this.btn_InqData.Size = new System.Drawing.Size(106, 33);
            this.btn_InqData.TabIndex = 2;
            this.btn_InqData.Text = "查詢";
            this.btn_InqData.UseVisualStyleBackColor = true;
            this.btn_InqData.Click += new System.EventHandler(this.btn_InqData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_status);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lab_date);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.lab_Age);
            this.groupBox1.Controls.Add(this.dtp_Birth);
            this.groupBox1.Controls.Add(this.btn_Update);
            this.groupBox1.Controls.Add(this.txb_Name);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(17, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 322);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查詢結果";
            this.groupBox1.Visible = false;
            // 
            // lab_status
            // 
            this.lab_status.AutoSize = true;
            this.lab_status.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lab_status.Location = new System.Drawing.Point(117, 75);
            this.lab_status.Name = "lab_status";
            this.lab_status.Size = new System.Drawing.Size(0, 24);
            this.lab_status.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "鑑測狀態：";
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lab_date.Location = new System.Drawing.Point(117, 30);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(0, 24);
            this.lab_date.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "鑑測日期：";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.Red;
            this.btn_Cancel.Location = new System.Drawing.Point(10, 260);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(143, 44);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lab_Age
            // 
            this.lab_Age.AutoSize = true;
            this.lab_Age.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Age.ForeColor = System.Drawing.Color.Blue;
            this.lab_Age.Location = new System.Drawing.Point(79, 210);
            this.lab_Age.Name = "lab_Age";
            this.lab_Age.Size = new System.Drawing.Size(0, 24);
            this.lab_Age.TabIndex = 9;
            // 
            // dtp_Birth
            // 
            this.dtp_Birth.Location = new System.Drawing.Point(79, 165);
            this.dtp_Birth.Name = "dtp_Birth";
            this.dtp_Birth.Size = new System.Drawing.Size(200, 33);
            this.dtp_Birth.TabIndex = 8;
            this.dtp_Birth.ValueChanged += new System.EventHandler(this.dtp_Birth_ValueChanged);
            // 
            // btn_Update
            // 
            this.btn_Update.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Update.ForeColor = System.Drawing.Color.Blue;
            this.btn_Update.Location = new System.Drawing.Point(279, 260);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(143, 44);
            this.btn_Update.TabIndex = 7;
            this.btn_Update.Text = "儲存更新";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // txb_Name
            // 
            this.txb_Name.Location = new System.Drawing.Point(79, 117);
            this.txb_Name.MaxLength = 10;
            this.txb_Name.Name = "txb_Name";
            this.txb_Name.Size = new System.Drawing.Size(200, 33);
            this.txb_Name.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "年齡：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "生日：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "姓名：";
            // 
            // lab_ingMsg
            // 
            this.lab_ingMsg.AutoSize = true;
            this.lab_ingMsg.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_ingMsg.ForeColor = System.Drawing.Color.Red;
            this.lab_ingMsg.Location = new System.Drawing.Point(139, 46);
            this.lab_ingMsg.Name = "lab_ingMsg";
            this.lab_ingMsg.Size = new System.Drawing.Size(0, 20);
            this.lab_ingMsg.TabIndex = 4;
            // 
            // UpdatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 407);
            this.Controls.Add(this.lab_ingMsg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_InqData);
            this.Controls.Add(this.txb_Id);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "個人基本資料補正";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdatesForm_FormClosing);
            this.Load += new System.EventHandler(this.UpdatesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Id;
        private System.Windows.Forms.Button btn_InqData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtp_Birth;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox txb_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_Age;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lab_date;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_ingMsg;
    }
}