using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Pensum_Area_Gestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack) {
            Form.DefaultButton = btnGuardar.UniqueID;
            this.cargar();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Area objArea                        = new Area();
            Anio_Escolar objAnio_Escolar        = (Anio_Escolar)Session["anioEscolar"];
            OperacionArea objOpeArea            = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objArea.descripcion                 = txtDescripcion.Text;
            objArea.id_anio_escolar             = objAnio_Escolar.id;
            objArea.porcentaje                  = int.Parse(txtPorcentaje.Text);
            objArea.id_usuario                  = int.Parse(Session["id_usuario"].ToString());
            string accion                       = Page.RouteData.Values["Accion"].ToString();
            if (accion.Equals("Agregar"))
            {
                
                objOpeArea.InsertarArea(objArea);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Busqueda", Accion = "Agrego" });
            }
            else
            {
                objArea.id                      = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objOpeArea.ActualizarArea(objArea);
                Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Busqueda", Accion = "Edito" });
            }
        }
        catch (Exception) { }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Busqueda", Accion = "Cancelo" });
    }

    public void cargar () {
        try {
            string accion                       = Page.RouteData.Values["accion"].ToString();
            if (accion.Equals("Edita")){
                Area objArea                        = new Area();
                Anio_Escolar objAnio_Escolar        = (Anio_Escolar)Session["anioEscolar"];
                OperacionArea objOpeArea            = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                GridView tbl_Area                   = new GridView();
                objArea.id                          = int.Parse(clsEncriptar.Desencriptar(Page.RouteData.Values["id"].ToString()));
                objArea.id_anio_escolar             = objAnio_Escolar.id;
                tbl_Area.DataSource                 = objOpeArea.ConsultarArea(objArea);
                tbl_Area.DataBind();
                txtDescripcion.Text                 = HttpUtility.HtmlDecode(tbl_Area.Rows[0].Cells[1].Text);
                txtPorcentaje.Text                  = tbl_Area.Rows[0].Cells[3].Text;
            }
        }
        catch (Exception) {}
    }
}