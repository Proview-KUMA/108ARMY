<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>陸軍官校鑑測站</title>
    <link id="Login_css" rel="stylesheet" href="main.css" type="text/css" />
    <style type="text/css">
        #Submit2
        {
            width: 98px;
        }
        a:link {
        COLOR: #0000FF;
        }
        a:visited {
        COLOR: #0000FF;
        }
        a:hover {
        COLOR: #0000FF;
        }
        a:active {
        COLOR: #0000FF;
        }

    </style>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
        $(function() {
            $('#txtName').mouseover(function() {
                var type = $('select option:selected').val();
                if (type == "user") {
                    $('#Label1').html('身分證號 : ');
                }
                if (type == "advance") {
                    $('#Label1').html('帳號 : ');
                }

            });

            $('#txtPwd').mouseover(function() {
                var type = $('select option:selected').val();
                if (type == "user") {
                    $('#Label2').html('出生年月日 : ');
                }
                if (type == "advance") {
                    $('#Label2').html('密碼 : ');
                }
            });
        });
    </script>
</head>
<body>
<center>
    <form id="form1" runat="server" submitdisabledcontrols="False">
    <div class="head_back_index">
    <a href="index.aspx" style="color:#0000FF">&nbsp;: : : 回首頁</a>
    </div>
    <div class="head_space_div"> 
    </div>
    <div class="head_div">  
        &nbsp;: : : 現在位置﹥登入
    </div>
    <div class="content_div">
    <div class="content_left_div">
        <div class="content_left_top">
        &nbsp;&nbsp; 登入  
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="content_left_bottom">
            <table>
                <tr>
                    <td class="td_space_left">
                        </td>
                    <td class="td_space_right">
                        </td>
                </tr>
                <tr>
                    <td class="td_left">
                        <asp:Label ID="Label1" runat="server" Text="帳號："></asp:Label>
                    </td>
                    <td class="td_right">
                        <asp:TextBox ID="txtName" runat="server" Height="19px" 
                            style="margin-left: 0px" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_space_left">
                    </td>
                    <td class="td_space_right">
                    </td>
                </tr>
                <tr>
                    <td class="td_left">
                        <asp:Label ID="Label2" runat="server" Text="密碼：" ></asp:Label>
                    </td>
                    <td class="td_right">
                        <asp:TextBox ID="txtPwd" runat="server" Height="19px" Width="90%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td_space_left">
                        </td>
                    <td class="td_space_right">
                        </td>
                </tr>
                <tr>
                    <td class="td_left">
                        &nbsp;</td>
                    <td class="td_right">
                        <asp:Button ID="submit" runat="server" Text="確定" onclick="submit_Click" 
                            Width="74px" />
                    </td>
                </tr>
                <tr>
                    <td class="td_left">
                    </td>
                    <td class="td_right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
   
    </div>
    </form>
    </center>
</body>
</html>
