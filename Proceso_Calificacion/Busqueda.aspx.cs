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

public partial class Proceso_Calificacion_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = btnBuscar.UniqueID;
        if (!IsPostBack)
        {
            this.cargar();
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.vertbl_Grupos_Calificacion();
    }

    public void vertbl_Grupos_Calificacion()
    {
        try
        {
            Asignacion objAsignacion                    = new Asignacion();
            OperacionAsignacion objOperAsignacion       = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_materia                    = int.Parse(ddlMateria.SelectedValue.ToString());
            if (int.Parse(Session["id_usuario_tipo"].ToString()) == 2)
            {
                objAsignacion.id_docente                = int.Parse(this.obtenerId_Docente());
            
            }else {
                objAsignacion.id_docente = int.Parse(ddlDocente.SelectedValue.ToString());
            }
            tbl_Calificacion.DataSource                 = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            tbl_Calificacion.DataBind();
            if (tbl_Calificacion.Rows.Count == 0)
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

    public void cargar()
    {
        try
        {
            if (int.Parse(Session["id_usuario_tipo"].ToString()) == 2)
            {
                docente.Visible = false;
                cargarMateria(obtenerId_Docente());
            }else {
                docente.Visible = true;
                cargarDocente();
            }
        }
        catch (Exception) { }
    }

    public void cargarDocente () {
        try
        {
            Docente objDocente                      = new Docente();
            OperacionDocente objOperDocente         = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable dtDocente                     = objOperDocente.ConsultarDocente(objDocente);
            dtDocente.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDocente.DataSource                   = dtDocente;
            ddlDocente.DataValueField               = "id";
            ddlDocente.DataTextField                = "nombre_completo";
            ddlDocente.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    public void cargarMateria (string id_docente) {
        try
        {
            Asignacion objAsignacion                                    = new Asignacion();
            OperacionAsignacion objOperAsignacion                       = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_docente                                    = int.Parse(id_docente);
            DataTable dt                                                = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            dt.Columns.Add("materia_grado", typeof(string), "materia + ' (' + descripcion_grado+')'");
            ddlMateria.DataValueField                                   = "id_materia";
            ddlMateria.DataTextField                                    = "materia_grado";
            var dv                                                      = dt.DefaultView.ToTable(true, "id_materia", "materia_grado", "materia", "grado").DefaultView;
            dv.Sort                                                     = "materia ASC, grado ASC";
            ddlMateria.DataSource                                       = dv;
            ddlMateria.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    public string obtenerId_Docente()
    {
        string id                                       = "";
        Usuario objUsuario                              = new Usuario();
        OperacionUsuario objOperUsuario                 = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Usuario                            = new GridView();
        objUsuario.id                                   = int.Parse(Session["id_usuario"].ToString());
        tbl_Usuario.DataSource                          = objOperUsuario.ConsultarUsuario(objUsuario);
        tbl_Usuario.DataBind();
        Docente objDocente                              = new Docente();
        OperacionDocente objOperDocente                 = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Docente                            = new GridView();
        objDocente.documento_numero                     = int.Parse(tbl_Usuario.Rows[0].Cells[3].Text);
        tbl_Docente.DataSource                          = objOperDocente.ConsultarDocente(objDocente);
        tbl_Docente.DataBind();
        if (tbl_Docente.Rows.Count == 1) {
            id                                          = tbl_Docente.Rows[0].Cells[0].Text;            
        }
        return id;
    }
    protected void tbl_Calificacion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.RedirectToRoute("General", new { Modulo = "Proceso", Entidad = "Calificacion", Pagina = "Gestion", Accion = "Edita", Id = clsEncriptar.Encriptar(tbl_Calificacion.Rows[e.NewSelectedIndex].Cells[0].Text) });
    }
    protected void tbl_Calificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Calificacion.PageIndex = e.NewPageIndex;
        vertbl_Grupos_Calificacion();
    }
    protected void ddlDocente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlMateria.Items.Clear();
            ListItem l              = new ListItem();
            l.Text                  = "--- SELECCIONE UNO ---";
            l.Value                 = "0";
            ddlMateria.Items.Add(l);
            cargarMateria(ddlDocente.SelectedValue.ToString());
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}