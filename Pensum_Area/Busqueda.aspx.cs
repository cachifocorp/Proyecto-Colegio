using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Pensum_Area_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            txtDescripcion.Focus();
            Form.DefaultButton = btnBuscar.UniqueID;
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Area objArea                    = new Area();
            OperacionArea objOpeArea        = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Area.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objArea.id = int.Parse(row.Cells[1].Text);
                        objArea.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOpeArea.EliminarArea(objArea);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Area();
    }

    public void vertbl_Area () {
        try {
            Area objArea                    = new Area();
            Anio_Escolar objAnio_Escolar    = (Anio_Escolar)Session["anioEscolar"];
            OperacionArea objOpeArea        = new OperacionArea(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objArea.id_anio_escolar         = objAnio_Escolar.id;
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objArea.descripcion         = txtDescripcion.Text.Trim();
            }
            else
            {
                objArea.descripcion         = null;
            }
            tbl_Area.DataSource             = objOpeArea.ConsultarArea(objArea);
            tbl_Area.DataBind();
            if (tbl_Area.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception) {}
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
    protected void tbl_Area_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Area", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Area.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Area_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Area.PageIndex = e.NewPageIndex;
        vertbl_Area();
    }
}