
using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Proceso_Observador_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDescripcion.Focus();
            Form.DefaultButton = btnBuscar.UniqueID;
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Observador();
    }

    public void vertbl_Observador()
    {
        try
        {
            Observador objObservador                = new Observador();
            OperacionObservador objOperObservador   = new OperacionObservador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (txtDescripcion.Text == "")
            {
                objObservador.id_estudiante = 0;
            }
            else
            {
                objObservador.id_estudiante = Convert.ToInt64(txtDescripcion.Text);
            }
            tbl_Observador.DataSource = objOperObservador.ConsultarObservador(objObservador);
            tbl_Observador.DataBind();
            if (tbl_Observador.Rows.Count == 0)
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
    protected void tbl_Observador_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Observador.PageIndex = e.NewPageIndex;
        vertbl_Observador();
    }
    protected void tbl_Observador_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Observador.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Observador objObservador = new Observador();
            OperacionObservador objOperObsevador = new OperacionObservador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Observador.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objObservador.id = int.Parse(row.Cells[1].Text);
                        objObservador.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOperObsevador.EliminarObservador(objObservador);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Observador", Pagina = "Gestion", Accion = "Agregar" });
    }
}