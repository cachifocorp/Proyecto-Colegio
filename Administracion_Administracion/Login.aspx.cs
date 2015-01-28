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

public partial class Administracion_Administracion_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Form.DefaultButton = btnLogin.UniqueID;
            if (!IsPostBack)
            {
                
                Session.Clear();
                Session.Abandon();    
            }
            
        }
        catch { }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            inicializarSesion();
            Usuario objUsuario                      = new Usuario();
            OperacionUsuario objOperUsuario         = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objUsuario.usuario                      = txtUsuario.Text.Trim();
            objUsuario.password                     = txtPass.Text.Trim();
            objOperUsuario.ConsultarUsuario(objUsuario);
            GridView tbl_Usuario                    = new GridView();
            tbl_Usuario.DataSource                  = objOperUsuario.ConsultarUsuario(objUsuario);
            tbl_Usuario.DataBind();
            if (tbl_Usuario.Rows.Count > 0)
            {
                Session["id_usuario"]                   = tbl_Usuario.Rows[0].Cells[0].Text;
                Session["usuario"]                      = HttpUtility.HtmlDecode(tbl_Usuario.Rows[0].Cells[1].Text);
                Session["id_usuario_tipo"]              = tbl_Usuario.Rows[0].Cells[5].Text;
                OperacionAnio_Escolar objAnioEscolar    = new OperacionAnio_Escolar(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                Anio_Escolar ObjAnio                    = new Anio_Escolar();
                GridView tbl_AnioEscolar                = new GridView();
                tbl_AnioEscolar.DataSource              = objAnioEscolar.ConsultarMaximo(new Anio_Escolar());
                tbl_AnioEscolar.DataBind();
                if (tbl_AnioEscolar.Rows.Count > 0)
                {
                    ObjAnio.id                              = int.Parse(tbl_AnioEscolar.Rows[0].Cells[0].Text);
                    ObjAnio.descripcion                     = int.Parse(tbl_AnioEscolar.Rows[0].Cells[1].Text);
                    ObjAnio.fecha_inicio                    = DateTime.Parse(tbl_AnioEscolar.Rows[0].Cells[2].Text);
                    ObjAnio.fecha_fin                       = DateTime.Parse(tbl_AnioEscolar.Rows[0].Cells[3].Text);
                    ObjAnio.nota_minima                     = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[4].Text);
                    ObjAnio.nota_maxima                     = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[5].Text);
                    ObjAnio.rendimiento_bajo                = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[6].Text);
                    ObjAnio.rendimiento_basico              = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[7].Text);
                    ObjAnio.rendimiento_alto                = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[8].Text);
                    ObjAnio.rendimiento_superior            = decimal.Parse(tbl_AnioEscolar.Rows[0].Cells[9].Text);
                    ObjAnio.numero_periodos                 = int.Parse(tbl_AnioEscolar.Rows[0].Cells[10].Text);
                    ObjAnio.id_colegio                      = int.Parse(tbl_AnioEscolar.Rows[0].Cells[11].Text);
                    Session["anioEscolar"]                  = ObjAnio;

                    Listado objListado = new Listado();
                    OperacionListado objOperListado = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                    Session["listado"] = objOperListado.ConsultarListado(objListado);
                    Menus objMenu = new Menus();
                    OperacionMenu objOperMenu               = new OperacionMenu(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                    objMenu.id_tipo_usuario                 = int.Parse(tbl_Usuario.Rows[0].Cells[5].Text);
                    Session["menu"]                         = objOperMenu.ConsultarMenu(objMenu);
                }
                txtUsuario.Text                             = "";
                txtPass.Text                                = "";
                Response.Redirect("~/Administracion/Administracion/Default");
            }
            else
            {
                ShowNotification("Login", Resources.Mensaje.msjLogin, "Info");
            }
        }
        catch { }
    }

    private void inicializarSesion()
    {
        Session["id_usuario"] = "";
        Session["usuario"] = "";
        Session["permisosmenu"] = "";
        Session["anioEscolar"] = "";
        Session["listado"] = "";
        Session["id_usuario_tipo"] = "";
        Session["menu"] = "";

    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
}