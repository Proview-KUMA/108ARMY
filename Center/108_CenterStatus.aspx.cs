using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using Lib;

public partial class _108_CenterStatus : System.Web.UI.Page
{
    //測試新位置git
    private static string SvIp = string.Empty;
    private static string GatewayIp = String.Empty;
    private static string TTL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SvIp = GetSvIp();
            GatewayIp = GetGatewayIp();
        }
        catch (Exception ex)
        {
            //記錄錯誤訊息
            //SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, this.ToString());
            SvIp = "查詢本機ip失敗";
            GatewayIp = "查詢本機ip失敗";
        }
        lab_SvIp.Text = SvIp;
        lab_GatewayIp.Text = GatewayIp;
        lab_DBTime.Text = Get_DBTime();
        lab_SvTime.Text = DateTime.Now.ToString();
    }

    //檢查閘道連線
    protected void btn_PingIp_Click(object sender, EventArgs e)
    {
        TTL = string.Empty;
        string GatewayIp = lab_GatewayIp.Text;
        lab_PingGatewayResult.ForeColor = Color.Red;
        lab_PingGatewayResult.Text = null;
        if (!string.IsNullOrEmpty(GatewayIp))
        {
            if (ByPing(GatewayIp))
            {
                lab_PingGatewayResult.ForeColor = Color.Blue;
                lab_PingGatewayResult.Text = "連線正常,TTL=" + TTL;
            }
            else
            {
                lab_PingGatewayResult.Text = "連線失敗";
            }

        }
        else
        {
            lab_PingGatewayResult.Text = "查無預設閘道IP設定值";
        }

    }

    //檢查總部伺服器連線
    public void btn_PingFec_Click(object sender, EventArgs e)
    {
        lab_PingFecResult.ForeColor = Color.Red;
        lab_PingFecResult.Text = null;
        try
        {
            ////鑑測站用
            //MainWS.WebService MainWebSv = new MainWS.WebService();
            //公司測試用
            office_MainWS.WebService MainWebSv = new office_MainWS.WebService();
            string s = MainWebSv.HelloWorld();
            lab_PingFecResult.ForeColor = Color.Blue;
            lab_PingFecResult.Text = "連線正常";
        }
        catch (Exception ex)
        {
            lab_PingFecResult.Text = "連線失敗";
        }
    }
    public bool ByPing(string ip)
    {
        IPAddress tIP = IPAddress.Parse(ip);
        Ping tPingControl = new Ping();
        PingReply tReply = tPingControl.Send(tIP);
        tPingControl.Dispose();
        if (tReply.Status != IPStatus.Success)
            return false;
        else
        {
            TTL = tReply.Options.Ttl.ToString();
            return true;
        }
    }
    public string GetSvIp()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_HOST"];//取得web伺服器ip

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public static string GetGatewayIp()
    {
        IPAddress result = null;
        var cards = NetworkInterface.GetAllNetworkInterfaces().ToList();
        if (cards.Any())
        {
            foreach (var card in cards)
            {
                var props = card.GetIPProperties();
                if (props == null)
                    continue;

                var gateways = props.GatewayAddresses;
                if (!gateways.Any())
                    continue;

                var gateway =
                    gateways.FirstOrDefault(g => g.Address.AddressFamily.ToString() == "InterNetwork");
                if (gateway == null)
                    continue;

                result = gateway.Address;
                break;
            };
        }

        return result.ToString();
    }


    //取得資料庫電腦時間
    public static string Get_DBTime()
    {
        string time = string.Empty;
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        dt = du.getDataTableByText(" select GETDATE()");
        if (dt.Rows.Count > 0)
        {
            time = dt.Rows[0][0].ToString();
        }
        else
        {
            time = "查詢資料庫時間失敗";
        }
        return time;
    }
}