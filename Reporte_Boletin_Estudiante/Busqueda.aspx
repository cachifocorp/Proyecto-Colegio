<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPageFull.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Reporte_Boletin_Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
                function endReq(sender, args) {
                    $(".combo").chosen();
                }
            </script>
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget border-<%=clsConfiguracion.Color_Sistema%>" id="widget-tbordered">
                        <div class="widget-header bg-<%=clsConfiguracion.Color_Sistema%>">
                            <div class="widget-icon"><i class="fa fa-book"></i></div>
                            <h4 class="widget-title">Busqueda  </h4>
                        </div>
                        <div class="widget-content">
                            <div class="form-horizontal">                               
                                <div class="control-group">
                                    <span id="message" runat="server"></span>
                                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo :" CssClass="control-label" AssociatedControlID="ddlPeriodo"></asp:Label>
                                    <div class="controls span5">
                                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="combo" AppendDataBoundItems="true">
                                            <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label ID="lbl" runat="server" Text="" CssClass="control-label" ></asp:Label>
                                    <div class="controls span5">
                                       
                                        <asp:Button ID="btnGenerar" runat="server" Text="Generar" class="btn bg-red" ToolTip="Buscar Elemento" OnClick="btnGenerar_Click" />
                                    </div>
                                </div>
                            </div>                          
                        </div>
                    </div>
                   
                </div>
            </div>
            <asp:HiddenField ID="txtOpcion" runat="server" />
        </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="btnGenerar" /></Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.combo').chosen();
        });
    </script>
</asp:Content>




