using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ViewResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
        SqlDataSource2.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
        SqlDataSource3.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
        SqlDataSource4.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
        SqlDataSource5.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
        SqlDataSource6.SelectParameters["value"].DefaultValue = System.DateTime.Now.ToShortDateString();
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {     
        TabContainer1.ActiveTabIndex = 0;
    }
    protected void GridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //e.Row.Cells[6].Text = Lib.SysSetting.ToRunMinSecFormat(e.Row.Cells[6].Text);
        //    //分數的呈現
        //    e.Row.Cells[3].Text = (e.Row.Cells[3].Text == "&nbsp;" || e.Row.Cells[3].Text == "999" ? " - " : e.Row.Cells[3].Text);
        //    e.Row.Cells[5].Text = (e.Row.Cells[5].Text == "&nbsp;" || e.Row.Cells[5].Text == "999" ? " - " : e.Row.Cells[5].Text);
        //    //次數與秒數的呈現
        //    e.Row.Cells[2].Text = (e.Row.Cells[2].Text == "&nbsp;" ? " 未測 " : e.Row.Cells[2].Text);
        //    e.Row.Cells[4].Text = (e.Row.Cells[4].Text == "&nbsp;" ? " 未測 " : e.Row.Cells[4].Text);
        //    if (e.Row.Cells[6].Text == "9999")
        //    {
        //        e.Row.Cells[6].Text = " - ";
        //        e.Row.Cells[7].Text = "未完測";
        //    }
        //    else if (e.Row.Cells[6].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[6].Text = "未測";
        //        e.Row.Cells[7].Text = " - ";
        //    }
        //    else
        //    {
        //        e.Row.Cells[6].Text = Lib.SysSetting.ToRunMinSecFormat(e.Row.Cells[6].Text);
        //    }
        //}

    }
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

    protected void GridView5_PageIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 4;
    }

    protected void GridView6_PageIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 5;
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        GridView1.DataBind();
        TabContainer1.ActiveTabIndex = 0;

    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        GridView2.DataBind();
        TabContainer1.ActiveTabIndex = 1;

    }
    protected void Button3_OnClick(object sender, EventArgs e)
    {
        GridView3.DataBind();
        TabContainer1.ActiveTabIndex = 2;

    }
    protected void Button4_OnClick(object sender, EventArgs e)
    {
        GridView4.DataBind();
        TabContainer1.ActiveTabIndex = 3;

    }
    protected void Button5_OnClick(object sender, EventArgs e)
    {
        GridView5.DataBind();
        TabContainer1.ActiveTabIndex = 4;

    }
    protected void Button6_OnClick(object sender, EventArgs e)
    {
        GridView6.DataBind();
        TabContainer1.ActiveTabIndex = 5;

    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
