using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 抄寫三千跑步受測資料，本服務先將資料庫有重複的受測資料刪除後新增，成功回傳Done，餘回傳例外訊息
    /// </summary>
    /// <param name="dt">the record in result table</param>
    /// <returns>Done or ex message</returns>
    [WebMethod]
    public string AddResultFor3KRun(DataTable dt)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            SqlConnection con = new SqlConnection(du.connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Race_AddResult";
                cmd.Parameters.AddWithValue("result", dt);
                cmd.ExecuteNonQuery();
                con.Close();
                return "Done";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }
        else
        {
            return "not in race mode";
        }

    }
    /// <summary>
    /// 更新三千受測資料成績，成功回傳Done，餘回傳例外訊息
    /// </summary>
    /// <param name="dt">the record in result</param>
    /// <returns>Done or ex message</returns>
    [WebMethod]
    public string UpdateResultFor3KRun(DataTable dt)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            SqlConnection con = new SqlConnection(du.connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Race_UpdateResult3KRun";
                cmd.Parameters.AddWithValue("result", dt);
                DataTable dt_msg = new DataTable();
                dt_msg.Load(cmd.ExecuteReader());
                con.Close();
                if (dt_msg.Rows[0][0].ToString() == "Done")
                {
                    return "Done";
                }
                else
                {
                    return "Return Value Not Done";
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }
        else
        {
            return "not in race mode";
        }
    }
    /// <summary>
    /// 新增受測完畢資料
    /// </summary>
    /// <param name="dt">data table</param>
    /// <returns>Done or ex message</returns>
    [WebMethod]
    public string InsertResult(DataTable dt)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            SqlConnection con = new SqlConnection(du.connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Race_InsertResultBatch";
                cmd.Parameters.AddWithValue("result", dt);
                DataTable dt_msg = new DataTable();
                dt_msg.Load(cmd.ExecuteReader());
                con.Close();
                if (dt_msg.Rows[0][0].ToString() == "Done")
                {
                    return "Done";
                }
                else
                {
                    return "Return Value Not Done";
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }
        else
        {
            return "not in race mode";
        }
    }

    /// <summary>
    /// Show Player ID List Within Local DataBase Only For status = 1XX or 2XX
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataTable RacePlayerIDList()
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            DataTable dt = new DataTable();
            dt = new Lib.DataUtility().getDataTableByText("select distinct id,center_code from result where substring([status],1,1) in ('1','2')");
            dt.TableName = "PlayerIDList";
            return dt;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public string RacePlayerIDCount(string center_id)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            return new Lib.DataUtility().getDataTableByText("select count (distinct id) from result where substring([status],1,1) in ('1','2') and center_code = '" + center_id + "'").Rows[0][0].ToString();
        }
        else
        {
            return "not in race mode";
        }


    }

    [WebMethod]
    public DataTable RaceRecordByID(string id)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            DataTable dt = new DataTable();
            dt = new Lib.DataUtility().getDataTableByText(@"select id, name, birth, age, gender, unit_code, rank_code, height, weight, BMI, bodyfat, sit_ups, sit_ups_score, push_ups, push_ups_score, run, run_score,
	 date, center_code, result, status, op_id, clothesNum, LF_Tag_ID, UHF_Tag_ID, code, memo from result where substring([status],1,1) in ('1','2') and id = '" + id + "'");
            dt.TableName = "PlayerIDList";
            return dt;
        }
        else
        {
            return null;
        }
    }
}


