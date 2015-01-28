using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Listado_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { 
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Listado objListado                              = new Listado();
            OperacionListado objOpeListado                  = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Listado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objListado.id                       = int.Parse(row.Cells[1].Text);
                        objListado.id_usuario               = int.Parse(Session["id_usuario"].ToString());
                        objOpeListado.EliminarListado(objListado);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Listado();
    }
    protected void tbl_Listado_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Listado", Pagina = "Gestion", Accion = "Edita", Id = tbl_Listado.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Listado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Listado.PageIndex = e.NewPageIndex;
        vertbl_Listado();
    }

    public void cargar()
    {
        try {
            Listado_Tipo objListado_Tipo                                = new Listado_Tipo();
            OperacionListado_Tipo objOperListado_Tipo                   = new OperacionListado_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            ddlTipo_Listado.DataSource                                  = objOperListado_Tipo.ConsultarListado_Tipo(objListado_Tipo);
            ddlTipo_Listado.DataValueField                              = "id";
            ddlTipo_Listado.DataTextField                               = "descripcion";
            ddlTipo_Listado.DataBind();
        }
        catch (Exception) {}
    }

    public void vertbl_Listado()
    {
        try
        {
            Listado objListado                          = new Listado();
            OperacionListado objOpeListado              = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objListado.id_tipo_listado                  = int.Parse(ddlTipo_Listado.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objListado.descripcion = txtDescripcion.Text.Trim();
            }
            else
            {
                objListado.descripcion = null;
            }
            tbl_Listado.DataSource = objOpeListado.ConsultarListado(objListado);
            tbl_Listado.DataBind();
            if (tbl_Listado.Rows.Count == 0)
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