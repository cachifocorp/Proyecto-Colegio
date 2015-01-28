﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Usuario_Usuario_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function endReq(sender, args) {
            $('.combo').chosen();
            $('.fecha').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
            });
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                                <asp:Label ID="lblEstudiante" runat="server" Text="Documento : " CssClass="control-label" AssociatedControlID="txtEstudiante"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtEstudiante" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Documento Estudiante"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombres" runat="server" Text="Nombres : " CssClass="control-label" AssociatedControlID="txtNombres"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="span12 obligatorio" placeholder="Nombres Estudiante" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblApellidos" runat="server" Text="Apellidos : " CssClass="control-label" AssociatedControlID="txtApellidos"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="span12 obligatorio" placeholder="Apellidos Estudiante" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblGrado" runat="server" Text="Grado :" CssClass="control-label" AssociatedControlID="ddlGrado"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlGrado" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblSalon" runat="server" Text="Salon :" CssClass="control-label" AssociatedControlID="ddlSalon"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlSalon" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
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
            $('.combo').chosen();
            $('.fecha').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
            });
        });
    </script>
</asp:Content>

