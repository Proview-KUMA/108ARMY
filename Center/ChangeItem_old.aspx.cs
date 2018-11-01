using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Lib.Center;

public partial class ChangeItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(TB_id.Text.Trim()))
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                Lib.DataUtility du = new Lib.DataUtility();
                d.Add("id", TB_id.Text.Trim());
                d.Add("date", System.DateTime.Today);
                DataTable dt = du.getDataTableByText(@"select memo,status from result where id = @id and date = @date", d);
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["status"].ToString() == "000" | dt.Rows[0]["status"].ToString() == "999")
                    {
                        DropDownList1.Items.Clear();
                        DropDownList2.Items.Clear();
                        DropDownList3.Items.Clear();
                        DropDownList4.SelectedIndex = -1;
                        DropDownList5.SelectedIndex = -1;
                        DropDownList6.SelectedIndex = -1;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員尚未完成檢錄，無法變更鑑測項目');", true);
                    }
                    else if (dt.Rows[0]["status"].ToString().Substring(0, 1) == "2")
                    {
                        DropDownList1.Items.Clear();
                        DropDownList2.Items.Clear();
                        DropDownList3.Items.Clear();
                        DropDownList4.SelectedIndex = -1;
                        DropDownList5.SelectedIndex = -1;
                        DropDownList6.SelectedIndex = -1;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員成績已上傳，無法變更鑑測項目');", true);
                    }
                    else
                    {
                        DropDownList4.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(0, 1);
                        DropDownList5.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(1, 1);
                        //2016-12-16回覆原版
                        DropDownList6.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(2, 1);
                        ////2016-11-21如果op_id的值是run2400，則第三項判定為1
                        //if (dt.Rows[0]["op_id"].ToString() == "run2400")
                        //    DropDownList6.SelectedValue = "1";
                        //else
                        //    DropDownList6.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(2, 1);

                        DropDownList1.Items.Clear();
                        DropDownList2.Items.Clear();
                        DropDownList3.Items.Clear();
                        d.Clear();
                        if (TB_id.Text.Trim().Substring(1, 1) == "1")
                            d.Add("Gender", "M");
                        else
                            d.Add("Gender", "F");

                        DataTable dt_items_1 = du.getDataTableBysp("GetRepMent", d);
                        DataTable dt_items_2 = du.getDataTableBysp("GetRepMent", d);
                        DataTable dt_items_3 = du.getDataTableBysp("GetRepMent", d);
                        ////2016-11-9把2400公尺選項刪掉
                        //for (int i = 0; i < dt_items_1.Rows.Count; i++)
                        //{
                        //    if(dt_items_1.Rows[i]["sid"].ToString()=="1")
                        //        dt_items_1.Rows.RemoveAt(i);
                        //}

                        DataRow DR_item1 = dt_items_1.NewRow();
                        DR_item1["rep_title"] = "仰臥起坐";
                        DR_item1["sid"] = "0";
                        dt_items_1.Rows.Add(DR_item1);
                        ////2016-11-9把2400公尺選項刪掉
                        //for (int i = 0; i < dt_items_2.Rows.Count; i++)
                        //{
                        //    if (dt_items_2.Rows[i]["sid"].ToString() == "1")
                        //        dt_items_2.Rows.RemoveAt(i);
                        //}

                        DataRow DR_item2 = dt_items_2.NewRow();
                        DR_item2["rep_title"] = "俯地起身";
                        DR_item2["sid"] = "0";
                        dt_items_2.Rows.Add(DR_item2);

                        DataRow DR_item3 = dt_items_3.NewRow();
                        DR_item3["rep_title"] = "三千公尺徒手跑步";
                        DR_item3["sid"] = "0";
                        dt_items_3.Rows.Add(DR_item3);

                        DropDownList1.DataSource = dt_items_1;
                        DropDownList2.DataSource = dt_items_2;
                        DropDownList3.DataSource = dt_items_3;
                        DropDownList1.DataTextField = "rep_title";
                        DropDownList1.DataValueField = "sid";
                        DropDownList2.DataTextField = "rep_title";
                        DropDownList2.DataValueField = "sid";
                        DropDownList3.DataTextField = "rep_title";
                        DropDownList3.DataValueField = "sid";
                        DropDownList1.DataBind();
                        DropDownList2.DataBind();
                        DropDownList3.DataBind();

                        DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;
                        DropDownList2.SelectedIndex = DropDownList2.Items.Count - 1;
                        DropDownList3.SelectedIndex = DropDownList3.Items.Count - 1;
                        DropDownList1.SelectedValue = DropDownList4.SelectedValue;
                        DropDownList2.SelectedValue = DropDownList5.SelectedValue;
                        DropDownList3.SelectedValue = DropDownList6.SelectedValue;
                    }
                    
                    
                }
                else
                {
                    DropDownList1.Items.Clear();
                    DropDownList2.Items.Clear();
                    DropDownList3.Items.Clear();
                    DropDownList4.SelectedIndex = -1;
                    DropDownList5.SelectedIndex = -1;
                    DropDownList6.SelectedIndex = -1;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('查無今日受測資料');", true);                    
                }
            }
            else
            {
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                DropDownList3.Items.Clear();
                DropDownList4.SelectedIndex = -1;
                DropDownList5.SelectedIndex = -1;
                DropDownList6.SelectedIndex = -1;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入身分證字號');", true);                    
            }
        }
        catch (Exception ex)
        {
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList4.SelectedIndex = -1;
            DropDownList5.SelectedIndex = -1;
            DropDownList6.SelectedIndex = -1;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            string script =@"<script language='JavaScript'>window.location.reload();</script>";
            string memo = string.Empty;
            memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + DropDownList3.SelectedValue;
            ////2016-11-21需判斷第三項是否為1(2400公尺)
            ////2016-12-16移除2400公尺
            //string op_id = string.Empty;
            //if (DropDownList3.SelectedValue == "1")
            //{
            //    memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + "0";
            //    op_id = "run2400";
            //}
            //else
            //{
            //    memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + DropDownList3.SelectedValue;
            //    op_id = "0";
            //}
            
            if (DropDownList1.SelectedItem.ToString() != DropDownList2.SelectedItem.ToString() && DropDownList2.SelectedItem.ToString() != DropDownList3.SelectedItem.ToString() && DropDownList1.SelectedItem.ToString() != DropDownList3.SelectedItem.ToString())
            {
                if (TB_id.Text != "")
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    Lib.DataUtility du = new Lib.DataUtility();
                    d.Add("id", TB_id.Text.Trim());
                    d.Add("date", System.DateTime.Today);
                    DataTable dt = du.getDataTableByText(@"select status, memo from result where id = @id and date = @date", d);
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["status"].ToString().Substring(0, 1) != "2")
                        {
                            Account_c acc = (Account_c)Session["account"];
                            Lib.SysSetting.AddLog("設定鑑測項目", acc.Account, @"設定對象 : " + TB_id.Text.Trim() + "原鑑測項目 : " + dt.Rows[0]["memo"].ToString() + " 新鑑測項目 : " + memo, DateTime.Now);
                            d.Add("memo", memo);
                            //d.Add("op_id", op_id);
                            new Lib.DataUtility().executeNonQueryByText("update result set memo = @memo where id = @id and date = @date", d);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('設定鑑測項目完畢');", true);
                            TB_id.Text = string.Empty;
                            DropDownList1.SelectedValue = "0";
                            DropDownList2.SelectedValue = "0";
                            DropDownList3.SelectedValue = "0";
                            DropDownList4.SelectedValue = "0";
                            DropDownList5.SelectedValue = "0";
                            DropDownList6.SelectedValue = "0";

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('此人員受測資料已上傳，不可變更鑑測項目');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('查無今日受測資料');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入身分證字號');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('鑑測項目不可重覆 , 請重設鑑測項目');", true);       
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
}
