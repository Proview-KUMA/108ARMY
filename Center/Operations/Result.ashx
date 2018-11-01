<%@ WebHandler Language="C#" Class="Result" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;



public class Result : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        if (context.Request.Params["type"] != null)
        {

            string type = context.Request.Params["type"];
            Lib.UnitTree Tree = new Lib.UnitTree();
            DataTable dt = Tree.GetUnitWithChild(context.Request.Params["unit_code"]).ChildUnitCodeTable;
            switch (type)
            {

                case "item":
                    d.Add("type", type);
                    d.Add("unit_code", context.Request.Params["unit_code"]);
                    d.Add("date_start", Lib.SysSetting.ToWorldDate(context.Request.Params["date_start"]));
                    d.Add("date_stop", Lib.SysSetting.ToWorldDate(context.Request.Params["date_stop"]));
                    DataSet ds_item = du.getDataSet("GetResultByUnit", d, "tempTable", dt);

                    // Get Male Data
                    string result1_item = Lib.SysSetting.GetJsonFormatData(ds_item.Tables[0]);
                    // Get Female Data
                    result1_item = result1_item.Substring(0, result1_item.Length - 1);
                    string result2_item = Lib.SysSetting.GetJsonFormatData(ds_item.Tables[1]);
                    result2_item = result2_item.Substring(1, result2_item.Length - 1);

                    context.Response.Write(result1_item + "," + result2_item);
                    break;

                case "age":
                    d.Add("type", type);
                    d.Add("unit_code", context.Request.Params["unit_code"]);
                    d.Add("date_start", Lib.SysSetting.ToWorldDate(context.Request.Params["date_start"]));
                    d.Add("date_stop", Lib.SysSetting.ToWorldDate(context.Request.Params["date_stop"]));
                    DataSet ds_age = du.getDataSet("GetResultByUnit", d, "tempTable", dt);

                    // Get Male Data
                    string result1_age = Lib.SysSetting.GetJsonFormatData(ds_age.Tables[0]);
                    // Get Female Data
                    result1_age = result1_age.Substring(0, result1_age.Length - 1);
                    string result2_age = Lib.SysSetting.GetJsonFormatData(ds_age.Tables[1]);
                    result2_age = result2_age.Substring(1, result2_age.Length - 1);

                    context.Response.Write(result1_age + "," + result2_age);
                    break;

                default:
                    break;
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