<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceView.aspx.cs" Inherits="SourceView" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
    <td colspan="2">
    <span>Current Path : </span>
        <asp:TextBox ID="txtPath" runat="server" Width="500px"></asp:TextBox><asp:Button
            ID="Button1" runat="server" Text="View Source Code" 
            onclick="Button1_Click" /><asp:Button ID="Button2" runat="server" 
            Text="Get File" onclick="Button2_Click" />
        
    </td>
    </tr>
    <tr>
    <td style="vertical-align:top"><div>
    <span>Auto View</span> <asp:CheckBox ID="CheckBox1" runat="server" />
        <asp:TreeView ID="TreeView1" runat="server" 
            onselectednodechanged="TreeView1_SelectedNodeChanged">
        </asp:TreeView>
    </div></td>
    <td style="vertical-align:top"><div>
    <textarea runat="server" id="codeBlock" rows="100" cols="120">
    
    </textarea>
    </div></td>
    </tr>
    </table>
    
    
    </form>
</body>
</html>
