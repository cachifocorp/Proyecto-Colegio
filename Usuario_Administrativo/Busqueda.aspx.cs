using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;
using System.Data;

public partial class Usuario_Administrativo_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void vertbl_Administrativo()
    {
        try
        {
            Administrativo objAdministrativo                    = new Administrativo();
            OperacionAdministrativo objOperAdministrativo       = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAdministrativo.id_tipo                           = int.Parse(ddlTipo.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objAdministrativo.nombre_completo               = txtDescripcion.Text.Trim();
            }
            else
            {
                objAdministrativo.nombre_completo               = null;
            }
            tbl_Administrativo.DataSource                       = objOperAdministrativo.ConsultarAdministrativo(objAdministrativo);
            tbl_Administrativo.DataBind();
            if (tbl_Administrativo.Rows.Count == 0)
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
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Administrativo objAdministrativo = new Administrativo();
            OperacionAdministrativo objOperAdministrativo = new OperacionAdministrativo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Administrativo.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objAdministrativo.id = int.Parse(row.Cells[1].Text);
                        objAdministrativo.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOperAdministrativo.EliminarAdministrativo(objAdministrativo);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Administrativo();
    }

    protected void tbl_Administrativo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Usuario", Entidad = "Administrativo", Pagina = "Gestion", Accion = "Edita", Id = tbl_Administrativo.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Administrativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Administrativo.PageIndex = e.NewPageIndex;
        vertbl_Administrativo();
    }

    public void cargar (){
        try {
            Listado objListado                          = new Listado();
            OperacionListado objOperListado             = new OperacionListado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objListado.id_tipo_listado                  = 12;
            this.enlazarCombo(objOperListado.ConsultarListado(objListado),ddlTipo);
        }
        catch (Exception){}
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource = dts;
        ddlCombo.DataValueField = "id";
        ddlCombo.DataTextField = "descripcion";
        ddlCombo.DataBind();
    }
}