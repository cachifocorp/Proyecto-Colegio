<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPageFull.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Tecnica_Asignacion_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                                    <div class="row-fluid">
                                        <div class="span12">
                                            <div class="span6">
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
                                                        <asp:BoundField DataField="id" HeaderText="Id Matricula" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblNombre_Completo" runat="server" Text="Nombre Estudiante"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNombre" Text='<%#Eval("apellidos")+" "+Eval("nombres")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="span6">
                                                <asp:GridView ID="tbl_Tecnicas" runat="server" CssClass="table table-bordered table-hover responsive" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderStyle Width="50px" />
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkTecnicaTodos" runat="server" onclick="checkAll(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkTecnica" runat="server" onclick="Check_Click(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="Id" />
                                                        <asp:BoundField DataField="descripcion" HeaderText="Grupos Técnicos" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions bg-silver">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn bg-amber" OnClick="btnGuardar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn bg-red" OnClick="btnCancelar_Click" />
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
</asp:Content>

