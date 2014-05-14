<%@ Page Title="ProtocoloWeb" Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProtocoloWeb._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <meta charset="utf-8" />
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />
        <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
        <script src="Scripts/jquery-1.7.2.min.js"></script>
        <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Home -->
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
            <div data-role="content">
                
                <asp:Label ID="lblUserRede" runat="server" Text=""></asp:Label>

                <ul data-role="listview" data-divider-theme="b" data-inset="true">
                    <li data-role="list-divider" role="heading">
                        ProtocoloWeb
                    </li>

                   
                    <li>
                        <a href="Insercao.aspx" target="_self">
                            Formulário
                        </a>
                    </li>
                    <% if (Session["pAdministrador"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Administrador.aspx" target="_self">
                            Administrador
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pPesquisar"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Pesquisar.aspx" target="_self">
                            Pesquisar
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pControladoria"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=1" target="_self">
                            Painel Controladoria
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountControladoria" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pFinanceira"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=2" target="_self">
                            Painel Financeira
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountFinanceira" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pComex"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=3" target="_self">
                            Painel Comex
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountComex" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pPCeAdiantamentos"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=4" target="_self">
                            Painel PC e Adiantamentos
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountPceAdiantamentos" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pFiscal"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=5" target="_self">
                            Painel Fiscal
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountFiscal" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pFornecedor"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=6" target="_self">
                            Painel Fornecedor
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountFornecedor" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>

                    <% if (Session["pInput"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Painel.aspx?idtipo=7" target="_self">
                            Painel Input
                            <span class="ui-li-count">
                                <asp:Label ID="lblCountInput" runat="server" Text="0" />
                            </span>
                        </a>
                    </li>
                    <% } %>
                     <% if (Session["pAdministrador"].ToString() == "True")
                       { %>
                    <li>
                        <a href="Ferramentas.aspx" target="_self">
                            Ferramentas
                        </a>
                    </li>
                    <% } %>
                </ul>
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
        </div>
    
</asp:Content>

