using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class SourceView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string path = Server.MapPath("/");
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

    private void ShowCode(string code)
    {
        codeBlock.Value = code;
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        txtPath.Text = TreeView1.SelectedValue;
        if (CheckBox1.Checked)
        {
            StreamReader reader = File.OpenText(txtPath.Text.Trim());
            codeBlock.Value = reader.ReadToEnd();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StreamReader reader = File.OpenText(txtPath.Text.Trim());
        codeBlock.Value = reader.ReadToEnd();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        StreamReader reader = File.OpenText(TreeView1.SelectedValue);
        Response.ContentType = "text/txt";
        Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + TreeView1.SelectedNode.Text);
        Response.Write(reader.ReadToEnd());
        Response.End();
    }
}
