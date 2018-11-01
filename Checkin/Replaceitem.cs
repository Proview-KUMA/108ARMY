using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using InI;


namespace InI
{
    public partial class Replaceitem : Form
    {
        private Form1 m_parent;
        public string center_code = "1";
        public string center_name = "陸軍專校鑑測站";

        public Replaceitem(Form1 mpform)
        {
            InitializeComponent();
            m_parent = mpform;
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            checkedListBox1.Items.Add("二分鐘仰臥起坐", false);
            checkedListBox1.Items.Add("二分鐘俯地挺身", false);
            checkedListBox1.Items.Add("三千公尺徒手跑步", false);

            try
            {
                center_code = System.Configuration.ConfigurationManager.AppSettings["centercode"].ToString();

                //Lib.DataUtility du_center = new Lib.DataUtility();
                //DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code ");
                //if (dt_center.Rows.Count == 1)
                //{
                //    center_code = dt_center.Rows[0]["center_code"].ToString();
                //    center_name = dt_center.Rows[0]["center_name"].ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
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
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        checkedListBox2.Items.Add(dt.Rows[i]["rep_title"].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == checkedListBox2.CheckedItems.Count)
            {
                m_parent.getreplactitem(checkedListBox1, checkedListBox2);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("「基本項目」與「替代項目」選取數量錯誤!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //checkedListBox1.CheckedItems.Count
            checkedListBox1.ClearSelected();
            checkedListBox2.ClearSelected();
            for(int i=0;i<checkedListBox1.Items.Count;i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }

        }

        private void Replaceitem_Load(object sender, EventArgs e)
        {

        }
        
    }
}
