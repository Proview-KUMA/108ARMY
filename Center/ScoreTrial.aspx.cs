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
using Lib;
using System.Collections.Generic;

public partial class ScoreTrial : System.Web.UI.Page
{
    public DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                DropDownList2.Items.Clear();
                for (int i = 18; i < 99; i++)
                    DropDownList2.Items.Add(new ListItem(i.ToString(), i.ToString()));

                if (DropDownList1.SelectedItem.ToString() == "男性")
                    SqlDataSource1.SelectParameters["Gender"].DefaultValue = "M";
                else
                    SqlDataSource1.SelectParameters["Gender"].DefaultValue = "F";

                Dictionary<string, object> d = new Dictionary<string, object>();
                Lib.DataUtility du = new DataUtility();
                dt = du.getDataTableByText(@"select sid, note from RepMent where IsService = 1");
                Session["CahcedTable"] = dt;
                Session["situps_unit"] = false;
                Session["pushups_unit"] = false;
                Session["run_unit"] = true;
            }
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.ToString() == "男性")
        {
            SqlDataSource1.SelectParameters["Gender"].DefaultValue = "M";
            //SqlDataSource1.DataBind();
        }
        else
        {
            SqlDataSource1.SelectParameters["Gender"].DefaultValue = "F";
           // SqlDataSource1.DataBind();
        }

    }
    protected void DropDownList3_PreRender(object sender, EventArgs e)
    {
        if(!DropDownList3.Items.Contains(new ListItem("仰臥起坐", "0")))
        {
            DropDownList3.Items.Add(new ListItem("仰臥起坐", "0"));
            DropDownList3.SelectedValue = "0";
            situps_min.Disabled = true;
            situps_sec.Disabled = true;
            situps_times.Disabled = false;
        }
    }
    protected void DropDownList4_PreRender(object sender, EventArgs e)
    {
        if (!DropDownList4.Items.Contains(new ListItem("俯地挺身", "0")))
        {
            DropDownList4.Items.Add(new ListItem("俯地挺身", "0"));
            DropDownList4.SelectedValue = "0";
            pushups_min.Disabled = true;
            pushups_sec.Disabled = true;
            pushups_times.Disabled = false;
        }
    }
    protected void DropDownList5_PreRender(object sender, EventArgs e)
    {
        if (!DropDownList5.Items.Contains(new ListItem("三千公尺跑步", "0")))
        {
            DropDownList5.Items.Add(new ListItem("三千公尺跑步", "0"));
            DropDownList5.SelectedValue = "0";
            run_min.Disabled = false;
            run_sec.Disabled = false;
            run_times.Disabled = true;
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = (DataTable)Session["CahcedTable"];
        if (DropDownList3.SelectedValue != "0")
        {
            foreach (DataRow DR in dt.Rows)
            {
                if (DR["sid"].ToString() == DropDownList3.SelectedValue)
                {
                    if (DR["note"].ToString() == "次")
                    {
                        situps_min.Disabled = true;
                        situps_sec.Disabled = true;
                        situps_times.Disabled = false;
                        Session["situps_unit"] = false;
                        situps_min.Value = string.Empty;
                        situps_sec.Value = string.Empty;                    
                        break;
                    }
                    else
                    {
                        situps_min.Disabled = false;
                        situps_sec.Disabled = false;
                        situps_times.Disabled = true;
                        Session["situps_unit"] = true;
                        situps_times.Value = string.Empty;
                        break;
                    }
                }
            }
        }
        else
        {
            situps_min.Disabled = true;
            situps_sec.Disabled = true;
            situps_times.Disabled = false;
            Session["situps_unit"] = false;
            situps_min.Value = string.Empty;
            situps_sec.Value = string.Empty;     
        }
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = (DataTable)Session["CahcedTable"];
        if (DropDownList4.SelectedValue != "0")
        {
            foreach (DataRow DR in dt.Rows)
            {
                if (DR["sid"].ToString() == DropDownList4.SelectedValue)
                {
                    if (DR["note"].ToString() == "次")
                    {
                        pushups_min.Disabled = true;
                        pushups_sec.Disabled = true;
                        pushups_times.Disabled = false;
                        Session["pushups_unit"] = false;
                        pushups_min.Value = string.Empty;
                        pushups_sec.Value = string.Empty;                       
                        break;
                    }
                    else
                    {
                        pushups_min.Disabled = false;
                        pushups_sec.Disabled = false;
                        pushups_times.Disabled = true;
                        Session["pushups_unit"] = true;
                        pushups_times.Value = string.Empty;
                        break;
                    }
                }
            }
        }
        else
        {
            pushups_min.Disabled = true;
            pushups_sec.Disabled = true;
            pushups_times.Disabled = false;
            Session["pushups_unit"] = false;
            pushups_min.Value = string.Empty;
            pushups_sec.Value = string.Empty;   
        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = (DataTable)Session["CahcedTable"];
        if (DropDownList5.SelectedValue != "0")
        {
            foreach (DataRow DR in dt.Rows)
            {
                if (DR["sid"].ToString() == DropDownList5.SelectedValue)
                {
                    if (DR["note"].ToString() == "次")
                    {
                        run_min.Disabled = true;
                        run_sec.Disabled = true;
                        run_times.Disabled = false;
                        Session["run_unit"] = false;
                        run_min.Value = string.Empty;
                        run_sec.Value = string.Empty;
                        break;
                    }
                    else
                    {
                        run_min.Disabled = false;
                        run_sec.Disabled = false;
                        run_times.Disabled = true;
                        Session["run_unit"] = true;
                        run_times.Value = string.Empty;
                        break;
                    }
                }
            }
        }
        else
        {
            run_min.Disabled = false;
            run_sec.Disabled = false;
            run_times.Disabled = true;
            Session["run_unit"] = true;
            run_times.Value = string.Empty;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (situps_min.Value.Trim() != string.Empty || situps_sec.Value.Trim() != string.Empty || situps_times.Value.Trim() != string.Empty)
        {
            if (pushups_min.Value.Trim() != string.Empty || pushups_sec.Value.Trim() != string.Empty || pushups_times.Value.Trim() != string.Empty)
            {
                if (run_min.Value.Trim() != string.Empty || run_sec.Value.Trim() != string.Empty || run_times.Value.Trim() != string.Empty)
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    int situps = 0, pushups = 0, run = 0;
                    if ((bool)Session["situps_unit"])  //第一項的單位是秒
                    {
                        if (!String.IsNullOrEmpty(situps_min.Value) && !String.IsNullOrEmpty(situps_sec.Value))
                        {
                            situps = Convert.ToInt32(situps_min.Value) * 60 + Convert.ToInt32(situps_sec.Value);
                        }
                        else if (!String.IsNullOrEmpty(situps_min.Value) && String.IsNullOrEmpty(situps_sec.Value))
                        {
                            situps = Convert.ToInt32(situps_min.Value) * 60;
                        }
                        else if (String.IsNullOrEmpty(situps_min.Value) && !String.IsNullOrEmpty(situps_sec.Value))
                        {
                            situps = Convert.ToInt32(situps_sec.Value);
                        }
                    }
                    else
                    {
                        situps = Convert.ToInt32(situps_times.Value);
                    }

                    if ((bool)Session["pushups_unit"])  //第一項的單位是秒
                    {
                        if (!String.IsNullOrEmpty(pushups_min.Value) && !String.IsNullOrEmpty(pushups_sec.Value))
                        {
                            pushups = Convert.ToInt32(pushups_min.Value) * 60 + Convert.ToInt32(pushups_sec.Value);
                        }
                        else if (!String.IsNullOrEmpty(pushups_min.Value) && String.IsNullOrEmpty(pushups_sec.Value))
                        {
                            pushups = Convert.ToInt32(pushups_min.Value) * 60;
                        }
                        else if (String.IsNullOrEmpty(pushups_min.Value) && !String.IsNullOrEmpty(pushups_sec.Value))
                        {
                            pushups = Convert.ToInt32(pushups_sec.Value);
                        }
                    }
                    else
                    {
                        pushups = Convert.ToInt32(pushups_times.Value);
                    }

                    if ((bool)Session["run_unit"])  //第一項的單位是秒
                    {
                        if (!String.IsNullOrEmpty(run_min.Value) && !String.IsNullOrEmpty(run_sec.Value))
                        {
                            run = Convert.ToInt32(run_min.Value) * 60 + Convert.ToInt32(run_sec.Value);
                        }
                        else if (!String.IsNullOrEmpty(run_min.Value) && String.IsNullOrEmpty(run_sec.Value))
                        {
                            run = Convert.ToInt32(run_min.Value) * 60;
                        }
                        else if (String.IsNullOrEmpty(run_min.Value) && !String.IsNullOrEmpty(run_sec.Value))
                        {
                            run = Convert.ToInt32(run_sec.Value);
                        }
                    }
                    else
                    {
                        run = Convert.ToInt32(run_times.Value);
                    }

                    d.Add("memo", DropDownList3.SelectedValue + DropDownList4.SelectedValue + DropDownList5.SelectedValue);
                    d.Add("pushup", pushups.ToString());
                    d.Add("situp", situps.ToString());
                    d.Add("run", run.ToString());
                    d.Add("age", DropDownList2.SelectedValue);
                    if (DropDownList1.SelectedItem.ToString() == "男性")
                        d.Add("gender", "M");
                    else
                        d.Add("gender", "F");

                    dt = new Lib.DataUtility().getDataTableBysp(@"Ex104_CalResultTrial", d);
                    if (dt.Rows.Count > 0)
                    {
                        situps_score.InnerText = dt.Rows[0]["sit_ups_score"].ToString();
                        pushups_score.InnerText = dt.Rows[0]["push_ups_score"].ToString();
                        run_score.InnerText = dt.Rows[0]["run_score"].ToString();
                        if (dt.Rows[0]["status"].ToString().Substring(2, 1) == "2")
                        {
                            status.InnerText = "合格";
                        }
                        else if (dt.Rows[0]["status"].ToString().Substring(2, 1) == "3")
                        {
                            status.InnerText = "不合格";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('查無資料')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入項目一')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入項目二')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('請輸入項目三')", true);
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
