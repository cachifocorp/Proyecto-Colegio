using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Usuario_Tipo_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDescripcion.Focus();
            Form.DefaultButton = btnBuscar.UniqueID;
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Usuario_Tipo objUsuario_Tipo                = new Usuario_Tipo();
            OperacionUsuario_Tipo objOpeUsuario_Tipo    = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Usuario_Tipo.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objUsuario_Tipo.id              = int.Parse(row.Cells[1].Text);
                        objUsuario_Tipo.id_usuario      = int.Parse(Session["id_usuario"].ToString());
                        objOpeUsuario_Tipo.EliminarUsuario_Tipo(objUsuario_Tipo);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Usuario_Tipo();
    }

    public void vertbl_Usuario_Tipo()
    {
        try
        {
            Usuario_Tipo objUsuario_Tipo                    = new Usuario_Tipo();
            OperacionUsuario_Tipo objOpeUsuario_Tipo        = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objUsuario_Tipo.descripcion                 = txtDescripcion.Text.Trim();
            }
            else
            {
                objUsuario_Tipo.descripcion = null;
            }
            tbl_Usuario_Tipo.DataSource                     = objOpeUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo);
            tbl_Usuario_Tipo.DataBind();
            if (tbl_Usuario_Tipo.Rows.Count == 0)
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
    protected void tbl_Usuario_Tipo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario_Tipo", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Usuario_Tipo.Rows[e.NewSelectedIndex].Cells[1].Text )});
    }
    protected void tbl_Usuario_Tipo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Usuario_Tipo.PageIndex = e.NewPageIndex;
        vertbl_Usuario_Tipo();
    }
}