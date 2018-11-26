<%@ Page Language="C#" AutoEventWireup="true" CodeFile="107_Result_To_CSV.aspx.cs" Inherits="_107_Result_To_CSV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>成績資料表轉CSV檔</title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Label ID="Label5" runat="server" Text="成績名冊匯出" Font-Size="XX-Large" Font-Bold="True" Font-Overline="False" ForeColor="#009900" Font-Underline="True"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="請選擇年份：" Font-Size="Larger"></asp:Label>
        <asp:DropDownList ID="ddl_year" runat="server" Font-Size="Medium"></asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="請選擇單季：" Font-Size="Larger"></asp:Label>
        <asp:DropDownList ID="ddl_season" runat="server" Font-Size="Medium">
            <asp:ListItem Value="1">第一季</asp:ListItem>
            <asp:ListItem Value="2">第二季</asp:ListItem>
            <asp:ListItem Value="3">第三季</asp:ListItem>
            <asp:ListItem Value="4">第四季</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <asp:Button ID="btn_InqResult" runat="server" Text="下載完整CSV檔" Font-Size="Larger" OnClick="btn_InqResult_Click" BackColor="#33CC33" Font-Bold="True" />
        <asp:Label ID="Label6" runat="server" Text="(供資料庫匯入使用)" ForeColor="Green"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="btn_InqResult_Simple" runat="server" Text="下載簡化CSV檔" Font-Size="Larger" OnClick="btn_InqResult_Simple_Click" BackColor="Aqua" Font-Bold="True" />
        <asp:Label ID="Label7" runat="server" Text="(供EXCEL分析使用)" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
