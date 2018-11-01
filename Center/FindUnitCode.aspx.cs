using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FindUnitCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> list = new Dictionary<string, object>();
        if (txtCode.Text != "")
        {
            list.Add("@unit_code", txtCode.Text.Trim());
            DataTable dt = new DataTable();
            dt = du.getDataTableByText("select unit_code [單位代碼], unit_title [單位全銜], [上級單位], parent_unit_code [上級單位代碼] from [VIEW_UNIT_CODE] where [unit_code] = @unit_code or [parent_unit_code] = @unit_code order by [單位代碼]", list);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> list = new Dictionary<string, object>();
        if (txtName.Text != "")
        {
            //list.Add("@title", txtName.Text.Trim());
            DataTable dt = new DataTable();
            string sqlcmd = string.Empty;
            sqlcmd = "select unit_code [單位代碼], unit_title [單位全銜], [上級單位], parent_unit_code [上級單位代碼] from [VIEW_UNIT_CODE] ";
            string[] keywords = txtName.Text.Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < keywords.Length; i++)
            {
                if (i == 0)
                {
                    sqlcmd += "where [unit_title] like '%" + keywords[i] + "%' ";
                }
                if (i == (keywords.Length - 1) && i != 0)
                {
                    sqlcmd += "and [unit_title] like '%" + keywords[i] + "%' order by [單位代碼]";
                }
                else
                {
                    sqlcmd += "and [unit_title] like '%" + keywords[i] + "%' ";
                }
            }
            dt = du.getDataTableByText(sqlcmd);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
