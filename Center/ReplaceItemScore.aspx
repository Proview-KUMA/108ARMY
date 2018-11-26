<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReplaceItemScore.aspx.cs" Inherits="ReplaceItemScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        //支援IE寫法
        function CheckNum() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit1"))//有sit1
            {
                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit2").value))//檢查sit1跟sit2
                {
                    //檢查錯誤
                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                }
                else //檢查正確
                {
                    //再來檢查push1
                    if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1"))//有push1
                    {
                        if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").value))//檢查push1跟push2
                        {
                            //檢查錯誤
                            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                            alert('成績輸入包含錯誤字元，請重新輸入!!');
                        }
                        else {
                            //檢查正確
                            //再來檢查run1
                            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1"))//有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//檢查run1跟run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                            else//沒有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//只檢查run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                        }
                    }
                    else//沒有push1
                    {
                        if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").value))//只檢查push2
                        {
                            //檢查錯誤
                            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                            alert('成績輸入包含錯誤字元，請重新輸入!!');
                        }
                        else //檢查正確
                        {
                            //檢查正確
                            //再來檢查run1
                            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1"))//有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//檢查run1跟run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                            else//沒有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//只檢查run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                        }
                    }
                }
            }
            else//沒有sit1
            {
                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit2").value))//只檢查sit2
                {
                    //檢查錯誤
                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                }
                else //檢查正確
                {
                    //再來檢查push1
                    if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1"))//有push1
                    {
                        if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").value))//檢查push1跟push2
                        {
                            //檢查錯誤
                            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                            alert('成績輸入包含錯誤字元，請重新輸入!!');
                        }
                        else {
                            //檢查正確
                            //再來檢查run1
                            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1"))//有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//檢查run1跟run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                            else//沒有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//只檢查run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                        }
                    }
                    else//沒有push1
                    {
                        if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").value))//只檢查push2
                        {
                            //檢查錯誤
                            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                            alert('成績輸入包含錯誤字元，請重新輸入!!');
                        }
                        else //檢查正確
                        {
                            //檢查正確
                            //再來檢查run1
                            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1"))//有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").value) |
                isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//檢查run1跟run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                            else//沒有run1
                            {
                                if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value))//只檢查run2
                                {
                                    //檢查錯誤
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                                    alert('成績輸入包含錯誤字元，請重新輸入!!');
                                }
                                else {
                                    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
                                    //alert('正確')
                                }
                            }
                        }
                    }
                }
            }



            //if (
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit1").value) |
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit2").value) |
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1").value) |
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").value) |
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").value) |
            //    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").value)
            //    )
            //{
            //    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
            //    alert('成績輸入包含錯誤字元，請重新輸入!!');
            //}
            //else
            //{
            //    document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '1';
            //    //alert('正確')
            //}
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="替代項目成績輸入">
<ContentTemplate>
<div style="margin-left:15px">
<table>
<tr>
<td>身分證號</td><td><input type="text" id="id" runat="server" maxlength="10" style="text-transform:uppercase;" autofocus="autofocus"/>
    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server"
        Text="搜尋" onclick="Button1_Click" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Font-Names="標楷體" style="cursor:pointer;background-color:#f0e68c" /></td>
</tr>
<tr>
<td>姓名</td><td><input type="text" id="name" runat="server" readonly="readonly" disabled="disabled" style="background-color:#CCCCFF;color:red;border-style:groove" /> &nbsp;&nbsp;&nbsp;</td>
</tr>
<tr>
<td><asp:Label ID="sit_ups_name" runat="server"></asp:Label></td>
<td>
    <asp:TextBox ID="txb_sit1" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_sit1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_sit2" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_sit2" runat="server" Text="秒" Visible="False"></asp:Label>
</td>
</tr>
<tr>
<td class="auto-style1"><asp:Label ID="push_ups_name" runat="server"></asp:Label></td>
<td class="auto-style1">
    <asp:TextBox ID="txb_push1" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_push1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_push2" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_push2" runat="server" Text="秒" Visible="False"></asp:Label>
</td>
</tr>
<tr>
<td><asp:Label ID="run_name" runat="server"></asp:Label></td>
<td>
    <asp:TextBox ID="txb_run1" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_run1" runat="server" Text="分" Visible="False"></asp:Label>
        <asp:TextBox ID="txb_run2" runat="server" Width="50px" Visible="False"  MaxLength="4"></asp:TextBox>
        <asp:Label ID="lab_run2" runat="server" Text="秒" Visible="False"></asp:Label>
</td>
</tr>
<tr>
<td colspan="2">
<li>替代項目成績輸入請參考替代項目評分標準表。</li>
    <li>如有未測驗之項目則該項目不輸入任何值。</li>
</td>
</tr>
<tr>
<td colspan="2"><asp:Button ID="Button2" runat="server" Text="確認替代項目成績" 
        Onclick="Button2_Click" OnClientClick="CheckNum()" Font-Bold="True" Font-Size="Large" ForeColor="Red" Font-Names="標楷體" style="cursor:pointer;background-color:#7fffd4" Visible="False" /><asp:HiddenField ID="dateValue" runat="server" />
    <asp:HiddenField ID="checkid" runat="server" />
    <asp:HiddenField ID="check_num" runat="server" />
</td>
</tr>
</table>
</div>
 
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

