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

public partial class Pensum_Desempeno_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtDescripcion.Focus();
            Form.DefaultButton = btnGuardar.UniqueID;
            this.cargar();
        }
    }

    public void cargar()
    {
        Anio_Escolar objAnio_Escolar                                = (Anio_Escolar)Session["anioEscolar"];
        Anio_Escolar_Periodo objAnio_Escolar_Periodo                = new Anio_Escolar_Periodo();
        objAnio_Escolar_Periodo.id_anio_escolar                     = objAnio_Escolar.id;
        OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo),ddlPeriodo);
        Grado objGrado                                              = new Grado();
        OperacionGrado objOperGrado                                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objGrado.id_anio_escolar                                    = objAnio_Escolar.id;
        clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado),ddlGrado);

        string accion                                               = Page.RouteData.Values["Accion"].ToString();
        if (accion.Equals("Edita"))
        {
            string id                                               = Page.RouteData.Values["Id"].ToString();
            Desempeno objDesempeno                                  = new Desempeno();
            OperacionDesempeno objOperDesempeno                     = new OperacionDesempeno(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Desempeno                                  = new GridView();
            objDesempeno.id                                         = int.Parse(id);
            tbl_Desempeno.DataSource                                = objOperDesempeno.ConsultarDesempeno(objDesempeno);
            tbl_Desempeno.DataBind();
            txtDescripcion.Text                                     = HttpUtility.HtmlDecode(tbl_Desempeno.Rows[0].Cells[1].Text);
            ddlPeriodo.SelectedValue                                = tbl_Desempeno.Rows[0].Cells[2].Text;
            ddlGrado.SelectedValue                                  = tbl_Desempeno.Rows[0].Cells[3].Text;
            Materia objMateria                                      = new Materia();
            OperacionMateria objOperMateria                         = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMateria.id_grado                                     = int.Parse(ddlGrado.SelectedValue.ToString());
            clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria),ddlMateria);
            ddlMateria.SelectedValue                                = tbl_Desempeno.Rows[0].Cells[4].Text;
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try {
            Desempeno objDesempeno                                      = new Desempeno();
            OperacionDesempeno objOperDesempeno                         = new OperacionDesempeno(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objDesempeno.descripcion                                    = txtDescripcion.Text;
            objDesempeno.id_anio_escolar_periodo                        = int.Parse(ddlPeriodo.SelectedValue.ToString());
            objDesempeno.id_grado                                       = int.Parse(ddlGrado.SelectedValue.ToString());
            objDesempeno.id_materia                                     = int.Parse(ddlMateria.SelectedValue.ToString());
            objDesempeno.id_usuario                                     = int.Parse(Session["id_usuario"].ToString());
            string accion                                           = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperDesempeno.InsertarDesempeno (objDesempeno);
                Response.RedirectToRoute("General", new { Modulo    = "Pensum", Entidad = "Desempeno", Pagina = "Busqueda", Accion = "Agrego" });
            } else {
                objDesempeno.id                                         = int.Parse(Page.RouteData.Values["id"].ToString());
                objOperDesempeno.ActualizarDesempeno(objDesempeno);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Desempeno", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) {}
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Desempeno", Pagina = "Busqueda", Accion = "Cancelo" });
    }
    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria.Items.Clear();
        ListItem l                              = new ListItem();
        l.Text                                  = " --- SELECCIONE UNO --- ";
        l.Value                                 = "0";
        ddlMateria.Items.Add(l);
        if (int.Parse(ddlGrado.SelectedValue.ToString())>0) {
        Materia objMateria                      = new Materia();
        OperacionMateria objOperMateria         = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMateria.id_grado                     = int.Parse(ddlGrado.SelectedValue.ToString());
        clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria),ddlMateria);
        }
    }
}