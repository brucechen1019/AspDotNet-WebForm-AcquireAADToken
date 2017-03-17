<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Async="true" Inherits="WebFormApplication.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Token:<asp:Label runat="server" ID="lblStatus"></asp:Label><br />
            <asp:Button runat="server" ID="btnLoginAsync" Text="Login asynchronously" OnClick="btnLoginAsync_Click" />
        </div>

        <div>
            Token:<asp:Label runat="server" ID="lblStatus1"></asp:Label><br />
            <asp:Button runat="server" ID="btnLoginSync" Text="Login synchronously" OnClick="btnLoginSync_Click" />
        </div>
    </form>
</body>
</html>
