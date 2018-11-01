using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib;

namespace InI
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Query3KRunList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Query3KRunList();
        }

        private void Query3KRunList()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            Lib.DataUtility du = new DataUtility();
            DataTable dt = new DataTable();
            try
            {
                d.Add("status", "001");
                d.Add("op_id", "Fail");
                dt = du.getDataTableBysp("Race_QueryTrans3KRunFail", d);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "身分證";
                dataGridView1.Columns[1].HeaderText = "姓名";
                dataGridView1.Columns[2].HeaderText = "單位";
                dataGridView1.Columns[3].HeaderText = "組別";
                dataGridView1.Columns[4].HeaderText = "日期";
                label1.Text = "資料筆數 : " + dt.Rows.Count.ToString() + " 筆";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CenterWS.WebService CenterWS = new InI.CenterWS.WebService();
                Lib.DataUtility du = new DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("op_id", "Fail");
                d.Add("status", "001");  //只抓必須要測3000公尺的資料
                DataTable dt = du.getDataTableBysp("Race_Get3KRunTransFail", d);
                if (dt.Rows.Count > 0)
                {
                    dt.TableName = "upload";
                    string msg = CenterWS.AddResultFor3KRun(dt);
                    if ("Done" == msg)
                    {
                        MessageBox.Show("3000公尺資料補傳成功");
                        d.Clear();
                        d.Add("op_id", "Fail");
                        d.Add("status", "001");  //只抓必須要測3000公尺的資料
                        du.executeNonQueryBysp("Race_Update3KRunTransFail", d);
                        Query3KRunList();
                    }
                    else
                    {   //傳輸檢錄資料至遠端電腦失敗, 以op_id做為flag
                        MessageBox.Show(msg);
                        Query3KRunList();
                    }
                }
                else
                {
                    MessageBox.Show("查無可補傳資料");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
