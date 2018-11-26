using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib.Center;

public partial class ReplaceItemScore : System.Web.UI.Page
{
    public static string sit_note = string.Empty;
    public static string push_note = string.Empty;
    public static string run_note = string.Empty;
    public static string sit_value = string.Empty;
    public static string push_value = string.Empty;
    public static string run_value = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //2016-1-23修改查詢清空前一筆記錄
        sit_note = string.Empty;
        push_note = string.Empty;
        run_note = string.Empty;
        sit_value = string.Empty;
        push_value = string.Empty;
        run_value = string.Empty;
        //
        //2016-1-12新增
        txb_sit1.Text = null;
        txb_sit2.Text = null;
        txb_push1.Text = null;
        txb_push2.Text = null;
        txb_run1.Text = null;
        txb_run2.Text = null;
        //
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Clear();
        DateTime date = DateTime.Today;
        d.Add("id", id.Value);
        d.Add("date", date);
        //2016-1-12修改sp，新增傳回次、秒、(合格:1/不合格:0)
        DataTable dt = du.getDataTableBysp("GetReplaceItemScore", d);
        if (dt.Rows.Count == 1)
        {
            Button2.Visible = true;
            name.Value = dt.Rows[0]["name"].ToString();
            checkid.Value = id.Value;
            sit_ups_name.Text = dt.Rows[0]["sit_ups_name"].ToString();
            push_ups_name.Text = dt.Rows[0]["push_ups_name"].ToString();
            run_name.Text = dt.Rows[0]["run_name"].ToString();
            //situps.Value = dt.Rows[0]["sit_ups"].ToString();
            //pushups.Value = dt.Rows[0]["push_ups"].ToString();
            //run.Value = dt.Rows[0]["run"].ToString();
            dateValue.Value = Convert.ToDateTime(dt.Rows[0]["date"].ToString()).ToShortDateString();
            //判斷是否為替代項目，不是的話不能修改
            if (dt.Rows[0]["memo"].ToString().Substring(0, 1) == "0")
            {
                txb_sit1.Enabled = false;
                txb_sit2.Enabled = false;

                //situps.Disabled = true;
            }
            else
            {
                txb_sit1.Enabled = true;
                txb_sit2.Enabled = true;

                //situps.Disabled = false;
            }

            if (dt.Rows[0]["memo"].ToString().Substring(1, 1) == "0")
            {
                txb_push1.Enabled = false;
                txb_push2.Enabled = false;

                //pushups.Disabled = true;
            }
            else
            {
                txb_push1.Enabled = true;
                txb_push2.Enabled = true;

                //pushups.Disabled = false;
            }

            if (dt.Rows[0]["memo"].ToString().Substring(2, 1) == "0" | dt.Rows[0]["memo"].ToString().Substring(2, 1) == "1")
            {
                txb_run1.Enabled = false;
                txb_run2.Enabled = false;

                //run.Disabled = true;
            }
            else
            {
                txb_run1.Enabled = true;
                txb_run2.Enabled = true;

                //run.Disabled = false;
            }

            //2016-1-12新增
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
            else if (sit_note == "(合格:1/不合格:0)")
            {
                txb_sit1.Visible = false;
                txb_sit2.Visible = true;
                lab_sit1.Visible = false;
                lab_sit2.Visible = true;
                lab_sit1.Text = "";
                lab_sit2.Text = "(合格:1/不合格:0)";
                if (!string.IsNullOrEmpty(sit_value))
                    txb_sit2.Text = sit_value.ToString();
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
            else if (push_note == "(合格:1/不合格:0)")
            {
                txb_push1.Visible = false;
                txb_push2.Visible = true;
                lab_push1.Visible = false;
                lab_push2.Visible = true;
                lab_push1.Text = "";
                lab_push2.Text = "(合格:1/不合格:0)";
                if (!string.IsNullOrEmpty(push_value))
                    txb_push2.Text = push_value.ToString();
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
            else if (run_note == "(合格:1/不合格:0)")
            {
                txb_run1.Visible = false;
                txb_run2.Visible = true;
                lab_run1.Visible = false;
                lab_run2.Visible = true;
                lab_run1.Text = "";
                lab_run2.Text = "(合格:1/不合格:0)";
                if (!string.IsNullOrEmpty(run_value))
                    txb_run2.Text = run_value.ToString();
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
            Button2.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('查無資料');", true);
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
            Button2.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (check_num.Value == "1")//前台傳回來的值(1：正確、0：錯誤)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Clear();
            //判斷身份證字號是否正確
            if (id.Value != "" && id.Value == checkid.Value)
            {
                //2016新增
                //判斷第一項是用次數還是秒
                //使用分秒
                if (sit_note == "秒")
                {
                    //2016-1-14新判斷
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_sit1.Text.Trim()) < 60 & Convert.ToInt32(txb_sit2.Text.Trim()) < 60 & Convert.ToInt32(txb_sit1.Text.Trim()) >= 0 & Convert.ToInt32(txb_sit2.Text.Trim()) >= 0)
                        //{
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60) + Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」.「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_sit1.Text.Trim()) < 60 & Convert.ToInt32(txb_sit1.Text.Trim()) >= 0)
                        //{
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60);
                        d.Add("sit_ups", sit_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_sit2.Text.Trim()) < 60 & Convert.ToInt32(txb_sit2.Text.Trim()) >= 0)
                        //{
                        int sit_sec = Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //都空白
                    else
                    {
                        d.Add("sit_ups", DBNull.Value);
                    }
                   
                }
                //200公尺游泳使用
                else if (sit_note == "(合格:1/不合格:0)")
                {
                    //判斷有沒有輸入值，沒有的話給null
                    if (!string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    //判斷是否值為0或1
                    {
                        if (txb_sit2.Text.Trim() == "0" || txb_sit2.Text.Trim() == "1")
                        {
                            d.Add("sit_ups", txb_sit2.Text.Trim());
                        }
                        else
                        {
                            //d.Add("sit_ups", DBNull.Value);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「200公尺游泳」項目請輸入正確格式(合格:1/不合格:0)');", true);
                            return;
                        }
                    }
                    else
                    {
                        d.Add("sit_ups", DBNull.Value);
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
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_sit1.Text.Trim()) & string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = (Convert.ToInt32(txb_sit1.Text.Trim()) * 60);
                        d.Add("sit_ups", sit_sec);
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_sit1.Text.Trim()) & !string.IsNullOrEmpty(txb_sit2.Text.Trim()))
                    {
                        int sit_sec = Convert.ToInt32(txb_sit2.Text.Trim());
                        d.Add("sit_ups", sit_sec);
                    }
                    //都空白
                    else
                    {
                        d.Add("sit_ups", DBNull.Value);
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
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_push1.Text.Trim()) < 60 & Convert.ToInt32(txb_push2.Text.Trim()) < 60 & Convert.ToInt32(txb_push1.Text.Trim()) >= 0 & Convert.ToInt32(txb_push2.Text.Trim()) >= 0)
                        //{
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60) + Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」.「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_push1.Text.Trim()) < 60 & Convert.ToInt32(txb_push1.Text.Trim()) >= 0)
                        //{
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60);
                        d.Add("push_ups", push_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_push2.Text.Trim()) < 60 & Convert.ToInt32(txb_push2.Text.Trim()) >= 0)
                        //{
                        int push_sec = Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //都空白
                    else
                    {
                        d.Add("push_ups", DBNull.Value);
                    }
                }
                //200公尺游泳使用
                else if (push_note == "(合格:1/不合格:0)")
                {
                    //判斷有沒有輸入值，沒有的話給null
                    if (!string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    //判斷是否值為0或1
                    {
                        if (txb_push2.Text.Trim() == "0" || txb_push2.Text.Trim() == "1")
                        {
                            d.Add("push_ups", txb_push2.Text.Trim());
                        }
                        else
                        {
                            //d.Add("push_ups", DBNull.Value);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「200公尺游泳」項目請輸入正確格式(合格:1/不合格:0)');", true);
                            return;
                        }
                    }
                    else
                    {
                        d.Add("push_ups", DBNull.Value);
                    }
                }
                //使用次數
                else
                {
                    //2016-1-26再修正值不等於"秒"，也是判斷二個欄位來相加
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60) + Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_push1.Text.Trim()) & string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = (Convert.ToInt32(txb_push1.Text.Trim()) * 60);
                        d.Add("push_ups", push_sec);
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_push1.Text.Trim()) & !string.IsNullOrEmpty(txb_push2.Text.Trim()))
                    {
                        int push_sec = Convert.ToInt32(txb_push2.Text.Trim());
                        d.Add("push_ups", push_sec);
                    }
                    //都空白
                    else
                    {
                        d.Add("push_ups", DBNull.Value);
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
                        ////再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_run1.Text.Trim()) < 60 & Convert.ToInt32(txb_run2.Text.Trim()) < 60 & Convert.ToInt32(txb_run1.Text.Trim()) >= 0 & Convert.ToInt32(txb_run2.Text.Trim()) >= 0)
                        //{
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60) + Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」.「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_run1.Text.Trim()) < 60 & Convert.ToInt32(txb_run1.Text.Trim()) >= 0)
                        //{
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60);
                        d.Add("run", run_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「分」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        //再判斷欄位是不是小於60(拿掉)
                        //if (Convert.ToInt32(txb_run2.Text.Trim()) < 60 & Convert.ToInt32(txb_run2.Text.Trim()) >= 0)
                        //{
                        int run_sec = Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「秒」欄位輸入範圍值應為：0-59');", true);
                        //    return;
                        //}

                    }
                    //都空白
                    else
                    {
                        d.Add("run", DBNull.Value);
                    }
                }
                //200公尺游泳使用
                else if (run_note == "(合格:1/不合格:0)")
                {
                    //判斷有沒有輸入值，沒有的話給null
                    if (!string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        //判斷是否值為0或1
                        if (txb_run2.Text.Trim() == "0" || txb_run2.Text.Trim() == "1")
                        {
                            d.Add("run", txb_run2.Text.Trim());
                        }
                        else
                        {
                            //d.Add("run", DBNull.Value);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「200公尺游泳」項目請輸入正確格式(合格:1/不合格:0)');", true);
                            return;

                        }
                    }
                    else
                    {
                        d.Add("run", DBNull.Value);
                    }

                }
                //使用次數
                else
                {
                    //2016-1-26再修正值不等於"秒"，也是判斷二個欄位來相加
                    //分、秒都有值
                    if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60) + Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                    }
                    //分有值、秒空白
                    else if (!string.IsNullOrEmpty(txb_run1.Text.Trim()) & string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = (Convert.ToInt32(txb_run1.Text.Trim()) * 60);
                        d.Add("run", run_sec);
                    }
                    //分空白、秒有值
                    else if (string.IsNullOrEmpty(txb_run1.Text.Trim()) & !string.IsNullOrEmpty(txb_run2.Text.Trim()))
                    {
                        int run_sec = Convert.ToInt32(txb_run2.Text.Trim());
                        d.Add("run", run_sec);
                    }
                    //都空白
                    else
                    {
                        d.Add("run", DBNull.Value);
                    }
                }

                //加入舊版回寫資料庫跟產生log
                d.Add("id", id.Value);
                d.Add("date", Convert.ToDateTime(dateValue.Value));
                try
                {
                    du.executeNonQueryByText("update result set sit_ups = @sit_ups, push_ups = @push_ups, run=@run where id = @id and date = @date and status in ('001') and (memo != '000' or memo != '999')", d);
                    Account_c acc = (Account_c)Session["account"];
                    Lib.SysSetting.AddLog("替代方案成績輸入", acc.Account, "輸入對象: (" + id.Value + ") 輸入成績為(" + sit_ups_name.Text.Trim() + ": " + sit_value + ", " + push_ups_name.Text.Trim() + ": " + push_value + ", " + run_name.Text.Trim() + ": " + run_value + ")", DateTime.Now);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('替代方案成績輸入完成!!');", true);
                    id.Value = "";
                    name.Value = "";

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
                    Button2.Visible = false;
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
    public void StopMessage()
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('「200公尺游泳」項目請輸入正確格式(合格:1/不合格:0)');", true);
    }
   
    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
