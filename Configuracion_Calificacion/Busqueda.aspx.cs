using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Calificacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Gestion", Accion = "Agrega"});
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Calificacion_Configuracion objCalificacion_Configuracion = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOpeCalificacion_Configuracion = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Calificacion_Configuracion.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objCalificacion_Configuracion.id                = int.Parse(row.Cells[1].Text);
                        objCalificacion_Configuracion.id_usuario        = int.Parse(Session["id_usuario"].ToString());
                        objOpeCalificacion_Configuracion.EliminarCalificacion_Configuracion(objCalificacion_Configuracion);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Calificacion_Configuracion();
    }
    protected void tbl_Calificacion_Configuracion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Calificacion", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Calificacion_Configuracion.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Calificacion_Configuracion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Calificacion_Configuracion.PageIndex = e.NewPageIndex;
        vertbl_Calificacion_Configuracion();
    }

    public void vertbl_Calificacion_Configuracion()
    {
        try
        {
            Calificacion_Configuracion objCalificacion_Configuracion = new Calificacion_Configuracion();
            OperacionCalificacion_Configuracion objOpeCalificacion_Configuracion = new OperacionCalificacion_Configuracion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objCalificacion_Configuracion.descripcion = txtDescripcion.Text.Trim();
            }
            else
            {
                objCalificacion_Configuracion.descripcion = null;
            }
            tbl_Calificacion_Configuracion.DataSource = objOpeCalificacion_Configuracion.ConsultarCalificacion_Configuracion(objCalificacion_Configuracion);
            tbl_Calificacion_Configuracion.DataBind();
            if (tbl_Calificacion_Configuracion.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception) { }
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

}