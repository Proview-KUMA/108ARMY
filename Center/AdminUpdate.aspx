<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUpdate.aspx.cs" Inherits="AdminUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="修改狀態(免測已上傳) =&gt; (免測未上傳) " />    
    </div>
    
    <div>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="修改狀態(塗晨貿已上傳) => (未上傳) " />            
    </div>
    <div>
        <asp:Button ID="Button3" runat="server" Text="修改替代項目評分表, 19歲下限改為0歲, 59歲上限改為99歲" 
            onclick="Button3_Click" />
    </div>
    <div>
        <asp:Button ID="Button4" runat="server" 
            Text="修改下列單位(63471,63355,31R43,150N4)的鑑測日期為2012-12-04,且重新計算年齡" 
            onclick="Button4_Click" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="身分證字號:"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button5" runat="server" Text="修改狀態(已上傳=>未上傳)" onclick="Button5_Click" />
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="訊息"></asp:Label>
    </div>
    </form>
</body>
</html>
