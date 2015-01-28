using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Sede_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Anio_Escolar objAnio_Escolar                = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                                = new Sede();
            OperacionSede objOperSede                   = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            
            foreach (GridViewRow row in tbl_Sede.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objSede.id                      = int.Parse(row.Cells[1].Text);
                        objSede.id_usuario              = int.Parse(Session["id_usuario"].ToString());
                        objOperSede.EliminarSede(objSede);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Sede();
    }
    protected void tbl_Sede_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Sede", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Sede.Rows[e.NewSelectedIndex].Cells[1].Text )});
    }
    protected void tbl_Sede_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Sede.PageIndex = e.NewPageIndex;
        vertbl_Sede();
    }

    public void vertbl_Sede () {
        try {
            Anio_Escolar objAnio_Escolar            = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                            = new Sede();
            OperacionSede objOperSede               = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objSede.id_colegio                      = objAnio_Escolar.id_colegio;
            tbl_Sede.DataSource                     = objOperSede.ConsultarSede(objSede);
            tbl_Sede.DataBind();
            if (tbl_Sede.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception){}
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
}