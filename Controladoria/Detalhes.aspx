<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhes.aspx.cs" Inherits="ProtocoloWeb.Detalhes" %>

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
            <script>
                function dimensaoGrid() {
                    $(function () {
                        $('#<%=GridViewJustificativas.ClientID %>').footable({
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
        </style>
        <!-- User-generated js -->
</head>
<body>
        
        <div data-role="page" id="page1">
            <div data-role="content" align="center">
                <form id="form1" runat="server" >
                    
                    <div id="divJustificativa" runat="server">
                        <h2 style="text-align:center;" >Justificativa ID: <asp:Label ID="lblIdFormulario" runat="server" Text=""></asp:Label></h2>
                        <br/>
                        <br/>
                        <asp:GridView ID="GridViewJustificativas" CssClass="footable" runat="server" AutoGenerateColumns="False" EnableViewState="False">
                            <Columns>
                                <asp:BoundField DataField="Justificativa" HeaderText="Justificativa" />
                                <asp:BoundField DataField="dtJustificativa" HeaderText="Data" />
                            </Columns>
                        </asp:GridView>
                        
                        <asp:Label ID="lblResposta" runat="server" Text=""></asp:Label>
                    </div>
                </form> 
            </div>
        </div>
        
    
     
</body>
</html>
