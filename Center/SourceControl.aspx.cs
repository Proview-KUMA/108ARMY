using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SourceControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null && (string)Session["admin"] != "true")
        {
            Response.Redirect("~/AdminLogin.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                string path = Server.MapPath("~/");
                txtpath.Text = path;
                Response.Write("current server source code located at :" + path + "<br /><hr />");
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles();
                if (files.Length != 0)
                {
                    Response.Write("<table>");
                    foreach (FileInfo file in files)
                    {
                        Response.Write("<tr>");
                        Response.Write("<td><a href='sourceupload.aspx?url=" + file.FullName + "' target='_blank'>" + file.FullName + "</a><br /></td>");
                        Response.Write("<td>" + file.LastWriteTime.ToString() + "</td>");
                        Response.Write("<td>Size: " + file.Length.ToString() + " </td>");
                        Response.Write("</tr>");
                    }
                    Response.Write("</table>");
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string path = string.Empty;
            path = txtpath.Text;
            Response.Write("current server source code located at :" + path + "<br /><hr />");
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            if (files.Length != 0)
            {
                Response.Write("<table>");
                foreach (FileInfo file in files)
                {
                    Response.Write("<tr>");
                    Response.Write("<td><a href='sourceupload.aspx?url=" + file.FullName + "' target='_blank'>" + file.FullName + "</a><br /></td>");
                    Response.Write("<td>" + file.LastWriteTime.ToString() + "</td>");
                    Response.Write("<td>Size: " + file.Length.ToString() + " </td>");
                    Response.Write("</tr>");
                }
                Response.Write("</table>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
