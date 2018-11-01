using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SysAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            version.Text = Lib.SysSetting.Unit_Version;
            MainWS.WebService MainWs = new MainWS.WebService();
            HQ_version.Text = MainWs.Unit_Version();
        }
        DataTable dt = new DataTable();
        Lib.DataUtility du = new Lib.DataUtility();
        dt = du.getDataTableByText("select count(*) as count from Result_Bak");
        if (dt.Rows[0]["count"].ToString() != "0")
        {
            Button1.Enabled = false;
        }
    }
    protected void ReNew_Click(object sender, EventArgs e)
    {
        try
        {
            MainWS.WebService mainWS = new MainWS.WebService();
            DataTable dt = mainWS.UnitData();
            Lib.DataUtility du = new Lib.DataUtility();
            du.executeNonQueryByText("delete from unit");
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("unit_code", row["unit_code"]);
                d.Add("unit_title", row["unit_title"]);
                d.Add("parent_unit_code", row["parent_unit_code"]);
                d.Add("service", row["service_code"]);
                list.Add(d);
                du.executeNonQueryByText("insert into unit (unit_code,unit_title,parent_unit_code,service_code) values (@unit_code,@unit_title,@parent_unit_code,@service)", list);
                list.Clear();
            }


            du.executeNonQueryBysp("add_roc_order");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            version.Text = mainWS.Unit_Version();
            Lib.SysSetting.Unit_Version = mainWS.Unit_Version();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message.Replace("\"", "").Replace("'", "") + "\");", true);
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
