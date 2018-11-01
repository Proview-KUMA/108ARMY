<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindUnitCode.aspx.cs" Inherits="FindUnitCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>單位代碼查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="單位代碼"></asp:Label><asp:TextBox ID="txtCode"
            runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="查詢代碼" onclick="Button1_Click" /><br />
            <asp:Label ID="Label2" runat="server" Text="單位全銜關鍵字"></asp:Label>
        <asp:TextBox ID="txtName"
            runat="server" Width="800px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server"
                Text="查詢關鍵字" onclick="Button2_Click" /><br />
        <asp:Label ID="Label3" runat="server" Text="(備註:連續空格關鍵字可以交集查詢，例如輸入:陸軍 營部連 軍團 二)"></asp:Label>
        <br />
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
            CellSpacing="2" ForeColor="Black">
            <RowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
