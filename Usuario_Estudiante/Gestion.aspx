<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Usuario_Estudiante_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .minuscula {
            text-transform: lowercase;
        }

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
                    $('#<%=txtNacimiento_Fecha.ClientID%>').datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                    });

                    $('.combo').chosen();

                    var url = document.location.toString();
                    if (url.match('#')) {
                        $('.nav-tabs a[href=#' + url.split('#')[1] + ']').tab('show');
                    }

                    $('.nav-tabs a').on('shown', function (e) {
                        window.location.hash = e.target.hash;
                    });
                }
            </script>
            <div class="row-fluid">
                <div class=" span12">
                    <div class="widget border-<%=clsConfiguracion.Color_Sistema%>" id="widget-tbordered">
                        <div class="widget-header bg-<%=clsConfiguracion.Color_Sistema%>">
                            <div class="widget-icon"><i class="fa fa-book"></i></div>
                            <h4 class="widget-title"><%=Page.RouteData.Values["Accion"].ToString() %></h4>
                            <div class="widget-action" id="myTab">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a data-toggle="tab" href="#informacion_personal">Informaci&oacute;n Personal</a>
                                    </li>
                                    <li><a data-toggle="tab" href="#acudientes">Acudientes</a></li>
                                    <li><a data-toggle="tab" href="#salud">Salud</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="widget-content">
                            <div class="tab-content">
                                <!--Informacion Personal-->
                                <div class="tab-pane fade in active" id="informacion_personal">
                                    <div class="form-vertical" id="frmDocente" runat="server">
                                        <div class="text-center morphing-tinting">
                                            <asp:Image ID="imgEstudiante" CssClass="image-wrap" runat="server" Width="150" Height="150" />
                                        </div>
                                        <br />
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDocumento_Id_Tipo" runat="server" Text="Tipo Documento :" CssClass="control-label" AssociatedControlID="ddlDocumento_Id_Tipo"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDocumento_Id_Tipo" runat="server" CssClass=" combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDocumento_Numero" runat="server" Text="Número de Documento : " CssClass="control-label" AssociatedControlID="txtDocumento_Numero"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDocumento_Numero" runat="server" CssClass="span12 " placeholder="Número De Documento"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDocumento_Departamento" runat="server" Text="Departamento Expedición :" CssClass="control-label" AssociatedControlID="ddlDocumento_Departamento"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDocumento_Departamento" runat="server" CssClass="span12  combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDocumento_Departamento_SelectedIndexChanged">
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
                                                        <asp:Label ID="lblDocumento_Municipio" runat="server" Text="Municipio Expedición :" CssClass="control-label" AssociatedControlID="ddlDocumento_Municipio"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDocumento_Municipio" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNombre_1" runat="server" Text="Nombre 1 :" CssClass="control-label" AssociatedControlID="txtNombre_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNombre_1" runat="server" CssClass="span12 " placeholder="Nombre 1"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="blNombre_2" runat="server" Text="Nombre 2 :" CssClass="control-label" AssociatedControlID="txtNombre_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNombre_2" runat="server" CssClass="span12" placeholder="Nombre 2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblApellido_1" runat="server" Text="Apellido 1 : " CssClass="control-label" AssociatedControlID="txtApellido_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtApellido_1" runat="server" CssClass="span12 " placeholder="Apellido 1"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblApellido_2" runat="server" Text="Apellido 2 : " CssClass="control-label" AssociatedControlID="txtApellido_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtApellido_2" runat="server" CssClass="span12 " placeholder="Apellido 2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblGenero" runat="server" Text="Género :" CssClass="control-label" AssociatedControlID="ddlGenero"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlGenero" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
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
                                                        <asp:Label ID="lblEmail" runat="server" Text="Email : " CssClass="control-label" AssociatedControlID="txtEmail"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="span12 " placeholder="Email"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNacimiento_Fecha" runat="server" Text="Nacimiento Fecha : " CssClass="control-label" AssociatedControlID="txtNacimiento_Fecha"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNacimiento_Fecha" runat="server" CssClass="span12 " placeholder="dd/mm/yyyy"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNacimiento_Departamento" runat="server" Text="Nacimiento Departamento :" CssClass="control-label" AssociatedControlID="ddlNacimiento_Departamento"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlNacimiento_Departamento" runat="server" CssClass="span12  combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlNacimiento_Departamento_SelectedIndexChanged">
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
                                                        <asp:Label ID="lblNacimiento_Municipio" runat="server" Text="Nacimiento Municipio :" CssClass="control-label" AssociatedControlID="ddlNacimiento_Municipio"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlNacimiento_Municipio" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Numero" runat="server" Text="Dirección Número : " CssClass="control-label" AssociatedControlID="txtDireccion_Numero"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Numero" runat="server" CssClass="span12 " placeholder="Dirección Número"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Barrio" runat="server" Text="Dirección Barrio : " CssClass="control-label" AssociatedControlID="txtDireccion_Barrio"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Barrio" runat="server" CssClass="span12 " placeholder="Dirección Barrio"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Departamento" runat="server" Text="Direccion Departamento :" CssClass="control-label" AssociatedControlID="ddlDireccion_Departamento"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Departamento" runat="server" CssClass="span12  combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDireccion_Departamento_SelectedIndexChanged">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Municipio" runat="server" Text="Direccion Municipio :" CssClass="control-label" AssociatedControlID="ddlDireccion_Municipio"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Municipio" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblZona" runat="server" Text="Zona :" CssClass="control-label" AssociatedControlID="ddlZona"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
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
                                                        <asp:Label ID="lblTelefono_Fijo" runat="server" Text="Teléfono Fijo : " CssClass="control-label" AssociatedControlID="txtTelefono_Fijo"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtTelefono_Fijo" runat="server" CssClass="span12 " placeholder="Teléfono Fijo"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblTelefono_Celular" runat="server" Text="Teléfono Celular : " CssClass="control-label" AssociatedControlID="txtTelefono_Celular"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtTelefono_Celular" runat="server" CssClass="span12 " placeholder="Teléfono Celular" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
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
                                    </div>

                                </div>

                                <!--Salud-->
                                <div class="tab-pane fade" id="salud">
                                    <div class="form-vertical" id="frmSalud" runat="server">
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblEps" runat="server" Text="Eps :" CssClass="control-label" AssociatedControlID="ddlEps"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlEps" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblGrupo_Sanguineo" runat="server" Text="Grupo Sanguineo :" CssClass="control-label" AssociatedControlID="ddlGrupo_Sanguineo"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlGrupo_Sanguineo" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblSisben_Numero" runat="server" Text="Sisben Número : " CssClass="control-label" AssociatedControlID="txtSisben_Numero"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtSisben_Numero" runat="server" CssClass="span12 " placeholder="Sisben Número"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblSisben_Nivel" runat="server" Text="Sisben Nivel : " CssClass="control-label" AssociatedControlID="ddlSisben_Nivel"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlSisben_Nivel" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="I" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="II" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="III" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="IV" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="V" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="VI" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="NO APLICA" Value="7"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblEstrato" runat="server" Text="Estrato : " CssClass="control-label" AssociatedControlID="ddlEstrato"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlEstrato" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="UNO" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="DOS" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="TRES" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="CUATRO" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="CINCO" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="SEIS" Value="6"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!--Acudientes-->
                                <div class="tab-pane fade" id="acudientes">
                                    <div class="form-vertical" id="frmAcudiente" runat="server">
                                        <h5><strong>Acudiente 1</strong></h5>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblTipo_Documento_Acudiente_1" runat="server" Text="Tipo Documento :" CssClass="control-label" AssociatedControlID="ddlTipo_Documento_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlTipo_Documento_Acudiente_1" runat="server" CssClass=" combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNumero_Documento_Acudiente_1" runat="server" Text="Número de Documento : " CssClass="control-label" AssociatedControlID="txtNumero_Documento_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNumero_Documento_Acudiente_1" runat="server" CssClass="span12 " placeholder="Número De Documento" OnTextChanged="txtNumero_Documento_Acudiente_1_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNombres_Acudiente_1" runat="server" Text="Nombres : " CssClass="control-label" AssociatedControlID="txtNombres_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNombres_Acudiente_1" runat="server" CssClass="span12 " placeholder="Nombres"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblApellidos_Acudiente_1" runat="server" Text="Apellidos : " CssClass="control-label" AssociatedControlID="txtApellidos_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtApellidos_Acudiente_1" runat="server" CssClass="span12 " placeholder="Apellidos"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblParentesco_Acudiente_1" runat="server" Text="Parentesco :" CssClass="control-label" AssociatedControlID="ddlParentesco_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlParentesco_Acudiente_1" runat="server" CssClass=" combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblEmail_Acudiente_1" runat="server" Text="Email : " CssClass="control-label" AssociatedControlID="txtEmail_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtEmail_Acudiente_1" runat="server" CssClass="span12  email" placeholder="Email"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Numero_Acudiente_1" runat="server" Text="Dirección Número : " CssClass="control-label" AssociatedControlID="txtDireccion_Numero_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Numero_Acudiente_1" runat="server" CssClass="span12 " placeholder="Dirección Número"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Barrio_Acudiente_1" runat="server" Text="Dirección Barrio : " CssClass="control-label" AssociatedControlID="txtDireccion_Barrio_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Barrio_Acudiente_1" runat="server" CssClass="span12 " placeholder="Dirección Barrio"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Departamento_Acudiente_1" runat="server" Text="Direccion Departamento :" CssClass="control-label" AssociatedControlID="ddlDireccion_Departamento_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Departamento_Acudiente_1" runat="server" CssClass="span12  combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDireccion_Departamento_Acudiente_1_SelectedIndexChanged">
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
                                                        <asp:Label ID="lblDireccion_Municipio_Acudiente_1" runat="server" Text="Direccion Municipio :" CssClass="control-label" AssociatedControlID="ddlDireccion_Municipio_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Municipio_Acudiente_1" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblTelefonos_Acudiente_1" runat="server" Text="Teléfonos :" CssClass="control-label" AssociatedControlID="txtTelefonos_Acudiente_1"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtTelefonos_Acudiente_1" runat="server" CssClass="span12 " placeholder="Teléfonos"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5><strong>Acudiente 2</strong></h5>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblTipo_Documento_Acudiente_2" runat="server" Text="Tipo Documento :" CssClass="control-label" AssociatedControlID="ddlTipo_Documento_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlTipo_Documento_Acudiente_2" runat="server" CssClass=" combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNumero_Documento_Acudiente_2" runat="server" Text="Número de Documento : " CssClass="control-label" AssociatedControlID="txtNumero_Documento_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNumero_Documento_Acudiente_2" runat="server" CssClass="span12 " placeholder="Número De Documento" OnTextChanged="txtNumero_Documento_Acudiente_2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblNombres_Acudiente_2" runat="server" Text="Nombres : " CssClass="control-label" AssociatedControlID="txtNombres_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNombres_Acudiente_2" runat="server" CssClass="span12 " placeholder="Nombres"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblApellidos_Acudiente_2" runat="server" Text="Apellidos : " CssClass="control-label" AssociatedControlID="txtApellidos_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtApellidos_Acudiente_2" runat="server" CssClass="span12 " placeholder="Apellidos"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblParentesco_Acudiente_2" runat="server" Text="Parentesco :" CssClass="control-label" AssociatedControlID="ddlParentesco_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlParentesco_Acudiente_2" runat="server" CssClass=" combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblEmail_Acudiente_2" runat="server" Text="Email : " CssClass="control-label" AssociatedControlID="txtEmail_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtEmail_Acudiente_2" runat="server" CssClass="span12  email" placeholder="Email"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Numero_Acudiente_2" runat="server" Text="Dirección Número : " CssClass="control-label" AssociatedControlID="txtDireccion_Numero_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Numero_Acudiente_2" runat="server" CssClass="span12 " placeholder="Dirección Número"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Barrio_Acudiente_2" runat="server" Text="Dirección Barrio : " CssClass="control-label" AssociatedControlID="txtDireccion_Barrio_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDireccion_Barrio_Acudiente_2" runat="server" CssClass="span12 " placeholder="Dirección Barrio"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblDireccion_Departamento_Acudiente_2" runat="server" Text="Direccion Departamento :" CssClass="control-label" AssociatedControlID="ddlDireccion_Departamento_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Departamento_Acudiente_2" runat="server" CssClass="span12  combo" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDireccion_Departamento_Acudiente_2_SelectedIndexChanged">
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
                                                        <asp:Label ID="lblDireccion_Municipio_Acudiente_2" runat="server" Text="Direccion Municipio :" CssClass="control-label" AssociatedControlID="ddlDireccion_Municipio_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDireccion_Municipio_Acudiente_2" runat="server" CssClass="span12  combo" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span4">
                                                    <div class="control-group">
                                                        <asp:Label ID="lblTelefonos_Acudiente_2" runat="server" Text="Teléfonos :" CssClass="control-label" AssociatedControlID="txtTelefonos_Acudiente_2"></asp:Label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtTelefonos_Acudiente_2" runat="server" CssClass="span12 " placeholder="Teléfonos"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-vertical">
                        <div class="form-actions bg-silver">
                            <asp:Button ID="btnGuardarPersonal" runat="server" Text="Guardar" class="btn bg-amber" OnClientClick="return getValidar()" OnClick="btnGuardarPersonal_Click" />
                            <asp:Button ID="btnCancelarPersonal" runat="server" Text="Cancelar" class="btn bg-red" OnClick="btnCancelarPersonal_Click" />
                        </div>
                    </div>
                </div>
            </div>
            </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardarPersonal" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtNacimiento_Fecha.ClientID%>').datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
            });
            $('.combo').chosen();

            var url = document.location.toString();
            if (url.match('#')) {
                $('.nav-tabs a[href=#' + url.split('#')[1] + ']').tab('show');
            }

            $('.nav-tabs a').on('shown', function (e) {
                window.location.hash = e.target.hash;
            })
        });

    </script>
</asp:Content>

