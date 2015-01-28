using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Data;
using System.Configuration;

public partial class Proceso_Matricula_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack){
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Matricula objMatricula                    = new Matricula();
            OperacionMatricula objOperMatricula       = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Matricula.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objMatricula.id           = int.Parse(row.Cells[1].Text);
                        objOperMatricula.EliminarMatricula(objMatricula);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Matricula();
    }
    
     public void vertbl_Matricula()
    {
        try
        {
            Matricula objMatricula                          = new Matricula();
            OperacionMatricula objOperMatricula             = new OperacionMatricula(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objMatricula.id_salon                           = int.Parse(ddlSalon.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objMatricula.id_estudiante                  = Convert.ToInt64(txtDescripcion.Text);
            }
            else
            {
                objMatricula.id_estudiante = null;
            }
            tbl_Matricula.DataSource = objOperMatricula.ConsultarMatricula(objMatricula);
            tbl_Matricula.DataBind();
            if (tbl_Matricula.Rows.Count == 0)
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

    public void cargar (){
        try {
            Salon objSalon                              = new Salon();
            OperacionSalon objOperGrado                 = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            enlazarCombo(objOperGrado.ConsultarSalon(objSalon),ddlSalon);
        }
        catch (Exception) {}

    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }
    protected void tbl_Matricula_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Matricula", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Matricula.Rows[e.NewSelectedIndex].Cells[1].Text )});
    }
    protected void tbl_Matricula_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Matricula.PageIndex = e.NewPageIndex;
        vertbl_Matricula();
    }
}