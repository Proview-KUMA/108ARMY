using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RaceNotice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        dt = du.getDataTableBysp("Race_RaceNotice");
        GridView1.DataSource = dt;
        GridView1.DataBind();

        Lib.DataUtility du2 = new Lib.DataUtility();
        DataTable dt2 = new DataTable();
        dt2 = du2.getDataTableBysp("Race_RaceNoticePerson");
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string unit_code = e.Row.Cells[12].Text;
            //e.Row.Cells[1].Attributes.Add("onclick", "alert('" + unit_code + "');return false;");
            e.Row.Cells[1].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=999');");
            e.Row.Cells[1].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=001');");
            e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=102');");
            e.Row.Cells[3].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=103');");
            e.Row.Cells[4].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=104');");
            e.Row.Cells[5].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[6].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=202');");
            e.Row.Cells[6].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[7].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=203');");
            e.Row.Cells[7].Attributes.Add("style", "cursor:pointer;");

            e.Row.Cells[8].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?unit_code=" + unit_code + "&status=204');");
            e.Row.Cells[8].Attributes.Add("style", "cursor:pointer;");
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string team = e.Row.Cells[0].Text;
            switch (team)
            {
                case "個人甲組":
                    e.Row.Cells[1].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M40&status=999');");
                    e.Row.Cells[1].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M40&status=001');");
                    e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M40&status=102');");
                    e.Row.Cells[3].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M40&status=103');");
                    e.Row.Cells[4].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M40&status=104');");
                    e.Row.Cells[5].Attributes.Add("style", "cursor:pointer;");

                    break;
                case "個人乙組":
                    e.Row.Cells[1].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M30&status=999');");
                    e.Row.Cells[1].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M30&status=001');");
                    e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M30&status=102');");
                    e.Row.Cells[3].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M30&status=103');");
                    e.Row.Cells[4].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M30&status=104');");
                    e.Row.Cells[5].Attributes.Add("style", "cursor:pointer;");
                    break;
                case "個人丙組":
                    e.Row.Cells[1].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M20&status=999');");
                    e.Row.Cells[1].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M20&status=001');");
                    e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M20&status=102');");
                    e.Row.Cells[3].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M20&status=103');");
                    e.Row.Cells[4].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=M20&status=104');");
                    e.Row.Cells[5].Attributes.Add("style", "cursor:pointer;");
                    break;
                case "女子組":
                    e.Row.Cells[1].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=F&status=999');");
                    e.Row.Cells[1].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[2].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=F&status=001');");
                    e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[3].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=F&status=102');");
                    e.Row.Cells[3].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[4].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=F&status=103');");
                    e.Row.Cells[4].Attributes.Add("style", "cursor:pointer;");

                    e.Row.Cells[5].Attributes.Add("onclick", "javascript:window.open('Race_DetailStatus.aspx?team=F&status=104');");
                    e.Row.Cells[5].Attributes.Add("style", "cursor:pointer;");
                    break;
                default:
                    break;
            }

        }
    }
}
