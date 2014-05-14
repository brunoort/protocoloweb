<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="ProtocoloWeb.Pesquisar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Formulário</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />

    <script src="Scripts/functions.js"></script>
    <script src="Scripts/functions.js"></script>
    <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
    <script src="Scripts/jquery-1.7.2.min.js"></script>
    <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
    <script src="Scripts/jquery.maskedinput.js"></script>
    <link rel="stylesheet" href="Content/my.css" />
    vi

        <!-- FooTable -->
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <link href="Styles/footable-0.1.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/data-generator.js" type="text/javascript"></script>

    <script src="Scripts/footable.js" type="text/javascript"></script>
    <!-- DataPicker -->
    <script>
        jQuery(function ($) {
            $("#txtDataInicial").mask("99/99/9999");
            $("#txtDataFinal").mask("99/99/9999");

        });

        function redireciona() {

            window.location.href = "exibeResultado.aspx";

        }

    </script>
    <!-- DataPicker -->
    <!-- User-generated css -->
    <style>
        .containing-element .ui-slider-switch {
            width: 9em;
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
                    <a href="#popupNested" data-rel="popup" style="float: right; color: white;" data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b">
                        <asp:Label ID="lblLogin" runat="server" />
                    </a>
                </li>
            </ul>
        </div>
        <br />
        <div data-role="header" data-theme="a">
            <a href="Default.aspx" target="_self" data-icon="home" data-theme="b">Menu</a>
            <h1 style="color: white;">Visualizar</h1>
        </div>
        <div data-role="content" align="center">
            <form id="form1" runat="server">
                <div id="divPeriodo" align="left">
                    <label class="desc" for="txtDataVencimento" style="padding-right: 10px;">PERÍODO</label>
                    <div style="display: inline-table;">
                        <input id="txtDataInicial" runat="server" name="txtDataInicial" class="field text fn" style="position: relative; z-index: 100;" value="" size="8" tabindex="5" />
                        <input id="txtDataFinal" runat="server" name="txtDataFinal" class="field text fn" style="position: relative; z-index: 100;" value="" size="8" tabindex="5" />
                    </div>
                </div>

                <div id="divNFAP" align="left">
                    <label class="desc" for="txtNF" style="padding-right: 10px;">Nº NF</label>
                    <div style="display: inline-table;">
                        <asp:TextBox ID="txtNF" runat="server" type="text" class="field text fn" value="" size="8" TabIndex="5"></asp:TextBox>
                    </div>

                    <label class="desc" for="txtAP" style="padding-right: 10px;">Nº AP</label>
                    <div style="display: inline-table;">
                        <asp:TextBox ID="txtAP" runat="server" type="text" class="field text fn" value="" size="8" TabIndex="5"></asp:TextBox>
                    </div>
                </div>

                <div id="divDPTP" align="left">

                    <label class="desc" for="txtStatus" style="padding-right: 10px;">Status</label>
                    <div style="display: inline-table;">
                        <select id="txtStatus" name="txtStatus" class="field select medium" tabindex="9" runat="server">
                            <option value="NULL">Todos</option>
                            <option value="True">Concluídos</option>
                        </select>
                    </div>


                    <label id="lblTipoDocumento" class="desc" for="txtTipoDocumento" style="padding-right: 10px;">Tipo</label>
                    <div style="display: inline-table;">
                        <select id="sltTipoDocumento" name="sltTipoDocumento" class="field select medium" tabindex="10" runat="server">
                            <option value="" selected=""></option>
                            <option value="ADIANTAMENTOS">ADIANTAMENTOS</option>
                            <option value="AMEX VIAGENS">AMEX VIAGENS</option>
                            <option value="AMEX CORPORATIVO">AMEX CORPORATIVO</option>
                            <option value="CTR">CONTRATO</option>
                            <option value="DANFE">DANFE</option>
                            <option value="DI">DOCUMENTO DE IMPORTAÇÃO</option>
                            <option value="FATURA">FATURA</option>
                            <option value="GUIA">GUIA</option>
                            <option value="INVOICE">INVOICE</option>
                            <option value="NF">NOTA FISCAL</option>
                            <option value="NOTA DE DEBITO">NOTA DE DEBITO</option>
                            <option value="PREST. CONTAS">PREST. CONTAS</option>
                            <option value="RECIBO">RECIBO</option>
                            <option value="CI">COMUNICAÇÃO INTERNA</option>
                        </select>
                    </div>

                    <label class="desc" for="txtCNPJ" style="padding-right: 10px;margin-top: 25px;">CNPJ</label>
                    <div style="display: inline-table;">
                        <input id="txtCNPJ" runat="server" name="txtCNPJ" class="field text fn" style="position: relative; z-index: 100;" value="" size="8" tabindex="5">
                    </div>

                </div>

                <asp:Button ID="btnBuscar" data-icon="check" data-role="button" runat="server" data-theme="b" Text="Buscar" OnClick="btnBuscar_Click" />

                <br />

                <div id="dados" runat="server"></div>

                <center>
                    <asp:Label ID="lblResposta" runat="server" Text=""></asp:Label>
                 </center>
            </form>
        </div>
    </div>

</body>
</html>
