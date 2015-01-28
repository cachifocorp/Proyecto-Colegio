using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Usuario_Estudiante_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack){
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void vertbl_Estudiante()
    {
        try
        {
            Estudiante objEstudiante                        = new Estudiante();
            OperacionEstudiante objOperEstudiante           = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objEstudiante.documento_numero = Int64.Parse(txtDescripcion.Text.Trim());
            }
            else
            {
                objEstudiante.documento_numero = 0;
            }
            if (!string.IsNullOrEmpty(txtNombre_1.Text))
            {
                objEstudiante.nombre_1 = txtNombre_1.Text;
            }
            else
            {
                objEstudiante.nombre_1 = null;
            }
            if (!string.IsNullOrEmpty(txtNombre_2.Text))
            {
                objEstudiante.nombre_2 = txtNombre_2.Text;
            }
            else
            {
                objEstudiante.nombre_2 = null;
            }
            if (!string.IsNullOrEmpty(txtApellido_1.Text))
            {
                objEstudiante.apellido_1 = txtApellido_1.Text;
            }
            else
            {
                objEstudiante.apellido_1 = null;
            }
            if (!string.IsNullOrEmpty(txtApellido_2.Text))
            {
                objEstudiante.apellido_2 = txtApellido_2.Text;
            }
            else
            {
                objEstudiante.apellido_2 = null;
            }
            tbl_Estudiante.DataSource = objOperEstudiante.ConsultarEstudiante(objEstudiante);
            tbl_Estudiante.DataBind();
            if (tbl_Estudiante.Rows.Count == 0)
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

    public void cargar () {

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Estudiante objEstudiante                            = new Estudiante();
            OperacionEstudiante objOperEstudiante               = new OperacionEstudiante(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Estudiante.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objEstudiante.id                        = int.Parse(row.Cells[1].Text);
                        objEstudiante.id_usuario                = int.Parse(Session["id_usuario"].ToString());
                        objOperEstudiante.EliminarEstudiante(objEstudiante);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Estudiante();
    }
    protected void tbl_Estudiante_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Estudiante", Pagina = "Gestion", Accion = "Edita", Id = tbl_Estudiante.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Estudiante_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Estudiante.PageIndex = e.NewPageIndex;
        vertbl_Estudiante();
    }
}