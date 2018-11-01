using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Race_DetailStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = new DataTable();
            if (Request.QueryString["unit_code"] != null && Request.QueryString["status"] != null)
            {
                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("unit_code", Request.QueryString["unit_code"].ToString());
                list.Add("status", Request.QueryString["status"].ToString());
                dt = du.getDataTableBysp("Race_DetailStatusByUnit", list);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                pageTitle.Text = "團體組各單位競賽狀態名單";
                DataTable dt2 = new DataTable();
                dt2 = du.getDataTableByText("select unit_title from unit where unit_code = '" + list["unit_code"] + "'");
                Label1.Text = dt2.Rows[0][0].ToString() + "<br />"; 
                Label1.Text += "共 " + dt.Rows.Count.ToString() + " 員";
            }

            if (Request.QueryString["team"] != null && Request.QueryString["status"] != null)
            {
                Dictionary<string, object> list = new Dictionary<string, object>();
                list.Add("team", Request.QueryString["team"].ToString());
                list.Add("status", Request.QueryString["status"].ToString());
                dt = du.getDataTableBysp("Race_DetailStatusByPerson", list);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                pageTitle.Text = "個人組分組競賽狀態名單";
                Label1.Text = "共 " + dt.Rows.Count.ToString() + " 員";
            }

        }
    }
}
