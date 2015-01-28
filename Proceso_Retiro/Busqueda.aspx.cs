using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Proceso_Retiro_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnRetirar.UniqueID;
    }
    protected void btnRetirar_Click(object sender, EventArgs e)
    {
        try
        {
            Retiro objRetiro                                        = new Retiro();
            OperacionRetiro objOperRetiro                           = new OperacionRetiro(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objRetiro.id_estudiante                                 = Convert.ToInt64(txtEstudiante.Text);
            objRetiro.descripcion                                   = txtDescripcion.Text;
            objRetiro.id_usuario                                    = int.Parse(Session["id_usuario"].ToString());
            objOperRetiro.InsertarRetiro(objRetiro);
            Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Agrego" });
            
        }
        catch (Exception)
        {
        }
    }
    protected void txtEstudiante_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Estudiante objEstudiante                                = new Estudiante();
            OperacionEstudiante objOperEstudiante                   = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objEstudiante.documento_numero                          = Convert.ToInt64(txtEstudiante.Text);
            DataTable dta_Estudiante                                = objOperEstudiante.ConsultarEstudiante(objEstudiante);
            txtNombres.Text                                         = dta_Estudiante.Rows[0].ItemArray[4].ToString() + " " +  dta_Estudiante.Rows[0].ItemArray[5].ToString();
            txtApellidos.Text                                       = dta_Estudiante.Rows[0].ItemArray[6].ToString() + " " + dta_Estudiante.Rows[0].ItemArray[7].ToString();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}