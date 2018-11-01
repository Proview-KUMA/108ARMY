<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Data;

public class ImageHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.Params["id"] != null)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
            d.Add("id", context.Request.Params["id"]);            
            d.Add("date", Lib.SysSetting.ToWorldDate(context.Request.Params["date"]));
            //d.Add("date", Convert.ToDateTime(context.Request.Params["date"]));
            DataTable dt = du.getDataTableByText("select photo from result where id = @id and date = @date",d);

            if (dt.Rows.Count == 1)
            {
                try
                {
                    byte[] data = (byte[])dt.Rows[0][0];

                    context.Response.ContentType = "image/bmp";

                    context.Response.BinaryWrite(data);
                }
                catch (Exception ex)
                {
                // 發生轉型失敗，代表資料庫沒有這個二進位資料
                }
            }
        }
        /*
        if (context.Request.Params["id"] != null)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
            d.Add("id", context.Request.Params["id"]);
            d.Add("date", Convert.ToDateTime(context.Request.Params["date"]));
            DataTable dt = du.getDataTableByText("select photo from result where id = @id and date = @date", "id", context.Request.Params["id"]);

            if (dt.Rows.Count == 1)
            {
                try
                {
                    byte[] data = (byte[])dt.Rows[0][0];

                    context.Response.ContentType = "image/bmp";

                    context.Response.BinaryWrite(data);
                }
                catch (Exception ex)
                {
                    // 發生轉型失敗，代表資料庫沒有這個二進位資料
                }
            }
        }
         * */
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}