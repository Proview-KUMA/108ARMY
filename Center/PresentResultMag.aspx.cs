using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PresentResultMag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

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

    protected void GridView5_PageIndexChanged(object sender, EventArgs e)
    {
        // 補測
        TabContainer1.ActiveTabIndex = 4;
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        // 未檢錄
        TabContainer1.ActiveTabIndex = 5;
    }
    protected void upload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result = '666' ", "status", "102"); // 102 未上傳合格
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
            GridView3.DataBind();
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "present");
            if (msg == "done")
            {
                //Lib.DataUtility main = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    d_u.Add("id", row["id"]);
                    list_u.Add(d_u);
                }
                try
                {
                    //    // 上傳更新總部資料
                    //    main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '202' , result = '777' where id = @id and result = '666' and status = '102' ", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳合格成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    dt.Dispose();
                    //list.Clear();
                    list_u.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳合格成績成功');", true);
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
            GridView3.DataBind();
        }
    }

    protected void falseUpload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result = '666'", "status", "103");
        //DataTable dt = du.getDataTableByText("select * from result where status = @status ", "status", "103"); // 未上傳不合格
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "present");
            if (msg == "done")
            {
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    d_u.Add("id", row["id"]);
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '203' , result = '777' where id = @id and result = '666' and status = '103'", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳不合格成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    dt.Dispose();
                    //list.Clear();
                    list_u.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳不合格成績成功');", true);

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
            GridView2.DataBind();
            TabContainer1.ActiveTabIndex = 1;
        }
    }

    protected void NoneUpload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result = '666'", "status", "104"); // 未上傳免測
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "present");
            if (msg == "done")
            {
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    d_u.Add("id", row["id"]);
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '204', result = '777' where id = @id and result = '666' and status = '104'", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳免測成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    dt.Dispose();
                    //list.Clear();
                    list_u.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳免測成績成功');", true);

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
            GridView4.DataBind();
            TabContainer1.ActiveTabIndex = 2;
        }
    }

    protected void ReActionUpload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result = '666' and (sit_ups is null or push_ups is null or run is null)", "status", "105"); // 未上傳補測
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
        }
        else
        { 
            foreach (DataRow _row in dt.Rows)
            {
                foreach (GridViewRow row in GridView5.Rows)
                {
                    RadioButtonList dbl = (RadioButtonList)row.Cells[8].FindControl("rb2");
                    var _v = dbl.SelectedValue;
                    if (_row["id"].ToString() == row.Cells[1].Text)
                    {
                        _row["status"] = _v;
                    }
                }
            }
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "present");
            if (msg == "done")
            {
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    d_u.Add("id", row["id"]);
                    d_u.Add("status", row["status"].ToString().Replace("1","2"));
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = @status , result = '777' where id = @id and result = '666' ", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳補測成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    dt.Dispose();
                    list_u.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳補測成績成功');", true);
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
            GridView5.DataBind();
            TabContainer1.ActiveTabIndex = 3;
        }
    }
    
    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
    protected void ToChangeCheckedDay_OnClick(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            foreach (GridViewRow row in GridView5.Rows)
            {
                RadioButtonList dbl = (RadioButtonList)row.Cells[6].FindControl("rb2");
                dbl.Items[0].Selected = false;
                dbl.Items[1].Selected = true;
            }
        }
        else
        {
            foreach (GridViewRow row in GridView5.Rows)
            {
                RadioButtonList dbl = (RadioButtonList)row.Cells[6].FindControl("rb2");
                dbl.Items[0].Selected = true;
                dbl.Items[1].Selected = false;
            }
        }
        TabContainer1.ActiveTabIndex = 3;
    }

    //刪除現報未檢錄之人員
    protected void btn_Del999_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        try
        {
            du.executeNonQueryBysp("EX107_DelReportedNoCheck");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料刪除完成!!');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料刪除失敗："+ex.Message+"');", true);
        }
        GridView1.DataBind();
        //重新整理網頁
        Response.Redirect(Request.FilePath);
        TabContainer1.ActiveTabIndex = 4;
    }
}
