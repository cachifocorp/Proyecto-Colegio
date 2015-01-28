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

public partial class Proceso_Observador_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardar.UniqueID;
        txtEstudiante.Focus();
        if (!IsPostBack)
        {
            cargar();
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
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Observador objObservador                        = new Observador();
            OperacionObservador objOperObservador           = new OperacionObservador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objObservador.descripcion                       = txtDescripcion.Text;
            objObservador.id_estudiante                     = Convert.ToInt64(txtEstudiante.Text);
            objObservador.id_usuario                        = int.Parse(Session["id_usuario"].ToString());
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {

                objOperObservador.InsertarObservador(objObservador);
                Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objObservador.id                            = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperObservador.ActualizarObservador(objObservador);
                Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar() {
        try
        {
            string accion                                   = Page.RouteData.Values["accion"].ToString();
            if (accion.Equals("Edita")){
                Observador objObservador                    = new Observador();
                OperacionObservador objOperObservador       = new OperacionObservador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Observador                     = new GridView();
                objObservador.id                            = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                tbl_Observador.DataSource                   = objOperObservador.ConsultarObservador(objObservador);
                tbl_Observador.DataBind();
                txtEstudiante.Text                          = HttpUtility.HtmlDecode(tbl_Observador.Rows[0].Cells[4].Text);
                txtNombres.Text                             = HttpUtility.HtmlDecode(tbl_Observador.Rows[0].Cells[5].Text);
                txtApellidos.Text                           = HttpUtility.HtmlDecode(tbl_Observador.Rows[0].Cells[6].Text);
                txtDescripcion.Text                         = HttpUtility.HtmlDecode(tbl_Observador.Rows[0].Cells[1].Text) ;
            }
        }
        catch (Exception)
        {
        }
    
    }
}