<%@ WebHandler Language="C#" Class="Race_Status" %>

using System;
using System.Web;
using System.IO;
using System.Data;

public class Race_Status : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Lib.DataUtility du = new Lib.DataUtility();

        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (context.Request.QueryString["type"] == null)
            {

                sql = @"select distinct r.unit_code [單位代碼], dbo.F_GetUnitTitle(r.unit_code) [單位], COUNT(id) [總數],
sum((case when status = '999' then 1 else 0 end)) [未檢錄],
sum((case when status = '001' then 1 else 0 end)) [已檢錄],
sum((case when SUBSTRING(r.status,3,1) = '2' then 1 else 0 end)) [合格],
sum((case when SUBSTRING(r.status,3,1) = '3' then 1 else 0 end)) [不合格],
sum((case when SUBSTRING(r.status,3,1) = '4' then 1 else 0 end)) [事故],
sum((case when SUBSTRING(r.result,1,1) = '1' then 1 else 0 end)) [個人組],
sum((case when SUBSTRING(r.result,1,1) = '2' then 1 else 0 end)) [團體組],
sum((case when SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [志願役],
sum((case when SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [義務役],
sum((case when SUBSTRING(r.result,3,1) != '6' then 1 else 0 end)) [未滿半年],
sum((case when SUBSTRING(r.status,1,1) = '1' then 1 else 0 end)) [未上傳],
sum((case when SUBSTRING(r.status,1,1) = '2' then 1 else 0 end)) [已上傳],
sum((case when r.gender = 'M' then 1 else 0 end)) [男性],
sum((case when r.gender = 'F' then 1 else 0 end)) [女性],
sum((case when r.memo != '000' then 1 else 0 end)) [替代項目],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [男性志願役],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [男性義務役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [女性志願役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [女性義務役]
from Result r --where unit_code like '1%'
group by r.unit_code
union
select '總計' [單位代碼], '' [單位], COUNT(id) [總數],
sum((case when status = '999' then 1 else 0 end)) [未檢錄],
sum((case when status = '001' then 1 else 0 end)) [已檢錄],
sum((case when SUBSTRING(r.status,3,1) = '2' then 1 else 0 end)) [合格],
sum((case when SUBSTRING(r.status,3,1) = '3' then 1 else 0 end)) [不合格],
sum((case when SUBSTRING(r.status,3,1) = '4' then 1 else 0 end)) [事故],
sum((case when SUBSTRING(r.result,1,1) = '1' then 1 else 0 end)) [個人組],
sum((case when SUBSTRING(r.result,1,1) = '2' then 1 else 0 end)) [團體組],
sum((case when SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [志願役],
sum((case when SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [義務役],
sum((case when SUBSTRING(r.result,3,1) != '6' then 1 else 0 end)) [未滿半年],
sum((case when SUBSTRING(r.status,1,1) = '1' then 1 else 0 end)) [未上傳],
sum((case when SUBSTRING(r.status,1,1) = '2' then 1 else 0 end)) [已上傳],
sum((case when r.gender = 'M' then 1 else 0 end)) [男性],
sum((case when r.gender = 'F' then 1 else 0 end)) [女性],
sum((case when r.memo != '000' then 1 else 0 end)) [替代項目],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [男性志願役],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [男性義務役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [女性志願役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [女性義務役]
from Result r
order by [單位代碼]";
            }
            else if (context.Request.QueryString["type"].ToString() == "final")
            {
                sql = @"select '總計' [單位代碼], (select center_name from center where center_code = '" + Lib.SysSetting.CenterCode + @"') [單位], COUNT(id) [總數],
sum((case when status = '999' then 1 else 0 end)) [未檢錄],
sum((case when status = '001' then 1 else 0 end)) [已檢錄],
sum((case when SUBSTRING(r.status,3,1) = '2' then 1 else 0 end)) [合格],
sum((case when SUBSTRING(r.status,3,1) = '3' then 1 else 0 end)) [不合格],
sum((case when SUBSTRING(r.status,3,1) = '4' then 1 else 0 end)) [事故],
sum((case when SUBSTRING(r.result,1,1) = '1' then 1 else 0 end)) [個人組],
sum((case when SUBSTRING(r.result,1,1) = '2' then 1 else 0 end)) [團體組],
sum((case when SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [志願役],
sum((case when SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [義務役],
sum((case when SUBSTRING(r.result,3,1) != '6' then 1 else 0 end)) [未滿半年],
sum((case when SUBSTRING(r.status,1,1) = '1' then 1 else 0 end)) [未上傳],
sum((case when SUBSTRING(r.status,1,1) = '2' then 1 else 0 end)) [已上傳],
sum((case when r.gender = 'M' then 1 else 0 end)) [男性],
sum((case when r.gender = 'F' then 1 else 0 end)) [女性],
sum((case when r.memo != '000' then 1 else 0 end)) [替代項目],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [男性志願役],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [男性義務役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [女性志願役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [女性義務役]
from Result r";
            }
            else if (context.Request.QueryString["type"].ToString() != "final")
            {
                sql = @"select '總計' [單位代碼], dbo.F_GetUnitTitle('" + context.Request.QueryString["type"].ToString() + @"') [單位], COUNT(id) [總數],
sum((case when status = '999' then 1 else 0 end)) [未檢錄],
sum((case when status = '001' then 1 else 0 end)) [已檢錄],
sum((case when SUBSTRING(r.status,3,1) = '2' then 1 else 0 end)) [合格],
sum((case when SUBSTRING(r.status,3,1) = '3' then 1 else 0 end)) [不合格],
sum((case when SUBSTRING(r.status,3,1) = '4' then 1 else 0 end)) [事故],
sum((case when SUBSTRING(r.result,1,1) = '1' then 1 else 0 end)) [個人組],
sum((case when SUBSTRING(r.result,1,1) = '2' then 1 else 0 end)) [團體組],
sum((case when SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [志願役],
sum((case when SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [義務役],
sum((case when SUBSTRING(r.result,3,1) != '6' then 1 else 0 end)) [未滿半年],
sum((case when SUBSTRING(r.status,1,1) = '1' then 1 else 0 end)) [未上傳],
sum((case when SUBSTRING(r.status,1,1) = '2' then 1 else 0 end)) [已上傳],
sum((case when r.gender = 'M' then 1 else 0 end)) [男性],
sum((case when r.gender = 'F' then 1 else 0 end)) [女性],
sum((case when r.memo != '000' then 1 else 0 end)) [替代項目],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [男性志願役],
sum((case when r.gender = 'M' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [男性義務役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'A' then 1 else 0 end)) [女性志願役],
sum((case when r.gender = 'F' and SUBSTRING(r.result,2,1) = 'B' then 1 else 0 end)) [女性義務役]
from Result r where r.unit_code = '" + context.Request.QueryString["type"].ToString() + "'";
            }

            try
            {
                dt = new Lib.DataUtility().getDataTableByText(sql);
                dt.TableName = "RaceStatus";
                string json = string.Empty;
                json = Lib.SysSetting.GetJsonFormatData(dt);
                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                context.Response.Write("{'exception':'" + ex.Message + "'}");
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