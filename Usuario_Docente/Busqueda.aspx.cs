using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Usuario_Docente_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack){
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
        }
    }

    public void vertbl_Docente () {
        try {
            Docente objDocente                      = new Docente();
            OperacionDocente objOperDocente         = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objDocente.documento_numero         = int.Parse(txtDescripcion.Text.Trim());
            }
            else
            {
                objDocente.documento_numero         = 0;
            }
            tbl_Docente.DataSource                  = objOperDocente.ConsultarDocente(objDocente);
            tbl_Docente.DataBind();
            if (tbl_Docente.Rows.Count == 0)
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

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Docente objDocente                  = new Docente();
            OperacionDocente objOperDocente     = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Docente.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objDocente.id           = int.Parse(row.Cells[1].Text);
                        objDocente.id_usuario   = int.Parse(Session["id_usuario"].ToString());
                        objOperDocente.EliminarDocente(objDocente);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Docente();
    }
    protected void tbl_Area_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Docente", Pagina = "Gestion", Accion = "Edita", Id = tbl_Docente.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Area_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Docente.PageIndex = e.NewPageIndex;
        vertbl_Docente();
    }
}