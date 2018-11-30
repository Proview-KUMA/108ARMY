using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lib;
using System.Threading;

namespace InI
{
    public partial class HistoryChart : Form
    {
        public HistoryChart()
        {
            InitializeComponent();
        }

        private void HistoryChart_Load(object sender, EventArgs e)
        {
            GetYearList();//取得資料年份
        }

        private void btn_InqTab1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbb_YearTab1.Text))
            {
                //清空圖表
                chart_RealTest.ChartAreas.Clear();
                chart_RealTest.Series.Clear();
                chart_RealTest.Legends.Clear();
                Draw_RealTest_Chart(cbb_YearTab1.Text);
            }
        }

        private void btn_InqTab2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbb_YearTab2.Text))
            {
                chart_PassRate.ChartAreas.Clear();
                chart_PassRate.Series.Clear();
                chart_PassRate.Legends.Clear();
                Draw_PassRate_Chart(cbb_YearTab2.Text);
            }
        }

        private void btn_InqTab3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbb_YearTab3.Text))
            {
                chart_StandartReplace.ChartAreas.Clear();
                chart_StandartReplace.Series.Clear();
                chart_StandartReplace.Legends.Clear();
                Draw_StandartReplace_Chart(cbb_YearTab3.Text);
            }
        }

        private void btn_InqTab4_Click(object sender, EventArgs e)
        {
            chart_RankTest.ChartAreas.Clear();
            chart_RankTest.Series.Clear();
            chart_RankTest.Legends.Clear();
            Draw_RankTest_Chart(cbb_YearTab4.Text);
        }

        #region [圖表處理]
        //1、實測人數
        private void Draw_RealTest_Chart(string year)
        {
            //建立XY軸基本資料
            string[] Value_X;
            int[] Value_Y;
            Value_X = new string[12] { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };
            Value_Y = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//先把月份人數全塞0

            string InqYear = year;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("year", InqYear);
            DataTable dt = new DataTable();
            dt = Do_GetTable_Dic("Ex108_Get_RealTest_Chart", dic);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Value_Y[Convert.ToInt32(dt.Rows[i]["month"].ToString()) - 1] = Convert.ToInt32(dt.Rows[i]["count"].ToString());
                }
            }


            //建立物件
            chart_RealTest.ChartAreas.Add("ChartAreas1");//建立圖表區塊
            chart_RealTest.Series.Add("Series1");//建立圖表
            chart_RealTest.Legends.Add("Legends1");//建立圖例
            //設定XY軸名稱顯示
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.Title = "月份";
            chart_RealTest.ChartAreas["ChartAreas1"].AxisY.Title = "人數";
            chart_RealTest.ChartAreas["ChartAreas1"].AxisY.TextOrientation = TextOrientation.Horizontal;//將文字變橫向顯示
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.TitleForeColor = Color.Blue;
            chart_RealTest.ChartAreas["ChartAreas1"].AxisY.TitleForeColor = Color.Red;
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_RealTest.ChartAreas["ChartAreas1"].AxisY.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            chart_RealTest.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            //加入下二行將x軸全部標籤顯示
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.LabelStyle.IsStaggered = true;
            chart_RealTest.ChartAreas["ChartAreas1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //圖例設定
            //chart_RealTest.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
            chart_RealTest.Legends["Legends1"].Docking = Docking.Top;//顯示位置上中下//上
            chart_RealTest.Legends["Legends1"].Alignment = StringAlignment.Near;//顯示位置左中右//左
            chart_RealTest.Legends["Legends1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_RealTest.Legends["Legends1"].BackColor = Color.Transparent;
            //chart_RealTest.Legends["Legends1"].Title = cbb_YearTab1.Text+"年";
            //chart_RealTest.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

            chart_RealTest.Series["Series1"].ChartType = SeriesChartType.Column;
            chart_RealTest.Series["Series1"].LegendText = "人數(" + cbb_YearTab1.Text + "年)";
            chart_RealTest.Series["Series1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_RealTest.Series["Series1"].Color = Color.Aquamarine;
            chart_RealTest.Series["Series1"].LabelForeColor = Color.Red;
            chart_RealTest.Series["Series1"].IsValueShownAsLabel = true;
            chart_RealTest.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
        }

        //2、合格率
        private void Draw_PassRate_Chart(string year)
        {
            string[] Value_X;
            int[] Value_Y;
            int[] Value_Y2;
            Value_X = new string[12] { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };

            Value_Y = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Value_Y2 = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            string InqYear = year;
            Dictionary<string, object> dic_pass = new Dictionary<string, object>();//合格
            Dictionary<string, object> dic_nopass = new Dictionary<string, object>();//不合格
            dic_pass.Add("year", InqYear);
            dic_pass.Add("status", "202");
            dic_nopass.Add("year", InqYear);
            dic_nopass.Add("status", "203");
            DataTable dt_pass = new DataTable();//合格
            DataTable dt_nopass = new DataTable();//不合格
            dt_pass = Do_GetTable_Dic("Ex108_Get_PassRate_Chart", dic_pass);
            //合格
            if (dt_pass.Rows.Count > 0)
            {
                for (int i = 0; i < dt_pass.Rows.Count; i++)
                {
                    Value_Y[Convert.ToInt32(dt_pass.Rows[i]["month"].ToString()) - 1] = Convert.ToInt32(dt_pass.Rows[i]["count"].ToString());
                }
            }
            //不合格
            dt_nopass = Do_GetTable_Dic("Ex108_Get_PassRate_Chart", dic_nopass);
            if (dt_nopass.Rows.Count > 0)
            {
                for (int i = 0; i < dt_nopass.Rows.Count; i++)
                {
                    Value_Y2[Convert.ToInt32(dt_nopass.Rows[i]["month"].ToString()) - 1] = Convert.ToInt32(dt_nopass.Rows[i]["count"].ToString());
                }
            }
            //處理合格率
            for (int i = 0; i < 12; i++)
            {
                if (Value_Y[i] != 0)
                {
                    float total, pass, pass_rate;
                    total = Value_Y[i] + Value_Y2[i];
                    pass = Value_Y[i];
                    pass_rate = (pass / total) * 100;
                    Value_X[i] += Environment.NewLine + "(" + pass_rate.ToString("#0.00") + "%)";
                }
                else
                {
                    Value_X[i] += Environment.NewLine + "(0%)";
                }
            }


            chart_PassRate.ChartAreas.Add("ChartAreas1");//建立圖表區塊
            chart_PassRate.Series.Add("Series1");//建立圖表
            chart_PassRate.Series.Add("Series2");//建立圖表
            chart_PassRate.Legends.Add("Legends1");//建立圖例

            //設定XY軸名稱顯示
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.Title = "月份";
            chart_PassRate.ChartAreas["ChartAreas1"].AxisY.Title = "人數";
            chart_PassRate.ChartAreas["ChartAreas1"].AxisY.TextOrientation = TextOrientation.Horizontal;//將文字變橫向顯示
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.TitleForeColor = Color.Blue;
            chart_PassRate.ChartAreas["ChartAreas1"].AxisY.TitleForeColor = Color.Red;
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_PassRate.ChartAreas["ChartAreas1"].AxisY.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            chart_PassRate.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            //加入下二行將x軸全部標籤顯示
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.LabelStyle.IsStaggered = true;
            chart_PassRate.ChartAreas["ChartAreas1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //chart_StandartReplace.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
            chart_PassRate.Legends["Legends1"].Docking = Docking.Top;//顯示位置上中下//上
            chart_PassRate.Legends["Legends1"].Alignment = StringAlignment.Near;//顯示位置左中右//左
            chart_PassRate.Legends["Legends1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_PassRate.Legends["Legends1"].BackColor = Color.Transparent;
            chart_PassRate.Legends["Legends1"].Title = cbb_YearTab2.Text + "年";
            chart_PassRate.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

            //第一筆數據
            chart_PassRate.Series["Series1"].ChartType = SeriesChartType.Column;
            chart_PassRate.Series["Series1"].LegendText = "合格";
            chart_PassRate.Series["Series1"].Font = new Font("微軟正黑體", 8, FontStyle.Bold);
            chart_PassRate.Series["Series1"].IsValueShownAsLabel = true;
            chart_PassRate.Series["Series1"].Color = Color.LightGreen;
            chart_PassRate.Series["Series1"].LabelForeColor = Color.Green;
            chart_PassRate.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
            //增加第二筆數據
            chart_PassRate.Series["Series2"].ChartType = SeriesChartType.Column;
            chart_PassRate.Series["Series2"].LegendText = "不合格";
            chart_PassRate.Series["Series2"].Font = new Font("微軟正黑體", 8, FontStyle.Bold);
            chart_PassRate.Series["Series2"].IsValueShownAsLabel = true;
            chart_PassRate.Series["Series2"].Color = Color.LightPink;
            chart_PassRate.Series["Series2"].LabelForeColor = Color.Red;
            chart_PassRate.Series["Series2"].Points.DataBindXY(Value_X, Value_Y2);
        }

        //3、基本、替代項目比例
        private void Draw_StandartReplace_Chart(string year)
        {
            string[] Value_X;
            int[] Value_Y;
            int[] Value_Y2;
            Value_X = new string[12] { "1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月" };

            Value_Y = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Value_Y2 = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            string InqYear = year;
            Dictionary<string, object> dic_std = new Dictionary<string, object>();//標準
            Dictionary<string, object> dic_rep = new Dictionary<string, object>();//替代/多元
            dic_std.Add("year", InqYear);
            dic_std.Add("item", "0");
            dic_rep.Add("year", InqYear);
            dic_rep.Add("item", "1");
            DataTable dt_std = new DataTable();//標準
            DataTable dt_rep = new DataTable();//替代/多元
            dt_std = Do_GetTable_Dic("Ex108_Get_StandartReplace_Chart", dic_std);
            //標準
            if (dt_std.Rows.Count > 0)
            {
                for (int i = 0; i < dt_std.Rows.Count; i++)
                {
                    Value_Y[Convert.ToInt32(dt_std.Rows[i]["month"].ToString()) - 1] = Convert.ToInt32(dt_std.Rows[i]["count"].ToString());
                }
            }
            //替代
            dt_rep = Do_GetTable_Dic("Ex108_Get_StandartReplace_Chart", dic_rep);
            if (dt_rep.Rows.Count > 0)
            {
                for (int i = 0; i < dt_rep.Rows.Count; i++)
                {
                    Value_Y2[Convert.ToInt32(dt_rep.Rows[i]["month"].ToString()) - 1] = Convert.ToInt32(dt_rep.Rows[i]["count"].ToString());
                }
            }
            //處理傳用標準三項率
            for (int i = 0; i < 12; i++)
            {
                if (Value_Y[i] != 0)
                {
                    float total, standart, standart_rate;
                    total = Value_Y[i] + Value_Y2[i];
                    standart = Value_Y[i];
                    standart_rate = (standart / total) * 100;
                    Value_X[i] += Environment.NewLine + "(" + standart_rate.ToString("#0.00") + "%)";
                }
                else
                {
                    Value_X[i] += Environment.NewLine + "(0%)";
                }
            }

            chart_StandartReplace.ChartAreas.Add("ChartAreas1");//建立圖表區塊
            chart_StandartReplace.Series.Add("Series1");//建立圖表
            chart_StandartReplace.Series.Add("Series2");//建立圖表
            chart_StandartReplace.Legends.Add("Legends1");//建立圖例

            //設定XY軸名稱顯示
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.Title = "月份";
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisY.Title = "人數";
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisY.TextOrientation = TextOrientation.Horizontal;//將文字變橫向顯示
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.TitleForeColor = Color.Blue;
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisY.TitleForeColor = Color.Red;
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisY.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            //加入下二行將x軸全部標籤顯示
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.LabelStyle.IsStaggered = true;
            chart_StandartReplace.ChartAreas["ChartAreas1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //chart_StandartReplace.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
            chart_StandartReplace.Legends["Legends1"].Docking = Docking.Top;//顯示位置上中下//上
            chart_StandartReplace.Legends["Legends1"].Alignment = StringAlignment.Near;//顯示位置左中右//左
            chart_StandartReplace.Legends["Legends1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_StandartReplace.Legends["Legends1"].BackColor = Color.Transparent;
            chart_StandartReplace.Legends["Legends1"].Title = cbb_YearTab3.Text + "年";
            chart_StandartReplace.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

            //第一筆數據
            chart_StandartReplace.Series["Series1"].ChartType = SeriesChartType.Column;
            chart_StandartReplace.Series["Series1"].LegendText = "標準三項";
            chart_StandartReplace.Series["Series1"].Font = new Font("微軟正黑體", 8, FontStyle.Bold);
            chart_StandartReplace.Series["Series1"].IsValueShownAsLabel = true;
            chart_StandartReplace.Series["Series1"].Color = Color.LightBlue;
            chart_StandartReplace.Series["Series1"].LabelForeColor = Color.Blue;
            chart_StandartReplace.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
            //增加第二筆數據
            chart_StandartReplace.Series["Series2"].ChartType = SeriesChartType.Column;
            chart_StandartReplace.Series["Series2"].LegendText = "替代/多元選項";
            chart_StandartReplace.Series["Series2"].Font = new Font("微軟正黑體", 8, FontStyle.Bold);
            chart_StandartReplace.Series["Series2"].IsValueShownAsLabel = true;
            chart_StandartReplace.Series["Series2"].Color = Color.LightPink;
            chart_StandartReplace.Series["Series2"].LabelForeColor = Color.Red;
            chart_StandartReplace.Series["Series2"].Points.DataBindXY(Value_X, Value_Y2);
        }

        //4、每年官、士、兵到測人數(含人工)
        private void Draw_RankTest_Chart(string year)
        {
            string[] Value_X;
            int[] Value_Y;
            Value_X = new string[9] { "官(應到)", "士(應到)", "兵(應到)", "官(實到)", "士(實到)", "兵(實到)" , "官(人工)", "士(人工)", "兵(人工)" };
            Value_Y = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0};//先把人數全塞0

            string InqYear = year;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("year", InqYear);
            DataTable dt = new DataTable();
            dt = Do_GetTable_Dic("Ex108_Get_RankTest_Chart", dic);
            if (dt.Rows.Count > 0)
            {
                Value_Y[0] = Convert.ToInt32(dt.Rows[0]["rank1_t"].ToString());
                Value_Y[1] = Convert.ToInt32(dt.Rows[0]["rank2_t"].ToString());
                Value_Y[2] = Convert.ToInt32(dt.Rows[0]["rank3_t"].ToString());
                Value_Y[3] = Convert.ToInt32(dt.Rows[0]["rank1_r"].ToString());
                Value_Y[4] = Convert.ToInt32(dt.Rows[0]["rank2_r"].ToString());
                Value_Y[5] = Convert.ToInt32(dt.Rows[0]["rank3_r"].ToString());
                Value_Y[6] = Convert.ToInt32(dt.Rows[0]["rank1_k"].ToString());
                Value_Y[7] = Convert.ToInt32(dt.Rows[0]["rank2_k"].ToString());
                Value_Y[8] = Convert.ToInt32(dt.Rows[0]["rank3_k"].ToString());
            }


            //建立物件
            chart_RankTest.ChartAreas.Add("ChartAreas1");//建立圖表區塊
            chart_RankTest.Series.Add("Series1");//建立圖表
            chart_RankTest.Legends.Add("Legends1");//建立圖例
            //設定XY軸名稱顯示
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.Title = "階級";
            chart_RankTest.ChartAreas["ChartAreas1"].AxisY.Title = "人數";
            chart_RankTest.ChartAreas["ChartAreas1"].AxisY.TextOrientation = TextOrientation.Horizontal;//將文字變橫向顯示
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.TitleForeColor = Color.Blue;
            chart_RankTest.ChartAreas["ChartAreas1"].AxisY.TitleForeColor = Color.Red;
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_RankTest.ChartAreas["ChartAreas1"].AxisY.TitleFont = new Font("微軟正黑體", 16, FontStyle.Bold);
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            chart_RankTest.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            //加入下二行將x軸全部標籤顯示
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.LabelStyle.IsStaggered = true;
            chart_RankTest.ChartAreas["ChartAreas1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //圖例設定
            //chart_RankTest.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
            chart_RankTest.Legends["Legends1"].Docking = Docking.Top;//顯示位置上中下//上
            chart_RankTest.Legends["Legends1"].Alignment = StringAlignment.Near;//顯示位置左中右//左
            chart_RankTest.Legends["Legends1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_RankTest.Legends["Legends1"].BackColor = Color.Transparent;
            //chart_RankTest.Legends["Legends1"].Title = cbb_YearTab1.Text+"年";
            //chart_RankTest.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

            chart_RankTest.Series["Series1"].ChartType = SeriesChartType.Column;
            chart_RankTest.Series["Series1"].LegendText = "人數(" + cbb_YearTab1.Text + "年)";
            chart_RankTest.Series["Series1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            chart_RankTest.Series["Series1"].Color = Color.MediumPurple;
            chart_RankTest.Series["Series1"].LabelForeColor = Color.Red;
            chart_RankTest.Series["Series1"].IsValueShownAsLabel = true;
            chart_RankTest.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
        }

        #endregion

        #region [自定處理方法]
        //取得年份下拉式清單
        private void GetYearList()
        {
            DataTable dt = Do_GetTable("Ex108_GetResultYear");
            if (dt.Rows.Count > 0)
            {
                cbb_YearTab1.Items.Clear();
                string[] years = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    years[i] = dt.Rows[i]["year"].ToString();
                    cbb_YearTab1.Items.Add(years[i]);
                    cbb_YearTab2.Items.Add(years[i]);
                    cbb_YearTab3.Items.Add(years[i]);
                    cbb_YearTab4.Items.Add(years[i]);
                }
                cbb_YearTab1.SelectedIndex = years.Length - 1;
                cbb_YearTab2.SelectedIndex = years.Length - 1;
                cbb_YearTab3.SelectedIndex = years.Length - 1;
                cbb_YearTab4.SelectedIndex = years.Length - 1;
            }       
        }
        #endregion

        #region [委派處理]
        //1、執行SP查詢資料表
        delegate DataTable GetTable_delgate(string SpName);
        private DataTable Do_GetTable(string SpName)
        {
            DataTable dt = new DataTable();
            IAsyncResult result;
            GetTable_delgate gettb;
            gettb = new GetTable_delgate(GetTable);
            result = gettb.BeginInvoke(SpName, null, null);
            dt = gettb.EndInvoke(result);
            return dt;
        }
        private DataTable GetTable(string SpName)
        {
            Lib.DataUtility du = new DataUtility();
            DataTable dt = new DataTable();
            try
            {
                dt = du.getDataTableBysp(SpName);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        //2、執行SP且帶有參數，查詢資料表
        delegate DataTable GetTable_Dic_delgate(string SpName, Dictionary<string, object> dic);
        private DataTable Do_GetTable_Dic(string SpName, Dictionary<string, object> dic)
        {
            DataTable dt = new DataTable();
            IAsyncResult result;
            GetTable_Dic_delgate gettb;
            gettb = new GetTable_Dic_delgate(GetTable_Dic);
            result = gettb.BeginInvoke(SpName, dic, null, null);
            dt = gettb.EndInvoke(result);
            return dt;
        }
        private DataTable GetTable_Dic(string SpName, Dictionary<string, object> dic)
        {
            Lib.DataUtility du = new DataUtility();
            DataTable dt = new DataTable();
            try
            {
                dt = du.getDataTableBysp(SpName, dic);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        #endregion

        
    }
    
}
