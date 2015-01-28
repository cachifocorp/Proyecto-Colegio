using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Anio_Escolar_Periodo_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardar.UniqueID;
        txtDescripcion.Focus();
        if(!IsPostBack) {
            this.load();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try { 
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                = new Anio_Escolar_Periodo();
            Anio_Escolar objAnio_Escolar                                = (Anio_Escolar)Session["anioEscolar"];
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAnio_Escolar_Periodo.descripcion                         = txtDescripcion.Text;
            objAnio_Escolar_Periodo.id_anio_escolar                     = objAnio_Escolar.id;
            objAnio_Escolar_Periodo.fecha_inicio                        = DateTime.Parse(Request.Form[txtFecha_Inicio.UniqueID]);
            objAnio_Escolar_Periodo.fecha_fin                           = DateTime.Parse(Request.Form[txtFecha_Fin.UniqueID]);
            objAnio_Escolar_Periodo.fecha_fin_calificacion              = DateTime.Parse(Request.Form[txtFecha_Fin_Calificacion.UniqueID]);
            objAnio_Escolar_Periodo.porcentaje                          = int.Parse(txtPorcentaje.Text);
            objAnio_Escolar_Periodo.numero_notas                        = int.Parse(txtNumero_Notas.Text);
            objAnio_Escolar_Periodo.id_usuario                          = int.Parse(Session["id_usuario"].ToString());
            string accion                                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar")) {
                objOperAnio_Escolar_Periodo.InsertarAnio_Escolar_Periodo(objAnio_Escolar_Periodo);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Busqueda", Accion = "Agrego" });
            }else {
                objAnio_Escolar_Periodo.id                              = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperAnio_Escolar_Periodo.ActualizarAnio_Escolar_Periodo(objAnio_Escolar_Periodo);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    protected void load()
    {
        try {
            string accion                                                           = Page.RouteData.Values["accion"].ToString();
                if (accion.Equals("Edita")){
                    GridView tbl_Anio_Escolar_Periodo                               = new GridView();
                    Anio_Escolar_Periodo objAnio_Escolar_Periodo                    = new Anio_Escolar_Periodo();
                    OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                    objAnio_Escolar_Periodo.id                                      = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                    tbl_Anio_Escolar_Periodo.DataSource                             = objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo);
                    tbl_Anio_Escolar_Periodo.DataBind();
                    txtDescripcion.Text                                             = tbl_Anio_Escolar_Periodo.Rows[0].Cells[1].Text;
                    txtFecha_Inicio.Text                                            = DateTime.Parse(tbl_Anio_Escolar_Periodo.Rows[0].Cells[3].Text).ToShortDateString();
                    txtFecha_Fin.Text                                               = DateTime.Parse(tbl_Anio_Escolar_Periodo.Rows[0].Cells[4].Text).ToShortDateString();
                    txtFecha_Fin_Calificacion.Text                                  = DateTime.Parse(tbl_Anio_Escolar_Periodo.Rows[0].Cells[5].Text).ToShortDateString();
                    txtPorcentaje.Text                                              = tbl_Anio_Escolar_Periodo.Rows[0].Cells[6].Text.Replace(",",".");
                    txtNumero_Notas.Text                                            = tbl_Anio_Escolar_Periodo.Rows[0].Cells[7].Text;
                }
        }
        catch (Exception) {}
    }

}