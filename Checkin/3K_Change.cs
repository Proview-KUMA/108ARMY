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
    public partial class _3K_Change : Form
    {
        private Form1 m_parent;
        public string center_code = "1";
        public string center_name = "陸軍專校鑑測站";
        public int Change_Item = 1;
        public int Age { get; set; }

        public _3K_Change(Form1 mpform)
        {
            InitializeComponent();
            m_parent = mpform;

            try
            {
                //取得鑑測站名稱
                Lib.DataUtility du_center = new Lib.DataUtility();
                DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code ");
                if (dt_center.Rows.Count == 1)
                {
                    center_code = dt_center.Rows[0]["center_code"].ToString();
                    center_name = dt_center.Rows[0]["center_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("連接資料庫異常，無法取得鑑測中心代碼及名稱。" + Environment.NewLine + ex.Message);
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
                    //判斷鑑測中心有無泳池
                    if (Convert.ToBoolean(dt_isSwin.Rows[0]["IsSwin"]))
                    {
                        //有游泳項目
                        rbt_800_Swim.Visible = true;
                    }
                    else
                    {
                        //沒有游泳項目           
                        rbt_800_Swim.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("連接資料庫異常，無法取得鑑測中心是否開放泳池項目。" + Environment.NewLine + ex.Message);
            }
        }

        private void rbt_3K_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_3K.Checked == true)
            {
                rbt_3K.BackColor = Color.Yellow;
                rbt_5K.BackColor = Color.White;
                rbt_Jump.BackColor = Color.White;
                rbt_800_Swim.BackColor = Color.White;
                Change_Item = 1;
            }
            else
            {
                rbt_3K.BackColor = Color.White;
            }

        }

        private void rbt_5K_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_5K.Checked == true)
            {
                rbt_3K.BackColor = Color.White;
                rbt_5K.BackColor = Color.PaleGreen;
                rbt_Jump.BackColor = Color.White;
                rbt_800_Swim.BackColor = Color.White;
                Change_Item = 2;
            }
            else
            {
                rbt_5K.BackColor = Color.White;
            }
        }

        private void rbt_Jump_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_Jump.Checked == true)
            {
                rbt_3K.BackColor = Color.White;
                rbt_5K.BackColor = Color.White;
                rbt_Jump.BackColor = Color.Pink;
                rbt_800_Swim.BackColor = Color.White;
                Change_Item = 3;
            }
            else
            {
                rbt_Jump.BackColor = Color.White;
            }
        }

        private void rbt_800_Swim_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_800_Swim.Checked == true)
            {
                rbt_3K.BackColor = Color.White;
                rbt_5K.BackColor = Color.White;
                rbt_Jump.BackColor = Color.White;
                rbt_800_Swim.BackColor = Color.Aqua;
                Change_Item = 4;
            }
            else
            {
                rbt_800_Swim.BackColor = Color.White;
            }
        }

        private void _3K_Change_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char key = e.KeyChar;
            //MessageBox.Show(Convert.ToString(key));
            int key = Convert.ToInt16(e.KeyChar);
            switch (key)
            {
                case 49://數字鍵1//3K=0
                    rbt_3K.Checked = true;
                    btn_Enter_Click(btn_Enter, e);
                    break;
                case 50://數字鍵2//5K=G
                    rbt_5K.Checked = true;
                    btn_Enter_Click(btn_Enter, e);
                    break;
                case 51://數字鍵3//跳繩=H
                    rbt_Jump.Checked = true;
                    btn_Enter_Click(btn_Enter, e);
                    break;
                case 52://數字鍵4//800游走=F
                    if (rbt_800_Swim.Visible == true)
                    {
                        rbt_800_Swim.Checked = true;
                        btn_Enter_Click(btn_Enter, e);
                    }
                    
                    break;
                case 13://Enter
                    btn_Enter_Click(btn_Enter, e);
                    break;
                case 27://Ese
                    btn_Ese_Click(btn_Ese, e);
                    break;
                default://Enter=13，Ese=27
                    break;
            }
            
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            m_parent.get_Change_3K_Item(Change_Item);
            this.Dispose();
        }

        private void btn_Ese_Click(object sender, EventArgs e)
        {
            m_parent.get_Change_3K_Item(0);
            this.Dispose();
        }

        private void _3K_Change_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;//開啟鍵盤觸發事件
            if (Age >= 45)
            {
                lab_age45up.Text = "該員年齡[" + Age.ToString() + "歲]，已符合多元選項條件。";
                lab_agealert.Text = "(年齡45歲(含)以上，可選取多元選項)";
            }
            else
            {
                lab_age45up.Text = "[多元選項]若未符合年齡條件，需出示證明。";
                lab_agealert.Text = "(年齡45歲(含)以上，可選取多元選項)";
            }
        }
    }
}
