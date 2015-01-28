﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Configuracion_Sede_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
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
                                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio" placeholder="Descripción"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDireccion" runat="server" Text="Dirección : " CssClass="control-label" AssociatedControlID="txtDireccion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="span12 obligatorio" placeholder="Dirección"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono : " CssClass="control-label" AssociatedControlID="txtTelefono"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="span12 obligatorio" placeholder="Teléfono"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblCoordinador" runat="server" Text="Coordinador : " CssClass="control-label" AssociatedControlID="ddlCoordinador"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlCoordinador" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblSecreatariaCoordinacion" runat="server" Text="Secretaria Coordinación : " CssClass="control-label" AssociatedControlID="ddlSecretariaCoordinacion"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlSecretariaCoordinacion" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
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
