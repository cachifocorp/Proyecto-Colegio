﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Proceso_Retiro_Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
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
                            <div class="form-vertical" id="frmCalificacion" runat="server">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblEstudiante" runat="server" Text="Documento Estudiante : " CssClass="control-label" AssociatedControlID="txtEstudiante"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtEstudiante" runat="server" CssClass="span12 obligatorio" placeholder="Documento Estdiante" OnTextChanged="txtEstudiante_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNombres" runat="server" Text="Nombres Estudiante : " CssClass="control-label" AssociatedControlID="txtNombres"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="span12 obligatorio" placeholder="Nombres Estudiante" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblApellidos" runat="server" Text="Apellidos Estudiante : " CssClass="control-label" AssociatedControlID="txtApellidos"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="span12 obligatorio" placeholder="Apellidos Estudiante" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span8">
                                        <div class="span">
                                            <div class="control-group">
                                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio" placeholder="Descripción" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-actions bg-silver">
                                    <asp:Button ID="btnRetirar" runat="server" Text="Retirar" class="btn bg-amber" OnClick="btnRetirar_Click" OnClientClick="return getValidar()"/>
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
</asp:Content>



