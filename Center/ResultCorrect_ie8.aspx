<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResultCorrect_ie8.aspx.cs" Inherits="ResultCorrect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
        if (document.querySelector) {//不支援ie7
            //alert("8+");
            if (typeof (JSON) == 'undefined') { //Fix IE JSON
                $.getScript('JS/json2.js');
            }
        }
        else {
            //alert("7-");
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
        //支援IE寫法
        function CheckNum() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit2") )
            {
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
            }
            else
            {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_check_num").value = '0';
                alert('請先查詢成績!!');
            }
            
        }
    </script>
        <script type="text/javascript">
            function SetSelected_sit1() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit1").select();
            }
            function SetSelected_sit2() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_sit2").select();
            }
            function SetSelected_push1() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push1").select();
            }
            function SetSelected_push2() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_push2").select();
            }
            function SetSelected_run1() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run1").select();
            }
            function SetSelected_run2() {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_txb_run2").select();
            }
            //ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_id
            //onload="document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_id').focus();"
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
<td>身分證號 </td><td><input type="text" id="id1" runat="server" maxlength="10" style="text-transform:uppercase;" />
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

    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="成績異動查詢">
<ContentTemplate>
    <div style="margin-top: 5px; margin-bottom: 5px; vertical-align: bottom; height: 22px;">
                    <asp:Label ID="Label1" runat="server" Text="請輸入鑑測日期(YYYY/MM/DD)："></asp:Label><asp:TextBox ID="txb_Date" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:Button ID="btn_InqDate" runat="server" CssClass="search" OnClick="btn_InqDate_Click" />
                </div>
                <div id="datenone" style="display: none;" runat="server">
                    <span style="color: red; width: 200px; font-size: 16px; font-weight: bold; text-align: left; font-family: 微軟正黑體;">搜尋結果:查無資料</span>
                </div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" lternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle"
                        Width="960px" PageSize="25" >
                        <Columns>                           
                            <asp:BoundField DataField="id" HeaderText="身份證字號" SortExpression="id" >
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="old_sit_ups" HeaderText="(1)原成績" SortExpression="old_sit_ups">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="new_sit_ups" HeaderText="(1)新成績" SortExpression="new_sit_ups">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="old_push_ups" HeaderText="(2)原成績" SortExpression="old_push_ups">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="new_push_ups" HeaderText="(2)新成績" SortExpression="new_push_ups">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="old_run" HeaderText="(3)原成績" SortExpression="old_run">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="new_run" HeaderText="(3)新成績" SortExpression="new_run">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="account" HeaderText="操作人員" SortExpression="account">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="account_id" HeaderText="操作人員ID" SortExpression="account_id">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="update_time" HeaderText="異動時間" SortExpression="update_time">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            
                        </Columns>
                        <EditRowStyle CssClass="EditRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" Wrap="False" />
                        <PagerStyle CssClass="PagerStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                    </asp:GridView>
                </div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

