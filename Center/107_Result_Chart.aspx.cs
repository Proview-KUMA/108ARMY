using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Lib;

public partial class _107_Result_Chart : System.Web.UI.Page
{
    
    private static string OtherItem_Date;
    private static string ItemPassRate_Date;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Update_7DayTable();//先取得最新七日人數
        Get_chart_7DayPlayerCount();//第一頁
        Get_chart_7DayInTest();//第二頁
        Get_chart_7DaySeleteOther();//第三頁
        
        if (!IsPostBack)
        {
            //第四頁
            OtherItem_Date = DateTime.Now.ToString("yyyy/MM/dd");
            txb_OtherItem_Date.Text = OtherItem_Date;
            Get_chart_SeleteOtherItem(OtherItem_Date);
            //第五頁
            ItemPassRate_Date = DateTime.Now.ToString("yyyy/MM/dd");
            txb_ItemPassRate_Date.Text = ItemPassRate_Date;
            Get_ItemPass(ItemPassRate_Date);
        }
        else
        {
            DateTime chdt1;
            DateTime chdt2;
            if (DateTime.TryParse(txb_OtherItem_Date.Text, out chdt1) == true)//檢查日期格式是否正確
            {
                OtherItem_Date = txb_OtherItem_Date.Text;
                Get_chart_SeleteOtherItem(OtherItem_Date);
            }
            if (DateTime.TryParse(txb_ItemPassRate_Date.Text, out chdt2) == true)//檢查日期格式是否正確
            {
                ItemPassRate_Date = txb_ItemPassRate_Date.Text;
                Get_ItemPass(ItemPassRate_Date);
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
    //取得未來七日來測人數
    private void Get_chart_7DayPlayerCount()
    {
        DataUtility du = new DataUtility();
        DataTable dt = new DataTable();
        dt = du.getDataTableByText("select top 7 * from Count7day where date>=convert(varchar, getdate(), 111) order by date");
        if (dt.Rows.Count > 0)
        {
            string[] xValues = new string[dt.Rows.Count];
            int[] yValues = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xValues[i] =Convert.ToDateTime(dt.Rows[i]["date"]).ToString("MM/dd");
                yValues[i] = Convert.ToInt16(dt.Rows[i]["count"].ToString());
            }
            //string[] xValues = { "11/01", "11/02", "11/03", "11/04", "11/05", "11/06", "11/07", };
            //int[] yValues = { 100, 94, 150, 120, 95, 88, 131 };
            string[] titleArr = { "人數" };

            //折線圖
            //設定顯示圖形的樣式類別
            chart_7DayPlayerCount.Series["Line"].ChartType = SeriesChartType.Line;
            chart_7DayPlayerCount.Legends["Line"].Alignment = StringAlignment.Far;//圖例距離圖形(遠)
            //chart_7DayPlayerCount.Legends["Line"].BackColor = Color.FromArgb(235, 235, 235); //背景色
            //斜線背景
            //chart_7DayPlayerCount.Legends["Line"].BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;
            //chart_7DayPlayerCount.Legends["Line"].BorderWidth = 2;
            //chart_7DayPlayerCount.Legends["Line"].BorderColor = Color.FromArgb(200, 200, 200);

            chart_7DayPlayerCount.Series["Line"].Points.DataBindXY(xValues, yValues);
            chart_7DayPlayerCount.Series["Line"].Legend = "Line";
            chart_7DayPlayerCount.Series["Line"].LegendText = titleArr[0];
            chart_7DayPlayerCount.ChartAreas["LineChartArea"].AxisX.Title = "日期";
            chart_7DayPlayerCount.ChartAreas["LineChartArea"].AxisY.Title = "人數";
            chart_7DayPlayerCount.ChartAreas["LineChartArea"].AxisY.TextOrientation = TextOrientation.Stacked;//將文字變橫向顯示
        }
        
    }

    //取得近七日到測率
    private void Get_chart_7DayInTest()
    {
        DataUtility du = new DataUtility();
        DataTable AllTest_dt = new DataTable();//查詢日期及應測人數
        DataTable NoTest_dt = new DataTable();//沒來測的
        Dictionary<string, object> d = new Dictionary<string, object>();
        AllTest_dt = du.getDataTableBysp("Ex107_Get_7day_AllTest");
        if (AllTest_dt.Rows.Count > 0)
        {
            string[] xValues = new string[AllTest_dt.Rows.Count];
            int[] yValues_All = new int[AllTest_dt.Rows.Count];
            int[] yValues_InTest = new int[AllTest_dt.Rows.Count];
            double[] yValues_Test_Rate = new double[AllTest_dt.Rows.Count];
            for (int i = AllTest_dt.Rows.Count-1; i >= 0; i--)
            {
                xValues[AllTest_dt.Rows.Count-(i+1)] = Convert.ToDateTime(AllTest_dt.Rows[i]["date"]).ToString("MM/dd");
                yValues_All[AllTest_dt.Rows.Count-(i+1)] = Convert.ToInt16(AllTest_dt.Rows[i]["alltest"].ToString());
                d.Add("date", Convert.ToDateTime(AllTest_dt.Rows[i]["date"]).ToString("yyyy/MM/dd"));
                NoTest_dt = du.getDataTableBysp("Ex107_Get_7day_NoTest", d);
                if (NoTest_dt.Rows.Count > 0)
                {
                    yValues_InTest[AllTest_dt.Rows.Count-(i+1)] = (Convert.ToInt16(AllTest_dt.Rows[i]["alltest"].ToString()) - Convert.ToInt16(NoTest_dt.Rows[0]["notest"].ToString()));
                    yValues_Test_Rate[AllTest_dt.Rows.Count-(i+1)]=Math.Round((Convert.ToDouble(yValues_InTest[AllTest_dt.Rows.Count-(i+1)]) / Convert.ToDouble(yValues_All[AllTest_dt.Rows.Count-(i+1)])) * 100, 1);
                }
                else
                {
                    yValues_InTest[AllTest_dt.Rows.Count-(i+1)] = Convert.ToInt16(AllTest_dt.Rows[i]["alltest"].ToString());
                    yValues_Test_Rate[AllTest_dt.Rows.Count - (i + 1)] = 100;
                }
                NoTest_dt.Clear();
                d.Clear();
            }
            string[] titleArr = { "到測率(%)" };

            

            //柱狀圖
            //設定 Legends------------------------------------------------------------------------                
            chart_7DayInTest.Legends["Column"].DockedToChartArea = "ColumnChartArea"; //顯示在圖表內
            //chart_7DayInTest.Legends["Column"].Alignment = StringAlignment.Far;//圖例距離圖形(遠)
            
            //繪製的樣式
            chart_7DayInTest.Series["Column"]["DrawingStyle"] = "Cylinder";
            chart_7DayInTest.Series["Column"].BorderWidth = 0;//設定線粗
            //設定圖例顯示的文字
            //chart_7DayInTest.Series["Column"].LegendText = "到測率(%)";
            //chart_7DayInTest.Series["Column"].LegendText = "實到";
            //設定顯示圖形的樣式類別
            //chart_7DayPlayerCount.Series["Column"].ChartType = SeriesChartType.Column;

            chart_7DayInTest.Series["Column"].Points.DataBindXY(xValues, yValues_Test_Rate);
            //chart_7DayInTest.Series["Column2"].Points.DataBindY(yValues_InTest);
            chart_7DayInTest.Series["Column"].Legend = "Column";
            chart_7DayInTest.Series["Column"].LegendText = titleArr[0];
            //chart_7DayInTest.Series["Column2"].LegendText = titleArr[1];
            //chart_7DayInTest.ChartAreas["ColumnChartArea"].Area3DStyle.Enable3D = true;
            chart_7DayInTest.ChartAreas["ColumnChartArea"].AxisX.Title = "日期";
            chart_7DayInTest.ChartAreas["ColumnChartArea"].AxisY.Title = "到測率%";
            chart_7DayInTest.ChartAreas["ColumnChartArea"].AxisY.TextOrientation = TextOrientation.Stacked;//將文字變橫向顯示
            chart_7DayInTest.ChartAreas["ColumnChartArea"].AxisY.Maximum = 100;//設定Y軸最大值

            //打開圖例
            chart_7DayInTest.Series["Column"].IsValueShownAsLabel = true;
            chart_7DayInTest.Series["Column"].IsVisibleInLegend = false;
            //chart_7DayInTest.Series["Column2"].IsValueShownAsLabel = true;

            //設定圖形X軸的顯示文字
            foreach (DataPoint dp in chart_7DayInTest.Series["Column"].Points)
            {
                dp.Label = dp.YValues[0].ToString() + "%";
            }
            //資料放入表格
            if (xValues.Length > 0)
            {
                lab_Test_date1.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-1]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Test_AllTest1.Text = yValues_All[0].ToString();//應到
                lab_Test_InTest1.Text = yValues_InTest[0].ToString();//實到
                lab_Test_Rate1.Text = yValues_Test_Rate[0].ToString();//到測率
            }            
            if (xValues.Length > 1)
            {
                lab_Test_date2.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-2]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest2.Text = yValues_All[1].ToString();
                lab_Test_InTest2.Text = yValues_InTest[1].ToString();
                lab_Test_Rate2.Text = yValues_Test_Rate[1].ToString();
            }            
            if (xValues.Length > 2)
            {
                lab_Test_date3.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-3]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest3.Text = yValues_All[2].ToString();
                lab_Test_InTest3.Text = yValues_InTest[2].ToString();
                lab_Test_Rate3.Text = yValues_Test_Rate[2].ToString();
            }            
            if (xValues.Length > 3)
            {
                lab_Test_date4.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-4]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest4.Text = yValues_All[3].ToString();
                lab_Test_InTest4.Text = yValues_InTest[3].ToString();
                lab_Test_Rate4.Text = yValues_Test_Rate[3].ToString();
            }            
            if (xValues.Length > 4)
            {
                lab_Test_date5.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-5]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest5.Text = yValues_All[4].ToString();
                lab_Test_InTest5.Text = yValues_InTest[4].ToString();
                lab_Test_Rate5.Text = yValues_Test_Rate[4].ToString();
            }            
            if (xValues.Length > 5)
            {
                lab_Test_date6.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-6]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest6.Text = yValues_All[5].ToString();
                lab_Test_InTest6.Text = yValues_InTest[5].ToString();
                lab_Test_Rate6.Text = yValues_Test_Rate[5].ToString();
            }  
            if (xValues.Length > 6)
            {
                lab_Test_date7.Text = Convert.ToDateTime(AllTest_dt.Rows[xValues.Length-7]["date"]).ToString("yyyy/MM/dd");
                lab_Test_AllTest7.Text = yValues_All[6].ToString();
                lab_Test_InTest7.Text = yValues_InTest[6].ToString();
                lab_Test_Rate7.Text = yValues_Test_Rate[6].ToString();
            }
        }
        
    }

    //取得近七日多元選項比例
    private void Get_chart_7DaySeleteOther()
    {
        DataUtility du = new DataUtility();
        DataTable AllItem_dt = new DataTable();//查詢日期及第三項基本+多元人數
        DataTable OtherItem_dt = new DataTable();//多元人數
        Dictionary<string, object> d = new Dictionary<string, object>();
        AllItem_dt = du.getDataTableBysp("Ex107_Get_7day_AllItemTest");

        if (AllItem_dt.Rows.Count > 0)
        {
            string[] xValues_date = new string[AllItem_dt.Rows.Count];
            string[] titleArr = { "多元比例(%)" };
            int[] yValues_AllItem = new int[AllItem_dt.Rows.Count];
            int[] yValues_OtherItem = new int[AllItem_dt.Rows.Count];
            double[] yValues_OtherRate = new double[AllItem_dt.Rows.Count];

            for (int i = AllItem_dt.Rows.Count - 1; i >= 0; i--)
            {
                xValues_date[AllItem_dt.Rows.Count - (i + 1)] = Convert.ToDateTime(AllItem_dt.Rows[i]["date"]).ToString("MM/dd");//日期
                yValues_AllItem[AllItem_dt.Rows.Count - (i + 1)] = Convert.ToInt16(AllItem_dt.Rows[i]["allitem"].ToString());//總人數
                d.Add("date", Convert.ToDateTime(AllItem_dt.Rows[i]["date"]).ToString("yyyy/MM/dd"));
                OtherItem_dt = du.getDataTableBysp("Ex107_Get_7day_OtherItemTest", d);
                if (OtherItem_dt.Rows.Count > 0)
                {
                    yValues_OtherItem[AllItem_dt.Rows.Count - (i + 1)] = Convert.ToInt16(OtherItem_dt.Rows[0]["otheritem"].ToString());//多元人數
                    yValues_OtherRate[AllItem_dt.Rows.Count - (i + 1)] = Math.Round((Convert.ToDouble(yValues_OtherItem[AllItem_dt.Rows.Count - (i + 1)] / Convert.ToDouble(yValues_AllItem[AllItem_dt.Rows.Count - (i + 1)])))*100,1);
                }
                else
                {
                    yValues_OtherItem[AllItem_dt.Rows.Count - (i + 1)] = 0;
                    yValues_OtherRate[AllItem_dt.Rows.Count - (i + 1)] = 0;
                }
                OtherItem_dt.Clear();
                d.Clear();

            }

            //柱狀圖
            //設定 Legends------------------------------------------------------------------------                
            chart_7DaySeleteOther.Legends["Column"].DockedToChartArea = "ColumnChartArea"; //顯示在圖表內


            //繪製的樣式
            chart_7DaySeleteOther.Series["Column"]["DrawingStyle"] = "Cylinder";
            chart_7DaySeleteOther.Series["Column"].BorderWidth = 0;//設定線粗

            chart_7DaySeleteOther.Series["Column"].Points.DataBindXY(xValues_date, yValues_OtherRate);
            chart_7DaySeleteOther.Series["Column"].Legend = "Column";
            chart_7DaySeleteOther.Series["Column"].LegendText = titleArr[0];

            chart_7DaySeleteOther.ChartAreas["ColumnChartArea"].AxisX.Title = "日期";
            chart_7DaySeleteOther.ChartAreas["ColumnChartArea"].AxisY.Title = "多元比例%";
            chart_7DaySeleteOther.ChartAreas["ColumnChartArea"].AxisY.TextOrientation = TextOrientation.Stacked;//將文字變橫向顯示
            chart_7DaySeleteOther.ChartAreas["ColumnChartArea"].AxisY.Maximum = 100;//設定Y軸最大值
            //打開圖例
            chart_7DaySeleteOther.Series["Column"].IsValueShownAsLabel = true;
            chart_7DaySeleteOther.Series["Column"].IsVisibleInLegend = false;

            //設定圖形X軸的顯示文字
            foreach (DataPoint dp in chart_7DaySeleteOther.Series["Column"].Points)
            {
                dp.Label = dp.YValues[0].ToString() + "%";
            }

            //放入資料表
            if (xValues_date.Length > 0)
            {
                lab_Item_date1.Text = Convert.ToDateTime(AllItem_dt.Rows[6]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All1.Text = yValues_AllItem[0].ToString();//總人數
                lab_Item_Other1.Text = yValues_OtherItem[0].ToString();//多元人數
                lab_Item_Rate1.Text = yValues_OtherRate[0].ToString();//多元比例
            }
            if (xValues_date.Length > 1)
            {
                lab_Item_date2.Text = Convert.ToDateTime(AllItem_dt.Rows[5]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All2.Text = yValues_AllItem[1].ToString();//總人數
                lab_Item_Other2.Text = yValues_OtherItem[1].ToString();//多元人數
                lab_Item_Rate2.Text = yValues_OtherRate[1].ToString();//多元比例
            }
            if (xValues_date.Length > 2)
            {
                lab_Item_date3.Text = Convert.ToDateTime(AllItem_dt.Rows[4]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All3.Text = yValues_AllItem[2].ToString();//總人數
                lab_Item_Other3.Text = yValues_OtherItem[2].ToString();//多元人數
                lab_Item_Rate3.Text = yValues_OtherRate[2].ToString();//多元比例
            }
            if (xValues_date.Length > 3)
            {
                lab_Item_date4.Text = Convert.ToDateTime(AllItem_dt.Rows[3]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All4.Text = yValues_AllItem[3].ToString();//總人數
                lab_Item_Other4.Text = yValues_OtherItem[3].ToString();//多元人數
                lab_Item_Rate4.Text = yValues_OtherRate[3].ToString();//多元比例
            }
            if (xValues_date.Length > 4)
            {
                lab_Item_date5.Text = Convert.ToDateTime(AllItem_dt.Rows[2]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All5.Text = yValues_AllItem[4].ToString();//總人數
                lab_Item_Other5.Text = yValues_OtherItem[4].ToString();//多元人數
                lab_Item_Rate5.Text = yValues_OtherRate[4].ToString();//多元比例
            }
            if (xValues_date.Length > 5)
            {
                lab_Item_date6.Text = Convert.ToDateTime(AllItem_dt.Rows[1]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All6.Text = yValues_AllItem[5].ToString();//總人數
                lab_Item_Other6.Text = yValues_OtherItem[5].ToString();//多元人數
                lab_Item_Rate6.Text = yValues_OtherRate[5].ToString();//多元比例
            }
            if (xValues_date.Length > 6)
            {
                lab_Item_date7.Text = Convert.ToDateTime(AllItem_dt.Rows[0]["date"]).ToString("yyyy/MM/dd");//日期
                lab_Item_All7.Text = yValues_AllItem[6].ToString();//總人數
                lab_Item_Other7.Text = yValues_OtherItem[6].ToString();//多元人數
                lab_Item_Rate7.Text = yValues_OtherRate[6].ToString();//多元比例
            }
        }

        
    }

    //單日多元選項項目使用比例
    private void Get_chart_SeleteOtherItem(string date)
    {
        string Date = date;
        //查詢資料
        DataUtility du = new DataUtility();
        DataTable dt = new DataTable();//查詢當日多元選項使用人次
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add("date", Date);
        dt = du.getDataTableBysp("Ex107_Get_UseOtherItemsCount",d);

        if (dt.Rows.Count > 0)
        {
            int original_count = 0;
            int swin_count = 0;
            int walk_count = 0;
            int jump_count = 0;
            int total_count = 0;
            double[] rate = new double[4];

            original_count = Convert.ToInt16(dt.Rows[0]["original"].ToString());
            swin_count = Convert.ToInt16(dt.Rows[0]["swin"].ToString());
            walk_count = Convert.ToInt16(dt.Rows[0]["walk"].ToString());
            jump_count = Convert.ToInt16(dt.Rows[0]["jump"].ToString());
            total_count = original_count + swin_count + walk_count + jump_count;
            if (total_count == 0)
            {
                //當日無多元選項人次
                chart_SeleteOtherItem.Series["Pie"].Name = "本日無受測人員";
                rate[0] = 0;
                rate[1] = 0;
                rate[2] = 0;
                rate[3] = 0;
            }
            else
            {
                //計算百分比
                rate[0] = Math.Round(Convert.ToDouble(original_count) / Convert.ToDouble(total_count) * 100, 1);
                rate[1] = Math.Round(Convert.ToDouble(swin_count) / Convert.ToDouble(total_count) * 100, 1);
                rate[2] = Math.Round(Convert.ToDouble(walk_count) / Convert.ToDouble(total_count) * 100, 1);
                rate[3] = Math.Round(Convert.ToDouble(jump_count) / Convert.ToDouble(total_count) * 100, 1);

                string[] xValues = { "基本項目(3km跑步)","800公尺游走", "五公里健走", "5分鐘跳繩" };
                string[] titleArr = { "項目", "人數" };
                int[] yValues = {original_count, swin_count, walk_count, jump_count };

                //繪製樣式
                chart_SeleteOtherItem.Series["Pie"]["DrawingStyle"] = "Cylinder";
                //顯示圖形的樣式類別
                chart_SeleteOtherItem.Series["Pie"].ChartType = SeriesChartType.Pie;

                chart_SeleteOtherItem.Series["Pie"].Points.DataBindXY(xValues, rate);
                chart_SeleteOtherItem.Series["Pie"].Legend = "Pie";
                //設定圖形X軸的顯示文字      
                chart_SeleteOtherItem.ChartAreas["PieChartArea"].AxisX.Title = "項目";
                chart_SeleteOtherItem.ChartAreas["PieChartArea"].AxisY.Title = "人數比例\n(%)";

                //塞入表格
                if (total_count > 0)
                {
                    //人數
                    lab_OtherItem_count1.Text = original_count.ToString();
                    lab_OtherItem_count2.Text = swin_count.ToString();
                    lab_OtherItem_count3.Text = walk_count.ToString();
                    lab_OtherItem_count4.Text = jump_count.ToString();
                    
                    //百分比
                    lab_OtherItem_rate1.Text = rate[0].ToString();
                    lab_OtherItem_rate2.Text = rate[1].ToString();
                    lab_OtherItem_rate3.Text = rate[2].ToString();
                    lab_OtherItem_rate4.Text = rate[3].ToString();

                    //設定圖形X軸的顯示文字
                    foreach (DataPoint dp in chart_SeleteOtherItem.Series["Pie"].Points)
                    {
                        dp.Label = dp.YValues[0].ToString() + "%";
                        dp.LegendText = dp.AxisLabel;//圖例名稱
                    }
                }
                
            }
        }

        
    }

    //單日單項合格率
    private void Get_ItemPass(string date)
    {
        string Date = date;
        //查詢資料
        DataUtility du = new DataUtility();
        DataTable dt = new DataTable();//查詢當日多元選項使用人次
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add("date", Date);
        dt = du.getDataTableBysp("Ex107_Get_ItemPass", d);
        if (dt.Rows.Count > 0)
        {
            //基本三項成績
            int[] sit_ups_Values = { Convert.ToInt16(dt.Rows[0]["sit_ups_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["sit_ups_nopass"].ToString()) };
            int[] push_ups_Values = { Convert.ToInt16(dt.Rows[0]["push_ups_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["push_ups_nopass"].ToString()) };
            int[] run_Values = { Convert.ToInt16(dt.Rows[0]["run_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["run_nopass"].ToString()) };
            //替代項目成績
            int[] _800m_swin_Values = { Convert.ToInt16(dt.Rows[0]["800m_swin_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["800m_swin_nopass"].ToString()) };
            int[] _5km_walk_Values = { Convert.ToInt16(dt.Rows[0]["5km_walk_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["5km_walk_nopass"].ToString()) };
            int[] _5min_jump_Values = { Convert.ToInt16(dt.Rows[0]["5min_jump_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["5min_jump_nopass"].ToString()) };
            int[] up_pole_Values = { Convert.ToInt16(dt.Rows[0]["up_pole_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["up_pole_nopass"].ToString()) };
            int[] set_pole_Values = { Convert.ToInt16(dt.Rows[0]["set_pole_pass"].ToString()), Convert.ToInt16(dt.Rows[0]["set_pole_nopass"].ToString()) };
            ////--基本三項--////
            //仰臥起坐
            Set_ItemPass(chart_sit_ups, sit_ups_Values);
            //伏地挺身
            Set_ItemPass(chart_push_ups, push_ups_Values);
            //三千公尺跑步
            Set_ItemPass(chart_run, run_Values);

            ////--替代項目--////
            //800公尺游走
            Set_ItemPass(chart_800m_swin, _800m_swin_Values);
            //5公里健走
            Set_ItemPass(chart_5km_walk, _5km_walk_Values);
            //5分鐘跳繩
            Set_ItemPass(chart_5min_jump, _5min_jump_Values);
            //單槓引體向上
            Set_ItemPass(chart_up_Pole, up_pole_Values);
            //女性屈臂懸垂
            Set_ItemPass(chart_set_Pole, set_pole_Values);
        }
        
    }

    //產生單項合格率函式
    private void Set_ItemPass(Chart _chart, int[] _yValues)
    {
        string[] xValues = { "合格", "不合格" };
        string[] titleArr = { "項目", "人數" };
        int[] yValues = _yValues;
        double totalValues = Convert.ToDouble(yValues[0]) + Convert.ToDouble(yValues[1]);

        //繪製樣式
        _chart.Series["Pie"]["DrawingStyle"] = "Cylinder";
        //顯示圖形的樣式類別
        _chart.Series["Pie"].ChartType = SeriesChartType.Pie;

        _chart.Series["Pie"].Points.DataBindXY(xValues, yValues);
        _chart.Series["Pie"].Legend = "Pie";
        //設定圖形X軸的顯示文字
        foreach (DataPoint dp in _chart.Series["Pie"].Points)
        {
            if(totalValues!=0)
            dp.Label = dp.YValues[0] + "(" + Math.Round((dp.YValues[0] / totalValues) * 100, 1).ToString() + "%)";//數據加百分比
            dp.LegendText = dp.AxisLabel;//右上圖例名稱
        }
        _chart.ChartAreas["PieChartArea"].AxisX.Title = "項目";
        _chart.ChartAreas["PieChartArea"].AxisY.Title = "人數";
    }

}