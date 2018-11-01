<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDetails.aspx.cs" Inherits="PrintDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
    $(function() {
        $('.go').click(function() {
        window.open('PrintDetails.aspx?date=' + $(':text').val(), '', '');
        });

        $('#ToExcel').click(function() {
        var curTbl = document.getElementById('detail');
            var oXL = new ActiveXObject("Excel.Application");
            //var ExcelSheet = new ActiveXObject("Excel.Sheet");
            var oWB = oXL.Workbooks.Add();
            var oSheet = oWB.ActiveSheet;
            var sel = document.body.createTextRange();
            sel.moveToElementText(curTbl);
            sel.select();
            sel.execCommand("Copy");
            oSheet.Paste();
            oXL.Visible = true;
        });
    });
</script>
</head>
<center>
<body>
    <form id="form1" runat="server">
    <input type="button" value="存成Excel" id="ToExcel" />
    <div id="detail">
    <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
    </div>
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" onrowdatabound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="sid" HeaderText="編號" 
                SortExpression="sid" />
            <asp:BoundField DataField="service_code" HeaderText="軍種" 
                SortExpression="service_code" />
            <asp:BoundField DataField="unit_title" HeaderText="單位" 
                SortExpression="unit_title" />
            <asp:BoundField DataField="rank_title" HeaderText="級職" 
                SortExpression="rank_title" />
            <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" />
            <asp:BoundField DataField="birth" HeaderText="生日" SortExpression="birth" DataFormatString="{0:yyyy/MM/dd}" />
            <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age" />
            <asp:BoundField DataField="result" HeaderText="報名方式" 
                SortExpression="result" /> 
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Toexcel" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="date" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </div>
    </form>
</body>
</center>
</html>
