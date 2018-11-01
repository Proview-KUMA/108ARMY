using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib;
using PCComm;

namespace ScoreClose
{
    public partial class ScoreCloseForm2 : Form
    {
        private Lib.DataUtility du = new DataUtility();
        private Dictionary<string, object> d = new Dictionary<string, object>();
        public DataTable dt = null;
        delegate void Callback(string text, string objectname);

        //2018-9-5維保新增//晶片感應結算成績
        private CommunicationManager comm = new CommunicationManager();

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

        private void Form2_Load(object sender, EventArgs e)
        {
            date.MaxDate = System.DateTime.Today;
            date.Value = System.DateTime.Today;
            this.AcceptButton = button1;

            //2018-9-5維保
            /*******  init LF rfid in  ****/
            comm.BaudRate = "9600";
            comm.Parity = "None";
            comm.StopBits = "One";
            comm.DataBits = "8";
            comm.PortName = "COM2";
            comm.DisplayWindow = rtb_LF_Tag;
            try
            {
                comm.OpenPort();//開啟晶片感應port
            }
            catch (Exception ex)
            {
                MessageBox.Show("晶片感應讀取器連接失敗"+Environment.NewLine+"錯誤訊息："+ex.Message);
            }
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
                        DateTime Change_time=new DateTime(2018,12,31,23,59,59);//檢查時間 2018年12月31日

                        if (date.Value > Change_time)//如果時間大於設定時間
                        {
                            //新版-2019年1月1日開始啟用之成績結算sp
                            dt = du.getDataTableBysp(@"Ex108_CalResultByID", d);
                        }
                        else
                        {
                            //舊版2018年之前用
                            dt = du.getDataTableBysp(@"Ex106_CalResultByID", d);
                        }
                        
                        
                       
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
                        DateTime Change_time = new DateTime(2018, 12, 31, 23, 59, 59);//檢查時間 2018年12月31日

                        if (date.Value > Change_time)//如果時間大於設定時間
                        {
                            //新版-2019年1月1日開始啟用之成績結算sp
                            dt = du.getDataTableBysp(@"Ex108_CalResultByCloNum", d);
                        }
                        else
                        {
                            //舊版2017年之前用
                            dt = du.getDataTableBysp(@"Ex106_CalResultByCloNum", d);
                        }
                        
                        
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

        //2018-9-5維保//感應晶片後事件處理
        private void rtb_LF_Tag_TextChanged(object sender, EventArgs e)
        {
            if (rtb_LF_Tag.TextLength == 10)
            {
                try
                {
                    d.Clear();
                    d.Add("lf_tag", rtb_LF_Tag.Text.Trim());
                    d.Add("date", date.Value);
                    DateTime Change_time = new DateTime(2018, 12, 31, 23, 59, 59);//檢查時間 2018年12月31日
                    if (date.Value > Change_time)//如果時間大於設定時間
                    {
                        //新版-2019年1月1日開始啟用之成績結算sp
                        dt = du.getDataTableBysp(@"Ex108_CalResultByLFTag", d);
                    }
                    else
                    {
                        //舊版2017年之前用
                        dt = du.getDataTableBysp(@"Ex106_CalResultByLFTag", d);
                    }
                    


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
                            if (_Form1.ShowDialog() == DialogResult.Cancel)
                            {
                                rtb_LF_Tag.Text = "";
                            }
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
                finally
                {
                    rtb_LF_Tag.Text = "";
                }
                
            }
            else
            {
                //MessageBox.Show("晶片感應失敗!!");
            }
            
        }

        private  delegate void CommDelegate(CommunicationManager _comm,string status);
        private void DoCommStatus(CommunicationManager _comm, string status)
        {
            try
            {
                switch (status)
                {
                    case "open":
                        _comm.OpenPort();
                        break;
                    case "close":
                        _comm.ClosePort();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ScoreCloseForm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //2018-9-5維保，關閉視窗時將晶片感應的port關閉
            comm.ClosePort();
        }

        private void ScoreCloseForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //2018-9-5維保，關閉視窗時將晶片感應的port關閉
            comm.ClosePort();
        }

    }
}
