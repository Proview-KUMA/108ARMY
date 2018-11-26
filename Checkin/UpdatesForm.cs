using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib;

namespace InI
{
    public partial class UpdatesForm : Form
    {
        private static string Id = string.Empty;
        private static DateTime Test_date;
        private static string Status = string.Empty;
        private static string Status_Name = string.Empty;
        private static string Old_Name = string.Empty;
        private static DateTime Old_Birth;
        private static string Old_Age = string.Empty;
        private static string New_Name = string.Empty;
        private static DateTime New_Birth;
        private static string New_Age = string.Empty;
        public UpdatesForm()
        {
            InitializeComponent();
        }

        private void UpdatesForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_InqData_Click(object sender, EventArgs e)
        {
            lab_ingMsg.Text = null;
            Id = string.Empty;
            Test_date = new DateTime();
            Status = string.Empty;
            Status_Name = string.Empty;
            Old_Name = string.Empty;
            Old_Birth = new DateTime();
            Old_Age = string.Empty;
            groupBox1.Visible = false;
            if (txb_Id.Text.Length == 10)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                DataUtility du = new DataUtility();
                DataTable dt = new DataTable();
                Id = txb_Id.Text.Trim();
                d.Add("@id", Id);
                try
                {
                    dt = du.getDataTableBysp("Ex108_GetUpdatePlayer", d);
                    if (dt.Rows.Count > 0)
                    {
                        groupBox1.Visible = true;
                        Test_date = Convert.ToDateTime(dt.Rows[0]["date"].ToString());
                        Status = dt.Rows[0]["status"].ToString();
                        Status_Name = dt.Rows[0]["status_name"].ToString();
                        Old_Name = dt.Rows[0]["name"].ToString();
                        Old_Birth = Convert.ToDateTime(dt.Rows[0]["birth"].ToString());
                        Old_Age = dt.Rows[0]["age"].ToString();
                        lab_date.Text = Test_date.ToShortDateString();
                        lab_status.Text = Status_Name;
                        txb_Name.Text = Old_Name;
                        dtp_Birth.Value = Old_Birth;
                        lab_Age.Text = Old_Age;
                    }
                    else
                    {
                        lab_ingMsg.Text = "查無資料";
                    }
                }
                catch (Exception ex)
                {
                    lab_ingMsg.Text = "查詢失敗";
                    groupBox1.Visible = false;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("身份證長度不足10碼");
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            New_Name = txb_Name.Text.Trim();
            New_Birth = dtp_Birth.Value;
            New_Age = lab_Age.Text.Trim();
            bool isChangeAge = false;
            string AlertMsg = string.Empty;
            if (New_Name != Old_Name || New_Birth.ToShortDateString() != Old_Birth.ToShortDateString() || New_Age != Old_Age)
            {
                AlertMsg += "請確認異動資料如下：" + Environment.NewLine;
                if (New_Name != Old_Name)
                    AlertMsg += "姓名：[" + Old_Name + "] --> [" + New_Name + "]" + Environment.NewLine;
                if (New_Birth.ToShortDateString() != Old_Birth.ToShortDateString())
                    AlertMsg += "生日：[" + Old_Birth.ToShortDateString() + "] --> [" + New_Birth.ToShortDateString() + "]" + Environment.NewLine;
                if (New_Age != Old_Age)
                {
                    isChangeAge = true;
                    AlertMsg += "年齡：[" + Old_Age + "] --> [" + New_Age + "]" + Environment.NewLine + Environment.NewLine + "(若已結算成績且年齡有異動者，系統將自動重新計算成績)" + Environment.NewLine;
                }
                    
                DialogResult dialogResult = MessageBox.Show(AlertMsg, "確認更新資料", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        DataUtility du = new DataUtility();
                        d.Add("id", Id);
                        d.Add("date", Test_date.ToShortDateString());
                        d.Add("name", New_Name);
                        d.Add("birth", New_Birth.ToShortDateString());
                        d.Add("age", New_Age);
                        du.executeNonQueryBysp("Ex108_UpdatePlayerData", d);

                        if (isChangeAge==true && Status.Substring(0, 1) == "1")
                        {
                            if(Status=="102" || Status == "103" || Status == "105")
                            {
                                Dictionary<string, object> di = new Dictionary<string, object>();
                                DataTable dt = new DataTable();
                                di.Add("id", Id);
                                di.Add("date", Test_date);
                                DateTime Change_time = new DateTime(2018, 12, 31, 23, 59, 59);//檢查時間 2018年12月31日

                                if (Test_date > Change_time)//如果時間大於設定時間
                                {
                                    //新版-2019年1月1日開始啟用之成績結算sp
                                    dt = du.getDataTableBysp(@"Ex108_CalResultByID", di);
                                }
                                else
                                {
                                    //舊版2018年之前用
                                    dt = du.getDataTableBysp(@"Ex106_CalResultByID", di);
                                }
                            }
                        }
                        btn_InqData_Click(btn_InqData, e);//再執行一次查詢
                        MessageBox.Show("資料更新成功!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("資料更新失敗，錯誤訊息如下：" + Environment.NewLine + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("資料未異動");
            }
        }

        private void dtp_Birth_ValueChanged(object sender, EventArgs e)
        {
            int ChangeAge = Lib.SysSetting.ConvertAge(dtp_Birth.Value, Convert.ToDateTime(lab_date.Text));
            lab_Age.Text = ChangeAge.ToString();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void UpdatesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
        }


    }
}
