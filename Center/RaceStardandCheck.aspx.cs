using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class RaceStardandCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(txtAge.Text))
        {
            List<System.Data.SqlClient.SqlParameter> list = new List<System.Data.SqlClient.SqlParameter>();
            list.Add(new SqlParameter("gender", ListBoxGender.SelectedValue));
            list.Add(new SqlParameter("age", Convert.ToInt16(txtAge.Text.Trim())));
            list.Add(new SqlParameter("type",ListBoxType.SelectedValue));
            list.Add(new SqlParameter("time",ListBoxTime.SelectedValue));
            list.Add(new SqlParameter("item",ListBoxItem.SelectedValue));
            dt = du.getDataTableBysp("Race_GetTeamStandardByItem2", list.ToArray());
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
