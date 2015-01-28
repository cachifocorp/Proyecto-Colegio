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

public partial class Pensum_Desempeno_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) {
            Form.DefaultButton = btnBuscar.UniqueID;
            txtDescripcion.Focus();
            this.cargar();
        }
    }

    public void cargar()
    {
        try
        {
            Anio_Escolar objAnio_Escolar                                = (Anio_Escolar)Session["anioEscolar"];
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                = new Anio_Escolar_Periodo();
            objAnio_Escolar_Periodo.id_anio_escolar                     = objAnio_Escolar.id;
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo   = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo),ddlPeriodo);
            Grado objGrado                                              = new Grado();
            OperacionGrado objOperGrado                                 = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                                    = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado),ddlGrado);
        }
        catch (Exception) { }
    }

    public void vertbl_Desempeno () {
        try {
            Desempeno objDesempeno                      = new Desempeno();
            OperacionDesempeno objOperDesempeno         = new OperacionDesempeno(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objDesempeno.id_anio_escolar_periodo        = int.Parse(ddlPeriodo.SelectedValue.ToString());
            objDesempeno.id_grado                       = int.Parse(ddlGrado.SelectedValue.ToString());
            objDesempeno.id_materia                     = int.Parse(ddlMateria.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objDesempeno.descripcion                = txtDescripcion.Text.Trim();
            }
            else
            {
                objDesempeno.descripcion                = null;
            }
            tbl_Desempeno.DataSource                    = objOperDesempeno.ConsultarDesempeno(objDesempeno);
            tbl_Desempeno.DataBind();
            if (tbl_Desempeno.Rows.Count == 0)
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
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Desempeno();
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Desempeno", Pagina = "Gestion", Accion = "Agregar" });

    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Desempeno objDesempeno = new Desempeno();
            OperacionDesempeno objOpeDesempeno = new OperacionDesempeno(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Desempeno.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objDesempeno.id = int.Parse(row.Cells[1].Text);
                        objDesempeno.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOpeDesempeno.EliminarDesempeno(objDesempeno);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Desempeno", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void tbl_Desempeno_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Desempeno", Pagina = "Gestion", Accion = "Edita", Id = tbl_Desempeno.Rows[e.NewSelectedIndex].Cells[1].Text });
    }
    protected void tbl_Desempeno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Desempeno.PageIndex = e.NewPageIndex;
        vertbl_Desempeno();
    }
    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria.Items.Clear();
        ListItem l                          = new ListItem();
        l.Text                              = " --- SELECCIONE UNO --- ";
        l.Value                             = "0";
        ddlMateria.Items.Add(l);
        if (int.Parse(ddlGrado.SelectedValue.ToString()) > 0){
        Materia objMateria                  = new Materia();
        OperacionMateria objOperMateria     = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMateria.id_grado                 = int.Parse(ddlGrado.SelectedValue.ToString());
        clsFunciones.enlazarCombo(objOperMateria.ConsultarMateria(objMateria),ddlMateria);
        }
    }
}