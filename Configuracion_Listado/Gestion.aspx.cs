using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Listado_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Form.DefaultButton = btnGuardar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void cargar()
    {
        try
        {
            Listado_Tipo objListado_Tipo                = new Listado_Tipo();
            OperacionListado_Tipo objOperListado_Tipo   = new OperacionListado_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            ddlTipo_Listado.DataSource                  = objOperListado_Tipo.ConsultarListado_Tipo(objListado_Tipo);
            ddlTipo_Listado.DataValueField              = "id";
            ddlTipo_Listado.DataTextField               = "descripcion";
            ddlTipo_Listado.DataBind();
            string accion                               = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita")){
                string id                               = Page.RouteData.Values["Id"].ToString();
                Listado objListado                      = new Listado();
                OperacionListado objOperListado         = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Listado                    = new GridView();
                objListado.id                           = int.Parse(id);
                tbl_Listado.DataSource                  = objOperListado.ConsultarListado(objListado);
                tbl_Listado.DataBind();
                txtDescripcion.Text                     = HttpUtility.HtmlEncode(tbl_Listado.Rows[0].Cells[1].Text);
                ddlTipo_Listado.SelectedValue           = tbl_Listado.Rows[0].Cells[2].Text;
            }
        }
        catch (Exception) { }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Listado objListado                      = new Listado();
            OperacionListado objOperListado         = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objListado.descripcion                  = txtDescripcion.Text;
            objListado.id_tipo_listado              = int.Parse(ddlTipo_Listado.SelectedValue.ToString());
            objListado.id_usuario                   = int.Parse(Session["id_usuario"].ToString());
            string accion = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {

                objOperListado.InsertarListado(objListado);
                Session["listado"]                  = objOperListado.ConsultarListado(objListado);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                string id                           = Page.RouteData.Values["Id"].ToString();
                objListado.id                       = int.Parse(id);
                objOperListado.ActualizarListado(objListado);
                Session["listado"]                  = objOperListado.ConsultarListado(objListado);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Busqueda", Accion = "Cancelo" });
    }
}