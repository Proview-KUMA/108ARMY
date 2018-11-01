using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
///using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InI
{
    public partial class Form2 : Form
    {
        public string Title = string.Empty;
        public string Status = string.Empty;
        delegate void Callback();
        String hintMessage = string.Empty;

        public Form2()
        {
            InitializeComponent();
        }

        private void idinput_Click(object sender, EventArgs e)
        {
            if (step1_id.Text.Length == 10)
            {
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", step1_id.Text.Trim());
                DataTable dt = du.getDataTableBysp("Race_GetPlayer", d);
                if (dt.Rows.Count > 0)
                {
                    _id.Text = dt.Rows[0]["id"].ToString().Trim();
                    _birth.Text = Lib.SysSetting.ToRocDateFormat(Convert.ToDateTime(dt.Rows[0]["birth"].ToString().Trim()).ToShortDateString());
                    DateTime _birth_dt = Lib.SysSetting.ToWorldDate(_birth.Text.Trim());
                    int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["RaceTime"].ToString()));
                    _age.Text = age_temp.ToString().Trim();
                    _name.Text = dt.Rows[0]["name"].ToString().Trim();
                    _unit.Text = dt.Rows[0]["unit_title"].ToString().Trim();
                    _rank.Text = dt.Rows[0]["rank_title"].ToString().Trim();
   
                    if (dt.Rows[0]["gender"].ToString().Trim() == "M")
                    {
                        _gender.Text = "男性";
                    }
                    else if (dt.Rows[0]["gender"].ToString().Trim() == "F")
                    {
                        _gender.Text = "女性";
                    }
                }
                else
                {
                    step1_id.Text = string.Empty;
                    MessageBox.Show("查無報進資訊");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                Title = "持免技測證明";
                Status = "133";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                Title = "懷孕";
                Status = "104";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                Title = "公勤(受訓,外調,婚喪假,因公住院,出國)";
                Status = "114";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton5.Checked = false;
                Title = "屆退";
                Status = "124";
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                Title = "無故未到";
                Status = "103";
            }
        }

        private void updateHintMessage(String msg)
        {
            this.hintMessage = msg;
            this.Invoke(new Callback(updateHintLbl));
        }

        private void updateHintLbl()
        {
            this.Message.Text = this.hintMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true || radioButton4.Checked == true || radioButton5.Checked == true)
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("判定人員 : " + _name.Text + " => 狀態: " + Title + " ?", "確認狀態視窗", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //確認狀態
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        Lib.DataUtility du = new Lib.DataUtility();

                        d.Add("id", _id.Text.Trim());
                        d.Add("status", Status);
                        d.Add("date", System.DateTime.Today);
                        d.Add("name", _name.Text.Trim());
                        du.executeNonQueryBysp("Race_CheckinStatusSet", d);
                        InitForm();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        //MessageBox.Show("n");
                    }
                }
                catch (Exception ex)
                {
                    this.updateHintMessage(ex.Message);
                }
            }
            else
            {
                this.updateHintMessage("請點選要判定的狀態");
            }
        }

        void InitForm()
        {
            step1_id.Text = string.Empty;
            _id.Text = string.Empty;
            _name.Text = string.Empty;
            _rank.Text = string.Empty;
            _unit.Text = string.Empty;
            _gender.Text = string.Empty;
            _birth.Text = string.Empty;
            _age.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
        }

        private void step1_id_TextChanged(object sender, EventArgs e)
        {
            if (step1_id.TextLength == 10)
            {
                idinput_Click(idinput, e);
            }
        }


    }
}
