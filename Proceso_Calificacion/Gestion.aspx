<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPageFull.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Proceso_Calificacion_Gestion" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .centrar {
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="full-content">
                <div class="row-fluid">
                    <div class=" span12">
                        <div class="widget border-<%=clsConfiguracion.Color_Sistema%>" id="widget-tbordered">
                            <div class="widget-header bg-<%=clsConfiguracion.Color_Sistema%>">
                                <div class="widget-icon"><i class="fa fa-book"></i></div>
                                <h4 class="widget-title"><%=Page.RouteData.Values["Accion"].ToString() %></h4>
                            </div>
                            <div class="widget-content">
                                <div class="form-horizontal" id="frmAnio_Escolar_Periodo">
                                    <h4 class="text-center"><strong><%=getTitulo() %></strong></h4>
                                    <asp:GridView ID="tbl_Calificacion" runat="server" CssClass="table table-bordered table-hover responsive" AutoGenerateColumns="false">
                                    </asp:GridView>
                                    <div class="form-actions bg-silver">
                                        <asp:Button ID="btnCargar" runat="server" Text="Cargar" class="btn bg-amber" OnClick="btnCargar_Click" />
                                        <asp:Button ID="btnVolver" runat="server" Text="Volver" class="btn bg-cyan" OnClick="btnVolver_Click"/>
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn bg-red" OnClick="btnCancelar_Click" />
                                        <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn bg-crimson" OnClick="btnExportar_Click" />
                                        <asp:FileUpload ID="fluImportar" runat="server" CssClass="btn-file" />
                                        <asp:Button ID="btnImportar" runat="server" Text="Importar" CssClass="btn bg-crimson" OnClick="btnImportar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="1610" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(":text").change(function () {
                
                <%ObjetosNegocio.Anio_Escolar objAnio_Escolar = (ObjetosNegocio.Anio_Escolar)Session["anioEscolar"];%>
                var tr = $(this).parent().parent();
                var inputs = $(tr).find('input');
                if ($(this).val().replace(",", ".") >= parseFloat('<%=objAnio_Escolar.nota_minima%>'.replace(",", ".")) && $(this).val().replace(",", ".") <= parseFloat('<%=objAnio_Escolar.nota_maxima%>'.replace(",", ".")) &&
                $(this).attr('id').toString() != $(inputs[inputs.length - 1]).attr('id').toString()) {

                    var span = $(tr).find('span');
                    var suma = 0;
                    for (var i = 0; i < inputs.length - 1; i++) {
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
                    if ($(this).attr('id').toString() != $(inputs[inputs.length - 1]).attr('id').toString()) {
                        alert("El valor esta fuera del rango \nNota Mínima: " + '<%=objAnio_Escolar.nota_minima%> \nNota Máxima: ' + '<%=objAnio_Escolar.nota_maxima%>');
                        $(this).focus();
                        $(this).val('');
                    }
                }
            });

            
        });
    </script>
</asp:Content>

