using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;


public partial class ResultCorrect : System.Web.UI.Page
{
    public static string old_situps = string.Empty;
    public static string old_situps_score = string.Empty;
    public static string old_pushups = string.Empty;
    public static string old_pushups_score = string.Empty;
    public static string old_run = string.Empty;
    public static string old_run_score = string.Empty;
    public static string new_sit_ups = string.Empty;
    public static string new_push_ups = string.Empty;
    public static string new_run = string.Empty;
    public static string sit_note = string.Empty;
    public static string push_note = string.Empty;
    public static string run_note = string.Empty;
    public static string sit_value = string.Empty;
    public static string push_value = string.Empty;
    public static string run_value = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        txb_sit1.Attributes.Add("onfocus", "SetSelected_sit1();");
        txb_sit2.Attributes.Add("onfocus", "SetSelected_sit2();");
        txb_push1.Attributes.Add("onfocus", "SetSelected_push1();");
        txb_push2.Attributes.Add("onfocus", "SetSelected_push2();");
        txb_run1.Attributes.Add("onfocus", "SetSelected_run1();");
        txb_run2.Attributes.Add("onfocus", "SetSelected_run2();");
        if(!Page.IsPostBack)
        txb_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Clear();
        DateTime date = DateTime.Today;
        d.Add("id", id1.Value.ToUpper());
        d.Add("date", date);
        DataTable dt = du.getDataTableBysp("GetResultCorrect", d);
        if (dt.Rows.Count == 1)
        {
            //2016-1-23修改查詢清空前一筆記錄
            sit_note = string.Empty;
            push_note = string.Empty;
            run_note = string.Empty;
            sit_value = string.Empty;
            push_value = string.Empty;
            run_value = string.Empty;
            //
            txb_sit1.Text = null;
            txb_sit2.Text = null;
            txb_push1.Text = null;
            txb_push2.Text = null;
            txb_run1.Text = null;
            txb_run2.Text = null;
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
            checkid.Value = id1.Value;
            //取得三項成績的次數或秒數
            if (!string.IsNullOrEmpty(dt.Rows[0]["sit_ups"].ToString()))
            {
                sit_value = dt.Rows[0]["sit_ups"].ToString();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["push_ups"].ToString()))
            {
                push_value = dt.Rows[0]["push_ups"].ToString();
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["run"].ToString()))
            {
                run_value = dt.Rows[0]["run"].ToString();
            }

            //取得三項使用的單位
            sit_note = dt.Rows[0]["sit_ups_note"].ToString();
            push_note = dt.Rows[0]["push_ups_note"].ToString();
            run_note = dt.Rows[0]["run_note"].ToString();
            if (sit_note == "秒")
            {
                txb_sit1.Visible = true;
                txb_sit2.Visible = true;
                lab_sit1.Visible = true;
                lab_sit2.Visible = true;
                lab_sit1.Text = "分";
                lab_sit2.Text = "秒";
                if (!string.IsNullOrEmpty(sit_value))
                    txb_sit1.Text = (Convert.ToInt32(sit_value) / 60).ToString();
                if (!string.IsNullOrEmpty(sit_value))
                    txb_sit2.Text = (Convert.ToInt32(sit_value) % 60).ToString();

            }
            else
            {
                txb_sit1.Visible = false;
                txb_sit2.Visible = true;
                lab_sit1.Visible = false;
                lab_sit2.Visible = true;
                lab_sit1.Text = "";
                lab_sit2.Text = "次";
                if (!string.IsNullOrEmpty(sit_value))
                    txb_sit2.Text = sit_value.ToString();
            }
            if (push_note == "秒")
            {
                txb_push1.Visible = true;
                txb_push2.Visible = true;
                lab_push1.Visible = true;
                lab_push2.Visible = true;
                lab_push1.Text = "分";
                lab_push2.Text = "秒";
                if (!string.IsNullOrEmpty(push_value))
                    txb_push1.Text = (Convert.ToInt32(push_value) / 60).ToString();
                if (!string.IsNullOrEmpty(push_value))
                    txb_push2.Text = (Convert.ToInt32(push_value) % 60).ToString();
            }
            else
            {
                txb_push1.Visible = false;
                txb_push2.Visible = true;
                lab_push1.Visible = false;
                lab_push2.Visible = true;
                lab_push1.Text = "";
                lab_push2.Text = "次";
                if (!string.IsNullOrEmpty(push_value))
                    txb_push2.Text = push_value.ToString();
            }
            if (run_note == "秒")
            {
                txb_run1.Visible = true;
                txb_run2.Visible = true;
                lab_run1.Visible = true;
                lab_run2.Visible = true;
                lab_run1.Text = "分";
                lab_run2.Text = "秒";
                if (!string.IsNullOrEmpty(run_value))
                    txb_run1.Text = (Convert.ToInt32(run_value) / 60).ToString();
                if (!string.IsNullOrEmpty(run_value))
                    txb_run2.Text = (Convert.ToInt32(run_value) % 60).ToString();
            }
            else
            {
                txb_run1.Visible = false;
                txb_run2.Visible = true;
                lab_run1.Visible = false;
                lab_run2.Visible = true;
                lab_run1.Text = "";
                lab_run2.Text = "次";
                if (!string.IsNullOrEmpty(run_value))
                    txb_run2.Text = run_value.ToString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('沒有資料');", true);
            //清空欄位
            name.Value = null;
            txb_sit1.Text = null;
            txb_sit2.Text = null;
            txb_push1.Text = null;
            txb_push2.Text = null;
            txb_run1.Text = null;
            txb_run2.Text = null;
            sit_note = string.Empty;
            push_note = string.Empty;
            run_note = string.Empty;
            sit_value = string.Empty;
            push_value = string.Empty;
            run_value = string.Empty;

            //2016-1-25修改完後欄位全關閉
            sit_ups_name.Text = null;
            txb_sit1.Visible = false;
            txb_sit2.Visible = false;
            lab_sit1.Text = null;
            lab_sit2.Text = null;
            push_ups_name.Text = null;
            txb_push1.Visible = false;
            txb_push2.Visible = false;
            lab_push1.Text = null;
            lab_push2.Text = null;
            run_name.Text = null;
            txb_run1.Visible = false;
            txb_run2.Visible = false;
            lab_run1.Text = null;
            lab_run2.Text = null;
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (check_num.Value == "1")//前台傳回來的值(1：正確、0：錯誤)
        {
            string t = old_run_score;
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Clear();
            //判斷身份證字號是否正確
            if (id1.Value != "" && id1.Value == checkid.Value)
            {
                //判斷第一項是用次數還是秒
                //使用分秒
                if (sit_note == "秒")
                {
                    //2016-1-14新判斷
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60) + Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60);
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("sit_ups", DBNull.Value);
                        new_sit_ups = string.Empty;
                    }
                }
                //使用次數
                else
                {
                    //2016-1-26再修正值不等於"秒"，也是判斷二個欄位來相加
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60) + Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60);
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        new_sit_ups = sit_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("sit_ups", DBNull.Value);
                        new_sit_ups = string.Empty;
                    }
                }

                //判斷第二項是用次數還是秒
                //使用分秒
                if (push_note == "秒")
                {
                    //2016-1-14新判斷
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60) + Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60);
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("push_ups", DBNull.Value);
                        new_push_ups = string.Empty;
                    }
                }
                //使用次數
                else
                {
                    if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60) + Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60);
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        new_push_ups = push_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("push_ups", DBNull.Value);
                        new_push_ups = string.Empty;
                    }

                }

                //判斷第三項是用次數還是秒
                //使用分秒
                if (run_note == "秒")
                {
                    //2016-1-14新判斷
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {

                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60) + Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60);
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("run", DBNull.Value);
                        new_run = string.Empty;
                    }
                }
                //使用次數
                else
                {
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60) + Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60);
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = Convert.ToInt32(txb_run2.Text);
                        d.Add("run", run_sec);
                        new_run = run_sec.ToString();
                    }
                    //都空白
                    else
                    {
                        d.Add("run", DBNull.Value);
                        new_run = string.Empty;
                    }

                }

                d.Add("id", id1.Value.ToUpper());
                d.Add("date", Convert.ToDateTime(dateValue.Value));
                try
                {
                    du.executeNonQueryByText("update result set sit_ups = @sit_ups, push_ups = @push_ups, run=@run where id = @id and date = @date and status in ('102','103','105')", d);

                    //2017-1-26再重新計算一次成績
                    Dictionary<string, object> d1 = new Dictionary<string, object>();
                    d1.Add("id", id1.Value.ToUpper());
                    d1.Add("date", Convert.ToDateTime(dateValue.Value));
                    //2018-10-11新增異動時間點
                    DateTime ChangeDate = new DateTime(2018, 12, 31, 23, 59, 59);
                    DateTime TodayDate = Convert.ToDateTime(dateValue.Value);
                    if (TodayDate > ChangeDate)
                    {
                        du.executeNonQueryBysp("Ex108_CalResultByID", d1);
                    }
                    else
                    {
                        du.executeNonQueryBysp("Ex106_CalResultByID", d1);
                    }

                    d1.Clear();

                    Account_c acc = (Account_c)Session["account"];
                    //Lib.SysSetting.AddLog("成績補正", acc.Account, "補正對象: (" + id.Value + ") 補正前成績為(" + sit_ups_name.Text + ": " + old_situps + "次/秒, " + push_ups_name.Text + ": " + old_pushups + "次/秒, " + run_name.Text + ": " + old_run + "次/秒)" + "補正後成績為(" + sit_ups_name.Text + ": " + sit_value + "次/秒, " + push_ups_name.Text + ": " + push_value + "次/秒, " + run_name.Text + ": " + run_value + "次/秒)", DateTime.Now);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("id", id1.Value.ToUpper());
                    dic.Add("name", name.Value);
                    dic.Add("date", Convert.ToDateTime(dateValue.Value));
                    if (!string.IsNullOrEmpty(old_situps))
                        dic.Add("old_sit_ups", old_situps);
                    else
                        dic.Add("old_sit_ups", DBNull.Value);
                    if (!string.IsNullOrEmpty(old_pushups))
                        dic.Add("old_push_ups", old_pushups);
                    else
                        dic.Add("old_push_ups", DBNull.Value);
                    if (!string.IsNullOrEmpty(old_run))
                        dic.Add("old_run", old_run);
                    else
                        dic.Add("old_run", DBNull.Value);
                    if (!string.IsNullOrEmpty(new_sit_ups))
                        dic.Add("new_sit_ups", new_sit_ups);
                    else
                        dic.Add("new_sit_ups", DBNull.Value);
                    if (!string.IsNullOrEmpty(new_push_ups))
                        dic.Add("new_push_ups", new_push_ups);
                    else
                        dic.Add("new_push_ups", DBNull.Value);
                    if (!string.IsNullOrEmpty(new_run))
                        dic.Add("new_run", new_run);
                    else
                        dic.Add("new_run", DBNull.Value);
                    dic.Add("account", acc.Account);
                    if (!string.IsNullOrEmpty(acc.ID))
                        dic.Add("account_id", acc.ID);
                    else
                        dic.Add("account_id", DBNull.Value);
                    dic.Add("update_time", DateTime.Now);
                    dic.Add("type", "成績補正");

                    Lib.SysSetting.AddResultCorrectLog(dic);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('成績補正完成!!');", true);
                    id1.Value = "";
                    //situps.Value = "";
                    //pushups.Value = "";
                    //run.Value = "";
                    name.Value = "";
                    //清空欄位
                    txb_sit1.Text = null;
                    txb_sit2.Text = null;
                    txb_push1.Text = null;
                    txb_push2.Text = null;
                    txb_run1.Text = null;
                    txb_run2.Text = null;
                    sit_note = string.Empty;
                    push_note = string.Empty;
                    run_note = string.Empty;
                    sit_value = string.Empty;
                    push_value = string.Empty;
                    run_value = string.Empty;
                    new_sit_ups = string.Empty;
                    new_push_ups = string.Empty;
                    new_run = string.Empty;
                    //2016-1-25修改完後欄位全關閉
                    sit_ups_name.Text = null;
                    txb_sit1.Visible = false;
                    txb_sit2.Visible = false;
                    lab_sit1.Text = null;
                    lab_sit2.Text = null;
                    push_ups_name.Text = null;
                    txb_push1.Visible = false;
                    txb_push2.Visible = false;
                    lab_push1.Text = null;
                    lab_push2.Text = null;
                    run_name.Text = null;
                    txb_run1.Visible = false;
                    txb_run2.Visible = false;
                    lab_run1.Text = null;
                    lab_run2.Text = null;
                }
                catch (Exception ex)
                {
                    Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + ex.Message.ToString() + "');", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料有誤!!請重新執行搜尋!!');", true);
                //清空欄位
                name.Value = null;
                txb_sit1.Text = null;
                txb_sit2.Text = null;
                txb_push1.Text = null;
                txb_push2.Text = null;
                txb_run1.Text = null;
                txb_run2.Text = null;
                sit_note = string.Empty;
                push_note = string.Empty;
                run_note = string.Empty;
                sit_value = string.Empty;
                push_value = string.Empty;
                run_value = string.Empty;

                //2016-1-25修改完後欄位全關閉
                sit_ups_name.Text = null;
                txb_sit1.Visible = false;
                txb_sit2.Visible = false;
                lab_sit1.Text = null;
                lab_sit2.Text = null;
                push_ups_name.Text = null;
                txb_push1.Visible = false;
                txb_push2.Visible = false;
                lab_push1.Text = null;
                lab_push2.Text = null;
                run_name.Text = null;
                txb_run1.Visible = false;
                txb_run2.Visible = false;
                lab_run1.Text = null;
                lab_run2.Text = null;
            }
        }

    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }


    protected void btn_InqDate_Click(object sender, EventArgs e)
    {
        try
        {
            this.datenone.Style.Value = "display:none";
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("date", txb_Date.Text.Trim());

            DataTable dt = du.getDataTableBysp("Ex108_GetResultCorrectLog", d);

            if (dt.Rows.Count > 0)
                GridView1.DataSource = dt;
            else if (dt.Rows.Count == 0)
                this.datenone.Style.Value = "";
            else
                this.datenone.Style.Value = "display:none";
            GridView1.DataBind();
            TabContainer1.ActiveTabIndex = 1;
        }
        catch(Exception ex)
        {
            this.datenone.Style.Value = "日期格式錯誤";
            GridView1.DataBind();
            TabContainer1.ActiveTabIndex = 1;
        }
        
    }

    
}
