using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Anio_Escolar_Periodo_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDescripcion.Focus();
        Form.DefaultButton = btnBuscar.UniqueID;
    }

    private void vertbl_Anio_Escolar_Periodo()
    {
        try
        {
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAnio_Escolar_Periodo.id_usuario                          = int.Parse(Session["Id_usuario"].ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objAnio_Escolar_Periodo.descripcion                     = txtDescripcion.Text.Trim();
            }
            else
            {
                objAnio_Escolar_Periodo.descripcion                     = null;
            }
            tbl_Anio_Escolar_Periodo.DataSource                         = objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo);
            tbl_Anio_Escolar_Periodo.DataBind();
            if (tbl_Anio_Escolar_Periodo.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception)
        {
        }
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Anio_Escolar_Periodo objAnio_Escolar_Periodo = new Anio_Escolar_Periodo();
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Anio_Escolar_Periodo.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objAnio_Escolar_Periodo.id = int.Parse(row.Cells[1].Text);
                        objAnio_Escolar_Periodo.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOperAnio_Escolar_Periodo.EliminarAnio_Escolar_Periodo(objAnio_Escolar_Periodo);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Anio_Escolar_Periodo();
    }
    protected void tbl_Anio_Escolar_Periodo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Anio_Escolar_Periodo", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Anio_Escolar_Periodo.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Anio_Escolar_Periodo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Anio_Escolar_Periodo.PageIndex = e.NewPageIndex;
        vertbl_Anio_Escolar_Periodo();
    }
}