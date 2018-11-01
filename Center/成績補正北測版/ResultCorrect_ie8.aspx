<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResultCorrect_ie8.aspx.cs" Inherits="ResultCorrect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
        if (typeof (JSON) == 'undefined') { //Fix IE JSON
            $.getScript('JS/json2.js');
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        
    </script>
    <script type="text/javascript">
        //function Check_note() {
        //    alert('1234');
        //    var sit = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_sit_note').value;
        //    var push = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_push_note').value;
        //    var run = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_run_note').value;
        //    alert('5678');
        //    alert(sit);
        //    alert(push);
        //    alert(run);
            
        //}
        
    </script>
    <script type="text/javascript">
        //$(function () {
        //    $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_Button1').click(function () {
        //        alert('1234');
        //        var sit = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_sit_note').value;
        //        var push = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_push_note').value;
        //        var run = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_run_note').value;
        //        alert('5678');
        //        alert(sit);
        //        alert(push);
        //        alert(run);
        //    })
        //})
    </script>
   
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="成績補正">
<ContentTemplate>
    
<div>
    
<table>

<tr>
<td>身分證號 </td><td><input type="text" id="id" runat="server" maxlength="10" style="text-transform:uppercase;" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server"
        Text="搜尋" onclick="Button1_Click" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Font-Names="標楷體" style="cursor:pointer;background-color:#f0e68c"/>
  
             
             </td>
</tr>
<tr>
<td>姓名
    </td><td><input type="text" id="name"  runat="server" readonly="readonly" disabled="disabled"  style="background-color:#CCCCFF;color:red;border-style:groove" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr>
<td><asp:Label ID="sit_ups_name" runat="server" ></asp:Label>
   
</td>
    <td> <asp:TextBox ID="txb_sit1" runat="server" Width="50px" Visible="False" CssClass="auto-style1" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_sit1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_sit2" runat="server" Width="50px" Visible="False" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_sit2" runat="server" Text="秒" Visible="False"></asp:Label>
    </td>
    
</tr>
<tr>
<td><asp:Label ID="push_ups_name" runat="server"></asp:Label>
    
</td><td> <asp:TextBox ID="txb_push1" runat="server" Width="50px" Visible="False" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_push1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_push2" runat="server" Width="50px" Visible="False" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_push2" runat="server" Text="秒" Visible="False"></asp:Label>
     </td>
</tr>
<tr>
<td><asp:Label ID="run_name" runat="server" ></asp:Label></td>
    <td>
        &nbsp;
        <asp:TextBox ID="txb_run1" runat="server" Width="50px" Visible="False" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_run1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_run2" runat="server" Width="50px" Visible="False" onKeypress="if(event.keyCode < 48 || event.keyCode > 57) if(event.keyCode != 13 ){ event.returnValue = false; }" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_run2" runat="server" Text="秒" Visible="False"></asp:Label>
     </td>
</tr>
<tr>

<td colspan="2" >
    <li style="margin-left:15px">如有未測驗之項目則該項目不輸入任何值。</li>
</td>
    <tr>
        <td colspan="2" style="margin-left:15px" >
            <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Size="Large" ForeColor="Red" OnClick="Button2_Click" style="cursor:pointer;background-color:#7fffd4" Text="補正成績" />
            <asp:HiddenField ID="dateValue" runat="server" />
            <asp:HiddenField ID="checkid" runat="server" />
        </td>
    </tr>
</tr>
</table>
</div>
        
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

