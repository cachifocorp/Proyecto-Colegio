<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Administracion_Administracion_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Academico</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Sistema Academico" />
    <meta name="author" content="Estigio" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma" content="no-cache" />

    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />

    <!-- styles -->
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/bootstrap-responsive.css" rel="stylesheet" />
    <!-- default theme -->
    <link href="../css/metro-bootstrap.css" rel="stylesheet" />
    <link href="../css/metro.css" rel="stylesheet" />
    <link href="../css/metro-responsive.css" rel="stylesheet" />
    <link href="../css/metro-helper.css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet">


    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->

    <script src="<%=ResolveUrl("../js/html5shiv.js") %>"></script>
    <script src="<%=ResolveUrl("../js/lte-ie7.js") %>"></script>

    <link href="../css/pnotify/jquery.pnotify.default.css" rel="stylesheet" />

    <link rel="shortcut icon" href="../ico/favicon.png" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <style type="text/css">
        .minuscula {
            text-transform: none !important;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin" defaultfocus="txtUsuario">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- start section content-->
                <section class="section-content bg-none">
                    <!-- start content -->
                    <div class="content full-page">
                        <!-- content page -->
                        <article class="content-page">
                            <!-- main page, you're application here -->
                            <div class="main-page">
                                <div class="content-inner">
                                    <div class="row-fluid">
                                        <div class="span4 offset4">
                                            <!-- widget login -->
                                            <div class="widget no-border bg-black">
                                                <!-- widget content -->
                                                <div class="widget-content">
                                                    <div class="text-center color-silver">
                                                        <h1>
                                                            <a href="#" class="help-block" title=""><i class="fa fa-book text-4x"></i></a>
                                                            Académico
                                                        </h1>
                                                    </div>
                                                    <div class="form-vertical">
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="span12 minuscula" placeholder="Usuario"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtPass" runat="server" CssClass="span12 minuscula" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Session" CssClass="btn  btn-block bg-green" OnClick="btnLogin_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <footer class="text-center">
                                                        <p>Copyright &copy; 2014. Todos Los Derechos Reservados.</p>
                                                        <a href="http://www.estigioportal.com">
                                                            <p>Estigio Portal de Soluciones</p>
                                                        </a>
                                                    </footer>
                                                </div>
                                                <!-- /widget content -->
                                            </div>
                                            <!-- /widget login -->

                                        </div>
                                        <!-- /span offset4 -->
                                    </div>
                                    <!-- /row -->

                                </div>
                                <!-- /content-inner -->
                            </div>
                            <!-- /main-page -->

                        </article>
                        <!-- /content page -->

                    </div>
                    <!--/ end content -->
                </section>
                <!-- /end section content-->

            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- javascript
        ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->

        <!-- required js -->
        <script type="text/javascript" src="<%=ResolveUrl("../js/m-scrollbar/jquery.mCustomScrollbar.min.js") %>"></script>


        <script type="text/javascript" src="<%=ResolveUrl("../js/jquery.min.js") %>"></script>
        <script type="text/javascript" src="<%=ResolveUrl("../js/jquery-ui.min.js") %>"></script>
        <script type="text/javascript" src="<%=ResolveUrl("../js/jquery.ui.touch-punch.min.js") %>"></script>
        <script type="text/javascript" src="<%=ResolveUrl("../js/bootstrap.min.js") %>"></script>
        <script type="text/javascript" src="<%=ResolveUrl("../js/sparkline/jquery.sparkline.min.js") %>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("../js/metro-base.js") %>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("../js/pnotify/jquery.pnotify.js") %>"></script>

        <script type="text/javascript" src="<%=ResolveUrl("../js/funciones.js") %>"></script>
    </form>
</body>
</html>
