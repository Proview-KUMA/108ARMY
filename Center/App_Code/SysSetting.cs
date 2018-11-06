using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Web.Script;
using System.Text;
using System.Web.Hosting;

namespace Lib
{
    /// <summary>
    /// SysSetting 的摘要描述
    /// </summary>
    public class SysSetting
    {


        public SysSetting()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //



        }

        public static bool isIPLock
        {
            get
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                return Convert.ToBoolean(c.AppSettings.Settings["isIPLock"].Value);
            }
            set
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                c.AppSettings.Settings["isIPLock"].Value = value.ToString();
                c.Save();
            }
        }

        public static string Unit_Version
        {
            get
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                return c.AppSettings.Settings["unit_version"].Value;
            }
            set
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                c.AppSettings.Settings["unit_version"].Value = value.ToString();
                c.Save();
            }
        }

        public static string CenterCode
        {
            get
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                return c.AppSettings.Settings["centerCode"].Value;
            }
        }

        public static string CenterName
        {
            get
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                return c.AppSettings.Settings["centerName"].Value;
            }
        }

        public static TimeSpan reserveTimeUnit
        {
            get
            {
                Configuration c = null;
                if (HostingEnvironment.ApplicationVirtualPath == "/")
                    c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

                WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
                c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
                TimeSpan ts = new TimeSpan(Convert.ToInt16(c.AppSettings.Settings["reserveTimeUnit"].Value), 0, 0);
                return ts;
            }
        }

        public static void setReserveTimeUnit(int hr)
        {
            Configuration c = null;
            if (HostingEnvironment.ApplicationVirtualPath == "/")
                c = WebConfigurationManager.OpenWebConfiguration("~/web.config");

            WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);
            c = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, HostingEnvironment.ApplicationVirtualPath);
            c.AppSettings.Settings["reserveTimeUnit"].Value = hr.ToString();
            c.Save();
        }

        private static WebConfigurationFileMap CreateFileMap(string applicationVirtualPath)
        {

            WebConfigurationFileMap fileMap =
                   new WebConfigurationFileMap();
            //WebConfigurationFileMap fileMap = CreateFileMap(HostingEnvironment.ApplicationVirtualPath);

            // Get he physical directory where this app runs. 
            // We'll use it to map the virtual directories 
            // defined next.  
            string physDir = HostingEnvironment.ApplicationPhysicalPath;

            // Create a VirtualDirectoryMapping object to use 
            // as the root directory for the virtual directory 
            // named config.  
            // Note: you must assure that you have a physical subdirectory 
            // named config in the curremt physical directory where this 
            // application runs.
            VirtualDirectoryMapping vDirMap =
                new VirtualDirectoryMapping(physDir, true);

            // Add vDirMap to the VirtualDirectories collection  
            // assigning to it the virtual directory name.
            fileMap.VirtualDirectories.Add(applicationVirtualPath, vDirMap);

            // Create a VirtualDirectoryMapping object to use 
            // as the default directory for all the virtual  
            // directories.
            VirtualDirectoryMapping vDirMapBase =
                new VirtualDirectoryMapping(physDir, true, "web.config");

            // Add it to the virtual directory mapping collection.
            fileMap.VirtualDirectories.Add("/", vDirMapBase);

            // Return the mapping. 
            return fileMap;
        }

        public static bool isOverTime(DateTime t)
        {
            // reserver date - reserverTimeUnit = deadline, then if now is ahead of deadline

            double _unit = SysSetting.reserveTimeUnit.TotalHours;
            DateTime deadline = t.AddHours(-_unit);

            if (deadline > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int ConvertAge(DateTime birth, DateTime reserved)
        {
            int age = reserved.Year - birth.Year;
            if (reserved < birth.AddYears(age))
                age--;
            return age;
        }

        public static Dictionary<string, DateTime> getAllowedDates(string unit_id)
        {

            DataUtility du = new DataUtility();
            Dictionary<string, DateTime> d = new Dictionary<string, DateTime>();
            DataTable allow = du.getDataTableBysp("getAllowedDates", "center_code", unit_id);

            foreach (DataRow row in allow.Rows)
            {
                d.Add(row["sid"].ToString(), Convert.ToDateTime(row["date"]));
            }
            return d;
        }

        public static Dictionary<string, DateTime> getDeniedDates(string unit_id)
        {
            DataUtility du = new DataUtility();
            Dictionary<string, DateTime> d = new Dictionary<string, DateTime>();
            DataTable deny = du.getDataTableBysp("getDeniedDates", "center_code", unit_id);

            foreach (DataRow row in deny.Rows)
            {
                d.Add(row["sid"].ToString(), Convert.ToDateTime(row["date"]));
            }
            return d;
        }

        [Flags]
        public enum Role
        {
            admin_hq = 1,
            mag_hq = 2,
            user_hg = 3,
            admin_center = 4,
            mag_center = 5,
            supervisor_center = 5,
            user_center = 7,
        }

        public static string GetJsonFormatData(DataTable table)
        {
            System.Web.Script.Serialization.JavaScriptSerializer s = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (DataRow r in table.Rows)
            {
                d = new Dictionary<string, object>();
                foreach (DataColumn c in table.Columns)
                {
                    d.Add(c.ColumnName, r[c]);
                }
                rows.Add(d);
            }

            return s.Serialize(rows);
        }

        public static string GetJsonFormatData(Dictionary<string, object> d)
        {
            System.Web.Script.Serialization.JavaScriptSerializer s = new System.Web.Script.Serialization.JavaScriptSerializer();
            return s.Serialize(d);
        }

        public static string GetJavaScriptHead(string s)
        {
            return "<script type='text/javascript'>" + s + "</script>";
        }

        public static DateTime ToWorldDate(string rocDateTime)
        {
            string[] operater = { "/" };
            string[] info = rocDateTime.Split(operater, StringSplitOptions.None);
            rocDateTime = (Convert.ToInt32(info[0]) + 1911).ToString() + "/" + info[1] + "/" + info[2];
            return Convert.ToDateTime(rocDateTime);
        }

        public static string GetRandomID(string RegionID)
        {
            string _regionID = string.Empty;
            Random r = new Random();
            int mycount = r.Next(10000000, 100000000);
            for (int i = mycount; i < 100000000; i++)
            {
                string[] regionID = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                int count = 0;


                #region Region Code
                _regionID = regionID[i % 26];
                switch (_regionID)
                {
                    case "A":
                        count = 1 * 1 + 0 * 9;
                        break;
                    case "B":
                        count = 1 * 1 + 1 * 9;
                        break;
                    case "C":
                        count = 1 * 1 + 2 * 9;
                        break;
                    case "D":
                        count = 1 * 1 + 3 * 9;
                        break;
                    case "E":
                        count = 1 * 1 + 4 * 9;
                        break;
                    case "F":
                        count = 1 * 1 + 5 * 9;
                        break;
                    case "G":
                        count = 1 * 1 + 6 * 9;
                        break;
                    case "H":
                        count = 1 * 1 + 7 * 9;
                        break;
                    case "I":
                        count = 3 * 1 + 4 * 9;
                        break;
                    case "J":
                        count = 1 * 1 + 8 * 9;
                        break;
                    case "K":
                        count = 1 * 1 + 9 * 9;
                        break;
                    case "L":
                        count = 2 * 1 + 0 * 9;
                        break;
                    case "M":
                        count = 2 * 1 + 1 * 9;
                        break;
                    case "N":
                        count = 2 * 1 + 2 * 9;
                        break;
                    case "O":
                        count = 3 * 1 + 5 * 9;
                        break;
                    case "P":
                        count = 2 * 1 + 3 * 9;
                        break;
                    case "Q":
                        count = 2 * 1 + 4 * 9;
                        break;
                    case "R":
                        count = 2 * 1 + 5 * 9;
                        break;
                    case "S":
                        count = 2 * 1 + 6 * 9;
                        break;
                    case "T":
                        count = 2 * 1 + 7 * 9;
                        break;
                    case "U":
                        count = 2 * 1 + 8 * 9;
                        break;
                    case "V":
                        count = 2 * 1 + 9 * 9;
                        break;
                    case "W":
                        count = 3 * 1 + 2 * 9;
                        break;
                    case "X":
                        count = 3 * 1 + 0 * 9;
                        break;
                    case "Y":
                        count = 3 * 1 + 1 * 9;
                        break;
                    case "Z":
                        count = 3 * 1 + 3 * 9;
                        break;
                    default:
                        break;
                }


                #endregion

                string stringNumber = Convert.ToString(i);
                count += 8 * 1;
                count += 7 * Convert.ToInt32(stringNumber.Substring(0, 1));
                count += 6 * Convert.ToInt32(stringNumber.Substring(1, 1));
                count += 5 * Convert.ToInt32(stringNumber.Substring(2, 1));
                count += 4 * Convert.ToInt32(stringNumber.Substring(3, 1));
                count += 3 * Convert.ToInt32(stringNumber.Substring(4, 1));
                count += 2 * Convert.ToInt32(stringNumber.Substring(5, 1));
                count += 1 * Convert.ToInt32(stringNumber.Substring(6, 1));

                int last = count % 10;
                if (10 - last == Convert.ToInt32(stringNumber.Substring(7, 1)))
                {
                    _regionID += "1" + Convert.ToInt32(i);
                    break;
                }
                
            }
            return _regionID;
        }

        public static string ToRunMinSecFormat(string run)
        {
            try
            {
                int _run = Convert.ToInt32(run);
                string min = (_run / 60).ToString();
                string sec = (_run % 60).ToString();
                return min + ":" + sec;
            }
            catch
            {
                return run;
            }
        }

        public static string ToRocDateFormat(string rawDate)
        {
            try
            {
                int year = Convert.ToInt32(rawDate.Substring(0, 4));
                return (year - 1911).ToString() + rawDate.Substring(4, rawDate.Length - 4);
            }
            catch
            {
                return rawDate;
            }
        }

        public static string ToRocWeekFormat(DayOfWeek day)
        {
            string result = string.Empty;
            switch (day)
            {
                case DayOfWeek.Monday:
                    result = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    result = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    result = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    result = "星期四";
                    break;
                case DayOfWeek.Friday:
                    result = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    result = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    result = "星期天";
                    break;
                default:
                    break;
            }
            return result;
        }

        public static void ExceptionLog(string type, string log, string page)
        {
            Lib.DataUtility du = new DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("type", type);
            d.Add("content", log);
            d.Add("date", DateTime.Now);
            d.Add("page", page);
            du.getDataTableByText("insert into exlog values (@date,@content,@type,@page)", d);
        }

        public static void AddLog(string _type, string _acc, string _event, DateTime _eventTime)
        {
            try
            {
                DataUtility du = new DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("type", _type);
                d.Add("acc", _acc);
                d.Add("event", _event);
                d.Add("eventTime", _eventTime);
                du.executeNonQueryBysp("AddLog", d);
            }
            catch (Exception ex)
            {

            }
        }
        //2018-11-5新增成績補正log
        public static void AddResultCorrectLog(Dictionary<string, object> dic)
        {
            try
            {
                DataUtility du = new DataUtility();
                Dictionary<string, object> d = dic;
                du.executeNonQueryBysp("Ex108_AddResultCorrectLog", d);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        

        public static SystemMode CurrentSystemMode()
        { 
           Lib.DataUtility du = new DataUtility();
           DataTable dt = new DataTable();
           try
           {
               // check CheckDB_MODE
               dt = du.getDataTableBysp("CheckDB_MODE");
               if (dt.Rows[0][0].ToString() == "upgraded")
               {
                   dt.Clear();
                   dt = du.getDataTableBysp("CheckSystem_MODE");
                   if (dt.Rows[0][0].ToString() == "race")
                   {
                       return SystemMode.Race;
                   }
                   else if (dt.Rows[0][0].ToString() == "normal")
                   {
                       return SystemMode.Normal;
                   }
                   else
                   {
                       throw new Exception("系統資料庫值錯亂(CheckSystem_MODE) : " + dt.Rows[0][0].ToString());
                   }
               }
               else
               {
                   return SystemMode.Original;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
        
        }


        public enum SystemMode
        {
            Original,
            Normal,
            Race
        }

        public static UploadMode CourrentUploadMode()
        {
            Lib.DataUtility du = new DataUtility();
            DataTable dt = new DataTable();
            try
            {
                dt = du.getDataTableBysp("CheckDB_MODE");
                if (dt.Rows[0][0].ToString() == "upgraded")
                {
                    dt.Clear();
                    dt = du.getDataTableBysp("CheckSystem_ExChange");
                    if (dt.Rows[0][0].ToString() == "remote")
                    {
                        return UploadMode.Remote;
                    }
                    else if (dt.Rows[0][0].ToString() == "local")
                    {
                        return UploadMode.Local;
                    }
                    else
                    {
                        throw new Exception("系統資料庫值錯亂(CheckSystem_MODE) : " + dt.Rows[0][0].ToString());
                    }
                }
                else
                {
                    return UploadMode.Local;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public enum UploadMode
        { 
            Remote,
            Local
        }

        public static string GetRemoteIP()
        {
            try
            {
                Lib.DataUtility du = new DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                DataTable dt = new DataTable();

                d.Add("item", "remote_ip");
                dt = du.getDataTableByText("select value from SysValue where item = @item", d);
                if (dt.Rows.Count == 1)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "localhost";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string StringToUnicode(string srcText)
        {
            string dst = "";
            char[] src = srcText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(src[i].ToString());
                string str = @"\u" + bytes[1].ToString("X2") + bytes[0].ToString("X2");
                dst += str;
            }
            return dst;
        }

    }
}
