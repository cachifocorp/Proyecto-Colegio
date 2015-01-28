<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Proceso_Cambio_Calificacion_Busqueda" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
                function endReq(sender, args) {
                    $(".combo").chosen();

                    $(":text").change(function () {
                        <%ObjetosNegocio.Anio_Escolar objAnio_Escolar = (ObjetosNegocio.Anio_Escolar)Session["anioEscolar"];%>
                        var tr = $(this).parent().parent();
                        var inputs = $(tr).find('input');
                        console.log("" + $(this).attr('id').toString());
                        if ($(this).val().replace(",", ".") >= parseFloat('<%=objAnio_Escolar.nota_minima%>'.replace(",", ".")) && $(this).val().replace(",", ".") <= parseFloat('<%=objAnio_Escolar.nota_maxima%>'.replace(",", "."))) {

                            var span = $(tr).find('span');
                            var suma = 0;
                            for (var i = 0; i < inputs.length; i++) {
                                console.log(inputs.length);
                                if (inputs[i].type == "text") {
                                    var clase = $(inputs[i]).attr('class').toString();
                                    var strs = clase.split(" ")

                                    var porcentaje = Number(strs[1] / 100);
                                    var valor = 0;
                                    if (inputs[i].value == "") {
                                        valor = 0;
                                    } else {
                                        valor = inputs[i].value.toString().replace(",", ".");
                                    }
                                    suma += valor * porcentaje;
                                }
                            }
                            for (var i = 0; i < span.length ; i++) {
                                var cssspan = $(span[i]).attr('class').toString().split(" ");
                                if (cssspan[1] == 'Periodo') {
                                    $(span[i]).text(parseFloat(suma).toFixed(2).toString().replace(".", ","));
                                }
                            }
                            var valor = $(this).val().toString().replace(",", ".");
                            $(this).val(parseFloat(valor).toFixed(2).replace(".", ","));
                        } else {
                            if ($(this).attr('id').toString() != "ContenidoPagina_txtEstudiante") { 
                            alert("El valor esta fuera del rango \nNota Mínima: " + '<%=objAnio_Escolar.nota_minima%> \nNota Máxima: ' + '<%=objAnio_Escolar.nota_maxima%>');
                            $(this).focus();
                            $(this).val('');
                        }
                        }
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
                            <div class="form-vertical" id="frmCalificacion" runat="server">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblEstudiante" runat="server" Text="Documento Estudiante : " CssClass="control-label" AssociatedControlID="txtEstudiante"></asp:Label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtEstudiante" runat="server" CssClass="span12 obligatorio" placeholder="Documento Estudiante" OnTextChanged="txtEstudiante_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                                    <div class="span12">
                                        <div class="span4">
                                            <div class="control-group">
                                                <asp:Label ID="lblPeriodo" runat="server" Text="Periodo : " CssClass="control-label" AssociatedControlID="ddlPeriodo"></asp:Label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="span12 obligatorio combo" AppendDataBoundItems="true" AutoPostBack="True">
                                                        <asp:ListItem Text="--- SELECCIONE UNO ---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions bg-silver">
                                    <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" class="btn bg-red" OnClick="btnCambiar_Click" CausesValidation="false" />
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <asp:GridView ID="tbl_Calificacion" runat="server" CssClass="table table-bordered table-hover responsive" AutoGenerateColumns="false">
                                        </asp:GridView>
                                    </div>
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
        $(document).on("ready", function () {

            $(".combo").chosen();
        });
    </script>
</asp:Content>


