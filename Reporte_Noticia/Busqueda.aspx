<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Reporte_Carnet_Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            <h4 class="widget-title">Busqueda | <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h4>
                        </div>
                        <div class="widget-content">
                            <div class="form-horizontal">
                                <div class="control-group" runat="server" id="docente">
                                    <asp:Label ID="lblSalon" runat="server" Text="Salón :" CssClass="control-label" AssociatedControlID="ddlSalon"></asp:Label>
                                    <div class="controls span5">
                                        <asp:DropDownList ID="ddlSalon" runat="server" CssClass="combo" AppendDataBoundItems="true">
                                            <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label ID="lbl" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    <div class="controls span5">
                                        <span id="mess" runat="server"></span>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn bg-teal" ToolTip="Buscar Estudiante" OnClick="btnBuscar_Click"/>
                                        <asp:Button ID="btnGenerar" runat="server" Text="Generar" class="btn bg-red" ToolTip="Generar Reporte" OnClick="btnGenerar_Click"/>
                                        <a href="#" class="btn bg-green" id="configurar">Configurar</a>
                                    </div>
                                </div>
                                <div class="control-group hidden" id="configurationSection">
                                    <div class="controls span5">
                                        <asp:Label ID="Label1" runat="server" Text="Fondo del carnet:" CssClass="control-label" AssociatedControlID="filebg"></asp:Label>
                                        <div class="controls span5">
                                            <span id="Span1" runat="server"></span>
                                            <asp:FileUpload ID="filebg" runat="server" />
                                            <asp:Button ID="btnEnviar" runat="server" Text="Subir Archivo" OnClick="Button1_Click"/>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="tbl_Estudiante" runat="server" CssClass="table table-bordered table-hover responsive" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="50px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkEstudianteTodos" runat="server" onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEstudiante" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="documento_numero" HeaderText="Id" />
                                    <asp:BoundField DataField="nombre_completo" HeaderText="Estudiante" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                   
                </div>
            </div>
            <asp:HiddenField ID="txtOpcion" runat="server" />
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviar" runat="server" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.combo').chosen();
        });

        $("#configurar").on('click', function () {
            $("#configurationSection").attr("class", "control-group");
        });

    </script>
</asp:Content>





