﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_Download.aspx.cs" Inherits="Race_Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>下載檔案</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server" 
            onselectednodechanged="TreeView1_SelectedNodeChanged">
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
