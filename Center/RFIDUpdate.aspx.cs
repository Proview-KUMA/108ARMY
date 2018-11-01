using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
public partial class RFIDUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if(FileUpload1.HasFile)
        {
            HttpPostedFile file = FileUpload1.PostedFile;
            StreamReader reader = new StreamReader(file.InputStream);
            try
            {
                Lib.DataUtility du = new Lib.DataUtility();
                Dictionary<string, object> d = new Dictionary<string, object>();
                du.executeNonQueryByText("delete from Rfid");
                while(reader.Peek() > 0)
                {
                    string readline = reader.ReadLine();
                    string[] operater = { "," };
                    string[] info = readline.Split(operater, StringSplitOptions.None);
                    if (info[0] != "SN")
                    {
                        d.Add("code", info[0]);
                        d.Add("UHF_Tag_ID", info[1]);
                        d.Add("LF_Tag_ID", info[2]);
                        du.executeNonQueryByText("Insert into Rfid (code,LF_Tag_ID,UHF_Tag_ID) values (@code,@LF_Tag_ID,@UHF_Tag_ID)", d);
                        d.Clear();
                    }
                }
                GridView1.DataSourceID = SqlDataSource1.ID;
            }
            catch (FileLoadException ex)
            {
                Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('資料格式不符 , 請重新檢查');", true);
            }
        }

    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
