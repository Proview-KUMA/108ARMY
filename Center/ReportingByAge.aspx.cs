using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ReportingByAge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set Date
        date_start.Value = (DateTime.Now.Year - 1911).ToString() + "/1/1";
        date_stop.Value = (DateTime.Now.Year - 1911).ToString() + "12/31";
        
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (Session["options"] != null)
        {
            DataTable dt = Session["options"] as DataTable;
            DataRow row = dt.NewRow();
            row[0] = Menu1.SelectedItem.Text;
            row[1] = Menu1.SelectedValue;
            dt.Rows.Add(row);
            Session["options"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("軍種別");
            dt.Columns.Add("代碼");
            DataRow row = dt.NewRow();
            row[0] = Menu1.SelectedItem.Text;
            row[1] = Menu1.SelectedValue;
            dt.Rows.Add(row);
            Session["options"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["options"] != null)
        {
            DataTable dt = Session["options"] as DataTable;
            dt.Clear();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Session["options"] = dt;
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
