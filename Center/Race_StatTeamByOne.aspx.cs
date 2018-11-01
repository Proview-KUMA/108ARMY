using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Race_StatTeamByOne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["unit_code"] != null)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = new Lib.DataUtility().getDataTableBysp("Race_StatTeamByOne", "unit_code_in", Request.QueryString["unit_code"].ToString());
                    if (dt.Rows.Count == 1)
                    {

                        lb_unit.Text = dt.Rows[0]["單位"].ToString();
                        title.Text = lb_unit.Text + "101年三項基本體能競賽成績冊";
                        lb_total.Text = dt.Rows[0]["餉冊人數"].ToString();
                        lb_213_223.Text = dt.Rows[0]["BMI大於30"].ToString();
                        lb_233.Text = dt.Rows[0]["免技測"].ToString();
                        lb_224.Text = dt.Rows[0]["屆退"].ToString();
                        lb_204.Text = dt.Rows[0]["懷孕"].ToString();
                        lb_214.Text = dt.Rows[0]["公勤"].ToString();
                        lb_freeNo.Text = (Convert.ToInt16(lb_213_223.Text) + Convert.ToInt16(lb_233.Text)).ToString();
                        lb_free.Text = (Convert.ToInt16(lb_204.Text) + Convert.ToInt16(lb_214.Text) + Convert.ToInt16(lb_224.Text)).ToString();
                        lb_repl.Text = dt.Rows[0]["替代項目"].ToString();
                        lb_should.Text = dt.Rows[0]["應測"].ToString();
                        lb_real.Text = dt.Rows[0]["實測"].ToString();
                        lb_attend_rate.Text = dt.Rows[0]["到測率"].ToString();
                        lb_202.Text = dt.Rows[0]["全項合格"].ToString();
                        lb_203.Text = dt.Rows[0]["全項不合格"].ToString();
                        if (Convert.ToDecimal(dt.Rows[0]["鑑測合格率"].ToString()) < 0)
                        {
                            lb_rate.Text = "0";
                        }
                        else
                        {
                            lb_rate.Text = dt.Rows[0]["鑑測合格率"].ToString();
                        }
                        note.Text = "(" + dt.Rows[0]["note"].ToString() + ")";

                        lb_sit_ups_count.Text = dt.Rows[0]["仰臥起坐受測數"].ToString();
                        lb_sit_ups_202.Text = dt.Rows[0]["仰臥起坐合格數"].ToString();
                        lb_sit_ups_rate.Text = dt.Rows[0]["仰臥起坐合格率"].ToString();

                        lb_push_ups_count.Text = dt.Rows[0]["伏地挺身受測數"].ToString();
                        lb_push_ups_202.Text = dt.Rows[0]["伏地挺身合格數"].ToString();
                        lb_push_ups_rate.Text = dt.Rows[0]["伏地挺身合格率"].ToString();

                        lb_run_count.Text = dt.Rows[0]["3000公尺受測數"].ToString();
                        lb_run_202.Text = dt.Rows[0]["3000公尺合格數"].ToString();
                        lb_run_rate.Text = dt.Rows[0]["3000公尺合格率"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
                }
            }
        }
    }
}
