using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;

public partial class Configuracion_Usuario_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton      = btnBuscar.UniqueID;
        if (!IsPostBack)
        {
            txtUsuario.Focus();
            cargar();
        }
    }

    public void cargar () {
        try
        {
            Usuario_Tipo objUsuario_Tipo                    = new Usuario_Tipo();
            OperacionUsuario_Tipo objOperUsuario_Tipo       = new OperacionUsuario_Tipo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperUsuario_Tipo.ConsultarUsuario_Tipo(objUsuario_Tipo),ddlTipo_Usuario);
            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Usuario();
    }

    public void vertbl_Usuario () {
        try
        {
            
            Usuario objUsuario                          = new Usuario();
            OperacionUsuario objOperUsuario             = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            if (!string.IsNullOrEmpty(txtUsuario.Text))
            {
                objUsuario.usuario                      = txtUsuario.Text;
            }else {
                objUsuario.usuario                      = null;
            }
            objUsuario.id_tipo_usuario                  = int.Parse(ddlTipo_Usuario.SelectedValue.ToString());
            tbl_Usuario.DataSource                      = objOperUsuario.ConsultarUsuario(objUsuario);
            tbl_Usuario.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Usuario objUsuario = new Usuario();
            OperacionUsuario objOpeUsuario = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Usuario.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objUsuario.id = int.Parse(row.Cells[1].Text);
                        objOpeUsuario.EliminarUsuario(objUsuario);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void tbl_Usuario_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Usuario.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Usuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Usuario.PageIndex = e.NewPageIndex;
        vertbl_Usuario();
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Configuracion", Entidad = "Usuario", Pagina = "Gestion", Accion = "Agrega"});
    }
}