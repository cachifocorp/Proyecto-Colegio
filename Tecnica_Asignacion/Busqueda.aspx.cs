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

public partial class Tecnica_Asignacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.cargar();
        }
    }

    public void cargar()
    {
        try
        {
            Grado objGrado                  = new Grado();
            OperacionGrado objOperGrado     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataView dv                     = objOperGrado.ConsultarGrado(objGrado).DefaultView;
            dv.RowFilter                    = "id > 6 AND id < 13";
            clsFunciones.enlazarCombo(dv,ddlGrado);
        }
        catch (Exception ex)
        {

        }

    }

    public void vertbl_Asignacion () {
        try
        {
            if (int.Parse(ddlGrado.SelectedValue.ToString()) > 6)
            {
                Salon objSalon                      = new Salon();
                OperacionSalon objOperSalon         = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
                objSalon.id_grado                   = int.Parse(ddlGrado.SelectedValue.ToString());
                tbl_Asignacion.DataSource           = objOperSalon.ConsultarSalon(objSalon);
                tbl_Asignacion.DataBind();
            }
            else
            {
                tbl_Asignacion.DataSource = null;
                tbl_Asignacion.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Asignacion();
    }
    protected void tbl_Asignacion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Tecnica", Entidad = "Asignacion", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Asignacion.Rows[e.NewSelectedIndex].Cells[0].Text) });
    }

    protected void tbl_Asignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Asignacion.PageIndex = e.NewPageIndex;
        vertbl_Asignacion();
    }
}