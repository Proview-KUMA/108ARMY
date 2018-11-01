using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class ToExcel : System.Web.UI.Page
{
    //public static int C = 0 , N = 0 , A = 0 , F = 0 , G = 0 , J = 0 , P = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = Lib.SysSetting.ToRocDateFormat(System.DateTime.Today.AddDays(1).ToShortDateString());
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
            //C = 0;
            //N = 0;
            //A = 0;
            //F = 0;
            //G = 0;
            //J = 0;
            //P = 0;
            //Label1.Text = "";
            //Label2.Text = "";
            //Label3.Text = "";
            //Label4.Text = "";
            //Label5.Text = "";
            //Label6.Text = "";
            //Label7.Text = "";
            //Label8.Text = "";
            //Label9.Text = "";
            if (txtDate.Text.Trim() != "")
            {
                try
                {
                    //SqlDataSource1.SelectParameters["date"].DefaultValue = Lib.SysSetting.ToWorldDate(txtDate.Text.Trim()).Date.ToString("");
                    SqlDataSource1.SelectParameters["date"].DefaultValue = Lib.SysSetting.ToWorldDate(txtDate.Text.Trim()).ToShortDateString();
                    Lib.DataUtility du = new Lib.DataUtility();
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("date", Lib.SysSetting.ToWorldDate(txtDate.Text.Trim()));
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
                    //Label10.Text = dt.Rows[0]["a_count"].ToString();
                    //Label11.Text = dt.Rows[0]["b_count"].ToString();
                    //Label12.Text = dt.Rows[0]["c_count"].ToString();
                    //Label13.Text = dt.Rows[0]["d_count"].ToString();
                    //Label14.Text = dt.Rows[0]["e_count"].ToString();
                    //Label15.Text = dt.Rows[0]["f_count"].ToString();
                    //Label16.Text = dt.Rows[0]["g_count"].ToString();
                    //Label17.Text = dt.Rows[0]["h_count"].ToString();
                    //SqlDataSource1.SelectParameters["date"].DefaultValue = Lib.SysSetting.ToWorldDate(txtDate.Text.Trim()).Date.ToString("");
                    //Lib.DataUtility du = new Lib.DataUtility();
                    //Dictionary<string, object> d = new Dictionary<string, object>();
                    //d.Add("date", Lib.SysSetting.ToWorldDate(txtDate.Text.Trim()));
                    //DataTable dt = du.getDataTableByText("select count(id) as count from result where (memo <> '000' and memo <> '999') and date = @date", d);
                    //Label9.Text = "替代項目: " + dt.Rows[0]["count"].ToString() + "人";
                    //GridView1.DataBind();
                    //if (GridView1.Rows.Count == 0)
                    //{
                    //    Label1.Text = "中央單位: " + C.ToString() + "人";
                    //    Label2.Text = "陸軍: " + A.ToString() + "人";
                    //    Label3.Text = "海軍: " + N.ToString() + "人";
                    //    Label4.Text = "空軍: " + F.ToString() + "人";
                    //    Label5.Text = "聯勤: " + J.ToString() + "人";
                    //    Label6.Text = "後備: " + G.ToString() + "人";
                    //    Label7.Text = "憲兵: " + P.ToString() + "人";
                    //    Label8.Text = "總人數: " + (C + A + N + F + J + G + P).ToString() + "人";
                    //    //GridView1.DataBind();
                    //    //Label1.Text = "";
                    //    //Label2.Text = "";
                    //    //Label3.Text = "";
                    //    //Label4.Text = "";
                    //    //Label5.Text = "";
                    //    //Label6.Text = "";
                    //    //Label7.Text = "";
                    //    //Label8.Text = "";
                    //}
                }
                catch (Exception ex)
                {
                    Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                    Response.Redirect("ToExcel.aspx");
                }
            }
        //Label1.Text = "中央單位:" + A.ToString();
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
                e.Row.Cells[0].Text = (e.Row.DataItemIndex + 1).ToString();
                e.Row.Cells[6].Text = "'"+Lib.SysSetting.ToRocDateFormat(e.Row.Cells[6].Text);
            }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    if (GridView1.Rows.Count > 0)
    //    {
    //        Response.Clear();
    //        Response.AddHeader("content-disposition",
    //            "attachment;filename=PoolExport.xls");
    //        Response.ContentType = "application/vnd.xls";
    //        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //        //Response.ContentType = "application/octet-stream";
    //        System.IO.StringWriter sw = new System.IO.StringWriter();
    //        System.Web.UI.HtmlTextWriter htw = new HtmlTextWriter(sw);

    //        //關閉換頁跟排序
    //        GridView1.AllowSorting = false;
    //        GridView1.AllowPaging = false;

    //        //移去不要的欄位
    //        //GridView1.Columns.RemoveAt(6);
    //        //GridView1.DataBind();

    //        //建立假HtmlForm避免以下錯誤
    //        //Control 'GridView1' of type 'GridView' must be placed inside 
    //        //a form tag with runat=server. 
    //        //另一種做法是override VerifyRenderingInServerForm後不做任何事
    //        //這樣就可以直接GridView1.RenderControl(htw);

    //        HtmlForm hf = new HtmlForm();
    //        Controls.Add(hf);
    //        hf.Controls.Add(GridView1);
    //        hf.RenderControl(htw);

    //        Response.Write(sw.ToString());
    //        Response.End();
    //    }
    //}
}
