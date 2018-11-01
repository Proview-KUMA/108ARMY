using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class SysSetting : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
	//ShowSystemMode();
        if (!Page.IsPostBack)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = new DataTable();
            dt = du.getDataTableByText("select item, value from sysvalue");
            string checkV = dt.Rows[1][1].ToString();
            if (checkV == "local")
            {
                CheckBoxList1.Items[0].Selected = true;
                CheckBoxList1.Items[1].Selected = false;
            }
            else
            {
                CheckBoxList1.Items[1].Selected = true;
                CheckBoxList1.Items[0].Selected = false;
            }
            checkV = dt.Rows[2][1].ToString();
            txtRemoteIP.Text = checkV;
        }

        Account_c acc = (Account_c)Session["account"];
        if (acc != null)
        {
            if (acc.Role == User_Role.Administrator)
            {
                ClearResult.Visible = true;
                ClearResult.Enabled = true;
                btnSwitch.Visible = true;
                btnSwitch.Enabled = true;
                Button1.Visible = true;
                Button1.Enabled = true;
            }
            else
            {
                ClearResult.Visible = false;
                ClearResult.Enabled = false;
                btnSwitch.Visible = false;
                btnSwitch.Enabled = false;
                Button1.Visible = false;
                Button1.Enabled = false;
            }
            ShowSystemMode();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "javascript:alert('逾時登出，請重新登入');window.location='./Login.aspx';", true);
        }
    }

    private void ShowSystemMode()
    {
        try
        {
            Lib.SysSetting.SystemMode myMode = Lib.SysSetting.CurrentSystemMode();
            if (myMode == Lib.SysSetting.SystemMode.Normal)
            {
                LB_mode.Text = "鑑測系統模式";
                input1.Disabled = true;
                input2.Disabled = true;
                input3.Disabled = true;
                input4.Disabled = true;
                input5.Disabled = true;
                input6.Disabled = true;
                input7.Disabled = true;
                input8.Disabled = true;
                input9.Disabled = true;
                input10.Disabled = true;
                input11.Disabled = true;
                input12.Disabled = true;
                input13.Disabled = true;
                ClearResult.Visible = false;
                Bt_TestWebService.Enabled = false;
            }
            if (myMode == Lib.SysSetting.SystemMode.Race)
            {
                if(Lib.SysSetting.CourrentUploadMode() == Lib.SysSetting.UploadMode.Local)
                    LB_mode.Text = "競賽系統 => 本機模式";
                else
                    LB_mode.Text = "競賽系統 => 遠端主機模式 , IP : " + Lib.SysSetting.GetRemoteIP();
                input1.Disabled = false;
                input2.Disabled = false;
                input3.Disabled = false;
                input4.Disabled = false;
                input5.Disabled = false;
                input6.Disabled = false;
                input7.Disabled = false;
                input8.Disabled = false;
                input9.Disabled = false;
                input10.Disabled = false;
                input11.Disabled = false;
                input12.Disabled = false;
                input13.Disabled = false;
		        ClearResult.Visible = true;
                Bt_TestWebService.Enabled = true;
            }
            if (myMode == Lib.SysSetting.SystemMode.Original)
            {
                LB_mode.Text = "此系統只有鑑測功能";
                input1.Disabled = true;
                input2.Disabled = true;
                input3.Disabled = true;
                input4.Disabled = true;
                input5.Disabled = true;
                input6.Disabled = true;
                input7.Disabled = true;
                input8.Disabled = true;
                input9.Disabled = true;
                input10.Disabled = true;
                input11.Disabled = true;
                input12.Disabled = true;
                input13.Disabled = true;
                ClearResult.Visible = false;
                Bt_TestWebService.Enabled = false;
            }

            //競賽期間將清除成績的功能全部關閉
            ClearResult.Enabled = false;
        }
        catch (Exception ex)
        {
            LB_mode.Text = ex.Message;
        }
    }

    protected void btnSwitch_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        try
        {
            if (Session["Account"] != null)
            {
                Lib.Center.Account_c acc = (Lib.Center.Account_c)Session["Account"];
                du.executeNonQueryBysp("SwitchSystem");
                Lib.SysSetting.AddLog("更新系統模式", acc.Account, LB_mode.Text + "已被切換", DateTime.Now);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "')", true);
        }
        ShowSystemMode();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if ((CheckBoxList1.Items[0].Selected == true && CheckBoxList1.Items[1].Selected == true) ||
            (CheckBoxList1.Items[0].Selected == false && CheckBoxList1.Items[1].Selected == false))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請單選一類模式')", true);
        }
        else
        {
            if (string.IsNullOrEmpty(txtRemoteIP.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入遠端主機位址，若為本機模式請輸入127.0.0.1')", true);
            }
            else
            { 
                //進行更新資料庫資料
                string mode_value = string.Empty;
                if (CheckBoxList1.Items[0].Selected)
                {
                    mode_value = CheckBoxList1.Items[0].Value;
                }
                else
                {
                    mode_value = CheckBoxList1.Items[1].Value;
                }
                if (Session["Account"] != null)
                {
                    Lib.Center.Account_c acc = (Lib.Center.Account_c)Session["Account"];

                    Lib.DataUtility du = new Lib.DataUtility();
                    du.executeNonQueryByText("update sysvalue set value = '" + mode_value + "' where item = 'exchange'");
                    du.executeNonQueryByText("update sysvalue set value = '" + txtRemoteIP.Text.Trim() + "' where item = 'remote_ip'");
                    Lib.SysSetting.AddLog("更新競賽系統設定值", acc.Account, "競賽系統設定值已被更改", DateTime.Now);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('更新成功')", true);
                    ShowSystemMode();
                    //Response.Redirect("~/index.aspx");
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
    protected void Bt_TestWebService_Click(object sender, EventArgs e)
    {
        try
        {
            LB_WsMessage.Text = string.Empty;
            if (Lib.SysSetting.CourrentUploadMode() == Lib.SysSetting.UploadMode.Remote)
            {
                RemoteWS.WebService RemoteWS = new RemoteWS.WebService();
                RemoteWS.Url = "http://" + txtRemoteIP.Text + "/WebService.asmx";
                RemoteWS.Discover();
                LB_WsMessage.Text = RemoteWS.HelloWorld();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請變更資料交換模式 => 遠端主機')", true);
            }
        }
        catch (Exception ex)
        {
            LB_WsMessage.Text = "工作失敗, 例外訊息 : " + ex.Source + ex.Message;  
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
        Response.Redirect("~/Login.aspx");
    }
    protected void ClearResult_Click(object sender, EventArgs e)
    {
        try
        {
            Lib.Center.Account_c acc = (Lib.Center.Account_c)Session["Account"];
            Lib.DataUtility du = new Lib.DataUtility();
            du.executeNonQueryBysp("Race_ClearData");
            Lib.SysSetting.AddLog("清除競賽成績", acc.Account, "競賽成績已被清除", DateTime.Now);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('已清除競賽成績')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "')", true);
        }
    }
}
