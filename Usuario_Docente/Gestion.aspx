<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Usuario_Docente_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .morphing-tinting .image-wrap {
            position: relative;
            -webkit-transition: 1s;
            -moz-transition: 1s;
            transition: 1s;
            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            border-radius: 20px;
        }

            .morphing-tinting .image-wrap:hover {
                -webkit-border-radius: 30em;
                -moz-border-radius: 30em;
                border-radius: 30em;
            }
        .minuscula {
            text-transform:lowercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
                function endReq(sender, args) {
                    $('#<%=txtFecha_Nacimiento.ClientID%>').datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
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
                            <div class="form-vertical" id="frmDocente" runat="server">
                                <div class="text-center morphing-tinting">
                                    <asp:Image ID="imgDocente" CssClass="image-wrap" runat="server" Width="150" Height="150" />
                                </div>
                                <br />
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDocumento_Id_Tipo" runat="server" Text="Tipo Documento :" CssClass="control-label" AssociatedControlID="ddlDocumento_Id_Tipo"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlDocumento_Id_Tipo" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDocumento_Numero" runat="server" Text="Número de Documento : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDocumento_Numero" runat="server" CssClass="span12 obligatorio" placeholder="Número De Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDescripcion" runat="server" Text="Nombre Completo : " CssClass="control-label" AssociatedControlID="txtDescripcion"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12 obligatorio" placeholder="Nombres"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
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
                                                <asp:Label ID="lblFecha_Nacimiento" runat="server" Text="Fecha Nacimiento : " CssClass="control-label" AssociatedControlID="txtFecha_Nacimiento"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtFecha_Nacimiento" runat="server" CssClass="span12 obligatorio" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblGenero" runat="server" Text="Genero :" CssClass="control-label" AssociatedControlID="ddlGenero"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlGenero" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
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
                                                <asp:Label ID="lblGrupo_Sanguineo" runat="server" Text="Grupo Sanguineo :" CssClass="control-label" AssociatedControlID="ddlGrupo_Sanguineo"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlGrupo_Sanguineo" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblEmail" runat="server" Text="Email : " CssClass="control-label" AssociatedControlID="txtEmail"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="span12 obligatorio email minuscula" placeholder="Email"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDireccion_Numero" runat="server" Text="Dirección Número : " CssClass="control-label" AssociatedControlID="txtDireccion_Numero"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtDireccion_Numero" runat="server" CssClass="span12 obligatorio email" placeholder="Dirección Número"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblDepartamento" runat="server" Text="Departamento :" CssClass="control-label"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblMunicipio" runat="server" Text="Municipio :" CssClass="control-label" AssociatedControlID="ddlMunicipio"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblTelefonos" runat="server" Text="Teléfonos : " CssClass="control-label" AssociatedControlID="txtTelefonos"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtTelefonos" runat="server" CssClass="span12 obligatorio" placeholder="Teléfonos"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblFoto" runat="server" Text="Foto : " CssClass="control-label" AssociatedControlID="fluFoto"></asp:Label>
                                                <div class="controls">
                                                    <asp:FileUpload ID="fluFoto" runat="server" CssClass="span12" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtFecha_Nacimiento.ClientID%>').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
            });
            $(".combo").chosen();
        });
    </script>
</asp:Content>

