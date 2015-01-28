<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPageFull.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Reporte_Carnet_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media print{
            #navbar-top, .content-header, #botonesImprimir {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <div style="margin: auto auto 20px" id="botonesImprimir">
        <!--<asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn bg-red" OnClick="btnExportar_Click" />-->
        <asp:Button ID="printButton" runat="server" Text="Imprimir" CssClass="btn bg-cyan" OnClientClick="javascript:window.print();" />
        
    </div>
    <div id="content" runat="server">
        <%=clsFunciones.carnet %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
</asp:Content>

