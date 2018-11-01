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
        public UpdatesForm()
        {
            InitializeComponent();
        }

        private void UpdatesForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txb_Id.Text.Length == 10)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                DataUtility du = new DataUtility();
                DataTable dt = new DataTable();
                d.Add("@id", txb_Id.Text.Trim());
                try
                {
                    dt = du.getDataTableBysp("Ex108_GetUpdatePlayer", d);
                    if (dt.Rows.Count > 0)
                    {
                        groupBox1.Visible = true;
                        lab_date.Text = Convert.ToDateTime(dt.Rows[0]["date"].ToString()).ToShortDateString();
                        txb_Name.Text = dt.Rows[0]["name"].ToString();
                        dtp_Birth.Value = Convert.ToDateTime(dt.Rows[0]["birth"].ToString());
                        lab_Age.Text = dt.Rows[0]["age"].ToString();
                    }
                    else
                    {
                        groupBox1.Visible = false;
                    }
                }
                catch(Exception ex)
                {
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
            string NewBirth = dtp_Birth.Value.ToShortDateString();
            MessageBox.Show(NewBirth);
        }

        private void dtp_Birth_ValueChanged(object sender, EventArgs e)
        {
            int ChangeAge = Lib.SysSetting.ConvertAge(dtp_Birth.Value, Convert.ToDateTime(lab_date.Text));
            lab_Age.Text = ChangeAge.ToString();
        }
    }
}
