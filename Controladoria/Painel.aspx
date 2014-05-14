<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Painel.aspx.cs" Inherits="ProtocoloWeb.Painel" EnableEventValidation="false" %>

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
    <script> 

        jQuery(function ($) {
            $("#txtContratoInicio_edit").mask("99/99/9999");
            $("#txtContratoFim_edit").mask("99/99/9999");

            $("#txtReceitaFederal_edit").mask("99/99/9999");
            $("#txtPrefeitura_edit").mask("99/99/9999");
            $("#txtCNAE_edit").mask("99/99/9999");
            $("#txtOptanteSimples_edit").mask("99/99/9999");
            $("#txtSintegra_edit").mask("99/99/9999");
            $("#txtJuntaComercial_edit").mask("99/99/9999");
            $("#txtCadastroMunicipal_edit").mask("99/99/9999");
        });


        function showDateTimeNow(pInput) {

            //alert(pInput);
            var input = document.getElementById(pInput.name).value;
            //var input = $("#" + pInput).val();

            var date = new Date();

            var day = "";
            if (date.getDate() < 10) {
                day = "0" + date.getDate();
            } else {
                day = date.getDate();
            }

            var month = "";
            if (date.getMonth() < 10) {
                month = "0" + (date.getMonth() + 1);
            } else {
                month = (date.getMonth() + 1);
            }

            var dataHoje = day + '/' + month + '/' + date.getFullYear();
            document.getElementById(pInput.name).value = dataHoje;
            //$("#" + pInput).val(dataHoje);
        }

        function limpaForm() {
            document.getElementById("txtNumPedido").value = "";
            document.getElementById("txtEmpresa").value = "";
            document.getElementById("txtCnpj").value = "";
            document.getElementById("sltMatriz").value = "";
            document.getElementById("txtCnpj").value = "";
            document.getElementById("sltMatriz").value = "";
            document.getElementById("txtNF").value = "";
            document.getElementById("txtDataVencimento").value = "";
            document.getElementById("txtValorNF").value = "";
            document.getElementById("txtDataEmissaoDoc").value = "";
            document.getElementById("txtJustificativa").value = "";
            document.getElementById("txtDepartamento").value = "";
            document.getElementById("txtEmailResponsavel").value = "";
        }



    </script>
    <!-- FooTable -->
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
            outline: 0 !important;
            font-family: Helvetica,Arial,sans-serif;
            text-shadow: 0 -1px 1px #000;
        }

        th {
            font-weight: bold;
            font-size: 12px;
            padding: 3px;
        }

        td {
            padding: 3px;
        }
    </style>
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

                
                <asp:GridView ID="GridView1" CssClass="footable" Width="98%" runat="server" AutoGenerateColumns="False" EnableViewState="True" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="Reprovado">
                    <Columns>
                        <asp:BoundField DataField="idFormulario" HeaderText="ID" />
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
                        
                        <asp:TemplateField HeaderText="Valor">
                            <ItemTemplate>
                                <% if (Session["pAdministrador"].ToString() == "True") { %>
                                        R$ <%# Eval("valorNF").ToString().Replace('.',',') %>
                                <% } else { %>
                                      -
                                <% }%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Aprovar">
                            <ItemTemplate>
                                <img id="certoIco" src="Images/certoIco.jpg" width="10" height="10" />
                                <asp:CheckBox ID="CheckBoxAprova" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxAprova_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reprovar">
                            <ItemTemplate>
                                <img id="erradoIco" src="Images/erradoIco.jpg" width="10" height="10" />
                                <asp:CheckBox ID="CheckBoxReprova" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxReprova_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <div style="display: inline-table; width: 110px; max-width: 110px">

                                    <asp:LinkButton ID="lnkEdit" runat="server" OnClick="ShowJustificativas"><img src="Images/red_card.png" alt="Ver Justificativas" title="Ver Justificativas" width="25" height="25" border="0"/></asp:LinkButton>
                                    <a id="linkEnviaEmail" href="mailto:<%# Eval("emailSolicitante") %>?subject=Notificação - Protocolo Web" target="_blank">
                                        <img class="imgBtn" width="30" height="30" src="Images/emailIco.png" alt="Enviar E-mail" title="Enviar E-mail" border="0" /></a>

                                    <asp:LinkButton sclass="imgIco" ID="LinkButton1" runat="server" CommandArgument='<%# Eval("NumAP") %>' CommandName="cmd"><img class="imgBtn" width="25" height="25" src="Images/lupa.png"  alt="Visualizar Documento" title="Visualizar Documento" border="0"/></asp:LinkButton>

                                    <%
                                        string vIdTipo = Request.QueryString["idtipo"];
                                        if (vIdTipo == "1" || vIdTipo == "2" || vIdTipo == "4" || vIdTipo == "5" || vIdTipo == "7")
                                        {
                                    %>
                                    <asp:LinkButton ID="LinkCarimbo" runat="server" OnClick="showCarimbo"><img src="Images/carimbo.png" alt="Carimbo" title="Carimbo" width="25" height="25" border="0"/></asp:LinkButton>
                                    <% 
                                        } 
                                    %>
                                    <%
                                        if (vIdTipo == "1" || vIdTipo == "2" || vIdTipo == "3" || vIdTipo == "4" || vIdTipo == "5" || vIdTipo == "6" || vIdTipo == "7")
                                        {
                                    %>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="showEditar"><img src="Images/editar.png" alt="Editar Formulário" title="Editar Formulário" width="25" height="25" border="0"/></asp:LinkButton>
                                    <% 
                                        }

                                        if (vIdTipo == "1" || vIdTipo == "2" || vIdTipo == "5" || vIdTipo == "7")
                                        {
                                    %>
                                    <a href="http://10.10.13.13/sistemas/ap/auto_pagto_editar.asp?cod=<%# Eval("NumAP") %>" target="_blank">
                                        <img src="Images/icone_ap.png" alt="Editar AP" title="Editar AP" width="25" height="25" border="0" /></a>
                                    <% 
                                        }

                                        //if (vIdTipo == "1")
                                        if (Session["pExclusao"].ToString() == "True")
                                        {
                                    %>
                                    <asp:LinkButton ID="LinkExcluir" runat="server" OnClick="ExcluirProtocolo"><img src="Images/icon_excluir.png" alt="Excluir Protocolo" title="Excluir Protocolo" width="25" height="25" border="0"/></asp:LinkButton>
                                    <%
                                        } 
                                    %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Reprovado" HeaderText="Reprovado" Visible="False" />
                    </Columns>
                </asp:GridView>

                



                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Exportar" OnClick="btnExport_Click" />

                <asp:Panel ID="pnlExport" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div id="divExportar" runat="server">
                    </div>
                </asp:Panel>

                <!-- INICIO POPUP REPROVA -->
                <asp:Panel ID="pnlJustificativas" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div data-role="content" data-theme="b" class="contentClass">
                        <div data-role="list-divider" data-mini="true" data-inset="true">
                            <h2 style="text-align: center;">Justificativa ID:
                                <asp:Label ID="lblIdFormulario" runat="server" Text=""></asp:Label></h2>
                            <div data-role="content" data-theme="b" class="ui-corner-bottom ui-content">
                                <div id="divGridJustificativas">
                                    <br />
                                    <br />
                                    <asp:GridView ID="GridView2" CssClass="footable" runat="server" AutoGenerateColumns="False" EnableViewState="False" Style="background: white;">
                                        <Columns>
                                            <asp:BoundField DataField="Justificativa" HeaderText="Justificativa" />
                                            <asp:BoundField DataField="dtJustificativa" HeaderText="Data" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <input type="button" value="Fechar" onclick="hidePopup()" />
                        </div>
                    </div>
                </asp:Panel>
                <!-- FIM POPUP REPROVA -->

                <!-- INICIO POPUP SALVAR JUSTIFICATIVAS -->
                <asp:Panel ID="pnlSalvaJustificativa" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div id="divJustificativas" runat="server">
                        <div data-role="content" data-theme="b" class="contentClass">
                            <div data-role="list-divider" data-mini="true" data-inset="true">
                                <h2 style="text-align: center;">Justificativa ID:
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h2>
                                <div data-role="content" data-theme="b" class="ui-corner-bottom ui-content">
                                    <div id="divJustificativa" runat="server">
                                        <h4 style="text-align: center;" class="ui-title">Insira a justificativa para a Reprovação</h4>
                                        <asp:TextBox ID="txtJustificativa" runat="server" Rows="6" Height="114px" MaxLength="2000" TextMode="MultiLine" Width="340px"></asp:TextBox>
                                        <div style="text-align: center; width: 400px;">
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandArgument='<%# Eval("idFormulario") %>' OnClick="salvaJustificativa" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <!-- FIM POPUP SALVAR JUSTIFICATIVAS -->

                <!-- INICIO POPUP CARIMBO -->
                <asp:Panel ID="pnlCarimbo" runat="server" CssClass="modalPopup" Style="display: none; width: 470px">
                    <div data-role="content" data-theme="b" class="contentClass">
                        <div data-role="list-divider" data-mini="true" data-inset="true">
                            <h2 style="text-align: center;">ID:
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h2>
                            <asp:HiddenField ID="HiddenFieldMatriz" runat="server" />
                            <div id="Formulario" runat="server">



                                <div id="divMensagem" align="center">
                                    <asp:Label ID="lblMensagem" runat="server" Text="" Style="color: red; font-weight: bold; font-size: 16px;"></asp:Label>
                                </div>

                                <br />

                                <!-- Não pode ser bloqueada -->
                                <div>
                                    <label class="desc" for="txtValorNF_carimbo">Valor NF</label>
                                    <div>
                                        <input id="txtValorNF_carimbo" name="txtValorNF_carimbo" type="text" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="6" maxlength="20" runat="server" readonly="readonly" />
                                    </div>
                                </div>

                                <br />

                                <div>
                                    <label class="desc" for="txtIRpct_edit">IR (%)</label>
                                    <div>
                                        <input id="txtIRpct_edit" name="txtIRpct_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaPorcentagem('txtValorNF_carimbo',this.value,'txtIRvalor_edit')" value="0" onkeypress="return validate(event)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtIRbase_edit">Base de cálculo (IR)</label>
                                    <div>
                                        <input id="txtIRbase_edit" name="txtIRbase_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaBase(this.value,'txtIRpct_edit','txtIRvalor_edit')" value="0" onkeypress="return validate(event)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtIRvalor_edit">IR (valor)</label>
                                    <div>
                                        <input id="txtIRvalor_edit" name="txtIRvalor_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onkeyup="javascript:somente_numero(this);" />
                                    </div>
                                </div>


                                <br />

                                <div>
                                    <label class="desc" for="txtPCCpct_edit">PCC (%)</label>
                                    <div>
                                        <input id="txtPCCpct_edit" name="txtPCCpct_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaPorcentagem('txtValorNF_carimbo',this.value,'txtPCCvalor_edit')" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtPCCbase_edit">Base de cálculo (PCC)</label>
                                    <div>
                                        <input id="txtPCCbase_edit" name="txtPCCbase_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaBase(this.value,'txtPCCpct_edit','txtPCCvalor_edit')" value="0" onkeypress="return validate(event)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtPCCvalor_edit">PCC (valor)</label>
                                    <div>
                                        <input id="txtPCCvalor_edit" name="txtPCCvalor_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <br />

                                <div>
                                    <label class="desc" for="txtISSpct_edit">ISS (%)</label>
                                    <div>
                                        <input id="txtISSpct_edit" name="txtISSpct_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaPorcentagem('txtValorNF_carimbo',this.value,'txtISSvalor_edit')" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtISSbase_edit">Base de cálculo (ISS)</label>
                                    <div>
                                        <input id="txtISSbase_edit" name="txtISSbase_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaBase(this.value,'txtISSpct_edit','txtISSvalor_edit')" value="0" onkeypress="return validate(event)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtISSvalor_edit">ISS (valor)</label>
                                    <div>
                                        <input id="txtISSvalor_edit" name="txtISSvalor_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <br />

                                <div>
                                    <label class="desc" for="txtINSSpct_edit">INSS (%)</label>
                                    <div>
                                        <input id="txtINSSpct_edit" name="txtINSSpct_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaPorcentagem('txtValorNF_carimbo',this.value,'txtINSSvalor_edit')" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtINSSbase_edit">Base de cálculo (INSS)</label>
                                    <div>
                                        <input id="txtINSSbase_edit" name="txtINSSbase_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onblur="calculaBase(this.value,'txtINSSpct_edit','txtINSSvalor_edit')" value="0" onkeypress="return validate(event)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtINSSvalor_edit">INSS (valor)</label>
                                    <div>
                                        <input id="txtINSSvalor_edit" name="txtINSSvalor_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>

                                <br />

                                <!--
                                <br/>

                                <div style="display: inline">
                                    <table width="90%">
                                        <tr>
                                            <td>Vl. Base Calc. PIS :<input id="VlCalcBase" name="VlCalcBase" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0,00" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" onblur="calculaBase()" /></td>
                                            <td>Vl. PIS : <input id="VlPis" name="VlPis" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0,00" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" onblur="calculaBase()" /></td>
                                            <td>Aliq. PIS: <input id="Text1" name="Aliq_PIS" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="0,00" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" /></td>
                                        </tr>
                                    </table>
                                </div>
                               
                                <br/>
                                -->

                                <div>
                                    <label class="desc" for="txtLiquido_edit">LÍQUIDO</label>
                                    <div>
                                        <input id="txtLiquido_edit" name="txtLiquido_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="15" size="8" tabindex="7" runat="server" onkeypress="return keypressed( this , event );" />
                                    </div>
                                </div>


                                <div>
                                    <label class="desc" for="txtCadastroMunicipal_edit">CADASTRO MUNICIPAL</label>
                                    <div>
                                        <input id="txtCadastroMunicipal_edit" name="txtCadastroMunicipal_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtCodRetencaoISS_edit">COD. RETENÇÃO ISS</label>
                                    <div>
                                        <input id="txtCodRetencaoISS_edit" name="txtCodRetencaoISS_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" maxlength="15" size="8" tabindex="7" runat="server" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtPCCnfs_edit">PCC S/NFS</label>
                                    <div>
                                        <input id="txtPCCnfs_edit" name="txtPCCnfs_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" maxlength="45" size="8" tabindex="7" runat="server" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtCFOP_edit">CFOP</label>
                                    <div>
                                        <input id="txtCFOP_edit" name="txtCFOP_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" maxlength="15" size="8" tabindex="7" runat="server" />
                                    </div>
                                </div>

                                <br />

                                <div>
                                    <label class="desc">Categoria NF</label>
                                    <div>
                                        <fieldset data-role="controlgroup" class="tamanhoCB" style="width: 90%;">
                                            <input type="checkbox" name="categoriaNFZA" id="categoriaNFZA" runat="server" />
                                            <label for="categoriaNFZA">ZA</label>
                                            <input type="checkbox" name="categoriaNFZS" id="categoriaNFZS" runat="server" />
                                            <label for="categoriaNFZS">ZS</label>
                                            <input type="checkbox" name="categoriaNFNT" id="categoriaNFNT" runat="server" />
                                            <label for="categoriaNFNT">NT</label>
                                        </fieldset>
                                    </div>
                                </div>
                                <div>
                                    <label class="desc" for="txtLote">Lote</label>
                                    <div>
                                        <input id="txtLote" name="txtLote" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>
                                <div>
                                    <label class="desc" for="txtCodServico">Código do Serviço</label>
                                    <div>
                                        <input id="txtCodServico" name="txtCodServico" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtValorDeducao">Valor da Dedução</label>
                                    <div>
                                        <input id="txtValorDeducao" name="txtValorDeducao" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="20" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtAliquota">Aliquota</label>
                                    <div>
                                        <input id="txtAliquota" name="txtAliquota" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="20" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtTipoDocumento">Tipo de Documento</label>
                                    <div>
                                        <input id="txtTipoDocumento" name="txtTipoDocumento" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtTributacaoServico">Tributação de Serviço</label>
                                    <div>
                                        <input id="txtTributacaoServico" name="txtTributacaoServico" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtCodPrestador">Código do Prestador</label>
                                    <div>
                                        <input id="txtCodPrestador" name="txtCodPrestador" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtAtribuicaoISS">Atribuição do ISS</label>
                                    <div>
                                        <input id="txtAtribuicaoISS" name="txtAtribuicaoISS" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtSituacaoNTFS">Situação da NTFS</label>
                                    <div>
                                        <input id="txtSituacaoNTFS" name="txtSituacaoNTFS" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtISSRet">ISS Retido</label>
                                    <div>
                                        <input id="txtISSRet" name="txtISSRet" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <div>
                                    <label class="desc" for="txtRegTributacao">Regime de Tributação</label>
                                    <div>
                                        <input id="txtRegTributacao" name="txtRegTributacao" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>

                                <!-- CAMPOS RJ -->
                                <div id="Carimbo1" style="display: none" runat="server">
                                    <label class="desc" for="txtOptSimples">Opt. Simples</label>
                                    <div>
                                        <input id="txtOptSimples" name="txtOptSimples" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="12" />
                                    </div>
                                </div>
                                <div id="Carimbo2" style="display: none" runat="server">
                                    <label class="desc" for="txtTipoNFConv">Tipo NF Conv.</label>
                                    <div>
                                        <input id="txtTipoNFConv" name="txtTipoNFConv" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>
                                <div id="Carimbo3" style="display: none" runat="server">
                                    <label class="desc" for="txtCodServicoMun">Cod. Serv. Mun</label>
                                    <div>
                                        <input id="txtCodServicoMun" name="txtCodServicoMun" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>
                                <div id="Carimbo4" style="display: none" runat="server">
                                    <label class="desc" for="txtTipoTribServico">Tp. Trib. Serv.</label>
                                    <div>
                                        <input id="txtTipoTribServico" name="txtTipoTribServico" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" maxlenght="50" />
                                    </div>
                                </div>
                                <!-- FIM CAMPOS RJ -->

                                <asp:Button ID="Button1" runat="server" Text="Salvar" OnClick="salvarCarimbo" />
                                <input type="button" value="Cancelar" onclick="hidePopup()" />

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!-- FIM POPUP CARIMBO -->

                <!-- INICIO POPUP EDITAR -->
                <asp:Panel ID="pnlEditar" runat="server" CssClass="modalPopup" Style="display: none; width: 470px;">
                    <div data-role="content" data-theme="b" class="contentClass">
                        <div data-role="list-divider" data-mini="true" data-inset="true">

                            <asp:HiddenField ID="idEditarForm" runat="server" />
                            <div id="divEditar" runat="server"></div>

                            <br />
                            <label class="desc" for="txtAP">Nº da AP</label>
                            <div>
                                <input id="txtAP_edit" name="txtAP_edit" type="text" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="4" runat="server" disabled="True" />
                            </div>



                            <label class="desc" for="txtEmpresa">Empresa</label>
                            <div>
                                <input id="txtEmpresa_edit" name="txtEmpresa_edit" type="text" maxlength="100" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="1" runat="server" disabled="True" />
                            </div>


                            <div>
                                <label class="desc" for="txtCnpj">CNPJ</label>
                                <div>
                                    <input id="txtCnpj_edit" name="txtCnpj_edit" type="text" maxlength="25" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="1" runat="server" disabled="True" />
                                </div>
                            </div>

                            <div>
                                <label for="sltMatriz_edit" class="select">Matriz</label>
                                <select id="sltMatriz_edit" name="sltMatriz_edit" data-mini="true" data-inline="true" runat="server" style="width: 90%" disabled="True">
                                    <option value=""></option>
                                    <option value="RE01">BARRA-FUNDA</option>
                                    <option value="RN">RECNOV</option>
                                    <option value="RP">PAULISTA</option>
                                    <option value="R7">R7</option>
                                </select>
                            </div>

                            <div>
                                <label class="desc" for="txtNF_edit">Nº NF/Recibo</label>
                                <div>
                                    <input id="txtNF_edit" name="txtNF_edit" type="text" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="3" runat="server" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtDataVencimento_edit">Data Vencimento</label>
                                <div>
                                    <input id="txtDataVencimento_edit" name="txtDataVencimento_edit" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="5" runat="server" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtValorNF_edit">Valor NF</label>
                                <div>
                                    <input id="txtValorNF_edit" name="txtValorNF_edit" type="text" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="6" maxlength="20" runat="server" disabled="True" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtDataEmissaoDoc_edit">Data Emissão DOC</label>
                                <div>
                                    <input id="txtDataEmissaoDoc_edit" name="txtDataEmissaoDoc_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="7" runat="server" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="sltCodJustificativa_edit">
                                    Justificativa
                                </label>
                                <div>
                                    <input id="txtJustificativa_edit" name="txtJustificativa_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="7" maxlength="250" runat="server" />
                                    <!--
                                    <select id="sltCodJustificativa_edit" name="sltCodJustificativa_edit" class="field select medium" tabindex="8" runat="server">
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
                                    -->
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtDepartamento_edit">Departamento</label>
                                <div>
                                    <input id="txtDepartamento_edit" name="txtDepartamento_edit" maxlength="100" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="7" runat="server" disabled="True" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="sltTipoDocumento_edit">
                                    Tipo Documento
                                </label>
                                <div>
                                    <select id="sltTipoDocumento_edit" name="sltTipoDocumento_edit" class="field select medium" tabindex="10" runat="server">
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
                                <label class="desc" for="txtDocumento_edit">Documento</label>
                                <div>
                                    <input runat="server" id="txtDocumento_edit" name="txtDocumento_edit" maxlength="200" type="text" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="" size="8" tabindex="11" runat="server" />
                                </div>
                            </div>


                            <div>
                                <label class="desc" for="sltVinculo_edit">
                                    VÍNCULO
                                </label>
                                <div>
                                    <select id="sltVinculo_edit" name="sltVinculo_edit" class="field select medium" tabindex="10" runat="server">
                                        <option value="CONTRATO">CONTRATO</option>
                                        <option value="ESPORÁDICO">ESPORÁDICO</option>
                                    </select>
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="sltTipoContrato_edit">
                                    TIPO CONTRATO
                                </label>
                                <div>
                                    <select id="sltTipoContrato_edit" name="sltTipoContrato_edit" class="field select medium" tabindex="10" runat="server">
                                        <option value="FIXO">FIXO</option>
                                        <option value="VARIÁVEL">VARIÁVEL</option>
                                    </select>
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtContratoInicio_edit">Se "CONTRATO" DE</label>
                                <div>
                                    <input id="txtContratoInicio_edit" name="txtContratoInicio_edit" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" size="8" tabindex="7" runat="server" value="01/01/2013" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtContratoFim_edit">Se "CONTRATO" ATÉ</label>
                                <div>
                                    <input id="txtContratoFim_edit" name="txtContratoFim_edit" style="position: relative; z-index: 100; width: 90%;" maxlength="20" class="field text fn" size="8" tabindex="7" runat="server" value="01/01/2013" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtReceitaFederal_edit">RECEITA FEDERAL</label>
                                <div>
                                    <input id="txtReceitaFederal_edit" name="txtReceitaFederal_edit" style="position: relative; z-index: 100; width: 90%;" maxlength="20" class="field text fn" size="8" tabindex="7" runat="server" value="01/01/2013" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtPrefeitura_edit">PREFEITURA</label>
                                <div>
                                    <input id="txtPrefeitura_edit" name="txtPrefeitura_edit" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtCNAE_edit">CNAE</label>
                                <div>
                                    <input id="txtCNAE_edit" name="txtCNAE_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="20" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="sltOptanteSimples_edit">
                                    OPTANTE/SIMPLES
                                </label>
                                <div>
                                    <select id="sltOptanteSimples_edit" name="sltOptanteSimples_edit" class="field select medium" tabindex="10" runat="server">
                                        <option value="False">NÃO</option>
                                        <option value="True">SIM</option>
                                    </select>
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtOptanteSimples_edit">OPTANTE/SIMPLES</label>
                                <div>
                                    <input id="txtOptanteSimples_edit" name="txtOptanteSimples_edit" maxlength="20" style="position: relative; z-index: 100; width: 90%;" class="field text fn" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtSintegra_edit">SINTEGRA</label>
                                <div>
                                    <input id="txtSintegra_edit" name="txtSintegra_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="20" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtJuntaComercial_edit">JUNTA COMERCIAL</label>
                                <div>
                                    <input id="txtJuntaComercial_edit" name="txtJuntaComercial_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="20" value="01/01/2013" size="8" tabindex="7" runat="server" onclick="showDateTimeNow(this)" />
                                </div>
                            </div>

                            <div>
                                <label class="desc" for="txtEmailResponsavel_edit">Email Responsável</label>
                                <div>
                                    <input id="txtEmailResponsavel_edit" name="txtEmailResponsavel_edit" style="position: relative; z-index: 100; width: 90%;" class="field text fn" maxlength="50" size="8" tabindex="7" runat="server" onchange="validaEmail(this.value)" />
                                </div>
                            </div>

                            <br />
                            <br />
                            <asp:Button ID="Button2" data-icon="check" data-role="button" runat="server" Text=" Salvar " data-theme="b" OnClick="SalvarEditar" />
                            <input type="button" value="Cancelar" onclick="hidePopup()" />
                        </div>
                    </div>
                </asp:Panel>
                <!-- FIM POPUP EDITAR -->

                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="popJustificativas" runat="server" DropShadow="false" PopupControlID="pnlJustificativas" TargetControlID="lnkFake" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                <cc1:ModalPopupExtender ID="popSalvaJustificativa" runat="server" DropShadow="false" PopupControlID="pnlSalvaJustificativa" TargetControlID="lnkFake" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                <cc1:ModalPopupExtender ID="popCarimbo" runat="server" DropShadow="false" PopupControlID="pnlCarimbo" TargetControlID="lnkFake" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                <cc1:ModalPopupExtender ID="popEditar" runat="server" DropShadow="false" PopupControlID="pnlEditar" TargetControlID="lnkFake" BackgroundCssClass="modalBackground" ViewStateMode="Enabled"></cc1:ModalPopupExtender>

                <triggers>
                        <asp:AsyncPostBackTrigger ControlID = "GridView1" EventName="showEditar" />
                        </triggers>




                <div id="loadGridView" runat="server"></div>

                <center>
                            <asp:Label ID="lblResposta" runat="server" Text=""></asp:Label>
                        </center>
            </form>
        </div>
    </div>

</body>
</html>
