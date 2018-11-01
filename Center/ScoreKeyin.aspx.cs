using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lib;

public partial class ScoreKeyin : System.Web.UI.Page
{
    public DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        Lib.DataUtility du = new DataUtility();
        dt = du.getDataTableByText(@"select sid, note from RepMent where IsService = 1");
        Session["CahcedTable"] = dt;
        Session["situps_unit"] = false;
        Session["pushups_unit"] = false;
        Session["run_unit"] = true;
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    MainScoreWS.WebService2 _Web = new MainScoreWS.WebService2();
    //    DataTable dt = _Web.QueryPlayer(id.Text.Trim(), TextBox1.Text.Trim());
    //    Response.Write(dt.Rows[0]["status"].ToString());
    //}

    //日曆
    protected void datepicker_SelectionChanged(object sender, EventArgs e)
    {
        date.Text = datepicker.SelectedDate.ToShortDateString();
    }

    //protected void DropDownList3_PreRender(object sender, EventArgs e)
    //{
    //    if (!DropDownList3.Items.Contains(new ListItem("仰臥起坐", "0")))
    //    {
    //        DropDownList3.Items.Add(new ListItem("仰臥起坐", "0"));
    //        DropDownList3.SelectedValue = "0";
    //        situps_min.Disabled = true;
    //        situps_sec.Disabled = true;
    //        situps_times.Disabled = false;
    //    }
    //}
    //protected void DropDownList4_PreRender(object sender, EventArgs e)
    //{
    //    if (!DropDownList4.Items.Contains(new ListItem("俯地挺身", "0")))
    //    {
    //        DropDownList4.Items.Add(new ListItem("俯地挺身", "0"));
    //        DropDownList4.SelectedValue = "0";
    //        pushups_min.Disabled = true;
    //        pushups_sec.Disabled = true;
    //        pushups_times.Disabled = false;
    //    }
    //}
    //protected void DropDownList5_PreRender(object sender, EventArgs e)
    //{
    //    if (!DropDownList5.Items.Contains(new ListItem("三千公尺跑步", "0")))
    //    {
    //        DropDownList5.Items.Add(new ListItem("三千公尺跑步", "0"));
    //        DropDownList5.SelectedValue = "0";
    //        run_min.Disabled = false;
    //        run_sec.Disabled = false;
    //        run_times.Disabled = true;
    //    }
    //}
    //protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dt = (DataTable)Session["CahcedTable"];
    //    if (DropDownList3.SelectedValue != "0")
    //    {
    //        foreach (DataRow DR in dt.Rows)
    //        {
    //            if (DR["sid"].ToString() == DropDownList3.SelectedValue)
    //            {
    //                if (DR["note"].ToString() == "次")
    //                {
    //                    situps_min.Disabled = true;
    //                    situps_sec.Disabled = true;
    //                    situps_times.Disabled = false;
    //                    Session["situps_unit"] = false;
    //                    situps_min.Value = string.Empty;
    //                    situps_sec.Value = string.Empty;
    //                    break;
    //                }
    //                else
    //                {
    //                    situps_min.Disabled = false;
    //                    situps_sec.Disabled = false;
    //                    situps_times.Disabled = true;
    //                    Session["situps_unit"] = true;
    //                    situps_times.Value = string.Empty;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        situps_min.Disabled = true;
    //        situps_sec.Disabled = true;
    //        situps_times.Disabled = false;
    //        Session["situps_unit"] = false;
    //        situps_min.Value = string.Empty;
    //        situps_sec.Value = string.Empty;
    //    }
    //}
    //protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dt = (DataTable)Session["CahcedTable"];
    //    if (DropDownList4.SelectedValue != "0")
    //    {
    //        foreach (DataRow DR in dt.Rows)
    //        {
    //            if (DR["sid"].ToString() == DropDownList4.SelectedValue)
    //            {
    //                if (DR["note"].ToString() == "次")
    //                {
    //                    pushups_min.Disabled = true;
    //                    pushups_sec.Disabled = true;
    //                    pushups_times.Disabled = false;
    //                    Session["pushups_unit"] = false;
    //                    pushups_min.Value = string.Empty;
    //                    pushups_sec.Value = string.Empty;
    //                    break;
    //                }
    //                else
    //                {
    //                    pushups_min.Disabled = false;
    //                    pushups_sec.Disabled = false;
    //                    pushups_times.Disabled = true;
    //                    Session["pushups_unit"] = true;
    //                    pushups_times.Value = string.Empty;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        pushups_min.Disabled = true;
    //        pushups_sec.Disabled = true;
    //        pushups_times.Disabled = false;
    //        Session["pushups_unit"] = false;
    //        pushups_min.Value = string.Empty;
    //        pushups_sec.Value = string.Empty;
    //    }
    //}
    //protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dt = (DataTable)Session["CahcedTable"];
    //    if (DropDownList5.SelectedValue != "0")
    //    {
    //        foreach (DataRow DR in dt.Rows)
    //        {
    //            if (DR["sid"].ToString() == DropDownList5.SelectedValue)
    //            {
    //                if (DR["note"].ToString() == "次")
    //                {
    //                    run_min.Disabled = true;
    //                    run_sec.Disabled = true;
    //                    run_times.Disabled = false;
    //                    Session["run_unit"] = false;
    //                    run_min.Value = string.Empty;
    //                    run_sec.Value = string.Empty;
    //                    break;
    //                }
    //                else
    //                {
    //                    run_min.Disabled = false;
    //                    run_sec.Disabled = false;
    //                    run_times.Disabled = true;
    //                    Session["run_unit"] = true;
    //                    run_times.Value = string.Empty;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        run_min.Disabled = false;
    //        run_sec.Disabled = false;
    //        run_times.Disabled = true;
    //        Session["run_unit"] = true;
    //        run_times.Value = string.Empty;
    //    }
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void datepicker_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.Date >= System.DateTime.Today)
        {
            e.Day.IsSelectable = false;
        }
    }
}