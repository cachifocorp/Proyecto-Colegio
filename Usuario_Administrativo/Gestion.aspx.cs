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

public partial class Usuario_Administrativo_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnGuardar.UniqueID;
            ddlDocumento_Id_Tipo.Focus();
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Administrativo objAdministrativo                        = new Administrativo();
            OperacionAdministrativo objOperAdministrativo           = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAdministrativo.documento_tipo                        = int.Parse(ddlDocumento_Id_Tipo.SelectedValue.ToString());
            objAdministrativo.documento_numero                      = int.Parse(txtDocumento_Numero.Text);
            objAdministrativo.nombre_completo                       = txtDescripcion.Text;
            objAdministrativo.email                                 = txtEmail.Text;
            objAdministrativo.direccion_completa                    = txtDireccion_Completa.Text;
            objAdministrativo.id_tipo                               = int.Parse(ddlTipo.SelectedValue.ToString());
            objAdministrativo.id_usuario                            = int.Parse(Session["id_usuario"].ToString());
            string accion                                           = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperAdministrativo.InsertarAdministrativo(objAdministrativo);
                Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objAdministrativo.id                                = int.Parse(Page.RouteData.Values["id"].ToString());
                objOperAdministrativo.ActualizarAdministrativo(objAdministrativo);
                Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar()
    {
        try
        {
            Listado objListado                              = new Listado();
            OperacionListado objOperListado                 = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objListado.id_tipo_listado                      = 1;
            enlazarCombo(objOperListado.ConsultarListado(objListado), ddlDocumento_Id_Tipo);
            objListado.id_tipo_listado                      = 12;
            enlazarCombo(objOperListado.ConsultarListado(objListado),ddlTipo);
            Administrativo objAdministrativo                = new Administrativo();
            OperacionAdministrativo objOperAdministrativo   = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            GridView tbl_Administrativo                     = new GridView();
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita"))
            {
                string id                                   = Page.RouteData.Values["id"].ToString();
                objAdministrativo.id                        = int.Parse(id);
                tbl_Administrativo.DataSource               = objOperAdministrativo.ConsultarAdministrativo(objAdministrativo);
                tbl_Administrativo.DataBind();
                ddlDocumento_Id_Tipo.SelectedValue          = tbl_Administrativo.Rows[0].Cells[1].Text;
                txtDocumento_Numero.Text                    = tbl_Administrativo.Rows[0].Cells[2].Text;
                txtDescripcion.Text                         = HttpUtility.HtmlDecode(tbl_Administrativo.Rows[0].Cells[3].Text);
                txtEmail.Text                               = HttpUtility.HtmlDecode(tbl_Administrativo.Rows[0].Cells[4].Text);
                txtDireccion_Completa.Text                  = tbl_Administrativo.Rows[0].Cells[5].Text;
                ddlTipo.SelectedValue                       = tbl_Administrativo.Rows[0].Cells[6].Text;
            }
        }
        catch (Exception) { }
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource                 = dts;
        ddlCombo.DataValueField             = "id";
        ddlCombo.DataTextField              = "descripcion";
        ddlCombo.DataBind();
    }
}