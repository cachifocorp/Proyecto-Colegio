using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Asignacion_Grado_Busqueda : System.Web.UI.Page
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
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Grado objGrado                          = new Grado();
            OperacionGrado objOpeGrado              = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Grado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow                 = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objGrado.id                 = int.Parse(row.Cells[1].Text);
                        objGrado.id_usuario         = int.Parse(Session["id_usuario"].ToString());
                        objOpeGrado.EliminarGrado(objGrado);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Grado();
    }
    protected void tbl_Grado_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Grado", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Grado.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Grado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Grado.PageIndex = e.NewPageIndex;
        vertbl_Grado();
    }

    public void vertbl_Grado () {
        try
        {
            Anio_Escolar objAnio_Escolar                    = (Anio_Escolar)Session["anioEscolar"];
            Grado objGrado                                  = new Grado();
            OperacionGrado objOperGrado                     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                        = objAnio_Escolar.id;
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objGrado.descripcion                        = txtDescripcion.Text.Trim();
            }
            else
            {
                objGrado.descripcion = null;
            }
            tbl_Grado.DataSource = objOperGrado.ConsultarGrado(objGrado);
            tbl_Grado.DataBind();
            if (tbl_Grado.Rows.Count == 0)
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
}