<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProtocoloWeb.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login ProtocoloWeb</title>
        <meta http-equiv="X-UA-Compatible" content="IE=9">
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9"/>
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />
        <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
        <link rel="stylesheet" href="Scripts/my.css" />
        <script src="Scripts/jquery-1.7.2.min.js"></script>
        <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Home -->
    <div data-role="navbar">
        <ul>
            <li><a href="#" >
                <img src="Images/RecordMob.png" /></a></li>
        </ul>
    </div>
            <div data-role="content">
                <ul data-role="listview" data-divider-theme="b" data-inset="true">
                    <li data-role="list-divider" role="heading">
                        Login
                    </li>
                    <li>
                        <div data-role="fieldcontain">
                             <label for="txtLogin">Usuário</label>
                             <input type="text" runat="server" name="txtLogin" id="txtLogin" value="">
                        </div>
                    </li>
                    <li>
                        <div data-role="fieldcontain">
                             <label for="txtSenha">Senha</label>
                             <input type="password" runat="server" name="txtSenha" id="txtSenha" value="">
                        </div>
                    </li>
                    <li runat="server" id="liMsg" visible="false">
                        <div data-role="fieldcontain">
                             <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </li>
                    
                </ul>
                
                <asp:Button ID="btnLogar" data-icon="check" data-role="button" runat="server" data-theme="b" Text="Logar" OnClick="btnLogar_Click" style="width:100%"/>
            </div>
          
    </form>
</body>
</html>
