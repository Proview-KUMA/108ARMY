using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DownLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void downLoad_Click(object sender, EventArgs e)
    {
        Lib.DataUtility du = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
        try
        {
            DataTable dt = new DataTable();
            MainWS.WebService MainWebService = new MainWS.WebService();
            dt = MainWebService.DownLoadResult(Lib.SysSetting.CenterCode);
            Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
            if (dt.Rows.Count != 0)
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("sid", row["sid"].ToString());
                    d.Add("id", row["id"].ToString());
                    d.Add("name", row["name"].ToString());
                    d.Add("gender", row["gender"].ToString());
                    d.Add("birth", row["birth"]);
                    d.Add("age", row["age"].ToString());
                    d.Add("unit_code", row["unit_code"].ToString());
                    d.Add("rank_code", row["rank_code"].ToString());
                    d.Add("date", Convert.ToDateTime(row["date"]));
                    d.Add("center_code", row["center_code"].ToString());
                    d.Add("status", "999");
                    d.Add("op_id", row["op_id"].ToString());
                    d.Add("sit_ups", row["sit_ups"]);
                    d.Add("sit_ups_score", row["sit_ups_score"]);
                    d.Add("push_ups", row["push_ups"]);
                    d.Add("push_ups_score", row["push_ups_score"]);
                    d.Add("run", row["run"]);
                    d.Add("run_score", row["run_score"]);
                    d.Add("memo", row["memo"]);
                    list.Add(d);
                }
                DataTable ds = local.getDataTableBysp("Download", list);
                int count = 0;
                foreach (DataRow DR in ds.Rows)
                {
                    count = count + Convert.ToInt32(DR[0]);
                }
                Dictionary<string, object> log_d = new Dictionary<string, object>();
                log_d.Add("date", DateTime.Now);
                log_d.Add("log", "已下載" + count.ToString() + "筆資料");
                log_d.Add("account", ((Lib.Center.Account_c)Session["account"]).Account);
                local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", log_d);
                log_d.Clear();
                list.Clear();
                dt.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('下載作業完成');", true);
                GridView1.DataBind();
            }
            else
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("date", DateTime.Now);
                d.Add("log", "目前沒有資料可以下載");
                d.Add("account", ((Lib.Center.Account_c)Session["account"]).Account);
                local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
                d.Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有資料可以下載');", true);
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message.Replace("\"", "").Replace("'", "") + "\");", true);
            System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
            Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
            d.Add("date", DateTime.Now);
            d.Add("log", ex.Message);
            d.Add("account", ((Lib.Center.Account_c)Session["account"]).Account);
            local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
        }
        //呼叫取得近七日測考人數方法
        //Update_7DayTable();
    }
    private static void Update_7DayTable()
    {
        Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
        System.Data.DataTable dt = new System.Data.DataTable();
        //需更新鑑測站web物件，重新參考服務web
        MainWS_KUMA_PC.WebService3 MainWebService = new MainWS_KUMA_PC.WebService3();
        //下面要改websv呼叫的方法
        dt = MainWebService.Get_7DayResultCount(Lib.SysSetting.CenterCode);//這裡要改呼叫Get_7DayResultCount
        if (dt.Rows.Count != 0)
        {
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> list = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
                d.Add("date", row["date"].ToString());
                d.Add("count", row["count"].ToString());

                list.Add(d);
            }
            System.Data.DataTable ds = local.getDataTableBysp("Ex107_Update_7DayTable", list);
            list.Clear();
            dt.Dispose();
        }
    }
    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
