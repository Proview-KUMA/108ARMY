using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



public partial class Race_Monitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null && (string)Session["admin"] != "true")
        {
            Response.Redirect("~/Index.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string ip = string.Empty;
        string center_code = row.Cells[0].Text;
        TextBox txtip = (TextBox)row.FindControl("TextBox1");
        if (string.IsNullOrEmpty(txtip.Text))
        {
            ip = "null";
        }
        else
        {
            ip = txtip.Text;
        }
        try
        {
            CenterWS.WebService ws = new CenterWS.WebService();
            ws.Url = "http://" + txtip.Text + "/WebService.asmx";
            ws.Discover();
            string remot_count = ws.RacePlayerIDCount(center_code);
            if (!string.IsNullOrEmpty(remot_count))
            {
                Label lab = (Label)row.FindControl("Label1");
                lab.Text = remot_count;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string ip = string.Empty;
        string center_code = row.Cells[0].Text;
        TextBox txtip = (TextBox)row.FindControl("TextBox1");
        if (string.IsNullOrEmpty(txtip.Text))
        {
            ip = "null";
        }
        else
        {
            try
            {
                new Lib.DataUtility().executeNonQueryByText("update center set ip_addr = @ip where center_code = '" + center_code + "'", "ip", txtip.Text);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('SAVE OK');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string ip = string.Empty;
        string center_code = row.Cells[0].Text;
        TextBox txtip = (TextBox)row.FindControl("TextBox1");
        if (string.IsNullOrEmpty(txtip.Text))
        {
            ip = "null";
        }
        else
        {
            ip = txtip.Text;
        }
        try
        {
            CenterWS.WebService ws = new CenterWS.WebService();
            ws.Url = "http://" + txtip.Text + "/WebService.asmx";
            ws.Discover();
            DataTable remote_dt = ws.RacePlayerIDList();
            DataTable local_dt = new Lib.DataUtility().getDataTableByText("select distinct id from result where center_code = '" + center_code + "' and substring([status],1,1) in ('1','2') ");
            //List<string> ID_List = new List<string>();
            for (int i_r = remote_dt.Rows.Count - 1; i_r >=0; i_r--)
            {
                for (int i = 0; i < local_dt.Rows.Count; i++)
                {
                    if (remote_dt.Rows[i_r]["id"].ToString() == local_dt.Rows[i]["id"].ToString())
                    {
                        remote_dt.Rows.RemoveAt(i_r);
                        local_dt.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
            if (remote_dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('All Match');", true);
            }
            else
            {
                remoteGV.DataSource = remote_dt;
                remoteGV.DataBind();
                localGV.DataSource = local_dt;
                localGV.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string ip = string.Empty;
        string center_code = row.Cells[0].Text;
        TextBox txtip = (TextBox)row.FindControl("TextBox1");
        TextBox txtID = (TextBox)row.FindControl("txtID");
        if (string.IsNullOrEmpty(txtip.Text))
        {
            ip = "null";
        }
        else
        {
            ip = txtip.Text;
        }
        try
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                CenterWS.WebService ws = new CenterWS.WebService();
                ws.Url = "http://" + txtip.Text + "/WebService.asmx";
                ws.Discover();
                DataTable dt = ws.RaceRecordByID(txtID.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('No Such ID Record');", true);
                }
                else
                {
                    Lib.DataUtility du = new Lib.DataUtility();
                    SqlConnection con = new SqlConnection(du.connectionString);
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Race_InsertResultBatch";
                        cmd.Parameters.AddWithValue("result", dt);
                        DataTable dt_msg = new DataTable();
                        dt_msg.Load(cmd.ExecuteReader());
                        con.Close();
                        if (dt_msg.Rows[0][0].ToString() == "Done")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Done');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Not Done');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
}
