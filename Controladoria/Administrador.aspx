<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="ProtocoloWeb.Administrador" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.33242, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProtocoloWeb</title>
    <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=9">
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9"/>
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
        <meta name="apple-mobile-web-app-capable" content="yes" />
        <meta name="apple-mobile-web-app-status-bar-style" content="black" />

        <script src="Scripts/functions.js"></script>
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
        <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
        <link href="Styles/jquery-ui.css" rel="Stylesheet" type="text/css" />
        <link rel="stylesheet" href="https://s3.amazonaws.com/codiqa-cdn/mobile/1.2.0/jquery.mobile-1.2.0.min.css" />
        <script src="Scripts/jquery.mobile-1.2.0.min.js"></script>
        <link rel="stylesheet" href="Content/my.css" />

        <!-- FooTable -->
          <meta name="viewport" content = "width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
          <link href="Styles/footable-0.1.css" rel="stylesheet" type="text/css" />
          <script src="Scripts/data-generator.js" type="text/javascript"></script>
            <script src="Scripts/session.js"></script>
          <script src="Scripts/footable.js" type="text/javascript"></script>
            <script>
                function dimensaoGrid() {
                    $(function () {
                        $('#<%=GridView1.ClientID %>').footable({
                            breakpoints: {
                                phone: 480,
                                tablet: 1024
                            }
                        });

                    });
                }

        </script>
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
</head>
<body>  
        <div data-role="page" id="page1">
            <div data-role="navbar">
                <ul>
                    <li>
                        <img style="float:left;" src="Images/RecordMob.png" />
                            <a href="#popupNested" data-rel="popup" style="float:right;color:white;" data-position-to="window" data-transition="pop" data-role="button" data-mini="true" data-inline="true" data-theme="b">
                                <asp:Label ID="lblLogin" runat="server" />
                            </a>
                    </li>
                </ul>
            </div>
            <br/>
            <div data-role="header" data-theme="a">
                <a href="Default.aspx" target="_self" data-icon="home" data-theme="b">Menu</a>
                <h1 style="color:white;">Administrador</h1>
                
            </div>
            <div data-role="content" align="center">
                <form id="form1" runat="server">
                <div style="width: 100%" align="left">
                    <table>
                        <tr>
                            <td>
                                <div style="display:inline-table">
                                Login: <input id="txtNomeUsuario" name="txtNomeUsuario" type="text" maxlength="20" class="field text fn" value="" size="8"  runat="server" style="width:300px" />
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnAddUser" runat="server" Text="Adicionar" Width="60px" OnClick="btnAddUser_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div style="color:red;font-weight: bold;">
                <asp:Label ID="lblResposta" runat="server"></asp:Label>
                </div>
                <br/><br/>
                    <asp:GridView ID="GridView1" CssClass="footable"  runat="server" AutoGenerateColumns="False" DataKeyNames="idUsuario" DataSourceID="SqlDataSource1" PageSize="25" HeaderStyle-Width="85px" HorizontalAlign="Center">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" CancelImageUrl="~/Images/erradoIco.jpg" CancelText="Cancelar" DeleteImageUrl="~/Images/icon_excluir.jpg" DeleteText="Remover" EditImageUrl="~/Images/editar.png" EditText="Editar" InsertText="Inserir" NewText="Novo" SelectText="Selecionar" UpdateImageUrl="~/Images/certoIco.jpg" UpdateText="Atualizar" HeaderStyle-Width="25px" ControlStyle-CssClass="espacoLateral">
                            <ControlStyle Height="10px" ></ControlStyle>
                            <HeaderStyle Width="120px"></HeaderStyle>
                            </asp:CommandField>
                            
                            
                            <asp:BoundField DataField="idUsuario" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idUsuario" HeaderStyle-Width="30px" />
                            
                            <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" HeaderStyle-Width="100px"/>
                            <asp:CheckBoxField DataField="Formulario" HeaderText="Formulario" SortExpression="Formulario" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Pesquisar" HeaderText="Pesquisar" SortExpression="Pesquisar" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Controladoria" HeaderText="Controladoria" SortExpression="Controladoria" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Financeira" HeaderText="Financeira" SortExpression="Financeira" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Comex" HeaderText="Comex" SortExpression="Comex" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="PCeAdiantamentos" HeaderText="PCeAdiantamentos" SortExpression="PCeAdiantamentos" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Fiscal" HeaderText="Fiscal" SortExpression="Fiscal" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Fornecedor" HeaderText="Fornecedor" SortExpression="Fornecedor" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Input" HeaderText="Input" SortExpression="Input" HeaderStyle-Width="80px"/>
                            <asp:CheckBoxField DataField="Administrador" HeaderText="Admin" SortExpression="Administrador" HeaderStyle-Width="60px"/>
                            <asp:CheckBoxField DataField="Exclusao" HeaderText="Exclusao" SortExpression="Exclusao" HeaderStyle-Width="60px"/>
                        </Columns>
                        <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                    <HeaderStyle Width="85px"></HeaderStyle>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ProtocoloWebConnectionString %>" DeleteCommand="DELETE FROM [Usuarios] WHERE [idUsuario] = @idUsuario" InsertCommand="INSERT INTO [Usuarios] ([Login], [Formulario], [Pesquisar], [Controladoria], [Financeira], [Comex], [PCeAdiantamentos], [Fiscal], [Fornecedor], [Input], [Administrador],[Exclusao]) VALUES (@Login, @Formulario, @Pesquisar, @Controladoria, @Financeira, @Comex, @PCeAdiantamentos, @Fiscal, @Fornecedor, @Input, @Administrador,@Exclusao)" SelectCommand="SELECT * FROM [Usuarios]" UpdateCommand="UPDATE [Usuarios] SET [Login] = @Login, [Formulario] = @Formulario, [Pesquisar] = @Pesquisar, [Controladoria] = @Controladoria, [Financeira] = @Financeira, [Comex] = @Comex, [PCeAdiantamentos] = @PCeAdiantamentos, [Fiscal] = @Fiscal, [Fornecedor] = @Fornecedor, [Input] = @Input, [Administrador] = @Administrador, [Exclusao] = @Exclusao  WHERE [idUsuario] = @idUsuario">
                        <DeleteParameters>
                            <asp:Parameter Name="idUsuario" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Login" Type="String" />
                            <asp:Parameter Name="Formulario" Type="Boolean" />
                            <asp:Parameter Name="Pesquisar" Type="Boolean" />
                            <asp:Parameter Name="Controladoria" Type="Boolean" />
                            <asp:Parameter Name="Financeira" Type="Boolean" />
                            <asp:Parameter Name="Comex" Type="Boolean" />
                            <asp:Parameter Name="PCeAdiantamentos" Type="Boolean" />
                            <asp:Parameter Name="Fiscal" Type="Boolean" />
                            <asp:Parameter Name="Fornecedor" Type="Boolean" />
                            <asp:Parameter Name="Input" Type="Boolean" />
                            <asp:Parameter Name="Administrador" Type="Boolean" />
                            <asp:Parameter Name="Exclusao" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Login" Type="String" />
                            <asp:Parameter Name="Formulario" Type="Boolean" />
                            <asp:Parameter Name="Pesquisar" Type="Boolean" />
                            <asp:Parameter Name="Controladoria" Type="Boolean" />
                            <asp:Parameter Name="Financeira" Type="Boolean" />
                            <asp:Parameter Name="Comex" Type="Boolean" />
                            <asp:Parameter Name="PCeAdiantamentos" Type="Boolean" />
                            <asp:Parameter Name="Fiscal" Type="Boolean" />
                            <asp:Parameter Name="Fornecedor" Type="Boolean" />
                            <asp:Parameter Name="Input" Type="Boolean" />
                            <asp:Parameter Name="Administrador" Type="Boolean" />
                            <asp:Parameter Name="Exclusao" />
                            <asp:Parameter Name="idUsuario" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </form>
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
