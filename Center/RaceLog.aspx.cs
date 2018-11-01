using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RaceLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (DropDownList1.SelectedValue != null)
            {
                DataTable dt = new DataTable();
                dt = new Lib.DataUtility().getDataTableBysp("Race_ShowRaceLog", "id", DropDownList1.SelectedValue);
                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
        }
    }




    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count == 1)
        {
            DataTable dt = new DataTable();
            dt = new Lib.DataUtility().getDataTableBysp("Race_ShowRaceLog", "id", DropDownList1.Items[0].Value);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
