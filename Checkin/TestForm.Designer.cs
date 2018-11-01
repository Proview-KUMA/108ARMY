namespace InI
{
    partial class TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.RB_Standard = new System.Windows.Forms.RadioButton();
            this.RB_Replaceitem = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select Score File Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_FileName
            // 
            this.TB_FileName.Location = new System.Drawing.Point(188, 14);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(461, 22);
            this.TB_FileName.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Load Score To DataBase";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RB_Standard
            // 
            this.RB_Standard.AutoSize = true;
            this.RB_Standard.Location = new System.Drawing.Point(12, 56);
            this.RB_Standard.Name = "RB_Standard";
            this.RB_Standard.Size = new System.Drawing.Size(71, 16);
            this.RB_Standard.TabIndex = 3;
            this.RB_Standard.TabStop = true;
            this.RB_Standard.Text = "標準三項";
            this.RB_Standard.UseVisualStyleBackColor = true;
            this.RB_Standard.CheckedChanged += new System.EventHandler(this.RB_Standard_CheckedChanged);
            // 
            // RB_Replaceitem
            // 
            this.RB_Replaceitem.AutoSize = true;
            this.RB_Replaceitem.Location = new System.Drawing.Point(89, 56);
            this.RB_Replaceitem.Name = "RB_Replaceitem";
            this.RB_Replaceitem.Size = new System.Drawing.Size(71, 16);
            this.RB_Replaceitem.TabIndex = 4;
            this.RB_Replaceitem.TabStop = true;
            this.RB_Replaceitem.Text = "替代項目";
            this.RB_Replaceitem.UseVisualStyleBackColor = true;
            this.RB_Replaceitem.CheckedChanged += new System.EventHandler(this.RB_Replaceitem_CheckedChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 132);
            this.Controls.Add(this.RB_Replaceitem);
            this.Controls.Add(this.RB_Standard);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TB_FileName);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton RB_Standard;
        private System.Windows.Forms.RadioButton RB_Replaceitem;
    }
}