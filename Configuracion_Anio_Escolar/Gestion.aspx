<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Configuracion_AnioEscolar_Gestion" %>

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
                                                <asp:Label ID="lblDescripcion" runat="server" Text="Año Escolar : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio" placeholder="Año Escolar"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha_Inicio" runat="server" Text="Fecha Inicio : " CssClass="control-label" AssociatedControlID="txtFecha_Inicio"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Inicio" runat="server" CssClass="span12 obligatorio" placeholder="Fecha Inicio"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFecha_Fin" runat="server" Text="Fecha Fin : " CssClass="control-label" AssociatedControlID="txtFecha_Fin"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Fin" runat="server" CssClass="span12 obligatorio" placeholder="Fecha Fin"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNota_Minima" runat="server" Text="Nota Mínima : " CssClass="control-label" AssociatedControlID="txtNota_Minima"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNota_Minima" runat="server" CssClass="span12 obligatorio numerico" placeholder="Nota Mínima"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNota_Maxima" runat="server" Text="Nota Máxima : " CssClass="control-label" AssociatedControlID="txtNota_Maxima"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNota_Maxima" runat="server" CssClass="span12 obligatorio numerico" placeholder="Nota Máxima"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblRendimiento_Bajo" runat="server" Text="Rendimiento Bajo : " CssClass="control-label" AssociatedControlID="txtRendimiento_Bajo"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtRendimiento_Bajo" runat="server" CssClass="span12 obligatorio numerico" placeholder="Rendimiento Bajo"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblRendimiento_Basico" runat="server" Text="Rendimiento Básico : " CssClass="control-label" AssociatedControlID="txtRendimiento_Basico"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtRendimiento_Basico" runat="server" CssClass="span12 obligatorio numerico" placeholder="Rendimiento Básico"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblRendimiento_Alto" runat="server" Text="Rendimiento Alto : " CssClass="control-label" AssociatedControlID="txtRendimiento_Alto"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtRendimiento_Alto" runat="server" CssClass="span12 obligatorio numerico" placeholder="Rendimiento Alto"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblRendimiento_Superior" runat="server" Text="Rendimiento Superior : " CssClass="control-label" AssociatedControlID="txtRendimiento_Superior"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtRendimiento_Superior" runat="server" CssClass="span12 obligatorio numerico" placeholder="Rendimiento Superior"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblNumero_Periodos" runat="server" Text="Número Periodos : " CssClass="control-label" AssociatedControlID="txtNumero_Periodos"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtNumero_Periodos" runat="server" CssClass="span12 obligatorio numerico entero" placeholder="Número Periodos"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblColegio" runat="server" Text="Colegio : " CssClass="control-label" AssociatedControlID="ddlColegio"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlColegio" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="--- SELECCIONE UNO ---"></asp:ListItem>
                                                    </asp:DropDownList>
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
            $(".combo").chosen();
        });

    </script>
</asp:Content>

