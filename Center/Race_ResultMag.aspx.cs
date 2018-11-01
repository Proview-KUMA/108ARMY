using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class Race_ResultMag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            if (Lib.SysSetting.CurrentSystemMode() != Lib.SysSetting.SystemMode.Race)
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {

        }

        TabContainer1.ActiveTabIndex = 0;

    }

    #region GridView PageIndexChanged
    
    protected void GridView2_PageIndexChanged(object sender, EventArgs e)
    {
        // 不合格
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView3_PageIndexChanged(object sender, EventArgs e)
    {
        // 合格
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void GridView4_PageIndexChanged(object sender, EventArgs e)
    {
        // 免測
        TabContainer1.ActiveTabIndex = 2;
    }
    #endregion

    #region Action On Click
    protected void upload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        try
        {
            d.Add("status", "102");
            DataTable dt = du.getDataTableBysp("Race_SelectResult", d); // 102 未上傳合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
                GridView3.DataBind();
            }
            else
            {
                dt.TableName = "upload";
                if (Lib.SysSetting.CourrentUploadMode() == Lib.SysSetting.UploadMode.Remote)
                {
                    RemoteWS.WebService RemoteWS = new RemoteWS.WebService();
                    RemoteWS.Url = "http://" + Lib.SysSetting.GetRemoteIP() + "/WebService.asmx";
                    RemoteWS.Discover();
                    string msg = RemoteWS.InsertResult(dt);
                    if (msg == "Done")
                    {
                        try
                        {
                            //    // 上傳更新總部資料
                            //    main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                            // 更新鑑測站資料狀態
                            d.Clear();
                            d.Add("result", dt);
                            int count = dt.Rows.Count;
                            dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                            if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                            {
                                Dictionary<string, object> d_log = new Dictionary<string, object>();
                                d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                                d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                                d_log.Add("log", "上傳合格成績 " + count + " 筆");
                                d_log.Add("date", DateTime.Now);
                                du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                                Account_c acc = (Account_c)Session["account"];
                                Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳合格成績 " + count + " 筆", DateTime.Now);
                                dt.Dispose();
                                //list.Clear();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳合格成績" + count + " 筆成功');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                    }
                }
                else
                {
                    try
                    {
                        d.Clear();
                        d.Add("result", dt);
                        int count = dt.Rows.Count;
                        dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                        if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                        {
                            Dictionary<string, object> d_log = new Dictionary<string, object>();
                            d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                            d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                            d_log.Add("log", "上傳合格成績 " + count + " 筆");
                            d_log.Add("date", DateTime.Now);
                            du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                            Account_c acc = (Account_c)Session["account"];
                            Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳合格成績 " + count + " 筆", DateTime.Now);
                            dt.Dispose();
                            //list.Clear();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳合格成績" + count + " 筆成功');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
        }
        GridView3.DataBind();
        Label1.Text = "合格成績共" + GridView3.Rows.Count.ToString() + "筆";
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void falseUpload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        try
        {
            d.Add("status", "103");
            DataTable dt = du.getDataTableBysp("Race_SelectResult", d); // 103 未上傳合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
                GridView2.DataBind();
            }
            else
            {
                dt.TableName = "upload";
                if (Lib.SysSetting.CourrentUploadMode() == Lib.SysSetting.UploadMode.Remote)
                {
                    RemoteWS.WebService RemoteWS = new RemoteWS.WebService();
                    RemoteWS.Url = "http://" + Lib.SysSetting.GetRemoteIP() + "/WebService.asmx";
                    RemoteWS.Discover();
                    System.Threading.Thread.Sleep(2000);
                    string msg = RemoteWS.InsertResult(dt);
                    if (msg == "Done")
                    {
                        try
                        {
                            //    // 上傳更新總部資料
                            //    main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                            // 更新鑑測站資料狀態
                            d.Clear();
                            d.Add("result", dt);
                            int count = dt.Rows.Count;
                            dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                            if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                            {
                                Dictionary<string, object> d_log = new Dictionary<string, object>();
                                d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                                d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                                d_log.Add("log", "上傳不合格成績 " + count + " 筆");
                                d_log.Add("date", DateTime.Now);
                                du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                                Account_c acc = (Account_c)Session["account"];
                                Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳不合格成績 " + count + " 筆", DateTime.Now);
                                dt.Dispose();
                                //list.Clear();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳不合格成績" + count + " 筆成功');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                    }
                }
                else
                {
                    try
                    {
                        d.Clear();
                        d.Add("result", dt);
                        int count = dt.Rows.Count;
                        dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                        if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                        {
                            Dictionary<string, object> d_log = new Dictionary<string, object>();
                            d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                            d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                            d_log.Add("log", "上傳不合格成績 " + count + " 筆");
                            d_log.Add("date", DateTime.Now);
                            du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                            Account_c acc = (Account_c)Session["account"];
                            Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳不合格成績 " + count + " 筆", DateTime.Now);
                            dt.Dispose();
                            //list.Clear();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳不合格成績" + count + " 筆成功');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
        }
        GridView2.DataBind();
        Label1.Text = "不合格成績共" + GridView2.Rows.Count.ToString() + "筆";
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void NoneUpload_OnClick(object sender, EventArgs e)
    {
       Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        try
        {
            d.Add("status", "104");
            DataTable dt = du.getDataTableBysp("Race_SelectResult", d); // 104 未上傳免測
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
                GridView4.DataBind();
            }
            else
            {
                dt.TableName = "upload";
                if (Lib.SysSetting.CourrentUploadMode() == Lib.SysSetting.UploadMode.Remote)
                {
                    RemoteWS.WebService RemoteWS = new RemoteWS.WebService();
                    RemoteWS.Url = "http://" + Lib.SysSetting.GetRemoteIP() + "/WebService.asmx";
                    RemoteWS.Discover();
                    System.Threading.Thread.Sleep(2000);
                    string msg = RemoteWS.InsertResult(dt);
                    if (msg == "Done")
                    {
                        try
                        {
                            //    // 上傳更新總部資料
                            //    main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                            // 更新鑑測站資料狀態
                            d.Clear();
                            d.Add("result", dt);
                            int count = dt.Rows.Count;
                            dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                            if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                            {
                                Dictionary<string, object> d_log = new Dictionary<string, object>();
                                d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                                d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                                d_log.Add("log", "上傳免測成績 " + count + " 筆");
                                d_log.Add("date", DateTime.Now);
                                du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                                Account_c acc = (Account_c)Session["account"];
                                Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳免測成績 " + count + " 筆", DateTime.Now);
                                dt.Dispose();
                                //list.Clear();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳免測成績" + count + " 筆成功');", true);                          
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                    }
                }
                else
                {
                    try
                    {
                        d.Clear();
                        d.Add("result", dt);
                        int count = dt.Rows.Count;
                        dt = du.getDataTableBysp("Race_UpdateResultAfterUpload", d);
                        if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Done")
                        {
                            Dictionary<string, object> d_log = new Dictionary<string, object>();
                            d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                            d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                            d_log.Add("log", "上傳免測成績 " + count + " 筆");
                            d_log.Add("date", DateTime.Now);
                            du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                            Account_c acc = (Account_c)Session["account"];
                            Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳免測成績 " + count + " 筆", DateTime.Now);
                            dt.Dispose();
                            //list.Clear();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳免測成績" + count + " 筆成功');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('更新local資料庫成績失敗');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
        }
        GridView4.DataBind();
        Label1.Text = "免測成績共" + GridView4.Rows.Count.ToString() + "筆";
        TabContainer1.ActiveTabIndex = 2;
    }

    #endregion

    #region GridView OnRowDataBound
    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    #endregion

    #region GridView Sorting


    protected void GridView2_Sorted(object sender, EventArgs e)
    {
        // 不合格
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView3_Sorted(object sender, EventArgs e)
    {
        // 合格
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void GridView4_Sorted(object sender, EventArgs e)
    {
        // 免測
        TabContainer1.ActiveTabIndex = 2;
    }

    #endregion

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 0)
            Label1.Text = "合格成績共" + GridView3.Rows.Count.ToString() + "筆";

        if (TabContainer1.ActiveTabIndex == 1)
            Label1.Text = "不合格成績共" + GridView2.Rows.Count.ToString() + "筆";

        if (TabContainer1.ActiveTabIndex == 2)
            Label1.Text = "免測成績共" + GridView4.Rows.Count.ToString() + "筆";
    }
}
