<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ferramentas.aspx.cs" Inherits="Controladoria.Ferramentas" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>Formulário</title>
        <meta http-equiv="X-UA-Compatible" content="IE=9">
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9"/>
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />

        <script src="Scripts/functions.js"></script>
        <script src="Scripts/functions.js"></script>
        <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
        <script src="Scripts/jquery-1.7.2.min.js"></script>
        <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
        <script src="Scripts/maskMoney.js"></script>
        <link rel="stylesheet" href="Content/my.css" />
        

        <!-- User-generated css -->
        <style>
            .containing-element .ui-slider-switch {
                width: 9em;
            }
            .overlay
            {
                position: absolute;
                top: 0;
                left:0;
                width: 100%;
                height: 100%;
                background: #000;
                display: none;
                opacity: .5;
                filter: alpha(opacity=50);
                -ms-filter:"progid:DXImageTransform.Microsoft.Alpha(Opacity=50)";


            }
            .popup
            {
                position: absolute;
                top: 50%;
                left: 50%;
                margin-left: -150px;
                margin-top: -100px;
                width: 300px;
                height: 200px;
                display: none;
                background: Orange;
            }
        </style>
        
</head>
<body>

  <div data-role="page" id="page1">
        <div data-role="navbar">
            <ul>
                <li>
                    <img style="float:left;" src="Images/RecordMob.png" />
                    <%--<a href="#" style="float:right;" data-role="button" data-mini="true" data-inline="true" data-theme="b">
                        <asp:Label ID="lblLogin" runat="server" />
                    </a>--%>
                    <a href="#popupNested" data-rel="popup" style="float:right;" data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b" style="color:white;">
                        <asp:Label ID="lblLogin" runat="server" />
                    </a>
                </li>
            </ul>
        </div>
        <br/>
        <div data-role="header" data-theme="a">
            <a href="Default.aspx" target="_self" data-icon="home" data-theme="b">Menu</a>
            <h1 style="color:white;">Ferramentas</h1>
        </div>
        <div data-role="content">
            <ul data-role="listview" data-divider-theme="b" data-inset="true">
                <li>
                    <a href="ExportacaoAP.aspx" target="_self">
                        Exportação AP
                    </a>
                </li>
            </ul>
        </div>
      
        <div data-role="popup" id="popupNested" data-theme="none">
            <div data-role="content" data-theme="b" style="margin:0; max-width:200px;">
                <div data-role="list-divider" data-mini="true" data-inset="true">
                    <h2 style="text-align:center;" >LogOff</h2>
                    <div data-role="content" data-theme="b" class="ui-corner-bottom ui-content">
                        <h3 style="text-align:center;" class="ui-title">Tem certeza que deseja fazer o Log Off?</h3>
                        <div style="text-align:center; width:150px;">
                            <a href="#" data-role="button" data-mini="true" data-inline="true" data-rel="back" data-theme="c" >Não</a>
                            <a href="Login.aspx?logout='Y'" target="_self" data-role="button" data-mini="true" data-inline="true" data-transition="flow" data-theme="a">Sim</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</body>
</html>

