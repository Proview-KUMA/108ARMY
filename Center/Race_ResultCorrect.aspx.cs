using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class Race_ResultCorrect : System.Web.UI.Page
{
    enum UnitPer
    {
        Times,
        Second
    }
    public static string old_situps = "";
    public static string old_situps_score = "";
    public static string old_pushups = "";
    public static string old_pushups_score = "";
    public static string old_run = "";
    public static string old_run_score = "";
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
        //d.Add("date", date);
        DataTable dt = du.getDataTableBysp("Race_GetResultCorrect", d);
        if (dt.Rows.Count == 1)
        {
            name.Value = dt.Rows[0]["name"].ToString();
            //situps.Value = dt.Rows[0]["sit_ups"].ToString();
            //pushups.Value = dt.Rows[0]["push_ups"].ToString();
            //run.Value = dt.Rows[0]["run"].ToString();
            old_situps = dt.Rows[0]["sit_ups"].ToString();
            old_pushups = dt.Rows[0]["push_ups"].ToString();
            old_run = dt.Rows[0]["run"].ToString();
            sit_ups_name.Text = dt.Rows[0]["sit_ups_name"].ToString();
            push_ups_name.Text = dt.Rows[0]["push_ups_name"].ToString();
            run_name.Text = dt.Rows[0]["run_name"].ToString();
            dateValue.Value = Convert.ToDateTime(dt.Rows[0]["date"].ToString()).ToShortDateString();
            status.Value = dt.Rows[0]["status"].ToString();
            checkid.Value = id.Value;
            if (dt.Rows[0]["memo"].ToString().Substring(0, 1) == "0")
            {
                situps.Disabled = false;
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
                pushups.Disabled = false;
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
                run_min.Disabled = false;
                run_sec.Disabled = false;
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (id.Value != "" && situps.Value != "" && pushups.Value != "" && run.Value != "" && id.Value == checkid.Value )
        if (id.Value != "" && id.Value == checkid.Value)
        {
            if (status.Value == "001")
            {   //For status 001
                string t = old_run_score;
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                //
                //d.Add("id", string t = (id.Value == "")?id.Value : System.DBNull.Value);
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
                    if (situps_min.Value == "" && situps_sec.Value == "")
                    {
                        d.Add("sit_ups", DBNull.Value);
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
                    if (pushups_min.Value == "" && pushups_sec.Value == "")
                    {
                        d.Add("push_ups", DBNull.Value);
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
                    if (run_min.Value == "" && run_sec.Value == "")
                    {
                        d.Add("run", DBNull.Value);
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
                //d.Add("date", Convert.ToDateTime(dateValue.Value));
                try
                {
                    du.executeNonQueryBysp("Race_ResultCorrect", d);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("成績補正", acc.Account, "補正對象: (" + id.Value + ") 補正前成績為(" + sit_ups_name.Text + ": " + old_situps + "次, " + push_ups_name.Text + ": " + old_pushups + "次, " + run_name.Text + ": " + old_run + "秒)" + "補正後成績為(" + sit_ups_name.Text + ": " + situps.Value + "次, " + situps_min.Value + "分:" + situps_sec.Value + "秒" + push_ups_name.Text + ": " + pushups.Value + "次, " + pushups_min.Value + "分:" + pushups_sec.Value + "秒" + run_name.Text + ": " + run.Value + "次," + run_min.Value + "分:" + run_sec.Value + "秒)", DateTime.Now);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('完成補正成績');", true);
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
                    Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
            else
            {   //For Status 102, 103
                string t = old_run_score;
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                //
                //d.Add("id", string t = (id.Value == "")?id.Value : System.DBNull.Value);
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
                    if (situps_min.Value == "" && situps_sec.Value == "")
                    {
                        d.Add("sit_ups", DBNull.Value);
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
                    if (pushups_min.Value == "" && pushups_sec.Value == "")
                    {
                        d.Add("push_ups", DBNull.Value);
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
                    if (run_min.Value == "" && run_sec.Value == "")
                    {
                        d.Add("run", DBNull.Value);
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
                //d.Add("date", Convert.ToDateTime(dateValue.Value));
                try
                {
                    du.executeNonQueryBysp("Race_ResultCorrect", d);
                    d.Clear();
                    d.Add("id", id.Value);
                    du.executeNonQueryBysp("Race_CalResult", d);
                    Account_c acc = (Account_c)Session["account"];
                    //Lib.SysSetting.AddLog("成績補正", acc.Account, "補正對象: (" + id.Value + ") 補正前成績為(" + sit_ups_name.Text + ": " + old_situps + "次, " + push_ups_name.Text + ": " + old_pushups + "次, " + run_name.Text + ": " + old_run + "秒)" + "補正後成績為(" + sit_ups_name.Text + ": " + situps.Value + "次, " + push_ups_name.Text + ": " + pushups.Value + "次, " + run_name.Text + ": " + run.Value + "秒)", DateTime.Now);
                    Lib.SysSetting.AddLog("成績補正", acc.Account, "補正對象: (" + id.Value + ") 補正前成績為(" + sit_ups_name.Text + ": " + old_situps + "次, " + push_ups_name.Text + ": " + old_pushups + "次, " + run_name.Text + ": " + old_run + "秒)" + "補正後成績為(" + sit_ups_name.Text + ": " + situps.Value + "次, " + situps_min.Value + "分:" + situps_sec.Value + "秒" + push_ups_name.Text + ": " + pushups.Value + "次, " + pushups_min.Value + "分:" + pushups_sec.Value + "秒" + run_name.Text + ": " + run.Value + "次," + run_min.Value + "分:" + run_sec.Value + "秒)", DateTime.Now);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('完成補正成績');", true);
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
                    Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有缺漏!!!');", true);
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

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
