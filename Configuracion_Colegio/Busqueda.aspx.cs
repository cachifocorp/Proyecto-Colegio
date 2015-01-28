using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Colegio_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Colegio objColegio                          = new Colegio();
        OperacionColegio objOperColegio             = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Colegio                        = new GridView();
        tbl_Colegio.DataSource                      = objOperColegio.ConsultarColegio(objColegio);
        tbl_Colegio.DataBind();
        if (tbl_Colegio.Rows.Count == 0) {
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Colegio", Pagina = "Gestion", Accion = "Agregar" });
        }else {
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Colegio", Pagina = "Gestion", Accion = "Editar", Id = clsEncriptar.Encriptar(tbl_Colegio.Rows[0].Cells[0].Text)});           
        }
    }
}