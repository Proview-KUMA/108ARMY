<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript" src="Script/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="Script/FreezeHead.js"></script>
    <script type="text/javascript" language="javascript">
        $(function() {

            $('#GridView1').FreezeHead({ speed: 5, limit: 20 });

            //            var rowcount = $('#GridView1 tbody tr').length;
            //            var index = 0;
            //            var speed = 3;
            //            $('body').bind('mousewheel', function(event, delta) {
            //                if (delta < 0) {
            //                    $('#GridView1').find('tbody tr').slice(index, index + speed).css('display', 'none');
            //                    if (index < 80) {
            //                        index = index + speed;
            //                    }
            //                }
            //                else {
            //                    $('#GridView1').find('tbody tr').slice(index - speed, index).css('display', '');
            //                    if (index >= 2) {
            //                        index = index - speed;
            //                    }
            //                }
            //                $('#log').text(index);
            //            });

        });
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position:fixed">
    <span>delta value</span><span id="log"></span>
    </div>
    <asp:Panel ID="panel1" runat="server">
    <div style="left:10px;position:fixed;top:20px">
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="sid" HeaderText="sid" InsertVisible="False" 
                    ReadOnly="True" SortExpression="sid" />
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
                <asp:BoundField DataField="acc" HeaderText="acc" SortExpression="acc" />
                <asp:BoundField DataField="event" HeaderText="event" SortExpression="event" />
                <asp:BoundField DataField="eventTime" HeaderText="eventTime" 
                    SortExpression="eventTime" />
                <asp:BoundField DataField="memo" HeaderText="memo" SortExpression="memo" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" 
            SelectCommand="SELECT top 100 * FROM [SysLog] order by sid"></asp:SqlDataSource>
    
    </div>
    </asp:Panel>
    
    </form>
</body>
</html>
