<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editarFormulario.aspx.cs" Inherits="ProtocoloWeb.editarFormulario" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProtocoloWeb</title>
    <meta charset="utf-8" />
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />

        <script src="Scripts/functions.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
        <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />        
        <script src="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.js"></script>
        <link rel="stylesheet" href="Content/my.css" />

       <!-- FooTable -->
        <meta name="viewport" content = "width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
        <link href="Styles/footable-0.1.css" rel="stylesheet" type="text/css" />
        <script src="Scripts/data-generator.js" type="text/javascript"></script>
        <script src="Scripts/footable.js" type="text/javascript"></script>
        <!-- FooTable -->
        
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
            </li>
        </ul>
    </div>
 <div data-role="content">
     <h2 style="text-align:center;" >ID: <asp:Label ID="lblIdFormulario" runat="server" Text=""></asp:Label></h2>
     <div id="Formulario" runat="server">    
      <form id="form" runat="server">
          
          <div id="divMensagem" align="center">
            <asp:Label ID="lblMensagem" runat="server" Text="" Style="color:red;font-weight: bold;font-size: 16px;"></asp:Label>
          </div>
          
          <div>
            <label class="desc" for="txtAP">Nº da AP</label>
            <div>
                <input id="txtAP" name="txtAP" type="text" maxlength="20" class="field text fn" value="" size="8" tabindex="4" runat="server" />
            </div>
          </div>
          
          <div>
            <label class="desc" for="txtEmpresa">Empresa</label>
            <div>
                <input id="txtEmpresa" name="txtEmpresa" type="text" maxlength="100" class="field text fn" value="" size="8" tabindex="1" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtCnpj">CNPJ</label>
            <div>
                <input id="txtCnpj" name="txtCnpj" type="text" maxlength="25" class="field text fn" value="" size="8" tabindex="1" runat="server"   />
            </div>
          </div>
    
          <div>
            <label for="sltMatrizz" class="select">Matriz</label>
            <select id="sltMatriz" name="sltMatriz" data-mini="true" data-inline="true" runat="server"  >
                <option value=""></option>
                <option value="RE01">BARRA-FUNDA</option>
                <option value="RN">RECNOV</option>
                <option value="RP">PAULISTA</option>
                <option value="R7">R7</option>
            </select>
          </div>
    
          <div>
            <label class="desc"  for="txtNF">Nº NF/Recibo</label>
            <div>
                <input id="txtNF" name="txtNF" type="text" maxlength="20" class="field text fn" value="" size="8" tabindex="3" runat="server"   />
            </div>
          </div>
    
           <div>
            <label class="desc" for="txtDataVencimento">Data Vencimento</label>
            <div>
                <input id="txtDataVencimento" name="txtDataVencimento" maxlength="20" class="field text fn" style="position: relative;z-index: 100;" value="" size="8" tabindex="5" runat="server"   />
            </div>
          </div>
    
           <div>
            <label class="desc" for="txtValorNF">Valor NF</label>
            <div>
                <input id="txtValorNF" name="txtValorNF"  type="text" class="field text fn" value="" size="8" tabindex="6" maxlength="20" onkeydown="verificaValor();" onKeyPress="return(MascaraMoeda(this,'.',',',event));" runat="server"   />
            </div>
          </div>
    
           <div>
            <label class="desc" for="txtDataEmissaoDoc">Data Emissão DOC</label>
            <div>
                <input id="txtDataEmissaoDoc" name="txtDataEmissaoDoc" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
    
           <div>
            <label class="desc"  for="sltCodJustificativa">
    	        Justificativa
            </label>
            <div>
            <select id="sltCodJustificativa" name="sltCodJustificativa" class="field select medium" tabindex="8" runat="server"  > 
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
  
          <div>
            <label class="desc" for="txtDepartamento">Departamento</label>
            <div>
                <input id="txtDepartamento" name="txtDepartamento" style="position: relative;z-index: 100;" maxlength="100" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
   
          <div>
            <label class="desc"  for="sltTipoDocumento">
    	        Tipo Documento
            </label>
            <div>
            <select id="sltTipoDocumento" name="sltTipoDocumento" class="field select medium" tabindex="10" runat="server"  > 
                <option value="" selected=""></option>
                <option value="ADIANTAMENTOS">ADIANTAMENTOS</option>
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
                <label class="desc" for="txtDocumento">Documento</label>
                <div>
                    <input runat="server" id="txtDocumento" name="txtDocumento" maxlength="200" type="text" class="field text fn" value="" size="8" tabindex="11" style="float:left;display:inline-table" runat="server" />
                </div>
            </div>
            
              
             <div>
            <label class="desc"  for="sltVinculo">
    	        VÍNCULO
            </label>
            <div>
            <select id="sltVinculo" name="sltVinculo" class="field select medium" tabindex="10" runat="server"  > 
                <option value="" selected=""></option>
                <option value="CONTRATO">CONTRATO</option>
                <option value="ESPORÁDICO">ESPORÁDICO</option>
            </select>
            </div>
          </div>
              
           <div>
            <label class="desc"  for="sltTipoContrato">
    	        TIPO CONTRATO
            </label>
            <div>
            <select id="sltTipoContrato" name="sltTipoContrato" class="field select medium" tabindex="10" runat="server"  > 
                <option value="" selected=""></option>
                <option value="FIXO">FIXO</option>
                <option value="VARIÁVEL">VARIÁVEL</option>
            </select>
            </div>
          </div>
              
            <div>
            <label class="desc" for="txtContratoInicio">Se "CONTRATO" DE</label>
            <div>
                <input id="txtContratoInicio" name="txtContratoInicio" style="position: relative;z-index: 100;" maxlength="20" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
           <div>
            <label class="desc" for="txtContratoFim">Se "CONTRATO" ATÉ</label>
            <div>
                <input id="txtContratoFim" name="txtContratoFim" style="position: relative;z-index: 100;" maxlength="20" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
           <div>
            <label class="desc" for="txtReceitaFederal">RECEITA FEDERAL</label>
            <div>
                <input id="txtReceitaFederal" name="txtReceitaFederal" style="position: relative;z-index: 100;" maxlength="20"  class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
           <div>
            <label class="desc" for="txtPrefeitura">PREFEITURA</label>
            <div>
                <input id="txtPrefeitura" name="txtPrefeitura" style="position: relative;z-index: 100;" maxlength="20" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtCNAE">CNAE</label>
            <div>
                <input id="txtCNAE" name="txtCNAE" style="position: relative;z-index: 100;" class="field text fn" maxlength="20" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
             <div>
            <label class="desc"  for="sltOptanteSimples">
    	        OPTANTE/SIMPLES
            </label>
            <div>
            <select id="sltOptanteSimples" name="sltOptanteSimples" class="field select medium" tabindex="10" runat="server"  > 
                <option value="True">SIM</option>
                <option value="False">NÃO</option>
            </select>
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtOptanteSimples">OPTANTE/SIMPLES</label>
            <div>
                <input id="txtOptanteSimples" name="txtOptanteSimples" style="position: relative;z-index: 100;" maxlength="20" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtSintegra">SINTEGRA</label>
            <div>
                <input id="txtSintegra" name="txtSintegra" style="position: relative;z-index: 100;" class="field text fn"  maxlength="20" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtJuntaComercial">JUNTA COMERCIAL</label>
            <div>
                <input id="txtJuntaComercial" name="txtJuntaComercial" style="position: relative;z-index: 100;" class="field text fn" maxlength="20" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
            
              <br/>
              <br/>
              
          <div>
            <label class="desc" for="txtIRpct">IR (%)</label>
            <div style="display:inline-table">
                <input id="txtIRpct" name="txtIRpct" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="3" size="8" tabindex="7" runat="server"  onblur="calculaPorcentagem('txtIRpct',this.value,'txtIRvalor')" />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtIRvalor">IR (valor)</label>
            <div style="display:inline-table">
                <input id="txtIRvalor" name="txtIRvalor" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server"/>
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtPCCpct">PCC (%)</label>
            <div style="display:inline-table">
                <input id="txtPCCpct" name="txtPCCpct" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="3" size="8" tabindex="7" runat="server"   onblur="calculaPorcentagem('txtPCCpct',this.value,'txtPCCvalor')" />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtPCCvalor">PCC (valor)</label>
            <div style="display:inline-table">
                <input id="txtPCCvalor" name="txtPCCvalor" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtISSpct">ISS (%)</label>
            <div style="display:inline-table">
                <input id="txtISSpct" name="txtISSpct" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="3" size="8" tabindex="7" runat="server"    onblur="calculaPorcentagem('txtISSpct',this.value,'txtISSvalor')" />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtISSvalor">ISS (valor)</label>
            <div style="display:inline-table">
                <input id="txtISSvalor" name="txtISSvalor" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtINSSpct">INSS (%)</label>
            <div style="display:inline-table">
                <input id="txtINSSpct" name="txtINSSpct" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="3" size="8" tabindex="7" runat="server"    onblur="calculaPorcentagem('txtINSSpct',this.value,'txtINSSvalor')" />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtINSSvalor">INSS (valor)</label>
            <div style="display:inline-table">
                <input id="txtINSSvalor" name="txtINSSvalor" style="position: relative;z-index: 100;" class="field text fn" value="0" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
             
          <div>
            <label class="desc" for="txtLiquido">LÍQUIDO</label>
            <div style="display:inline-table">
                <input id="txtLiquido" name="txtLiquido" style="position: relative;z-index: 100;" class="field text fn"  maxlength="15" size="8" tabindex="7" runat="server" onKeyPress="return(MascaraMoeda(this,'.',',',event));"  />
            </div>
          </div> 
              

           <div>
            <label class="desc" for="txtCadastroMunicipal">CADASTRO MUNICIPAL</label>
            <div style="display:inline-table">
                <input id="txtCadastroMunicipal" name="txtCadastroMunicipal" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtCodRetencaoISS">COD. RETENÇÃO ISS</label>
            <div style="display:inline-table">
                <input id="txtCodRetencaoISS" name="txtCodRetencaoISS" style="position: relative;z-index: 100;" class="field text fn" value="" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtPCCnfs">PCC S/NFS</label>
            <div style="display:inline-table">
                <input id="txtPCCnfs" name="txtPCCnfs" style="position: relative;z-index: 100;" class="field text fn" value="" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtCFOP">CFOP</label>
            <div style="display:inline-table">
                <input id="txtCFOP" name="txtCFOP" style="position: relative;z-index: 100;" class="field text fn" value="" maxlength="15" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
         
            <br />
            <br />
          
            <div>
                <label class="desc" for="cbCategoriaNF">Categoria NF</label>
                <div style="display:inline-table">
                    <fieldset data-role="controlgroup" class="tamanhoCB">
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
            <label class="desc" for="txtCodServico">Código do Serviço</label>
            <div style="display:inline-table">
                <input id="txtCodServico" name="txtCodServico" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>   
              
              <div>
            <label class="desc" for="txtValorDeducao">Valor da Dedução</label>
            <div style="display:inline-table">
                <input id="txtValorDeducao" name="txtValorDeducao" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtAliquota">Aliquota</label>
            <div style="display:inline-table">
                <input id="txtAliquota" name="txtAliquota" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtTipoDocumento">Tipo de Documento</label>
            <div style="display:inline-table">
                <input id="txtTipoDocumento" name="txtTipoDocumento" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtTributacaoServico">Tributação de Serviço</label>
            <div style="display:inline-table">
                <input id="txtTributacaoServico" name="txtTributacaoServico" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
              <div>
            <label class="desc" for="txtCodPrestador">Código do Prestador</label>
            <div style="display:inline-table">
                <input id="txtCodPrestador" name="txtCodPrestador" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
           <div>
            <label class="desc" for="txtAtribuicaoISS">Atribuição do ISS</label>
            <div style="display:inline-table">
                <input id="txtAtribuicaoISS" name="txtAtribuicaoISS" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
            <div>
            <label class="desc" for="txtSituacaoNTFS">Situação da NTFS</label>
            <div style="display:inline-table">
                <input id="txtSituacaoNTFS" name="txtSituacaoNTFS" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtISSRet">ISS Retido</label>
            <div style="display:inline-table">
                <input id="txtISSRet" name="txtISSRet" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
          <div>
            <label class="desc" for="txtRegTributacao">Registro de Tributação</label>
            <div style="display:inline-table">
                <input id="txtRegTributacao" name="txtRegTributacao" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
            </div>
          </div>
              
        
        <!-- CAMPOS RJ -->
        
        <div id="Carimbo1" style="display: none">
        <label class="desc" for="txtOptSimples">Opt. Simples</label>
        <div style="display:inline-table">
            <input id="txtOptSimples" name="txtOptSimples" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
        </div>
        </div>
        <div id="Carimbo2" style="display: none">
        <label class="desc" for="txtTipoNFConv">Tipo NF Conv.</label>
        <div style="display:inline-table">
            <input id="txtTipoNFConv" name="txtTipoNFConv" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
        </div>
        </div>
        <div id="Carimbo3" style="display: none">
        <label class="desc" for="txtCodServicoMun">Cod. Serv. Mun</label>
        <div style="display:inline-table">
            <input id="txtCodServicoMun" name="txtCodServicoMun" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
        </div>
        </div>
        <div id="Carimbo4" style="display: none">
        <label class="desc" for="txtTipoTribServico">Tp. Trib. Serv.</label>
        <div style="display:inline-table">
            <input id="txtTipoTribServico" name="txtTipoTribServico" style="position: relative;z-index: 100;" class="field text fn" value="" size="8" tabindex="7" runat="server"   />
        </div>
        </div>
        
        <div id="divCarimboRJ" runat="server" ></div>      
        <!-- CAMPOS RJ -->
           

             <div data-role="popup" id="popupFiles" data-theme="none" style="z-index:101">
                    <div data-role="content" data-theme="b" style="margin:0; max-width:500px;">
                        <div data-role="list-divider" data-mini="true" data-inset="true">
                            <div id="dados" runat="server" style="width:500px;"></div>
                        </div>
                    </div>
                </div>
              
              <asp:Button ID="Button1" data-icon="check" data-role="button" runat="server"  Text=" Salvar " data-theme="b" OnClick="SalvarAlteracao"/>
          </form>
     </div>
          <div id="divFunction" runat="server"></div>
            
     </div>
  </div>
     
</body>
</html>
