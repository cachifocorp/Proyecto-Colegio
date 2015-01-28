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

public partial class Configuracion_Permiso_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnBuscar.UniqueID;
            ddlMenu.Focus();
            this.cargar();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Permiso objPermiso                  = new Permiso();
            OperacionPermiso objOpePermiso      = new OperacionPermiso(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Permiso.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objPermiso.id           = int.Parse(row.Cells[1].Text);
                        objOpePermiso.EliminarPermiso(objPermiso);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Permiso();
    }

    public void vertbl_Permiso()
    {
        try
        {
            Permiso objPermiso                      = new Permiso();
            OperacionPermiso objOpePermiso          = new OperacionPermiso(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objPermiso.id_menu                      = int.Parse(ddlMenu.SelectedValue.ToString());
            objPermiso.id_usuario_tipo              = int.Parse(ddlUsuario_Tipo.SelectedValue.ToString());
            if (int.Parse(ddlMenu.SelectedValue) > 0 && int.Parse(ddlUsuario_Tipo.SelectedValue) == 0) {
                tbl_Permiso.DataSource = objOpePermiso.ConsultarPermiso(objPermiso);
            }else {
                tbl_Permiso.DataSource = objOpePermiso.ConsultarPermiso_Modulo(objPermiso);
            }
            tbl_Permiso.DataBind();
            if (tbl_Permiso.Rows.Count == 0)
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
    protected void tbl_Permiso_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Permiso", Pagina = "Gestion", Accion = "Edita", Id = tbl_Permiso.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Permiso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Permiso.PageIndex = e.NewPageIndex;
        vertbl_Permiso();
    }

    public void cargar () {
        try {
            Menus objMenu                               = new Menus();
            OperacionMenu objOperMenu                   = new OperacionMenu(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Usuario_Tipo objUsuario_Tipo                = new Usuario_Tipo();
            OperacionUsuario_Tipo objOperUsuario_Tipo   = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataView dtv_Menu                           = objOperMenu.ConsultarMenu(objMenu).DefaultView;
            dtv_Menu.RowFilter                          = "url = '#'";
            clsFunciones.enlazarCombo(dtv_Menu,ddlMenu);
            clsFunciones.enlazarCombo(objOperUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo),ddlUsuario_Tipo);
        }
        catch (Exception) {}
    }
}