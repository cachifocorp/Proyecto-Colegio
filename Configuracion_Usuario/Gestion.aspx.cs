using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Usuario_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnGuardar.UniqueID;
        if (!IsPostBack)
        {
            cargar();
        }
    }

    public void cargar () {
        try
        {
            Usuario_Tipo objUsuario_Tipo                        = new Usuario_Tipo();
            OperacionUsuario_Tipo objOperUsuario_Tipo           = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo), ddlTipo_Usuario);
            Usuario objUsuario                                  = new Usuario();
            OperacionUsuario objOperUsuario                     = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            string accion                                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita"))
            {
                GridView tbl_Usuario                            = new GridView();
                objUsuario.id                                   = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                tbl_Usuario.DataSource                          = objOperUsuario.ConsultarUsuario(objUsuario);
                tbl_Usuario.DataBind();
                txtNombres.Text                                 = HttpUtility.HtmlDecode(tbl_Usuario.Rows[0].Cells[1].Text);
                txtApellidos.Text                               = HttpUtility.HtmlDecode(tbl_Usuario.Rows[0].Cells[2].Text);
                txtDocumento.Text                               = HttpUtility.HtmlDecode(tbl_Usuario.Rows[0].Cells[3].Text);
                txtUsuario.Text                                 = HttpUtility.HtmlDecode(tbl_Usuario.Rows[0].Cells[4].Text);
                ddlTipo_Usuario.SelectedValue                   = tbl_Usuario.Rows[0].Cells[5].Text;
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
            Usuario objUsuario                                  = new Usuario();
            OperacionUsuario objOperUsuario                     = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objUsuario.nombre                                   = txtNombres.Text;
            objUsuario.apellido                                 = txtApellidos.Text;
            objUsuario.documento                                = int.Parse(txtDocumento.Text);
            objUsuario.usuario                                  = txtUsuario.Text;
            if (!string.IsNullOrEmpty(txtContraseña.Text))
            {
                objUsuario.password                             = txtContraseña.Text;
                
            }else {
                objUsuario.password                             = null;
            }
            objUsuario.id_tipo_usuario                          = int.Parse(ddlTipo_Usuario.SelectedValue.ToString());
            string accion                                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agrega"))
            {
                objOperUsuario.InsertarUsuario(objUsuario);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Busqueda", Accion = "Agrego" });
                
            }else {
                objUsuario.id                                   = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
                objOperUsuario.ActualizarUsuario(objUsuario);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Busqueda", Accion = "Edito" });

            }
        }
        catch (Exception)
        {
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Busqueda", Accion = "Cancelo" });
    }
}