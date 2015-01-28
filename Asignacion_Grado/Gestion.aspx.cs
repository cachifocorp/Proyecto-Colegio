using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Asignacion_Grado_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack){
            txtDescripcion.Focus();
            Form.DefaultButton = btnGuardar.UniqueID;
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Anio_Escolar objAnio_Escolar                    = (Anio_Escolar)Session["anioEscolar"];
            Grado objGrado                                  = new Grado();
            OperacionGrado objOperGrado                     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.descripcion                            = txtDescripcion.Text;
            objGrado.contraccion                            = txtContraccion.Text;
            objGrado.id_anio_escolar                        = objAnio_Escolar.id;
            objGrado.id_usuario                             = int.Parse(Session["id_usuario"].ToString());
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperGrado.InsertarGrado(objGrado);
                Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objGrado.id                                 = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperGrado.ActualizarGrado(objGrado);
                Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar (){
        try{
            Anio_Escolar objAnio_Escolar            = (Anio_Escolar)Session["anioEscolar"];
            Grado objGrado                          = new Grado();
            OperacionGrado objOperGrado              = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                = objAnio_Escolar.id;
            GridView tbl_Grado                      = new GridView();
            string accion                           = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita"))
            {
                string id                           = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objGrado.id                         = int.Parse(id);
                tbl_Grado.DataSource                = objOperGrado.ConsultarGrado(objGrado);
                tbl_Grado.DataBind();
                txtDescripcion.Text                 = HttpUtility.HtmlDecode(tbl_Grado.Rows[0].Cells[1].Text);
                txtContraccion.Text                 = tbl_Grado.Rows[0].Cells[2].Text;
            }
        }
        catch (Exception) {}
    }
}