using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;

public partial class Configuracion_Anio_Escolar_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Anio_Escolar objAnio_Escolar = (Anio_Escolar)Session["anioEscolar"];
        if (objAnio_Escolar.descripcion != DateTime.Today.Year)
        {
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar", Pagina = "Gestion", Accion = "Agregar" });
        }else {
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar", Pagina = "Gestion", Accion = "Editar", Id = clsEncriptar.Encriptar(objAnio_Escolar.id.ToString()) });
        }
    }
}