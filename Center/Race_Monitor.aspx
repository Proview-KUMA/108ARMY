<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_Monitor.aspx.cs" Inherits="Race_Monitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SqlDataSource_local" GridLines="Horizontal" 
            Caption="Control Board" >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundField DataField="center_code" HeaderText="center_code" 
                    SortExpression="center_code" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="center_name" HeaderText="center_name" 
                    SortExpression="center_name" />
                <asp:BoundField DataField="local count" HeaderText="local count" 
                    ReadOnly="True" SortExpression="local count" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="remote_count">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="remote_ip">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("ip_addr") %>'></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Reflash" />
                        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Save IP" />
                        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Check List" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Get Remote ID">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text="ID : "></asp:Label>
                        <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                        <asp:Button ID="Button4" runat="server" Text="Get" onclick="Button4_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource_local" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="select Center.center_code,Center.center_name , (select COUNT(id) from result where result.center_code = Center.center_code and substring(result.[status],1,1) in ('1','2')) as 'local count', ip_addr from Center
order by center_code"></asp:SqlDataSource>
    </div>
    <div>
        <asp:GridView ID="remoteGV" runat="server" Caption="remain id in remote server">
        </asp:GridView>
        <asp:GridView ID="localGV" runat="server" Caption="remian id in local server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
