using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PrintDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            SqlDataSource1.SelectParameters["date"].DefaultValue = Lib.SysSetting.ToWorldDate(Request.QueryString["date"].Trim()).Date.ToString("");
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("date", Lib.SysSetting.ToWorldDate(Request.QueryString["date"].Trim()));
            DataTable dt = du.getDataTableBysp("QueryreplaceItemCount", d);
            Label1.Text = dt.Rows[0]["c_total"].ToString();
            Label2.Text = dt.Rows[0]["a_total"].ToString();
            Label3.Text = dt.Rows[0]["n_total"].ToString();
            Label4.Text = dt.Rows[0]["f_total"].ToString();
            Label5.Text = dt.Rows[0]["j_total"].ToString();
            Label6.Text = dt.Rows[0]["g_total"].ToString();
            Label7.Text = dt.Rows[0]["p_total"].ToString();
            Label8.Text = dt.Rows[0]["total"].ToString();
            Label9.Text = dt.Rows[0]["rep_total"].ToString();
        }
        catch (Exception ex)
        {
            Response.Write("查無資料");
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "A")
            {
                e.Row.Cells[1].Text = "陸軍";
            }
            if (e.Row.Cells[1].Text == "C")
            {
                e.Row.Cells[1].Text = "中央單位";
            }
            if (e.Row.Cells[1].Text == "N")
            {
                e.Row.Cells[1].Text = "海軍";
            }
            if (e.Row.Cells[1].Text == "F")
            {
                e.Row.Cells[1].Text = "空軍";
            }
            if (e.Row.Cells[1].Text == "P")
            {
                e.Row.Cells[1].Text = "憲兵";
            }
            if (e.Row.Cells[1].Text == "J")
            {
                e.Row.Cells[1].Text = "聯勤";
            }
            if (e.Row.Cells[1].Text == "G")
            {
                e.Row.Cells[1].Text = "後備";
            }
            e.Row.Cells[5].Text = "'"+Lib.SysSetting.ToRocDateFormat(e.Row.Cells[5].Text);
            e.Row.Cells[0].Text = (e.Row.DataItemIndex + 1).ToString();
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
