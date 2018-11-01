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
using System.Configuration;

namespace InI
{
    public partial class Form3 : Form
    {
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

        public TabPage _TabPage1;
        public TabPage _TabPage2;
        public bool OptionForRaceOrResult = true;
        public bool comm_Isopen = false;

        public Form3()
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
                if (cMger.Cameras.Count > 0)
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
                DataTable dt = du.getDataTableBysp("Race_GetPlayer",d);
                if (dt.Rows.Count > 0)
                {
                    _id.Text = dt.Rows[0]["id"].ToString().Trim();                    
                    _birth.Text = Lib.SysSetting.ToRocDateFormat(Convert.ToDateTime(dt.Rows[0]["birth"].ToString().Trim()).ToShortDateString());
                    DateTime _birth_dt = Lib.SysSetting.ToWorldDate(_birth.Text.Trim());
                    int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, System.DateTime.Today);
                    //int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["RaceTime"].ToString()));
                    _age.Text = age_temp.ToString().Trim();
                    _name.Text = dt.Rows[0]["name"].ToString().Trim();
                    _unit.Text = dt.Rows[0]["unit_title"].ToString().Trim();
                    _rank.Text = dt.Rows[0]["rank_title"].ToString().Trim();
                    //_memo = dt.Rows[0]["memo"].ToString().Trim();
                    _class.Text = Lib.SysSetting.ConvertResult(dt.Rows[0]["result"].ToString(), _age.Text, dt.Rows[0]["gender"].ToString());
                    Gender = dt.Rows[0]["gender"].ToString();
                    selectreplaceitem.Enabled = true;
                    
                    if (dt.Rows[0]["gender"].ToString().Trim() == "M")
                    {
                        _gender.Text = "男性";
                        this.measureGenderMale = true;
                    }
                    else if (dt.Rows[0]["gender"].ToString().Trim() == "F")
                    {
                        _gender.Text = "女性";
                        this.measureGenderMale = false;
                    }
                    tabControl1.SelectedTab = tabPage2;
                    step1_inputid();
                    comm.OpenPort();
                    comm_Isopen = true;
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

        public void getreplactitem(CheckedListBox original, CheckedListBox replaceitem)
        {
            char[] temp_memo = "000".ToCharArray();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string,object> d = new Dictionary<string,object>();
            for (int i = 0; i < original.CheckedItems.Count; i++)
            {
                checkedListBox1.Items.Add(original.CheckedItems[i].ToString(), true);
            }
            for (int i = 0; i < replaceitem.CheckedItems.Count; i++)
            {
                checkedListBox2.Items.Add(replaceitem.CheckedItems[i].ToString(), true);                
            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i].ToString() == "二分鐘仰臥起坐")
                {
                    d.Clear();
                    d.Add("rep_title",checkedListBox2.Items[i].ToString());
                    DataTable dt_sit = du.getDataTableByText("select sid from RepMent where rep_title = @rep_title",d);
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
                if (OptionForRaceOrResult)  //競賽檢錄系統
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
                        richTextBox1.Text = "";
                        _backnum.Text = "";
                    }
                }
                else   //成績結算
                {
                    try
                    {
                        textBox2.Text = richTextBox1.Text;
                        Lib.DataUtility du = new Lib.DataUtility();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        DataTable dt = new DataTable();
                        d.Add("LF_Tag_ID", richTextBox1.Text);
                        d.Add("status", "001");
                        dt = du.getDataTableBysp("Race_GetInfoByClose", d);
                        if (dt.Rows.Count == 1)
                        {
                            close_blacknum.Text = dt.Rows[0]["clothesNum"].ToString();
                            close_name.Text = dt.Rows[0]["name"].ToString();
                            close_tagid.Text = dt.Rows[0]["code"].ToString();
                            byte[] data = (byte[])dt.Rows[0]["photo"];
                            MemoryStream ms = new MemoryStream(data);
                            Image returnImage = Image.FromStream(ms);
                            close_picture.Image = returnImage;
                            ms.Close();

                            string id = dt.Rows[0]["id"].ToString();
                            d.Clear();
                            d.Add("id", id);
                            du.executeNonQueryBysp("Race_CalResult", d);

                            d.Clear();
                            d.Add("id", id);
                            dt = du.getDataTableBysp("Race_QueryStatusByClose", d);
                            if (dt.Rows.Count == 1)
                            {
                                close_status.Text = dt.Rows[0][0].ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("查無此腕錶受測資料");
                        }
                        richTextBox1.Text = "";
                        _backnum.Text = "";
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = "";
                        _backnum.Text = "";
                        MessageBox.Show(ex.Source + " " + ex.Message);
                    }
                }
            }
        }

        #endregion

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
            this._bodyfat.Text = this.measure.BodyFat.ToString();
            double bmrInKcal = (this.measure.Bmr / 4.184);
            this._rate.Text = bmrInKcal.ToString("0.0") + " kcal";
            this._weight.Text = this.measure.Weight.ToString("0.0");

            #region 鑑測版的模式
            //if (this.measure.Gender == BodyFatController.Gender_M)
            //{
            //    MessageBox.Show("男性體脂率:" + this.measure.BodyFat.ToString() + ",請醫官判定是否未達受測標準.");
            //}
            //if (this.measure.Gender == BodyFatController.Gender_F)
            //{
            //    MessageBox.Show("女性體脂率:" + this.measure.BodyFat.ToString() + ",請醫官判定是否未達受測標準.");
            //}
            #endregion

            if ((this.measure.Gender == BodyFatController.Gender_M) && (this.measure.BodyFat > 25))
            {
                updatebodyfatmore("M");
                MessageBox.Show("男性體脂率:" + this.measure.BodyFat.ToString() + ", 體脂率超過25% -> 判定為不合格.");
                initMeasure();
                resetallflow();
                updateHintMessage("請確認身高機與體脂機已開機且連線正常");                        
            }
            else if ((this.measure.Gender == BodyFatController.Gender_F) && (this.measure.BodyFat > 30))
            {
                updatebodyfatmore("F");
                MessageBox.Show("女性體脂率:" + this.measure.BodyFat.ToString() + ", 體脂率超過30% -> 判定為不合格.");
                initMeasure();
                resetallflow();
                updateHintMessage("請確認身高機與體脂機已開機且連線正常");
            }
            else  //體脂率正常, 照正常流程
            {
                //MessageBox.Show("正常流程");
                tabControl1.SelectedTab = tabPage4;
                bodyfaton.Enabled = false;
                bodyfatcancel.Enabled = false;
                this.enableNotPassBtn();
            }
        }

        void updatebodyfatmore(string _gender)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", _id.Text);
                d.Add("status", "999");
                d.Add("age", Convert.ToInt32(_age.Text));
                du.executeNonQueryByText("update Result set age = @age where id = @id and status = @status ", d);
                d.Clear();
                d.Add("height", float.Parse(_height.Text));
                d.Add("weight", float.Parse(_weight.Text));
                d.Add("BMI", float.Parse(_bmi.Text));
                d.Add("bodyfat", float.Parse(_bodyfat.Text.Trim()));
                d.Add("photo", ms.ToArray());
                d.Add("id", _id.Text);
                d.Add("birth", Lib.SysSetting.ToWorldDate(_birth.Text));
                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                d.Add("name", _name.Text.Trim());
                if (_gender == "M")
                    d.Add("status", "113");
                else
                    d.Add("status", "123");
                du.executeNonQueryBysp("Race_BodyFatMoreByCheckin", d);
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
                        catch (Exception ex) {
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
            if (this.measure.Bmi > 30)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["IsUsedBodyfat"].ToString() == "1")
                {
                    MessageBox.Show("BMI超過30,請進行體脂肪檢驗.");
                    Thread.Sleep(100);
                    step3_bodyfaton();
                    this.Invoke(new Callback(enableInitFlowBtn));
                }
                else
                {
                    MessageBox.Show("BMI超過30,請至其他檢錄站重新檢錄");
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
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            tabControl1.SelectedTab = tabPage1;
            pictureBox2.Image = null;
            LF_Tag_ID = string.Empty;
            UHF_Tag_ID = string.Empty;
            code = string.Empty;
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
            _class.Text = "團體組";
            richTextBox1.Text = "";
        }

        private void enableNotPassBtn()
        {
            this.notest.Enabled = true;
            this.musttest.Enabled = true;
            finishcheckin.Enabled = true;
        }
        #endregion  

        private void selectreplaceitem_Click(object sender, EventArgs e)
        {
            ReplaceitemRace _replaceitme = new ReplaceitemRace(this);
            _replaceitme.Show();
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
                        if (saveresult())  //完成檢錄過程無誤
                        {
                            if (checkBox1.Checked)  //如果勾選要傳輸至遠端電腦
                            {
                                CenterWS.WebService CenterWS = new InI.CenterWS.WebService();
                                d.Clear();
                                d.Add("id", _id.Text);
                                d.Add("status", "001");  //只抓必須要測3000公尺的資料
                                DataTable dt = du.getDataTableBysp("Race_QueryResultFor3KRun", d);  //預存程序中有加入條件memo取第三字元為'0'  
                                dt.TableName = "upload";
                                try
                                {
                                    if ("Done" == CenterWS.AddResultFor3KRun(dt))
                                    {
                                        MessageBox.Show("完成檢錄 , 且傳輸檢錄資料至遠端電腦");
                                    }
                                    else
                                    {   //傳輸檢錄資料至遠端電腦失敗, 以op_id做為flag
                                        MessageBox.Show("傳輸檢錄資料至遠端電腦失敗 , 請稍候作三千公尺資料補傳");
                                        d.Clear();
                                        d.Add("id", _id.Text);
                                        d.Add("status", "001");
                                        d.Add("op_id", "Fail");
                                        du.executeNonQueryByText("update result set op_id = @op_id where id = @id and status = @status", d);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("傳輸檢錄資料至遠端電腦失敗 , 請稍候作三千公尺資料補傳");
                                    d.Clear();
                                    d.Add("id", _id.Text);
                                    d.Add("status", "001");
                                    d.Add("op_id", "Fail");
                                    du.executeNonQueryByText("update result set op_id = @op_id where id = @id and status = @status", d);
                                }
                            }
                            else
                            {
                                MessageBox.Show("完成檢錄");
                            }
                            initMeasure();
                            resetallflow();
                            updateHintMessage("請確認身高機與體脂機已開機且連線正常");
                        }
                        else  //完成檢錄過程有異常
                        {
                            initMeasure();
                            resetallflow();
                            updateHintMessage("請確認身高機與體脂機已開機且連線正常");
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
                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                d.Add("status", "104");
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

        bool saveresult()
        {
            bool retunrvalue = false;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("id", _id.Text);
                d.Add("status", "999");
                d.Add("age", Convert.ToInt32(_age.Text));
                du.executeNonQueryByText("update Result set age = @age where id = @id and status = @status", d);
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
                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                d.Add("name", _name.Text.Trim());
                du.executeNonQueryBysp("Race_SaveResult", d);
                retunrvalue = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("發生異常, 檢錄失敗, 訊息 => " + ex.ToString());
                retunrvalue = false;
            }
            finally
            {
                ms.Dispose();
                
            }
            return retunrvalue;
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
                d.Add("memo", _memo);
                d.Add("date", System.DateTime.Today);
                du.executeNonQueryBysp("SaveMustResult", d);
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
                        //int age_temp = Lib.SysSetting.ConvertAge(_birth_dt, Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["RaceTime"].ToString()));
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

        private void _backnum_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    int clothesNum = Convert.ToInt32(_backnum.Text.Trim());
            //    Lib.DataUtility du = new Lib.DataUtility();
            //    Dictionary<string, object> d = new Dictionary<string, object>();
            //    d.Add("clothesNum", clothesNum);
            //    d.Add("date", System.DateTime.Today);
            //    DataTable dt = du.getDataTableBysp("CheckClothesNum", d);
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("背號已經重複使用 , 請選擇其他背號");
            //        //_backnum.Focus();
            //    }
            //    else
            //    { 
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("請重新確認背號");
            //    //_backnum.Focus();
            //}
        }

        private void _backnum_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int clothesNum = Convert.ToInt32(_backnum.Text.Trim());
            //    Lib.DataUtility du = new Lib.DataUtility();
            //    Dictionary<string, object> d = new Dictionary<string, object>();
            //    d.Add("clothesNum", clothesNum);
            //    d.Add("date", System.DateTime.Today);
            //    DataTable dt = du.getDataTableBysp("CheckClothesNum", d);
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("背號已經重複使用 , 請選擇其他背號");
            //        //_backnum.Focus();
            //    }
            //    else
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("請重新確認背號");
            //    //_backnum.Focus();
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RemoteIP.Text = Lib.SysSetting.GetRemoteIP();

            _TabPage1 = tabControl2.TabPages[0];
            _TabPage2 = tabControl2.TabPages[1];
            OptionForRaceOrResult = true;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add(_TabPage1);

            try
            {
                Lib.DataUtility du_center = new DataUtility();
                Lib.SysSetting.SystemMode myMode = Lib.SysSetting.CurrentSystemMode();
                if (myMode == Lib.SysSetting.SystemMode.Normal)
                {
                    DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code");
                    if (dt_center.Rows.Count == 1)
                    {
                        center_code = dt_center.Rows[0]["center_code"].ToString();
                        center_name = dt_center.Rows[0]["center_name"].ToString();
                    }
                }
                if (myMode == Lib.SysSetting.SystemMode.Race)
                {
                    DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Resulted R, Center C where R.center_code = C.center_code");
                    if (dt_center.Rows.Count == 1)
                    {
                        center_code = dt_center.Rows[0]["center_code"].ToString();
                        center_name = dt_center.Rows[0]["center_name"].ToString();
                    }
                }
                if (myMode == Lib.SysSetting.SystemMode.Original)
                {
                    DataTable dt_center = du_center.getDataTableByText("select distinct C.center_code as center_code, C.center_name as center_name  from Result R, Center C where R.center_code = C.center_code");
                    if (dt_center.Rows.Count == 1)
                    {
                        center_code = dt_center.Rows[0]["center_code"].ToString();
                        center_name = dt_center.Rows[0]["center_name"].ToString();
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

            if (System.Configuration.ConfigurationManager.AppSettings["centercode"].ToString() == "9")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }

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
            System.Timers.Timer  _timer = new System.Timers.Timer();
            _timer.Interval = 60000;
            _timer.Enabled = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(CheckBarCodeReader);

            System.Timers.Timer timer_Race = new System.Timers.Timer();
            timer_Race.Interval = 5000;
            timer_Race.Enabled = true;
            timer_Race.Elapsed += new System.Timers.ElapsedEventHandler(timer_Race_Elapsed);
        }

        void timer_Race_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Lib.SysSetting.SystemMode SystemMode = Lib.SysSetting.CurrentSystemMode();
            if (SystemMode != SysSetting.SystemMode.Race)
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
                idinput_Click(idinput , e);  
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 _form2 = new Form2();
            _form2.Show();
        }

        private void 競賽檢錄系統ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initMeasure();
            resetallflow();
            updateHintMessage("請確認身高機與體脂機已開機且連線正常");      
            OptionForRaceOrResult = true;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add(_TabPage1);   
        }

        private void 成績結算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comm_Isopen == false)
            {
                comm.OpenPort();
            }
            OptionForRaceOrResult = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add(_TabPage2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //if (checkBox1.Checked)  //如果勾選要傳輸至遠端電腦
            //{
            //    CenterWS.WebService CenterWS = new InI.CenterWS.WebService();
            //    Dictionary<string, object> d = new Dictionary<string, object>();
            //    Lib.DataUtility du = new DataUtility();
            //    d.Clear();
            //    d.Add("id", _id.Text);
            //    d.Add("status", "001");  //只抓必須要測3000公尺的資料
            //    DataTable dt = du.getDataTableBysp("Race_QueryResultFor3KRun", d);  //預存程序中有加入條件memo取第三字元為'0'  
            //    dt.TableName = "upload";
            //    if ("Done" == CenterWS.AddResultFor3KRun(dt))
            //    {
            //        MessageBox.Show("完成檢錄 , 且傳輸檢錄資料至遠端電腦");
            //    }
            //    else
            //    {   //傳輸檢錄資料至遠端電腦失敗, 以op_id做為flag
            //        d.Clear();
            //        d.Add("id", _id.Text);
            //        d.Add("status", "001");
            //        d.Add("op_id", "Fail");
            //        du.executeNonQueryByText("update result set op_id = @op_id where id = @id and status = @status", d);
            //    }
            //}

            CenterWS.WebService CenterWS = new InI.CenterWS.WebService();
            Dictionary<string, object> d = new Dictionary<string, object>();
            Lib.DataUtility du = new DataUtility();
            try
            {           
                if (checkBox1.Checked)  //如果勾選要傳輸至遠端電腦
                {                    
                    d.Clear();
                    d.Add("status", "001");
                    DataTable dt = du.getDataTableByText("select * from result where status = @status and substring(memo, 3, 1) = '0'", d);
                    dt.TableName = "upload";
                    string msg = CenterWS.AddResultFor3KRun(dt);
                    if ("Done" == msg)
                    {
                        MessageBox.Show("完成檢錄 , 且傳輸檢錄資料至遠端電腦");
                    }
                    else
                    {   //傳輸檢錄資料至遠端電腦失敗, 以op_id做為flag

                        d.Clear();
                        d.Add("status", "001");
                        d.Add("op_id", "Fail");
                        du.executeNonQueryByText("update result set op_id = @op_id where status = @status and substring(memo, 3, 1) = '0'", d);
                        MessageBox.Show("傳送檢錄資料至遠端主機失敗 , 請使用3000公尺資料補傳功能重新上傳");
                    }
                }
            }
            catch (Exception ex)
            {
                d.Clear();
                d.Add("status", "001");
                d.Add("op_id", "Fail");
                du.executeNonQueryByText("update result set op_id = @op_id where status = @status and substring(memo, 3, 1) = '0'", d);
            }
            
            //MessageBox.Show(Lib.SysSetting.GetRemoteIP());
            //Configuration _con = ConfigurationManager.OpenExeConfiguration(Application.StartupPath + "\\Checkin.exe");
            //ClientSettingsSection AS = (ClientSettingsSection)_con.SectionGroups["applicationSettings"].Sections["InI.Properties.Settings"];
            //MessageBox.Show(AS.Settings.Get("Checkin_CenterWS_WebService").Value.ValueXml.InnerText);
            //var tempsetting = AS.Settings.Get("Checkin_CenterWS_WebService");
            //tempsetting.Value.ValueXml.InnerText = "http://localhost/Center/WebService.asmx";
            //var oldsetting = AS.Settings.Get("Checkin_CenterWS_WebService");
            //AS.Settings.Remove(oldsetting);

            //AS.Settings.Add(tempsetting);

            //_con.Save(ConfigurationSaveMode.Full);


            //CenterWS.WebServiceSoapClient WS = new InI.CenterWS.WebServiceSoapClient();
            //Dictionary<string, object> d = new Dictionary<string,object>();
            //Lib.DataUtility du = new DataUtility();
            //DataTable dt = new DataTable();
            //d.Add("status", "001");
            //d.Add("date", System.DateTime.Today);
            //try
            //{
            //    dt = du.getDataTableByText("select * from Result where status = @status and date = @date", d);
            //    if (dt.Rows.Count > 0)
            //    {
            //        dt.TableName = "upload";
            //        string reply = WS.AddResultFor3KRun(dt);
            //        MessageBox.Show(reply);
            //    }
            //    else
            //    {
            //        MessageBox.Show("目前無檢錄資料");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Source + ex.Message);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string response = string.Empty;
            //Form4 input = new Form4();
            //DialogResult dr = input.ShowDialog();           
            //if (dr == DialogResult.OK)
            //{
            //    response = input.GetMsg();
            //}

            DataTable dt = new DataTable();
            if (textBox1.Text.Trim().Length == 10)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                Lib.DataUtility du = new DataUtility();
                d.Add("id", textBox1.Text.Trim());
                d.Add("status", "001");
                try
                {
                    dt = du.getDataTableBysp("Race_GetInfoByCloseID", d);
                    if (dt.Rows.Count == 1)
                    {
                        close_blacknum.Text = dt.Rows[0]["clothesNum"].ToString();
                        close_name.Text = dt.Rows[0]["name"].ToString();
                        close_tagid.Text = dt.Rows[0]["code"].ToString();
                        if (dt.Rows[0]["photo"] != DBNull.Value)
                        {
                            byte[] data = (byte[])dt.Rows[0]["photo"];
                            MemoryStream ms = new MemoryStream(data);
                            Image returnImage = Image.FromStream(ms);
                            close_picture.Image = returnImage;
                            ms.Close();
                        }

                        string id = dt.Rows[0]["id"].ToString();
                        d.Clear();
                        d.Add("id", id);
                        du.executeNonQueryBysp("Race_CalResult", d);

                        d.Clear();
                        d.Add("id", id);
                        dt = du.getDataTableBysp("Race_QueryStatusByClose", d);
                        if (dt.Rows.Count == 1)
                        {
                            close_status.Text = dt.Rows[0][0].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("查無此身分證成績");    
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("資料庫連線異常");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CenterWS.WebService CenterWS = new InI.CenterWS.WebService();
                CenterWS.Url = "http://" + RemoteIP.Text.Trim() + "/WebService.asmx";
                CenterWS.Discover();
                if (CenterWS.HelloWorld() == "Hello World")
                {
                    //可正常連線
                    MessageBox.Show("與遠端主機連線正常");
                    Configuration _con = ConfigurationManager.OpenExeConfiguration(Application.StartupPath + "\\Checkin.exe");
                    ClientSettingsSection AS = (ClientSettingsSection)_con.SectionGroups["applicationSettings"].Sections["InI.Properties.Settings"];
                    var tempsetting = AS.Settings.Get("Checkin_CenterWS_WebService");
                    tempsetting.Value.ValueXml.InnerText = "http://" + RemoteIP.Text.Trim() + "/WebService.asmx";
                    var oldsetting = AS.Settings.Get("Checkin_CenterWS_WebService");
                    AS.Settings.Remove(oldsetting);
                    AS.Settings.Add(tempsetting);
                    _con.Save(ConfigurationSaveMode.Full);
                }
                else
                {
                    //無法正常連線
                    MessageBox.Show("與遠端主機連線異常");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(img);
            Point ori = new Point(0, 0);
            g.CopyFromScreen(pictureBox1.PointToScreen(ori), ori, new Size(pictureBox1.Width, pictureBox1.Height));
            IntPtr dc = g.GetHdc();
            g.ReleaseHdc(dc);
            this.pictureBox2.Image = img;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("photo", ms.ToArray());
            Lib.DataUtility du = new DataUtility();
            du.executeNonQueryByText("update result Set photo = @photo where status != '999'", d);
            //try
            //{
            //    if (this.heightPort != null && !this.heightPort.IsOpen)
            //        this.heightPort.Open();

            //    step3();
            //    updateHintMessage("請站上身高機量測身理資訊");
            //    tabControl1.SelectedTab = tabPage3;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("身高機連線異常");
            //}
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["centercode"].ToString() == "9")
            {
                Form5 _form5 = new Form5();
                _form5.Show();
            }
            else
            {

            }            
        }

        private void 功能切換ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
     
    }
}
