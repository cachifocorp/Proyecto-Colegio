using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Administracion_Administracion_Perfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try{
            if (Session["id_usuario"].ToString() != ""){
                Usuario objUsuario                  = new Usuario();
                OperacionUsuario objOperUsuario     = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objUsuario.id                       = int.Parse(Session["id_usuario"].ToString());
                objUsuario.password                 = txtContraseña.Text.Trim();
                objOperUsuario.ActualizarUsuario(objUsuario);
                Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Edito" });
            }
        }catch (Exception ex) {}
    }
}