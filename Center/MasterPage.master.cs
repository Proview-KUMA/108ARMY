using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.Center;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Account_c)Session["account"] != null)
            {
                Account_c acc = (Account_c)Session["account"];
                user.Text = " 歡迎 " + acc.Account + " 登入系統  , 現在是中華民國 " + Lib.SysSetting.ToRocDateFormat(DateTime.Now.ToString("yyyy")) + " 年 " + DateTime.Now.Month.ToString() + " 月 " + DateTime.Now.Day.ToString() + " 日 " + Lib.SysSetting.ToRocWeekFormat(DateTime.Now.DayOfWeek) + ", ";
                //user.Text = acc.Account;
                MenuItem item_new = new MenuItem("鑑測系統設定");
                item_new.NavigateUrl = "~/SysSetting.aspx";
                Menu1.Items.Add(item_new);
                switch (acc.Role)
                {
                    case User_Role.Administrator:
                        MenuItem item = new MenuItem("個人資訊設定");
                        item.NavigateUrl = "~/Pro.aspx";
                        Menu1.Items.Add(item);
                        MenuItem item1 = new MenuItem("帳號管理者管理");
                        item1.NavigateUrl = "~/AccMagEdit.aspx";
                        Menu1.Items.Add(item1);
                        MenuItem item2 = new MenuItem("事件記錄簿檢視");
                        item2.NavigateUrl = "~/LogView.aspx";
                        Menu1.Items.Add(item2);
                        MenuItem item21 = new MenuItem("系統維護");
                        item21.NavigateUrl = "~/SysAdmin.aspx";
                        Menu1.Items.Add(item21);
                        break;
                    case User_Role.AccountManager:
                        MenuItem item3 = new MenuItem("個人資訊設定");
                        item3.NavigateUrl = "~/Pro.aspx";
                        Menu1.Items.Add(item3);
                        MenuItem item4 = new MenuItem("使用者管理");
                        item4.NavigateUrl = "~/UserMagEdit.aspx";
                        Menu1.Items.Add(item4);
                        break;
                    case User_Role.CenterSupervisor:
                        MenuItem item5 = new MenuItem("個人資訊設定");
                        item5.NavigateUrl = "~/Pro.aspx";
                        Menu1.Items.Add(item5);
                        MenuItem item18 = new MenuItem("查詢鑑測狀態");
                        item18.NavigateUrl = "~/ViewResult.aspx";
                        Menu2.Items.Add(item18);
                        MenuItem item6 = new MenuItem("成績管理");
                        item6.NavigateUrl = "~/ResultMag.aspx";
                        Menu2.Items.Add(item6);
                        MenuItem item17 = new MenuItem("現報成績管理");
                        item17.NavigateUrl = "~/PresentResultMag.aspx";
                        Menu2.Items.Add(item17);
                        MenuItem item25 = new MenuItem("人工成績管理");
                        item25.NavigateUrl = "~/HumanResultMag.aspx";
                        Menu2.Items.Add(item25);
                        MenuItem item24 = new MenuItem("人工鑑測成績輸入");
                        item24.NavigateUrl = "~/ScoreKeyin.aspx";
                        Menu1.Items.Add(item24);
                        MenuItem item8 = new MenuItem("資料下載");
                        item8.NavigateUrl = "~/Download.aspx";
                        Menu1.Items.Add(item8);
                        MenuItem item13 = new MenuItem("成績補正");
                        item13.NavigateUrl = "~/ResultCorrect.aspx";
                        Menu1.Items.Add(item13);
                        MenuItem item14 = new MenuItem("成績統計");
                        item14.NavigateUrl = "~/Reporting.aspx";
                        Menu1.Items.Add(item14);                
                        MenuItem item19 = new MenuItem("數據資料檢視");
                        item19.NavigateUrl = "~/107_Result_Chart.aspx";
                        Menu2.Items.Add(item19);
                        MenuItem item22 = new MenuItem("設定鑑測項目");
                        item22.NavigateUrl = "~/ChangeItem.aspx";
                        Menu1.Items.Add(item22);
                        MenuItem item7 = new MenuItem("RFID對照表");
                        item7.NavigateUrl = "~/RFIDUpdate.aspx";
                        Menu1.Items.Add(item7);
                        MenuItem item12 = new MenuItem("系統維護");
                        item12.NavigateUrl = "~/SysAdmin.aspx";
                        Menu1.Items.Add(item12);
                        break;
                    case User_Role.CenterOfficer:
                        MenuItem item9 = new MenuItem("個人資訊設定");
                        item9.NavigateUrl = "~/Pro.aspx";
                        Menu1.Items.Add(item9);
                        MenuItem item10 = new MenuItem("查詢鑑測狀態");
                        item10.NavigateUrl = "~/ViewResult.aspx";
                        Menu1.Items.Add(item10);
                        MenuItem item11 = new MenuItem("清冊下載");
                        item11.NavigateUrl = "~/ToExcel.aspx";
                        Menu1.Items.Add(item11);
                        MenuItem item15 = new MenuItem("成績統計");
                        item15.NavigateUrl = "~/Reporting.aspx";
                        Menu1.Items.Add(item15);
                        MenuItem item16 = new MenuItem("成績查詢");
                        item16.NavigateUrl = "~/SearchScore.aspx";
                        Menu1.Items.Add(item16);
                        MenuItem item20 = new MenuItem("替代項目成績輸入");
                        item20.NavigateUrl = "~/ReplaceItemScore.aspx";
                        Menu1.Items.Add(item20);
                        MenuItem item23 = new MenuItem("成績試算");
                        item23.NavigateUrl = "~/ScoreTrial.aspx";
                        Menu1.Items.Add(item23);
                        MenuItem item26 = new MenuItem("數據資料檢視");
                        item26.NavigateUrl = "~/107_Result_Chart.aspx";
                        Menu1.Items.Add(item26);
                        break;
                    default:
                        break;

                }

                if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
                {
                    string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                    System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                    string sRet = oInfo.Name;
                    if (sRet != "SysSetting.aspx" && sRet != "LogView.aspx" && sRet != "SysAdmin.aspx")
                    {
                        Response.Redirect("~/SysSetting.aspx");
                    }
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "javascript:alert('逾時登出，請重新登入');window.location='./Login.aspx';", true);
                //Response.Redirect("~/Login.aspx");
            }
        }
    }
    protected void logOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Login.aspx");
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
