<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryNonHandle.aspx.cs" Inherits="QueryNonHandle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<link id="Login_css" rel="stylesheet" href="main.css" type="text/css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
    $.postJson('Operations/NonHandle.ashx', { type: 'QueryNonHandle' }, function(data, status) {
            if (status == 'success') {
                document.getElementById('today_001').innerHTML = data[0]["status_001"];
                document.getElementById('today_999').innerHTML = data[0]["status_999"];
                document.getElementById('today_102').innerHTML = data[0]["status_102"];
                document.getElementById('today_103').innerHTML = data[0]["status_103"];
                document.getElementById('today_104').innerHTML = data[0]["status_104"];
                document.getElementById('today_105').innerHTML = data[0]["status_105"];
                document.getElementById('today_106').innerHTML = data[0]["status_106"];
                document.getElementById('yesterday_001').innerHTML = data[1]["status_001"];
                document.getElementById('yesterday_999').innerHTML = data[1]["status_999"];
                document.getElementById('yesterday_102').innerHTML = data[1]["status_102"];
                document.getElementById('yesterday_103').innerHTML = data[1]["status_103"];
                document.getElementById('yesterday_104').innerHTML = data[1]["status_104"];
                document.getElementById('yesterday_105').innerHTML = data[1]["status_105"];
                document.getElementById('yesterday_106').innerHTML = data[1]["status_106"];           
            }
        });
    });
</script>
    <title>鑑測成績未處理統計</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="nonhandle" style="font-family: Arial, Sans-Serif;
    font-size:small;
    table-layout: auto;
    border-collapse: collapse;
    border:#91a7b4 1px solid;
    text-align:center">
        <tbody>
            <tr>
                <td style="border:1px solid #C0C0C0;">狀態</td>
                <td style="border:1px solid #C0C0C0;"><span>鑑測中</span></td>
                <td style="border:1px solid #C0C0C0;"><span>未檢錄</span></td>
                <td style="border:1px solid #C0C0C0;"><span>合格</span></td>
                <td style="border:1px solid #C0C0C0;"><span>不合格</span></td>
                <td style="border:1px solid #C0C0C0;"><span>免測</span></td>
                <td style="border:1px solid #C0C0C0;"><span>補測</span></td>
                <td style="border:1px solid #C0C0C0;"><span>請假</span></td>
            </tr>
            <tr>
                <td style="border:1px solid #C0C0C0;"><span><asp:Label ID="today" runat="server" Text="Label"></asp:Label>鑑測成績未處理統計</span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_001"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_999"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_102"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_103"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_104"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_105"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="today_106"></span></td>
            </tr>
            <tr>
                <td style="border:1px solid #C0C0C0;"><span><asp:Label ID="yesterday" runat="server" Text="Label"></asp:Label>前(含)鑑測成績未處理統計</span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_001"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_999"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_102"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_103"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_104"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_105"></span></td>
                <td style="border:1px solid #C0C0C0;"><span id="yesterday_106"></span></td>
            </tr>
        </tbody>
    </table>
    
    </div>
    <div style="font-family: Arial, Sans-Serif;font-size:small; padding-left:15px">
    <li>鑑測中: 有資料未執行成績列印</li>
    <li>未檢錄: 有未檢錄資料尚未上傳至總部</li>
    <li>不合格: 有不合格資料尚未上傳至總部</li>
    <li>合格: 有合格資料尚未上傳至總部</li>  
    <li>免測: 有免測資料尚未上傳至總部</li>
    <li>補測: 有補測資料尚未上傳至總部</li>
    <li>請假: 有請假資料尚未上傳至總部</li>
    </div>
    <center>
    <div><input id="Button1" type="button" value="關閉" onclick="window.close();"/></div>
    </center>
    </form>
</body>
</html>
