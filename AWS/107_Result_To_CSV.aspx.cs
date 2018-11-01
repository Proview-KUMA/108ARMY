using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib;

public partial class _107_Result_To_CSV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] != null)
        {
            Account a = (Account)Session["account"];
            if (a.Role != ((int)SysSetting.Role.admin_hq).ToString())
            {
                Response.Redirect("~/index.aspx");
            }
            else
            {
                //執行查詢成績
                if (Page.IsPostBack == true)
                {
                    
                }
                else
                {
                    //取得年份列表
                    SetYearList();
                }
            }
        }
        if (Session["account"] == null && Session["player"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        
    }
    
    //2018-1-12修正正確寫法，才能在Client端儲存檔案
    private void Save_csv_toClient(DataTable dt, string svPath)
    {
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.ClearContent();
        response.Clear();
        response.ContentType = "text/plain";
        response.AddHeader("Content-Disposition", "attachment; filename="+svPath+";");
        StringBuilder sb = new StringBuilder();

        
        string data = "";

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                if (!string.IsNullOrEmpty(row[column].ToString().Trim()))
                {
                    if (column.ColumnName == "birth" | column.ColumnName == "date")
                    {
                        data += Convert.ToDateTime(row[column].ToString().Trim()).ToString("yyyy-MM-dd 00:00:00.000") + ",";
                    }
                    else
                        data += row[column].ToString().Trim() + ",";
                }
                else
                    data += "NULL,";
            }
            data += "";
            sb.AppendLine(data.Substring(0, (data.Length - 1)));
            data = "";
        }
        response.Write(sb.ToString());

        response.Flush();
        response.End();
    }
    //加入年份列表
    private void SetYearList()
    {
        int thisYear;
        thisYear = DateTime.Now.Year;
        ddl_year.Items.Clear();
        for(int i = 0; i < 7; i++)
        {
            ddl_year.Items.Add(thisYear.ToString());
            thisYear--;
        }
    }

    protected void btn_InqResult_Click(object sender, EventArgs e)
    {
        string year = ddl_year.SelectedItem.Text.ToString() + "-";
        string start_date = string.Empty;
        string end_date = string.Empty;
        switch (ddl_season.SelectedValue)
        {
            case "1":
                start_date = year + "01-01";
                end_date = year + "03-31";
                break;
            case "2":
                start_date = year + "04-01";
                end_date = year + "06-30";
                break;
            case "3":
                start_date = year + "07-01";
                end_date = year + "09-30";
                break;
            case "4":
                start_date = year + "10-01";
                end_date = year + "12-31";
                break;
            default:
                start_date = year + "01-01";
                end_date = year + "03-31";
                break;
        }

        Dictionary<string, object> d = new Dictionary<string, object>();
        Lib.DataUtility du = new Lib.DataUtility();
        try
        {
            d.Add("start_date", start_date);
            d.Add("end_date", end_date);
            DataTable dt = new DataTable();
            //2017-11-23使用新的函式，加長timeout為120秒
            dt = du.getDataTableBysp_BigData(@"Ex107_GetResultByDate", d);
            if (dt.Rows.Count > 0)
            {
                string svPath = "Result(" + start_date + "_" + end_date + ").csv";
                Save_csv_toClient(dt, svPath);             
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + "查無資料!!" + "')", true);
            }

        }
        catch (Exception ex)
        {
            //記錄錯誤訊息
            SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, this.ToString());
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "')", true);
        }
    }
}