using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Usuario_Tipo_Gestion : System.Web.UI.Page
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
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario_Tipo objUsuario_Tipo                        = new Usuario_Tipo();
            OperacionUsuario_Tipo objOpeUsuario_Tipo            = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objUsuario_Tipo.descripcion                         = txtDescripcion.Text;
            objUsuario_Tipo.id_usuario                          = int.Parse(Session["id_usuario"].ToString());
            string accion                                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {

                objOpeUsuario_Tipo.InsertarUsuario_Tipo(objUsuario_Tipo);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objUsuario_Tipo.id                              = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                objOpeUsuario_Tipo.ActualizarUsuario_Tipo(objUsuario_Tipo);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar()
    {
        try
        {
            string accion = Page.RouteData.Values["accion"].ToString();
            if (accion.Equals("Edita"))
            {
                Usuario_Tipo objUsuario_Tipo                    = new Usuario_Tipo();
                OperacionUsuario_Tipo objOpeUsuario_Tipo        = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Usuario_Tipo                       = new GridView();
                objUsuario_Tipo.id                              = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                tbl_Usuario_Tipo.DataSource                     = objOpeUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo);
                tbl_Usuario_Tipo.DataBind();
                txtDescripcion.Text                             = HttpUtility.HtmlDecode(tbl_Usuario_Tipo.Rows[0].Cells[1].Text);
            }
        }
        catch (Exception) { }
    }
}