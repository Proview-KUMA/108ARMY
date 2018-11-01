using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using CheckinLib;
using CheckinLib.device;
using System.Threading;
using System.Diagnostics;
using System.IO;
using PCComm;
using TouchlessLib;
using webcam;
using Lib;
using System.Runtime.InteropServices;
using DeviceInterfaceConsole;
using ScoreClose;


namespace InI
{
    public partial class Form1 : Form
    {
        //2018-11-1加入github
        public static SystemTimeForm ST_Form;
        //2017-11-16即時數據視窗
        public static DataChart dtChart_Fm;
        //2018-10-17歷史數據查詢
        public static HistoryChart hsChart_Fm;
        public string center_code = "1";
        public string center_name = "陸軍專校鑑測站";
        public bool checkbarcode = false;
        public string _memo = "000";
        public string LF_Tag_ID = string.Empty;
        public string UHF_Tag_ID = string.Empty;
        public string code = string.Empty;
        public string Gender = string.Empty;
        private static Image _latestFrame;
        public bool isCheckBirth = true;
        SerialPort bodyFatPort, heightPort;
        TouchlessMgr cMger;
        string heightDeviceMessage;
        Stage[] bodyFatStages;
        Measure measure;
        BodyFatController bodyfatDev;
        String hintMessage = "";
        Boolean measureGenderMale = true,
               deviceError = false, // body fat device status chacking
               bodyFatCanceled = false;
        delegate void Callback();
        //Register _newform = null;
        CommunicationManager comm = new CommunicationManager();
        public Boolean IsHavebodyfat = true;

        public Form1()
        {
            InitializeComponent();
            initDevice();

            bodyFatStages = new Stage[7];
            bodyFatStages[0] = new Stage("checkUnit");
            bodyFatStages[1] = new Stage("setClothingWt");
            bodyFatStages[2] = new Stage("setGender");
            bodyFatStages[3] = new Stage("setBodyType");
            bodyFatStages[4] = new Stage("setHeight");
            bodyFatStages[5] = new Stage("setAge");
            bodyFatStages[6] = new Stage("go");

            step1();
            tabControl1.SelectedTab = tabPage1;
            initMeasure();

        }

        public void initDevice()
        {
            GetAllDeviceClasses();
            /*******  init first CAM in usb ****/
            cMger = new TouchlessMgr();
            try
            {
                if (cMger.Cameras.Count == 1)
                {
                    Camera myCamera = cMger.Cameras[0];
                    cMger.CurrentCamera = myCamera;
                    myCamera.OnImageCaptured += new EventHandler<CameraEventArgs>(OnImageCaptured);
                    this.pictureBox1.Paint += new PaintEventHandler(drawLatestImage);
                }
                else
                {
                    MessageBox.Show("系統偵測攝影機數目為" + cMger.Cameras.Count + ", 請檢查攝影機是否連接");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            /*******  init LF rfid in  ****/
            comm.BaudRate = "9600";
            comm.Parity = "None";
            comm.StopBits = "One";
            comm.DataBits = "8";
            comm.PortName = "COM2";
            comm.DisplayWindow = richTextBox1;
            /*******  init height device  ****/
            try
            {
                heightPort = new SerialPort("COM4", 4800, Parity.None, 8, StopBits.One);
                heightPort.DataReceived += new SerialDataReceivedEventHandler(portHeight_DataReceived);
            }
            catch (Exception ex)
            {
                MessageBox.Show("身高機連線失敗,請檢查線路或連線控制權");
            }

            /*******  init body fat device ****/
            bodyfatDev = new BodyFatController("COM3");
            try
            {
                bodyFatPort = bodyfatDev.connect();
                bodyFatPort.DataReceived += new SerialDataReceivedEventHandler(portBodyFat_DataReceived);
                Thread.Sleep(50);
                bodyfatDev.setUnit(BodyFatController.Unit_KG);
            }
            catch (Exception ex)
            {
                IsHavebodyfat = false;
                MessageBox.Show("體脂機連線失敗,請檢查線路或連線控制權");
            }
        }

        private void initMeasure()
        {
            this.measure = new Measure();
            initBodyFatStageStatus();
            String clothingWeight = "";
            //String clothingWeight = myIni.IniReadValue("MEASURE", "CLOTHING_WT");
            if (clothingWeight.Length < 1)
            {
                clothingWeight = "1.0";
                //myIni.IniWriteValue("MEASURE", "CLOTHING_WT", clothingWeight);
            }
            measure.ClothingWeight = Double.Parse(clothingWeight);
        }
        //2018-1-25檢查資料年齡
        private Boolean CheckAge(int dataAge,string birthday)
        {
            DateTime dt=new DateTime();
            if (DateTime.TryParse(birthday.Trim(),out dt))
            {
                DateTime _birth_dt = Lib.SysSetting.ToWorldDate(birthday.Trim());
                int systemAge = Lib.SysSetting.ConvertAge(_birth_dt, System.DateTime.Today);
                if (dataAge == systemAge)
                {
                    //報名年齡跟系統年齡比對正確
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("該員報進年齡為「" + dataAge.ToString() + "」經系統重新計算正確年齡應為「" + systemAge.ToString() + "」" + Environment.NewLine + "請確認是否轉換為正確年齡?"+Environment.NewLine+Environment.NewLine+"---------------------------------------------------------------"+Environment.NewLine+"若轉換年齡錯誤，請完成以下檢查："+Environment.NewLine+"1、核對受測人員生日是否正確。"+Environment.NewLine+"2、檢查檢錄電腦系統時間設定是否正確。", "年齡比對確認", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                        _age.Text = systemAge.ToString();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                    
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void idinput_Click(object sender, EventArgs e)
        {
            //GetAllDeviceClasses();
            //if (System.Configuration.ConfigurationManager.AppSettings["IsAuto"].ToString() == "1")
            //{
            //    if (!checkbarcode)
            //    {
            //        MessageBox.Show("條碼機未正確安裝 , 程式即將關閉");
            //        Application.Exit();
            //    }
            //}

            if (step1_id.Text.Length == 10)
            {
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", step1_id.Text.Trim());
                DataTable dt = du.getDataTableBysp("GetPlayer", d);
                if (dt.Rows.Count > 0)
                {
                    _id.Text = dt.Rows[0]["id"].ToString().Trim();
                    _age.Text = dt.Rows[0]["age"].ToString().Trim();
                    _birth.Text = Lib.SysSetting.ToRocDateFormat(Convert.ToDateTime(dt.Rows[0]["birth"].ToString().Trim()).ToShortDateString());
                    _name.Text = dt.Rows[0]["name"].ToString().Trim();
                    _unit.Text = dt.Rows[0]["unit_title"].ToString().Trim();
                    _rank.Text = dt.Rows[0]["rank_title"].ToString().Trim();
                    _memo = dt.Rows[0]["memo"].ToString().Trim();
                    Gender = dt.Rows[0]["gender"].ToString();
                    if (dt.Rows[0]["gender"].ToString().Trim() == "M")
                    {
                        _gender.Text = "男性";
                    }
                    else if (dt.Rows[0]["gender"].ToString().Trim() == "F")
                    {
                        _gender.Text = "女性";
                    }
                    //2018-10-15檢查年齡
                    CheckAge(Convert.ToInt16(_age.Text.Trim()), _birth.Text.Trim());
                    NewReplaceitem _replaceform = new NewReplaceitem(this);
                    _3K_Change __3K_Change = new _3K_Change(this);
                        
                    //2016-12-16三千公尺四選一，這裡要把選基本三項也能勾選替代
                    switch (dt.Rows[0]["memo"].ToString().Trim())
                    {
                        case "000":
                            //2016-12-21報名基本三項跳出四選一視窗
                            //新版
                            btn_Change_3K.Enabled = true;
                            selectreplaceitem.Enabled = false;
                            //2018-10-15多元選項不值接顯示，如果年齡不是空的且大於等於45歲就直接顯示多元選項
                            if (!string.IsNullOrEmpty(_age.Text.Trim()) && Convert.ToInt16(_age.Text.Trim()) >= 45)
                            {
                                int age = Convert.ToInt16(_age.Text.Trim());
                                __3K_Change.Age = age;
                                __3K_Change.ShowDialog();
                            }
                            break;
                        case "":
                            selectreplaceitem.Enabled = true;
                            btn_Change_3K.Enabled = false;
                            break;
                        case "999":
                            selectreplaceitem.Enabled = true;
                            btn_Change_3K.Enabled = false;
                            _replaceform.ShowDialog();
                            break;
                        default:   //當情況是替代方案人員回來補測 , 必須補測上次測驗的項目
                            selectreplaceitem.Enabled = false;
                            btn_Change_3K.Enabled = false;
                            char[] temp_memo = _memo.ToCharArray();
                            checkedListBox1.Items.Clear();
                            checkedListBox2.Items.Clear();
                            Lib.DataUtility du_repment = new Lib.DataUtility();
                            Dictionary<string, object> d_repment = new Dictionary<string, object>();
                            if (temp_memo[0].ToString() != "0")
                            {
                                d_repment.Clear();
                                checkedListBox1.Items.Add("二分鐘仰臥起坐", true);
                                d_repment.Add("sid", temp_memo[0].ToString());
                                DataTable dt_sit = du_repment.getDataTableByText("select rep_title from RepMent where sid = @sid", d_repment);
                                if (dt_sit.Rows.Count > 0)
                                    checkedListBox2.Items.Add(dt_sit.Rows[0][0].ToString(), true);
                            }

                            if (temp_memo[1].ToString() != "0")
                            {
                                d_repment.Clear();
                                checkedListBox1.Items.Add("二分鐘俯地挺身", true);
                                d_repment.Add("sid", temp_memo[1].ToString());
                                DataTable dt_push = du_repment.getDataTableByText("select rep_title from RepMent where sid = @sid", d_repment);
                                if (dt_push.Rows.Count > 0)
                                    checkedListBox2.Items.Add(dt_push.Rows[0][0].ToString(), true);
                            }

                            if (temp_memo[2].ToString() != "0")
                            {
                                d_repment.Clear();
                                checkedListBox1.Items.Add("三千公尺徒手跑步", true);
                                d_repment.Add("sid", temp_memo[2].ToString());
                                DataTable dt_run = du_repment.getDataTableByText("select rep_title from RepMent where sid = @sid", d_repment);
                                if (dt_run.Rows.Count > 0)
                                    checkedListBox2.Items.Add(dt_run.Rows[0][0].ToString(), true);
                            }
                            break;
                    }

                    tabControl1.SelectedTab = tabPage2;
                    step1_inputid();
                    comm.OpenPort();
                    updateHintMessage("請感應腕錶");

                }
                else
                {
                    updateHintMessage("查無報進資訊");
                }
            }
        }

        private void capture_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(img);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Point ori = new Point(0, 0);
            g.CopyFromScreen(pictureBox1.PointToScreen(ori), ori, new Size(pictureBox1.Width, pictureBox1.Height));
            IntPtr dc = g.GetHdc();
            g.ReleaseHdc(dc);
            this.pictureBox2.Image = img;
            try
            {
                if (this.heightPort != null && !this.heightPort.IsOpen)
                    this.heightPort.Open();

                step3();
                updateHintMessage("請站上身高機量測身理資訊");
                tabControl1.SelectedTab = tabPage3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("身高機連線異常");
            }

        }

        public void getregister(string reg_id, string reg_birth, string reg_gender, string reg_rank_title, string reg_unit_title, string reg_name, DateTime reg_date)
        {
            if (reg_gender == "男")
                Gender = "M";
            else
                Gender = "F";

            _id.Text = reg_id;
            _birth.Text = reg_birth;
            _gender.Text = reg_gender;
            _rank.Text = reg_rank_title;
            _unit.Text = reg_unit_title;
            _name.Text = reg_name;
            _age.Text = Lib.SysSetting.ConvertAge(Lib.SysSetting.ToWorldDate(reg_birth), System.DateTime.Today).ToString();
            selectreplaceitem.Enabled = true;
            tabControl1.SelectedTab = tabPage2;
            comm.OpenPort();
            updateHintMessage("請感應腕錶");
        }
        //建立四選一勾選配對方法
        public void get_Change_3K_Item(int Select_Item)
        {
            string memo = string.Empty;
            memo = "000";
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            try
            {
                int item = Select_Item;
                switch (item)
                {
                    case 1:
                        checkedListBox1.Items.Clear();
                        checkedListBox2.Items.Clear();
                        memo = "000";
                        break;
                    case 2:
                        checkedListBox1.Items.Add("三千公尺徒手跑步");
                        checkedListBox2.Items.Add("5公里健走");
                        memo = "00G";
                        break;
                    case 3:
                        checkedListBox1.Items.Add("三千公尺徒手跑步");
                        checkedListBox2.Items.Add("5分鐘跳繩");
                        memo = "00H";
                        break;
                    case 4:
                        checkedListBox1.Items.Add("三千公尺徒手跑步");
                        checkedListBox2.Items.Add("800公尺游走");
                        memo = "00F";
                        break;
                    default:
                        checkedListBox1.Items.Clear();
                        checkedListBox2.Items.Clear();
                        memo = "000";
                        break;
                }
            }
            catch (Exception ex)
            {
                label1.Text = memo;
            }
            //2017-1-19
            _memo = memo;
            label1.Text = memo;
        }

        //2018-10-31新增回傳替代項目
        public void GetRepItem(string memo,string[] itmeName)
        {
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            if (memo.Substring(0, 1) != "0")
            {
                checkedListBox1.Items.Add("仰臥起坐");
                checkedListBox2.Items.Add(itmeName[0]);
            }
            if (memo.Substring(1, 1) != "0")
            {
                checkedListBox1.Items.Add("俯地挺身");
                checkedListBox2.Items.Add(itmeName[1]);
            }
            if (memo.Substring(2, 1) != "0")
            {
                checkedListBox1.Items.Add("三千跑步");
                checkedListBox2.Items.Add(itmeName[2]);
            }

            _memo = memo;
            label1.Text = _memo;
        }

        //原本替代項目勾選配對方法
        public void getreplactitem(CheckedListBox original, CheckedListBox replaceitem)
        {
            char[] temp_memo = "000".ToCharArray();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            for (int i = 0; i < original.CheckedItems.Count; i++)
            {
                checkedListBox1.Items.Add(original.CheckedItems[i].ToString(), true);
            }
            for (int i = 0; i < replaceitem.CheckedItems.Count; i++)
            {
                checkedListBox2.Items.Add(replaceitem.CheckedItems[i].ToString(), true);
            }
            if (checkedListBox2.CheckedItems.Count > 0)
            {
         
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.Items[i].ToString() == "二分鐘仰臥起坐")
                    {
                        d.Clear();
                        d.Add("rep_title", checkedListBox2.Items[i].ToString());
                        DataTable dt_sit = du.getDataTableByText("select sid from RepMent where rep_title = @rep_title", d);
                        if (dt_sit.Rows.Count > 0)
                            temp_memo[0] = Convert.ToChar(dt_sit.Rows[0]["sid"].ToString());
                    }

                    if (checkedListBox1.Items[i].ToString() == "二分鐘俯地挺身")
                    {
                        d.Clear();
                        d.Add("rep_title", checkedListBox2.Items[i].ToString());
                        DataTable dt_push = du.getDataTableByText("select sid from RepMent where rep_title = @rep_title", d);
                        if (dt_push.Rows.Count > 0)
                            temp_memo[1] = Convert.ToChar(dt_push.Rows[0]["sid"].ToString());
                    }
                    if (checkedListBox1.Items[i].ToString() == "三千公尺徒手跑步")
                    {
                        d.Clear();
                        d.Add("rep_title", checkedListBox2.Items[i].ToString());
                        DataTable dt_run = du.getDataTableByText("select sid from RepMent where rep_title = @rep_title", d);
                        if (dt_run.Rows.Count > 0)
                            temp_memo[2] = Convert.ToChar(dt_run.Rows[0]["sid"].ToString());
                    }


                }

            }


            _memo = new string(temp_memo);
            label1.Text = _memo;
        }

        #region webcam device

        public void OnImageCaptured(object sender, CameraEventArgs args)
        {
            _latestFrame = args.Image;
            this.pictureBox1.Invalidate();
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
            }
        }

        #endregion

        #region rfid device

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength == 10)
            {
                try
                {
                    Lib.DataUtility du = new Lib.DataUtility();
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("LF_Tag_ID", richTextBox1.Text);
                    d.Add("date", System.DateTime.Today);
                    DataTable dt_checkrfid = du.getDataTableBysp("CheckRfid", d);
                    if (dt_checkrfid.Rows.Count > 0)
                    {
                        MessageBox.Show("此腕錶在" + dt_checkrfid.Rows[0]["date"].ToString() + "仍由" + dt_checkrfid.Rows[0]["id"].ToString() + "鎖定(狀態為鑑測中) , 請選擇其他腕錶");
                        richTextBox1.Text = "";
                        _backnum.Text = "";
                    }
                    else
                    {
                        d.Clear();
                        d.Add("LF_Tag_ID", richTextBox1.Text);
                        DataTable dt = du.getDataTableBysp("GetRfid", d);
                        if (dt.Rows.Count == 1)
                        {
                            _backnum.Text = Convert.ToInt32(dt.Rows[0]["code"].ToString().Substring(1)).ToString();
                            richTextBox1.Text = "";
                            LF_Tag_ID = dt.Rows[0]["LF_Tag_ID"].ToString().Trim();
                            UHF_Tag_ID = dt.Rows[0]["UHF_Tag_ID"].ToString().Trim();
                            code = dt.Rows[0]["code"].ToString().Trim();
                            _tagid.Text = dt.Rows[0]["code"].ToString().Trim();
                            step2(); //流程進入Step2
                            updateHintMessage("請擷取受測者照片");
                        }
                        else
                        {
                            richTextBox1.Text = "";
                            _backnum.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion
        //量測體脂
        #region bodyfatdevice
        private void portBodyFat_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort bodyfat = (SerialPort)sender;
            String message = bodyfat.ReadLine();
            if (message.Length == 3)
            {
                if (message.Equals("U0\r")) //confirm unit setting
                    bodyFatStages[0].Ok = true;
                else if (message.Equals("D0\r")) //confirm clothing wt setting
                    bodyFatStages[1].Ok = true;
                else if (message.Equals("D1\r")) //confirm gender setting
                    bodyFatStages[2].Ok = true;
                else if (message.Equals("D2\r")) //confirm body type setting
                    bodyFatStages[3].Ok = true;
                else if (message.Equals("D3\r")) //confirm height setting
                    bodyFatStages[4].Ok = true;
                else if (message.Equals("D4\r")) //confirm age setting
                    bodyFatStages[5].Ok = true;
                else if (message.Equals("G1\r"))
                {//start analyzing
                    bodyFatStages[6].Ok = true;
                    this.updateHintMessage("請在體脂機嗶聲後站上,雙手緊握把手,並維持身體穩定平衡.");
                }
            }
            else
            {
                //Console.WriteLine(message);
            }

            if (message[0] == 0x1E)
            {
                //cancel successfully
                this.bodyFatCanceled = true;
            }

            if (message.StartsWith("\""))
            {
                //it's an report
                this.updateHintMessage("體脂率量測完成,請稍候體脂機自動重置");
                bodyFatStages[0].Ok = false;
                bodyFatStages[1].Ok = false;
                bodyFatStages[2].Ok = false;
                bodyFatStages[3].Ok = false;
                bodyFatStages[4].Ok = false;
                bodyFatStages[5].Ok = false;
                bodyFatStages[6].Ok = false;
                String[] report = message.Split(',');
                this.measure.BodyFat = Double.Parse(report[6]);
                this.measure.Bmr = double.Parse(report[12]);
                //將體脂送回醫官判定介面
                this.Invoke(new Callback(updateBodyFatMeasure));
                //this.Invoke(new Callback(checkBodyfat));
                //this.Invoke(new Callback(disableBodyfatUserControlsAndStartWaitingTimer));
            }
            else if (message.StartsWith("E"))
            {
                this.Invoke(new Callback(updateBodyFatMeasure));
                //this.Invoke(new Callback(uncheckBodyfat));
                //this.Invoke(new Callback(enableBodyfatUserControls));
                this.updateHintMessage("體脂率量測失敗,請手動重置體脂機");
            }
        }

        private void updateBodyFatMeasure()
        {
            //體脂量測完，判定體脂
            this._bodyfat.Text = this.measure.BodyFat.ToString();//體脂
            double bmrInKcal = (this.measure.Bmr / 4.184);
            this._rate.Text = bmrInKcal.ToString("0.0") + " kcal";
            this._weight.Text = this.measure.Weight.ToString("0.0");

            if (this.measure.Gender == BodyFatController.Gender_M)
            {
                MessageBox.Show("男性體脂率:" + this.measure.BodyFat.ToString() + ",請醫官判定是否未達受測標準.");
            }
            if (this.measure.Gender == BodyFatController.Gender_F)
            {
                MessageBox.Show("女性體脂率:" + this.measure.BodyFat.ToString() + ",請醫官判定是否未達受測標準.");
            }

            //if ((this.measure.Gender == BodyFatController.Gender_M) && (this.measure.BodyFat > 25))
            //{
            //    MessageBox.Show("男性體脂率超過25%,請醫官判定是否未達受測標準.");
            //    //tabControl1.SelectedTab = tabPage4;
            //bodyfaton.Enabled = false;
            //    //bodyfatcancel.Enabled = false;
            //    //this.enableNotPassBtn();
            //}
            //else if ((this.measure.Gender == BodyFatController.Gender_F) && (this.measure.BodyFat > 30))
            //{
            //    MessageBox.Show("女性體脂率超過30%,請醫官判定是否未達受測標準.");    
            //}
            tabControl1.SelectedTab = tabPage4;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            this.enableNotPassBtn();
        }

        private void bodyfatcancel_Click(object sender, EventArgs e)
        {
            this.bodyfatDev.cancleMeasure();
            int time = 0;

            while (!this.bodyFatCanceled && time++ < 20)
            {
                this.bodyfatDev.cancleMeasure();
                Thread.Sleep(200);
            }

            if (time < 20)
            {
                this.Invoke(new Callback(enableFlowBtn));
                this.updateHintMessage("體脂肪量測已取消");
            }
            else
            {
                MessageBox.Show("體脂機連線異常, 請嘗試再按一次取消");
            }
        }

        private void bodyfaton_Click(object sender, EventArgs e)
        {
            startBodyFatMeasureingProcedure();
        }

        private void resetBodyFatMeasure()
        {
            this._bodyfat.Text = "N / A";
            this._rate.Text = "N / A";
        }

        private void startBodyFatMeasureingProcedure()
        {
            this.measure.Age = Convert.ToInt32(_age.Text);
            //this.timerMesureReset.Stop();
            if (this.measure.Age > 16 && this.measure.Height > 0)
            {
                this.bodyFatCanceled = false;
                this.Invoke(new Callback(disableAllBtn));
                this.Invoke(new Callback(enableCancleBodyFatBtn));
                this.Invoke(new Callback(resetBodyFatMeasure));
                initBodyFatStageStatus();
                this.fetchBodyFatPara();
                //wait for final Ack code (Age setting Ack) from body fat device
                if (!deviceError)
                {
                    int timeoutChecker = 0;
                    while (!bodyFatStages[5].Ok && timeoutChecker++ < 10)
                    {
                        bodyfatDev.setAge(measure.Age);
                        Thread.Sleep(100);
                    }
                    if (timeoutChecker > 10)
                    {
                        deviceError = true;
                        messageBodyFatDeviceError();
                    }
                    else
                    {
                        //                      this.updateHintMessage("請在體脂機嗶聲後站上,雙手緊握把手,並維持身體穩定平衡.");
                        int timeoutChecker2 = 0;
                        while (!bodyFatStages[6].Ok && timeoutChecker2++ < 10)
                        {
                            bodyfatDev.start();
                            Thread.Sleep(100);
                        }
                        if (timeoutChecker > 10)
                        {
                            deviceError = true;
                            messageBodyFatDeviceError();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("年齡或身高有誤,無法量測體脂肪");
            }
        }

        private void initBodyFatStageStatus()
        {
            bodyFatStages[1].Ok = false;
            bodyFatStages[2].Ok = false;
            bodyFatStages[3].Ok = false;
            bodyFatStages[4].Ok = false;
            bodyFatStages[5].Ok = false;
            bodyFatStages[6].Ok = false;
        }

        private void messageBodyFatDeviceError()
        {
            MessageBox.Show("體脂機連線異常,請重新測量.");
            this.Invoke(new Callback(enableFlowBtn));
        }

        private void fetchBodyFatPara()
        {
            if (this.measureGenderMale)
            {
                this.measure.Gender = BodyFatController.Gender_M;
            }
            else
            {
                this.measure.Gender = BodyFatController.Gender_F;
            }

            this.measure.Height = this.measure.Height;
            this.measure.Age = Int16.Parse(this._age.Text);
            //while (!bodyFatStages[0].Ok) { Thread.Sleep(50); }
            //1. set clothing weight
            bodyfatDev.setClothingWt(measure.ClothingWeight);
            deviceError = false;
            int timeoutChecker = 0;
            while (!bodyFatStages[1].Ok && timeoutChecker++ < 10)
            {
                bodyfatDev.setClothingWt(measure.ClothingWeight);
                Thread.Sleep(100);
            }
            //2. check status and set gender
            if (timeoutChecker < 10)
            {
                timeoutChecker = 0;
                bodyfatDev.setGender(measure.Gender);
            }
            else
            {
                deviceError = true;
                messageBodyFatDeviceError();
            }

            if (!deviceError)
            {
                while (!bodyFatStages[2].Ok && timeoutChecker++ < 10)
                {
                    bodyfatDev.setGender(measure.Gender);
                    Thread.Sleep(100);
                }
                //3. check status and set body type
                if (timeoutChecker < 10)
                {
                    timeoutChecker = 0;
                    bodyfatDev.setBodyType(BodyFatController.Body_Std);
                }
                else
                {
                    deviceError = true;
                    messageBodyFatDeviceError();
                }
            }

            if (!deviceError)
            {
                while (!bodyFatStages[3].Ok && timeoutChecker++ < 10)
                {
                    bodyfatDev.setBodyType(BodyFatController.Body_Std);
                    Thread.Sleep(100);
                }
                //4. check status and set height
                if (timeoutChecker < 10)
                {
                    timeoutChecker = 0;
                    bodyfatDev.setHeight(measure.Height);
                }
                else
                {
                    deviceError = true;
                    messageBodyFatDeviceError();
                }
            }

            if (!deviceError)
            {
                while (!bodyFatStages[4].Ok && timeoutChecker++ < 10)
                {
                    bodyfatDev.setHeight(measure.Height);
                    Thread.Sleep(100);
                }

                //5. check status and set age
                if (timeoutChecker < 10)
                {
                    timeoutChecker = 0;
                    bodyfatDev.setAge(measure.Age);
                }
                else
                {
                    deviceError = true;
                    messageBodyFatDeviceError();
                }
            }
        }
        #endregion

        #region height device
        private void portHeight_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //2016年12-6，身高體重器傳回字串範例：Height168.5cmWeight78.00Kg
            SerialPort height = (SerialPort)sender;
            String message = height.ReadExisting();
            this.heightDeviceMessage += message;

            if (this.heightDeviceMessage.Contains("Height") && this.heightDeviceMessage.Contains("Kg"))
            {
                try
                {
                    String ht = this.heightDeviceMessage.Substring(heightDeviceMessage.IndexOf("cm") - 5, 5);
                    ht = ht.TrimStart(' ');
                    String wt = this.heightDeviceMessage.Substring(heightDeviceMessage.IndexOf("Weight") + 6);
                    int wtPointIdx = wt.IndexOf('.');
                    wt = wt.Substring(0, wtPointIdx + 2);
                    this.measure.Height = Double.Parse(ht);
                    this.measure.Weight = Double.Parse(wt);
                    this.measure.Bmi = this.measure.Weight / (this.measure.Height / 100) / (this.measure.Height / 100);
                    this.measure.ArmLength = (this.measure.Height * 0.16);
                    this.heightDeviceMessage = "";
                    this.heightPort.Close();
                    this.Invoke(new Callback(updateBmiMeasure));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("身高機連線異常");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void hitmeasure_Click(object sender, EventArgs e)
        {
            this.measure.ArmLength = 0.0;
            this.measure.Height = 0.0;
            this.measure.Weight = 0.0;
            this.measure.Bmi = 0.0;
            this._height.Text = this.measure.Height.ToString("0.0");
            this._weight.Text = this.measure.Weight.ToString("0.0");
            this._bmi.Text = this.measure.Bmi.ToString("0.0");
            try
            {
                if (this.heightPort != null && !this.heightPort.IsOpen)
                    this.heightPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("身高機連線異常");
            }
        }

        private void updateBmiMeasure()
        {

            this._height.Text = this.measure.Height.ToString("0.0");
            this._weight.Text = this.measure.Weight.ToString("0.0");
            this._bmi.Text = this.measure.Bmi.ToString("0.0");
            //2016-12-27新增修訂
            //需判斷男生還是女生
            //男生17<=BMI<=31，女生17<=BMI<=26
            //體脂男生小於25，女生小於30，要三選一(800游走、5公里健走、5分鐘跳繩)，大於的就「免測」

            //判斷男女
            if (_gender.Text == "男性")
            {
                if (this.measure.Bmi > 31)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["IsUsedBodyfat"].ToString() == "1")
                    {
                        MessageBox.Show("「男性」BMI超過31,請進行體脂肪檢驗.");
                        Thread.Sleep(100);
                        step3_bodyfaton();
                        this.Invoke(new Callback(enableInitFlowBtn));
                    }
                    else
                    {
                        MessageBox.Show("「男性」BMI超過31,請至其他檢錄站重新檢錄");
                        resetallflow();
                        updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                    }
                }
                else
                {
                    updateHintMessage("基本量測完成,請進行確認");
                    tabControl1.SelectedTab = tabPage4;
                    step4_finish();
                    //this.Invoke(new Callback(enableInitFlowBtn));
                }
            }
            else if (_gender.Text == "女性")
            {
                if (this.measure.Bmi > 26)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["IsUsedBodyfat"].ToString() == "1")
                    {
                        MessageBox.Show("「女性」BMI超過26,請進行體脂肪檢驗.");
                        Thread.Sleep(100);
                        step3_bodyfaton();
                        this.Invoke(new Callback(enableInitFlowBtn));
                    }
                    else
                    {
                        MessageBox.Show("「女性」BMI超過26,請至其他檢錄站重新檢錄");
                        resetallflow();
                        updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                    }
                }
                else
                {
                    updateHintMessage("基本量測完成,請進行確認");
                    tabControl1.SelectedTab = tabPage4;
                    step4_finish();
                    //this.Invoke(new Callback(enableInitFlowBtn));
                }
            }
            else
            {
                MessageBox.Show("「性別」欄位顯示異常，無法進行下一步驟!!");
            }
            
        }
        #endregion

        #region message function
        private void updateHintMessage(String msg)
        {
            this.hintMessage = msg;
            this.Invoke(new Callback(updateHintLbl));
        }

        private void updateHintLbl()
        {
            this.Message.Text = this.hintMessage;
        }
        #endregion

        //button 流程控制
        #region flow control
        private void step1() //初始化由輸入身分證字號開始
        {
            idinput.Enabled = true;
            capture.Enabled = false;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = false;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
            step1_id.Focus();
        }

        private void step1_inputid() //輸入完身份證載入受測者資料後~
        {
            idinput.Enabled = false;
            capture.Enabled = false;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = false;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
        }

        private void step2() //感應腕錶後啟動拍照
        {
            idinput.Enabled = false;
            capture.Enabled = true;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = false;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
        }

        private void step3() //拍照後啟動手動測量身高體重
        {
            idinput.Enabled = false;
            capture.Enabled = true;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = true;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
        }

        private void step3_bodyfaton() //量完身高體重發生BMI高於30
        {
            idinput.Enabled = false;
            capture.Enabled = true;
            bodyfaton.Enabled = true;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = false;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
        }

        private void step4()
        {
            idinput.Enabled = false;
            capture.Enabled = true;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = true;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
        }

        private void step4_finish()
        {
            idinput.Enabled = false;
            capture.Enabled = true;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = true;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = true;
        }

        private void enableFlowBtn() //可手動測量身高機與體脂機
        {
            hitmeasure.Enabled = true;
            bodyfaton.Enabled = true;
            bodyfatcancel.Enabled = false;
        }

        private void enableCancleBodyFatBtn()
        {
            this.bodyfatcancel.Enabled = true;
            this.bodyfaton.Enabled = false;
        }

        private void enableInitFlowBtn()
        {
            bodyfaton.Enabled = true;
        }

        private void disableAllBtn()
        {

        }

        private void resetallflow()
        {
            idinput.Enabled = true;
            capture.Enabled = false;
            bodyfaton.Enabled = false;
            bodyfatcancel.Enabled = false;
            hitmeasure.Enabled = false;
            musttest.Enabled = false;
            notest.Enabled = false;
            finishcheckin.Enabled = false;
            selectreplaceitem.Enabled = false;
            btn_Change_3K.Enabled = false;
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            tabControl1.SelectedTab = tabPage1;
            pictureBox2.Image = null;
            LF_Tag_ID = string.Empty;
            UHF_Tag_ID = string.Empty;
            code = string.Empty;
            label1.Text = "000";
            _memo = "000";
            step1_id.Text = "";
            _id.Text = "A123456789";
            _name.Text = "王小明";
            _gender.Text = "男性";
            _rank.Text = "二等兵";
            _birth.Text = "";
            _age.Text = "29";
            _unit.Text = "國防部";
            _backnum.Text = "";
            _tagid.Text = "X000";
            _height.Text = "N / A";
            _weight.Text = "N / A";
            _bmi.Text = "N / A";
            _bodyfat.Text = "N / A";
            _rate.Text = "N / A";
        }

        private void enableNotPassBtn()
        {
            this.notest.Enabled = true;
            this.musttest.Enabled = true;
            finishcheckin.Enabled = false;
        }
        #endregion

        private void 現場報名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register _newform = new Register(this);
            _newform.ShowDialog();
        }

        private void selectreplaceitem_Click(object sender, EventArgs e)
        {
            ////舊的替代項目選單
            //Replaceitem _replaceitme = new Replaceitem(this);
            //_replaceitme.ShowDialog();
            //新的替代項目選單
            NewReplaceitem nrpFm = new NewReplaceitem(this);
            nrpFm.ShowDialog();

        }

        private void finishcheckin_Click(object sender, EventArgs e)
        {
            //if (isCheckBirth)
            //{
            try
            {
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("LF_Tag_ID", LF_Tag_ID);
                d.Add("date", System.DateTime.Today);
                DataTable dt_checkrfid = du.getDataTableBysp("CheckRfid", d);
                if (dt_checkrfid.Rows.Count > 0)
                {
                    //MessageBox.Show("此腕錶已經重複註冊,請重新檢錄");
                    MessageBox.Show("此腕錶在" + dt_checkrfid.Rows[0]["date"].ToString() + "仍由" + dt_checkrfid.Rows[0]["id"].ToString() + "鎖定(狀態為鑑測中) , 請選擇其他腕錶");
                    initMeasure();
                    resetallflow();
                    updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                }
                else
                {
                    try
                    {
                        int clothesNum = Convert.ToInt32(_backnum.Text.Trim());
                        d.Clear();
                        d.Add("clothesNum", clothesNum);
                        d.Add("date", System.DateTime.Today);
                        DataTable dt = du.getDataTableBysp("CheckClothesNum", d);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("背號已經重複使用 , 請選擇其他背號");
                            _backnum.Focus();
                        }
                        else
                        {
                            //2018-1-24新增檢查年齡，比對系統年齡跟資料欄位年齡
                            //DialogResult myResult = MessageBox.Show("你要選是還是否?", "顯示在彈出視窗上面的字", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            saveresult();
                            MessageBox.Show("完成檢錄");
                            initMeasure();
                            resetallflow();
                            updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("請重新確認背號");
                        _backnum.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // }
        }

        private void musttest_Click(object sender, EventArgs e)
        {
            //2016-12-27應測需先判定第三項代碼是不是0
            if (label1.Text.Substring(2, 1) == "0")//第三項選了0：三千公尺
            {
                MessageBox.Show("男性BMI超過31且體脂小於25、女性BMI超過26且體脂小於30者，不可施測「三千公尺徒手跑步」項目，僅可選擇「5公里健走」、「800公尺游走」、「5分鐘跳繩」替代項目施測，以維訓測安全。");
            }
            else//第三項未選擇三千公尺
            {
                if (isCheckBirth)
                {
                    try
                    {
                        Lib.DataUtility du = new Lib.DataUtility();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        d.Add("LF_Tag_ID", LF_Tag_ID);
                        d.Add("date", System.DateTime.Today);
                        DataTable dt_checkrfid = du.getDataTableBysp("CheckRfid", d);
                        if (dt_checkrfid.Rows.Count > 0)
                        {
                            //MessageBox.Show("此腕錶已經重複註冊,請重新檢錄");
                            MessageBox.Show("此腕錶在" + dt_checkrfid.Rows[0]["date"].ToString() + "仍由" + dt_checkrfid.Rows[0]["id"].ToString() + "鎖定(狀態為鑑測中) , 請選擇其他腕錶");
                            initMeasure();
                            resetallflow();
                            updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                        }
                        else
                        {
                            try
                            {
                                int clothesNum = Convert.ToInt32(_backnum.Text.Trim());
                                d.Clear();
                                d.Add("clothesNum", clothesNum);
                                d.Add("date", System.DateTime.Today);
                                DataTable dt = du.getDataTableBysp("CheckClothesNum", d);
                                if (dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("背號已經重複使用 , 請選擇其他背號");
                                    _backnum.Focus();
                                }
                                else
                                {
                                    savemustresult();
                                    MessageBox.Show("完成檢錄 , 醫官判定為應測");
                                    initMeasure();
                                    resetallflow();
                                    updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("請重新確認背號");
                                _backnum.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            
        }

        private void notest_Click(object sender, EventArgs e)
        {
            if (isCheckBirth)
            {
                try
                {
                    savenotest();
                    MessageBox.Show("完成檢錄 , 醫官判定為免測");
                    initMeasure();
                    resetallflow();
                    updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //2016-11-21檢錄完成更新資料庫動作(正常程序)
        void savenotest()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", _id.Text);
                d.Add("date", System.DateTime.Today);
                d.Add("age", Convert.ToInt32(_age.Text));
                du.executeNonQueryByText("update Result set age = @age where id = @id and status = '999' and date = @date", d);
                d.Clear();
                d.Add("height", float.Parse(_height.Text));
                d.Add("weight", float.Parse(_weight.Text));
                d.Add("BMI", float.Parse(_bmi.Text));
                d.Add("bodyfat", float.Parse(_bodyfat.Text.Trim()));
                d.Add("photo", ms.ToArray());
                d.Add("id", _id.Text);
                d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text));
                if (string.IsNullOrEmpty(_memo))
                {
                    _memo = "000";
                }
                else if (_memo == "999")
                {
                    _memo = "000";
                }               
                else
                {
                }
                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                d.Add("status", "104");
                //新增op_id
                du.executeNonQueryByText("update result set height = @height,weight = @weight,BMI = @BMI,bodyfat = @bodyfat,photo = @photo,birth = @birth,memo = @memo,status = @status where id = @id and date = @date and status = '999'", d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ms.Dispose();
            }
        }
        //2016-11-21檢錄完成更新資料庫動作(醫官判定應測)
        void saveresult()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", _id.Text);
                d.Add("date", System.DateTime.Today);
                d.Add("age", Convert.ToInt32(_age.Text));
                du.executeNonQueryByText("update Result set age = @age where id = @id and status = '999' and date = @date", d);
                d.Clear();
                d.Add("height", float.Parse(_height.Text));
                d.Add("weight", float.Parse(_weight.Text));
                d.Add("BMI", float.Parse(_bmi.Text));
                d.Add("clothesNum", Convert.ToInt32(_backnum.Text));
                d.Add("LF_Tag_ID", LF_Tag_ID);
                d.Add("UHF_Tag_ID", UHF_Tag_ID);
                d.Add("code", code);
                d.Add("photo", ms.ToArray());
                d.Add("id", _id.Text);
                d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text));
                
                if (string.IsNullOrEmpty(_memo))
                {
                    _memo = "000";
                }
                else if (_memo == "999")
                {
                    _memo = "000";
                }
                else
                {
                }

                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                du.executeNonQueryBysp("SaveResult", d);//SP需新增op_id選項
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ms.Dispose();
            }
        }

        void savemustresult()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", _id.Text);
                d.Add("date", System.DateTime.Today);
                d.Add("age", Convert.ToInt32(_age.Text));
                du.executeNonQueryByText("update Result set age = @age where id = @id and status = '999' and date = @date", d);
                d.Clear();
                d.Add("height", float.Parse(_height.Text.Trim()));
                d.Add("weight", float.Parse(_weight.Text.Trim()));
                d.Add("bodyfat", float.Parse(_bodyfat.Text.Trim()));
                d.Add("BMI", float.Parse(_bmi.Text.Trim()));
                d.Add("clothesNum", Convert.ToInt32(_backnum.Text.Trim()));
                d.Add("LF_Tag_ID", LF_Tag_ID);
                d.Add("UHF_Tag_ID", UHF_Tag_ID);
                d.Add("code", code);
                d.Add("photo", ms.ToArray());
                d.Add("id", _id.Text);
                d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text.Trim()));
                if (string.IsNullOrEmpty(_memo))
                {
                    _memo = "000";
                }
                else if (_memo == "999")
                {
                    _memo = "000";
                }                
                else
                {
                }

                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                du.executeNonQueryBysp("SaveMustResult", d);//SP需新增op_id選項
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ms.Dispose();
            }
        }

        private void _birth_Leave(object sender, EventArgs e)
        {
            try
            {
                if (_birth.Text.Trim().Length >= 6)
                {
                    string[] operater = { "/" };
                    string[] info = _birth.Text.Trim().Split(operater, StringSplitOptions.None);
                    if (info[0].Length < 4)
                    {
                        DateTime _birth_dt = Lib.SysSetting.ToWorldDate(_birth.Text.Trim());
                        int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, System.DateTime.Today);
                        if (age_temp >= 18)
                        {
                            isCheckBirth = true;
                            _age.Text = age_temp.ToString();
                        }
                        else
                        {
                            isCheckBirth = false;
                            MessageBox.Show("年齡不得小於18歲,請重新輸入");
                            _birth.Focus();
                        }
                    }
                    else
                    {
                        isCheckBirth = false;
                        MessageBox.Show("請輸入民國格式之生日");
                        _birth.Focus();
                    }
                }
                else
                {
                    isCheckBirth = false;
                    MessageBox.Show("請輸入民國格式之生日");
                    _birth.Focus();
                }
            }
            catch (Exception ex)
            {
                isCheckBirth = false;
                MessageBox.Show("生日格式錯誤請重新輸入");
                _birth.Focus();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    //showSystemTime();
            //    //2018-1-29出現自製連線視窗
            //    if (ST_Form == null)
            //    {
            //        ST_Form = new SystemTimeForm();
            //        ST_Form.ShowDialog();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("取得系統時間失敗：" + Environment.NewLine + ex.Message);
            //}
            try
            {
                Lib.DataUtility du_center = new DataUtility();
                DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code");
                if (dt_center.Rows.Count == 1)
                {
                    center_code = dt_center.Rows[0]["center_code"].ToString();
                    center_name = dt_center.Rows[0]["center_name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            //暫時先Mark

            //FileInfo Old_FileInfo = new FileInfo("C://成績列印//CertPrint.exe");
            //FileInfo New_FileInfo = new FileInfo(Application.StartupPath + "//UpdateItem//CertPrint.exe");

            //if (Old_FileInfo.LastWriteTime != New_FileInfo.LastWriteTime)
            //{ 
            //    //作檔案複製的動作
            //    try
            //    {
            //        New_FileInfo.CopyTo("C://成績列印//CertPrint.exe", true);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("請執行以下步驟\r\r1.請關閉成績列印\r\r2.重新啟動檢錄系統", "版本更新錯誤", MessageBoxButtons.OK);
            //        Application.Exit();
            //    }
            //}

            ActiveControl = step1_id;
            if (System.Configuration.ConfigurationManager.AppSettings["IsAuto"].ToString() == "1")
            {
                GetAllDeviceClasses();
                if (!checkbarcode)
                {
                    MessageBox.Show("條碼機未正確安裝 , 只允許人工輸入身分證字號");
                }
            }
            
            System.Timers.Timer _timer = new System.Timers.Timer();
            _timer.Interval = 60000;
            _timer.Enabled = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(CheckBarCodeReader);

            System.Timers.Timer timer_Race = new System.Timers.Timer();
            timer_Race.Interval = 5000;
            timer_Race.Enabled = true;
            timer_Race.Elapsed += new System.Timers.ElapsedEventHandler(timer_Race_Elapsed);
        }
        //2018-1-24顯示系統時間
        private void showSystemTime()
        {
            string today = DateTime.Now.ToString("       西元yyyy年MM月dd日(dddd) HH點mm分");
            MessageBox.Show("----------------檢錄電腦系統時間----------------" + Environment.NewLine + today + Environment.NewLine + "---------------------------------------------------" + Environment.NewLine + "請確實檢查檢錄電腦系統時間是否正確!!"+ Environment.NewLine+"時間錯誤將影響「年齡」及「成績換算標準」。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void timer_Race_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Lib.SysSetting.SystemMode SystemMode = Lib.SysSetting.CurrentSystemMode();
            if (SystemMode == SysSetting.SystemMode.Race)
            {
                Application.Exit();
            }
        }

        private void CheckBarCodeReader(object o, System.Timers.ElapsedEventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsAuto"].ToString() == "1")
            {
                GetAllDeviceClasses();
                if (!checkbarcode)
                {
                    MessageBox.Show("條碼機未正確安裝 , 只允許人工輸入身分證字號");
                }
            }
        }

        private void step1_id_TextChanged(object sender, EventArgs e)
        {
            if (step1_id.TextLength == 10)
            {
                idinput_Click(idinput, e);
            }
        }

        public void GetAllDeviceClasses()
        {
            Lib.DeviceID _DeviceID = new Lib.DeviceID();
            _DeviceID.GetDeviceID();
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + "\\System.txt");
                while (reader.Peek() >= 0)
                {
                    string readline = reader.ReadLine();
                    foreach (KeyValuePair<string, string> instantid in _DeviceID.DeviceIdList)
                    {
                        if (instantid.Value.ToString() == readline)
                        {
                            checkbarcode = true;
                            break;
                        }
                        else
                        {
                            checkbarcode = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                checkbarcode = false;
            }
        }

        static String GetClassNameFromGuid(Guid guid)
        {
            StringBuilder strClassName = new StringBuilder(0);
            Int32 iRequiredSize = 0;
            Int32 iSize = 0;
            Int32 iRet = Win32DeviceMgmt.SetupDiClassNameFromGuid(ref guid, strClassName, iSize, ref iRequiredSize);
            strClassName = new StringBuilder(iRequiredSize);
            iSize = iRequiredSize;
            iRet = Win32DeviceMgmt.SetupDiClassNameFromGuid(ref guid, strClassName, iSize, ref iRequiredSize);
            if (iRet == 1)
            {
                return strClassName.ToString();
            }

            return String.Empty;
        }

        static String GetClassDescriptionFromGuid(Guid guid)
        {
            StringBuilder strClassDesc = new StringBuilder(0);
            Int32 iRequiredSize = 0;
            Int32 iSize = 0;
            Int32 iRet = Win32DeviceMgmt.SetupDiGetClassDescription(ref guid, strClassDesc, iSize, ref iRequiredSize);
            strClassDesc = new StringBuilder(iRequiredSize);
            iSize = iRequiredSize;
            iRet = Win32DeviceMgmt.SetupDiGetClassDescription(ref guid, strClassDesc, iSize, ref iRequiredSize);
            if (iRet == 1)
            {
                return strClassDesc.ToString();
            }

            return String.Empty;
        }

        static String GetDeviceInstanceId(IntPtr DeviceInfoSet, Win32DeviceMgmt.SP_DEVINFO_DATA DeviceInfoData)
        {
            StringBuilder strId = new StringBuilder(0);
            Int32 iRequiredSize = 0;
            Int32 iSize = 0;
            Int32 iRet = Win32DeviceMgmt.SetupDiGetDeviceInstanceId(DeviceInfoSet, ref DeviceInfoData, strId, iSize, ref iRequiredSize);
            strId = new StringBuilder(iRequiredSize);
            iSize = iRequiredSize;
            iRet = Win32DeviceMgmt.SetupDiGetDeviceInstanceId(DeviceInfoSet, ref DeviceInfoData, strId, iSize, ref iRequiredSize);
            if (iRet == 1)
            {
                return strId.ToString();
            }

            return String.Empty;
        }

        private void 重置程式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetallflow();
            updateHintMessage("請確認身高機與體脂機已開機且連線正常");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void 成績結算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreCloseForm2 _ScoreCloseForm2 = new ScoreCloseForm2();
            comm.ClosePort();
            richTextBox1.Text = "";//下了ClosePort會有回傳值賽入內文，所以要先清空
            //_ScoreCloseForm2.Show();
            if (_ScoreCloseForm2.ShowDialog() == DialogResult.Cancel)
            {
                comm.OpenPort();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            capture_Click(sender, e);
        }

        private void btn_Change_3K_Click(object sender, EventArgs e)
        {
            _3K_Change Ch3K = new _3K_Change(this);
            if (!string.IsNullOrEmpty(_age.Text.Trim()) && Convert.ToInt16(_age.Text.Trim()) >= 45)
            {
                int age = Convert.ToInt16(_age.Text.Trim());
                Ch3K.Age = age;
            }
            Ch3K.ShowDialog();
        }

        private void 即時數據檢視ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtChart_Fm == null)
            {
                dtChart_Fm = new DataChart();
                dtChart_Fm.Show();
            }
        }

        private void 數據查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hsChart_Fm == null)
            {
                hsChart_Fm = new HistoryChart();
                hsChart_Fm.Show();
            }
        }


    }
}
