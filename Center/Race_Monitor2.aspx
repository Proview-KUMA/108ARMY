<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_Monitor2.aspx.cs" Inherits="Race_Monitor2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <%--<script type="text/javascript">
        $(function() {
            $.getJSON("http://localhost/Center/Race_Status.ashx", {}, function(data, status) {
                if (status = 'success') {
                    $.each(data, function(index, value) {
                        $('#body').append("<tr>");
                        $('#body').append("<td>" + value["單位代碼"] + "</td>");
                        $('#body').append("<td>" + value["單位"] + "</td>");
                        $('#body').append("<td>" + value["總數"] + "</td>");
                        $('#body').append("<td>" + value["未檢錄"] + "</td>");
                        $('#body').append("<td>" + value["已檢錄"] + "</td>");
                        $('#body').append("<td>" + value["合格"] + "</td>");
                        $('#body').append("<td>" + value["不合格"] + "</td>");
                        $('#body').append("<td>" + value["事故"] + "</td>");
                        $('#body').append("<td>" + value["未上傳"] + "</td>");
                        $('#body').append("<td>" + value["已上傳"] + "</td>");
                        $('#body').append("<td>" + value["個人組"] + "</td>");
                        $('#body').append("<td>" + value["團體組"] + "</td>");
                        $('#body').append("<td>" + value["志願役"] + "</td>");
                        $('#body').append("<td>" + value["義務役"] + "</td>");
                        $('#body').append("<td>" + value["未滿半年"] + "</td>");
                        $('#body').append("<td>" + value["男性"] + "</td>");
                        $('#body').append("<td>" + value["女性"] + "</td>");
                        $('#body').append("<td>" + value["替代項目"] + "</td>");
                        $('#body').append("</tr>");
                        //alert(value["單位"]);
                    });
                    //$('table').attr({ style: "border:solid 1px black" });
                    //$('td').attr({ style: "border:solid 1px black" });
                    //$('tr').attr({ style: "border:solid 1px black" });
                    //$('th').attr({ style: "border:solid 1px black" });

                    
                }
                else {
                    alert('Get Json Error !');
                }
            });

        });
    
    </script>--%>
    
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>
    <table>
    <thead id="head">
    <tr>
    <th>單位代碼</th><th>單位</th><th>總數</th><th>未檢錄</th><th>已檢錄</th><th>合格</th><th>不合格</th><th>事故</th><th>未上傳</th><th>已上傳</th><th>個人組</th><th>團體組</th><th>志願役</th><th>義務役</th><th>未滿半年</th><th>男性</th><th>女性</th><th>替代項目</th>
    </tr>
    </thead>
    <tbody id="body"></tbody>
    </table>
    </div>--%>
    <div>
    
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
            <RowStyle BackColor="#E3EAEB" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
