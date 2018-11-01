<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td>
        <asp:TextBox ID="timeunit" runat="server"></asp:TextBox>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        </td><td>
            <asp:Button ID="Button3" runat="server" Text="SetTimeUnit" 
                onclick="Button3_Click" /><br />
            <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="~/SourceControl.aspx">Source Control</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="~/sourceupload.aspx?type=addnew">Add Source</asp:HyperLink><br />
            <asp:Label ID="Label2" runat="server" Text="Add Source beta : "></asp:Label><asp:FileUpload
                ID="FileUpload1" runat="server" /><br />
            <asp:Button ID="Button9" runat="server" Text="Add Source" 
                onclick="Button9_Click" />
        </td>
    </tr>
    <tr>
    <td>
    <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" 
            Width="456px"></asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="TextBox2" Height="200px" TextMode="MultiLine" runat="server" 
            Width="441px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="Button1" runat="server" Text="Select" onclick="Button1_Click" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="GetColumn" />
        <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
            Text="System Info" />
        <asp:Button ID="Button6" runat="server" onclick="Button6_Click" Text="who" />
        <asp:Button ID="Button8" runat="server" Text="Get SP" Width="100px" 
            onclick="Button8_Click" />
        <br />
        <span>start</span><asp:TextBox ID="date_s" runat="server" Width="60px"></asp:TextBox>
        <span>end</span><asp:TextBox ID="date_e" runat="server" Width="60px"></asp:TextBox>
        <asp:Button ID="Button7" runat="server" onclick="Button7_Click1" 
            Text="Center Monitor" />
        <input id="input10" type="button" runat="server" value="Race_Monitor" onclick="window.open('Race_Monitor.aspx');" />
        <asp:HyperLink ID="logout" runat="server" NavigateUrl="~/Logout.aspx" Text="登出" ForeColor="Blue"></asp:HyperLink> 
    </td>
    <td>
        <asp:Button ID="Button2" runat="server" Text="Update" onclick="Button2_Click" />
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="exMsg" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MainDB %>" 
            SelectCommand="CenterMonitor" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="date_s" Name="date_s" PropertyName="Text" 
                    Type="DateTime" />
                <asp:ControlParameter ControlID="date_e" Name="date_e" PropertyName="Text" 
                    Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    </td>
        <td>
        
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        
        </td>
    </tr>
    
    </table>
        
    </div>
    </form>
</body>
</html>
