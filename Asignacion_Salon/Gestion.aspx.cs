using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;
using System.Data;

public partial class Asignacion_Salon_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Form.DefaultButton = btnGuardar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Salon objSalon                                  = new Salon();
            OperacionSalon objOperSalon                     = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objSalon.descripcion                            = HttpUtility.HtmlDecode(txtDescripcion.Text);
            objSalon.id_sede                                = int.Parse(ddlSede.SelectedValue.ToString());
            objSalon.id_jornada                             = int.Parse(ddlJornada.SelectedValue.ToString());
            objSalon.id_director                            = int.Parse(ddlDirector.SelectedValue.ToString());
            objSalon.id_grado                               = int.Parse(ddlGrado.SelectedValue.ToString());
            objSalon.cantidad                               = int.Parse(txtCantidad.Text);
            objSalon.id_usuario                             = int.Parse(Session["id_usuario"].ToString());
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperSalon.InsertarSalon(objSalon);
                Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objSalon.id                                 = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperSalon.ActualizarSalon(objSalon);
                Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar () {
        try {
            Anio_Escolar objAnio_Escolar                                = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                                                = new Sede();
            OperacionSede objOperSede                                   = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objSede.id_colegio                                          = objAnio_Escolar.id_colegio;
            this.enlazarCombo(objOperSede.ConsultarSede(objSede),ddlSede);
            DataView dtv_Municipio                                      = ((DataTable)Session["listado"]).DefaultView;
            dtv_Municipio.RowFilter                                     = "id_tipo_listado=7";
            this.enlazarCombo(dtv_Municipio, ddlJornada);
            Docente objDocente                                          = new Docente();
            OperacionDocente objOperDocente                             = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable dt                                                = objOperDocente.ConsultarDocente(objDocente);
            dt.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDirector.DataSource                                      = dt;
            ddlDirector.DataValueField                                  = "id";
            ddlDirector.DataTextField                                   = "nombre_completo";
            ddlDirector.DataBind();
            Grado objGrado                                              = new Grado();
            OperacionGrado objOperGrado                                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                                    = objAnio_Escolar.id;
            this.enlazarCombo(objOperGrado.ConsultarGrado(objGrado),ddlGrado);
            Salon objSalon                                              = new Salon();
            OperacionSalon objOperSalon                                 = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Salon                                          = new GridView();
            string accion                                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita")) {
                string id                                               = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objSalon.id                                             = int.Parse(id);
                tbl_Salon.DataSource                                    = objOperSalon.ConsultarSalon(objSalon);
                tbl_Salon.DataBind();
                txtDescripcion.Text                                     = HttpUtility.HtmlEncode(tbl_Salon.Rows[0].Cells[1].Text);
                ddlSede.SelectedValue                                   = tbl_Salon.Rows[0].Cells[2].Text;
                ddlJornada.SelectedValue                                = tbl_Salon.Rows[0].Cells[3].Text;
                ddlDirector.SelectedValue                               = tbl_Salon.Rows[0].Cells[4].Text;
                ddlGrado.SelectedValue                                  = tbl_Salon.Rows[0].Cells[5].Text;
                txtCantidad.Text                                        = tbl_Salon.Rows[0].Cells[6].Text;
            }  
        }
        catch (Exception) {}
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource                     = dts;
        ddlCombo.DataValueField                 = "id";
        ddlCombo.DataTextField                  = "descripcion";
        ddlCombo.DataBind();
    }

    public void enlazarCombo(DataView dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource                     = dts;
        ddlCombo.DataValueField                 = "id";
        ddlCombo.DataTextField                  = "descripcion";
        ddlCombo.DataBind();
    }
}