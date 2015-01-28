using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Pensum_Materia_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDescripcion.Focus();
        Form.DefaultButton = btnGuardar.UniqueID;
        if (!IsPostBack) {
            this.cargar();
        }
    }

    public void cargar()
    {
        try
        {
            Grado objGrado                              = new Grado();
            Anio_Escolar objAnio_Escolar                = (Anio_Escolar)Session["anioEscolar"];
            OperacionGrado objOperGrado                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                    = objAnio_Escolar.id;
            ddlGrado.DataSource                         = objOperGrado.ConsultarGrado(objGrado);
            ddlGrado.DataValueField                     = "id";
            ddlGrado.DataTextField                      = "descripcion";
            ddlGrado.DataBind();
            Area objArea                                = new Area();
            OperacionArea objOperArea                   = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objArea.id_anio_escolar                     = objAnio_Escolar.id;
            ddlArea.DataSource                          = objOperArea.ConsultarArea(objArea);
            ddlArea.DataValueField                      = "id";
            ddlArea.DataTextField                       = "descripcion";
            ddlArea.DataBind();
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita"))
            {
                string id                               = Page.RouteData.Values["Id"].ToString();
                Materia objMateria                      = new Materia();
                OperacionMateria objOperMateria         = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Materia                    = new GridView();
                objMateria.id                           = int.Parse(clsEncriptar.Desencriptar(id));                
                tbl_Materia.DataSource                  = objOperMateria.ConsultarMateria(objMateria);
                tbl_Materia.DataBind();
                txtDescripcion.Text                     = HttpUtility.HtmlDecode(tbl_Materia.Rows[0].Cells[1].Text);
                ddlGrado.SelectedValue                  = tbl_Materia.Rows[0].Cells[2].Text;
                ddlArea.SelectedValue                   = tbl_Materia.Rows[0].Cells[3].Text;
                txtOrden_Impresion.Text                 = tbl_Materia.Rows[0].Cells[4].Text;
                txtPorcentaje.Text                      = tbl_Materia.Rows[0].Cells[5].Text;
            }
        }
        catch (Exception) { }

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Materia objMateria                          = new Materia();
            OperacionMateria objOperMateria             = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMateria.descripcion                      = txtDescripcion.Text;
            objMateria.id_grado                         = int.Parse(ddlGrado.SelectedValue.ToString());
            objMateria.id_area                          = int.Parse(ddlArea.SelectedValue.ToString());
            objMateria.orden_impresion                  = int.Parse(txtOrden_Impresion.Text);
            objMateria.porcentaje                       = int.Parse(txtPorcentaje.Text);
            objMateria.id_usuario                       = int.Parse(Session["id_usuario"].ToString());
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperMateria.InsertarMateria(objMateria);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objMateria.id                           = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperMateria.ActualizarMateria(objMateria);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Busqueda", Accion = "Cancelo" });
    }
}