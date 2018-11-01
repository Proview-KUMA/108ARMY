using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO.Ports;

namespace CheckinLib.device
{
    class BodyFatController
    {
        #region constant
        public const int Unit_KG = 0;
        public const int Unit_LB = 1;

        public const int Gender_M = 1;
        public const int Gender_F = 2;

        public const int Body_Std = 0;
        public const int Body_Ath = 2;

        #endregion


        string portName;
        SerialPort port;

        public BodyFatController(String portName) {
            this.portName = portName;
            this.port = new SerialPort(this.portName, 4800, Parity.None, 8,StopBits.One);
        }

        public SerialPort connect() {
                try
                {
                    this.port.Open();
                    return this.port;
                }
                catch (Exception e) {
                    throw e;
                }
                
            
        }

        private void execute(String cmd) {
            byte[] data = ASCIIEncoding.Default.GetBytes(cmd + "\r" + "\n");
            Console.WriteLine("BodyFat Exe:" + cmd);
            this.port.Write(data,0,data.Length);
        }

        public void resetSetting() {
            int hex = 0x1F;
            char s = (char)hex;
            byte[] data = ASCIIEncoding.Default.GetBytes(s + "\r");
            Console.WriteLine("BodyFat Exe: 0X1F");
            this.port.Write(data, 0, data.Length);
           
        }

        public void cancleMeasure()
        {
            int hex = 0x1E;
            char s = (char)hex;
            byte[] data = ASCIIEncoding.Default.GetBytes(s + "\r");
            Console.WriteLine("BodyFat Exe: 0X1E");
            this.port.Write(data, 0, data.Length);

        }

        public void setUnit(int unit) {
            String cmd = "U" + unit.ToString();
            execute(cmd);
        }

        public void setClothingWt(double weight)
        {
            String strWt = weight.ToString().Trim('0');
            if (!strWt.Contains(".")) {
                strWt = strWt + ".0";
            }
            if (strWt.Length > 5) {
                strWt = strWt.Substring(strWt.Length - 5);
            }
            while (strWt.Length < 5) {
                strWt = "0" + strWt;
            }


            String cmd = "D0" + strWt;
            execute(cmd);
        }

        public void setGender(int gender) {
            String strGender = gender.ToString();
            String cmd = "D1" + strGender;
            execute(cmd);
        }

        public void setBodyType(int bodyType)
        {
            String strType = bodyType.ToString();
            String cmd = "D2" + strType;
            execute(cmd);
        }

        public void setHeight(double par)
        {

            String height = Convert.ToInt16(par).ToString();
            if (height.Length > 5) {
                height = height.Substring(height.Length-5);
            }
            while (height.Length < 5)
            {
                height = "0" + height;
            }

            String cmd = "D3" + height;
            execute(cmd);
        }

        public void setAge(int par)
        {
            String age = par.ToString();
            if (age.Length <2)
            {
                age = age.Substring(age.Length - 2);
            }
            while (age.Length < 2)
            {
                age = "0" + age;
            }

            String cmd = "D4" + age;
            execute(cmd);
        }

        public void checkInputArgument() {
            String cmd = "D?";
            execute(cmd);
        }

        public void start()
        {

            String cmd = "G1";
            execute(cmd);
        }
    }
}
