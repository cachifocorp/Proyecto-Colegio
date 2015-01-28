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

public partial class Configuracion_Permiso_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Form.DefaultButton = btnGuardar.UniqueID;
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Permiso objPermiso                  = new Permiso();
            OperacionPermiso objOpePermiso      = new OperacionPermiso(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objPermiso.id_menu                  = int.Parse(ddlMenu.SelectedValue.ToString());
            objPermiso.id_usuario_tipo          = int.Parse(ddlUsuario_Tipo.SelectedValue.ToString());
            string accion                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOpePermiso.InsertarPermiso(objPermiso);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objPermiso.id                   = int.Parse(Page.RouteData.Values["id"].ToString());
                objOpePermiso.ActualizarPermiso(objPermiso);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar()
    {
        try
        {
            Menus objMenu                                   = new Menus();
            OperacionMenu objOperMenu                       = new OperacionMenu(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Usuario_Tipo objUsuario_Tipo                    = new Usuario_Tipo();
            OperacionUsuario_Tipo objOperUsuario_Tipo       = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataView dtv_Menu                               = objOperMenu.ConsultarMenu(objMenu).DefaultView;
            dtv_Menu.RowFilter                              = "url <> '#'";
            clsFunciones.enlazarCombo(dtv_Menu, ddlMenu);
            this.enlazarCombo(objOperUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo),ddlUsuario_Tipo);
            string accion = Page.RouteData.Values["accion"].ToString();
            if (accion.Equals("Edita"))
            {
                Permiso objPermiso                          = new Permiso();
                OperacionPermiso objOpePermiso              = new OperacionPermiso(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Permiso                        = new GridView();
                objPermiso.id = int.Parse(Page.RouteData.Values["id"].ToString());
                tbl_Permiso.DataSource = objOpePermiso.ConsultarPermiso(objPermiso);
                tbl_Permiso.DataBind();
                ddlMenu.SelectedValue                       = tbl_Permiso.Rows[0].Cells[1].Text;
                ddlUsuario_Tipo.SelectedValue               = tbl_Permiso.Rows[0].Cells[2].Text;
            }
        }
        catch (Exception) { }
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }
}