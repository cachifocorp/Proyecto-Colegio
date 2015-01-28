<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administracion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContenidoPagina" runat="Server" >
    <%if( Session["id_usuario_tipo"].ToString() == "1"){ %>
    <div class="hero-unit bg-silver" >
        <h2>Bienvenido Administrador</h2>
        <p>Integrese con nuestro portal para llevar a cabo todos los procesos que se llevan a cabo en su instituci&oacute;n</p>
        <p><a class="btn bg-green btn-large">Aprende Más</a></p>
    </div>
    <%} %>
    <%if (Session["id_usuario_tipo"].ToString() == "2" || Session["id_usuario_tipo"].ToString() == "4")
      { %>
        <h2>NOTICIAS</h2>
        <hr />
        <div class="row-fluid" id="pubsdoc" runat="server">
        </div>
    <%} %>
    <%if( Session["id_usuario_tipo"].ToString() == "5"){ %>    
    <h2>NOTICIAS</h2>
    <hr />
    <div class="row-fluid" id="pubs" runat="server">
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="JavaScript" runat="Server">
</asp:Content>

