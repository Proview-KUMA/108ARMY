<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_FindPerson.aspx.cs" Inherits="Race_FindPerson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>受測員查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td style="background-color:Blue;color:White"><div>
    
        <asp:Label ID="Label1" runat="server" Text="身分證號"></asp:Label><asp:TextBox ID="TextBox1"
            runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" 
            Text="查詢" onclick="Button1_Click" />
    </div></td>
    <td style="background-color:Red;color:White"><div>姓名 : 
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:Button ID="Button3" runat="server"
            Text="更改姓名" onclick="Button3_Click" /></div>
    
    </td>
    </tr>
    <tr>
    <td style="background-color:Blue;color:White"><asp:Label ID="Label2" runat="server" Text="姓名關鍵字"></asp:Label><asp:TextBox ID="TextBox2"
            runat="server"></asp:TextBox><asp:Button ID="Button2" runat="server" 
            Text="查詢" onclick="Button2_Click" /></td>
    <td style="background-color:Red;color:White"><div>身分字號 : 
        <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
        <asp:Button ID="Button4" runat="server"
            Text="更改身分證號" onclick="Button4_Click" /></div></td>
    </tr>
    <tr>
    <td style="background-color:Green;color:Lime">
        <asp:Label ID="Label3" runat="server" Text="身分證號"></asp:Label>
        <asp:TextBox ID="txtResetID" runat="server"></asp:TextBox>
        <asp:Button ID="Button5" runat="server" Text="重新檢錄" onclick="Button5_Click" />
        </td>
    <td style="background-color:Red;color:White"><div>索引鍵 (ID): 
        <asp:TextBox ID="txtIndexID" runat="server"></asp:TextBox></div></td>
    </tr>
    <tr>
    <td colspan="2" style="background-color:Aqua;color:Blue">
        <asp:Label ID="Label4" runat="server" Text="身分證號:"></asp:Label><asp:TextBox ID="txtID_BMI"
            runat="server"></asp:TextBox><asp:Label ID="Label5" runat="server" Text="晶片外碼"></asp:Label><asp:TextBox
                ID="txtCode" runat="server"></asp:TextBox><asp:Label ID="Label6" runat="server" Text="號碼衣"></asp:Label><asp:TextBox
                    ID="txtCloNO" runat="server"></asp:TextBox><br />
                    
        <br />
        <asp:Button ID="Button6" runat="server" Text="重新檢錄BMI超標不合格測員" 
            onclick="Button6_Click" />
    </td>
    </tr>
    <tr>
    <td colspan="2" style="background-color:Lime;color:White"><div>
    <span>身分證號 : </span>
        <asp:TextBox ID="txtID_repl" runat="server" Width="198px"></asp:TextBox>
    <br />
        <asp:Label ID="Label7" runat="server" Text="項目一"></asp:Label>
        <asp:DropDownList ID="DropDownList1"
            runat="server">
            <asp:ListItem Selected="True" Value="0">仰臥起坐</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上(限男性)</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂(限女性)</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Text="項目二"></asp:Label>
        <asp:DropDownList ID="DropDownList2"
            runat="server">
            <asp:ListItem Selected="True" Value="0">俯地起身</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上(限男性)</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂(限女性)</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label9" runat="server" Text="項目三"></asp:Label>
        <asp:DropDownList ID="DropDownList3"
            runat="server">
            <asp:ListItem Selected="True" Value="0">徒手跑步</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上(限男性)</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂(限女性)</asp:ListItem>
        </asp:DropDownList><br />
        <asp:Button ID="Button7" runat="server" Text="設定替代項目" onclick="Button7_Click" />
        </div></td>
    </tr>
    <tr>
         <td colspan="2" style="background-color:black;color:white">
            <span>身分證號 : </span>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <asp:Button ID="Button8" runat="server" Text="查詢受測者基本資料(含相片)" 
                 onclick="Button8_Click" />
         </td>
    </tr>
    </table>
    </div>
    
    
    <div>
        <asp:GridView ID="GridView1" runat="server" 
            onrowdatabound="GridView1_RowDataBound" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <RowStyle BackColor="White" ForeColor="#003399" />
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>
    </div>
    
    <div>
        <asp:Image runat="server" ID="image1" AlternateText="受測者相片" /> 
    </div>
    </form>
</body>
</html>
