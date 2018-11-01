<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeSignUp.aspx.cs" Inherits="DeSignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript" src="Script/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="Script/FreezeHead.js"></script>
    <script type="text/javascript" language="javascript">
        $(function() {
            $('#GridView3').FreezeHead({ speed: 2, limit: 10 });
        });   
    </script>
    <title>檢視報進名單</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="單位選單 : "></asp:Label>
    
        <asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="True"
            DataSourceID="SqlDataSource1" DataTextField="name" 
            DataValueField="condition" ondatabound="DropDownList1_DataBound" >
        </asp:DropDownList>
    
        <asp:Button ID="Button1" runat="server" Text="刪除名單" onclick="Button1_Click" OnClientClick="if(confirm('確定要刪除報進名單嗎？')){return true;}else{return false;}" />
    
    &nbsp;
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:Button ID="btnOutPut" runat="server" Text="匯出報進名單檔案(依單位及個人組)" 
            onclick="btnOutPut_Click" />
    
    </div>
    <div style="left:10px;position:fixed;top:30px">
         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"            
            Width="1000px" AllowPaging="false" BackColor="White" 
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
            CellSpacing="1" GridLines="None">
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="unit_code" HeaderText="單位" SortExpression="unit_code"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身份證" SortExpression="id"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age"  ItemStyle-Wrap="false"/>        
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" SortExpression="sit_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" SortExpression="sit_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups" HeaderText="俯地挺身" SortExpression="push_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups_score" HeaderText="俯地挺身成績" SortExpression="push_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run" HeaderText="三千公尺" SortExpression="run" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" SortExpression="run_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="status" HeaderText="鑑測結果" SortExpression="status"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="result" HeaderText="Result" SortExpression="result"  ItemStyle-Wrap="false"/>
            </Columns>
    </asp:GridView>
    </div>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="Race_QueryCheckinInfo" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    </form>
</body>
</html>
