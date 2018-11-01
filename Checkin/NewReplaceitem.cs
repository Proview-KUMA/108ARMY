using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InI
{
    public partial class NewReplaceitem : Form
    {
        public string center_code = string.Empty;
        private Form1 m_parent;
        Dictionary<string, string> Dic_Item = new Dictionary<string, string>();
        private static string Memo = string.Empty;
        private static string[] RepName;
        public NewReplaceitem(Form1 mpform)
        {
            InitializeComponent();
            m_parent = mpform;
        }
        private void NewReplaceitem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;//開啟鍵盤觸發事件
            try
            {
                center_code = System.Configuration.ConfigurationManager.AppSettings["centercode"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取單位代碼失敗，請檢查config設定檔");
            }
            

            //讀取項目
            try
            {
                Dic_Item = new Dictionary<string, string>();
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("center_code", center_code);
                DataTable dt_isSwin = du.getDataTableByText("select IsSwin from Center where center_code = @center_code", d);
                DataTable dt = new DataTable();
                if (dt_isSwin.Rows.Count > 0)
                {
                    d.Clear();
                    //有游泳項目
                    if (Convert.ToBoolean(dt_isSwin.Rows[0]["IsSwin"]))
                    {
                        d.Add("Gender", m_parent.Gender);
                        dt = du.getDataTableBysp("GetRepMent", d);
                    }
                    else
                    {   //沒有游泳項目
                        d.Add("Gender", m_parent.Gender);
                        dt = du.getDataTableBysp("GetRepMentNonSwin", d);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    cbb_RepItem1.Items.Add("請選擇替代項目");
                    cbb_RepItem2.Items.Add("請選擇替代項目");
                    cbb_RepItem3.Items.Add("請選擇替代項目");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Dic_Item.Add(dt.Rows[i]["rep_title"].ToString(), dt.Rows[i]["sid"].ToString());
                        cbb_RepItem1.Items.Add(dt.Rows[i]["rep_title"].ToString());
                        cbb_RepItem2.Items.Add(dt.Rows[i]["rep_title"].ToString());
                        cbb_RepItem3.Items.Add(dt.Rows[i]["rep_title"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            cbb_RepItem1.SelectedIndex = 0;
            cbb_RepItem2.SelectedIndex = 0;
            cbb_RepItem3.SelectedIndex = 0;
        }
        private void cbb_RepItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbb_RepItem1.SelectedIndex == cbb_RepItem2.SelectedIndex || cbb_RepItem1.SelectedIndex == cbb_RepItem3.SelectedIndex) && cbb_RepItem1.SelectedIndex > 0)//檢查有沒有重複
            {
                MessageBox.Show("選項重複");
                cbb_RepItem1.SelectedIndex = 0;
                pictureBox1.Visible = false;
                
            }
            else
            {
                if (cbb_RepItem1.SelectedIndex > 0)
                    pictureBox1.Visible = true;
                else
                    pictureBox1.Visible = false;
            }
        }

        private void cbb_RepItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbb_RepItem2.SelectedIndex == cbb_RepItem1.SelectedIndex || cbb_RepItem2.SelectedIndex == cbb_RepItem3.SelectedIndex) && cbb_RepItem2.SelectedIndex > 0)//檢查有沒有重複
            {
                MessageBox.Show("選項重複");
                cbb_RepItem2.SelectedIndex = 0;
                pictureBox2.Visible = false;

            }
            else
            {
                if (cbb_RepItem2.SelectedIndex > 0)
                    pictureBox2.Visible = true;
                else
                    pictureBox2.Visible = false;
            }
        }

        private void cbb_RepItem3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbb_RepItem3.SelectedIndex == cbb_RepItem1.SelectedIndex || cbb_RepItem3.SelectedIndex == cbb_RepItem2.SelectedIndex) && cbb_RepItem3.SelectedIndex > 0)//檢查有沒有重複
            {
                MessageBox.Show("選項重複");
                cbb_RepItem3.SelectedIndex = 0;
                pictureBox3.Visible = false;

            }
            else
            {
                if (cbb_RepItem3.SelectedIndex > 0)
                    pictureBox3.Visible = true;
                else
                    pictureBox3.Visible = false;
            }
        }

        private void btn_ChangeOk_Click(object sender, EventArgs e)
        {
            Memo = string.Empty;
            RepName = new string[3];
            if (cbb_RepItem1.SelectedIndex == 0)
            {
                Memo += "0";
                RepName[0] = "仰臥起坐";
            }         
            else
            {
                Memo += Dic_Item[cbb_RepItem1.SelectedItem.ToString()];
                RepName[0] = cbb_RepItem1.SelectedItem.ToString();
            }         

            if (cbb_RepItem2.SelectedIndex == 0)
            {
                Memo += "0";
                RepName[1] = "俯地挺身";
            } 
            else
            {
                Memo += Dic_Item[cbb_RepItem2.SelectedItem.ToString()];
                RepName[1] = cbb_RepItem2.SelectedItem.ToString();
            }
                
            if (cbb_RepItem3.SelectedIndex == 0)
            {
                Memo += "0";
                RepName[2] = "三千公尺跑步";
            }           
            else
            {
                Memo += Dic_Item[cbb_RepItem3.SelectedItem.ToString()];
                RepName[2] = cbb_RepItem3.SelectedItem.ToString();
            }

            m_parent.GetRepItem(Memo, RepName);
            this.Dispose();

            //MessageBox.Show(Memo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void NewReplaceitem_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = Convert.ToInt16(e.KeyChar);
            switch (key)
            {
                case 13://Enter
                    btn_ChangeOk_Click(btn_ChangeOk, e);
                    break;
                case 27://Ese
                    button1_Click(button1, e);
                    break;
                default://Enter=13，Ese=27
                    break;
            }
        }
        
    }
}
