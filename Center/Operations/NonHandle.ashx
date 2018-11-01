<%@ WebHandler Language="C#" Class="NonHandle" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class NonHandle : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        if (context.Request.Params["type"] != null)
        {

            string type = context.Request.Params["type"];
            switch (type)
            {

                case "QueryNonHandle":
                    d.Clear();
                    d.Add("date", System.DateTime.Today);
		    d.Add("action","today");
                    DataTable dt_nonhandle_today = du.getDataTableBysp("QueryNonHandle", d);
                    d.Clear();
                    d.Add("date", System.DateTime.Today.AddDays(-1));
		    d.Add("action","yesterday");	
                    DataTable dt_nonhandle_yesterday = du.getDataTableBysp("QueryNonHandle", d);
                    if (dt_nonhandle_today.Rows.Count > 0)
                    {
                        string yesterday_status = Lib.SysSetting.GetJsonFormatData(dt_nonhandle_yesterday);
                        yesterday_status = yesterday_status.Substring(1, yesterday_status.Length - 1);
                        string today_status = Lib.SysSetting.GetJsonFormatData(dt_nonhandle_today);
                        today_status = today_status.Substring(0, today_status.Length - 1);
                        //string json = today_001.Substring(0, today_001.Length);
                        context.Response.Write(today_status + "," + yesterday_status);
                    }

                    break;
                default:
                    break;
            }
        }


    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}