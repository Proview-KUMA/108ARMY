using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DeSignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            if (Lib.SysSetting.CurrentSystemMode() != Lib.SysSetting.SystemMode.Race)
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {

        }

        if (Page.IsPostBack)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            if (DropDownList1.SelectedValue.Length == 5)
            {
                d.Add("type", "unit_code");
            }
            else
            {
                d.Add("type", "personal");
            }
            d.Add("value", DropDownList1.SelectedValue);
            GridView3.DataSource = new Lib.DataUtility().getDataTableBysp("Race_QueryResult", d);
            GridView3.DataBind();

            GridView3.UseAccessibleHeader = true;
            GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count > 0)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();

            try
            {
                if (DropDownList1.SelectedValue.Length == 5)
                {
                    d.Add("value", DropDownList1.SelectedValue);
                    d.Add("type", "unit_code");
                }
                else
                {
                    d.Add("value", DropDownList1.SelectedValue);
                    d.Add("type", "personal");
                }
                du.executeNonQueryBysp("Race_DelCheckinInfo", d);
                System.Threading.Thread.Sleep(2000);
                Response.Redirect("DeSignUp.aspx");
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"無報進名單\");", true);
        }
    }
   
    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count > 0)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            if (DropDownList1.SelectedValue.Length == 5)
            {
                d.Add("type", "unit_code");
            }
            else
            {
                d.Add("type", "personal");
            }         
            d.Add("value", DropDownList1.SelectedValue);
            GridView3.DataSource = new Lib.DataUtility().getDataTableBysp("Race_QueryResult", d);
            GridView3.DataBind();

            GridView3.UseAccessibleHeader = true;
            GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
            Button1.Enabled = true;
            btnOutPut.Enabled = true;
        }
        else
        {
            Button1.Enabled = false;
            btnOutPut.Enabled = false;
        }
    }
    protected void btnOutPut_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue != null)
        {
            DataTable dt = new DataTable();
            if (DropDownList1.SelectedValue != "1")
            {
                dt = new Lib.DataUtility().getDataTableByText("select id, name, convert(nvarchar(10),birth,111) [birthday], unit_code, rank_code,result from result where unit_code = '" + DropDownList1.SelectedValue + "' and status = '999' and substring(result,1,1) = '2'");
                Response.ContentType = "text/txt";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + DropDownList1.SelectedItem.Text + "競賽系統報進.txt");
                Response.Write(@"身分證字號,姓名,生日(西元),單位代碼,軍階代碼,役別(志願役:A 義務役:B),新進人員區分代碼(結訓:1 滿3個月:3 滿4個月:4 滿5個月:5 滿6個月以上:6)
");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Response.Write(dt.Rows[i]["id"].ToString() + ",");
                    Response.Write(dt.Rows[i]["name"].ToString() + ",");
                    Response.Write(dt.Rows[i]["birthday"].ToString() + ",");
                    Response.Write(dt.Rows[i]["unit_code"].ToString() + ",");
                    Response.Write(dt.Rows[i]["rank_code"].ToString().Trim() + ",");
                    Response.Write(dt.Rows[i]["result"].ToString().Substring(1, 1) + ",");
                    Response.Write(dt.Rows[i]["result"].ToString().Substring(2, 1));
                    Response.Write(Environment.NewLine);
                }
                Response.End();
            }
            else
            {
                dt = new Lib.DataUtility().getDataTableByText("select id, name, convert(nvarchar(10),birth,111) [birthday], unit_code, rank_code,result from result where status = '999' and substring(result,1,1) = '1'");
                Response.ContentType = "text/txt";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + DropDownList1.SelectedItem.Text + "競賽系統報進.txt");
                Response.Write(@"身分證字號,姓名,生日(西元),單位代碼,軍階代碼,役別(志願役:A 義務役:B),新進人員區分代碼(結訓:1 滿3個月:3 滿4個月:4 滿5個月:5 滿6個月以上:6)
");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Response.Write(dt.Rows[i]["id"].ToString() + ",");
                    Response.Write(dt.Rows[i]["name"].ToString() + ",");
                    Response.Write(dt.Rows[i]["birthday"].ToString() + ",");
                    Response.Write(dt.Rows[i]["unit_code"].ToString() + ",");
                    Response.Write(dt.Rows[i]["rank_code"].ToString().Trim() + ",");
                    Response.Write(dt.Rows[i]["result"].ToString().Substring(1, 1) + ",");
                    Response.Write(dt.Rows[i]["result"].ToString().Substring(2, 1));
                    Response.Write(Environment.NewLine);
                }
                Response.End();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('沒有單位');", true);
        }
    }
}
