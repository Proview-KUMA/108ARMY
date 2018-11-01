<%@ WebHandler Language="C#" Class="Race_Export" %>

using System;
using System.Web;

public class Race_Export : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            if (context.Request.QueryString["status"] != null)
            {
                Lib.DataUtility du = new Lib.DataUtility();
                string status = context.Request.QueryString["status"].ToString();
                string sql = "SELECT [id],[name],convert(nvarchar(20),[birth],20) [birth],[age],[gender],[unit_code],[rank_code],[height],[weight],[BMI],[bodyfat],[sit_ups],[sit_ups_score],[push_ups],[push_ups_score],[run],[run_score],convert(nvarchar(20),[date],20) [date],[center_code],[result],[status],[op_id],[clothesNum],[LF_Tag_ID],[UHF_Tag_ID],[code],[memo] FROM [Center].[dbo].[Result]";
                switch (status)
                {
                    case "999":
                        sql += " where status = '999'";
                        break;
                    case "001":
                        sql += " where status = '001'";
                        break;
                    case "102":
                        sql += " where substring(result.status,3,1) = '2'";
                        break;
                    case "103":
                        sql += " where substring(result.status,3,1) = '3'";
                        break;
                    case "104":
                        sql += " where substring(result.status,3,1) = '4'";
                        break;
                    case "all":
                        break;
                    default:
                        sql = string.Empty;
                        break;
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    try
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = du.getDataTableByText(sql);
                        string json = Lib.SysSetting.GetJsonFormatData(dt);
                        context.Response.Write(json);
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write("{'exception':'" + ex.Message + "'}");
                    }
                }
            }

        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}