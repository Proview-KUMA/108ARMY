<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaceStardandCheck.aspx.cs" Inherits="RaceStardandCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <span>查詢團體組競賽項目標準</span><br />
    <tabe>
    <tr>
    <td><span>競賽項目</span></td><td><asp:ListBox ID="ListBoxItem" runat="server">
            <asp:ListItem Value="sit_ups" Selected="True">仰臥起坐</asp:ListItem>
            <asp:ListItem Value="push_ups">扶地起身</asp:ListItem>
            <asp:ListItem Value="run">三千公尺跑步</asp:ListItem>
        </asp:ListBox></td>
    </tr>
    <tr>
    <td><span>役別</span></td>
    <td><asp:ListBox ID="ListBoxType" runat="server">
            <asp:ListItem Value="義務役" Selected="True">義務役</asp:ListItem>
            <asp:ListItem Value="志願役">志願役</asp:ListItem>
        </asp:ListBox></td>
    </tr>
    <tr>
    <td>區分</td>
    <td><asp:ListBox ID="ListBoxTime" runat="server">
            <asp:ListItem Value="1" Selected="True">入伍結訓</asp:ListItem>
            <asp:ListItem Value="3">入伍滿三月</asp:ListItem>
            <asp:ListItem Value="4">入伍滿四月</asp:ListItem>
            <asp:ListItem Value="5">入伍滿五月</asp:ListItem>
            <asp:ListItem Value="6">入伍滿六月</asp:ListItem>
        </asp:ListBox></td>
    </tr>
    <tr>
    <td>性別</td><td><asp:ListBox ID="ListBoxGender" runat="server">
            <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
            <asp:ListItem Value="女">女</asp:ListItem>
        </asp:ListBox></td>
    </tr>
    <tr>
    <td>年齡<td></td><asp:TextBox ID="txtAge" runat="server"></asp:TextBox></td>
    </tr>
    </tabe>
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="查詢" onclick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
