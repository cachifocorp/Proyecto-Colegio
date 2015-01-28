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

public partial class Configuracion_Sede_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {  
            Form.DefaultButton = btnGuardar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void cargar () {
        try {
            Anio_Escolar objAnio_Escolar                    = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                                    = new Sede();
            OperacionSede objOperSede                       = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Administrativo objAdministrativo                = new Administrativo();
            OperacionAdministrativo objOperAdministrativo   = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataView dtv_Municipio                          = objOperAdministrativo.ConsultarAdministrativo(objAdministrativo).DefaultView;
            dtv_Municipio.RowFilter                         = "id_tipo=" + 969;
            this.enlazarCombo(dtv_Municipio,ddlCoordinador);
            dtv_Municipio.RowFilter                         = "id_tipo=" + 970;
            this.enlazarCombo(dtv_Municipio, ddlSecretariaCoordinacion);
            string accion                                   = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Edita")){
                string id                                   = clsEncriptar.Desencriptar(Page.RouteData.Values["Id"].ToString());
                objSede.id = int.Parse(id);
                objSede.id_colegio                          = objAnio_Escolar.id_colegio;
                GridView tbl_Sede                           = new GridView();
                tbl_Sede.DataSource                         = objOperSede.ConsultarSede(objSede);
                tbl_Sede.DataBind();
                txtDescripcion.Text                         = tbl_Sede.Rows[0].Cells[1].Text;
                txtDireccion.Text                           = tbl_Sede.Rows[0].Cells[3].Text;
                txtTelefono.Text                            = tbl_Sede.Rows[0].Cells[4].Text;
                ddlCoordinador.SelectedValue                = tbl_Sede.Rows[0].Cells[5].Text;
                ddlSecretariaCoordinacion.SelectedValue     = tbl_Sede.Rows[0].Cells[6].Text;
            }
        }
        catch (Exception) {}    
    }

    public void enlazarCombo(DataView dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource = dts;
        ddlCombo.DataValueField = "id";
        ddlCombo.DataTextField = "nombre_completo";
        ddlCombo.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Anio_Escolar objAnio_Escolar        = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                        = new Sede();
            OperacionSede objOperSede           = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objSede.descripcion                 = HttpUtility.HtmlDecode(txtDescripcion.Text);
            objSede.direccion                   = txtDireccion.Text;
            objSede.telefono                    = txtTelefono.Text;
            objSede.id_coordinador              = int.Parse(ddlCoordinador.SelectedValue.ToString());
            objSede.id_secretaria_coordinacion  = int.Parse(ddlSecretariaCoordinacion.SelectedValue.ToString());
            objSede.id_colegio                  = objAnio_Escolar.id_colegio;
            objSede.id_usuario                  = int.Parse(Session["id_usuario"].ToString());
            string accion                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                objOperSede.InsertarSede(objSede);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objSede.id                      = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOperSede.ActualizarSede(objSede);
                Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Busqueda", Accion = "Cancelo" });
    }
}