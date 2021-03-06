﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserMagEdit.aspx.cs" Inherits="UserMagEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript" src="Script/RankCode.ashx"></script>
    <script type="text/javascript" src="Script/Common.js"></script>
    <script type="text/javascript">
        function DomID(id) {
            //var prefix = "ctl00_ContentPlaceHolder1_";
            var prefix = "ctl00_ContentPlaceHolder1_tabconatiner_tabpanel1_";
            return '#' + prefix + id;
        }
        function DomID2(id) {
            //var prefix = "ctl00_ContentPlaceHolder1_";
            var prefix = "ctl00_ContentPlaceHolder1_tabconatiner_tabpanel2_";
            return '#' + prefix + id;
        }
        $(function () {
            var id_ok = false;
            var pwd_ok = false;
            var rank_ok = false;
            var update_ok = true;
            $(DomID('txtID')).blur(function () {
                if (checkID($(this).val())) {
                    $(this).parent().next().html('');
                    id_ok = true;
                }
                else {
                    $(this).parent().next().html('身分證錯誤');
                    id_ok = false;
                }
            });
            $(DomID2('_txtID')).blur(function () {
                if (checkID($(this).val())) {
                    $(this).parent().next().html('');
                    update_ok = true;
                }
                else {
                    $(this).parent().next().html('身分證錯誤');
                    update_ok = false;
                }
            });
            $(DomID('txtPwd')).blur(function () {
                $(this).parent().next().html(checkPwd($(this).val()));
                if (checkPwd($(this).val()) == '') {
                    pwd_ok = true;
                }
                else {
                    pwd_ok = false;
                }


            });
            $(DomID2('_txtPwd')).blur(function () {
                $(this).parent().next().html(checkPwd($(this).val()));
                if (checkPwd($(this).val()) == '') {
                    update_ok = true;
                }
                else {
                    update_ok = false;
                }


            });
            $(DomID('txtRank')).blur(function () {
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
            $(DomID2('_txtRank')).blur(function () {
                $(this).parent().next().html(GetRank($(this).val()));
                //alert(GetRank($(this).val()));
                if (!GetRank($(this).val())) {
                    update_ok = false;
                    $(this).parent().next().html('無此級職');
                }
                else {
                    update_ok = true;
                }
            });

            $('#confirm').click(function () {
                if (id_ok == true && pwd_ok == true && rank_ok == true) {
                    $('#ctl00_ContentPlaceHolder1_submitType').val('add');
                    $('#aspnetForm')[0].submit();
                }
                else {
                    alert('資料不正確');
                }
            });
            $('#updateBtn').click(function () {
                if (update_ok == true) {
                    $('#ctl00_ContentPlaceHolder1_submitType').val('update');
                    $('#aspnetForm')[0].submit();
                }
                else {
                    alert('資料不正確');
                }

            });
            $('#deleteBtn').click(function () {
                $('#ctl00_ContentPlaceHolder1_submitType').val('delete');
                $('#aspnetForm')[0].submit();
            });

        });

    </script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptmag"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="submitType" />
    <ajaxToolkit:TabContainer runat="server" ID="tabconatiner" CssClass="ajax__tab_yuitabview-theme">
        <ajaxToolkit:TabPanel runat="server" ID="tabpanel1" HeaderText="新增使用者">
            <ContentTemplate>
                <div id="div_inq" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="帳號身份："></asp:Label>
                    <asp:DropDownList ID="drl_Type" runat="server">
                        <asp:ListItem Value="3">鑑測站主任</asp:ListItem>
                        <asp:ListItem Value="4">鑑測官</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="身份證字號："></asp:Label>
                    <asp:TextBox ID="txb_checkId" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:Button ID="btn_CheckId" runat="server" Text="檢查身份證字號" OnClick="btn_CheckId_Click" />
                    <br />
                    <asp:Label ID="lag_Msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div id="div_data" style="display: none;" runat="server">
                    <table id="add">
                        <tbody>


                            <tr>
                                <td>帳號身分</td>
                                <td>
                                    <asp:DropDownList ID="roleType" runat="server">
                                        <asp:ListItem Value="3">鑑測站主任</asp:ListItem>
                                        <asp:ListItem Value="4">鑑測官</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="auto-style1">帳號</td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txtAcc" runat="server" MaxLength="20"></asp:TextBox></td>
                                <td class="auto-style1"></td>
                            </tr>

                            <tr>
                                <td>姓名</td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="10"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>身分證號</td>
                                <td>
                                    <asp:TextBox ID="txtID" runat="server" Enabled="False" MaxLength="10"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>密碼</td>
                                <td>
                                    <asp:TextBox ID="txtPwd" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>級職代碼</td>
                                <td>
                                    <asp:TextBox ID="txtRank" runat="server" MaxLength="2"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>電話</td>
                                <td>
                                    <asp:TextBox ID="txtTel" runat="server" MaxLength="20"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>電子郵件</td>
                                <td>
                                    <asp:TextBox ID="txtMail" runat="server" MaxLength="30"></asp:TextBox>@webmail.mil.tw</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>行動電話</td>
                                <td>
                                    <asp:TextBox ID="txtCell" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>IP</td>
                                <td>
                                    <asp:TextBox ID="txtIP" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btn_AddAccount" OnClick="btn_AddAccount_Click" runat="server" Text="新增" />
                                </td>

                                    
                                    <td>
                                        <asp:Button ID="btn_Cancel" runat="server" OnClick="btn_Cancel_Click" Text="取消" />
                                    </td>


                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabpanel2" runat="server" HeaderText="使用者維護">
            <ContentTemplate>
                <div>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:Center %>"
                        SelectCommand="SELECT [account] FROM [Account_c] where [role_code] in ('3','4') and [account] not in('proview3','proview4')"></asp:SqlDataSource>
                </div>
                <div>
                    <table id="update">
                        <tbody>

                            <tr>
                                <td>帳號</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                                        DataSourceID="SqlDataSource1" DataTextField="account" DataValueField="account"
                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" OnDataBound="DropDownList1_OnDataBound">
                                    </asp:DropDownList></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td>姓名</td>
                                <td>
                                    <asp:TextBox ID="_txtName" runat="server" MaxLength="10"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>身分證號</td>
                                <td>
                                    <asp:TextBox ID="_txtID" runat="server" Enabled="False" MaxLength="10"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>密碼</td>
                                <td>
                                    <asp:TextBox ID="_txtPwd" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <%--   <tr>
   <td>單位代碼</td>
   <td>
       <asp:TextBox ID="_txtUnit" runat="server"></asp:TextBox></td><td></td>
   </tr>--%>
                            <tr>
                                <td>級職代碼</td>
                                <td>
                                    <asp:TextBox ID="_txtRank" runat="server" MaxLength="2"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>電話</td>
                                <td>
                                    <asp:TextBox ID="_txtTel" runat="server" MaxLength="20"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>電子郵件</td>
                                <td>
                                    <asp:TextBox ID="_txtMail" runat="server" MaxLength="30"></asp:TextBox>@webmail.mil.tw</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>行動電話</td>
                                <td>
                                    <asp:TextBox ID="_txtCell" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>IP</td>
                                <td>
                                    <asp:TextBox ID="_txtIP" runat="server" MaxLength="15"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>帳號身分</td>
                                <td>
                                    <asp:TextBox ID="_txtFun" runat="server" Enabled="false"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input id="updateBtn" type="button" value="確定更新" /></td>
                                <td><input id="deleteBtn" type="button" value="刪除" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>

