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

public partial class Pensum_Asignacion_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try {
            Asignacion objAsignacion                    = new Asignacion();
            OperacionAsignacion objOperAsignacion       = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_salon                      = int.Parse(ddlSalon.SelectedValue.ToString());
            objAsignacion.id_materia                    = int.Parse(ddlMateria.SelectedValue.ToString());
            objAsignacion.id_docente                    = int.Parse(ddlDocente.SelectedValue.ToString());
            objAsignacion.intensidad                    = int.Parse(txtIntensidad.Text);
            objAsignacion.tecnica                       = int.Parse(ddlTecnica.SelectedValue.ToString());
            objAsignacion.id_usuario                    = int.Parse(Session["id_usuario"].ToString());
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar")){
                objOperAsignacion.InsertarAsignacion(objAsignacion);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Asignacion", Pagina = "Busqueda", Accion = "Agrego" });
            }else {
                string id                               = Page.RouteData.Values["Id"].ToString();
                objAsignacion.id                        = int.Parse(clsEncriptar.Desencriptar(id));
                objOperAsignacion.ActualizarAsignacion(objAsignacion);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Asignacion", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) {}
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Asignacion", Pagina = "Busqueda", Accion = "Cancelo" });
    }
    
    public void cargar () {
        try {
            Anio_Escolar objAnio_Escolar                = (Anio_Escolar)Session["anioEscolar"];
            Grado objGrado                              = new Grado();
            OperacionGrado objOperGrado                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                    = objAnio_Escolar.id;
            ddlGrado.DataSource                         = objOperGrado.ConsultarGrado(objGrado);
            ddlGrado.DataValueField                     = "id";
            ddlGrado.DataTextField                      = "descripcion";
            ddlGrado.DataBind();

            
            Docente objDocente                          = new Docente();
            OperacionDocente objOperDocente             = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

            DataTable dtDocente                         = objOperDocente.ConsultarDocente(objDocente);
            dtDocente.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDocente.DataSource                       = dtDocente;
            ddlDocente.DataValueField                   = "id";
            ddlDocente.DataTextField                    = "nombre_completo";
            ddlDocente.DataBind();
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita")){
                Asignacion objAsignacion                = new Asignacion();
                OperacionAsignacion objOperAsignacion   = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Asignacion                 = new GridView();
                string id                               = Page.RouteData.Values["Id"].ToString();
                objAsignacion.id                        = int.Parse(clsEncriptar.Desencriptar(id));
                tbl_Asignacion.DataSource               = objOperAsignacion.ConsultarAsignacion(objAsignacion);
                tbl_Asignacion.DataBind();
                
                ddlGrado.SelectedValue                  = tbl_Asignacion.Rows[0].Cells[11].Text;
                Salon objSalon = new Salon();
                OperacionSalon objOperSalon             = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objSalon.id_grado                       = int.Parse(ddlGrado.SelectedValue.ToString());
                DataTable dts                           = objOperSalon.ConsultarSalon(objSalon);
                clsFunciones.enlazarCombo(dts, ddlSalon);
                ddlSalon.SelectedValue                  = tbl_Asignacion.Rows[0].Cells[1].Text;
                Materia objMateria                      = new Materia();
                OperacionMateria objOperMateria         = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objMateria.id_grado                     = int.Parse(ddlGrado.SelectedValue.ToString());
                clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria), ddlMateria);
                ddlMateria.SelectedValue                = tbl_Asignacion.Rows[0].Cells[2].Text;
                ddlDocente.SelectedValue                = tbl_Asignacion.Rows[0].Cells[3].Text;
                txtIntensidad.Text                      = tbl_Asignacion.Rows[0].Cells[4].Text;
                ddlTecnica.Text                         = tbl_Asignacion.Rows[0].Cells[5].Text;
            }
        }
        catch (Exception) {}
    }
    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSalon.Items.Clear();
        ListItem l                      = new ListItem();
        l.Text                          = "--- SELECCIONE UNO ---";
        l.Value                         = "0";
        ddlSalon.Items.Add(l);
        Salon objSalon                  = new Salon();
        OperacionSalon objOperSalon     = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objSalon.id_grado               = int.Parse(ddlGrado.SelectedValue.ToString());
        clsFunciones.enlazarCombo(objOperSalon.ConsultarSalon(objSalon),ddlSalon);
        ddlMateria.Items.Clear();

        ddlMateria.Items.Add(l);
        Materia objMateria              = new Materia();
        OperacionMateria objOperMateria = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMateria.id_grado             = int.Parse(ddlGrado.SelectedValue.ToString());
        clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria), ddlMateria);
    }

    public void seleccionar_Grado(DropDownList ddlGrado, DropDownList ddlSalon,DataTable dts)
    {
        if (ddlSalon.SelectedValue != null && int.Parse(ddlSalon.SelectedValue) > 0)
        {
            ddlGrado.SelectedValue = dts.Select("id=" + ddlSalon.SelectedValue)[0][5].ToString();
        }
    }

}