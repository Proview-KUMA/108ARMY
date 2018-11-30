using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lib;

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
    //結算剩餘成績
    protected void btn_CalculationResult_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        DataUtility du = new DataUtility();
        DataTable dt = new DataTable();
        try
        {
            du.executeNonQueryByText("Delete Result where id like 'AA________'");//先刪除測試人員帳號
            d.Add("type", "001");
            d.Add("value", System.DateTime.Now.ToShortDateString());
            dt = du.getDataTableBysp("Ex108_ViewStatus", d);
            DateTime ChangeDate = new DateTime(2018, 12, 31, 23, 59, 59);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dictionary<string, object> di = new Dictionary<string, object>();
                    string id = dt.Rows[i]["id"].ToString();
                    DateTime today = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                    di.Add("id", id);
                    di.Add("date", today);
                    if (today > ChangeDate)
                    {
                        du.executeNonQueryBysp("Ex108_CalResultByID", di);
                    }
                    else
                    {
                        du.executeNonQueryBysp("Ex106_CalResultByID", di);
                    }
                }
                Button6_OnClick(Button6, e);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "outside('" + dt.Rows.Count.ToString() + "')", true);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "outside('Err')", true);
        }
    }
}
