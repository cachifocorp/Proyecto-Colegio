using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pensum_Indicador_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton          = btnGuardar.UniqueID;
        if (!IsPostBack)
        {
            txtDescripcion.Focus();
            cargar();
        }
    }
    
    public void cargar () {
        try
        {
            Anio_Escolar objAnio_Escolar                                                = (Anio_Escolar)Session["anioEscolar"];
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                                = new Anio_Escolar_Periodo();
            objAnio_Escolar_Periodo.id_anio_escolar                                     = objAnio_Escolar.id;
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo                   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo), ddlPeriodo);
            Grado objGrado = new Grado();
            OperacionGrado objOperGrado                                                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                                                    = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado), ddlGrado);
            
            string accion = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita"))
            {
                Indicador objIndicador                                                      = new Indicador();
                OperacionIndicador objOperIndicador                                         = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objIndicador.id                                                             = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                GridView tbl_Indicador                                                      = new GridView();
                tbl_Indicador.DataSource                                                    = objOperIndicador.ConsultarIndicador(objIndicador);
                tbl_Indicador.DataBind();
                txtDescripcion.Text                                                         = HttpUtility.HtmlDecode(tbl_Indicador.Rows[0].Cells[1].Text);
                ddlPeriodo.SelectedValue                                                    = tbl_Indicador.Rows[0].Cells[2].Text;
                ddlGrado.SelectedValue                                                      = tbl_Indicador.Rows[0].Cells[3].Text;
                Materia objMateria                                                          = new Materia();
                OperacionMateria objOperMateria                                             = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objMateria.id_grado                                                         = int.Parse(ddlGrado.SelectedValue.ToString());
                clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria), ddlMateria);
                obtenerSaber();
                ddlMateria.SelectedValue                                                    = tbl_Indicador.Rows[0].Cells[4].Text;
                ddlSaber.SelectedValue                                                      = tbl_Indicador.Rows[0].Cells[5].Text;
            }
        }
        catch (Exception)
        {
        }
    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Indicador objIndicador                  = new Indicador();
            OperacionIndicador objOperIndicador     = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objIndicador.descripcion                = txtDescripcion.Text;
            objIndicador.id_anio_escolar_periodo    = int.Parse(ddlPeriodo.SelectedValue.ToString());
            objIndicador.id_grado                   = int.Parse(ddlGrado.SelectedValue.ToString());
            objIndicador.id_materia                 = int.Parse(ddlMateria.SelectedValue.ToString());
            objIndicador.id_saber                   = int.Parse(ddlSaber.SelectedValue.ToString());
            objIndicador.id_usuario                 = int.Parse(Session["id_usuario"].ToString());
            string accion                           = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperIndicador.InsertarIndicador(objIndicador);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objIndicador.id                     = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperIndicador.ActualizarIndicador(objIndicador);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void obtenerSaber() { 
    ddlSaber.Items.Clear();
        ListItem l = new ListItem();
        l.Value = "0";
        l.Text = "--- SELECCIONE UNO ---";
        ddlSaber.Items.Add(l);
        Calificacion_Configuracion objCalificacion_Configuracion = new Calificacion_Configuracion();
        OperacionCalificacion_Configuracion objOperCalificacion_Configuracion = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objCalificacion_Configuracion.id_periodo = int.Parse(ddlPeriodo.SelectedValue);
        clsFunciones.enlazarCombo(objOperCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objCalificacion_Configuracion), ddlSaber);
    }

    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria.Items.Clear();
        ListItem l                                  = new ListItem();
        l.Text                                      = " --- SELECCIONE UNO --- ";
        l.Value                                     = "0";
        ddlMateria.Items.Add(l);
        if (int.Parse(ddlGrado.SelectedValue.ToString())>0) {
            Materia objMateria                      = new Materia();
            OperacionMateria objOperMateria         = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMateria.id_grado                     = int.Parse(ddlGrado.SelectedValue.ToString());
            clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria),ddlMateria);
        }
    }
    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        obtenerSaber();
    }
}