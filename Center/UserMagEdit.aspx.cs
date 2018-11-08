using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class UserMagEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

        }
        if (Page.IsPostBack)
        {
            var v = submitType.Value;
            Lib.Center.Account_c a = (Lib.Center.Account_c)Session["account"];
            Dictionary<string, object> d = new Dictionary<string, object>();
            Lib.DataUtility du = new Lib.DataUtility();
            try
            {
                switch (v)
                {

                    case "add":
                        #region 新增帳號管理員
                        d.Clear();
                        d.Add("acc", txtAcc.Text.Trim());
                        DataTable dt = du.getDataTableBysp("CheckAccExist",d);
                        if (dt.Rows.Count == 0)
                        {
                            d.Add("account", txtAcc.Text.Trim());
                            d.Add("password", txtPwd.Text.Trim());
                            d.Add("rold_code", roleType.SelectedValue); // 3 = 鑑測站主任代碼, 4 = 鑑測官代碼
                            d.Add("name", txtName.Text.Trim());
                            d.Add("id", txtID.Text.Trim());
                            // d.Add("unit_code", txtUnit.Text.Trim());
                            d.Add("rank_code", txtRank.Text.Trim());
                            d.Add("tel", txtTel.Text.Trim());
                            d.Add("cellphone", txtCell.Text.Trim());
                            d.Add("mail", txtMail.Text.Trim());
                            d.Add("ip", txtIP.Text.Trim());
                            d.Add("pwdChange", "0");
                            d.Add("status", "1");
                            d.Add("byAcc", ((Lib.Center.Account_c)Session["account"]).Account);
                            du.executeNonQueryByText("insert into account_c (account,password,role_code,name,id,rank_code,tel,cellphone,mail,ip,pwdChange,status,byAcc) values (@account,@password,@rold_code,@name,@id,@rank_code,@tel,@cellphone,@mail,@ip,@pwdChange,@status,@byAcc)", d);
                            Lib.SysSetting.AddLog("帳號管理", a.Account, "新增帳號:" + txtAcc.Text.Trim(), System.DateTime.Now);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('新增成功');", true);
                            txtAcc.Text = "";
                            txtPwd.Text = "";
                            txtName.Text = "";
                            txtID.Text = "";
                            //txtUnit.Text = "";
                            txtRank.Text = "";
                            txtTel.Text = "";
                            txtCell.Text = "";
                            txtMail.Text = "";
                            txtIP.Text = "";
                            tabconatiner.ActiveTabIndex = 0;
                            DropDownList1.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('帳號已存在請使用其他帳號');", true);
                            txtAcc.Text = "";
                            txtPwd.Text = "";
                            txtName.Text = "";
                            txtID.Text = "";
                            //txtUnit.Text = "";
                            txtRank.Text = "";
                            txtTel.Text = "";
                            txtCell.Text = "";
                            txtMail.Text = "";
                            txtIP.Text = "";
                            tabconatiner.ActiveTabIndex = 0;
                        }
                        #endregion
                        break;
                    case "update":
                        #region 更新帳號管理員
                        d.Add("password", _txtPwd.Text.Trim());
                        d.Add("name", _txtName.Text.Trim());
                        d.Add("id", _txtID.Text.Trim());
                       // d.Add("unit_code", _txtUnit.Text.Trim());
                        d.Add("rank_code", _txtRank.Text.Trim());
                        d.Add("tel", _txtTel.Text.Trim());
                        d.Add("cellphone", _txtCell.Text.Trim());
                        d.Add("mail", _txtMail.Text.Trim());
                        d.Add("ip", _txtIP.Text.Trim());
                        d.Add("account", DropDownList1.SelectedValue);
                        du.executeNonQueryByText("update account_c set password = @password, name = @name, id = @id, rank_code = @rank_code, tel = @tel, cellphone = @cellphone, mail = @mail, ip = @ip where account = @account", d);
                        Lib.SysSetting.AddLog("帳號管理", a.Account, "更新帳號:" + DropDownList1.SelectedValue, System.DateTime.Now);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新成功');", true);
                        #endregion
                        break;
                    case "delete":
                        #region
                        d.Add("account", DropDownList1.SelectedValue);
                        du.executeNonQueryBysp("DelAccount", d);
                        Lib.SysSetting.AddLog("帳號管理", a.Account, "刪除帳號:" + DropDownList1.SelectedValue, System.DateTime.Now);
                        _txtName.Text = "";
                        _txtID.Text = "";
                        _txtPwd.Text = "";
                        _txtRank.Text = "";
                        _txtTel.Text = "";
                        _txtMail.Text = "";
                        _txtCell.Text = "";
                        _txtIP.Text = "";
                        _txtFun.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('成功刪除');", true);
                        DropDownList1.Items.Remove(DropDownList1.SelectedItem);
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
            }
            d.Clear();
            submitType.Value = "";
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        tabconatiner.ActiveTabIndex = 1;
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from account_c where account = @account", "account", DropDownList1.SelectedValue);
        _txtName.Text = dt.Rows[0]["name"].ToString();
        _txtID.Text = dt.Rows[0]["id"].ToString();
        _txtPwd.Text = dt.Rows[0]["password"].ToString();
        //_txtUnit.Text = dt.Rows[0]["unit_code"].ToString();
        _txtRank.Text = dt.Rows[0]["rank_code"].ToString();
        _txtTel.Text = dt.Rows[0]["tel"].ToString();
        _txtMail.Text = dt.Rows[0]["mail"].ToString();
        _txtCell.Text = dt.Rows[0]["cellphone"].ToString();
        _txtIP.Text = dt.Rows[0]["ip"].ToString();
        if (dt.Rows[0]["role_code"].ToString() == "3")
            _txtFun.Text = "鑑測主任";
        else if(dt.Rows[0]["role_code"].ToString() == "4")
            _txtFun.Text = "鑑測官";

    }

    protected void DropDownList1_OnDataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count == 1)
        {
            tabconatiner.ActiveTabIndex = 1;
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select * from account_c where account = @account", "account", DropDownList1.SelectedValue);
            _txtName.Text = dt.Rows[0]["name"].ToString();
            _txtID.Text = dt.Rows[0]["id"].ToString();
            _txtPwd.Text = dt.Rows[0]["password"].ToString();
           // _txtUnit.Text = dt.Rows[0]["unit_code"].ToString();
            _txtRank.Text = dt.Rows[0]["rank_code"].ToString();
            _txtTel.Text = dt.Rows[0]["tel"].ToString();
            _txtMail.Text = dt.Rows[0]["mail"].ToString();
            _txtCell.Text = dt.Rows[0]["cellphone"].ToString();
            _txtIP.Text = dt.Rows[0]["ip"].ToString();
        }

    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }

    //查詢身份證字號
    protected void btn_CheckId_Click(object sender, EventArgs e)
    {
        lag_Msg.Text = "";
        if (!string.IsNullOrEmpty(txb_checkId.Text))
        {
            if (txb_checkId.Text.Length == 10)
            {
                try
                {
                    MainScoreWS.WebService2 mainWebSv = new MainScoreWS.WebService2();
                    DataTable dt = new DataTable();
                    DateTime checktime = DateTime.Now.AddDays(600);//檢查日期+600天，防止檢查到同年份有報進資料
                    dt = mainWebSv.QueryPlayer(txb_checkId.Text, checktime.ToShortDateString());
                    if (dt.Rows.Count > 0)
                    {
                        string status = string.Empty;
                        status = dt.Rows[0]["status"].ToString();
                        if (status == "OK")
                        {
                            //檢查id是否有註冊過帳號
                            Lib.DataUtility du = new Lib.DataUtility();
                            DataTable IdDt = du.getDataTableByText("select id from Account_c where id='" + txb_checkId.Text.ToUpper() + "' and role_code='" + drl_Type.SelectedValue + "'");
                            if (IdDt.Rows.Count == 0)
                            {
                                txtID.Text = txb_checkId.Text.ToUpper();
                                txtName.Text = dt.Rows[0]["name"].ToString();
                                txtRank.Text = dt.Rows[0]["rank_code"].ToString();
                                roleType.SelectedIndex = drl_Type.SelectedIndex;
                                this.div_inq.Style.Value = "display:none";
                                this.div_data.Style.Value = "";
                            }
                            else
                            {
                                if (drl_Type.SelectedValue == "3")
                                {
                                    lag_Msg.Text = "此身份證字號已註冊過「鑑測主任」之帳號，無法再新增";
                                }
                                else
                                {
                                    lag_Msg.Text = "此身份證字號已註冊過「鑑測官」之帳號，無法再新增";
                                }
                            }
                            
                        }
                        else
                        {
                            lag_Msg.Text = "該員尚未於[基本體能鑑測網]註冊帳號，故無法建立管理者帳號!!";
                        }

                    }
                }
                catch(Exception ex)
                {
                    lag_Msg.Text = "連接總部伺服器失敗，無法驗證身份證字號!!";
                }
                
            }
            else
            {
                lag_Msg.Text = "身份證字號不足10碼!!";
            }
            
        }
        else
        {
            lag_Msg.Text = "請輸入身份證字號!!";
        }
    }
    //新增帳號
    protected void btn_AddAccount_Click(object sender, EventArgs e)
    {
        //檢查必填欄位有無空白
        if(!string.IsNullOrEmpty(txtAcc.Text) && !string.IsNullOrEmpty(txtID.Text) && !string.IsNullOrEmpty(txtPwd.Text) && 
            !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtRank.Text))
        {
            //檢查帳號有無重複
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable AccDt = du.getDataTableByText("select account from Account_c where account='" + txtAcc.Text + "'");
            if (AccDt.Rows.Count == 0)
            {
                //檢查id是否有註冊過帳號
                DataTable IdDt = du.getDataTableByText("select id from Account_c where id='" + txtID.Text + "' and role_code='" + roleType.SelectedValue + "'");
                if (IdDt.Rows.Count == 0)
                {
                    Lib.Center.Account_c a = (Lib.Center.Account_c)Session["account"];
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("account", txtAcc.Text.Trim());
                    d.Add("password", txtPwd.Text.Trim());
                    d.Add("rold_code", roleType.SelectedValue); // 3 = 鑑測站主任代碼, 4 = 鑑測官代碼
                    d.Add("name", txtName.Text.Trim());
                    d.Add("id", txtID.Text.Trim());
                    d.Add("rank_code", txtRank.Text.Trim());
                    d.Add("tel", txtTel.Text.Trim());
                    d.Add("cellphone", txtCell.Text.Trim());
                    d.Add("mail", txtMail.Text.Trim());
                    d.Add("ip", txtIP.Text.Trim());
                    d.Add("pwdChange", "0");
                    d.Add("status", "1");
                    d.Add("byAcc", ((Lib.Center.Account_c)Session["account"]).Account);
                    du.executeNonQueryByText("insert into account_c (account,password,role_code,name,id,rank_code,tel,cellphone,mail,ip,pwdChange,status,byAcc) values (@account,@password,@rold_code,@name,@id,@rank_code,@tel,@cellphone,@mail,@ip,@pwdChange,@status,@byAcc)", d);
                    Lib.SysSetting.AddLog("帳號管理", a.Account, "新增帳號:" + txtAcc.Text.Trim(), System.DateTime.Now);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('新增成功');", true);
                    txtAcc.Text = "";
                    txtPwd.Text = "";
                    txtName.Text = "";
                    txtID.Text = "";
                    txtRank.Text = "";
                    txtTel.Text = "";
                    txtCell.Text = "";
                    txtMail.Text = "";
                    txtIP.Text = "";
                    txb_checkId.Text = "";
                    tabconatiner.ActiveTabIndex = 0;
                    DropDownList1.DataBind();
                    this.div_inq.Style.Value = "";
                    this.div_data.Style.Value = "display:none";
                }
                else
                {
                    if (roleType.SelectedValue == "3")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('此身份證字號已註冊過「鑑測主任」之帳號，無法再新增!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('此身份證字號已註冊過「鑑測官」之帳號，無法再新增!!');", true);
                    }
                }
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('帳號重複，無法使用!!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('帳號、姓名、身份證字號、密碼、級職代碼欄位不可空白!!');", true);
        }
    }
    //取消
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        this.div_inq.Style.Value = "";
        this.div_data.Style.Value = "display:none";
    }

}
