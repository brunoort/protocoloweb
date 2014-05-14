<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carimbo.aspx.cs" Inherits="ProtocoloWeb.Carimbo" %>

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
        <div id="divCarimboRJ" runat="server" ></div> 

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
                   
        <!-- CAMPOS RJ -->
          

             <div data-role="popup" id="popupFiles" data-theme="none" style="z-index:101">
                    <div data-role="content" data-theme="b" style="margin:0; max-width:500px;">
                        <div data-role="list-divider" data-mini="true" data-inset="true">
                            <div id="dados" runat="server" style="width:500px;"></div>
                        </div>
                    </div>
                </div>
              
              <asp:Button ID="Button1" data-icon="check" data-role="button" runat="server" OnClick="Button1_Click" Text=" Salvar " data-theme="b" />
          </form>
     </div>
          <div id="divFunction" runat="server"></div>
            
     </div>
  </div>
     
</body>
</html>

