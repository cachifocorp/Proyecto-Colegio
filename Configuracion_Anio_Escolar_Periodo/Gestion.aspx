<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Configuracion_Anio_Escolar_Periodo_Gestion" %>

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
                    $('#<%=txtFecha_Inicio.ClientID%>').datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                    });
                    $('#<%=txtFecha_Fin.ClientID%>').datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true
                    });
                    $('#<%=txtFecha_Fin_Calificacion.ClientID%>').datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true
                    });

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
                                                <asp:Label ID="lblNombre" runat="server" Text="Descripción : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio texto" placeholder="Descripción"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha_Inicio" runat="server" Text="Fecha Inicio : " CssClass="control-label" AssociatedControlID="txtFecha_Inicio"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Inicio" runat="server" CssClass="span12 obligatorio" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha_Fin" runat="server" Text="Fecha Fin : " CssClass="control-label" AssociatedControlID="txtFecha_Fin"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Fin" runat="server" CssClass="span12 obligatorio" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha_Fin_Calificacion" runat="server" Text="Fecha Fin Calificación : " CssClass="control-label" AssociatedControlID="txtFecha_Fin_Calificacion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Fin_Calificacion" runat="server" CssClass="span12 obligatorio" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblPorcentaje" runat="server" Text="Porcentaje : " CssClass="control-label" AssociatedControlID="txtPorcentaje"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Porcentaje" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNumero_Notas" runat="server" Text="Número Notas : " CssClass="control-label" AssociatedControlID="txtNumero_Notas"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNumero_Notas" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Número Notas" MaxLength="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions bg-silver">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn bg-amber" OnClick="btnGuardar_Click" OnClientClick="return getValidar()" />
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
            $('#<%=txtFecha_Inicio.ClientID%>').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
            });
            $('#<%=txtFecha_Fin.ClientID%>').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('#<%=txtFecha_Fin_Calificacion.ClientID%>').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
</asp:Content>
