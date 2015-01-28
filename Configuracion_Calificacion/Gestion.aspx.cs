using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Calificacion_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Form.DefaultButton = btnGuardar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void cargar () {
        try {
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                                = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo                   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo),ddlPeriodo);
            string accion                                                               = Page.RouteData.Values["Accion"].ToString();
            GridView tbl_Configuracion_Calificacion                                     = new GridView();
            Calificacion_Configuracion objCalificacion_Configuracion                    = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOperCalificacion_Configuracion       = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (accion.Equals("Edita")) {
                objCalificacion_Configuracion.id                                        = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                tbl_Configuracion_Calificacion.DataSource                               = objOperCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objCalificacion_Configuracion);
                tbl_Configuracion_Calificacion.DataBind();
                txtDescripcion.Text                                                     = HttpUtility.HtmlEncode(tbl_Configuracion_Calificacion.Rows[0].Cells[1].Text);
                ddlPeriodo.SelectedValue                                                = tbl_Configuracion_Calificacion.Rows[0].Cells[2].Text;
                txtPorcentaje.Text                                                      = tbl_Configuracion_Calificacion.Rows[0].Cells[3].Text;
            }
        }
        catch (Exception) {}
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Calificacion_Configuracion objCalificacion_Configuracion                = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOperCalificacion_Configuracion   = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objCalificacion_Configuracion.descripcion                               = txtDescripcion.Text;
            objCalificacion_Configuracion.id_periodo                                = int.Parse(ddlPeriodo.SelectedValue.ToString());
            objCalificacion_Configuracion.porcentaje                                = int.Parse(txtPorcentaje.Text);
            objCalificacion_Configuracion.id_usuario                                = int.Parse(Session["id_usuario"].ToString());
            string accion                                                           = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agrega")){
                objOperCalificacion_Configuracion.InsertarCalificacion_Configuracion(objCalificacion_Configuracion);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Busqueda", Accion = "Agrego" });

            }else {
                string id                                                   = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objCalificacion_Configuracion.id                            = int.Parse(id);
                objOperCalificacion_Configuracion.ActualizarCalificacion_Configuracion(objCalificacion_Configuracion);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Busqueda", Accion = "Cancelo" });
       
    }
}