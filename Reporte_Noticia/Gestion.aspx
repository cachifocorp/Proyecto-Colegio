<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion_Administracion/MasterPageFull.master" AutoEventWireup="true" CodeFile="Gestion.aspx.cs" Inherits="Reporte_Carnet_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
     #contenedor{
        width:642.519685039px;
        height:207.874015748px;  
        border:1px solid black;         
      }
      #infocarnet{
        width:321.2598425197px;
        height:207.874015748px;       
        float:left;
        font-family:arial;
      }
        #infocarnet h2{
          font-size:15px;
          text-align:center;
          line-height:1.5;
          padding: 5px;
        }
      #infocolegio  {
        width:321.2598425197px;
        height:207.874015748px;       
        float:left;
        text-align:center;   
        font-family:arial;    
      }
      #infocolegio p{
        padding:5px;
        font-size:12px;
        line-height: 1.5;
        padding-top:3%;
      }
      .foto{
        width:100px;
        height:120px;        
        background-size: 100% 100%;
        margin-left:5px;
        float:left;
      }
      #information{
        width:60%;
        height:140px;      
        margin-left:5px;
        float:left;       
        text-align:left;
        font-size:13px;  
        padding-top:5px;     
      }

      #firma{
        width: 100px;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPagina" runat="Server">
    <div style="margin: auto auto 20px">
        <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn bg-red" OnClick="btnExportar_Click" />
        <asp:Button ID="printButton" runat="server" Text="Imprimir" CssClass="btn bg-cyan" OnClientClick="javascript:window.print();" />
        
    </div>
    <div id="content" runat="server">
        <%=clsFunciones.carnet %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScript" runat="Server">
</asp:Content>

