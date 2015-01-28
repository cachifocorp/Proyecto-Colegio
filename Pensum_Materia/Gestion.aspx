<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Pensum_Materia_Gestion" %>

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
                                                <asp:Label ID="lblGrado" runat="server" Text="Grado : " CssClass="control-label" AssociatedControlID="ddlGrado"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlGrado" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblArea" runat="server" Text="Area : " CssClass="control-label" AssociatedControlID="ddlArea"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblOrden_Impresion" runat="server" Text="Orden Impresión : " CssClass="control-label" AssociatedControlID="txtOrden_Impresion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtOrden_Impresion" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Orden Impresión" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblPorcentaje" runat="server" Text="Porcentaje : " CssClass="control-label" AssociatedControlID="txtPorcentaje"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Porcentaje" MaxLength="3"></asp:TextBox>
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

