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

public partial class Asignacion_Salon_Busqueda : System.Web.UI.Page
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
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Salon objSalon = new Salon();
            OperacionSalon objOperSalon = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Salon.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objSalon.id                 = int.Parse(row.Cells[1].Text);
                        objSalon.id_usuario         = int.Parse(Session["id_usuario"].ToString());
                        objOperSalon.EliminarSalon(objSalon);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Salon();
    }
    protected void tbl_Salon_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Asignacion", Entidad = "Salon", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Salon.Rows[e.NewSelectedIndex].Cells[1].Text) });
    }
    protected void tbl_Salon_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Salon.PageIndex = e.NewPageIndex;
        vertbl_Salon();
    }

    public void vertbl_Salon () {
        try {
            Salon objSalon                                  = new Salon();
            OperacionSalon objOperSalon                     = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objSalon.id_sede                                = int.Parse(ddlSede.SelectedValue.ToString());
            objSalon.id_jornada                             = int.Parse(ddlJornada.SelectedValue.ToString());
            objSalon.id_director                            = int.Parse(ddlDirector.SelectedValue.ToString());
            objSalon.id_grado                               = int.Parse(ddlGrado.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtDescripcion.Text))
            {
                objSalon.descripcion                        = txtDescripcion.Text.Trim();
            }
            else
            {
                objSalon.descripcion                        = null;
            }
            tbl_Salon.DataSource                            = objOperSalon.ConsultarSalon(objSalon);
            tbl_Salon.DataBind();
            if (tbl_Salon.Rows.Count == 0) {
                this.ShowNotification("Datos", Resources.Mensaje.msjNoDatos, "success");
            }
        }
        catch (Exception) {}
    }

    public void cargar () {
        try {
            Anio_Escolar objAnio_Escolar                    = (Anio_Escolar)Session["anioEscolar"];
            Sede objSede                                    = new Sede();
            OperacionSede objOperSede                       = new OperacionSede(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            this.enlazarCombo(objOperSede.ConsultarSede(objSede),ddlSede);
            DataView dtv_Municipio                          = ((DataTable)Session["listado"]).DefaultView;
            dtv_Municipio.RowFilter                         = "id_tipo_listado=7";
            this.enlazarCombo(dtv_Municipio, ddlJornada);
            Docente objDocente                              = new Docente();
            OperacionDocente objOperDocente                 = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable dt                                    = objOperDocente.ConsultarDocente(objDocente);
            dt.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDirector.DataSource                          = dt;
            ddlDirector.DataValueField                      = "id";
            ddlDirector.DataTextField                       = "nombre_completo";
            ddlDirector.DataBind();
            Grado objGrado                                  = new Grado();
            OperacionGrado objOperGrado                     = new OperacionGrado(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objGrado.id_anio_escolar                        = objAnio_Escolar.id;
            this.enlazarCombo(objOperGrado.ConsultarGrado(objGrado),ddlGrado);
        }
        catch (Exception) {}
    }

    private void ShowNotification(string title, string msg, string nt)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "pnotifySuccess('" + title + "','" + msg + "','" + nt.ToString() + "');", true);
    }

    public void enlazarCombo(DataTable dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }

    public void enlazarCombo(DataView dts, DropDownList ddlCombo)
    {
        ddlCombo.DataSource             = dts;
        ddlCombo.DataValueField         = "id";
        ddlCombo.DataTextField          = "descripcion";
        ddlCombo.DataBind();
    }
}   
