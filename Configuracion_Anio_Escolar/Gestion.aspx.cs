using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_AnioEscolar_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnGuardar.UniqueID;
            txtDescripcion.Focus();
            this.load();
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Anio_Escolar objAnio_Escolar                = new Anio_Escolar();
            OperacionAnio_Escolar objOperAnio_Escolar   = new OperacionAnio_Escolar(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAnio_Escolar.descripcion                 = int.Parse(txtDescripcion.Text);
            objAnio_Escolar.fecha_inicio                = DateTime.Parse(Request.Form[txtFecha_Inicio.UniqueID]);
            objAnio_Escolar.fecha_fin                   = Convert.ToDateTime(Request.Form[txtFecha_Fin.UniqueID]);
            objAnio_Escolar.nota_minima                 = Math.Round(decimal.Parse(txtNota_Minima.Text.Replace(".", ",")), 2);
            objAnio_Escolar.nota_maxima                 = Math.Round(decimal.Parse(txtNota_Maxima.Text.Replace(".",",")),2);
            objAnio_Escolar.rendimiento_bajo            = Math.Round(decimal.Parse(txtRendimiento_Bajo.Text.Replace(".",",")),2);
            objAnio_Escolar.rendimiento_basico          = Math.Round(decimal.Parse(txtRendimiento_Basico.Text.Replace(".",",")),2);
            objAnio_Escolar.rendimiento_alto            = Math.Round(decimal.Parse(txtRendimiento_Alto.Text.Replace(".",",")),2);
            objAnio_Escolar.rendimiento_superior        = Math.Round(decimal.Parse(txtRendimiento_Superior.Text.Replace(".",",")),2);
            objAnio_Escolar.numero_periodos             = int.Parse(txtNumero_Periodos.Text);
            objAnio_Escolar.id_colegio                  = int.Parse(ddlColegio.SelectedValue.ToString());
            objAnio_Escolar.id_usuario                  = int.Parse(Session["id_usuario"].ToString());
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar")){
                objOperAnio_Escolar.InsertarAnio_Escolar(objAnio_Escolar);
                Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Agrego" });
            }else {
                string id                               = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objAnio_Escolar.id                      = int.Parse(id);
                objOperAnio_Escolar.ActualizarAnio_Escolar(objAnio_Escolar);
                Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Edito" });
            }
        }
        catch (Exception) {}
    }

    protected void load()
    {
        try {
        Colegio objColegio                              = new Colegio();
        OperacionColegio objOperColegio                 = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        ddlColegio.DataSource                           = objOperColegio.ConsultarColegio(objColegio);
        ddlColegio.DataValueField                       = "id";
        ddlColegio.DataTextField                        = "nombre";
        ddlColegio.DataBind();
        string accion                                   = Page.RouteData.Values["Accion"].ToString();
        if (accion.Equals("Editar")) {
            txtDescripcion.Enabled                      = false;
            Anio_Escolar objAnio_Escolar                = new Anio_Escolar();
            OperacionAnio_Escolar objOperAnio_Escolar   = new OperacionAnio_Escolar(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Anio_Escolar                   = new GridView();
            tbl_Anio_Escolar.DataSource                 = objOperAnio_Escolar.ConsultarMaximo(objAnio_Escolar);
            tbl_Anio_Escolar.DataBind();
            txtDescripcion.Text                         = tbl_Anio_Escolar.Rows[0].Cells[1].Text;
            txtFecha_Inicio.Text                        = DateTime.Parse(tbl_Anio_Escolar.Rows[0].Cells[2].Text).ToShortDateString();
            txtFecha_Fin.Text                           = DateTime.Parse(tbl_Anio_Escolar.Rows[0].Cells[3].Text).ToShortDateString();
            txtNota_Minima.Text                         = tbl_Anio_Escolar.Rows[0].Cells[4].Text.Replace(",",".");
            txtNota_Maxima.Text                         = tbl_Anio_Escolar.Rows[0].Cells[5].Text.Replace(",", ".");
            txtRendimiento_Bajo.Text                    = tbl_Anio_Escolar.Rows[0].Cells[6].Text.Replace(",",".");
            txtRendimiento_Basico.Text                  = tbl_Anio_Escolar.Rows[0].Cells[7].Text.Replace(",", ".");
            txtRendimiento_Alto.Text                    = tbl_Anio_Escolar.Rows[0].Cells[8].Text.Replace(",", ".");
            txtRendimiento_Superior.Text                = tbl_Anio_Escolar.Rows[0].Cells[9].Text.Replace(",", ".");
            txtNumero_Periodos.Text                     = tbl_Anio_Escolar.Rows[0].Cells[10].Text;
            ddlColegio.SelectedValue                    = tbl_Anio_Escolar.Rows[0].Cells[11].Text;
        }
        }catch (Exception) {}
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Editar" });

    }
}