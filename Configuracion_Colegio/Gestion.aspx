<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Configuracion_Colegio_Gestion" %>

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
                            <div class="form-vertical" id="frmAnio_Escolar">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDescripcion" runat="server" Text="Nombre : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio" placeholder="Nombre"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblEslogan" runat="server" Text="Eslogan : " CssClass="control-label" AssociatedControlID="txtEslogan"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtEslogan" runat="server" CssClass="span12 obligatorio" placeholder="Eslogan"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNumero_Cuenta" runat="server" Text="Número de Cuenta : " CssClass="control-label" AssociatedControlID="txtNumero_Cuenta"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNumero_Cuenta" runat="server" CssClass="span12 obligatorio" placeholder="Número de Cuenta"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblRector" runat="server" Text="Rector : " CssClass="control-label" AssociatedControlID="ddlRector"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlRector" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblSecreataria" runat="server" Text="Secretaria : " CssClass="control-label" AssociatedControlID="ddlSecretaria"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlSecretaria" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True">
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


