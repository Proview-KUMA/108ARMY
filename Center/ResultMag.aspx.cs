using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class ResultMag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["date"].DefaultValue = System.DateTime.Today.ToShortDateString();
    }

    #region GridView PageIndexChanged
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["date"].DefaultValue = System.DateTime.Today.ToShortDateString();
        TabContainer1.ActiveTabIndex = 4;
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
        TabContainer1.ActiveTabIndex = 3;
    }
    #endregion

    #region Action On Click
    protected void upload_OnClick(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result is NULL", "status", "102"); // 102 未上傳合格
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
            GridView3.DataBind();
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt,"102");
            if (msg == "done")
            {
                //Lib.DataUtility main = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    //    Dictionary<string, object> d = new Dictionary<string, object>();
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    //    d.Add("sid", row["sid"]);
                    d_u.Add("sid", row["sid"]);
                    //    d.Add("height", row["height"]);
                    //    d.Add("weight", row["weight"]);
                    //    d.Add("BMI", row["BMI"]);
                    //    d.Add("bodyfat", row["bodyfat"]);
                    //    d.Add("sit_ups", row["sit_ups"]);
                    //    d.Add("sit_ups_score", row["sit_ups_score"]);
                    //    d.Add("push_ups", row["push_ups"]);
                    //    d.Add("push_ups_score", row["push_ups_score"]);
                    //    d.Add("run", row["run"]);
                    //    d.Add("run_score", row["run_score"]);
                    //    d.Add("status", "202");  // 202 已上傳合格
                    //    list.Add(d);
                    list_u.Add(d_u);
                }
                try
                {
                    //    // 上傳更新總部資料
                    //    main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '202' where sid = @sid", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳合格成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳合格成績 " + dt.Rows.Count.ToString() + " 筆", DateTime.Now);
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
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result is NULL", "status", "103");
        //DataTable dt = du.getDataTableByText("select * from result where status = @status ", "status", "103"); // 未上傳不合格
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "103");
            if (msg == "done")
            {
                //Lib.DataUtility main = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    //Dictionary<string, object> d = new Dictionary<string, object>();
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    //d.Add("sid", row["sid"]);
                    d_u.Add("sid", row["sid"]);
                    //d.Add("height", row["height"]);
                    //d.Add("weight", row["weight"]);
                    //d.Add("BMI", row["BMI"]);
                    //d.Add("bodyfat", row["bodyfat"]);
                    //d.Add("sit_ups", row["sit_ups"]);
                    //d.Add("sit_ups_score", row["sit_ups_score"]);
                    //d.Add("push_ups", row["push_ups"]);
                    //d.Add("push_ups_score", row["push_ups_score"]);
                    //d.Add("run", row["run"]);
                    //d.Add("run_score", row["run_score"]);
                    //d.Add("status", "203");  // 203 已上傳不合格
                    //list.Add(d);
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '203' where sid = @sid", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳不合格成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳不合格成績 " + dt.Rows.Count.ToString() + " 筆", DateTime.Now);
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
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result is NULL", "status", "104"); // 未上傳免測
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有成績');", true);
        }
        else
        {
            dt.TableName = "upload";
            MainWS.WebService MainWs = new MainWS.WebService();
            string msg = MainWs.UploadResult(dt, "104");
            if (msg == "done")
            {
                //Lib.DataUtility main = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    //Dictionary<string, object> d = new Dictionary<string, object>();
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    //d.Add("sid", row["sid"]);
                    d_u.Add("sid", row["sid"]);
                    //d.Add("height", row["height"]);
                    //d.Add("weight", row["weight"]);
                    //d.Add("BMI", row["BMI"]);
                    //d.Add("bodyfat", row["bodyfat"]);
                    //d.Add("sit_ups", row["sit_ups"]);
                    //d.Add("sit_ups_score", row["sit_ups_score"]);
                    //d.Add("push_ups", row["push_ups"]);
                    //d.Add("push_ups_score", row["push_ups_score"]);
                    //d.Add("run", row["run"]);
                    //d.Add("run_score", row["run_score"]);
                    //d.Add("status", "204");  // 204 已上傳免測
                    //list.Add(d);
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = '204' where sid = @sid", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳免測成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳免測成績 " + dt.Rows.Count.ToString() + " 筆", DateTime.Now);
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
        DataTable dt = du.getDataTableByText("select * from result where status = @status and result is NULL", "status", "105"); // 未上傳補測
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
            string msg = MainWs.UploadResult(dt, "105");
            if (msg == "done")
            {
                List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d_u = new Dictionary<string, object>();
                    d_u.Add("sid", row["sid"]);
                    d_u.Add("status", row["status"]);
                    list_u.Add(d_u);
                }
                try
                {
                    // 上傳更新總部資料
                    //main.executeNonQueryByText("update result set height = @height, weight=@weight, BMI = @BMI, bodyfat = @bodyfat, sit_ups = @sit_ups, sit_ups_score = @sit_ups_score, push_ups = @push_ups, push_ups_score = @push_ups_score, run = @run, run_score = @run_score, status = @status where sid = @sid", list);
                    // 更新鑑測站資料狀態
                    du.executeNonQueryByText("update result set status = @status where sid = @sid", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳補測成績 " + dt.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    du.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳補測成績 " + dt.Rows.Count.ToString() + " 筆", DateTime.Now);
                    dt.Dispose();
                    //list.Clear();
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

    protected void ToDo_OnClick(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有資料');", true);
        }
        else
        {
            DataTable dt = new DataTable();
            dt.TableName = "upload";
            dt.Columns.Add("sid");
            dt.Columns.Add("status");
            Lib.DataUtility center = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
            List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow _row = dt.NewRow();
                Dictionary<string, object> d_u = new Dictionary<string, object>();
                RadioButtonList dbl = (RadioButtonList)row.Cells[6].FindControl("rbl");
                var _v = dbl.SelectedValue;
                _row["sid"] = row.Cells[7].Text;
                _row["status"] = _v;
                d_u.Add("sid", row.Cells[7].Text);
                d_u.Add("status", _v);
                dt.Rows.Add(_row);
                list_u.Add(d_u);
            }
            try
            {
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "pending");
                if (msg == "done")
                {
                    center.executeNonQueryByText("update result set status = @status where sid = @sid", list_u);
                    Dictionary<string, object> d_log = new Dictionary<string, object>();
                    d_log.Add("acc", ((Lib.Center.Account_c)Session["account"]).Account);
                    d_log.Add("name", ((Lib.Center.Account_c)Session["account"]).Name);
                    d_log.Add("log", "上傳未檢錄成績 " + GridView1.Rows.Count.ToString() + " 筆");
                    d_log.Add("date", DateTime.Now);
                    center.executeNonQueryByText("insert into log values (@acc,@name,@log,@date)", d_log);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績上傳", acc.Account, "上傳未檢錄成績 " + GridView1.Rows.Count.ToString() + " 筆", DateTime.Now);
                    list_u.Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳成功');",
                    true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            catch (Exception ex)
            {
                Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
            }
            GridView1.DataBind();
            TabContainer1.ActiveTabIndex = 4;
        }
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
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["date"].DefaultValue = System.DateTime.Today.ToShortDateString();
        TabContainer1.ActiveTabIndex = 4;
    }

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

    protected void GridView5_Sorted(object sender, EventArgs e)
    {
        // 補測
        TabContainer1.ActiveTabIndex = 3;
    }

    #endregion
    protected void GridView1_OnRowCommand(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["date"].DefaultValue = System.DateTime.Today.ToShortDateString();
        TabContainer1.ActiveTabIndex = 4;
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }

    protected void ToChangeChecked_OnClick(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                RadioButtonList dbl = (RadioButtonList)row.Cells[6].FindControl("rbl");
                dbl.Items[0].Selected = false;
                dbl.Items[1].Selected = true;
            }
        }
        else
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                RadioButtonList dbl = (RadioButtonList)row.Cells[6].FindControl("rbl");
                dbl.Items[0].Selected = true;
                dbl.Items[1].Selected = false;
            }
        }
        TabContainer1.ActiveTabIndex = 4;
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

}
