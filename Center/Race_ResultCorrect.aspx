﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_ResultCorrect.aspx.cs" Inherits="Race_ResultCorrect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

    function MyFun_situps(e, obj, click) {
        if (click) {
            $('#situps_min').val('');
            $('#situps_sec').val('');
        }
    }

    function MyFun_situps_min(e, obj, click) {
        if (click) {
            $('#situps').val('');
        }
    }

    function MyFun_situps_sec(e, obj, click) {
        if (click) {
            $('#situps').val('');
        }
    }

    function MyFun_pushups(e, obj, click) {
        if (click) {
            $('#pushups_min').val('');
            $('#pushups_sec').val('');
        }
    }

    function MyFun_pushups_min(e, obj, click) {
        if (click) {
            $('#pushups').val('');
        }
    }

    function MyFun_pushups_sec(e, obj, click) {
        if (click) {
            $('#pushups').val('');
        }
    }

    function MyFun_run(e, obj, click) {
        if (click) {
            $('#run_min').val('');
            $('#run_sec').val('');
        }
    }

    function MyFun_run_min(e, obj, click) {
        if (click) {
            $('#run').val('');
        }
    }

    function MyFun_run_sec(e, obj, click) {
        if (click) {
            $('#run').val('');
        }
    }
    </script>
    <title>成績補正</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<table>
<tr>
<td>身分證號</td><td><input type="text" id="id" runat="server" />
    <asp:Button ID="Button1" runat="server"
        Text="搜尋" onclick="Button1_Click" /></td>
</tr>
<tr>
<td>姓名</td><td><input type="text" id="name" runat="server" /> </td>
</tr>
<tr>
<td><asp:Label ID="sit_ups_name" runat="server" Text="測驗項目"></asp:Label></td><td><input type="text" id="situps" runat="server" onkeypress="return isNumberKey(event)"  onkeydown ="MyFun_situps(event, this, true)" /> 次&nbsp; 
    <input type="text" id="situps_min" runat="server" onkeypress="return isNumberKey(event)" onkeydown="MyFun_situps_min(event, this, true)"/>&nbsp; 分&nbsp; 
    <input type="text" id="situps_sec" runat="server" onkeypress="return isNumberKey(event)" onkeydown="MyFun_situps_sec(event, this, true)"/>&nbsp; 
    秒</td>
</tr>
<tr>
<td><asp:Label ID="push_ups_name" runat="server" Text="測驗項目"></asp:Label></td><td><input type="text" id="pushups" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_pushups(event, this, true)" /> 次&nbsp; 
    <input type="text" id="pushups_min" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_pushups_min(event, this, true)" />&nbsp; 分&nbsp; 
    <input type="text" id="pushups_sec" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_pushups_sec(event, this, true)" />&nbsp; 
    秒</td>
</tr>
<tr>
<td><asp:Label ID="run_name" runat="server" Text="測驗項目"></asp:Label></td><td><input type="text" id="run" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_run(event, this, true)"/> 次&nbsp; 
    <input type="text" id="run_min" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_run_min(event, this, true)"/>&nbsp; 分&nbsp; 
    <input type="text" id="run_sec" runat="server" onkeypress="return isNumberKey(event)" onkeydown ="MyFun_run_sec(event, this, true)"/>&nbsp; 
    秒</td>
</tr>
<tr>
<td colspan="2"><asp:Button ID="Button2" runat="server" Text="補正成績" 
        onclick="Button2_Click" /><asp:HiddenField ID="dateValue" runat="server" />
    <asp:HiddenField ID="checkid" runat="server" />
    <asp:HiddenField ID="status" runat="server" />
</td>
</tr>
</table>
</div>

    </form>
</body>
</html>
