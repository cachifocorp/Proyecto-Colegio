using LogicaNegocio;
using ObjetosNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte_Planilla_Busqueda : System.Web.UI.Page
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
        this.vertbl_Grupos_Planilla();
    }

    public void vertbl_Grupos_Planilla()
    {
        try
        {
            Asignacion objAsignacion = new Asignacion();
            OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_materia = int.Parse(ddlMateria.SelectedValue.ToString());
            if (int.Parse(Session["id_usuario_tipo"].ToString()) == 2)
            {
                objAsignacion.id_docente = int.Parse(this.obtenerId_Docente());

            }
            else
            {
                objAsignacion.id_docente = int.Parse(ddlDocente.SelectedValue.ToString());
            }
            tbl_Planilla.DataSource = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            tbl_Planilla.DataBind();
            if (tbl_Planilla.Rows.Count == 0)
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
            }
            else
            {
                docente.Visible = true;
                cargarDocente();
            }
        }
        catch (Exception) { }
    }

    public void cargarDocente()
    {
        try
        {
            Docente objDocente = new Docente();
            OperacionDocente objOperDocente = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            DataTable dtDocente = objOperDocente.ConsultarDocente(objDocente);
            dtDocente.Columns.Add("nombre_completo", typeof(string), "nombres + ' ' + apellidos");
            ddlDocente.DataSource = dtDocente;
            ddlDocente.DataValueField = "id";
            ddlDocente.DataTextField = "nombre_completo";
            ddlDocente.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void cargarMateria(string id_docente)
    {
        try
        {
            Asignacion objAsignacion = new Asignacion();
            OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objAsignacion.id_docente = int.Parse(id_docente);
            DataTable dt = objOperAsignacion.ConsultarAsignacion(objAsignacion);
            dt.Columns.Add("materia_grado", typeof(string), "materia + ' (' + descripcion_grado+')'");
            ddlMateria.DataValueField = "id_materia";
            ddlMateria.DataTextField = "materia_grado";
            var dv = dt.DefaultView.ToTable(true, "id_materia", "materia_grado", "materia", "grado").DefaultView;
            dv.Sort = "materia ASC, grado ASC";
            ddlMateria.DataSource = dv;
            ddlMateria.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public string obtenerId_Docente()
    {
        string id = "";
        Usuario objUsuario = new Usuario();
        OperacionUsuario objOperUsuario = new OperacionUsuario(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Usuario = new GridView();
        objUsuario.id = int.Parse(Session["id_usuario"].ToString());
        tbl_Usuario.DataSource = objOperUsuario.ConsultarUsuario(objUsuario);
        tbl_Usuario.DataBind();
        Docente objDocente = new Docente();
        OperacionDocente objOperDocente = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        GridView tbl_Docente = new GridView();
        objDocente.documento_numero = int.Parse(tbl_Usuario.Rows[0].Cells[3].Text);
        tbl_Docente.DataSource = objOperDocente.ConsultarDocente(objDocente);
        tbl_Docente.DataBind();
        if (tbl_Docente.Rows.Count == 1)
        {
            id = tbl_Docente.Rows[0].Cells[0].Text;
        }
        return id;
    }
    protected void tbl_Planilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        tbl_Planilla.PageIndex = e.NewPageIndex;
        vertbl_Grupos_Planilla();
    }
    protected void ddlDocente_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlMateria.Items.Clear();
            ListItem l = new ListItem();
            l.Text = "--- SELECCIONE UNO ---";
            l.Value = "0";
            ddlMateria.Items.Add(l);
            cargarMateria(ddlDocente.SelectedValue.ToString());
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsFunciones.planilla = "";
        foreach (GridViewRow row in tbl_Planilla.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkMateria") as CheckBox);
                if (chkRow.Checked)
                {
                    clsFunciones.planilla += generarPlanilla(int.Parse(row.Cells[1].Text)) + " <br style='page-break-after: always;'/>";
                    Response.RedirectToRoute("General", new { Modulo = "Reporte", Entidad = "Planilla", Pagina = "Gestion", Accion = "Edita" });

                }
            }
        }
    }

    public string generarPlanilla(int asignacion)
    {
        string html = "";
        Asignacion objAsignacion = new Asignacion();
        OperacionAsignacion objOperAsignacion = new OperacionAsignacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objAsignacion.id = asignacion;
        DataTable dtsEstudiante = objOperAsignacion.ConsultarEstudiante(objAsignacion);
        Colegio objColegio = new Colegio();
        OperacionColegio objOperColegio = new OperacionColegio(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        DataTable dt = objOperColegio.ConsultarColegio(objColegio);
        Anio_Escolar objAnio_Escolar = (Anio_Escolar)Session["anioEscolar"];
        DataTable dts_Asignacion = objOperAsignacion.ConsultarAsignacion(objAsignacion);
        Salon objSalon = new Salon();
        OperacionSalon objOperSalon = new OperacionSalon(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objSalon.id = int.Parse(dts_Asignacion.Rows[0].ItemArray[1].ToString());
        DataTable dts_Salon = objOperSalon.ConsultarSalon(objSalon);
        Docente objDocente = new Docente();
        OperacionDocente objOperDocente = new OperacionDocente(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);

        if (int.Parse(Session["id_usuario_tipo"].ToString()) == 2)
        {
            objDocente.id = int.Parse(this.obtenerId_Docente());

        }
        else
        {
            objDocente.id = int.Parse(ddlDocente.SelectedValue.ToString());
        }
        //objDocente.id = int.Parse(dts_Salon.Rows[0].ItemArray[4].ToString());
        DataTable dts_Docente = objOperDocente.ConsultarDocente(objDocente);
        Materia objMateria = new Materia();
        OperacionMateria objOperMateria = new OperacionMateria(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objMateria.id = int.Parse(dts_Asignacion.Rows[0].ItemArray[2].ToString());
        DataTable dts_Materia = objOperMateria.ConsultarMateria(objMateria);

        Anio_Escolar_Periodo objAnio_Escolar_Periodo = new Anio_Escolar_Periodo();
        OperacionAnio_Escolar_Periodo objOperAnio_Escolar_Periodo = new OperacionAnio_Escolar_Periodo(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
        objAnio_Escolar_Periodo.id_anio_escolar = objAnio_Escolar.id;
        DataTable dts_Periodo = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Actual(objAnio_Escolar_Periodo);
        DataTable dts_Periodo_Anterior = objOperAnio_Escolar_Periodo.ConsultarPeriodo_Anterior(objAnio_Escolar_Periodo);
        GridView tbl_Promedio = new GridView();
        DataView promedio = new DataView();
        if (objOperAnio_Escolar_Periodo.ConsultarAnio_Escolar_Periodo(objAnio_Escolar_Periodo).Rows.Count > 1)
        {
            Calificacion objCalificacion = new Calificacion();
            OperacionCalificacion objOperCalificacion = new OperacionCalificacion(ConfigurationManager.ConnectionStrings["estigioacademicoConnectionString"].ConnectionString);
            objCalificacion.id_usuario = int.Parse(dts_Asignacion.Rows[0].ItemArray[0].ToString());
            objCalificacion.id_asignacion = int.Parse(dts_Periodo_Anterior.Rows[0].ItemArray[0].ToString());
            promedio = objOperCalificacion.ConsultarPromedio_PeriodoAcumulado(objCalificacion).DefaultView;

        }
        string estilo = " style = 'border:1px solid #000; text-align: center'";
        string htmlencabezado = "";

        htmlencabezado += " <table width='100%' height='100%' style='font-size:10px; font-family:Calibri ;border-collapse:collapse;' width='100%'><tr><td width='20%' style = 'text-align:center'><img alt='logo' src = 'http://academico.itipuentenacional.edu.co/img/logo.png'  width='60' height='60' ></td><td width='80%' >";
        htmlencabezado += " <table width='100%' width='100%' height='100%' style='font-size:10px; font-family:Calibri ;border-collapse:collapse;border: 1px solid #000'>";
        htmlencabezado += "<tr>";
        htmlencabezado += "<td colspan='3' style = 'text-align: center ; font-weight:bold' ><h4>" + dt.Rows[0].ItemArray[1].ToString() + "</h4></td>";
        htmlencabezado += " </tr>";
        htmlencabezado += " <tr>";
        htmlencabezado += "  <td style = 'border:1px solid #000;'><strong>MATERIA:</strong> " + dts_Materia.Rows[0].ItemArray[1].ToString() + "</td>";
        htmlencabezado += "  <td style = 'border:1px solid #000;'><strong>SALÓN:</strong> " + dts_Salon.Rows[0].ItemArray[1].ToString() + "</td>";
        htmlencabezado += "   <td style = 'border:1px solid #000;'><strong>AÑO:</strong> " + objAnio_Escolar.descripcion + "</td>";
        htmlencabezado += "  </tr>";
        htmlencabezado += "  <tr>";
        htmlencabezado += "  <td style = 'border:1px solid #000;'><strong>DOCENTE:</strong> " + dts_Docente.Rows[0].ItemArray[3].ToString() + " " + dts_Docente.Rows[0].ItemArray[4].ToString() + "</td>";
        if (dts_Periodo.Rows.Count > 0)
        {
            htmlencabezado += "  <td  style = 'border:1px solid #000;'><strong>PERIODO:</strong> " + dts_Periodo.Rows[0].ItemArray[1].ToString() + "</td>";

        }
        else
        {
            htmlencabezado += "  <td  style = 'border:1px solid #000;'><strong>PERIODO:</strong> </td>";
        }
        DateTime date = DateTime.Now;
        htmlencabezado += "<td><strong>Fecha Impresión:</strong>  "+ date.ToString("yyyy/MM/dd HH:mm")+"</td>";
        htmlencabezado += "</tr>";      
        htmlencabezado += "</table>";
        htmlencabezado += "</td></tr></table>";

        string htmlcuerpo = "<table width='100%' height='100%' style='font-size:12px; font-family:Calibri ;border-collapse:collapse; border: 0.5px solid #000'>";
        htmlcuerpo += "<tr>";
        htmlcuerpo += "<td bgcolor='#d6e3bc' " + estilo + " width = '5%'>#</td>";
        htmlcuerpo += "<td bgcolor='#d6e3bc' " + estilo + " width = '30%'> <strong>ESTUDIANTE</strong></td>";
        htmlcuerpo += "</tr>";
        for (int i = 0; i < dtsEstudiante.Rows.Count; i++)
        {
            htmlcuerpo += "<tr>";
            htmlcuerpo += "<td " + estilo + "><strong>" + (i + 1) + "</strong></td>";
            htmlcuerpo += "<td style = 'border:1px solid #000; font-size:12px'> " + dtsEstudiante.Rows[i].ItemArray[4] + " " + dtsEstudiante.Rows[i].ItemArray[5]
            + " " + dtsEstudiante.Rows[i].ItemArray[2] + " " + dtsEstudiante.Rows[i].ItemArray[3] + "</td>";             
            htmlcuerpo += "</tr>";
        }
        htmlcuerpo += "</table>";
        html += htmlencabezado + "<br>" + htmlcuerpo;
        return html;
    }
}