<%@ WebHandler Language="C#" Class="GetValueByCode" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;

public class GetValueByCode : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (context.Request.Params["mode"] != null)
        {
            string mode = context.Request.Params["mode"];
            string json = string.Empty;
            Lib.DataUtility du = new Lib.DataUtility();
            System.Data.DataTable dt = new System.Data.DataTable();
            string value = context.Request.Params["value"];
            Dictionary<string, object> d = new Dictionary<string, object>();
            switch (mode)
            {

                case "rank":
                    dt = du.getDataTableBysp("getrank", "rank_code", value);
                    if (dt.Rows.Count == 0)
                    {
                        d.Add("status", "false");
                    }
                    else
                    {
                        d.Add("status", dt.Rows[0]["rank_title"].ToString());
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "center":
                    d.Add("id", value);
                    dt = du.getDataTableByText("select center_name from center where center_code = @id", d);
                    json = Lib.SysSetting.GetJsonFormatData(dt);
                    break;

                case "accExist":
                    dt = du.getDataTableBysp("CheckAccExist", "acc", value);
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        d.Add("status", "flase");
                    }
                    else
                    {
                        d.Add("status", "true");
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "askaccExist":
                    dt = du.getDataTableBysp("CheckAskExist", "id", value);
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        d.Add("status", "flase");
                    }
                    else
                    {
                        d.Add("status", "true");
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "playerExist":
                    dt = du.getDataTableBysp("CheckPlaExist", "id", value);
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        d.Add("status", "flase");
                    }
                    else
                    {
                        d.Add("status", "true");
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "QueryplayerExist"://人工鑑測成績輸入：查詢人員基本資料
                    try
                    {
                        MainScoreWS.WebService2 _Web = new MainScoreWS.WebService2();
                        dt = _Web.QueryPlayer(context.Request.Params["id"].ToString(), context.Request.Params["date"].ToString());
                        if (dt.Rows[0]["status"].ToString() != "OK")
                        {
                            d.Add("status", dt.Rows[0]["status"].ToString());
                        }
                        else
                        {
                            Dictionary<string, object> d_title = new Dictionary<string, object>();
                            d_title.Add("unit_code", dt.Rows[0]["unit_code"].ToString());
                            DataTable dt_unit_title = du.getDataTableByText(@"select unit_title from Unit where unit_code=@unit_code", d_title);//查詢單位名稱
                            d_title.Clear();
                            d_title.Add("rank_code", dt.Rows[0]["rank_code"].ToString());
                            DataTable dt_rank_title = du.getDataTableByText(@"select rank_title from Rank where rank_code=@rank_code", d_title);//查詢階級名稱
                            d.Add("status", "true");
                            d.Add("gender", dt.Rows[0]["gender"].ToString());
                            d.Add("birth", dt.Rows[0]["birth"].ToString());
                            d.Add("name", dt.Rows[0]["name"].ToString());
                            d.Add("unit_code", dt.Rows[0]["unit_code"].ToString());
                            d.Add("rank_code", dt.Rows[0]["rank_code"].ToString());
                            d.Add("unit_title", dt_unit_title.Rows[0]["unit_title"].ToString());
                            d.Add("rank_title", dt_rank_title.Rows[0]["rank_title"].ToString());
                        }
                        json = Lib.SysSetting.GetJsonFormatData(d);
                    }
                    catch (Exception ex)
                    {
                        d.Add("status", ex.Message);
                        json = Lib.SysSetting.GetJsonFormatData(d);
                    }
                    break;
                case "GetSitUpsItem":
                    dt = du.getDataTableBysp("Ex104_GetRepMentByScoreTrail", "Gender", value);
                    foreach (DataRow dr in dt.Rows)
                    {
                        d.Add(dr[0].ToString(), dr[1].ToString());
                    }
                    json = Lib.SysSetting.GetJsonFormatData(dt);
                    break;
                case "GetItemUnit":
                    dt = du.getDataTableBysp("Ex104_GetRepMentNote", "sid", value);
                    if (dt.Rows.Count == 0)
                    {
                        d.Add("status", "false");
                    }
                    else
                    {
                        d.Add("status", dt.Rows[0][0].ToString());
                    }

                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;
                case "InsertScore":
                    Dictionary<string, object> paralist = new Dictionary<string, object>();
                    paralist.Add("id", context.Request.Params["id"]);
                    paralist.Add("name", context.Request.Params["name"]);d.Add("name", context.Request.Params["name"]);
                    paralist.Add("birth", Lib.SysSetting.ToWorldDate(context.Request.Params["birth"].Replace('-', '/')));
                    paralist.Add("gender", context.Request.Params["gender"]);
                    paralist.Add("unit_code", context.Request.Params["unit_code"]);
                    paralist.Add("rank_code", context.Request.Params["rank_code"]);
                    paralist.Add("sit_ups", context.Request.Params["sit_ups"]);
                    paralist.Add("push_ups", context.Request.Params["push_ups"]);
                    paralist.Add("run", context.Request.Params["run"]);
                    paralist.Add("date", context.Request.Params["date"]);
                    paralist.Add("memo", context.Request.Params["memo"]);
                    paralist.Add("age", Lib.SysSetting.ConvertAge(Lib.SysSetting.ToWorldDate(context.Request.Params["birth"].Replace('-', '/')), Convert.ToDateTime(context.Request.Params["date"])).ToString());
                    paralist.Add("center_code", Lib.SysSetting.CenterCode);
                    //paralist.Add("center_code", "10");
                    dt = du.getDataTableByText(@"Select * from Result where id = @id and date = @date", paralist);
                    if (dt.Rows.Count == 0)
                    {
                        try
                        {
                            du.executeNonQueryBysp("Ex104_InsertScore", paralist);
                            paralist.Clear();
                            paralist.Add("id", context.Request.Params["id"]);
                            paralist.Add("date", context.Request.Params["date"]);

                            DateTime Change_time = new DateTime(2018, 12, 31, 23, 59, 59);//檢查時間 2018年12月31日
                            DateTime Now_time = Convert.ToDateTime(context.Request.Params["date"].ToString());
                            if (Now_time > Change_time)//如果時間大於設定時間
                            {
                                //新版-2019年1月1日開始啟用之成績結算sp
                                du.executeNonQueryBysp("Ex108_CalResultByID", paralist);
                            }
                            else
                            {
                                //舊版2017年之前用
                                du.executeNonQueryBysp("Ex106_CalResultByID", paralist);
                            }
                            d.Add("status", "OK");
                        }
                        catch (Exception ex)
                        {
                            d.Add("status", ex.Message);
                        }

                    }
                    else
                    {
                        d.Add("status", "[鑑測站]當日已有一筆鑑測資料");
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "unit":
                    dt = du.getDataTableBysp("GetUnitName", "unit_code", value);

                    d.Add("status", dt.Rows[0][0].ToString());

                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "parent_unit":
                    bool isFind = false;
                    DataTable _dt = du.getDataTableByText("select service_code from Unit where unit_code = @unit_code", "unit_code", value);
                    if (_dt.Rows.Count > 0)
                    {
                        dt = du.getDataTableByText("select distinct service_code from Account a , Unit u where a.role_code = @role_code and a.unit_code = u.unit_code", "role_code", "2");
                        foreach (DataRow row in dt.Rows)
                        {
                            if (_dt.Rows[0]["service_code"].ToString() == row["service_code"].ToString())
                            {
                                isFind = true;
                            }
                        }
                    }
                    if (isFind)
                    {
                        dt = du.getDataTableBysp("GetUnitName", "unit_code", value);
                        d.Add("status", dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        d.Add("status","");
                    }
                    json = Lib.SysSetting.GetJsonFormatData(d);
                    break;

                case "centerLimit":
                    d.Add("center_code", context.Request.Params["center_code"]);
                    string _v = context.Request.Params["date"];
                    string _date = string.Empty;
                    string[] operater = { "/" };
                    string[] info = _v.Split(operater, StringSplitOptions.None);
                    _date = (Convert.ToInt32(info[0]) + 1911).ToString() + "/" + info[1] + "/" + info[2];
                    //try
                    //{
                    //    _date = (Convert.ToInt32(_v.Substring(0, 2)) + 1911).ToString() + _v.Substring(2, _v.Length - 2);
                    //}
                    //catch (Exception ex)
                    //{
                    //    _date = (Convert.ToInt32(_v.Substring(0, 3)) + 1911).ToString() + _v.Substring(3, _v.Length - 3);
                    //}
                    d.Add("date", Convert.ToDateTime(_date));
                    dt = du.getDataTableBysp("GetCenterLimit", d);
                    //json = _date;
                    json = "{\"status\":\"" + dt.Rows[0][0].ToString() + "\"}";
                    break;

                case "replaceitem":
                    try
                    {
                        d.Add("center_code", context.Request.Params["center_code"]);
                        dt = du.getDataTableByText("select IsSwin from Center where center_code =@center_code", d);
                        if (Convert.ToBoolean(dt.Rows[0]["IsSwin"]))
                        {
                            d.Clear();
                            dt = du.getDataTableByText("select rep_title from RepMent", d);
                        }
                        else
                        {
                            d.Clear();
                            d.Add("IsSwinItem", false);
                            dt = du.getDataTableByText("select rep_title from RepMent where IsSwinItem =@IsSwinItem", d);
                        }
                        string _result = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _result = _result + dt.Rows[i]["rep_title"].ToString() + "<br />";
                        }
                        json = "{\"status\":\"" + _result + "\"}";
                    }
                    catch(Exception ex)
                    {

                    }
                    break;

                case "player" :
                    d.Add("id", value);
                    dt = du.getDataTableByText("select id from player where id = @id", d);
                    if (dt.Rows.Count == 0)
                    {
                        json = "{\"status\":\"none\"}";
                    }
                    else
                    {
                        json = "{\"status\":\"false\"}";
                    }
                    break;

                case "iprenew":
                    try
                    {
                        du.executeNonQueryBysp("iprenew", "apply", value);
                        json = "{\"status\":\"done\"}";

                    }
                    catch (Exception ex)
                    {
                        json = "{\"status\":\"" + ex.Message + "\"}";
                    }
                    break;

                case "deletesch":
                    try
                    {
                        du.executeNonQueryByText("delete daterule where sid = @sid", "sid", value);
                        Lib.SysSetting.AddLog("鑑測站", context.Request.Params["op_id"], "日期規則被刪除", DateTime.Now);
                        json = "{\"status\":\"done\"}";
                    }

                    catch (Exception ex)
                    {
                        json = "{\"status\":\"" + ex.Message + "\"}";
                    }
                    break;
                case "changelimit":
                    try
                    {
                        du.executeNonQueryByText("update center set limit = " + value + " where center_code = '" + context.Request.Params["sid"] + "'");
                        Lib.SysSetting.AddLog("鑑測站", context.Request.Params["who"], " 鑑測站 (代碼 " + context.Request.Params["sid"] + ") 人數限制被更改為 " + value , DateTime.Now);
                        json = "{\"status\":\"done\"}";
                    }
                    catch (Exception ex)
                    {
                        json = "{\"status\":\"" + ex.Message + "\"}";
                    }
                    break;
                default:
                    break;
            }

            d.Clear();
            dt.Dispose();
            context.Response.Write(json);
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