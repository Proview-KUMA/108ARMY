using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class Race_ReplaceItemScore : System.Web.UI.Page
{
    enum UnitPer
    { 
        Times,
        Second
    }

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

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        DateTime date = DateTime.Today;
        d.Add("id", id.Value);
        d.Add("date", date);
        DataTable dt = du.getDataTableBysp("Race_GetReplaceItemScore", d);
        if (dt.Rows.Count == 1)
        {
            name.Value = dt.Rows[0]["name"].ToString();
            checkid.Value = id.Value;
            sit_ups_name.Text = dt.Rows[0]["sit_ups_name"].ToString();
            push_ups_name.Text = dt.Rows[0]["push_ups_name"].ToString();
            run_name.Text = dt.Rows[0]["run_name"].ToString();
            //situps.Value = dt.Rows[0]["sit_ups"].ToString();
            //pushups.Value = dt.Rows[0]["push_ups"].ToString();
            //run.Value = dt.Rows[0]["run"].ToString();
            status.Value = dt.Rows[0]["status"].ToString();
            dateValue.Value = Convert.ToDateTime(dt.Rows[0]["date"].ToString()).ToShortDateString();
            if (dt.Rows[0]["memo"].ToString().Substring(0, 1) == "0")
            {
                situps.Disabled = true;
                situps_min.Disabled = true;
                situps_sec.Disabled = true;
                situps.Value = dt.Rows[0]["sit_ups"].ToString();
            }
            else
            {
                UnitPer _situps = ConvertUnit(dt.Rows[0]["memo"].ToString().Substring(0, 1));
                if (_situps == UnitPer.Second)
                {
                    situps.Disabled = true;
                    situps_min.Disabled = false;
                    situps_sec.Disabled = false;
                    if (dt.Rows[0]["sit_ups"] != DBNull.Value)
                    {
                        int min = Convert.ToInt32(dt.Rows[0]["sit_ups"]) / 60;
                        int sec = Convert.ToInt32(dt.Rows[0]["sit_ups"]) % 60;
                        situps_min.Value = min.ToString();
                        situps_sec.Value = sec.ToString();
                    }
                }
                else
                {
                    situps.Disabled = false;
                    situps_min.Disabled = true;
                    situps_sec.Disabled = true;
                    situps.Value = dt.Rows[0]["sit_ups"].ToString();
                }                
            }

            if (dt.Rows[0]["memo"].ToString().Substring(1, 1) == "0")
            {
                pushups.Disabled = true;
                pushups_min.Disabled = true;
                pushups_sec.Disabled = true;
                pushups.Value = dt.Rows[0]["push_ups"].ToString();
            }
            else
            {
                UnitPer _pushups = ConvertUnit(dt.Rows[0]["memo"].ToString().Substring(1, 1));
                if (_pushups == UnitPer.Second)
                {
                    pushups_min.Disabled = false;
                    pushups_sec.Disabled = false;
                    pushups.Disabled = true;
                    if (dt.Rows[0]["push_ups"] != DBNull.Value)
                    {
                        int min = Convert.ToInt32(dt.Rows[0]["push_ups"]) / 60;
                        int sec = Convert.ToInt32(dt.Rows[0]["push_ups"]) % 60;
                        pushups_min.Value = min.ToString();
                        pushups_sec.Value = sec.ToString();
                    }
                }
                else
                {
                    pushups_min.Disabled = true;
                    pushups_sec.Disabled = true;
                    pushups.Disabled = false;
                    pushups.Value = dt.Rows[0]["push_ups"].ToString();
                }
            }

            if (dt.Rows[0]["memo"].ToString().Substring(2, 1) == "0")
            {
                run.Disabled = true;
                run_min.Disabled = true;
                run_sec.Disabled = true;
                if (dt.Rows[0]["run"] != DBNull.Value)
                {
                    int min = Convert.ToInt32(dt.Rows[0]["run"]) / 60;
                    int sec = Convert.ToInt32(dt.Rows[0]["run"]) % 60;
                    run_min.Value = min.ToString();
                    run_sec.Value = sec.ToString();
                }
            }
            else
            {
                UnitPer _run = ConvertUnit(dt.Rows[0]["memo"].ToString().Substring(2, 1));
                if (_run == UnitPer.Second)
                {                   
                    run_min.Disabled = false;
                    run_sec.Disabled = false;
                    run.Disabled = true;
                    if (dt.Rows[0]["run"] != DBNull.Value)
                    {
                        int min = Convert.ToInt32(dt.Rows[0]["run"]) / 60;
                        int sec = Convert.ToInt32(dt.Rows[0]["run"]) % 60;
                        run_min.Value = min.ToString();
                        run_sec.Value = sec.ToString();
                    }
                }
                else
                {
                    run_min.Disabled = true;
                    run_sec.Disabled = true;
                    run.Disabled = false;
                    run.Value = dt.Rows[0]["run"].ToString();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('沒有資料');", true);
        }
    }

    private UnitPer ConvertUnit(string ItemId)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        d.Add("sid", ItemId);
        dt = du.getDataTableByText("select note from RepMent where sid = @sid", d);
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][0].ToString() == "次")
            {
                return UnitPer.Times;            
            }
            else
            {
                return UnitPer.Second;
            }
        }
        
        return UnitPer.Second;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (id.Value != "" && id.Value == checkid.Value)
        {
            if (sit_ups_name.Text == "200公尺游泳")
            {
                if (situps.Value == "0" || situps.Value == "1")
                    SaveScore();
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有缺漏!!!');", true);
            }
            else if (push_ups_name.Text == "200公尺游泳")
            {
                if (pushups.Value == "0" || pushups.Value == "1")
                    SaveScore();
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有缺漏!!!');", true);
            }
            else if (run_name.Text == "200公尺游泳")
            {
                if (run.Value == "0" || run.Value == "1")
                    SaveScore();
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有缺漏!!!');", true);
            }
            else
            {
                SaveScore();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有缺漏!!!');", true);
        }
    }

    public void SaveScore()
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        try
        {
            if (!situps.Disabled)
            {
                if (situps.Value == "")
                    d.Add("sit_ups", DBNull.Value);
                else
                    d.Add("sit_ups", Convert.ToInt32(situps.Value));
            }
            else
            { 
                int total = 0;
                if (situps.Value == "" && situps_min.Value == "" && situps_sec.Value == "")
                {
                    d.Add("sit_ups", DBNull.Value);
                }
                else if (situps.Value != "")
                {
                    d.Add("sit_ups", situps.Value);                    
                }
                else
                {
                    if (situps_min.Value != "")
                        total += Convert.ToInt32(situps_min.Value) * 60;

                    if (situps_sec.Value != "")
                        total += Convert.ToInt32(situps_sec.Value);

                    if (total != 0)
                        d.Add("sit_ups", total);
                    else
                        d.Add("sit_ups", DBNull.Value);
                }
            }

            if (!pushups.Disabled)
            {
                if (pushups.Value == "")
                    d.Add("push_ups", DBNull.Value);
                else
                    d.Add("push_ups", Convert.ToInt32(pushups.Value));
            }
            else
            {
                int total = 0;
                if (pushups.Value == "" && pushups_min.Value == "" && pushups_sec.Value == "")
                {
                    d.Add("push_ups", DBNull.Value);
                }
                else if (pushups.Value != "")
                {
                    d.Add("push_ups", pushups.Value);                    
                }
                else
                {
                    if (pushups_min.Value != "")
                        total += Convert.ToInt32(pushups_min.Value) * 60;

                    if (pushups_sec.Value != "")
                        total += Convert.ToInt32(pushups_sec.Value);

                    if (total != 0)
                        d.Add("push_ups", total);
                    else
                        d.Add("push_ups", DBNull.Value);
                }

            }

            if (!run.Disabled)
            {
                if (run.Value == "")
                    d.Add("run", DBNull.Value);
                else
                    d.Add("run", Convert.ToInt32(run.Value));
            }
            else
            {
                int total = 0;
                if (run.Value == "" && run_min.Value == "" && run_sec.Value == "")
                {
                    d.Add("run", DBNull.Value);
                }
                else if (run.Value != "")
                {
                    d.Add("run", run.Value);                   
                }
                else
                {
                    if (run_min.Value != "")
                        total += Convert.ToInt32(run_min.Value) * 60;

                    if (run_sec.Value != "")
                        total += Convert.ToInt32(run_sec.Value);

                    if (total != 0)
                        d.Add("run", total);
                    else
                        d.Add("run", DBNull.Value);
                }
            }

            d.Add("id", id.Value);
            du.executeNonQueryByText("update result set sit_ups = @sit_ups, push_ups = @push_ups, run=@run where id = @id and status in ('001','103','102') and (memo != '000' or memo != '999')", d);
            Account_c acc = (Account_c)Session["account"];
            Lib.SysSetting.AddLog("替代方案成績輸入", acc.Account, @"輸入對象: (" + id.Value + ") 輸入成績為(" + sit_ups_name.Text + ": " + situps.Value + "次, " + situps_min.Value + "分:" + situps_sec.Value + "秒" + push_ups_name.Text + ": " + pushups.Value + "次, " + pushups_min.Value + "分:" + pushups_sec.Value + "秒" + run_name.Text + ": " + run.Value + "次," + run_min.Value + "分:" + run_sec.Value + "秒)", DateTime.Now);
            
            if (status.Value == "102" || status.Value == "103")
            {   //status = 合格或不合格 , 需要再做成績補正
                d.Clear();
                d.Add("id", id.Value);
                du.executeNonQueryBysp("Race_CalResult", d);
            }
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('替代方案成績輸入完成!!');", true);
            id.Value = "";
            situps.Value = "";
            situps_min.Value = "";
            situps_sec.Value = "";
            pushups.Value = "";
            pushups_min.Value = "";
            pushups_sec.Value = "";
            run.Value = "";
            run_min.Value = "";
            run_sec.Value = "";
            name.Value = "";

        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, "Race_ReplaceItemScore.aspx");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
