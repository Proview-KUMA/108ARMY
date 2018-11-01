using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Lib.Center.Account_c)Session["account"] != null)
            {
                Lib.Center.Account_c acc = (Lib.Center.Account_c)Session["account"];
                txtName.Text = acc.Name;
                txtID.Text = acc.ID;
                txtPwd.Text = acc.Password;
                pwd_HF.Value = acc.Password;
                txtRank.Text = acc.Rank_Code;
                txtTel.Text = acc.Tel;
                txtCell.Text = acc.Cell;
                txtMail.Text = acc.Mail;
                txtIP.Text = acc.IP;
            }
        }
        else
        {
            if ((Lib.Center.Account_c)Session["account"] != null)
            {
                Lib.Center.Account_c acc = (Lib.Center.Account_c)Session["account"];
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                try
                {
                    if (submitType.Value == "profile")
                    {


                        d.Add("id", txtID.Text.Trim());
                        d.Add("name", txtName.Text.Trim());
                        d.Add("pwd", txtPwd.Text.Trim());
                        d.Add("rank", txtRank.Text.Trim());
                        d.Add("tel", txtTel.Text.Trim());
                        d.Add("cell", txtCell.Text.Trim());
                        d.Add("mail", txtMail.Text.Trim());
                        d.Add("ip", txtIP.Text.Trim());
                        d.Add("acc", acc.Account);

                        du.executeNonQueryByText("update account_c set id = @id, name = @name, password = @pwd, rank_code = @rank, tel = @tel, cellphone = @cell, mail = @mail, ip = @ip where account = @acc", d);

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新成功');", true);
                        TabContainer1.ActiveTabIndex = 0;
                        Lib.Center.Account_c newacc = new Lib.Center.Account_c(acc.Account, txtPwd.Text.Trim());
                        Session["account"] = newacc;
                    }
                    if (submitType.Value == "pwdchange")
                    {
                        var newPwd = pwd_HF.Value;
                        d.Add("password", newPwd);
                        d.Add("account", acc.Account);
                        du.executeNonQueryByText("update account_c set password = @password where account = @account", d);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新成功');", true);
                        acc.Password = newPwd;
                        Session["account"] = acc;
                        TabContainer1.ActiveTabIndex = 1;
                    }
                }
                catch (Exception ex)
                {
                    Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + ex.Message + "');", true);
                }
                finally
                {
                    d.Clear();
                    submitType.Value = "";
                }
            }
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
