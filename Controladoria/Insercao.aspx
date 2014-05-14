<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insercao.aspx.cs" Inherits="ProtocoloWeb.Insercao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>Formulário</title>
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
    <script src="Scripts/maskMoney.js"></script>
    <link rel="stylesheet" href="Content/my.css" />
    <script>
        function liberarCampos() {
            
            var numAp = document.getElementById("txtAP").value;
            
            if (numAp == "") {
                
               
                
                document.getElementById("txtAP").removeAttribute("readonly", 0);
                document.getElementById("txtEmpresa").removeAttribute("readonly", 0);
                document.getElementById("txtCnpj").removeAttribute("readonly", 0);
                document.getElementById("sltMatriz").removeAttribute("readonly", 0);
                document.getElementById("txtNF").removeAttribute("readonly", 0);
                document.getElementById("txtDataVencimento").removeAttribute("readonly", 0);
                document.getElementById("txtValorNF").removeAttribute("readonly", 0);
                document.getElementById("txtDataEmissaoDoc").removeAttribute("readonly", 0);
                document.getElementById("txtDepartamento").removeAttribute("readonly", 0);
                

                //$("#txtEmpresa").removeAttr("readonly");
                //$("#txtCnpj").removeAttr("readonly");
            }
        }
        
        //function bloqueiaCampos() {
        //    $("#txtNumPedido").attr("readonly", "readonly");
        //    $("#txtEmpresa").attr("readonly", "readonly");
        //    $("#txtCnpj").attr("readonly", "readonly");
        //    $("#sltMatriz").attr("readonly", "readonly");
        //    $("#txtNF").attr("readonly", "readonly");
        //    $("#txtDataVencimento").attr("readonly", "readonly");
        //    $("#txtValorNF").attr("readonly", "readonly");
        //    $("#txtDataEmissaoDoc").attr("readonly", "readonly");
        //    $("#txtDepartamento").attr("readonly", "readonly");
        //}

    </script>

    <!-- User-generated css -->
    <style>
        .containing-element .ui-slider-switch {
            width: 9em;
        }

        .overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: #000;
            display: none;
            opacity: .5;
            filter: alpha(opacity=50);
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)";
        }

        .popup {
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
    <script>
    /*
        $(document).ready(function () {
            $.datepicker.setDefaults($.datepicker.regional['pt']);

            $("#txtDataVencimento").datepicker();
            $("#txtDataEmissaoDoc").datepicker();
            $("#txtContratoInicio").datepicker();
            $("#txtContratoFim").datepicker();
            $("#txtReceitaFederal").datepicker();
            $("#txtPrefeitura").datepicker();
            $("#txtCNAE").datepicker();
            $("#txtOptanteSimples").datepicker();
            $("#txtSintegra").datepicker();
            $("#txtJuntaComercial").datepicker();
            $("#txtCadastroMunicipal").datepicker();
            
            $("#txtDataVencimento").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtDataEmissaoDoc").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtContratoInicio").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtContratoFim").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtReceitaFederal").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtPrefeitura").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtCNAE").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtOptanteSimples").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtSintegra").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtJuntaComercial").datepicker("option", "dateFormat", "dd/mm/yy");
            $("#txtCadastroMunicipal").datepicker("option", "dateFormat", "dd/mm/yy");

        });
        */
    </script>
    <!-- User-generated js -->
</head>
<body>

    <div data-role="page" id="page1">
        <div data-role="navbar">
            <ul>
                <li>
                    <img style="float: left;" src="Images/RecordMob.png" />
                    <%--<a href="#" style="float:right;" data-role="button" data-mini="true" data-inline="true" data-theme="b">
                    <asp:Label ID="lblLogin" runat="server" />
                </a>--%>
                    <a href="#popupNested" data-rel="popup" style="float: right;" data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b" style="color: white;">
                        <asp:Label ID="lblLogin" runat="server" />
                    </a>
                </li>
            </ul>
        </div>
        <br />
        <div data-role="header" data-theme="a">
            <a href="Default.aspx" target="_self" data-icon="home" data-theme="b">Menu</a>
            <h1 style="color: white;">Formulário</h1>
        </div>
        <div data-role="content">
            <form id="form" runat="server">

                <div id="divMensagem" runat="server" align="center" style="color:red;font-size:14px;font-weight: bold;">
                    <asp:Label ID="lblMensagem" runat="server" Text="" Style="color: red; font-weight: bold; font-size: 16px;"></asp:Label>    
                </div>
                
                <div>
                    <label class="desc" for="txtAP">Nº da AP</label>
                    <div>
                        <input id="txtAP" name="txtAP" type="text" maxlength="20" class="field text fn" value="" size="8" tabindex="4" runat="server" onchange="bloqueiaCampos();"/>
                        <asp:Button ID="btnPesquisarAP" runat="server" Text="Pesquisar" data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b" Style="color: white;" OnClick="PesquisarAP_Click" />
                        <!--<input id="linkPesquisaAP" type="button" value="   Pesquisar  " data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b" style="color:white;" onclick="PesquisaAP()"/> 
                <div id="linkEditaAP" style="display:inline-table" runat="server"></div>-->
                    </div>
                </div>
                <div>
                    <label class="desc" for="txtNumPedido">Nº Pedido</label>
                    <div>
                        <input id="txtNumPedido" name="txtNumPedido" type="text" maxlength="20" class="field text fn" value="" size="8" tabindex="4" runat="server" onchange="liberarCampos()"/>
                    </div>
                </div>
                <div>
                    <label class="desc" for="txtEmpresa">Empresa</label>
                    <div>
                        <input id="txtEmpresa" name="txtEmpresa" type="text" maxlength="100" class="field text fn" value="" size="8" tabindex="1" runat="server" readonly="readonly"/>
                    </div>
                </div>

                <div>
                    <label class="desc" for="txtCnpj">CNPJ</label>
                    <div>
                        <input id="txtCnpj" name="txtCnpj" type="text" maxlength="25" class="field text fn" value="" size="8" tabindex="1" runat="server"  readonly="readonly" />
                    </div>
                </div>

                <div>
                    <label class="desc" for="txtCnpj">Matriz</label>
                    <div>
                        <input id="sltMatriz" name="sltMatriz" type="text" maxlength="25" class="field text fn" value="" size="8" tabindex="1" runat="server"  readonly="readonly"/>
                    </div>
                </div>
                <!--
          <div>
            <label for="x" class="select">Matriz</label>
            <select id="x" name="x" data-mini="true" data-inline="true" runat="server" >
                <option value=""></option>
                <option value="RE01">BARRA-FUNDA</option>
                <option value="RN">RECNOV</option>
                <option value="RP">PAULISTA</option>
                <option value="R7">R7</option>
            </select>
          </div>
        -->

                <div>
                    <label class="desc" for="txtEmpresa">Nº NF/Recibo</label>
                    <div>
                        <input id="txtNF" name="txtNF" type="text" maxlength="20" class="field text fn" value="" size="8" tabindex="3" runat="server" />
                    </div>
                </div>

                <div>
                    <label class="desc" for="txtDataVencimento">Data Vencimento</label>
                    <div>
                        <input id="txtDataVencimento" name="txtDataVencimento" class="field text fn" maxlength="20" style="position: relative; z-index: 100;" size="8" tabindex="5" runat="server"  readonly="readonly"/>
                    </div>
                </div>

                <div>
                    <label class="desc" for="txtValorNF">Valor NF</label>
                    <div>
                        <input id="txtValorNF" name="txtValorNF" type="text" class="field text fn" value="" size="8" tabindex="6" maxlength="20" onkeydown="verificaValor();" runat="server"  readonly="readonly"/>
                    </div>
                </div>

                <div>
                    <label class="desc" for="txtDataEmissaoDoc">Data Emissão DOC</label>
                    <div>
                        <input id="txtDataEmissaoDoc" name="txtDataEmissaoDoc" style="position: relative; z-index: 100;" maxlength="20" class="field text fn" size="8" tabindex="7" runat="server"  readonly="readonly"/>
                    </div>
                </div>
                
                <div>
                    <label class="desc" for="txtJustificativa">Justificativa</label>
                    <div>
                        <input id="txtJustificativa" name="txtJustificativa" style="position: relative; z-index: 100;" maxlength="130" class="field text fn" size="8" tabindex="7" runat="server" />
                    </div>
                </div>
                <!--
           <div>
            <label class="desc"  for="sltCodJustificativa">
    	        Justificativa
            </label>
            <div>
            <select id="sltCodJustificativa" name="sltCodJustificativa" class="field select medium" tabindex="8" runat="server" > 
                <option value="" selected=""></option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
                <option value="6">6</option>
                <option value="7">7</option>
                <option value="8">8</option>
            </select>
            </div>
          </div>
            -->
                <div>
                    <label class="desc" for="txtDepartamento">Departamento</label>
                    <div>
                        <input id="txtDepartamento" name="txtDepartamento" style="position: relative; z-index: 100;" class="field text fn" value="" maxlength="100" size="8" tabindex="7" runat="server"  readonly="readonly"/>
                    </div>
                </div>

                <div>
                    <label class="desc" for="sltTipoDocumento">
                        Tipo Documento
                    </label>
                    <div>
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
                </div>

                <div>
                    <label class="desc" for="txtEmailResponsavel">E-mail Responsável</label>
                    <div>
                        <input id="txtEmailResponsavel" name="txtEmailResponsavel" style="position: relative; z-index: 100;" class="field text fn" value="" maxlength="100" size="8" tabindex="7" runat="server" onchange="validaEmail(this.value)"/>
                    </div>
                </div>
                
                <asp:Button ID="Button1" data-icon="check" data-role="button" runat="server" OnClick="Button1_Click" Text=" Incluir " data-theme="b" />

                <div data-role="popup" id="popupFiles" data-theme="none" style="z-index: 101">
                    <div data-role="content" data-theme="b" style="margin: 0; max-width: 500px;">
                        <div data-role="list-divider" data-mini="true" data-inset="true">
                            <div id="dados" runat="server" style="width: 500px;"></div>
                        </div>
                    </div>
                </div>

            </form>
            <div id="divFunction" runat="server"></div>

            <div data-role="popup" id="popupNested" data-theme="none">
                <div data-role="content" data-theme="b" style="margin: 0; max-width: 200px;">
                    <div data-role="list-divider" data-mini="true" data-inset="true">
                        <h2 style="text-align: center;">LogOff</h2>
                        <div data-role="content" data-theme="b" class="ui-corner-bottom ui-content">
                            <h3 style="text-align: center;" class="ui-title">Tem certeza que deseja fazer o Log Off?</h3>
                            <div style="text-align: center; width: 150px;">
                                <a href="#" data-role="button" data-mini="true" data-inline="true" data-rel="back" data-theme="c">Não</a>
                                <a href="Login.aspx?logout='Y'" target="_self" data-role="button" data-mini="true" data-inline="true" data-transition="flow" data-theme="a">Sim</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</body>
</html>
