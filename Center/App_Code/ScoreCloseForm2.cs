using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib;

namespace ScoreClose
{
    public partial class ScoreCloseForm2 : Form
    {
        private Lib.DataUtility du = new DataUtility();
        private Dictionary<string, object> d = new Dictionary<string, object>();
        public DataTable dt = null;
        delegate void Callback(string text, string objectname);

        public ScoreCloseForm2()
        {
            InitializeComponent();
        }

        private void UpdateUIStatus(string content, string objectname)
        {
            switch (objectname)
            {
                case "button1":
                    switch (content)
                    {
                        case "false":
                            button1.Enabled = false;
                            break;
                        case "true":
                            button1.Enabled = true;
                            break;
                        default:
                            button1.Text = content;
                            break;
                    }
                    break;
                case "TB_clothesNum":
                    switch (content)
                    {
                        case "false":
                            TB_clothesNum.Enabled = false;
                            break;
                        case "true":
                            TB_clothesNum.Enabled = true;
                            break;
                        case "clear":
                            TB_clothesNum.Text = string.Empty;
                            break;
                        default:
                            TB_clothesNum.Text = content;
                            break;
                    }
                    break;
                case "TB_id":
                    switch (content)
                    {
                        case "false":
                            TB_id.Enabled = false;
                            break;
                        case "true":
                            TB_id.Enabled = true;
                            break;
                        case "clear":
                            TB_id.Text = string.Empty;
                            break;
                        default:
                            TB_id.Text = content;
                            break;
                    }
                    break;
                default:
                    break;
            }
        
        }
       
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            date.MaxDate = System.DateTime.Today;
            date.Value = System.DateTime.Today;

            this.AcceptButton = button1;
        }

        private void ClearText()
        {
            this.Invoke(new Callback(UpdateUIStatus), new object[] { "clear", "TB_clothesNum" });
            this.Invoke(new Callback(UpdateUIStatus), new object[] { "clear", "TB_id" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RB_id.Checked == true)
            {
                if (!String.IsNullOrEmpty(TB_id.Text.Trim()))
                {
                    try
                    {
                        this.Invoke(new Callback(UpdateUIStatus), new object[] { "false", "button1" });
                        d.Clear();
                        d.Add("id", TB_id.Text.Trim());
                        d.Add("date", date.Value);
                        dt = du.getDataTableBysp(@"Ex104_CalResultByID", d);
                        if (dt.Rows.Count == 1)
                        {
                            if (dt.Columns.Contains("error"))
                            {
                                MessageBox.Show("查無此受測人員成績");
                            }
                            else
                            {
                                ScoreCloseForm1 _Form1 = new ScoreCloseForm1(dt);
                                _Form1.TopMost = true;
                                _Form1.Activate();
                                _Form1.WindowState = FormWindowState.Normal;
                                _Form1.ShowDialog();

                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("查無此受測人員成績");
                        }
                        else
                        {

                            MessageBox.Show("依條件查詢 , 取得成績為" + dt.Rows.Count.ToString() + "筆, 此為異常情況請洽鑑測官");
                        }           
                        ClearText();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.Invoke(new Callback(UpdateUIStatus), new object[] { "true", "button1" });
                    TB_id.Focus();
                }
                else
                {
                    MessageBox.Show("身分證字號錯誤!! 請輸入身份證字號");
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(TB_clothesNum.Text.Trim()))
                {
                    try
                    {
                        this.Invoke(new Callback(UpdateUIStatus), new object[] { "false", "button1" });
                        d.Clear();
                        d.Add("cloNum", TB_clothesNum.Text.Trim());
                        d.Add("date", date.Value);
                        dt = du.getDataTableBysp(@"Ex104_CalResultByCloNum", d);
                        if (dt.Rows.Count == 1)
                        {
                            if (dt.Columns.Contains("error"))
                            {
                                MessageBox.Show("查無此受測人員成績");
                            }
                            else
                            {
                                ScoreCloseForm1 _Form1 = new ScoreCloseForm1(dt);
                                _Form1.TopMost = true;
                                _Form1.Activate();
                                _Form1.WindowState = FormWindowState.Normal;
                                _Form1.ShowDialog();
                            }
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("查無此受測人員成績");
                        }
                        else
                        {

                            MessageBox.Show("依條件查詢 , 取得成績為" + dt.Rows.Count.ToString() + "筆, 此為異常情況請洽鑑測官");
                        }
                        ClearText();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    this.Invoke(new Callback(UpdateUIStatus), new object[] { "true", "button1" });
                    TB_clothesNum.Focus();
                }
                else
                {
                    MessageBox.Show("背號錯誤!! 請輸入背號");
                }

            }

        }

        private void TB_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void RB_id_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
