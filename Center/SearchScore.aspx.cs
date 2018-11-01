using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchScore : System.Web.UI.Page
{
    public string _date;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    #region search button
    protected void search1_Click(object sender, EventArgs e)
    {
        SqlDataSource5.SelectParameters["id"].DefaultValue = id.Text.Trim();
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void search2_Click(object sender, EventArgs e)
    {
        SqlDataSource2.SelectParameters["value"].DefaultValue = name.Text.Trim();
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void search3_Click(object sender, EventArgs e)
    {
        SqlDataSource3.SelectParameters["value"].DefaultValue = unit_code.Text.Trim();
        TabContainer1.ActiveTabIndex = 2;
    }

    protected void search4_Click(object sender, EventArgs e)
    {
        try
        {
            SqlDataSource4.SelectParameters["value"].DefaultValue = Lib.SysSetting.ToWorldDate(date.Text.Trim()).ToShortDateString();
            TabContainer1.ActiveTabIndex = 3;
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }
    #endregion

    #region GridView Sorting

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        TabContainer1.ActiveTabIndex = 2;
    }

    protected void GridView4_Sorting(object sender, GridViewSortEventArgs e)
    {
        TabContainer1.ActiveTabIndex = 3;
    }
    #endregion

    #region GridView PageIndexChanged
    protected void GridView2_PageIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void GridView3_PageIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 2;
    }

    protected void GridView4_PageIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 3;
    }
    #endregion

    protected void DropDownList1_OnDataBound(object sender, EventArgs e)
    {    
        
        for(int i = 0 ; i < DropDownList1.Items.Count; i ++)
        {
            DropDownList1.Items[i].Text = Lib.SysSetting.ToRocDateFormat(DropDownList1.Items[i].Text);
            //DropDownList1.Items[i].Value = Lib.SysSetting.ToRocDateFormat(DropDownList1.Items[i].Value);
        }
        if (DropDownList1.Items.Count > 0)
        {
            //SqlDataSource1.SelectParameters["id"].DefaultValue = id.Text.Trim();
            //SqlDataSource1.SelectParameters["value"].DefaultValue = DropDownList1.SelectedValue;
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlDataSource1.SelectParameters["id"].DefaultValue = id.Text.Trim();
            SqlDataSource1.SelectParameters["value"].DefaultValue = DropDownList1.SelectedValue;
        }
        catch (Exception ex)
        { 
        
        }
    }
    protected void DetailsView1_DataBound(object sender, EventArgs e)
    {
        if (DetailsView1.Rows.Count > 0)
        {
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
