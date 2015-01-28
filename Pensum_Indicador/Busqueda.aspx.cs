using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObjetosNegocio;
using LogicaNegocio;
using System.Configuration;


public partial class Pensum_Indicador_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton              = btnBuscar.UniqueID;
        if (!IsPostBack)
        {
            cargar();
        }
    }

    public void cargar () {
        try
        {
            Anio_Escolar objAnio_Escolar                                    = (Anio_Escolar)Session["anioEscolar"];
            Anio_Escolar_Periodo objAnio_Escolar_Periodo                    = new Anio_Escolar_Periodo();
            objAnio_Escolar_Periodo.id_anio_escolar                         = objAnio_Escolar.id;
            OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo       = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo), ddlPeriodo);
            Grado objGrado                                                  = new Grado();
            OperacionGrado objOperGrado                                     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                                        = objAnio_Escolar.id;
            clsFunciones.enlazarCombo(objOperGrado.ConsultarGrado(objGrado), ddlGrado);
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
            Indicador objIndicador                  = new Indicador();
            OperacionIndicador objOperIndicador     = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Indicador.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objIndicador.id = int.Parse(row.Cells[1].Text);
                        objIndicador.id_usuario = int.Parse(Session["id_usuario"].ToString());
                        objOperIndicador.EliminarIndicador(objIndicador);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Gestion", Accion = "Agregar" });
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        vertbl_Indicador();
    }

    private void vertbl_Indicador()
    {
        try
        {   
            Indicador objIndicador                              = new Indicador();
            OperacionIndicador objOperIndicador                 = new OperacionIndicador(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objIndicador.id_anio_escolar_periodo                = int.Parse(ddlPeriodo.SelectedValue.ToString());
            objIndicador.id_grado                               = int.Parse(ddlGrado.SelectedValue.ToString());
            objIndicador.id_materia                             = int.Parse(ddlMateria.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objIndicador.descripcion          = txtDescripcion.Text.Trim();
            }
            else
            {
                objIndicador.descripcion = null;
            }
            tbl_Indicador.DataSource = objOperIndicador.ConsultarIndicador(objIndicador);
            tbl_Indicador.DataBind();
            if (tbl_Indicador.Rows.Count == 0)
            {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception)
        {
        }
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }
    protected void tbl_Indicador_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Indicador", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Indicador.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Indicador_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Indicador.PageIndex = e.NewPageIndex;
        vertbl_Indicador();
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