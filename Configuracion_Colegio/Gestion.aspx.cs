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

public partial class Configuracion_Colegio_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) {
            Form.DefaultButton = btnGuardar.UniqueID;
            this.cargar();
        }
    }

    public void cargar () {
        try {
            Administrativo objAdministrativo                = new Administrativo();
            OperacionAdministrativo objOperAdministrativo   = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataView dtv_Municipio                          = objOperAdministrativo.ConsultarAdministrativo(objAdministrativo).DefaultView;
            dtv_Municipio.RowFilter                         = "id_tipo=" + 967;
            this.enlazarCombo(dtv_Municipio, ddlRector);
            dtv_Municipio.RowFilter                         = "id_tipo=" + 968;
            this.enlazarCombo(dtv_Municipio, ddlSecretaria);
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Editar")){
                Colegio objColegio                          = new Colegio();
                OperacionColegio objOperColegio             = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Colegio                        = new GridView();
                objColegio.id                               = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                tbl_Colegio.DataSource                      = objOperColegio.ConsultarColegio(objColegio);
                tbl_Colegio.DataBind();
                txtDescripcion.Text                         = HttpUtility.HtmlDecode(tbl_Colegio.Rows[0].Cells[1].Text);
                txtEslogan.Text                             = HttpUtility.HtmlEncode(tbl_Colegio.Rows[0].Cells[2].Text);
                txtNumero_Cuenta.Text                       = tbl_Colegio.Rows[0].Cells[3].Text;
                ddlRector.SelectedValue                     = tbl_Colegio.Rows[0].Cells[4].Text;
                ddlSecretaria.SelectedValue                 = tbl_Colegio.Rows[0].Cells[5].Text;
            }
        }
        catch (Exception) {}
    }

    public void enlazarCombo(DataView dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource                     = dts;
        ddlCombo.DataValueField                 = "id";
        ddlCombo.DataTextField                  = "nombre_completo";
        ddlCombo.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Colegio objColegio                          = new Colegio();
        OperacionColegio objOperColegio             = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objColegio.nombre                           = txtDescripcion.Text;
        objColegio.eslogan                          = txtEslogan.Text;
        objColegio.banco_numero_cuenta              = txtNumero_Cuenta.Text;
        objColegio.id_rector                        = int.Parse(ddlRector.SelectedValue.ToString());
        objColegio.id_secretaria                    = int.Parse(ddlSecretaria.SelectedValue.ToString());
        objColegio.id_usuario                       = int.Parse(Session["id_usuario"].ToString());
        string accion                               = Page.RouteData.Values["Accion"].ToString();
        if (accion.Equals("Agregar")) {
            objOperColegio.InsertarColegio(objColegio);
            Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Agrego" });
        }else {
           objColegio.id                           = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString()));
           objOperColegio.ActualizarColegio(objColegio);
           Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Edito" });
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Administracion", Entidad = "Administracion", Pagina = "Default", Accion = "Cancelo" });
    }
}