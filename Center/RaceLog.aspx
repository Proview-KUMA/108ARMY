<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaceLog.aspx.cs" Inherits="RaceLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>競賽紀錄</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource_unit" DataTextField="unit_title" 
            DataValueField="unit_code" >
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_unit" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" 
            SelectCommand="select distinct result.unit_code, dbo.F_GetUnitTitle(result.unit_code) as unit_title from result where result.id in (select distinct id from racelog)">
        </asp:SqlDataSource>
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="SqlDataSource_ID" DataTextField="name" DataValueField="id" 
            AutoPostBack="True" ondatabound="DropDownList1_DataBound">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_ID" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="select distinct race.id,race.id + '(' + r.[name] + ')' as 'name' from racelog race, result r where race.id in
(select distinct id from result where unit_code = @unit_code) and race.id = r.id" >
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="unit_code" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="" GridLines="Horizontal" 
            >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField DataField="TYPE" HeaderText="TYPE" SortExpression="TYPE" />
                <asp:BoundField DataField="DETAIL" HeaderText="DETAIL" 
                    SortExpression="DETAIL" />
                <asp:BoundField DataField="time" HeaderText="time" ReadOnly="True" 
                    SortExpression="time" />
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceRACELOG" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" 
            SelectCommand="Race_ShowRaceLog" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="id" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
