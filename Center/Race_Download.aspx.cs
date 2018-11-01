using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Race_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string path = @Server.MapPath("~/") + "DownLoad/";
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                DirectoryInfo[] info_child = info.GetDirectories();
                foreach (DirectoryInfo d_child in info_child)
                {
                    TreeNode node = new TreeNode(d_child.Name);
                    node.Value = d_child.FullName;
                    TreeView1.Nodes.Add(node);

                    foreach (DirectoryInfo dd_info in d_child.GetDirectories())
                    {
                        TreeNode node2 = new TreeNode(dd_info.Name);
                        node2.Value = dd_info.FullName;
                        node.ChildNodes.Add(node2);

                        foreach (FileInfo f in dd_info.GetFiles())
                        {
                            TreeNode node3 = new TreeNode(f.Name);
                            node3.Value = f.FullName;
                            node2.ChildNodes.Add(node3);

                        }

                    }

                    foreach (FileInfo file in d_child.GetFiles())
                    {
                        TreeNode node_child = new TreeNode(file.Name);
                        node_child.Value = file.FullName;
                        node.ChildNodes.Add(node_child);
                    }
                }

                FileInfo[] files = info.GetFiles();
                foreach (FileInfo file in files)
                {
                    TreeNode node = new TreeNode(file.Name);
                    node.Value = file.FullName;
                    TreeView1.Nodes.Add(node);

                }

            }
        }
        else
        {

        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/octet-stream";
            Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5");
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + TreeView1.SelectedNode.Text);
            Response.WriteFile(TreeView1.SelectedValue);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
