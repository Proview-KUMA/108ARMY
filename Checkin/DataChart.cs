using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lib;
using System.Threading;

namespace InI
{
    public partial class DataChart : Form
    {
        public AWS_WS.WebService2SoapClient AWS = null;//連接總部
        private bool IpSetOn;
        private string[] ip_List;
        private bool Auto_Updt_CheckReta;//自動更新檢錄完成率
        private bool Auto_Updt_CompleteReta;//自動更新結算完成率

        public DataChart()
        {
            InitializeComponent();
        }
        private void DataChart_Load(object sender, EventArgs e)
        {
            //IP設定關閉
            IpSetOn = false;
            //自動更新關閉
            Auto_Updt_CheckReta = false;
            Auto_Updt_CompleteReta = false;


            Get_CheckReta();
            Get_ItemTestedReta();
            Get_ItemPlayerCount();
            Get_Result_Complete();
            //讀取設定IP
            Load_IP();
        }
        //關閉視窗後釋放資源
        private void DataChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.dtChart_Fm.Dispose();
            Form1.dtChart_Fm = null;
            this.Close();
        }

        #region [限制IP輸入格式及範圍]

        //設定限制輸入數字
        private void txb_sit_ip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void txb_sit_ip2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_sit_ip10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txb_push_ip10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        //設定限制輸入範圍0-255
        private void txb_sit_ip1_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit1.Text = "未測試";
            btn_Status_sit1.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip2_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit2.Text = "未測試";
            btn_Status_sit2.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip3_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit3.Text = "未測試";
            btn_Status_sit3.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip4_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit4.Text = "未測試";
            btn_Status_sit4.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip5_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit5.Text = "未測試";
            btn_Status_sit5.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip6_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit6.Text = "未測試";
            btn_Status_sit6.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip7_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit7.Text = "未測試";
            btn_Status_sit7.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip8_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit8.Text = "未測試";
            btn_Status_sit8.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip9_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit9.Text = "未測試";
            btn_Status_sit9.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_sit_ip10_TextChanged(object sender, EventArgs e)
        {
            btn_Status_sit10.Text = "未測試";
            btn_Status_sit10.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip1_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push1.Text = "未測試";
            btn_Status_push1.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip2_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push2.Text = "未測試";
            btn_Status_push2.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip3_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push3.Text = "未測試";
            btn_Status_push3.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip4_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push4.Text = "未測試";
            btn_Status_push4.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip5_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push5.Text = "未測試";
            btn_Status_push5.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip6_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push6.Text = "未測試";
            btn_Status_push6.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip7_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push7.Text = "未測試";
            btn_Status_push7.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip8_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push8.Text = "未測試";
            btn_Status_push8.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip9_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push9.Text = "未測試";
            btn_Status_push9.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }

        private void txb_push_ip10_TextChanged(object sender, EventArgs e)
        {
            btn_Status_push10.Text = "未測試";
            btn_Status_push10.BackColor = Color.DarkGray;
            TextBox tb = (TextBox)sender;
            if (int.Parse(tb.Text) <= 0 || int.Parse(tb.Text) > 255)
            {
                tb.Text = null;
            }
        }
        #endregion

        #region [委派處理]
        //查詢資料表委派
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

        //顯示狀態委派
        delegate void ChengText_delgate(Control txb, string text);
        private void Do_Ch_txbText(TextBox txb, string text)
        {
            this.BeginInvoke(new ChengText_delgate(Ch_txbText), txb, text);
        }
        private void Ch_txbText(Control txb, string text)
        {
            txb.Text = text;
        }

        //傳送狀態委派
        delegate void SendStatus_Delegate(Control status_btn, string text, Color color, bool enabled);
        private void SendStatus(Control status_btn, string text, Color color, bool enabled)
        {
            status_btn.Enabled = true;
            status_btn.BackColor = Color.DarkGray;
            status_btn.Text = text;
            status_btn.ForeColor = color;
            status_btn.Enabled = enabled;
        }
        private void Do_SendStatus(Control status_btn, string text, Color color, bool enabled)
        {
            this.BeginInvoke(new SendStatus_Delegate(SendStatus), status_btn, text, color, enabled);
        }

        //建立檢查ip委派
        //1、宣告委派
        delegate void CheckIP_Delegate(Control btn, string ip, Control Enter_btn);
        //2、執行委派
        private void Do_CheckIP(Control btn, string ip, Control Enter_btn)
        {
            try
            {
                this.BeginInvoke(new CheckIP_Delegate(Check_IP), btn, ip, Enter_btn);
            }
            catch (Exception ex)
            {

            }
        }

        //3、委派使用的方法
        private void Check_IP(Control _btn, string _ip, Control Enter_btn)
        {
            try
            {
                Enter_btn.Text = "連線中…";
                Enter_btn.ForeColor = Color.Black;
                Enter_btn.Enabled = false;
                //Do_SendStatus(Enter_btn, "連線中", Color.Black, false);
                Ping p = new Ping();
                PingReply RePing;
                RePing = p.Send(_ip,1000);
                if (RePing.Status == IPStatus.Success)
                {
                    _btn.Enabled = true;
                    _btn.Text = "ON";
                    _btn.BackColor = Color.Lime;
                    _btn.Enabled = false;
                }
                else
                {
                    _btn.Enabled = true;
                    _btn.Text = "OFF";
                    _btn.BackColor = Color.HotPink;
                    _btn.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _btn.Enabled = true;
                _btn.Text = "NG";
                _btn.BackColor = Color.Gold;
                _btn.Enabled = false;
            }
            finally
            {
                Enter_btn.Text = "測試";
                Enter_btn.ForeColor = Color.Blue;
                Enter_btn.Enabled = true;
            }
        }

        #endregion

        #region [數據圖形處理]
        //1、檢錄完成率
        public void Get_CheckReta()
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Check_Reta");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已檢錄", "未檢錄" };
                //測試資料
                //Value_Y = new int[2] { 39, 55 };
                //正式資料
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isCheck"].ToString()), Convert.ToInt16(dt.Rows[0]["noCheck"].ToString()) };

                chart_CheckReta.ChartAreas.Add("ChartAreas1");//建立圖表區塊
                chart_CheckReta.Series.Add("Series1");//建立圖表
                chart_CheckReta.Legends.Add("Legends1");//建立圖例
                chart_CheckReta.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
                chart_CheckReta.Legends["Legends1"].Font = new Font("微軟正黑體", 16, FontStyle.Bold);
                chart_CheckReta.Legends["Legends1"].BackColor = Color.Transparent;
                chart_CheckReta.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_CheckReta.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 14, FontStyle.Bold);

                chart_CheckReta.Series["Series1"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_CheckReta.Series["Series1"].Font = new Font("微軟正黑體", 20, FontStyle.Bold);

                chart_CheckReta.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_CheckReta.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                    
                }

                
            }
            
            
        }
        //中心位置顯示檢錄完成率
        private void chart_CheckReta_PrePaint(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea)
            {
                var ta = new TextAnnotation();
                Chart cht = (Chart)sender;
                double isCheck = cht.Series[0].Points[0].YValues[0];
                double noCheck = cht.Series[0].Points[1].YValues[0];
                double Check_Rate;
                if ((isCheck + noCheck) == 0)
                    Check_Rate = 0;
                else
                    Check_Rate = Math.Round((isCheck / (isCheck + noCheck)) * 100, 1);

                ta.Text = "完成率\n" + Check_Rate.ToString() + "%";
                ta.Width = e.Position.Width;
                ta.Height = e.Position.Height;
                ta.X = e.Position.X - 1;
                ta.Y = e.Position.Y + 1;
                ta.Font = new Font("微軟正黑體", 20, FontStyle.Bold);

                chart_CheckReta.Annotations.Clear();//先清空
                chart_CheckReta.Annotations.Add(ta);//再加入
            }
        }
        //2、單項完成率
        private void Get_ItemTestedReta()
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_ItemTested");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y_sit;
                int[] Value_Y_push;
                int[] Value_Y_run;
                int[] Value_Y_swin;
                int[] Value_Y_walk;
                int[] Value_Y_jump;
                int[] Value_Y_uppole;
                int[] Value_Y_setpole;

                Value_X = new string[2] { "已測", "未測" };

                //測試資料
                //Value_Y_sit = new int[2] { 55,66 };
                //Value_Y_push = new int[2] { 65,35 };
                //Value_Y_run = new int[2] { 23,78 };
                //Value_Y_swin = new int[2] { 8,12 };
                //Value_Y_walk = new int[2] { 22,42 };
                //Value_Y_jump = new int[2] {3,5 };
                //Value_Y_uppole = new int[2] { 2,4 };
                //Value_Y_setpole = new int[2] { 0,3 };

                //正式資料
                Value_Y_sit = new int[2] { Convert.ToInt16(dt.Rows[0]["sit_ups_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["sit_ups_notest"].ToString()) };
                Value_Y_push = new int[2] { Convert.ToInt16(dt.Rows[0]["push_ups_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["push_ups_notest"].ToString()) };
                Value_Y_run = new int[2] { Convert.ToInt16(dt.Rows[0]["run_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["run_notest"].ToString()) };
                Value_Y_swin = new int[2] { Convert.ToInt16(dt.Rows[0]["800m_swin_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["800m_swin_notest"].ToString()) };
                Value_Y_walk = new int[2] { Convert.ToInt16(dt.Rows[0]["5km_walk_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["5km_walk_notest"].ToString()) };
                Value_Y_jump = new int[2] { Convert.ToInt16(dt.Rows[0]["5min_jump_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["5min_jump_notest"].ToString()) };
                Value_Y_uppole = new int[2] { Convert.ToInt16(dt.Rows[0]["up_pole_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["up_pole_notest"].ToString()) };
                Value_Y_setpole = new int[2] { Convert.ToInt16(dt.Rows[0]["set_pole_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["set_pole_notest"].ToString()) };



                chart_ItemTestedReta.ChartAreas.Add("ChartAreas1");//建立圖表區塊1
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas2");//建立圖表區塊2
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas3");//建立圖表區塊3
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas4");//建立圖表區塊4
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas5");//建立圖表區塊5
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas6");//建立圖表區塊6
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas7");//建立圖表區塊7
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas8");//建立圖表區塊8
                chart_ItemTestedReta.ChartAreas.Add("ChartAreas9");//建立圖表區塊9

                chart_ItemTestedReta.Series.Add("Series1");//建立圖表
                chart_ItemTestedReta.Series.Add("Series2");//建立圖表
                chart_ItemTestedReta.Series.Add("Series3");//建立圖表
                chart_ItemTestedReta.Series.Add("Series4");//建立圖表
                chart_ItemTestedReta.Series.Add("Series5");//建立圖表
                chart_ItemTestedReta.Series.Add("Series6");//建立圖表
                chart_ItemTestedReta.Series.Add("Series7");//建立圖表
                chart_ItemTestedReta.Series.Add("Series8");//建立圖表
                chart_ItemTestedReta.Legends.Add("Legends1");//建立圖例

                //chart_ItemTestedReta.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
                chart_ItemTestedReta.Legends["Legends1"].Font = new Font("微軟正黑體", 14, FontStyle.Bold);
                chart_ItemTestedReta.Legends["Legends1"].BackColor = Color.Transparent;
                chart_ItemTestedReta.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_ItemTestedReta.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

                //仰臥起坐圖表
                chart_ItemTestedReta.Series["Series1"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series1"].ChartArea = "ChartAreas1";
                chart_ItemTestedReta.Series["Series1"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series1"].Points.DataBindXY(Value_X, Value_Y_sit);
                chart_ItemTestedReta.Series["Series1"].Legend = "Legends1";
                //俯地挺身圖表
                chart_ItemTestedReta.Series["Series2"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series2"].ChartArea = "ChartAreas2";
                chart_ItemTestedReta.Series["Series2"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series2"].Points.DataBindXY(Value_X, Value_Y_push);
                chart_ItemTestedReta.Series["Series2"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //三千公尺圖表
                chart_ItemTestedReta.Series["Series3"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series3"].ChartArea = "ChartAreas3";
                chart_ItemTestedReta.Series["Series3"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series3"].Points.DataBindXY(Value_X, Value_Y_run);
                chart_ItemTestedReta.Series["Series3"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //八百游走圖表
                chart_ItemTestedReta.Series["Series4"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series4"].ChartArea = "ChartAreas4";
                chart_ItemTestedReta.Series["Series4"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series4"].Points.DataBindXY(Value_X, Value_Y_swin);
                chart_ItemTestedReta.Series["Series4"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //五公里健走圖表
                chart_ItemTestedReta.Series["Series5"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series5"].ChartArea = "ChartAreas5";
                chart_ItemTestedReta.Series["Series5"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series5"].Points.DataBindXY(Value_X, Value_Y_walk);
                chart_ItemTestedReta.Series["Series5"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //五分鐘跳繩圖表
                chart_ItemTestedReta.Series["Series6"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series6"].ChartArea = "ChartAreas6";
                chart_ItemTestedReta.Series["Series6"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series6"].Points.DataBindXY(Value_X, Value_Y_jump);
                chart_ItemTestedReta.Series["Series6"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //引體向上(男)圖表
                chart_ItemTestedReta.Series["Series7"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series7"].ChartArea = "ChartAreas7";
                chart_ItemTestedReta.Series["Series7"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series7"].Points.DataBindXY(Value_X, Value_Y_uppole);
                chart_ItemTestedReta.Series["Series7"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示
                //屈臂懸垂(女)圖表
                chart_ItemTestedReta.Series["Series8"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_ItemTestedReta.Series["Series8"].ChartArea = "ChartAreas8";
                chart_ItemTestedReta.Series["Series8"].Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                chart_ItemTestedReta.Series["Series8"].Points.DataBindXY(Value_X, Value_Y_setpole);
                chart_ItemTestedReta.Series["Series8"].IsVisibleInLegend = false;//不顯示在圖例裡面，會重復顯示

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series2"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series3"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series4"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series5"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series6"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series7"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series8"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                chart_ItemTestedReta.Annotations.Clear();//清空圓形中間文字
            }

        }
        //中心位置顯示單項完成率
        private void chart_ItemTestedReta_PrePaint(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea)
            {
                var ta = new TextAnnotation();
                //ta.Text = "完成率\n99.9%";
                ta.Width = e.Position.Width;
                ta.Height = e.Position.Height;
                ta.X = e.Position.X - 1;
                ta.Y = e.Position.Y + 1;
                ta.Font = new Font("微軟正黑體", 10, FontStyle.Bold);

                Chart cht = (Chart)sender;

                double isTest = 0;
                double noTest = 0;
                double AllTest;

                string Reat = e.ChartElement.ToString();
                switch (Reat)
                {
                    case "ChartArea-ChartAreas1":
                        isTest = cht.Series[0].Points[0].YValues[0];
                        noTest = cht.Series[0].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "仰臥起坐\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas2":
                        isTest = cht.Series[1].Points[0].YValues[0];
                        noTest = cht.Series[1].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "俯地挺身\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas3":
                        isTest = cht.Series[2].Points[0].YValues[0];
                        noTest = cht.Series[2].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "三千公尺\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas4":
                        isTest = cht.Series[3].Points[0].YValues[0];
                        noTest = cht.Series[3].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "八百游走\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas5":
                        isTest = cht.Series[4].Points[0].YValues[0];
                        noTest = cht.Series[4].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "健走\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas6":
                        isTest = cht.Series[5].Points[0].YValues[0];
                        noTest = cht.Series[5].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "跳繩\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas7":
                        isTest = cht.Series[6].Points[0].YValues[0];
                        noTest = cht.Series[6].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "引體向上\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    case "ChartArea-ChartAreas8":
                        isTest = cht.Series[7].Points[0].YValues[0];
                        noTest = cht.Series[7].Points[1].YValues[0];
                        if ((isTest + noTest) == 0)
                            AllTest = 0;
                        else
                            AllTest = Math.Round((isTest / (isTest + noTest)) * 100, 1);
                        ta.Text = "屈臂懸垂\n" + AllTest.ToString() + "%";
                        ta.X = e.Position.X - 1;
                        ta.Y = e.Position.Y + 1;
                        break;
                    default:
                        ta.Text = "";
                        break;
                }

                //chart_ItemTestedReta.Annotations.Clear();
                chart_ItemTestedReta.Annotations.Add(ta);
            }
        }
        //3、取得單項測考人數比例
        private void Get_ItemPlayerCount()
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_ItemTestCount");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[8] { "仰臥起坐", "俯地挺身", "三千公尺", "八百游走", "5公里健走", "5分鐘跳繩", "引體向上", "屈臂懸垂" };
                //測試資料
                //Value_Y = new int[8] { 120, 125, 55, 22, 45, 12, 5, 0 };
                //正試資料
                Value_Y = new int[8] { Convert.ToInt16(dt.Rows[0]["sit_ups_testcount"]), Convert.ToInt16(dt.Rows[0]["push_ups_testcount"]), Convert.ToInt16(dt.Rows[0]["run_testcount"]), Convert.ToInt16(dt.Rows[0]["800m_swin_testcount"]), Convert.ToInt16(dt.Rows[0]["5km_walk_testcount"]), Convert.ToInt16(dt.Rows[0]["5min_jump_testcount"]), Convert.ToInt16(dt.Rows[0]["up_pole_testcount"]), Convert.ToInt16(dt.Rows[0]["set_pole_testcount"]) };

                chart_ItemPlayerCount.ChartAreas.Add("ChartAreas1");//建立圖表區塊
                chart_ItemPlayerCount.Series.Add("Series1");//建立圖表
                chart_ItemPlayerCount.Legends.Add("Legends1");//建立圖例

                //設定XY軸名稱顯示
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.Title = "項目";
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisY.Title = "人數";
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisY.TextOrientation = TextOrientation.Horizontal;//將文字變橫向顯示
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.TitleForeColor = Color.Blue;
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisY.TitleForeColor = Color.Red;
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisY.TitleFont = new Font("微軟正黑體", 20, FontStyle.Bold);
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new Font("微軟正黑體", 14, FontStyle.Bold);
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new Font("微軟正黑體", 20, FontStyle.Bold);
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.IsLabelAutoFit = true;//X軸名稱顯示可自動調整
                chart_ItemPlayerCount.ChartAreas["ChartAreas1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.StaggeredLabels;//調整方式為上下排列

                chart_ItemPlayerCount.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
                chart_ItemPlayerCount.Legends["Legends1"].Font = new Font("微軟正黑體", 18, FontStyle.Bold);
                chart_ItemPlayerCount.Legends["Legends1"].BackColor = Color.Transparent;
                chart_ItemPlayerCount.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_ItemPlayerCount.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 12, FontStyle.Bold);

                chart_ItemPlayerCount.Series["Series1"].ChartType = SeriesChartType.Column;
                chart_ItemPlayerCount.Series["Series1"].LegendText = "人數";
                chart_ItemPlayerCount.Series["Series1"].Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                chart_ItemPlayerCount.Series["Series1"].IsValueShownAsLabel = true;

                chart_ItemPlayerCount.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);

                //重設右上及圖形顯示內容
                //foreach (DataPoint dp in chart_ItemPlayerCount.Series["Series1"].Points)
                //{
                //    dp.Label = dp.YValues[0].ToString();//圖形內文字
                //    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                //}
            }

        }
        //4、成績結算比例(領成績單)
        private void Get_Result_Complete()
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Result_Complete");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已結算", "未結算" };
                //測試資料
                //Value_Y = new int[2] { 45, 80 };
                //正式資料
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isComplete"]), Convert.ToInt16(dt.Rows[0]["noComplete"]) };
                chart_Result_Complete.ChartAreas.Add("ChartAreas1");//建立圖表區塊
                chart_Result_Complete.Series.Add("Series1");//建立圖表
                chart_Result_Complete.Legends.Add("Legends1");//建立圖例
                chart_Result_Complete.Legends["Legends1"].DockedToChartArea = "ChartAreas1";//顯示在圖表內
                chart_Result_Complete.Legends["Legends1"].Font = new Font("微軟正黑體", 16, FontStyle.Bold);
                chart_Result_Complete.Legends["Legends1"].BackColor = Color.Transparent;
                chart_Result_Complete.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_Result_Complete.Legends["Legends1"].TitleFont = new Font("微軟正黑體", 14, FontStyle.Bold);

                chart_Result_Complete.Series["Series1"].ChartType = SeriesChartType.Doughnut;//環狀圖
                chart_Result_Complete.Series["Series1"].Font = new Font("微軟正黑體", 20, FontStyle.Bold);

                chart_Result_Complete.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_Result_Complete.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
            }
        }
        //中心位置顯示結算完成率
        private void chart_Result_Complete_PrePaint(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea)
            {
                var ta = new TextAnnotation();

                Chart cht = (Chart)sender;
                double isComplete = cht.Series[0].Points[0].YValues[0];
                double noComplete = cht.Series[0].Points[1].YValues[0];
                double Complete_Rate;
                if ((isComplete + noComplete) == 0)
                    Complete_Rate = 0;
                else
                    Complete_Rate = Math.Round((isComplete / (isComplete + noComplete)) * 100, 1);

                ta.Text = "完成率\n" + Complete_Rate.ToString() + "%";

                ta.Width = e.Position.Width;
                ta.Height = e.Position.Height;
                ta.X = e.Position.X - 1;
                ta.Y = e.Position.Y + 1;
                ta.Font = new Font("微軟正黑體", 20, FontStyle.Bold);

                chart_Result_Complete.Annotations.Clear();
                chart_Result_Complete.Annotations.Add(ta);
            }
        }
        #endregion


        #region 測試IP連線

        //IP設定開或關
        private void Set_IP_OnOff(bool status)
        {
            bool btn_status;
            if (status == true)
                btn_status = false;
            else
                btn_status = true;
            txb_sit_ip1.Enabled = status;
            txb_sit_ip2.Enabled = status;
            txb_sit_ip3.Enabled = status;
            txb_sit_ip4.Enabled = status;
            txb_sit_ip5.Enabled = status;
            txb_sit_ip6.Enabled = status;
            txb_sit_ip7.Enabled = status;
            txb_sit_ip8.Enabled = status;
            txb_sit_ip9.Enabled = status;
            txb_sit_ip10.Enabled = status;
            txb_push_ip1.Enabled = status;
            txb_push_ip2.Enabled = status;
            txb_push_ip3.Enabled = status;
            txb_push_ip4.Enabled = status;
            txb_push_ip5.Enabled = status;
            txb_push_ip6.Enabled = status;
            txb_push_ip7.Enabled = status;
            txb_push_ip8.Enabled = status;
            txb_push_ip9.Enabled = status;
            txb_push_ip10.Enabled = status;
            //設定按鍵
            btn_Ping_All.Enabled = btn_status;
            btn_Ping_sit1.Enabled = btn_status;
            btn_Ping_sit2.Enabled = btn_status;
            btn_Ping_sit3.Enabled = btn_status;
            btn_Ping_sit4.Enabled = btn_status;
            btn_Ping_sit5.Enabled = btn_status;
            btn_Ping_sit6.Enabled = btn_status;
            btn_Ping_sit7.Enabled = btn_status;
            btn_Ping_sit8.Enabled = btn_status;
            btn_Ping_sit9.Enabled = btn_status;
            btn_Ping_sit10.Enabled = btn_status;
            btn_Ping_push1.Enabled = btn_status;
            btn_Ping_push2.Enabled = btn_status;
            btn_Ping_push3.Enabled = btn_status;
            btn_Ping_push4.Enabled = btn_status;
            btn_Ping_push5.Enabled = btn_status;
            btn_Ping_push6.Enabled = btn_status;
            btn_Ping_push7.Enabled = btn_status;
            btn_Ping_push8.Enabled = btn_status;
            btn_Ping_push9.Enabled = btn_status;
            btn_Ping_push10.Enabled = btn_status;
        }
        //儲存IP
        private void Save_IP()
        {
            ip_List = new string[20];
            ip_List[0] = txb_sit_ip1.Text;
            ip_List[1] = txb_sit_ip2.Text;
            ip_List[2] = txb_sit_ip3.Text;
            ip_List[3] = txb_sit_ip4.Text;
            ip_List[4] = txb_sit_ip5.Text;
            ip_List[5] = txb_sit_ip6.Text;
            ip_List[6] = txb_sit_ip7.Text;
            ip_List[7] = txb_sit_ip8.Text;
            ip_List[8] = txb_sit_ip9.Text;
            ip_List[9] = txb_sit_ip10.Text;
            ip_List[10] = txb_push_ip1.Text;
            ip_List[11] = txb_push_ip2.Text;
            ip_List[12] = txb_push_ip3.Text;
            ip_List[13] = txb_push_ip4.Text;
            ip_List[14] = txb_push_ip5.Text;
            ip_List[15] = txb_push_ip6.Text;
            ip_List[16] = txb_push_ip7.Text;
            ip_List[17] = txb_push_ip8.Text;
            ip_List[18] = txb_push_ip9.Text;
            ip_List[19] = txb_push_ip10.Text;
            Properties.Settings.Default.IParray = ip_List;
            Properties.Settings.Default.Save();
        }
        private void Load_IP()
        {
            if (Properties.Settings.Default.IParray != null)
            {
                ip_List = Properties.Settings.Default.IParray;
                Do_Ch_txbText(txb_sit_ip1, ip_List[0]);
                Do_Ch_txbText(txb_sit_ip2, ip_List[1]);
                Do_Ch_txbText(txb_sit_ip3, ip_List[2]);
                Do_Ch_txbText(txb_sit_ip4, ip_List[3]);
                Do_Ch_txbText(txb_sit_ip5, ip_List[4]);
                Do_Ch_txbText(txb_sit_ip6, ip_List[5]);
                Do_Ch_txbText(txb_sit_ip7, ip_List[6]);
                Do_Ch_txbText(txb_sit_ip8, ip_List[7]);
                Do_Ch_txbText(txb_sit_ip9, ip_List[8]);
                Do_Ch_txbText(txb_sit_ip10, ip_List[9]);
                Do_Ch_txbText(txb_push_ip1, ip_List[10]);
                Do_Ch_txbText(txb_push_ip2, ip_List[11]);
                Do_Ch_txbText(txb_push_ip3, ip_List[12]);
                Do_Ch_txbText(txb_push_ip4, ip_List[13]);
                Do_Ch_txbText(txb_push_ip5, ip_List[14]);
                Do_Ch_txbText(txb_push_ip6, ip_List[15]);
                Do_Ch_txbText(txb_push_ip7, ip_List[16]);
                Do_Ch_txbText(txb_push_ip8, ip_List[17]);
                Do_Ch_txbText(txb_push_ip9, ip_List[18]);
                Do_Ch_txbText(txb_push_ip10, ip_List[19]);
            }
            
        }
        //IP設定儲存鍵
        private void btn_Set_IP_Click(object sender, EventArgs e)
        {
            if (IpSetOn == false)//關閉中
            {
                //打開欄位設定
                IpSetOn = true;
                btn_Set_IP.Text = "儲存IP設定";
                btn_Set_IP.ForeColor = Color.Green;
                Set_IP_OnOff(true);
            }
            else//開啟中
            {
                //關閉欄位設定
                Save_IP();
                IpSetOn = false;
                btn_Set_IP.Text = "設定IP";
                btn_Set_IP.ForeColor = Color.Red;
                Set_IP_OnOff(false);
            }
        }
        
        //全部測試鍵
        private  void btn_Ping_All_Click(object sender, EventArgs e)
        {
            Button[] Enter_btn = new Button[20] { btn_Ping_sit1, btn_Ping_sit2, btn_Ping_sit3, btn_Ping_sit4, btn_Ping_sit5, btn_Ping_sit6, btn_Ping_sit7, btn_Ping_sit8, btn_Ping_sit9, btn_Ping_sit10, btn_Ping_push1, btn_Ping_push2, btn_Ping_push3, btn_Ping_push4, btn_Ping_push5, btn_Ping_push6, btn_Ping_push7, btn_Ping_push8, btn_Ping_push9, btn_Ping_push10 };
            Button[] Status_btn = new Button[20] { btn_Status_sit1, btn_Status_sit2, btn_Status_sit3, btn_Status_sit4, btn_Status_sit5, btn_Status_sit6, btn_Status_sit7, btn_Status_sit8, btn_Status_sit9, btn_Status_sit10, btn_Status_push1, btn_Status_push2, btn_Status_push3, btn_Status_push4, btn_Status_push5, btn_Status_push6, btn_Status_push7, btn_Status_push8, btn_Status_push9, btn_Status_push10 };
            try
            {
                for (int i = 0; i < ip_List.Length; i++)
                {
                    Do_SendStatus(Status_btn[i], "測試中", Color.DarkGray, false);
                }
                Do_SendStatus(btn_Ping_All, "測試中…", Color.Black, false);
                for (int i = 0; i < ip_List.Length; i++)
                {
                    Do_CheckIP(Status_btn[i], "192.168.0." + ip_List[i], Enter_btn[i]);
                }
            }
            catch
            {
                btn_Ping_All.Text = "全機測試";
                btn_Ping_All.ForeColor = Color.Blue;
                btn_Ping_All.Enabled = true;
            }
            finally
            {

                Do_SendStatus(btn_Ping_All, "全機測試", Color.Blue, true);
            }
        }
        //單獨測試鍵
        private void btn_Ping_sit1_Click(object sender, EventArgs e)
        {
            btn_Status_sit1.Enabled = true;
            btn_Status_sit1.Text = "測試中";
            btn_Status_sit1.BackColor = Color.DarkGray;
            btn_Status_sit1.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit1, "192.168.0." + ip_List[0], bt);
        }

        private void btn_Ping_sit2_Click(object sender, EventArgs e)
        {
            btn_Status_sit2.Enabled = true;
            btn_Status_sit2.Text = "測試中";
            btn_Status_sit2.BackColor = Color.DarkGray;
            btn_Status_sit2.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit2, "192.168.0." + ip_List[1], bt);
        }

        private void btn_Ping_sit3_Click(object sender, EventArgs e)
        {
            btn_Status_sit3.Enabled = true;
            btn_Status_sit3.Text = "測試中";
            btn_Status_sit3.BackColor = Color.DarkGray;
            btn_Status_sit3.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit3, "192.168.0." + ip_List[2], bt);
        }

        private void btn_Ping_sit4_Click(object sender, EventArgs e)
        {
            btn_Status_sit4.Enabled = true;
            btn_Status_sit4.Text = "測試中";
            btn_Status_sit4.BackColor = Color.DarkGray;
            btn_Status_sit4.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit4, "192.168.0." + ip_List[3], bt);
        }

        private void btn_Ping_sit5_Click(object sender, EventArgs e)
        {
            btn_Status_sit5.Enabled = true;
            btn_Status_sit5.Text = "測試中";
            btn_Status_sit5.BackColor = Color.DarkGray;
            btn_Status_sit5.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit5, "192.168.0." + ip_List[4], bt);
        }

        private void btn_Ping_sit6_Click(object sender, EventArgs e)
        {
            btn_Status_sit6.Enabled = true;
            btn_Status_sit6.Text = "測試中";
            btn_Status_sit6.BackColor = Color.DarkGray;
            btn_Status_sit6.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit6, "192.168.0." + ip_List[5], bt);
        }

        private void btn_Ping_sit7_Click(object sender, EventArgs e)
        {
            btn_Status_sit7.Enabled = true;
            btn_Status_sit7.Text = "測試中";
            btn_Status_sit7.BackColor = Color.DarkGray;
            btn_Status_sit7.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit7, "192.168.0." + ip_List[6], bt);
        }

        private void btn_Ping_sit8_Click(object sender, EventArgs e)
        {
            btn_Status_sit8.Enabled = true;
            btn_Status_sit8.Text = "測試中";
            btn_Status_sit8.BackColor = Color.DarkGray;
            btn_Status_sit8.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit8, "192.168.0." + ip_List[7], bt);
        }

        private void btn_Ping_sit9_Click(object sender, EventArgs e)
        {
            btn_Status_sit9.Enabled = true;
            btn_Status_sit9.Text = "測試中";
            btn_Status_sit9.BackColor = Color.DarkGray;
            btn_Status_sit9.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit9, "192.168.0." + ip_List[8], bt);
        }

        private void btn_Ping_sit10_Click(object sender, EventArgs e)
        {
            btn_Status_sit10.Enabled = true;
            btn_Status_sit10.Text = "測試中";
            btn_Status_sit10.BackColor = Color.DarkGray;
            btn_Status_sit10.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_sit10, "192.168.0." + ip_List[9], bt);
        }

        private void btn_Ping_push1_Click(object sender, EventArgs e)
        {
            btn_Status_push1.Enabled = true;
            btn_Status_push1.Text = "測試中";
            btn_Status_push1.BackColor = Color.DarkGray;
            btn_Status_push1.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push1, "192.168.0." + ip_List[10], bt);
        }

        private void btn_Ping_push2_Click(object sender, EventArgs e)
        {
            btn_Status_push2.Enabled = true;
            btn_Status_push2.Text = "測試中";
            btn_Status_push2.BackColor = Color.DarkGray;
            btn_Status_push2.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push2, "192.168.0." + ip_List[11], bt);
        }

        private void btn_Ping_push3_Click(object sender, EventArgs e)
        {
            btn_Status_push3.Enabled = true;
            btn_Status_push3.Text = "測試中";
            btn_Status_push3.BackColor = Color.DarkGray;
            btn_Status_push3.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push3, "192.168.0." + ip_List[12], bt);
        }

        private void btn_Ping_push4_Click(object sender, EventArgs e)
        {
            btn_Status_push4.Enabled = true;
            btn_Status_push4.Text = "測試中";
            btn_Status_push4.BackColor = Color.DarkGray;
            btn_Status_push4.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push4, "192.168.0." + ip_List[13], bt);
        }

        private void btn_Ping_push5_Click(object sender, EventArgs e)
        {
            btn_Status_push5.Enabled = true;
            btn_Status_push5.Text = "測試中";
            btn_Status_push5.BackColor = Color.DarkGray;
            btn_Status_push5.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push5, "192.168.0." + ip_List[14], bt);
        }

        private void btn_Ping_push6_Click(object sender, EventArgs e)
        {
            btn_Status_push6.Enabled = true;
            btn_Status_push6.Text = "測試中";
            btn_Status_push6.BackColor = Color.DarkGray;
            btn_Status_push6.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push6, "192.168.0." + ip_List[15], bt);
        }

        private void btn_Ping_push7_Click(object sender, EventArgs e)
        {
            btn_Status_push7.Enabled = true;
            btn_Status_push7.Text = "測試中";
            btn_Status_push7.BackColor = Color.DarkGray;
            btn_Status_push7.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push7, "192.168.0." + ip_List[16], bt);
        }

        private void btn_Ping_push8_Click(object sender, EventArgs e)
        {
            btn_Status_push8.Enabled = true;
            btn_Status_push8.Text = "測試中";
            btn_Status_push8.BackColor = Color.DarkGray;
            btn_Status_push8.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push8, "192.168.0." + ip_List[17], bt);
        }

        private void btn_Ping_push9_Click(object sender, EventArgs e)
        {
            btn_Status_push9.Enabled = true;
            btn_Status_push9.Text = "測試中";
            btn_Status_push9.BackColor = Color.DarkGray;
            btn_Status_push9.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push9, "192.168.0." + ip_List[18], bt);
        }

        private void btn_Ping_push10_Click(object sender, EventArgs e)
        {
            btn_Status_push10.Enabled = true;
            btn_Status_push10.Text = "測試中";
            btn_Status_push10.BackColor = Color.DarkGray;
            btn_Status_push10.Enabled = false;
            Button bt = (Button)sender;
            Do_CheckIP(btn_Status_push10, "192.168.0." + ip_List[19], bt);
        }

        //總部連線測試
        delegate void PingAws_delegate();
        private void PingAWS()
        {
            try
            {
                btn_Status_AWS.Enabled = true;
                btn_Status_AWS.Text = "測試中";
                btn_Status_AWS.BackColor = Color.DarkGray;
                btn_Status_AWS.Enabled = false;
                btn_Ping_AWS.Text = "連線中…";
                btn_Ping_AWS.ForeColor = Color.Black;
                btn_Ping_AWS.Enabled = false;
                AWS = new AWS_WS.WebService2SoapClient();//連接到Web_SV
                //連線成功會回傳一個字串HelloWorld
                string testString = AWS.HelloWorld();
                //連線成功
                btn_Status_AWS.Text = "ON";
                btn_Status_AWS.BackColor = Color.Lime;
            }
            catch (Exception ex)
            {
                //連線失敗
                btn_Status_AWS.Text = "OFF";
                btn_Status_AWS.BackColor = Color.HotPink;
            }
            finally
            {
                btn_Ping_AWS.Text = "測試";
                btn_Ping_AWS.ForeColor = Color.Blue;
                btn_Ping_AWS.Enabled = true;
            }
        }
        private void Do_PingAws()
        {
            this.BeginInvoke(new PingAws_delegate(PingAWS));
        }
        private void btn_Ping_AWS_Click(object sender, EventArgs e)
        {
            Do_PingAws();
        }
        #endregion

        #region [手動立即更新最新數據圖表按鍵]
        //1、立即更新檢錄率
        private void btn_CheckReta_Now_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Check_Reta");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已檢錄", "未檢錄" };
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isCheck"].ToString()), Convert.ToInt16(dt.Rows[0]["noCheck"].ToString()) };
                chart_CheckReta.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
                chart_CheckReta.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_CheckReta.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
            }

        }
        //2、立即更新單項完成率
        private void btn_ItemTestedReta_Now_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_ItemTested");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y_sit;
                int[] Value_Y_push;
                int[] Value_Y_run;
                int[] Value_Y_swin;
                int[] Value_Y_walk;
                int[] Value_Y_jump;
                int[] Value_Y_uppole;
                int[] Value_Y_setpole;

                Value_X = new string[2] { "已測", "未測" };
                Value_Y_sit = new int[2] { Convert.ToInt16(dt.Rows[0]["sit_ups_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["sit_ups_notest"].ToString()) };
                Value_Y_push = new int[2] { Convert.ToInt16(dt.Rows[0]["push_ups_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["push_ups_notest"].ToString()) };
                Value_Y_run = new int[2] { Convert.ToInt16(dt.Rows[0]["run_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["run_notest"].ToString()) };
                Value_Y_swin = new int[2] { Convert.ToInt16(dt.Rows[0]["800m_swin_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["800m_swin_notest"].ToString()) };
                Value_Y_walk = new int[2] { Convert.ToInt16(dt.Rows[0]["5km_walk_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["5km_walk_notest"].ToString()) };
                Value_Y_jump = new int[2] { Convert.ToInt16(dt.Rows[0]["5min_jump_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["5min_jump_notest"].ToString()) };
                Value_Y_uppole = new int[2] { Convert.ToInt16(dt.Rows[0]["up_pole_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["up_pole_notest"].ToString()) };
                Value_Y_setpole = new int[2] { Convert.ToInt16(dt.Rows[0]["set_pole_istest"].ToString()), Convert.ToInt16(dt.Rows[0]["set_pole_notest"].ToString()) };

                //仰臥起坐圖表
                chart_ItemTestedReta.Series["Series1"].Points.DataBindXY(Value_X, Value_Y_sit);
                //俯地挺身圖表
                chart_ItemTestedReta.Series["Series2"].Points.DataBindXY(Value_X, Value_Y_push);
                //三千公尺圖表
                chart_ItemTestedReta.Series["Series3"].Points.DataBindXY(Value_X, Value_Y_run);
                //八百游走圖表
                chart_ItemTestedReta.Series["Series4"].Points.DataBindXY(Value_X, Value_Y_swin);
                //五公里健走圖表
                chart_ItemTestedReta.Series["Series5"].Points.DataBindXY(Value_X, Value_Y_walk);
                //五分鐘跳繩圖表
                chart_ItemTestedReta.Series["Series6"].Points.DataBindXY(Value_X, Value_Y_jump);
                //引體向上(男)圖表
                chart_ItemTestedReta.Series["Series7"].Points.DataBindXY(Value_X, Value_Y_uppole);
                //屈臂懸垂(女)圖表
                chart_ItemTestedReta.Series["Series8"].Points.DataBindXY(Value_X, Value_Y_setpole);

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series2"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series3"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series4"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series5"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series6"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series7"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                foreach (DataPoint dp in chart_ItemTestedReta.Series["Series8"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
                chart_ItemTestedReta.Annotations.Clear();

            }
        }
        //3、立即更新單項人數比例
        private void btn_ItemPlayerCount_Now_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_ItemTestCount");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[8] { "仰臥起坐", "俯地挺身", "三千公尺", "八百游走", "5公里健走", "5分鐘跳繩", "引體向上", "屈臂懸垂" };
                //測試資料
                //Value_Y = new int[8] { 120, 125, 55, 22, 45, 12, 5, 0 };
                //正試資料
                Value_Y = new int[8] { Convert.ToInt16(dt.Rows[0]["sit_ups_testcount"]), Convert.ToInt16(dt.Rows[0]["push_ups_testcount"]), Convert.ToInt16(dt.Rows[0]["run_testcount"]), Convert.ToInt16(dt.Rows[0]["800m_swin_testcount"]), Convert.ToInt16(dt.Rows[0]["5km_walk_testcount"]), Convert.ToInt16(dt.Rows[0]["5min_jump_testcount"]), Convert.ToInt16(dt.Rows[0]["up_pole_testcount"]), Convert.ToInt16(dt.Rows[0]["set_pole_testcount"]) };
                chart_ItemPlayerCount.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");

                chart_ItemPlayerCount.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
            }
        }
        //4、立即更新成績結算比例
        private void btn_Result_Complete_Now_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Result_Complete");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已結算", "未結算" };
                //測試資料
                //Value_Y = new int[2] { 45, 80 };
                //正式資料
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isComplete"]), Convert.ToInt16(dt.Rows[0]["noComplete"]) };

                chart_Result_Complete.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_Result_Complete.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_Result_Complete.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
            }
        }
        #endregion
        #region [自動更新數據圖表按鍵處理]
        //1、自動更新最新檢錄完成率
        private void btn_CheckReta_Auto_Click(object sender, EventArgs e)
        {
            if (Auto_Updt_CheckReta == false)//原本關閉，開啟
            {
                Auto_Updt_CheckReta = true;
                lab_AutoUp_CheckReta.Text = "開啟";
                lab_AutoUp_CheckReta.ForeColor = Color.Blue;
                btn_CheckReta_Auto.Text = "關閉自動更新";
                btn_CheckReta_Auto.ForeColor = Color.Red;

                timer1.Interval = 1000 * 10;
                timer1.Start();
            }
            else//原本開啟，關閉
            {
                Auto_Updt_CheckReta = false;
                lab_AutoUp_CheckReta.Text = "關閉";
                lab_AutoUp_CheckReta.ForeColor = Color.Red;
                btn_CheckReta_Auto.Text = "開啟自動更新";
                btn_CheckReta_Auto.ForeColor = Color.Blue;

                timer1.Stop();
            }
        }
        //1、自動更新最新檢錄完成率-計時器
        private void timer1_Tick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Check_Reta");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已檢錄", "未檢錄" };
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isCheck"].ToString()), Convert.ToInt16(dt.Rows[0]["noCheck"].ToString()) };
                chart_CheckReta.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);
                chart_CheckReta.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_CheckReta.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
            }
        }
        //2、自動更新最新成績結算完成率
        private void btn_Result_Complete_Auto_Click(object sender, EventArgs e)
        {
            if (Auto_Updt_CompleteReta == false)//原本關閉，開啟
            {
                Auto_Updt_CompleteReta = true;
                lab_AutoUp_Result_Complete.Text = "開啟";
                lab_AutoUp_Result_Complete.ForeColor = Color.Blue;
                btn_Result_Complete_Auto.Text = "關閉自動更新";
                btn_Result_Complete_Auto.ForeColor = Color.Red;

                timer2.Interval = 1000 * 10;
                timer2.Start();
            }
            else//原本開啟，關閉
            {
                Auto_Updt_CompleteReta = false;
                lab_AutoUp_Result_Complete.Text = "關閉";
                lab_AutoUp_Result_Complete.ForeColor = Color.Red;
                btn_Result_Complete_Auto.Text = "開啟自動更新";
                btn_Result_Complete_Auto.ForeColor = Color.Blue;

                timer2.Stop();
            }
        }
        //2、自動更新最新成績結算完成率-計時器
        private void timer2_Tick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Do_GetTable("Ex107_Get_Result_Complete");
            if (dt.Rows.Count > 0)
            {
                string[] Value_X;
                int[] Value_Y;
                Value_X = new string[2] { "已結算", "未結算" };
                //測試資料
                //Value_Y = new int[2] { 45, 80 };
                //正式資料
                Value_Y = new int[2] { Convert.ToInt16(dt.Rows[0]["isComplete"]), Convert.ToInt16(dt.Rows[0]["noComplete"]) };

                chart_Result_Complete.Legends["Legends1"].Title = Convert.ToDateTime(dt.Rows[0]["date"]).ToString("yyyy/MM/dd (dddd)");
                chart_Result_Complete.Series["Series1"].Points.DataBindXY(Value_X, Value_Y);

                //重設右上及圖形顯示內容
                foreach (DataPoint dp in chart_Result_Complete.Series["Series1"].Points)
                {
                    dp.LegendText = dp.AxisLabel;//右上圖例名稱
                    if (dp.YValues[0].ToString() == "0")
                        dp.AxisLabel = null;
                    else
                        dp.AxisLabel = dp.YValues[0].ToString();//圖形內文字
                }
            }
        }
        #endregion

        //ping網段內所有ip
        private void btn_inq_IP_Click(object sender, EventArgs e)
        {
            Ping p = new Ping();
            PingReply RePing;
            string ip_title="192.168.0.";
            string msg = string.Empty;
            btn_inq_IP.Text = "IP查詢中…";
            btn_inq_IP.Enabled = false;
            int n=1;
            while (n < 256)
            {
                try
                {
                    RePing = p.Send(ip_title + n.ToString(), 100);
                    if (RePing.Status == IPStatus.Success)
                    {
                        msg += ip_title + n.ToString() + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    n++;
                }  
            }
            btn_inq_IP.Enabled = true;
            btn_inq_IP.Text = "查詢可用IP";
            
            MessageBox.Show(msg);
            
        }
        //啟用ping全部ip按鍵，焦點停在俯3測試鍵再按P
        private void btn_Ping_push3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 80)
            {
                if (btn_inq_IP.Visible == true)
                {
                    btn_inq_IP.Visible = false;
                }
                else
                    btn_inq_IP.Visible = true;
            }
        }

    }
}
