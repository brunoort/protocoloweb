<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exibeResultado.aspx.cs" Inherits="ProtocoloWeb.exibeResultado" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.33242, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProtocoloWeb</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <script src="Scripts/functions.js"></script>

    <script src="Scripts/jquery.maskedinput.js"></script>
    <script src="Scripts/maskMoney.js"></script>
    <!-- User-generated css -->
    <style>
        body {
            background-color: #f8f8f8;
            padding: 0;
            margin: 0;
            font-family: 'trebuchet MS', 'Lucida sans', Arial;
        }

        .footable {
            font-size: 11px;
            width: 98%;
            color: black;
        }

        .modalPopup {
            background-color: #696969;
            color: white;
            text-align: left;
            border-radius: 10px;
        }

        .contentClass {
            margin: 0;
            width: 440px;
            max-height: 440px;
            overflow-x: hidden;
            padding-left: 20px;
        }

        .header {
            border: 1px solid #333;
            background: #111;
            background-image: url("Images/bgBarra.jpg");
            color: #fff;
            font-weight: bold;
            text-shadow: 0 -1px 1px #000;
        }

        h1 {
            min-height: 1.1em;
            text-align: center;
            font-size: 16px;
            display: block;
            margin: .6em 30% .8em;
            padding: 0;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            outline: 0!important;
            font-family: Helvetica,Arial,sans-serif;
            text-shadow: 0 -1px 1px #000;
        }

        th {
            font-weight: bold;
            font-size: 13px;
            padding: 3px;
        }

        td {
            padding: 3px;
        }
    </style>
    <script>
        function hidePopup() {
            document.getElementById("pnlDetails").style.display = "none";
            document.getElementById("popDetails_backgroundElement").style.display = "none";
        }
    </script>
    <!-- User-generated js -->
</head>
<body>
    <div data-role="page" id="page1">
        <div data-role="navbar">
            <ul>
                <img style="float: left;" src="Images/RecordMob.png" />
            </ul>
        </div>
        <br />
        <br />
        <div class="header">
            <a href="Default.aspx" target="_self">
                <img style="float: left" src="Images/menu.jpg" border="0" /></a>
            <h1>
                <div id="divNomePainel" runat="server"></div>
                <div id="divResult" runat="server"></div>
            </h1>
        </div>
        <br />

        <div data-role="content" align="center">
            <form id="form1" runat="server">
                <div id="dados" runat="server"></div>

                <div id="tabelaDados" runat="server"></div>

                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>


                <asp:GridView ID="GridView1" CssClass="footable" Width="98%" runat="server" AutoGenerateColumns="False" EnableViewState="True" OnRowCommand="GridView1_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="idFormulario" HeaderText="idFormulario" />
                        <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                        <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" />
                        <asp:BoundField DataField="Matriz" HeaderText="Matriz" />
                        <asp:BoundField DataField="NumNF" HeaderText="Nº NF" />
                        <asp:BoundField DataField="NumAP" HeaderText="Nº AP" />
                        <asp:BoundField DataField="NumPedido" HeaderText="Nº Pedido" />
                        <asp:BoundField DataField="dtVencimento" HeaderText="Vencimento" />
                        <asp:BoundField DataField="dtEmissaoDOC" HeaderText="Data Emissão" />
                        <asp:BoundField DataField="codJustificativa" HeaderText="Justificativa" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo" />
                        <asp:BoundField DataField="dataCadastro" HeaderText="Data Cadastro" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <div style="display: inline-table; width: 110px; max-width: 110px">
                                    <asp:LinkButton sclass="imgIco" ID="LinkButton1" runat="server" CommandArgument='<%# Eval("NumAP") %>' CommandName="cmd"><img class="imgBtn" width="25" height="25" src="Images/lupa.png"  alt="Visualizar Documento" title="Visualizar Documento" border="0"/></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="showDetails"><img src="Images/icon_detalhe.png" alt="Exibir Detalhes" title="Exibir Detalhes" width="25" height="25" border="0"/></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Exportar" OnClick="btnExport_Click" />

                <!-- INICIO POPUP SALVAR DETALHES -->
                <asp:Panel ID="pnlDetails" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div id="divDetails" runat="server">
                        <div data-role="content" data-theme="b" class="contentClass">
                            <div data-role="list-divider" data-mini="true" data-inset="true">
                                <h2 style="text-align: center;">Detalhes - ID:
                                    <asp:Label ID="lblIdFormulario" runat="server" Text="Label"></asp:Label>
                                </h2>
                            </div>
                            <div id="divDetalhe" style="font-size: 12px;" runat="server">

                                <asp:GridView ID="GridViewDetalhe" runat="server" AutoGenerateColumns="False" Width="95%">
                                    <Columns>
                                        <asp:BoundField DataField="idFormulario" HeaderText="ID" Visible="False"   />
                                        <asp:BoundField DataField="login" HeaderText="Login" />
                                        <asp:BoundField DataField="acao" HeaderText="Ação"  />
                                        <asp:BoundField DataField="area" HeaderText="Área" />
                                        <asp:BoundField DataField="dataAcao" HeaderText="Data"  />
                                    </Columns>
                                </asp:GridView>
                                <br/><br/>
                            </div>
                            <input type="button" value="Fechar" onclick="hidePopup()" />
                        </div>

                    </div>
                </asp:Panel>

                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="popDetails" runat="server" DropShadow="false" PopupControlID="pnlDetails" TargetControlID="lnkFake" BackgroundCssClass="modalBackground" ViewStateMode="Enabled"></cc1:ModalPopupExtender>

                <asp:Panel ID="pnlExport" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div id="divExportar" runat="server">
                    </div>
                </asp:Panel>

                <asp:Label ID="lblResposta" runat="server" Text=""></asp:Label>

                <div id="loadGridView" runat="server"></div>
            </form>
        </div>
    </div>
    <br />
    <br />
    <br />
</body>
</html>
