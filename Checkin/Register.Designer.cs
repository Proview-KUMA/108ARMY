namespace InI
{
    partial class Register
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
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this._unit_title = new System.Windows.Forms.Label();
            this._unit_code = new System.Windows.Forms.TextBox();
            this._birth = new System.Windows.Forms.TextBox();
            this._rank = new System.Windows.Forms.ComboBox();
            this._name = new System.Windows.Forms.TextBox();
            this.checkid = new System.Windows.Forms.Label();
            this._gender = new System.Windows.Forms.Label();
            this._id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_UseAWSService = new System.Windows.Forms.CheckBox();
            this.LB_ReserveMsg = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this._unit_title);
            this.groupBox1.Controls.Add(this._unit_code);
            this.groupBox1.Controls.Add(this._birth);
            this.groupBox1.Controls.Add(this._rank);
            this.groupBox1.Controls.Add(this._name);
            this.groupBox1.Controls.Add(this.checkid);
            this.groupBox1.Controls.Add(this._gender);
            this.groupBox1.Controls.Add(this._id);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 416);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "受測人員資料";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(331, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 27);
            this.label7.TabIndex = 17;
            this.label7.Text = "例: 72/2/30";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(317, 364);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 37);
            this.button2.TabIndex = 16;
            this.button2.Text = "清除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 37);
            this.button1.TabIndex = 15;
            this.button1.Text = "確認送出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _unit_title
            // 
            this._unit_title.AutoSize = true;
            this._unit_title.Location = new System.Drawing.Point(143, 321);
            this._unit_title.Name = "_unit_title";
            this._unit_title.Size = new System.Drawing.Size(96, 27);
            this._unit_title.TabIndex = 14;
            this._unit_title.Text = "單位全銜";
            // 
            // _unit_code
            // 
            this._unit_code.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this._unit_code.Location = new System.Drawing.Point(144, 271);
            this._unit_code.MaxLength = 5;
            this._unit_code.Name = "_unit_code";
            this._unit_code.Size = new System.Drawing.Size(172, 35);
            this._unit_code.TabIndex = 13;
            this._unit_code.TextChanged += new System.EventHandler(this._unit_code_TextChanged);
            // 
            // _birth
            // 
            this._birth.Location = new System.Drawing.Point(144, 219);
            this._birth.MaxLength = 9;
            this._birth.Name = "_birth";
            this._birth.Size = new System.Drawing.Size(172, 35);
            this._birth.TabIndex = 12;
            this._birth.Leave += new System.EventHandler(this._birth_Leave);
            // 
            // _rank
            // 
            this._rank.FormattingEnabled = true;
            this._rank.Location = new System.Drawing.Point(144, 168);
            this._rank.Name = "_rank";
            this._rank.Size = new System.Drawing.Size(172, 35);
            this._rank.TabIndex = 11;
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(144, 117);
            this._name.MaxLength = 10;
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(172, 35);
            this._name.TabIndex = 10;
            // 
            // checkid
            // 
            this.checkid.AutoSize = true;
            this.checkid.ForeColor = System.Drawing.Color.Red;
            this.checkid.Location = new System.Drawing.Point(331, 31);
            this.checkid.Name = "checkid";
            this.checkid.Size = new System.Drawing.Size(117, 27);
            this.checkid.TabIndex = 8;
            this.checkid.Text = "檢查身份證";
            // 
            // _gender
            // 
            this._gender.AutoSize = true;
            this._gender.Location = new System.Drawing.Point(139, 74);
            this._gender.Name = "_gender";
            this._gender.Size = new System.Drawing.Size(63, 27);
            this._gender.TabIndex = 7;
            this._gender.Text = "男/女";
            // 
            // _id
            // 
            this._id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this._id.Location = new System.Drawing.Point(144, 26);
            this._id.MaxLength = 10;
            this._id.Name = "_id";
            this._id.Size = new System.Drawing.Size(172, 35);
            this._id.TabIndex = 6;
            this._id.TextChanged += new System.EventHandler(this._id_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "單位:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "生日(民國):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "級職:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "姓名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "性別:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "身份證字號:";
            // 
            // CB_UseAWSService
            // 
            this.CB_UseAWSService.AutoSize = true;
            this.CB_UseAWSService.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CB_UseAWSService.Location = new System.Drawing.Point(12, 12);
            this.CB_UseAWSService.Name = "CB_UseAWSService";
            this.CB_UseAWSService.Size = new System.Drawing.Size(367, 31);
            this.CB_UseAWSService.TabIndex = 1;
            this.CB_UseAWSService.Text = "從基本體能鑑測網取得個人基本資料";
            this.CB_UseAWSService.UseVisualStyleBackColor = true;
            this.CB_UseAWSService.CheckedChanged += new System.EventHandler(this.CB_UseAWSService_CheckedChanged);
            // 
            // LB_ReserveMsg
            // 
            this.LB_ReserveMsg.AutoSize = true;
            this.LB_ReserveMsg.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LB_ReserveMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LB_ReserveMsg.Location = new System.Drawing.Point(28, 481);
            this.LB_ReserveMsg.Name = "LB_ReserveMsg";
            this.LB_ReserveMsg.Size = new System.Drawing.Size(54, 27);
            this.LB_ReserveMsg.TabIndex = 18;
            this.LB_ReserveMsg.Text = "訊息";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 517);
            this.Controls.Add(this.LB_ReserveMsg);
            this.Controls.Add(this.CB_UseAWSService);
            this.Controls.Add(this.groupBox1);
            this.Name = "Register";
            this.Text = "現場報名";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label checkid;
        private System.Windows.Forms.Label _gender;
        private System.Windows.Forms.TextBox _id;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _name;
        private System.Windows.Forms.Label _unit_title;
        private System.Windows.Forms.TextBox _unit_code;
        private System.Windows.Forms.TextBox _birth;
        private System.Windows.Forms.ComboBox _rank;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CB_UseAWSService;
        private System.Windows.Forms.Label LB_ReserveMsg;
    }
}