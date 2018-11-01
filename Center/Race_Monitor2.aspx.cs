using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;

public partial class Race_Monitor2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (var webclient = new System.Net.WebClient())
            {
                var json = webclient.DownloadData("http://localhost/Center/Race_Status.ashx");
                string msg = System.Text.Encoding.UTF8.GetString(json);

                GridView1.DataSource = TransferDataTableFromJson(msg);
                GridView1.DataBind();

            }
            
        }
    }

    private DataTable TransferDataTableFromJson(string json)
    {
        if (json.Substring(0, 1) == "[" && json.Substring(json.Length - 1, 1) == "]")
        {
            json = json.Remove(json.Length - 2, 2);
            json = json.Remove(0, 2);
            string[] j = json.Split(new string[] { "},{" }, StringSplitOptions.None);

            DataTable dt = new DataTable("jsonTable");

            foreach (string jj in j)
            {
                string[] jjj = jj.Split(new string[] { "," }, StringSplitOptions.None);

                string[] keyColumn = jjj[0].Split(new string[] { ":" }, StringSplitOptions.None);
                
                // create table column
                foreach (string s in jjj)
                {
                    string[] ss = s.Split(new string[] { ":" }, StringSplitOptions.None);
                    try
                    {
                        dt.Columns.Add(ss[0].Replace("\"",""));
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                }

                // insert datarow
                DataRow row = dt.NewRow();
                for (int i = 0; i < jjj.Length; i++)
                {
                    string[] ss = jjj[i].Split(new string[] { ":" }, StringSplitOptions.None);
                    try
                    {
                        row[i] = ss[1].Replace("\"", "");
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                }
                dt.Rows.Add(row);
            }

            return dt;
        }
        else
        {
            throw new Exception("formate error, check the data format first");
        }
    }
}
