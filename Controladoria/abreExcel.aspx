<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abreExcel.aspx.cs" Inherits="Controladoria.abreExcel" ContentType="application/vnd.ms-excel" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.33242, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProtocoloWeb</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />

    <script src="Scripts/functions.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
    <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
    <script src="Scripts/jquery.maskedinput.js"></script>
    <script src="Scripts/maskMoney.js"></script>
    <link rel="stylesheet" href="Content/my.css" />

    <!-- FooTable -->
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <link href="Styles/footable-0.1.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/data-generator.js" type="text/javascript"></script>
    <script src="Scripts/session.js"></script>
    <script src="Scripts/footable.js" type="text/javascript"></script>

    <!-- FooTable -->
    <!-- User-generated css -->
    <style>
        .containing-element .ui-slider-switch {
            width: 9em;
        }


        .ui-select .ui-btn {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <!-- User-generated js -->
</head>
<body>
    <div data-role="page" id="page1">
        <div data-role="navbar">
            <ul>
                <li>
                    <img style="float: left;" src="Images/RecordMob.png" />

                </li>
            </ul>
        </div>
        <form id="form1" runat="server">
        </form>
        <div data-role="header" data-theme="a">
            <a href="Default.aspx" target="_self" data-icon="home" data-theme="b">Menu</a>

            <div id="mask" class="mask" style="display: none"></div>
            <div data-role="content" align="center">
            </div>
        </div>
    </div>

</body>
</html>
