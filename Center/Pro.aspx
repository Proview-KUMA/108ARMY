<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pro.aspx.cs" Inherits="Pro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
<script type="text/javascript" src="Script/RankCode.ashx"></script>
<script type="text/javascript" src="Script/Common.js"></script>
<script type="text/javascript">
    function DomID(id) {
        //var prefix = "ctl00_ContentPlaceHolder1_";
        var prefix = "ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_";
    return '#' + prefix + id;
}

$(function() {
    var id_ok = true;
    var pwd_ok = true;
    var rank_ok = true;
    var pwd_old_ok = false;
    var pwd_new_ok = false;
    $(DomID('txtID')).blur(function() {
        if (checkID($(this).val())) {
            $(this).parent().next().html('');
            id_ok = true;
        }
        else {
            $(this).parent().next().html('身分證錯誤');
            id_ok = false;
        }
    });
    $(DomID('txtPwd')).blur(function() {
        $(this).parent().next().html(checkPwd($(this).val()));
        if (checkPwd($(this).val()) == '') {
            pwd_ok = true;
        }
        else {
            pwd_ok = false;
        }


    });
    $(DomID('txtRank')).blur(function() {
        $(this).parent().next().html(GetRank($(this).val()));
        //alert(GetRank($(this).val()));
        if (!GetRank($(this).val())) {
            rank_ok = false;
            $(this).parent().next().html('無此級職');
        }
        else {
            rank_ok = true;
        }
    });

    $('#confirm').click(function() {
        if (id_ok == true && pwd_ok == true && rank_ok == true) {
            $('#ctl00_ContentPlaceHolder1_submitType').val('profile');
            $('#aspnetForm')[0].submit();
        }
        else {
            alert('資料不正確');
        }
    });

    $('#oldPwd').blur(function() {
        if ($(this).val() != $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_pwd_HF').val()) {
            $(this).parent().next().html('*');
            pwd_old_ok = false;
        }
        else {
            $(this).parent().next().html('');
            pwd_old_ok = true;
        }
    });

    $('#newPwd1').blur(function() {
        if ($(this).val() == '') {
            $(this).parent().next().html('*');
            pwd_new_ok = false;
        }
        else {
            $(this).parent().next().html('');
        }
    });

    $('#newPwd2').blur(function() {
        var v = checkPwd($(this).val());
        //alert(v);
        if ($('#newPwd1').val() != '' && $(this).val() == $('#newPwd1').val() && checkPwd($('#newPwd2').val()) == '') {
            $(this).parent().next().html('');
            pwd_new_ok = true;
        }
        else {
            $(this).parent().next().html('*');
            pwd_new_ok = false;
        }
    });

    $('#pwdChgBtn').click(function() {
        if (pwd_new_ok == true && pwd_old_ok == true) {
            $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_pwd_HF').val($('#newPwd2').val());
            $('#ctl00_ContentPlaceHolder1_submitType').val('pwdchange');
            $('#aspnetForm')[0].submit();
        }
        else {
            alert('請檢核密碼');
        }

    });

});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:HiddenField ID="submitType" runat="server" />
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="個人資料維護">
<ContentTemplate>
<table>
   <tbody>
   <tr>
   <td>姓名</td>
   <td>
       <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td>身分證號</td>
   <td>
       <asp:TextBox ID="txtID" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr style="display:none">
   <td>密碼</td>
   <td>
       <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td>級職</td>
   <td>
       <asp:TextBox ID="txtRank" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td>電話</td>
   <td>
       <asp:TextBox ID="txtTel" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td>電子郵件</td>
   <td>
       <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>@webmail.mil.tw</td><td></td>
   </tr>
   <tr>
   <td>行動電話</td>
   <td>
       <asp:TextBox ID="txtCell" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td>IP</td>
   <td>
       <asp:TextBox ID="txtIP" runat="server"></asp:TextBox></td><td></td>
   </tr>
   <tr>
   <td colspan="3">
       <input id="confirm" type="button" value="確定" /></td>
   </tr>
   </tbody>
  </table>
</ContentTemplate>
  

</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="密碼變更">
<ContentTemplate>
<div>
<asp:HiddenField ID="pwd_HF" runat="server" />
<table id="pwdchange">
<tbody>
<tr>
<td>舊密碼</td>
<td><input type="password" id="oldPwd"   /></td>
<td></td>
</tr>
<tr>
<td>新密碼</td>
<td><input type="password" id="newPwd1" /></td>
<td></td>
</tr>
<tr>
<td>確認新密碼</td>
<td><input type="password" id="newPwd2" /></td>
<td></td>
</tr>
<tr>
<td colspan="3"><input type="button" id="pwdChgBtn" value="確認" /></td>
</tr>
</tbody>
</table>
</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>



</asp:Content>

