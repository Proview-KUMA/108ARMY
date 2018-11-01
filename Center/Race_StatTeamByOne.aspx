<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_StatTeamByOne.aspx.cs" Inherits="Race_StatTeamByOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server" id="title">101年三項基本體能競賽成績冊</title>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#report td, #report th, #report tr, #report').attr({ style: "border:solid 1px black" });
            $('#report').css('width', '21cm').css('height', '29cm').css('font-family', '標楷體').css('font-size', 'large');

            $('[ID*=lb]').parent('td').css('text-align', 'right');
            $('#head').css('text-align', 'center').css('font-size', 'xx-large');
            
            $('#word').click(function() {
                var curTbl = document.getElementById('main');
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

        function printpr() {
            var OLECMDID = 7;
            /* OLECMDID values:
            * 6 - print
            * 7 - print preview
            * 1 - open window
            * 4 - Save As
            */
            var PROMPT = 1; // 2 DONTPROMPTUSER
            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser);
            WebBrowser1.ExecWB(OLECMDID, PROMPT);
            WebBrowser1.outerHTML = "";
        }
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <table id="report" style="width: 100%;border:1px solid black">
            <tr style="border: 1px solid #C0C0C0">
                <td id="head" colspan="7" style="text-align:center;font-size:larger">
                    
                   <span><asp:Label ID="lb_unit" runat="server" Text="Label"></asp:Label></span> 
                    <br />
                    <span>參加國軍101年三項基本體能競賽成績冊</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    餉冊人數
                </td>
                <td colspan="2">
                    <asp:Label ID="lb_total" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="6">
                    仰臥起坐</td>
                <td rowspan="2">
                    受測人數</td>
                <td rowspan="2">
                    <asp:Label ID="lb_sit_ups_count" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    BMI&gt;=30</td>
                <td>
                    <asp:Label ID="lb_213_223" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="2">
                    <asp:Label ID="lb_freeNo" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    免技測</td>
                <td>
                    <asp:Label ID="lb_233" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="2">
                    合格人數</td>
                <td rowspan="2">
                    <asp:Label ID="lb_sit_ups_202" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="3">
                    免測人數</td>
                <td>
                    屆退</td>
                <td>
                    <asp:Label ID="lb_224" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="3">
                    <asp:Label ID="lb_free" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    懷孕</td>
                <td>
                    <asp:Label ID="lb_204" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="2">
                    合格率(%)<br />(含替代人員)</td>
                <td rowspan="2">
                    <asp:Label ID="lb_sit_ups_rate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                   公勤<br />
                   (受訓,外調,婚喪假,因公住院,出國)</td>
                <td>
                    <asp:Label ID="lb_214" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    替代項目測驗人數</td>
                <td>
                    <asp:Label ID="lb_repl" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="4">
                    俯地挺身</td>
                <td>
                    受測人數</td>
                <td>
                    <asp:Label ID="lb_push_ups_count" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    應測人數<br />(餉冊人數減免測人數)</td>
                <td>
                    <asp:Label ID="lb_should" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    合格人數</td>
                <td>
                    <asp:Label ID="lb_push_ups_202" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    實測人數<br />(應測人數減無故未測人數)</td>
                <td>
                    <asp:Label ID="lb_real" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="2">
                    合格率(%)<br />(含替代人員)</td>
                <td rowspan="2">
                    <asp:Label ID="lb_push_ups_rate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    到測率(%)<br />(實測人數除以應測人數)
                    <br />
                    </td>
                <td>
                    <asp:Label ID="lb_attend_rate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    全項合格人數</td>
                <td>
                    <asp:Label ID="lb_202" runat="server" Text="Label"></asp:Label>
                </td>
                <td rowspan="3">
                    3000公尺跑步</td>
                <td>
                    受測人數</td>
                <td>
                    <asp:Label ID="lb_run_count" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    不合格人數</td>
                <td>
                    <asp:Label ID="lb_203" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    合格人數</td>
                <td>
                    <asp:Label ID="lb_run_202" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    鑑測合格率(%)<br />(合格率:全項合格人數除以應測人數)
                    <br />
                    <asp:Label ID="note" runat="server" Text=""></asp:Label>
                    
                    
                    </td>
                <td>
                    <asp:Label ID="lb_rate" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    合格率(%)<br />(含替代人員)</td>
                <td>
                    <asp:Label ID="lb_run_rate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    鑑測官簽章</td>
                <td colspan="3">
                    &nbsp;</td>
                <td colspan="2">
                    監察官簽章</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="1">
                    鑑測組長簽章</td>
                <td colspan="3">
                    &nbsp;</td>
                <td colspan="2">
                    單位主官簽章</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
    <input id="print" value="列印(限IE)" type="button" onclick="JavaScript:this.style.display='none';word.style.display='none';printpr();" />
    <input id="word" value="EXCEL(限IE)" type="button" />
    </div>
    </form>
</body>
</html>
