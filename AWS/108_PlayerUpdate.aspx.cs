using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib;

public partial class _108_PlayerUpdate : System.Web.UI.Page
{
    static string Sid = string.Empty;
    static string Name = string.Empty;
    static string Id = string.Empty;
    static string Birth = string.Empty;
    static string Mail = string.Empty;
    static string Password = string.Empty;
    static string Old_Name = string.Empty;
    static string Old_Id = string.Empty;
    static string Old_Birth = string.Empty;
    static string Old_Mail = string.Empty;
    static string Old_Password = string.Empty;
    static string UpdateLog = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] != null)
        {
            Account a = (Account)Session["account"];
            if (a.Role != ((int)SysSetting.Role.admin_hq).ToString())
            {
                Response.Redirect("~/index.aspx");
            }
            else
            {
                Sid = Request.QueryString["sid"].ToString();
                if (Page.IsPostBack == false)//剛開始載入頁面
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    Lib.DataUtility du = new Lib.DataUtility();
                    d.Add("value", Sid);
                    d.Add("type", "sid");
                    DataTable dt = du.getDataTableBysp("Ex108_GetPlayerData", d);
                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["name"].ToString()))
                        {
                            Name = dt.Rows[0]["name"].ToString();
                            txb_Name.Text = Name;
                            Old_Name = Name;
                            lab_OldName.Text = "(" + Old_Name + ")";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["id"].ToString()))
                        {
                            Id = dt.Rows[0]["id"].ToString();
                            txb_Id.Text = Id;
                            Old_Id = Id;
                            lab_OldId.Text = "(" + Old_Id + ")";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["birth"].ToString()))
                        {
                            Birth = dt.Rows[0]["birth"].ToString();
                            txb_Birth.Text = Birth;
                            Old_Birth = Birth;
                            lab_OldBirth.Text = "(" + Old_Birth + ")";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["mail"].ToString()))
                        {
                            Mail = dt.Rows[0]["mail"].ToString();
                            txb_Mail.Text = Mail;
                            Old_Mail = Mail;
                            lab_OldMail.Text = "(" + Old_Mail + ")";
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["password"].ToString()))
                        {
                            Password = dt.Rows[0]["password"].ToString();
                            txb_Password.Text = Password;
                            Old_Password = Password;
                            lab_OldPassword.Text = "(" + Old_Password + ")";
                        }

                    }
                }
                else//提交資料後回傳
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    Lib.DataUtility du = new Lib.DataUtility();
                    Name = txb_Name.Text;
                    Id = txb_Id.Text.Trim();
                    Birth = txb_Birth.Text.Trim();
                    Mail = txb_Mail.Text.Trim();
                    d.Add("sid", Sid);
                    d.Add("name", Name);
                    d.Add("id", Id);
                    d.Add("birth", Birth);
                    d.Add("mail", Mail);
                    d.Add("password", Password);
                    try
                    {
                        //更新資料
                        du.executeNonQueryBysp("Ex108_UpdatePlayerData", d);
                        //寫入log
                        UpdateLog = string.Empty;
                        if (Old_Name != Name)
                            UpdateLog += "名[" + Old_Name + "," + Name + "]";
                        if (Old_Id != Id)
                            UpdateLog += "證[" + Old_Id + "," + Id + "]";
                        if (Old_Birth != Birth)
                            UpdateLog += "生[" + Old_Birth + "," + Birth + "]";
                        if (Old_Mail != Mail)
                            UpdateLog += "郵[" + Old_Mail + "," + Mail + "]";
                        if (Old_Password != Password)
                            UpdateLog += "密[" + Old_Password + "," + Password + "]";
                        if (!string.IsNullOrEmpty(UpdateLog))
                            SysSetting.AddLog("基本資料異動", a.AccountName, UpdateLog, DateTime.Now);
                        //回傳成功
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "window.opener.outside('" + Id + "');window.close()", true);
                    }
                    catch (Exception ex)
                    {
                        //記錄錯誤訊息
                        SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, this.ToString());
                        //回傳失敗
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "window.opener.outside('Err');window.close()", true);
                    }

                }
            }
            
        }
        if (Session["account"] == null && Session["player"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        
    }
}