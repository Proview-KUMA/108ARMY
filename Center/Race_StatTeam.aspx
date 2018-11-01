<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_StatTeam.aspx.cs" Inherits="Race_StatTeam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>單位成績表</title>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript" src="Script/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="Script/FreezeHead.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#GridView1').FreezeHead({ speed: 2, limit: 10 });
        });
    </script>
    <script type="text/javascript">
        function TableToExcel() {
            var curTbl = document.getElementById('GridView1');
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

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="unit_title" 
            DataValueField="unit_code" AutoPostBack="True">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" 
            SelectCommand="Race_GetTeamUnitCodeList" 
            SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <input type="button" value="EXCEL 輸出" onclick="TableToExcel();" />
    </div>
    <div style="position:fixed;top:35px">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Horizontal">
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
