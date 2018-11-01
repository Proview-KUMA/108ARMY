using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Race_DetailStatusRare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Lib.SysSetting.CurrentSystemMode() == Lib.SysSetting.SystemMode.Race)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = new DataTable();
            if (Request.QueryString["unit_code"] != null && Request.QueryString["status"] != null)
            {
                string status = Request.QueryString["status"].ToString();
                string unit_code = Request.QueryString["unit_code"].ToString();
                switch (status)
                { 
                    case "bmi":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], 'BMI>=30且體指率超標' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) in ('13','23')");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "race":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], '競賽中' [狀態] from result where unit_code ='" + unit_code + "' and status = '001'");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "freerace":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], '免技測' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) in ('33')");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "retire":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], '屆退' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) in ('24')");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "baby":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], '懷孕' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) in ('04')");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "training":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名], '公勤' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) in ('14')");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "repl":
                        dt = du.getDataTableByText("select id [身分證號], name [姓名],(select rep_title from repment where sid = substring(memo,1,1)) [仰臥起坐],(select rep_title from repment where sid = substring(memo,2,1)) [俯地起身],(select rep_title from repment where sid = substring(memo,3,1)) [徒手跑步] from result where unit_code ='" + unit_code + "' and memo != '000'");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;
                    case "absent":
                        dt = du.getDataTableByText("select code [code],id [身分證號], name [姓名], '無故未測' [狀態] from result where unit_code ='" + unit_code + "' and substring(status,2,2) = '03' and [code] is null");
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        break;

                    default:
                        break;
                
                }
            }
        }
    }
}
