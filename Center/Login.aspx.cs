using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label3.Text = ConfigurationManager.AppSettings["centerName"].ToString();
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        Lib.Center.Account_c acc = new Lib.Center.Account_c(txtName.Text.Trim(), txtPwd.Text.Trim());
        if (acc.LoginStatus == Lib.Center.LoginStatus.Logout)
        {
            Lib.SysSetting.AddLog("登入", txtName.Text, "進階使用者登入密碼驗證失敗,登入IP : " + Request.UserHostAddress.ToString(), DateTime.Now);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('登入失敗')", true);
        }
        if (acc.LoginStatus == Lib.Center.LoginStatus.Login)
        {
            Session["account"] = acc;
            Lib.SysSetting.AddLog("登入", acc.Account, "進階使用者登入成功,登入IP : " + Request.UserHostAddress.ToString(), DateTime.Now); 
            Response.Redirect("~/Index.aspx");
        }
    }
    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message,sender.ToString());
        Server.ClearError();
    }
}
