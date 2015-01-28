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

public partial class Pensum_Asignacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form.DefaultButton = btnBuscar.UniqueID;
            ddlSalon.Focus();
            this.cargar();
        }
    }

    private void vertbl_Asignacion()
    {
        try
        {
            Asignacion objAsignacion                    = new Asignacion();
            OperacionAsignacion objOperAsignacion       = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_salon                      = int.Parse(ddlSalon.SelectedValue.ToString());
            objAsignacion.id_docente                    = int.Parse(ddlDocente.SelectedValue.ToString());
            tbl_Asignacion.DataSource = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            tbl_Asignacion.DataBind();
            if (tbl_Asignacion.Rows.Count == 0)
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

    public void cargar () {
        try {
            Salon objSalon                      = new Salon();
            OperacionSalon objOperSalon         = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Materia objMateria                  = new Materia();
            OperacionMateria objOperMateria     = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            Docente objDocente                  = new Docente();
            OperacionDocente objOperDocente     = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            clsFunciones.enlazarCombo(objOperSalon.ConsultarSalon(objSalon),ddlSalon);

            DataTable dtDocente                 = objOperDocente.ConsultarDocente(objDocente);
            dtDocente.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDocente.DataSource               = dtDocente;
            ddlDocente.DataValueField           = "id";
            ddlDocente.DataTextField            = "nombre_completo";
            ddlDocente.DataBind();
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

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Asignacion", Pagina = "Gestion", Accion = "Agregar" });
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtOpcion.Value) == 1)
        {
            Asignacion objAsignacion                = new Asignacion();
            OperacionAsignacion objOperAsignacion   = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            foreach (GridViewRow row in tbl_Asignacion.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        objAsignacion.id            = int.Parse(row.Cells[1].Text);
                        objAsignacion.id_usuario    = int.Parse(Session["id_usuario"].ToString());
                        objOperAsignacion.EliminarAsignacion(objAsignacion);
                    }
                }
            }
            Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Materia", Pagina = "Busqueda", Accion = "Elimino" });
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Asignacion();
    }
    protected void tbl_Asignacion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Pensum", Entidad = "Asignacion", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Asignacion.Rows[e.NewSelectedIndex].Cells[1].Text )});
    }
    protected void tbl_Asignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Asignacion.PageIndex = e.NewPageIndex;
        this.vertbl_Asignacion();
    }
    
}