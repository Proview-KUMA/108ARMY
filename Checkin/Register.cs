using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lib;
using System.Data.SqlClient;   

namespace InI
{
    public partial class Register : Form
    {
        delegate void Callback();
        public bool isUnit = false;
        public bool isBirth = false;
        private Form1 m_parent;
        public string center_code = "1";
        public string center_name = "陸軍專校鑑測站";
        public AWS_WS.WebService2SoapClient AWS = null;
        public Register(Form1 frm)
        {
            InitializeComponent();

            try
            {
                Lib.DataUtility du_center = new DataUtility();
                DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code");
                if (dt_center.Rows.Count == 1)
                {
                    center_code = dt_center.Rows[0]["center_code"].ToString();
                    center_name = dt_center.Rows[0]["center_name"].ToString();
                }
                //SetIsAcceptReserver(false);

                AWS = new AWS_WS.WebService2SoapClient();
            }
            catch (Exception ex)
            {

            }

            m_parent = frm;
            initrank(); 
        }

        private void initrank()
        {
            try
            {
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                DataTable dt = du.getDataTableByText("select rank_title from rank order by rank_code desc", d);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    { 
                        _rank.Items.Add(dt.Rows[i]["rank_title"].ToString().Trim());   
                    }
                }
            }
            catch(Exception ex)
            {
            
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isBirth == true && isUnit == true && checkid.Text == "" && _id.Text.Trim().Length == 10)
            {
                //ReSetControl();
                string gender_sql = "M";
                if (_gender.Text == "男性")
                {
                    gender_sql = "M";
                }
                else
                {
                    gender_sql = "F";
                }
                Lib.DataUtility du = new Lib.DataUtility();            
                DateTime Start = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
                DateTime End = Start.AddMonths(1).AddDays(-1);
                SqlParameter output = new SqlParameter("message", SqlDbType.NVarChar, 50);
                output.Direction = ParameterDirection.Output;
                SqlParameter p1 = new SqlParameter("id", _id.Text.Trim());
                SqlParameter p2 = new SqlParameter("reserveDate", System.DateTime.Today);
                SqlParameter p3 = new SqlParameter("start", Start);
                SqlParameter p4 = new SqlParameter("end", End);
                SqlParameter p5 = new SqlParameter("checkover", System.DateTime.Today.AddDays(-30));
                SqlParameter p6 = new SqlParameter("firstday", new DateTime(System.DateTime.Today.Year, 1, 1));
                SqlParameter p7 = new SqlParameter("lastday", new DateTime(System.DateTime.Today.Year, 12, 31));
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(output);
                list.Add(p1);
                list.Add(p2);
                list.Add(p3);
                list.Add(p4);
                list.Add(p5);
                list.Add(p6);
                list.Add(p7);
                SqlParameter[] sqls = list.ToArray();
                du.executeNonQueryBysp("Ex105_AddPlayerBySelf", sqls);
                Dictionary<string, object> d = new Dictionary<string, object>();
                switch (output.Value.ToString())
                { 
                    case "beok":
                        UpdateReserveMsg(@"該員" + _id.Text.Trim() + "今年度鑑測已經合格.", Color.Red);
                        break;
                    case "canreserve":
                        //新增報進資料
                        d.Clear();
                        d.Add("id", _id.Text.Trim());
                        d.Add("name", _name.Text.Trim());
                        d.Add("gender", gender_sql);
                        d.Add("unit_code", _unit_code.Text.Trim());
                        d.Add("rank_title", _rank.SelectedItem.ToString());
                        d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text));
                        d.Add("center_code", center_code);
                        d.Add("date", DateTime.Today);
                        d.Add("case", "canreserve");
                        try
                        {
                            StringBuilder SB = new StringBuilder();
                            SB.Append("身分證字號:" + " " + _id.Text.Trim() + "\r");
                            SB.Append("姓名:" + " " + _name.Text.Trim() + "\r");
                            SB.Append("生日:" + " " + _birth.Text.Trim() + "\r");
                            SB.Append("鑑測日期:" + " " + DateTime.Today.ToString("yyyy-MM-dd" + "\r"));
                            string message = SB.ToString();
                            string caption = "將新增一筆報進資料.";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult result;
                            result = MessageBox.Show(message, caption, buttons);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                du.executeNonQueryBysp(@"Ex105_InsertNowPlayer", d);
                                UpdateReserveMsg( _id.Text.Trim() + "報進成功", Color.Green);
                                m_parent.getregister(_id.Text.Trim(), _birth.Text.Trim(), _gender.Text, _rank.Text, _unit_title.Text, _name.Text, System.DateTime.Today);
                                this.Dispose();
                            }
                            else
                            {
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            UpdateReserveMsg("無法新增報進, 例外訊息: " + ex.Message, Color.Red);
                        }
                        break;
                    case "noreserve":
                        UpdateReserveMsg(@"該員" + _id.Text.Trim() + "這個月已有一筆鑑測為 不合格/免測.", Color.Red);
                        break;
                    case "bereserve":
                        UpdateReserveMsg(@"該員" + _id.Text.Trim() + "今日已存在一筆預約.", Color.Red);
                        break;
                    case "havetodaydata":
                        UpdateReserveMsg(@"該員" + _id.Text.Trim() + "今日已存在一筆鑑測資料", Color.Red);
                        break;
                    case "againreserve":
                        //新增報進資料, 帶入上一次的鑑測成績
                        d.Clear();
                        d.Add("id", _id.Text.Trim());
                        d.Add("name", _name.Text.Trim());
                        d.Add("gender", gender_sql);
                        d.Add("unit_code", _unit_code.Text.Trim());
                        d.Add("rank_title", _rank.SelectedItem.ToString());
                        d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text));
                        d.Add("center_code", center_code);
                        d.Add("date", DateTime.Today);
                        d.Add("case", "againreserve");
                        try
                        {
                            StringBuilder SB = new StringBuilder();
                            SB.Append("身分證字號:" + " " + _id.Text.Trim() + "\r");
                            SB.Append("姓名:" + " " + _name.Text.Trim() + "\r");
                            SB.Append("生日:" + " " + _birth.Text.Trim() + "\r");
                            SB.Append("鑑測日期:" + " " + DateTime.Today.ToString("yyyy-MM-dd" + "\r"));
                            string message = SB.ToString();
                            string caption = "將新增一筆30天內原地補測報進資料.";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            DialogResult result;
                            result = MessageBox.Show(message, caption, buttons);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                du.executeNonQueryBysp(@"Ex105_InsertNowPlayer", d);
                                UpdateReserveMsg(_id.Text.Trim() + "報進成功", Color.Green);
                                m_parent.getregister(_id.Text.Trim(), _birth.Text.Trim(), _gender.Text, _rank.Text, _unit_title.Text, _name.Text, System.DateTime.Today);
                                this.Dispose();
                            }
                            else
                            {
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            UpdateReserveMsg("無法新增報進, 例外訊息: " + ex.Message, Color.Red);
                        }
                        break;
                    default:
                        break;
                }
               // ReSetControl();
            }
            else
            {
                UpdateReserveMsg("請檢查資料格式", Color.Red);
            }
        }

        delegate void AWSQueryByIDDelegate(object sender, EventArgs e);
        public void AWSQueryByID(object sender, EventArgs e)
        {
            this.BeginInvoke(new AWSQueryByIDDelegate(this.AWSQueryByID_Delegate), sender, e);        
        }
        private void AWSQueryByID_Delegate(object sender, EventArgs e)
        {
            if (_id.Text.Trim().Length == 10 && Lib.SysSetting.IsNatural_Number(_id.Text.Trim()) == true)
            {
                UpdateReserveMsg(string.Empty, Color.Green);
                string msg = string.Empty;
                Lib.DataUtility du = new Lib.DataUtility();
                if (CB_UseAWSService.Checked == true)
                {
                    //AWS = new AWS_WS.WebService2SoapClient();
                    //AWS.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 20);
                    try
                    {
                        DataTable dt = AWS.QueryPlayer(_id.Text.Trim(), DateTime.Today.ToString("yyyy-MM-dd"));
                        if (dt.Rows.Count == 1)
                        {
                            if (dt.Rows[0]["status"].ToString() == "OK")
                            {
                                _name.Text = dt.Rows[0]["name"].ToString();
                                _birth.Text = dt.Rows[0]["birth"].ToString().Replace("-", "/");
                                _unit_code.Text = dt.Rows[0]["unit_code"].ToString();

                                if (dt.Rows[0]["gender"].ToString() == "M")
                                {
                                    _gender.Text = "男性";
                                }
                                else
                                {
                                    _gender.Text = "女性";
                                }

                                Dictionary<string, object> d = new Dictionary<string, object>();
                                d.Add("rank_code", dt.Rows[0]["rank_code"].ToString());
                                DataTable dt_rank = du.getDataTableByText(@"select rank_title from Rank where rank_code = @rank_code", d);
                                if (dt_rank.Rows.Count == 1)
                                {
                                    _rank.SelectedItem = dt_rank.Rows[0][0].ToString();
                                }

                                UpdateMsg("");
                                _birth_Leave(sender, e);
                                _unit_code_TextChanged(sender, e);
                            }
                            else
                            {
                                ReSetControl();
                                UpdateMsg(dt.Rows[0]["status"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"服務無法存取, 請確認此服務網址是否有效. URL:" + AWS.Endpoint.ListenUri.ToString());
                        ReSetControl();
                    }
                }
                else
                {
                    try
                    {
                        if (_id.Text.Substring(1, 1) == "1")
                        {
                            _gender.Text = "男性";
                        }
                        else if (_id.Text.Substring(1, 1) == "2")
                        {
                            _gender.Text = "女性";
                        }
                        UpdateMsg("");

                        if (_gender.Text != "男性" && _gender.Text != "女性")
                            UpdateMsg(@"檢查身份證");
                    }
                    catch (Exception ex)
                    {
                        UpdateMsg(@"檢查身份證");
                    }
                }
            }
            else
            {
                //UpdateMsg(@"檢查身份證");
            }
        }

        private void _id_TextChanged(object sender, EventArgs e)
        {
            AWSQueryByID(sender, e);      
        }

        private void _unit_code_TextChanged(object sender, EventArgs e)
        {
            if (_unit_code.Text.Length == 5)
            {
                try
                {
                    Lib.DataUtility du = new Lib.DataUtility();
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("unit_code", _unit_code.Text.Trim());
                    DataTable dt = du.getDataTableBysp("GetUnit", d);
                    if (dt.Rows.Count > 0)
                    {
                        _unit_title.Text = dt.Rows[0]["unit_title"].ToString();
                        isUnit = true;
                    }
                    else
                    {
                        _unit_title.Text = "無此單位代碼";
                        isUnit = false;
                    }
                }
                catch (Exception ex)
                {
                    isUnit = false;
                }
            }
            else
            {
                isUnit = false;
            }
        }

        private void _birth_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_birth.Text.Trim().Length >= 6)
                {
                    string[] operater = { "/" };
                    string[] info = _birth.Text.Trim().Split(operater, StringSplitOptions.None);
                    if (info[0].Length < 4)
                    {
                        DateTime _birth_dt = Lib.SysSetting.ToWorldDate(_birth.Text.Trim());
                        int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, System.DateTime.Today);
                        if (age_temp >= 15)
                        {
                            isBirth = true;
                        }
                        else
                        {
                            isBirth = false;
                            MessageBox.Show("年齡不得小於15歲,請重新輸入");
                            _birth.Focus();
                        }
                    }
                    else
                    {
                        isBirth = false;
                        MessageBox.Show("請輸入民國格式之生日");
                        _birth.Focus();
                    }
                }
                else
                {
                    isBirth = false;
                    MessageBox.Show("請輸入民國格式之生日");
                    _birth.Focus();
                }
            }
            catch (Exception ex)
            {
                isBirth = false;
                MessageBox.Show("生日格式錯誤請重新輸入");
                _birth.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _id.Text = "";
            _name.Text = "";
            _birth.Text = "";
            _unit_code.Text = "";
            _unit_title.Text = "單位全銜";
        }

        private void CB_UseAWSService_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_UseAWSService.Checked == true)
            {
                SetUseAWSService(true);
            }
            else
            {
                SetUseAWSService(false);
            }
        }

        delegate void BT_IsAcceptReserverDelegate(bool IsAccept);
        public void SetIsAcceptReserver(bool IsAccept)
        {
            this.BeginInvoke(new BT_IsAcceptReserverDelegate(this.SetIsAcceptReserverDelegate), IsAccept);
        }
        private void SetIsAcceptReserverDelegate(bool IsAccept)
        {
            if (IsAccept)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        delegate void ControlDelegate();
        private void ReSetControlDelegate()
        {
            _id.Text = string.Empty;
            _name.Text = string.Empty;
            _birth.Text = string.Empty;
            _unit_code.Text = string.Empty;
            _unit_title.Text = "單位全銜";
            _gender.Text = "男/女";
            LB_ReserveMsg.Text = string.Empty;
        }
        private void ReSetControl()
        {
            this.BeginInvoke(new ControlDelegate(this.ReSetControlDelegate));
        }

        delegate void CB_UseAWSServiceDelegate(bool IsUse);
        public void SetUseAWSService(bool IsUse)
        {
            this.BeginInvoke(new CB_UseAWSServiceDelegate(this.SetUseAWSServiceDelegate), IsUse);    
        }
        private void SetUseAWSServiceDelegate(bool IsUse)
        {
            if (IsUse)
            {
                _name.Enabled = false;
                _rank.Enabled = false;
                _birth.Enabled = false;
                _unit_code.Enabled = false;
                ReSetControl();
            }
            else
            {
                _name.Enabled = true;
                _rank.Enabled = true;
                _birth.Enabled = true;
                _unit_code.Enabled = true;
                ReSetControl();
            }
        }

        delegate void MsgDelegate(string msg);
        public void UpdateMsg(string msg)
        {
            this.BeginInvoke(new MsgDelegate(this.UpdateMsgDelegate), msg);
        }
        private void UpdateMsgDelegate(string msg)
        {
            checkid.Text = msg;
        }

        delegate void ReserveMsgDelegaet(string msg, Color _colr);
        public void UpdateReserveMsg(string msg, Color _color)
        {
            this.BeginInvoke(new ReserveMsgDelegaet(this.UpdateReserveMsgDelegate), msg, _color);
        }
        private void UpdateReserveMsgDelegate(string msg, Color _color)
        {
            LB_ReserveMsg.Text = msg;
            LB_ReserveMsg.ForeColor = _color;
        }
    }
}
