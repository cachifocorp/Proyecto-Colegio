﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Configuracion_Usuario_Tipo_Busqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span12">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn bg-amber" ToolTip="Agregar Elemento" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn bg-red" ToolTip="Eliminar Elemento" OnClientClick="return Confirmar();" OnClick="btnEliminar_Click" />
                    <br />
                    <br />
                    <div class="widget border-<%=clsConfiguracion.Color_Sistema%>" id="widget-tbordered">
                        <div class="widget-header bg-<%=clsConfiguracion.Color_Sistema%>">
                            <div class="widget-icon"><i class="fa fa-book"></i></div>
                            <h4 class="widget-title">Busqueda</h4>
                        </div>
                        <div class="widget-content">
                            <div class="form-horizontal">
                                <div class="control-group">
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion :" CssClass="control-label"></asp:Label>
                                    <div class="controls span5">
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="span12" placeholder="Descripcion"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label ID="lbl" runat="server" Text="" CssClass="control-label"></asp:Label>
                                    <div class="controls span5">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn bg-teal" ToolTip="Buscar Elemento" OnClick="btnBuscar_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="tbl_Usuario_Tipo" runat="server" CssClass="table table-bordered table-hover responsive" EnableModelValidation="True" AllowPaging="True" DataKeyNames="Id" AutoGenerateColumns="False" OnPageIndexChanging="tbl_Usuario_Tipo_PageIndexChanging" OnSelectedIndexChanging="tbl_Usuario_Tipo_SelectedIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="50px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id" HeaderText="Id" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:TemplateField>
                                        <HeaderStyle Width="50px" />
                                        <HeaderTemplate>
                                            <asp:Label ID="lblEditar" runat="server" Text="Editar"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/img/windows-icons/dark/edit.png"
                                                Width="30px" Height="30px" ToolTip="Seleccione Para Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings PageButtonCount="10" />
                                <PagerStyle CssClass="paginar" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="txtOpcion" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
    <script type="text/javascript">
        function Confirmar() {
            var seleccionado = false;
            $("#<%=tbl_Usuario_Tipo.ClientID%> :checkbox").each(function () {
                if (this.checked) {
                    seleccionado = true;
                }
            });
            var ele = document.getElementById('<%=txtOpcion.ClientID%>');
            if (seleccionado) {
                if (confirm("<%=Resources.Mensaje.msjConfirmarEliminar%>")) {
                    if (confirm("<%=Resources.Mensaje.msjConfirmarConfirmarEliminar%>")) {
                        ele.value = 1;
                        return true;
                    } else {
                        ele.value = 0;
                        return false;
                    }
                } else {
                    ele.value = 0;
                    return false;
                }
            }
            else {
                alert('<%=Resources.Mensaje.msjErrorEliminar%>');
                return false;
            }
        }
    </script>
</asp:Content>

