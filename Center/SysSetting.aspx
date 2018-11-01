<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SysSetting.aspx.cs" Inherits="SysSetting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager> 
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">

<ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="系統設定">
    <HeaderTemplate>
        系統設定
    </HeaderTemplate>
<ContentTemplate>
<div>
    <h4>目前系統設定為 : <asp:Label ID="LB_mode" runat="server" Text="Label" Font-Size="XX-Large" ForeColor="Red"></asp:Label></h4><br />
    <br /><br />
    <asp:Button ID="btnSwitch" runat="server" Text="切換系統" OnClientClick="if(confirm('確定要切換系統嗎？')){return true;}else{return false;}" 
        onclick="btnSwitch_Click" /><br /><br /><br />
        <h4>競賽系統設定</h4>
        <span>(注意 : 以下設定只有在競賽模式有效)</span><br />
        <span>資料交換模式</span><asp:CheckBoxList ID="CheckBoxList1" runat="server">
        <asp:ListItem Text="本機" Value="local"></asp:ListItem><asp:ListItem Text="遠端主機" 
            Value="remote"></asp:ListItem>
        </asp:CheckBoxList>
        <span>遠端主機網路位址 : </span><asp:TextBox ID="txtRemoteIP" runat="server"></asp:TextBox>
    <asp:Button ID="Bt_TestWebService"
        runat="server" Text="測試遠端主機連線" onclick="Bt_TestWebService_Click" />
    <asp:Label ID="LB_WsMessage" runat="server"></asp:Label><br />
    <asp:Button ID="Button1" runat="server" Text="確認" onclick="Button1_Click" /><br /><br />
</div>
<div>
<table>
    <tbody>
        <tr>
            <td>管理類 :</td>
            <td>
            <input id="input2" type="button" runat="server" value="競賽報進" onclick="window.open('SignUp.aspx');" />
            <input id="input11" type="button" runat="server" value="檢視報進名單(檢錄前)" onclick="window.open('DeSignUp.aspx');" />
            <input id="input4" type="button" runat="server" value="替代項目輸入(成績結算前)" onclick="window.open('Race_ReplaceItemScore.aspx');" />
            <input id="input3" type="button" runat="server" value="成績補正(成績結算後)" onclick="window.open('Race_ResultCorrect.aspx');" />            
            <input id="input5" type="button" runat="server" value="成績上傳" onclick="if(confirm('成績一經上傳無法進行成績補正，請確認已經完成必要動作'))window.open('Race_ResultMag.aspx');" />          
            <asp:Button ID="ClearResult" runat="server" Text="清除競賽成績" OnClientClick="if(confirm('確定要清除競賽成績嗎?\n競賽期間請自行評估刪除競賽成績所承擔之風險，被刪除的成績不將有任何備份')){return true;}else{return false;}" onclick="ClearResult_Click" Visible="false" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>報表類 :</td>
            <td>
            <input id="input6" type="button" runat="server" value="競賽狀態檢視" onclick="window.open('RaceNotice.aspx');" />
            <input id="input7" type="button" runat="server" value="個人組成績表" onclick="window.open('Race_StatPerson.aspx');" />
            <input id="input8" type="button" runat="server" value="單位成績表" onclick="window.open('Race_StatTeam.aspx');" />
            <input id="input9" type="button" runat="server" value="團體組成績冊" onclick="window.open('Race_StatTeamAll.aspx');" />
            <input id="input1" type="button" runat="server" value="競賽標準查詢" onclick="window.open('RaceStardandCheck.aspx');" />
            <input id="input10" type="button" runat="server" value="競賽記錄" onclick="window.open('RaceLog.aspx');" />
            <input id="input12" type="button" runat="server" value="查詢單位代碼" onclick="window.open('FindUnitCode.aspx');" />
  <input id="input13" type="button" runat="server" value="查詢受測員" onclick="window.open('Race_FindPerson.aspx');" />
            </td>
        </tr>
        <tr>
        <td>其他類</td>
        <td><input id="input14" type="button" runat="server" value="檔案下載" onclick="window.open('Race_DownLoad.aspx');" /></td>
        </tr>
        
    </tbody>
</table>







</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>
</asp:Content>


