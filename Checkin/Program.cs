using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace InI
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Lib.DataUtility du = new Lib.DataUtility();
            Application.Run(new Form1());
            try
            {
                //Lib.SysSetting.SystemMode myMode = Lib.SysSetting.CurrentSystemMode();
                //if (myMode == Lib.SysSetting.SystemMode.Normal)
                //{
                //    Application.Run(new Form1());
                //    //Application.Run(new TestForm());
                //}
                //if (myMode == Lib.SysSetting.SystemMode.Race)
                //{
                //    Application.Run(new Form3());
                //}
                //if (myMode == Lib.SysSetting.SystemMode.Original)
                //{
                //    Application.Run(new Form1());
                //}
            }
            catch (Exception ex)
            {

            }

            //Lib.DataUtility du = new Lib.DataUtility();
            //Dictionary<string, object> d = new Dictionary<string, object>();
            //d.Add("item", "mode");
            //DataTable dt = du.getDataTableByText("select value from SysValue where item = @item", d);
            //if (dt.Rows.Count == 1)
            //{
            //    if (dt.Rows[0][0].ToString() == "race")
            //    {
            //        Application.Run(new Form3());
            //    }
            //    else
            //    {
            //        Application.Run(new Form1());
            //    }
            //}
        }
    }
}
