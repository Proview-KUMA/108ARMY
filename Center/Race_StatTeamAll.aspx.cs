using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Race_StatTeamAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GridView1.DataSource = new Lib.DataUtility().getDataTableBysp("Race_StatTeamAll");
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToDecimal(e.Row.Cells[16].Text) < 0)
            {
                e.Row.Cells[16].Text = "0<br />(" + e.Row.Cells[16].Text + ")";
            }

            // add detail
            string unit_code = e.Row.Cells[0].Text;
            e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=bmi');");
            e.Row.Cells[3].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=race');");
            e.Row.Cells[4].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=freerace');");
            e.Row.Cells[5].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[6].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=retire');");
            e.Row.Cells[6].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[7].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=baby');");
            e.Row.Cells[7].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[8].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=training');");
            e.Row.Cells[8].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[9].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=repl');");
            e.Row.Cells[9].Attributes.Add("style", "cursor:pointer");

            e.Row.Cells[10].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatusRare.aspx?unit_code=" + unit_code + "&status=absent');");
            e.Row.Cells[10].Attributes.Add("style", "cursor:pointer");
        }
    }
}
