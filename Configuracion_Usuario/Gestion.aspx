<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Configuracion_Usuario_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <style type="text/css">
        .minuscula {
            text-transform: none !important;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
                function endReq(sender, args) {
                    $(".combo").chosen();
                }
            </script>
            <div class="row-fluid">
                <div class=" span12">
                    <div class="widget border-<%=clsConfiguracion.Color_Sistema%>" id="widget-tbordered">
                        <div class="widget-header bg-<%=clsConfiguracion.Color_Sistema%>">
                            <div class="widget-icon"><i class="fa fa-book"></i></div>
                            <h4 class="widget-title"><%=Page.RouteData.Values["Accion"].ToString() %></h4>
                        </div>
                        <div class="widget-content">
                            <div class="form-vertical" id="frmAnio_Escolar_Periodo">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombres" runat="server" Text="Nombres : " CssClass="control-label" AssociatedControlID="txtNombres"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="span12 obligatorio" placeholder="Nombres"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblApellidos" runat="server" Text="Apellidos : " CssClass="control-label" AssociatedControlID="txtApellidos"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="span12 obligatorio" placeholder="Apellidos"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDocumento" runat="server" Text="Documento : " CssClass="control-label" AssociatedControlID="txtDocumento"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="span12 obligatorio" placeholder="Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblUsuario" runat="server" Text="Usuario : " CssClass="control-label" AssociatedControlID="txtUsuario"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="span12 obligatorio minuscula" placeholder="Usuario"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblTipo_Usuario" runat="server" Text="Tipo Usuario : " CssClass="control-label" AssociatedControlID="ddlTipo_Usuario"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlTipo_Usuario" runat="server" CssClass="obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="---SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblContraseña" runat="server" Text="Contraseña : " CssClass="control-label" AssociatedControlID="txtContraseña"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtContraseña" runat="server" CssClass="span12 minuscula" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions bg-silver">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn bg-amber" OnClientClick="return getValidar()" OnClick="btnGuardar_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn bg-red" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".combo").chosen();
        });
    </script>
</asp:Content>
