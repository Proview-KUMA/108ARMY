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
                DataTable dt = du.getDataTableByText(@"select status,memo from result where id = @id and date = @date", d);
                if (dt.Rows.Count == 1)
                {
                    string status = string.Empty;
                    string memo1 = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["status"].ToString()))
                        status = dt.Rows[0]["status"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["memo"].ToString()))
                        memo1 = dt.Rows[0]["memo"].ToString();
                    else
                        memo1 = "000";
                    if (!string.IsNullOrEmpty(status) & status != "000" & status != "999" & status.Substring(0,1)!="2")
                    {
                        DropDownList1.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(0, 1);
                        DropDownList2.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(1, 1);
                        DropDownList3.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(2, 1);
                        DropDownList4.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(0, 1);
                        DropDownList5.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(1, 1);
                        DropDownList6.SelectedValue = dt.Rows[0]["memo"].ToString().Substring(2, 1);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員尚未完成檢錄或成績已上傳，不可變更鑑測項目');", true);
                        Re_Select();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('查無今日受測資料');", true);
                    Re_Select();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入身分證字號');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
    public void Re_Select()
    {
        DropDownList1.SelectedValue = "0";
        DropDownList2.SelectedValue = "0";
        DropDownList3.SelectedValue = "0";
        DropDownList4.SelectedValue = "0";
        DropDownList5.SelectedValue = "0";
        DropDownList6.SelectedValue = "0";
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            //string script = @"<script language='JavaScript'>window.location.reload();</script>";

            string memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + DropDownList3.SelectedValue;
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
                            new Lib.DataUtility().executeNonQueryByText("update result set memo = @memo where id = @id and date = @date", d);


                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('設定鑑測項目完畢');", true);
                            TB_id.Text = string.Empty;
                            Re_Select();

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
