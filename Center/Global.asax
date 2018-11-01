<%@ Application Language="C#" %>

<script runat="server">
    public static bool IsON = false;
    public static bool IsDid = false;
    void Application_Start(object sender, EventArgs e) 
    {
        //// 應用程式啟動時執行的程式碼
        //System.Timers.Timer timer = new System.Timers.Timer(10000);
        //timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        //timer.Interval = 1 * 60 * 1000; // 每分鐘 1000代表1秒x60代表1分鐘作一次
        //timer.Enabled = true;
        
        ////2017-11-8新增計時器，抓取未來七日測考人數
        //System.Timers.Timer Get7day_timer = new System.Timers.Timer(600000);//10分鐘做一次
        //Get7day_timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Get7dayResultCount);//2017-11-8新增事件
        //Get7day_timer.Enabled = true;
        
        //Lib.DataUtility du = new Lib.DataUtility();
        //System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
        //d.Add("date", DateTime.Now);
        //d.Add("log", "定時器已經啟動!!");
        //d.Add("account", "系統");
        //du.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
        //if (Session["Player"] == null)
        //{
        //    Lib.SysSetting.ExceptionLog("player session null", "session null", sender.ToString());
        //}
        //呼叫取得近七日測考人數方法
        //Update_7DayTable();
    }

    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式關閉時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message,sender.ToString());
        Server.ClearError();
        Response.Redirect("~/ExLogView.aspx");
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。

    }
    //2017-11-8新增自動抓取未來七日測考人數
    private static void timer_Get7dayResultCount(object source, System.Timers.ElapsedEventArgs e)
    {
        if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 6)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            System.Data.DataTable dt = new System.Data.DataTable();
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            dt = du.getDataTableBysp("Ex107_Check_Update_7day");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["change"].ToString() == "1")//要更新未來七日表格
                {
                    Update_7DayTable();
                }
            }
        }
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

    private static void timer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
    {
        if (DateTime.Now.Hour == 23)
            IsDid = false;
        if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 20)
        {
            if (!IsON && !IsDid)
            {
                Lib.SysSetting.SystemMode myMode = Lib.SysSetting.CurrentSystemMode();
                if (myMode == Lib.SysSetting.SystemMode.Normal)
                {
                    ToDoDownLoad();
                    //"鑑測系統模式"
                }
                if (myMode == Lib.SysSetting.SystemMode.Race)
                {
                   //"競賽系統模式"
                }
                if (myMode == Lib.SysSetting.SystemMode.Original)
                {
                    ToDoDownLoad();
                    //"此系統只有鑑測功能"
                }                
            }
        }
    }

    private static void ToDoDownLoad()
    {
        IsON = true;
        Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            MainWS.WebService MainWebService = new MainWS.WebService();
            //2017-11-7新版的自動下載改呼叫Auto_DownLoadResult()，需更新web服務參考
            dt = MainWebService.DownLoadResult(Lib.SysSetting.CenterCode);
            IsDid = true;
            //Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
            if (dt.Rows.Count != 0)
            {
                System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> list = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
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
                System.Data.DataTable ds = local.getDataTableBysp("Download", list);
                int count = 0;
                foreach (System.Data.DataRow DR in ds.Rows)
                {
                    count = count + Convert.ToInt32(DR[0]);
                }
                System.Collections.Generic.Dictionary<string, object> log_d = new System.Collections.Generic.Dictionary<string, object>();
                log_d.Add("date", DateTime.Now);
                log_d.Add("log", "已下載" + count.ToString() + "筆資料");
                log_d.Add("account", "系統");
                local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", log_d);
                log_d.Clear();
                list.Clear();
                dt.Dispose();
            }
            else
            {
                System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
                d.Add("date", DateTime.Now);
                d.Add("log", "目前沒有資料可以下載");
                d.Add("account", "系統");
                local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
                d.Clear();
                //GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
            d.Add("date", DateTime.Now);
            d.Add("log", ex.Message);
            d.Add("account", "系統");
            local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, "Global.asax");
        }
        IsON = false;
    }

    /*
#region old version
if (DateTime.Now < DateTime.Now.Date.AddHours(19) && DateTime.Now > DateTime.Now.Date.AddHours(18))
{
    //TimeWS.TimeWS ws = new TimeWS.TimeWS();
    //string hour = ws.ReservseTime(); // 取得報名截止時間單位
    string hour = "12";
    DateTime _date = DateTime.Now.Date.AddDays(1);
    DateTime deadline = _date.AddHours(-Convert.ToDouble(hour));
    DateTime targetTime = DateTime.Now.Date;
    if (DateTime.Now > deadline) // 如果現在已經超過明天報名截止時間，可以取得明日以前資料
    {
        targetTime = _date.AddDays(1);
    }
    else // 否則只能取得今日以前資料
    {
        //targetTime remain 
    }
    Lib.DataUtility du = new Lib.DataUtility(Lib.DataUtility.ConnectionType.MainDB);
    Lib.DataUtility local = new Lib.DataUtility(Lib.DataUtility.ConnectionType.CenterDB);
    try
    {   
        System.Data.DataTable dt = du.getDataTableByText("select id, name, gender, unit_code, rank_code, date, center_code, op_id from result where center_code = @center_code and status = '000' and date <= '" + targetTime.ToString() + "'", "center_code", Lib.SysSetting.CenterCode);
                
        if (dt.Rows.Count != 0)
        {
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> list = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
                d.Add("id", row["id"].ToString());
                d.Add("name", row["name"].ToString());
                d.Add("gender", row["gender"].ToString());
                d.Add("unit_code", row["unit_code"].ToString());
                d.Add("rank_code", row["rank_code"].ToString());
                d.Add("date", Convert.ToDateTime(row["date"]));
                d.Add("center_code", row["center_code"].ToString());
                d.Add("status", "999");
                d.Add("op_id", row["op_id"].ToString());
                list.Add(d);
            }
            local.executeNonQueryByText("insert into result (id, name, gender, unit_code, rank_code, date, center_code, status, op_id) values(@id, @name, @gender, @unit_code, @rank_code, @date, @center_code, @status, @op_id)", list);
            du.executeNonQueryByText("update result set status = '999' where center_code = @center_code and status = '000' and date <= '" + targetTime.ToString() + "'", "center_code", Lib.SysSetting.CenterCode);
            System.Collections.Generic.Dictionary<string, object> log_d = new System.Collections.Generic.Dictionary<string, object>();
            log_d.Add("date", DateTime.Now);
            log_d.Add("log", "已下載" + dt.Rows.Count.ToString() + "筆資料");
            log_d.Add("account", "系統");
            local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", log_d);
            log_d.Clear();
            list.Clear();
            dt.Dispose();
        }
        else
        {
            System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
            d.Add("date", DateTime.Now);
            d.Add("log", "目前沒有資料可以下載");
            d.Add("account", "系統");
            local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
            d.Clear();
        }
    }
    catch (Exception ex)
    {
        System.Collections.Generic.Dictionary<string, object> d = new System.Collections.Generic.Dictionary<string, object>();
        d.Add("date", DateTime.Now);
        d.Add("log", ex.Message);
        d.Add("account", "系統");
        local.executeNonQueryByText("insert into downloadlog values (@date,@log,@account)", d);
    }
}
#endregion
 */
</script>



